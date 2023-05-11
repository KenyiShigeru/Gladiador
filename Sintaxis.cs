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
                    texto = texto.Substring(1);
                    texto = EliminarSiguiente();
                    try
                    {
                        INSTRUCCIONES();
                        if (texto[0] != '}')
                        {
                            throw new Exception("Falta la cerradura del bloque de instruccion }");
                        }
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
            /*if(Regex.IsMatch(texto,Patrones.id))
            {
                encontrado = Regex.Match(texto, "^"+Patrones.id).Value;
                if(encontrado == "imprimir")
                {
                    ESCRIBIR();
                }
                else if (encontrado == "leer")
                {

                }
                else if (encontrado == "mientras")
                {

                }
                else
                {
                    if (ASIGNACION())
                    {

                    }
                }
            }*/
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

        }



        private String Quitar(String quitar)
        {
            return texto.Substring(quitar.Length);
        }

        private String EliminarSiguiente()
        {
            return Regex.IsMatch(texto, "^(\\n|\\s)")? texto.Substring(1): texto;
        }


        
    }
}
