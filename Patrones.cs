﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gladiador
{
    internal class Patrones
    {
        public static String id = "([a-z]|[A-Z]|ñ|Ñ)+([A-Z]|_|[a-z]|[1-9]|ñ|Ñ)*";
        public static String opRelac = "(<=|>=|==|!=)";
        public String comentario = "(\\/\\*(\\s*|.*?)*\\*\\/)|(\\/\\/.*)";
        //Palabras reservadas

        public static LinkedList<String> tipodedato = new LinkedList<String>(new String[] {"entero", "flotante", "caracter", "boleano"});

        public static LinkedList<String> reservada = new LinkedList<String>(new String[] {"clase","mientras","si","entonces", "leer","imprimir"});

        

        #region Caracteres especiales
        
        
        public static LinkedList<char> especiales = new LinkedList<char>(new char[] {'(',')','{','}','[',']',','});

        #endregion

        #region Declaracion de los numeros
        public static String digito = "[0-9]"; 
        public static String digitos = "(" + digito + "+)";
        public static String decimales = "(\\." + digitos + "|)";
        public static String exponente = "(E(\\+|\\-|)" + digitos + "|)";
        public static String numero = digitos + decimales + exponente;
        #endregion
    }

    public enum TipoToken
    {
        Inicio, id, opRelac, comentario, asignacion, reservada, numerico, caracter_especial
    }
}
