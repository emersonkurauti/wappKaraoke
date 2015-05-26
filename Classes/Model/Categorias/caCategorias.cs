using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wappKaraoke.Classes.Model.Categorias
{
    public static class caCategorias
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
            get { return "CATEGORIAS"; }
        }

        public static string nmCampoChave
        {
            get { return "cdCategoria"; }
        }

        public static string dePrincipal
        {
            get { return "deCategoria"; }
        }

        public static string deChaveComposta
        {
            get { return "[ChComposta]"; }
        }

		/// <summary>
		/// Atributos
	 	/// </summary>        
		public static string deFormulaPontuacao
        {
            get { return "deFormulaPontuacao"; }
        }
		public static string deCategoria
        {
            get { return "deCategoria"; }
        }
		public static string cdCategoria
        {
            get { return "cdCategoria"; }
        }

        /// <summary>
        /// Retorna os fields para montar DataGridView
        /// </summary>
        /// <param name="strFields"></param>
        /// <param name="strVisivel"></param>
        /// <param name="strNome"></param>
        public static void RetornarFields()
        {
            _strFields = CC_cdRegistro  + "," + deFormulaPontuacao + "," + deCategoria + "," + cdCategoria;

            _strNome = "Cd. Registro, deFormulaPontuacao, deCategoria, cdCategoria";

            _strVisivel = "0, 1, 1, 1";
        }
    }
}