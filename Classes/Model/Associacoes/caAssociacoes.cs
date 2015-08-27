using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wappKaraoke.Classes.Model.Associacoes
{
    public static class caAssociacoes
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
            get { return "ASSOCIACOES"; }
        }

        public static string nmCampoChave
        {
            get { return "cdAssociacao"; }
        }

        public static string dePrincipal
        {
            get { return "nmAssociacao"; }
        }

        public static string deChaveComposta
        {
            get { return "[ChComposta]"; }
        }

		/// <summary>
		/// Atributos
	 	/// </summary>        
		public static string nuEnderecoRepresentante
        {
            get { return "nuEnderecoRepresentante"; }
        }
		public static string nmAssociacao
        {
            get { return "nmAssociacao"; }
        }
        public static string deSiglaAssociacao
        {
            get { return "deSiglaAssociacao"; }
        }
		public static string cdAssociacao
        {
            get { return "cdAssociacao"; }
        }
		public static string nuCEPRepresentante
        {
            get { return "nuCEPRepresentante"; }
        }
		public static string nuEnderecoPresidente
        {
            get { return "nuEnderecoPresidente"; }
        }
		public static string nmRepresentante
        {
            get { return "nmRepresentante"; }
        }
		public static string deComplementoRepresentante
        {
            get { return "deComplementoRepresentante"; }
        }
		public static string nmPresidente
        {
            get { return "nmPresidente"; }
        }
		public static string deRuaRepresentante
        {
            get { return "deRuaRepresentante"; }
        }
		public static string deBairroRepresentante
        {
            get { return "deBairroRepresentante"; }
        }
		public static string deBairroPresidente
        {
            get { return "deBairroPresidente"; }
        }
		public static string nuCEPPresidente
        {
            get { return "nuCEPPresidente"; }
        }
		public static string deRuaPresidente
        {
            get { return "deRuaPresidente"; }
        }
		public static string deComplementoPresidente
        {
            get { return "deComplementoPresidente"; }
        }

        /// <summary>
        /// Retorna os fields para montar DataGridView
        /// </summary>
        /// <param name="strFields"></param>
        /// <param name="strVisivel"></param>
        /// <param name="strNome"></param>
        public static void RetornarFields()
        {
            _strFields = CC_cdRegistro + "," + nuEnderecoRepresentante + "," + nmAssociacao + "," + deSiglaAssociacao + "," + cdAssociacao + "," + nuCEPRepresentante + "," + nuEnderecoPresidente + "," + nmRepresentante + "," + deComplementoRepresentante + "," + nmPresidente + "," + deRuaRepresentante + "," + deBairroRepresentante + "," + deBairroPresidente + "," + nuCEPPresidente + "," + deRuaPresidente + "," + deComplementoPresidente;

            _strNome = "Cd. Registro, nuEnderecoRepresentante, nmAssociacao, deSiglaAssociacao, cdAssociacao, nuCEPRepresentante, nuEnderecoPresidente, nmRepresentante, deComplementoRepresentante, nmPresidente, deRuaRepresentante, deBairroRepresentante, deBairroPresidente, nuCEPPresidente, deRuaPresidente, deComplementoPresidente";

            _strVisivel = "0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1";
        }
    }
}