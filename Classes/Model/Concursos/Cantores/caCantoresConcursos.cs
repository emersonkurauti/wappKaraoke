using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wappKaraoke.Classes.Model.CantoresConcursos
{
    public static class caCantoresConcursos
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
            get { return false; }
        }

        public static bool _bControlaTransacao
        {
            get { return false; }
        }

        public static string CC_cdRegistro
        {
            get { return "CC_cdRegistro"; }
        }

        public static string nmTabela
        {
            get { return "CANTORESCONCURSOS"; }
        }

        public static string nmCampoChave
        {
            get { return "cdCantor"; }
        }

        public static string dePrincipal
        {
            get { return "CC_nmAssociacao"; }
        }

        public static string deChaveComposta
        {
            get { return "[ChComposta]"; }
        }

		/// <summary>
		/// Atributos
	 	/// </summary>       
        public static string CC_Controle
        {
            get { return "CC_Controle"; }
        }
		public static string cdCantor
        {
            get { return "cdCantor"; }
        }
		public static string cdConcurso
        {
            get { return "cdConcurso"; }
        }
		public static string cdAssociacao
        {
            get { return "cdAssociacao"; }
        }
		public static string CC_nmCantor
        {
            get { return "CC_nmCantor"; }
        }
		public static string CC_nmAssociacao
        {
            get { return "CC_nmAssociacao"; }
        }

        /// <summary>
        /// Retorna os fields para montar DataGridView
        /// </summary>
        /// <param name="strFields"></param>
        /// <param name="strVisivel"></param>
        /// <param name="strNome"></param>
        public static void RetornarFields()
        {
            _strFields = CC_Controle + "," + CC_cdRegistro + "," + cdCantor + "," + cdConcurso + "," + cdAssociacao + "," + CC_nmCantor + "," + CC_nmAssociacao;

            _strNome = "Controle, Cd. Registro, cdCantor, cdConcurso, cdAssociacao, CC_nmCantor, CC_nmAssociacao";

            _strVisivel = "0, 0, 1, 1, 1, 1, 1";
        }
    }
}