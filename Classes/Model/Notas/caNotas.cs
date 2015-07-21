using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wappKaraoke.Classes.Model.Notas
{
    public static class caNotas
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
            get { return "NOTAS"; }
        }

        public static string nmCampoChave
        {
            get { return "cdConcurso"; }
        }

        public static string dePrincipal
        {
            get { return "nuNota"; }
        }

        public static string deChaveComposta
        {
            get { return "cdCantor;cdFase;cdCategoria;cdJurado"; }
        }

		/// <summary>
		/// Atributos
	 	/// </summary>        
		public static string cdCategoria
        {
            get { return "cdCategoria"; }
        }
		public static string cdFase
        {
            get { return "cdFase"; }
        }
		public static string cdCantor
        {
            get { return "cdCantor"; }
        }
		public static string cdConcurso
        {
            get { return "cdConcurso"; }
        }
		public static string cdJurado
        {
            get { return "cdJurado"; }
        }
		public static string deObservacao
        {
            get { return "deObservacao"; }
        }
		public static string nuNota
        {
            get { return "nuNota"; }
        }
		public static string CC_deCategoria
        {
            get { return "CC_deCategoria"; }
        }
		public static string CC_deFase
        {
            get { return "CC_deFase"; }
        }
		public static string CC_nmCantor
        {
            get { return "CC_nmCantor"; }
        }
		public static string CC_nmConcurso
        {
            get { return "CC_nmConcurso"; }
        }
		public static string CC_nmJurado
        {
            get { return "CC_nmJurado"; }
        }
        public static string CC_deFormula
        {
            get { return "CC_deFormula"; }
        }

        /// <summary>
        /// Retorna os fields para montar DataGridView
        /// </summary>
        /// <param name="strFields"></param>
        /// <param name="strVisivel"></param>
        /// <param name="strNome"></param>
        public static void RetornarFields()
        {
            _strFields = CC_cdRegistro + "," + cdCategoria + "," + cdFase + "," + cdCantor + "," + cdConcurso + "," + cdJurado + "," + deObservacao + "," + nuNota + "," + CC_deCategoria + "," + CC_deFase + "," + CC_nmCantor + "," + CC_nmConcurso + "," + CC_nmJurado + "," + CC_deFormula;

            _strNome = "Cd. Registro, cdCategoria, cdFase, cdCantor, cdConcurso, cdJurado, deObservacao, nuNota, CC_deCategoria, CC_deFase, CC_nmCantor, CC_nmConcurso, CC_nmJurado, CC_deFormula";

            _strVisivel = "0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1";
        }
    }
}