using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wappKaraoke.Classes.Model.Jurados
{
    public static class caJurados
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
            get { return "JURADOS"; }
        }

        public static string nmCampoChave
        {
            get { return "cdJurado"; }
        }

        public static string dePrincipal
        {
            get { return "nmJurado"; }
        }

        public static string deChaveComposta
        {
            get { return "[ChComposta]"; }
        }

		/// <summary>
		/// Atributos
	 	/// </summary>        
		public static string cdCidade
        {
            get { return "cdCidade"; }
        }
		public static string nuTelefone
        {
            get { return "nuTelefone"; }
        }
		public static string nmJurado
        {
            get { return "nmJurado"; }
        }
		public static string nmNomeKanji
        {
            get { return "nmNomeKanji"; }
        }
		public static string cdJurado
        {
            get { return "cdJurado"; }
        }
		public static string CC_nmCidade
        {
            get { return "CC_nmCidade"; }
        }

        /// <summary>
        /// Retorna os fields para montar DataGridView
        /// </summary>
        /// <param name="strFields"></param>
        /// <param name="strVisivel"></param>
        /// <param name="strNome"></param>
        public static void RetornarFields()
        {
            _strFields = CC_cdRegistro  + "," + cdCidade + "," + nuTelefone + "," + nmJurado + "," + nmNomeKanji + "," + cdJurado + "," + CC_nmCidade;

            _strNome = "Cd. Registro, cdCidade, nuTelefone, nmJurado, nmNomeKanji, cdJurado, CC_nmCidade";

            _strVisivel = "0, 1, 1, 1, 1, 1, 1";
        }
    }
}