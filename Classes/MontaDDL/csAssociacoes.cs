using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;

namespace wappKaraoke.Classes
{
    public class csAssociacoes
    {
        private DataTable _dtDados = new DataTable();
        public DataTable dtDados
        {
            get { return getDtDados(); }
            set { _dtDados = value; }
        }

        private DataTable getDtDados()
        {
            _dtDados.Columns.Add("cdAssociacao", typeof(int));
            _dtDados.Columns.Add("nmAssociacao", typeof(string));

            _dtDados.Rows.Add(1, "ACAE");
            _dtDados.Rows.Add(2, "ACEVI");
            _dtDados.Rows.Add(3, "ACEO");
            _dtDados.Rows.Add(4, "ACAD");

            return _dtDados;
        }

        public DropDownList CarregaDDL(DropDownList pDDL)
        {
            pDDL.DataSource = getDtDados();
            pDDL.DataValueField = "cdAssociacao";
            pDDL.DataTextField = "nmAssociacao";
            pDDL.DataBind();
            pDDL.SelectedIndex = 0;

            return pDDL;
        }

        public string MontaSelect(string psId)
        {
            string strSelect = "<select name=\"nmAssociacao_" + psId + "\" id=\"idAssociacao_" + psId + "\" class=\"form-control selectpicker\" style=\"width:100%;text-align:left\">";

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
                strOprions += "<option value=\"" + _dtDados.Rows[0]["cdAssociacao"].ToString() + "\">" + _dtDados.Rows[0]["nmAssociacao"].ToString() + "</option>";

                for (int i = 1; i < _dtDados.Rows.Count; i++)
                {
                    strOprions += "<option value=\"" + _dtDados.Rows[i]["cdAssociacao"].ToString() + "\">" + _dtDados.Rows[i]["nmAssociacao"].ToString() + "</option>";
                }
            }
            return strOprions;
        }
    }
}