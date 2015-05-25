using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using wappKaraoke.Classes.Controller;

namespace wappKaraoke.Classes.Model.Cidades
{
    public class coCidades : KuraFrameWork.ClasseBase.csModelBase
    {
        private static  int _CC_cdRegistro;
        public static int CC_cdRegistro
        {
            get { return coCidades._cdCidade; }
            set { coCidades._cdCidade = value; }
        }

		/// <summary>
		/// Atributos
		/// </summary>        
		private static string _deUF = "";
        public string deUF
        {
            get { return _deUF; }
            set { _deUF = value; }
        }

		private static int _cdCidade;
        public int cdCidade
        {
            get { return _cdCidade; }
            set { _cdCidade = value; }
        }

		private static string _nmCidade = "";
        public string nmCidade
        {
            get { return _nmCidade; }
            set { _nmCidade = value; }
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public coCidades()
        {
            AtualizaObj();
            LimparAtributos();
            _CC_cdRegistro = cdCidade;
            tobjCA = typeof(caCidades);
        }

        /// <summary>
        /// Sobrescrito para retornar a chave
        /// </summary>
        /// <returns></returns>
        public override bool Inserir()
        {
            if (base.Inserir())
            {
                cdCidade = objBanco.cdChave;
                return true;
            }

            return false;
        }
    }
}