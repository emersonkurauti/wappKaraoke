using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using wappKaraoke.Classes.Controller;

namespace wappKaraoke.Classes.Model.TipoStatus
{
    public class coTipoStatus : KuraFrameWork.ClasseBase.csModelBase
    {
        private static  int _CC_cdRegistro;
        public static int CC_cdRegistro
        {
            get { return coTipoStatus._cdTpStatus; }
            set { coTipoStatus._cdTpStatus = value; }
        }

		/// <summary>
		/// Atributos
		/// </summary>        
		private static string _deTpStatus = "";
        public string deTpStatus
        {
            get { return _deTpStatus; }
            set { _deTpStatus = value; }
        }
		private static int _cdTpStatus;
        public int cdTpStatus
        {
            get { return _cdTpStatus; }
            set { _cdTpStatus = value; }
        }
		private static string _deCor = "";
        public string deCor
        {
            get { return _deCor; }
            set { _deCor = value; }
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public coTipoStatus()
        {
            AtualizaObj();
            LimparAtributos();
            _CC_cdRegistro = cdTpStatus;
            tobjCA = typeof(caTipoStatus);
        }



        /// <summary>
        /// Sobrescrito para retornar a chave
        /// </summary>
        /// <returns></returns>
        public override bool Inserir()
        {
            if (base.Inserir())
            {
                cdTpStatus = objBanco.cdChave;
                return true;
            }

            return false;
        }
    }
}