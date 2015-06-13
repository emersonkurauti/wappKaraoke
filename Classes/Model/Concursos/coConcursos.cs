using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using wappKaraoke.Classes.Controller;
using wappKaraoke.Classes.Model.Cidades;

namespace wappKaraoke.Classes.Model.Concursos
{
    public class coConcursos : KuraFrameWork.ClasseBase.csModelBase
    {
        private DataTable _dtArquivos;
        public DataTable dtArquivos
        {
            get { return _dtArquivos; }
            set { _dtArquivos = value; }
        }

        private static  int _CC_cdRegistro;
        public static int CC_cdRegistro
        {
            get { return coConcursos._cdConcurso; }
            set { coConcursos._cdConcurso = value; }
        }

		/// <summary>
		/// Atributos
		/// </summary>        
		private static int _cdCidade;
        public int cdCidade
        {
            get { return _cdCidade; }
            set { _cdCidade = value; }
        }

		private static DateTime _dtIniConcurso;
        public DateTime dtIniConcurso
        {
            get { return _dtIniConcurso; }
            set { _dtIniConcurso = value; }
        }

		private static string _flFinalizado = "";
        public string flFinalizado
        {
            get { return _flFinalizado; }
            set { _flFinalizado = value; }
        }

        private static DateTime _dtFimConcurso;
        public DateTime dtFimConcurso
        {
            get { return _dtFimConcurso; }
            set { _dtFimConcurso = value; }
        }

		private static int _cdConcurso;
        public int cdConcurso
        {
            get { return _cdConcurso; }
            set { _cdConcurso = value; }
        }

		private static string _nmConcursoKanji = "";
        public string nmConcursoKanji
        {
            get { return _nmConcursoKanji; }
            set { _nmConcursoKanji = value; }
        }

		private static string _nmConcurso = "";
        public string nmConcurso
        {
            get { return _nmConcurso; }
            set { _nmConcurso = value; }
        }

		private static string _CC_nmCidade = "";
        public string CC_nmCidade
        {
            get { return _CC_nmCidade; }
            set { _CC_nmCidade = value; }
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public coConcursos()
        {
            AtualizaObj();
            LimparAtributos();
            _CC_cdRegistro = cdConcurso;
            tobjCA = typeof(caConcursos);
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
               conCidades objConCidades = new conCidades();

               DataTable dtAux = dtDados;

               dtDados.Columns[caConcursos.CC_nmCidade].ReadOnly = false;
               dtDados.Columns[caConcursos.CC_nmCidade].MaxLength = 100;

               foreach (DataRow dr in dtAux.Rows)
               {
                   objConCidades.objCoCidades.LimparAtributos();
                   objConCidades.objCoCidades.cdCidade = Convert.ToInt32(dr[caConcursos.cdCidade].ToString());


                   if (conCidades.Select())
                   {
                       if (objConCidades.dtDados.Rows.Count > 0)
                       {
                           dr[caConcursos.CC_nmCidade] = objConCidades.dtDados.Rows[0][caCidades.nmCidade].ToString();

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
            objBanco.BeginTransaction();

            try
            {
                if (base.Inserir())
                {
                    cdConcurso = objBanco.cdChave;
                }

                objBanco.CommitTransaction();
                return true;
            }
            catch
            {
                objBanco.RollbackTransaction();
                return false;
            }
        }
    }
}