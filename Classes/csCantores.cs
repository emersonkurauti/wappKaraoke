using System;
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
    }
}