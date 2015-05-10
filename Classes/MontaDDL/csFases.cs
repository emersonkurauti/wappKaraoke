using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;

namespace wappKaraoke.Classes
{
    public class csFases
    {
        private DataTable _dtDados = new DataTable();
        public DataTable dtDados
        {
            get { return getDtDados(); }
            set { _dtDados = value; }
        }

        private DataTable getDtDados()
        {
            _dtDados.Columns.Add("cdFase", typeof(int));
            _dtDados.Columns.Add("deFase", typeof(string));

            _dtDados.Rows.Add(1, "Calssificatória");
            _dtDados.Rows.Add(2, "Eliminatória");
            _dtDados.Rows.Add(3, "Final");

            return _dtDados;
        }

        public DropDownList CarregaDDL(DropDownList pDDL)
        {
            pDDL.DataSource = getDtDados();
            pDDL.DataValueField = "cdFase";
            pDDL.DataTextField = "deFase";
            pDDL.DataBind();
            pDDL.SelectedIndex = 0;

            return pDDL;
        }

        public string MontaSelect(string psId)
        {
            string strSelect = "<select name=\"nmFase_" + psId + "\" id=\"idFase_" + psId + "\" class=\"form-control selectpicker\" style=\"width:100%;text-align:left\">";

            strSelect += MontaOptions();

            strSelect += "</select>";

            return strSelect;
        }

        private string MontaOptions()
        {
            getDtDados();
            string strOprions = "";

            if (_dtDados.Rows.Count > 0)
            {
                strOprions += "<option value=\"" + _dtDados.Rows[0]["cdFase"].ToString() + "\">" + _dtDados.Rows[0]["deFase"].ToString() + "</option>";

                for (int i = 1; i < _dtDados.Rows.Count; i++)
                {
                    strOprions += "<option value=\"" + _dtDados.Rows[i]["cdFase"].ToString() + "\">" + _dtDados.Rows[i]["deFase"].ToString() + "</option>";
                }
            }
            return strOprions;
        }
    }
}