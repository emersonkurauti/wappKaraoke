using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using wappKaraoke.Classes.Controller;
using wappKaraoke.Classes.Model.TipoArquivo;

namespace wappKaraoke.Classes.Model.Arquivos
{
    public class coArquivos : KuraFrameWork.ClasseBase.csModelBase
    {
        private static  int _CC_cdRegistro;
        public static int CC_cdRegistro
        {
            get { return coArquivos._cdTipoArquivo; }
            set { coArquivos._cdTipoArquivo = value; }
        }

		/// <summary>
		/// Atributos
		/// </summary>        
        private static string _CC_Controle = "";
        public string CC_Controle
        {
            get { return _CC_Controle; }
            set { _CC_Controle = value; }
        }

        private static int _cdTipoArquivo;
        public int cdTipoArquivo
        {
            get { return _cdTipoArquivo; }
            set { _cdTipoArquivo = value; }
        }

		private static int _cdConcurso;
        public int cdConcurso
        {
            get { return _cdConcurso; }
            set { _cdConcurso = value; }
        }

		private static string _deArquivo = "";
        public string deArquivo
        {
            get { return _deArquivo; }
            set { _deArquivo = value; }
        }

		private static string _deCaminhoArquivo = "";
        public string deCaminhoArquivo
        {
            get { return _deCaminhoArquivo; }
            set { _deCaminhoArquivo = value; }
        }

		private static string _nmArquivo = "";
        public string nmArquivo
        {
            get { return _nmArquivo; }
            set { _nmArquivo = value; }
        }

		private static int _cdArquivo;
        public int cdArquivo
        {
            get { return _cdArquivo; }
            set { _cdArquivo = value; }
        }

		private static string _CC_deTipoArquivo = "";
        public string CC_deTipoArquivo
        {
            get { return _CC_deTipoArquivo; }
            set { _CC_deTipoArquivo = value; }
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public coArquivos()
        {
            AtualizaObj();
            LimparAtributos();
            _CC_cdRegistro = cdTipoArquivo;
            tobjCA = typeof(caArquivos);
        }

       /// <summary>
       /// Método sobrescrito por conta do campo calculado
       /// </summary>
       /// <param name="dtDados"></param>
       /// <returns></returns>
       public override bool Select(out DataTable dtDados)
       {
           if (base.Select(out dtDados))
           {
               conTipoArquivo objConTipoArquivo = new conTipoArquivo();

               DataTable dtAux = dtDados;

               dtDados.Columns[caArquivos.CC_deTipoArquivo].ReadOnly = false;
               dtDados.Columns[caArquivos.CC_deTipoArquivo].MaxLength = 100;

               foreach (DataRow dr in dtAux.Rows)
               {
                   objConTipoArquivo.objCoTipoArquivo.LimparAtributos();
                   objConTipoArquivo.objCoTipoArquivo.cdTipoArquivo = Convert.ToInt32(dr[caArquivos.cdTipoArquivo].ToString());


                   if (conTipoArquivo.Select())
                   {
                       if (objConTipoArquivo.dtDados.Rows.Count > 0)
                       {
                           dr[caArquivos.CC_deTipoArquivo] = objConTipoArquivo.dtDados.Rows[0][caTipoArquivo.deTipoArquivo].ToString();

                       }
                   }
               }

               dtDados = dtAux;
           }
           else
               return false;

           return true;
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