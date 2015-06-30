using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using wappKaraoke.Classes.Paginas_Default;
using wappKaraoke.Classes.Model.Categorias;
using wappKaraoke.Classes.Controller;

namespace wappKaraoke.Classes
{
    public class csCategorias : Paginas_Default.csMontaDDL
    {
        public csCategorias()
        {
            strTextoCombo = "a Categoria";
            tobjCa = typeof(caCategorias);
            objCon = new conCategorias();
        }

        public override DataTable getDtDados()
        {
            if (bUtilizaDadosExternos)
            {
                DataTable dt = new DataTable();
                dt = conCategorias.objCo.RetornaEstruturaDT();
                DataRow dr = dt.NewRow();
                dr[caCategorias.nmCampoChave.ToString()] = 0;
                dr[caCategorias.dePrincipal.ToString()] = "--Selecione " + strTextoCombo + "--";
                dt.Rows.Add(dr);

                conCategorias objConCategorias = new conCategorias();
                objConCategorias.objCoCategorias.LimparAtributos();

                string strCategorias = " WHERE cdCategoria IN (";

                foreach (DataRow drext in dtDadosExternos.Rows)
                {
                    strCategorias += drext[caCategorias.cdCategoria].ToString() + ",";
                }

                strCategorias = strCategorias.Substring(0, strCategorias.Length - 1) + ")";

                objConCategorias.objCoCategorias.strFiltro = strCategorias;

                if (conCategorias.Select())
                {
                    foreach (DataRow drCopy in objConCategorias.dtDados.Rows)
                        dt.ImportRow(drCopy);
                }

                return dt;
            }
            else
                return base.getDtDados();
        }
    }
}