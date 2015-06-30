using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using wappKaraoke.Classes.Paginas_Default;
using wappKaraoke.Classes.Model.Fases;
using wappKaraoke.Classes.Controller;

namespace wappKaraoke.Classes
{
    public class csFases: Paginas_Default.csMontaDDL
    {
        public csFases()
        {
            strTextoCombo = "a Fase";
            tobjCa = typeof(caFases);
            objCon = new conFases();
        }

        public override DataTable getDtDados()
        {
            if (bUtilizaDadosExternos)
            {
                DataTable dt = new DataTable();
                dt = conFases.objCo.RetornaEstruturaDT();
                DataRow dr = dt.NewRow();
                dr[caFases.nmCampoChave.ToString()] = 0;
                dr[caFases.dePrincipal.ToString()] = "--Selecione " + strTextoCombo + "--";
                dt.Rows.Add(dr);

                conFases objConFases = new conFases();
                objConFases.objCoFases.LimparAtributos();

                string strFases = " WHERE cdFase IN (";

                foreach (DataRow drext in dtDadosExternos.Rows)
                {
                    strFases += drext[caFases.cdFase].ToString() + ",";
                }

                strFases = strFases.Substring(0, strFases.Length - 1) + ")";

                objConFases.objCoFases.strFiltro = strFases;

                if (conFases.Select())
                {
                    foreach (DataRow drCopy in objConFases.dtDados.Rows)
                        dt.ImportRow(drCopy);
                }

                return dt;
            }
            else
                return base.getDtDados();
        }
    }
}