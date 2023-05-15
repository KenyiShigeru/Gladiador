using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Gladiador
{
    internal class Sintaxis
    {
        private String texto;
        private String encontrado;
        private bool leyendoinstrucciones;
        public Sintaxis(String texto) 
        {
            this.texto = texto;
        }
        public void Analizar()
        {
            PROGRAMA();
        }
        private void PROGRAMA()
        {
            if (Regex.IsMatch(texto, "^clase(\\n|\\s)"))
            {
                encontrado = Regex.Match(texto, "^clase(\\n|\\s)").Value;
                texto = Quitar(encontrado);
                if (Regex.IsMatch(texto, "^"+Patrones.id))
                {
                    if(Regex.IsMatch(texto,"^(\\n|\\s)"))
                        texto = EliminarSiguiente();
                    encontrado = Regex.Match(texto, Patrones.id).Value;
                    texto = Quitar(encontrado);
                    BLOQUE();
                }
                else
                    throw new Exception("Se esperaba un nombre para la clase");
            }
            else
                throw new Exception("Se esperaba que el programa Iniciara con la palabra clase");
        }


        private void BLOQUE()
        {
            this.leyendoinstrucciones = true;
            texto = EliminarSiguiente();
            try
            {
                if (texto[0] != '{')
                    throw new Exception("Se esperaba un {");
                else
                {
                    texto = avanzar();
                    texto = EliminarSiguiente();
                    try
                    {
                        INSTRUCCIONES();
                        if (texto[0] != '}')
                        {
                            throw new Exception("Falta la cerradura del bloque de instruccion }");
                        }
                        return;
                    }
                    catch
                    {
                        throw new Exception("El programa debe terminar con una cerradura }");
                    }
                }
            }
            catch
            {
                throw new Exception("Error en el analisis");
            }
        }


        private void INSTRUCCIONES()
        {
            texto = EliminarSiguiente();
            encontrado = Regex.Match(texto, "^" + Patrones.id).Value;
            if (Regex.IsMatch(texto,Patrones.id))
            {
                try
                {
                    numeracionReservadas palabra = (numeracionReservadas)Patrones.encontrarReservada(encontrado);
                    switch (palabra)
                    {
                        case numeracionReservadas.mientras:
                            texto = EliminarSiguiente();
                            CICLO();
                            break;
                        case numeracionReservadas.si:
                            break;
                        case numeracionReservadas.leer:
                            break;
                        default:
                            MessageBox.Show("Es una asignacion por descarte");
                            texto = Quitar(encontrado);
                            break;
                    }
                }
                catch
                {
                }
            }
            leyendoinstrucciones = false;
        }

        

        private void ENTRADA()
        {

        }

        private void ESCRIBIR()
        {

        }

        private void CICLO()
        {
            texto = Quitar(encontrado);
            texto = EliminarSiguiente();
            COMPARACION();
            BLOQUE();
        }

        private void ASIGNACION()
        {


            #region Documentacion del funcionamiento ASIGNACION
            
            #endregion
        }

        private void DECLARACION()
        {


            #region Documentacion funcionamiento DECLARACION
            /*
                    tipo -> int | boleano | caracter | flotante
                    Declaracion -> tipo identificador;

                VAMOS A EMPEZAR BUSCANDO ESAS PALABRAS RESERVADAS
                SI LAS ENCUENTRA PASAMOS A LA DECLARACION QUE ES
                PONER UNA IDENTIFICADOR QUE SERIA EL NOMBRE DE LA 
                VARIABLE QUE SERÁ RECONOCIBLE PARA EL PROGRAMADOR DE
                ALLÍ QUE SE LLAME IDENTIFICADOR
             */
            #endregion
        }

        private void COMPARACION()
        {
            texto = EliminarSiguiente();
            if (Patrones.especiales.ElementAt((int)numeracionespeciales.parentesisabre) == texto[0])
            {
                texto = EliminarSiguiente();
                String patron = "^(" + Patrones.id + "|" + Patrones.numero +//Separe el codigo para que sea mas pequeno
                    ")";
                texto = avanzar();
                encontrado = Regex.IsMatch(texto,patron)? Regex.Match(texto,patron).Value: "";
                if (encontrado == "")
                    throw new Exception("No se encontro una instruccion valida");
                else
                {
                    if (Regex.IsMatch(texto,Patrones.id))
                    {
                        texto = Quitar(encontrado);
                        texto = EliminarSiguiente();
                        if (Patrones.especiales.ElementAt((int)numeracionespeciales.igual) == texto[0])
                        {
                            texto = avanzar();
                            try {
                                COMPARACION(); 
                            } 
                            catch 
                            {
                                encontrado = Regex.IsMatch(texto, patron) ? Regex.Match(texto, patron).Value : "";
                                texto = Quitar(encontrado);
                                texto = avanzar();
                                return;
                            }
                            texto = avanzar();
                            texto = EliminarSiguiente();
                            texto = avanzar();
                            texto = EliminarSiguiente();
                            encontrado = Regex.IsMatch(texto, patron) ? Regex.Match(texto, patron).Value : "";
                            texto = Quitar(encontrado);
                        }
                    }
                }
                if (texto[0] != Patrones.especiales.ElementAt((int)numeracionespeciales.parentesiscierre))
                    throw new Exception("Se esperaba un )");
                texto = avanzar();
                return;
            }
            else
                throw new Exception("Se esperaba iniciara con un parentesis (");
        }



        private String Quitar(String quitar)
        {
            return texto.Substring(quitar.Length);
        }

        private String EliminarSiguiente()
        {
            String expr = "^(\\n|\\s)";
            return Regex.IsMatch(texto, expr)? 
                texto.Substring(Regex.Match(texto, expr).Length) : texto;
        }

        private String avanzar()
        {
            return texto.Substring(1);
        }
        
    }
}
