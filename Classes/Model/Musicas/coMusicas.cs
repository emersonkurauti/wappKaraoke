using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using wappKaraoke.Classes.Controller;

namespace wappKaraoke.Classes.Model.Musicas
{
    public class coMusicas : KuraFrameWork.ClasseBase.csModelBase
    {
        private static  int _CC_cdRegistro;
        public static int CC_cdRegistro
        {
            get { return coMusicas._cdMusica; }
            set { coMusicas._cdMusica = value; }
        }

		/// <summary>
		/// Atributos
		/// </summary>        
		private static string _deCaminhoMusica = "";
        public string deCaminhoMusica
        {
            get { return _deCaminhoMusica; }
            set { _deCaminhoMusica = value; }
        }

		private static string _deCaminhoMusicaKaraoke = "";
        public string deCaminhoMusicaKaraoke
        {
            get { return _deCaminhoMusicaKaraoke; }
            set { _deCaminhoMusicaKaraoke = value; }
        }

		private static string _nmCantor = "";
        public string nmCantor
        {
            get { return _nmCantor; }
            set { _nmCantor = value; }
        }

		private static int _cdMusica;
        public int cdMusica
        {
            get { return _cdMusica; }
            set { _cdMusica = value; }
        }

		private static string _nmMusicaKanji = "";
        public string nmMusicaKanji
        {
            get { return _nmMusicaKanji; }
            set { _nmMusicaKanji = value; }
        }

		private static string _nmMusica = "";
        public string nmMusica
        {
            get { return _nmMusica; }
            set { _nmMusica = value; }
        }

		private static int _nuAnoLanc;
        public int nuAnoLanc
        {
            get { return _nuAnoLanc; }
            set { _nuAnoLanc = value; }
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public coMusicas()
        {
            AtualizaObj();
            LimparAtributos();
            _CC_cdRegistro = cdMusica;
            tobjCA = typeof(caMusicas);
        }

        /// <summary>
        /// Sobrescrito para retornar a chave
        /// </summary>
        /// <returns></returns>
        public override bool Inserir()
        {
            if (base.Inserir())
            {
                cdMusica = objBanco.cdChave;
                return true;
            }

            return false;
        }
    }
}