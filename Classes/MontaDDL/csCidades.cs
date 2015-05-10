using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;

namespace wappKaraoke.Classes
{
    public class csCidades
    {
        private DataTable _dtDados = new DataTable();
        public DataTable dtDados
        {
            get { return getDtDados(); }
            set { _dtDados = value; }
        }

        private DataTable getDtDados()
        {
            _dtDados.Columns.Add("cdCidade", typeof(int));
            _dtDados.Columns.Add("nmCidade", typeof(string));

            _dtDados.Rows.Add(1, "Presidente Prudente");
            _dtDados.Rows.Add(2, "Presidente Venceslau");
            _dtDados.Rows.Add(3, "Presidente Bernardes");

            return _dtDados;
        }

        public DropDownList CarregaDDL(DropDownList pDDL)
        {
            pDDL.DataSource = getDtDados();
            pDDL.DataValueField = "cdCidade";
            pDDL.DataTextField = "nmCidade";
            pDDL.DataBind();
            pDDL.SelectedIndex = 0;

            return pDDL;
        }
    }
}