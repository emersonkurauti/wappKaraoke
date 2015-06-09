using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wappKaraoke.Classes.Model.Grupos
{
    public static class caGrupos
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
            get { return "GRUPOS"; }
        }

        public static string nmCampoChave
        {
            get { return "cdConcurso"; }
        }

        public static string dePrincipal
        {
            get { return "CC_nmJurado"; }
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
		public static string cdJurado
        {
            get { return "cdJurado"; }
        }
		public static string deGrupo
        {
            get { return "deGrupo"; }
        }
		public static string CC_nmJurado
        {
            get { return "CC_nmJurado"; }
        }
		public static string CC_nmNomeKanji
        {
            get { return "CC_nmNomeKanji"; }
        }

        /// <summary>
        /// Retorna os fields para montar DataGridView
        /// </summary>
        /// <param name="strFields"></param>
        /// <param name="strVisivel"></param>
        /// <param name="strNome"></param>
        public static void RetornarFields()
        {
            _strFields = CC_cdRegistro  + "," + cdConcurso + "," + cdJurado + "," + deGrupo + "," + CC_nmJurado + "," + CC_nmNomeKanji;

            _strNome = "Cd. Registro, cdConcurso, cdJurado, deGrupo, CC_nmJurado, CC_nmNomeKanji";

            _strVisivel = "0, 1, 1, 1, 1, 1";
        }
    }
}