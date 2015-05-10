using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;

namespace wappKaraoke.Classes
{
    public class csStatus
    {
        private DataTable _dtDados = new DataTable();
        public DataTable dtDados
        {
            get { return getDtDados(); }
            set { _dtDados = value; }
        }

        private DataTable getDtDados()
        {
            _dtDados.Columns.Add("cdStatus", typeof(int));
            _dtDados.Columns.Add("deStatus", typeof(string));

            _dtDados.Rows.Add(1, "Chegou");
            _dtDados.Rows.Add(2, "Camarim");
            _dtDados.Rows.Add(3, "Cantando");

            return _dtDados;
        }

        public DropDownList CarregaDDL(DropDownList pDDL)
        {
            pDDL.DataSource = getDtDados();
            pDDL.DataValueField = "cdStatus";
            pDDL.DataTextField = "deStatus";
            pDDL.DataBind();
            pDDL.SelectedIndex = 0;

            return pDDL;
        }

        public string MontaSelect(string psId)
        {
            string strSelect = "<select name=\"nmStatus_" + psId + "\" id=\"idStatus_" + psId + "\" class=\"form-control selectpicker\" style=\"width:100%;text-align:left\">";

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
                strOprions += "<option value=\"" + _dtDados.Rows[0]["cdStatus"].ToString() + "\">" + _dtDados.Rows[0]["deStatus"].ToString() + "</option>";

                for (int i = 1; i < _dtDados.Rows.Count; i++)
                {
                    strOprions += "<option value=\"" + _dtDados.Rows[i]["cdStatus"].ToString() + "\">" + _dtDados.Rows[i]["deStatus"].ToString() + "</option>";
                }
            }
            return strOprions;
        }
    }
}