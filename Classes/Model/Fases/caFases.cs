using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wappKaraoke.Classes.Model.Fases
{
    public static class caFases
    {
        private static string _strFields = "";
        public static string strFields
        {
            get { return _strFields; }
            set { _strFields = value; }
        }

        private static string _strVisivel = "";
        public static string strVisivel
        {
            get { return _strVisivel; }
            set { _strVisivel = value; }
        }

        private static string _strNome = "";
        public static string strNome
        {
            get { return _strNome; }
            set { _strNome = value; }
        }

        public static bool _bGeraChave
        {
            get { return true; }
        }

        public static bool _bControlaTransacao
        {
            get { return true; }
        }

        public static string CC_cdRegistro
        {
            get { return "CC_cdRegistro"; }
        }

        public static string nmTabela
        {
            get { return "FASES"; }
        }

        public static string nmCampoChave
        {
            get { return "cdFase"; }
        }

        public static string dePrincipal
        {
            get { return "deFase"; }
        }

        public static string deChaveComposta
        {
            get { return "[ChComposta]"; }
        }

		/// <summary>
		/// Atributos
	 	/// </summary>        
		public static string deFase
        {
            get { return "deFase"; }
        }
		public static string cdFase
        {
            get { return "cdFase"; }
        }

        /// <summary>
        /// Retorna os fields para montar DataGridView
        /// </summary>
        /// <param name="strFields"></param>
        /// <param name="strVisivel"></param>
        /// <param name="strNome"></param>
        public static void RetornarFields()
        {
            _strFields = CC_cdRegistro  + "," + deFase + "," + cdFase;

            _strNome = "Cd. Registro, deFase, cdFase";

            _strVisivel = "0, 1, 1";
        }
    }
}