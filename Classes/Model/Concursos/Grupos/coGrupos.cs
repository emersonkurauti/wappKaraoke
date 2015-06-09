using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using wappKaraoke.Classes.Controller;
using wappKaraoke.Classes.Model.Jurados;

namespace wappKaraoke.Classes.Model.Grupos
{
    public class coGrupos : KuraFrameWork.ClasseBase.csModelBase
    {
        private static  int _CC_cdRegistro;
        public static int CC_cdRegistro
        {
            get { return coGrupos._cdConcurso; }
            set { coGrupos._cdConcurso = value; }
        }

		/// <summary>
		/// Atributos
		/// </summary>        
		private static int _cdConcurso;
        public int cdConcurso
        {
            get { return _cdConcurso; }
            set { _cdConcurso = value; }
        }

		private static int _cdJurado;
        public int cdJurado
        {
            get { return _cdJurado; }
            set { _cdJurado = value; }
        }

		private static string _deGrupo = "";
        public string deGrupo
        {
            get { return _deGrupo; }
            set { _deGrupo = value; }
        }

		private static string _CC_nmJurado = "";
        public string CC_nmJurado
        {
            get { return _CC_nmJurado; }
            set { _CC_nmJurado = value; }
        }

		private static string _CC_nmNomeKanji = "";
        public string CC_nmNomeKanji
        {
            get { return _CC_nmNomeKanji; }
            set { _CC_nmNomeKanji = value; }
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public coGrupos()
        {
            AtualizaObj();
            LimparAtributos();
            _CC_cdRegistro = cdConcurso;
            tobjCA = typeof(caGrupos);
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
               conJurados objConJurados = new conJurados();

               DataTable dtAux = dtDados;

               dtDados.Columns[caGrupos.CC_nmJurado].ReadOnly = false;
               dtDados.Columns[caGrupos.CC_nmJurado].MaxLength = 100;
               dtDados.Columns[caGrupos.CC_nmNomeKanji].ReadOnly = false;
               dtDados.Columns[caGrupos.CC_nmNomeKanji].MaxLength = 100;

               foreach (DataRow dr in dtAux.Rows)
               {
                   objConJurados.objCoJurados.LimparAtributos();
                   objConJurados.objCoJurados.cdJurado = Convert.ToInt32(dr[caGrupos.cdJurado].ToString());


                   if (conJurados.Select())
                   {
                       if (objConJurados.dtDados.Rows.Count > 0)
                       {
                           dr[caGrupos.CC_nmJurado] = objConJurados.dtDados.Rows[0][caJurados.nmJurado].ToString();
                           dr[caGrupos.CC_nmNomeKanji] = objConJurados.dtDados.Rows[0][caJurados.nmNomeKanji].ToString();

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