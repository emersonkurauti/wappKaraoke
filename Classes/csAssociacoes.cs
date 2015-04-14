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
            _dtDados.Rows.Add(3, "ACAD");

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
    }
}