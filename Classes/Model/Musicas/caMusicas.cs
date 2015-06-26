using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wappKaraoke.Classes.Model.Musicas
{
    public static class caMusicas
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
            get { return "MUSICAS"; }
        }

        public static string nmCampoChave
        {
            get { return "cdMusica"; }
        }

        public static string dePrincipal
        {
            get { return "nmMusica"; }
        }

        public static string deChaveComposta
        {
            get { return "[ChComposta]"; }
        }

		/// <summary>
		/// Atributos
	 	/// </summary>        
		public static string deCaminhoMusica
        {
            get { return "deCaminhoMusica"; }
        }
		public static string deCaminhoMusicaKaraoke
        {
            get { return "deCaminhoMusicaKaraoke"; }
        }
		public static string nmCantor
        {
            get { return "nmCantor"; }
        }
		public static string cdMusica
        {
            get { return "cdMusica"; }
        }
		public static string nmMusicaKanji
        {
            get { return "nmMusicaKanji"; }
        }
		public static string nmMusica
        {
            get { return "nmMusica"; }
        }
		public static string nuAnoLanc
        {
            get { return "nuAnoLanc"; }
        }

        /// <summary>
        /// Retorna os fields para montar DataGridView
        /// </summary>
        /// <param name="strFields"></param>
        /// <param name="strVisivel"></param>
        /// <param name="strNome"></param>
        public static void RetornarFields()
        {
            _strFields = CC_cdRegistro  + "," + deCaminhoMusica + "," + deCaminhoMusicaKaraoke + "," + nmCantor + "," + cdMusica + "," + nmMusicaKanji + "," + nmMusica + "," + nuAnoLanc;

            _strNome = "Cd. Registro, deCaminhoMusica, deCaminhoMusicaKaraoke, nmCantor, cdMusica, nmMusicaKanji, nmMusica, nuAnoLanc";

            _strVisivel = "0, 1, 1, 1, 1, 1, 1, 1";
        }
    }
}