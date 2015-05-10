using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;

namespace wappKaraoke.Classes
{
    public class csJurados
    {
        private DataTable _dtDados = new DataTable();
        public DataTable dtDados
        {
            get { return getDtDados(); }
            set { _dtDados = value; }
        }

        private DataTable getDtDados()
        {
            _dtDados.Columns.Add("cdJurado", typeof(int));
            _dtDados.Columns.Add("nmJurado", typeof(string));

            _dtDados.Rows.Add(1, "Jurado de teste 1");
            _dtDados.Rows.Add(2, "Jurado de teste 2");
            _dtDados.Rows.Add(3, "Jurado de teste 3");

            return _dtDados;
        }

        public DropDownList CarregaDDL(DropDownList pDDL)
        {
            pDDL.DataSource = getDtDados();
            pDDL.DataValueField = "cdJurado";
            pDDL.DataTextField = "nmJurado";
            pDDL.DataBind();
            pDDL.SelectedIndex = 0;

            return pDDL;
        }
    }
}