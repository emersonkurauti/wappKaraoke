using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wappKaraoke.Classes.Model.Cantores
{
    public static class caCantores
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
            get { return "CANTORES"; }
        }

        public static string nmCampoChave
        {
            get { return "cdCantor"; }
        }

        public static string dePrincipal
        {
            get { return "nmCantor"; }
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
		public static string cdCantor
        {
            get { return "cdCantor"; }
        }
		public static string nmNomeKanji
        {
            get { return "nmNomeKanji"; }
        }
		public static string nuTelefone
        {
            get { return "nuTelefone"; }
        }
		public static string nuRG
        {
            get { return "nuRG"; }
        }
		public static string dtNascimento
        {
            get { return "dtNascimento"; }
        }
		public static string nmCantor
        {
            get { return "nmCantor"; }
        }
		public static string deEmail
        {
            get { return "deEmail"; }
        }
		public static string nmNomeArtistico
        {
            get { return "nmNomeArtistico"; }
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
            _strFields = CC_cdRegistro  + "," + cdCidade + "," + cdCantor + "," + nmNomeKanji + "," + nuTelefone + "," + nuRG + "," + dtNascimento + "," + nmCantor + "," + deEmail + "," + nmNomeArtistico + "," + CC_nmCidade;

            _strNome = "Cd. Registro, cdCidade, cdCantor, nmNomeKanji, nuTelefone, nuRG, dtNascimento, nmCantor, deEmail, nmNomeArtistico, CC_nmCidade";

            _strVisivel = "0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1";
        }
    }
}