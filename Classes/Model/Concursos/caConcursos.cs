using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wappKaraoke.Classes.Model.Concursos
{
    public static class caConcursos
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
            get { return false; }
        }

        public static string CC_cdRegistro
        {
            get { return "CC_cdRegistro"; }
        }

        public static string nmTabela
        {
            get { return "CONCURSOS"; }
        }

        public static string nmCampoChave
        {
            get { return "cdConcurso"; }
        }

        public static string dePrincipal
        {
            get { return "nmConcurso"; }
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
		public static string dtIniConcurso
        {
            get { return "dtIniConcurso"; }
        }
		public static string flFinalizado
        {
            get { return "flFinalizado"; }
        }
        public static string flConcursoCorrente
        {
            get { return "flConcursoCorrente"; }
        }
		public static string dtFimConcurso
        {
            get { return "dtFimConcurso"; }
        }
		public static string cdConcurso
        {
            get { return "cdConcurso"; }
        }
		public static string nmConcursoKanji
        {
            get { return "nmConcursoKanji"; }
        }
		public static string nmConcurso
        {
            get { return "nmConcurso"; }
        }
        public static string cdFaseCorrente
        {
            get { return "cdFaseCorrente"; }
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
            _strFields = CC_cdRegistro + "," + cdCidade + "," + dtIniConcurso + "," + flFinalizado + "," + flConcursoCorrente + "," + dtFimConcurso + "," + cdConcurso + "," + nmConcursoKanji + "," + nmConcurso + "," +cdFaseCorrente + "," + CC_nmCidade;

            _strNome = "Cd. Registro, cdCidade, dtIniConcurso, flFinalizado, flConcursoCorrente, dtFimConcurso, cdConcurso, nmConcursoKanji, nmConcurso, cdFaseCorrente, CC_nmCidade";

            _strVisivel = "0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1";
        }
    }
}