using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using wappKaraoke.Classes.Paginas_Default;
using wappKaraoke.Classes.Model.Associacoes;
using wappKaraoke.Classes.Controller;
using wappKaraoke.Classes.Model.ConcursosAssociacoes;

namespace wappKaraoke.Classes
{
    public class csAssociacoes : Paginas_Default.csMontaDDL
    {
        private DataTable _dtDadosExternos;
        public DataTable dtDadosExternos
        {
            get { return _dtDadosExternos; }
            set { _dtDadosExternos = value; }
        }

        private bool _bUtilizaDadosExternos;
        public bool bUtilizaDadosExternos
        {
            get { return _bUtilizaDadosExternos; }
            set { _bUtilizaDadosExternos = value; }
        }

        public csAssociacoes()
        {
            strTextoCombo = "a Associação";
            tobjCa = typeof(caAssociacoes);
            objCon = new conAssociacoes();
        }

        public override DataTable getDtDados()
        {
            if (bUtilizaDadosExternos)
            {
                DataTable dt = new DataTable();
                dt = conAssociacoes.objCo.RetornaEstruturaDT();
                DataRow dr = dt.NewRow();
                dr[caAssociacoes.nmCampoChave.ToString()] = 0;
                dr[caAssociacoes.dePrincipal.ToString()] = "--Selecione " + strTextoCombo + " do Cantor--";
                dt.Rows.Add(dr);

                conAssociacoes objConAssociacoes = new conAssociacoes();
                objConAssociacoes.objCoAssociacoes.LimparAtributos();

                string strAssociacoes = " WHERE cdAssociacao IN (";

                foreach (DataRow drext in _dtDadosExternos.Rows)
                {
                    strAssociacoes += drext[caAssociacoes.cdAssociacao].ToString() + ",";
                }

                strAssociacoes = strAssociacoes.Substring(0, strAssociacoes.Length - 1) + ")";

                objConAssociacoes.objCoAssociacoes.strFiltro = strAssociacoes;

                if (conAssociacoes.Select())
                {
                    foreach (DataRow drCopy in objConAssociacoes.dtDados.Rows)
                        dt.ImportRow(drCopy);
                }

                return dt;
            }
            else
                return base.getDtDados();
        }
    }
}