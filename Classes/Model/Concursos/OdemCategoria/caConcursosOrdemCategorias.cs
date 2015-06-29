using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wappKaraoke.Classes.Model.ConcursosOrdemCategorias
{
    public static class caConcursosOrdemCategorias
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
            get { return "CONCURSOSORDEMCATEGORIAS"; }
        }

        public static string nmCampoChave
        {
            get { return "cdConcurso"; }
        }

        public static string dePrincipal
        {
            get { return "nuOrdem"; }
        }

        public static string deChaveComposta
        {
            get { return "cdCategoria"; }
        }

		/// <summary>
		/// Atributos
	 	/// </summary>        
        public static string CC_Controle
        {
            get { return "CC_Controle"; }
        }
		public static string cdConcurso
        {
            get { return "cdConcurso"; }
        }
		public static string cdCategoria
        {
            get { return "cdCategoria"; }
        }
		public static string nuOrdem
        {
            get { return "nuOrdem"; }
        }
		public static string CC_deCategoria
        {
            get { return "CC_deCategoria"; }
        }

        /// <summary>
        /// Retorna os fields para montar DataGridView
        /// </summary>
        /// <param name="strFields"></param>
        /// <param name="strVisivel"></param>
        /// <param name="strNome"></param>
        public static void RetornarFields()
        {
            _strFields = CC_Controle + "," + CC_cdRegistro + "," + cdConcurso + "," + cdCategoria + "," + nuOrdem + "," + CC_deCategoria;

            _strNome = "Controle, Cd. Registro, cdConcurso, cdCategoria, nuOrdem, CC_deCategoria";

            _strVisivel = "0, 0, 1, 1, 1, 1";
        }
    }
}