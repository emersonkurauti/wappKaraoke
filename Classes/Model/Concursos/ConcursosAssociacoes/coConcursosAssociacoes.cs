using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using wappKaraoke.Classes.Controller;
using wappKaraoke.Classes.Model.Associacoes;

namespace wappKaraoke.Classes.Model.ConcursosAssociacoes
{
    public class coConcursosAssociacoes : KuraFrameWork.ClasseBase.csModelBase
    {
        private static  int _CC_cdRegistro;
        public static int CC_cdRegistro
        {
            get { return coConcursosAssociacoes._cdConcurso; }
            set { coConcursosAssociacoes._cdConcurso = value; }
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

		private static int _cdAssociacao;
        public int cdAssociacao
        {
            get { return _cdAssociacao; }
            set { _cdAssociacao = value; }
        }

		private static string _deEmail = "";
        public string deEmail
        {
            get { return _deEmail; }
            set { _deEmail = value; }
        }

		private static string _nmRepresentante = "";
        public string nmRepresentante
        {
            get { return _nmRepresentante; }
            set { _nmRepresentante = value; }
        }

		private static string _CC_nmAssociacao = "";
        public string CC_nmAssociacao
        {
            get { return _CC_nmAssociacao; }
            set { _CC_nmAssociacao = value; }
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public coConcursosAssociacoes()
        {
            AtualizaObj();
            LimparAtributos();
            _CC_cdRegistro = cdConcurso;
            tobjCA = typeof(caConcursosAssociacoes);
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
               conAssociacoes objConAssociacoes = new conAssociacoes();

               DataTable dtAux = dtDados;

               dtDados.Columns[caConcursosAssociacoes.CC_nmAssociacao].ReadOnly = false;
               dtDados.Columns[caConcursosAssociacoes.CC_nmAssociacao].MaxLength = 100;

               foreach (DataRow dr in dtAux.Rows)
               {
                   objConAssociacoes.objCoAssociacoes.LimparAtributos();
                   objConAssociacoes.objCoAssociacoes.cdAssociacao = Convert.ToInt32(dr[caConcursosAssociacoes.cdAssociacao].ToString());


                   if (conAssociacoes.Select())
                   {
                       if (objConAssociacoes.dtDados.Rows.Count > 0)
                       {
                           dr[caConcursosAssociacoes.CC_nmAssociacao] = objConAssociacoes.dtDados.Rows[0][caAssociacoes.nmAssociacao].ToString();

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