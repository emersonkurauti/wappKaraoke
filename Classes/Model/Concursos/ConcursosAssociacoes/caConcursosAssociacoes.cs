using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wappKaraoke.Classes.Model.ConcursosAssociacoes
{
    public static class caConcursosAssociacoes
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
            get { return "CONCURSOSASSOCIACOES"; }
        }

        public static string nmCampoChave
        {
            get { return "cdConcurso"; }
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
		public static string cdConcurso
        {
            get { return "cdConcurso"; }
        }
		public static string cdAssociacao
        {
            get { return "cdAssociacao"; }
        }
		public static string deEmail
        {
            get { return "deEmail"; }
        }
		public static string nmRepresentante
        {
            get { return "nmRepresentante"; }
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
            _strFields = CC_cdRegistro  + "," + cdConcurso + "," + cdAssociacao + "," + deEmail + "," + nmRepresentante + "," + CC_nmAssociacao;

            _strNome = "Cd. Registro, cdConcurso, cdAssociacao, deEmail, nmRepresentante, CC_nmAssociacao";

            _strVisivel = "0, 1, 1, 1, 1, 1";
        }
    }
}