using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wappKaraoke.Classes.Model.CantoresFases
{
    public static class caCantoresFases
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
            get { return "CANTORESFASES"; }
        }

        public static string nmCampoChave
        {
            get { return "cdConcurso"; }
        }

        public static string dePrincipal
        {
            get { return "nuCantor"; }
        }

        public static string deChaveComposta
        {
            get { return "cdCantor;cdFase;cdCategoria;nuCantor"; }
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
		public static string cdCantor
        {
            get { return "cdCantor"; }
        }
		public static string cdMusica
        {
            get { return "cdMusica"; }
        }
		public static string cdTpStatus
        {
            get { return "cdTpStatus"; }
        }
		public static string cdCategoria
        {
            get { return "cdCategoria"; }
        }
		public static string cdFase
        {
            get { return "cdFase"; }
        }
		public static string nuCantor
        {
            get { return "nuCantor"; }
        }
		public static string deCaminhoMusica
        {
            get { return "deCaminhoMusica"; }
        }
		public static string nuOrdemApresentacao
        {
            get { return "nuOrdemApresentacao"; }
        }
		public static string nuNotafinal
        {
            get { return "nuNotafinal"; }
        }
		public static string pcDesconto
        {
            get { return "pcDesconto"; }
        }
        public static string flFaseCorrente
        {
            get { return "flFaseCorrente"; }
        }
		public static string CC_nmCantor
        {
            get { return "CC_nmCantor"; }
        }
		public static string CC_nmMusica
        {
            get { return "CC_nmMusica"; }
        }
		public static string CC_deTpStatus
        {
            get { return "CC_deTpStatus"; }
        }
		public static string CC_deFase
        {
            get { return "CC_deFase"; }
        }
		public static string CC_deCategoria
        {
            get { return "CC_deCategoria"; }
        }
		public static string CC_nmNomeKanji
        {
            get { return "CC_nmNomeKanji"; }
        }
		public static string CC_nmMusicaKanji
        {
            get { return "CC_nmMusicaKanji"; }
        }
        public static string CC_deFormulaPontuacao
        {
            get { return "CC_deFormulaPontuacao"; }
        }

        /// <summary>
        /// Retorna os fields para montar DataGridView
        /// </summary>
        /// <param name="strFields"></param>
        /// <param name="strVisivel"></param>
        /// <param name="strNome"></param>
        public static void RetornarFields()
        {
            _strFields = CC_Controle + "," + CC_cdRegistro + "," + cdConcurso + "," + cdCantor + "," + cdMusica + "," + cdTpStatus + "," + cdCategoria + "," + cdFase + "," + nuCantor + "," + deCaminhoMusica + "," + nuOrdemApresentacao + "," + nuNotafinal + "," + pcDesconto + "," + flFaseCorrente +"," + CC_nmCantor + "," + CC_nmMusica + "," + CC_deTpStatus + "," + CC_deFase + "," + CC_deCategoria + "," + CC_nmNomeKanji + "," + CC_nmMusicaKanji + "," + CC_deFormulaPontuacao;

            _strNome = "Controle, Cd. Registro, cdConcurso, cdCantor, cdMusica, cdTpStatus, cdCategoria, cdFase, nuCantor, deCaminhoMusica, nuOrdemApresentacao, nuNotafinal, pcDesconto, flFaseCorrente, CC_nmCantor, CC_nmMusica, CC_deTpStatus, CC_deFase, CC_deCategoria, CC_nmNomeKanji, CC_nmMusicaKanji, CC_deFormulaPontuacao";

            _strVisivel = "0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1";
        }
    }
}