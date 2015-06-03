using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using wappKaraoke.Classes.Controller;

namespace wappKaraoke.Classes.Model.TipoArquivo
{
    public class coTipoArquivo : KuraFrameWork.ClasseBase.csModelBase
    {
        private static  int _CC_cdRegistro;
        public static int CC_cdRegistro
        {
            get { return coTipoArquivo._cdTipoArquivo; }
            set { coTipoArquivo._cdTipoArquivo = value; }
        }

		/// <summary>
		/// Atributos
		/// </summary>        
		private static string _deTipoArquivo = "";
        public string deTipoArquivo
        {
            get { return _deTipoArquivo; }
            set { _deTipoArquivo = value; }
        }

		private static int _cdTipoArquivo;
        public int cdTipoArquivo
        {
            get { return _cdTipoArquivo; }
            set { _cdTipoArquivo = value; }
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public coTipoArquivo()
        {
            AtualizaObj();
            LimparAtributos();
            _CC_cdRegistro = cdTipoArquivo;
            tobjCA = typeof(caTipoArquivo);
        }

        /// <summary>
        /// Sobrescrito para retornar a chave
        /// </summary>
        /// <returns></returns>
        public override bool Inserir()
        {
            if (base.Inserir())
            {
                cdTipoArquivo = objBanco.cdChave;
                return true;
            }

            return false;
        }
    }
}