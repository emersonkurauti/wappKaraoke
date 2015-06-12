using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using wappKaraoke.Classes.Controller;
using wappKaraoke.Classes.Model.Cantores;
using wappKaraoke.Classes.Model.Associacoes;

namespace wappKaraoke.Classes.Model.CantoresConcursos
{
    public class coCantoresConcursos : KuraFrameWork.ClasseBase.csModelBase
    {
        private static  int _CC_cdRegistro;
        public static int CC_cdRegistro
        {
            get { return coCantoresConcursos._cdCantor; }
            set { coCantoresConcursos._cdCantor = value; }
        }

		/// <summary>
		/// Atributos
		/// </summary>        
		private static int _cdCantor;
        public int cdCantor
        {
            get { return _cdCantor; }
            set { _cdCantor = value; }
        }

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

		private static string _CC_nmCantor = "";
        public string CC_nmCantor
        {
            get { return _CC_nmCantor; }
            set { _CC_nmCantor = value; }
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
        public coCantoresConcursos()
        {
            AtualizaObj();
            LimparAtributos();
            _CC_cdRegistro = cdCantor;
            tobjCA = typeof(caCantoresConcursos);
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
                conCantores objConCantores = new conCantores();
                conAssociacoes objConAssociacoes = new conAssociacoes();

                DataTable dtAux = dtDados;

                dtDados.Columns[caCantoresConcursos.CC_nmCantor].ReadOnly = false;
                dtDados.Columns[caCantoresConcursos.CC_nmCantor].MaxLength = 100;
                dtDados.Columns[caCantoresConcursos.CC_nmAssociacao].ReadOnly = false;
                dtDados.Columns[caCantoresConcursos.CC_nmAssociacao].MaxLength = 100;

                foreach (DataRow dr in dtAux.Rows)
                {
                    objConCantores.objCoCantores.LimparAtributos();
                    objConCantores.objCoCantores.cdCantor = Convert.ToInt32(dr[caCantoresConcursos.cdCantor].ToString());
                    objConAssociacoes.objCoAssociacoes.LimparAtributos();
                    objConAssociacoes.objCoAssociacoes.cdAssociacao = Convert.ToInt32(dr[caCantoresConcursos.cdAssociacao].ToString());

                    if (conAssociacoes.Select())
                    {
                        if (objConAssociacoes.dtDados.Rows.Count > 0)
                        {
                            dr[caCantoresConcursos.CC_nmCantor] = objConCantores.dtDados.Rows[0][caCantores.nmCantor].ToString();
                        }
                    }
                    if (conAssociacoes.Select())
                    {
                        if (objConAssociacoes.dtDados.Rows.Count > 0)
                        {
                            dr[caCantoresConcursos.CC_nmAssociacao] = objConAssociacoes.dtDados.Rows[0][caAssociacoes.nmAssociacao].ToString();
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
                cdCantor = objBanco.cdChave;
                return true;
            }

            return false;
        }
    }
}