using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using wappKaraoke.Classes.Controller;

namespace wappKaraoke.Classes.Model.Fases
{
    public class coFases : KuraFrameWork.ClasseBase.csModelBase
    {
        private static int _CC_cdRegistro = 0;
        public static int CC_cdRegistro
        {
            get { return coFases._cdFase; }
            set { coFases._cdFase = value; }
        }

		/// <summary>
		/// Atributos
		/// </summary>        
		private static string _deFase = "";
        public string deFase
        {
            get { return _deFase; }
            set { _deFase = value; }
        }

		private static int _cdFase;
        public int cdFase
        {
            get { return _cdFase; }
            set { _cdFase = value; }
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public coFases()
        {
            AtualizaObj();
            LimparAtributos();
            _CC_cdRegistro = cdFase;
            tobjCA = typeof(caFases);
        }

        /// <summary>
        /// Sobrescrito para retornar a chave
        /// </summary>
        /// <returns></returns>
        public override bool Inserir()
        {
            if (base.Inserir())
            {
                cdFase = objBanco.cdChave;
                return true;
            }

            return false;
        }
    }
}