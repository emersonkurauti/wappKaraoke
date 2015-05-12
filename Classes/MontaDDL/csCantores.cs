﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;

namespace wappKaraoke.Classes
{
    public class csCantores
    {
        private DataTable _dtDados = new DataTable();
        public DataTable dtDados
        {
            get { return getDtDados(); }
            set { _dtDados = value; }
        }

        private DataTable getDtDados()
        {
            _dtDados.Columns.Add("cdCantor", typeof(int));
            _dtDados.Columns.Add("nmCantor", typeof(string));

            _dtDados.Rows.Add(1, "Cantor de teste");
            _dtDados.Rows.Add(2, "Cantor de teste 2");
            _dtDados.Rows.Add(3, "Cantor de teste 3");

            return _dtDados;
        }

        public DropDownList CarregaDDL(DropDownList pDDL)
        {
            pDDL.DataSource = getDtDados();
            pDDL.DataValueField = "cdCantor";
            pDDL.DataTextField = "nmCantor";
            pDDL.DataBind();
            pDDL.SelectedIndex = 0;

            return pDDL;
        }

        public string MontaSelect(string psId)
        {
            string strSelect = "<select name=\"nmCantor_" + psId + "\" id=\"idCantor_" + psId + "\" class=\"form-control selectpicker\" style=\"width:100%;text-align:left\">";

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
                strOprions += "<option value=\"" + _dtDados.Rows[0]["cdCantor"].ToString() + "\">" + _dtDados.Rows[0]["nmCantor"].ToString() + "</option>";

                for (int i = 1; i < _dtDados.Rows.Count; i++)
                {
                    strOprions += "<option value=\"" + _dtDados.Rows[i]["cdCantor"].ToString() + "\">" + _dtDados.Rows[i]["nmCantor"].ToString() + "</option>";
                }
            }
            return strOprions;
        }
    }
}