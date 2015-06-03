using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wappKaraoke.Classes.Model.Arquivos
{
    public static class caArquivos
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
            get { return "ARQUIVOS"; }
        }

        public static string nmCampoChave
        {
            get { return "cdArquivo"; }
        }

        public static string dePrincipal
        {
            get { return "nmArquivo"; }
        }

        public static string deChaveComposta
        {
            get { return "cdArquivo;cdConcurso;cdTipoArquivo"; }
        }

		/// <summary>
		/// Atributos
	 	/// </summary>        
		public static string cdTipoArquivo
        {
            get { return "cdTipoArquivo"; }
        }
		public static string cdConcurso
        {
            get { return "cdConcurso"; }
        }
		public static string deArquivo
        {
            get { return "deArquivo"; }
        }
		public static string deCaminhoArquivo
        {
            get { return "deCaminhoArquivo"; }
        }
		public static string nmArquivo
        {
            get { return "nmArquivo"; }
        }
		public static string cdArquivo
        {
            get { return "cdArquivo"; }
        }
		public static string CC_deTipoArquivo
        {
            get { return "CC_deTipoArquivo"; }
        }

        /// <summary>
        /// Retorna os fields para montar DataGridView
        /// </summary>
        /// <param name="strFields"></param>
        /// <param name="strVisivel"></param>
        /// <param name="strNome"></param>
        public static void RetornarFields()
        {
            _strFields = CC_cdRegistro  + "," + cdTipoArquivo + "," + cdConcurso + "," + deArquivo + "," + deCaminhoArquivo + "," + nmArquivo + "," + cdArquivo + "," + CC_deTipoArquivo;

            _strNome = "Cd. Registro, cdTipoArquivo, cdConcurso, deArquivo, deCaminhoArquivo, nmArquivo, cdArquivo, CC_deTipoArquivo";

            _strVisivel = "0, 1, 1, 1, 1, 1, 1, 1";
        }
    }
}