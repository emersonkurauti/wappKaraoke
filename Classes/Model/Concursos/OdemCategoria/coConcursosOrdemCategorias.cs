using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using wappKaraoke.Classes.Controller;
using wappKaraoke.Classes.Model.Categorias;

namespace wappKaraoke.Classes.Model.ConcursosOrdemCategorias
{
    public class coConcursosOrdemCategorias : KuraFrameWork.ClasseBase.csModelBase
    {
        private static  int _CC_cdRegistro;
        public static int CC_cdRegistro
        {
            get { return coConcursosOrdemCategorias._cdConcurso; }
            set { coConcursosOrdemCategorias._cdConcurso = value; }
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

		private static int _cdConcurso;
        public int cdConcurso
        {
            get { return _cdConcurso; }
            set { _cdConcurso = value; }
        }

		private static int _cdCategoria;
        public int cdCategoria
        {
            get { return _cdCategoria; }
            set { _cdCategoria = value; }
        }

		private static int _nuOrdem;
        public int nuOrdem
        {
            get { return _nuOrdem; }
            set { _nuOrdem = value; }
        }

		private static string _CC_deCategoria = "";
        public string CC_deCategoria
        {
            get { return _CC_deCategoria; }
            set { _CC_deCategoria = value; }
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public coConcursosOrdemCategorias()
        {
            AtualizaObj();
            LimparAtributos();
            _CC_cdRegistro = cdConcurso;
            tobjCA = typeof(caConcursosOrdemCategorias);
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
               conCategorias objConCategorias = new conCategorias();

               DataTable dtAux = dtDados;

               dtDados.Columns[caConcursosOrdemCategorias.CC_Controle].ReadOnly = false;
               dtDados.Columns[caConcursosOrdemCategorias.CC_deCategoria].ReadOnly = false;
               dtDados.Columns[caConcursosOrdemCategorias.CC_deCategoria].MaxLength = 100;

               foreach (DataRow dr in dtAux.Rows)
               {
                   objConCategorias.objCoCategorias.LimparAtributos();
                   objConCategorias.objCoCategorias.cdCategoria = Convert.ToInt32(dr[caConcursosOrdemCategorias.cdCategoria].ToString());


                   if (conCategorias.Select())
                   {
                       if (objConCategorias.dtDados.Rows.Count > 0)
                       {
                           dr[caConcursosOrdemCategorias.CC_deCategoria] = objConCategorias.dtDados.Rows[0][caCategorias.deCategoria].ToString();

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
                cdConcurso = objBanco.cdChave;
                return true;
            }

            return false;
        }
    }
}