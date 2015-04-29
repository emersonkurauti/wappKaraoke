using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;

namespace wappKaraoke.Classes
{
    public class csCategorias
    {
        private DataTable _dtDados = new DataTable();
        public DataTable dtDados
        {
            get { return getDtDados(); }
            set { _dtDados = value; }
        }

        private DataTable getDtDados()
        {
            _dtDados.Columns.Add("cdCategoria", typeof(int));
            _dtDados.Columns.Add("deCategoria", typeof(string));

            _dtDados.Rows.Add(1, "Veterano A");
            _dtDados.Rows.Add(2, "Infantil B");
            _dtDados.Rows.Add(3, "Juvenil");

            return _dtDados;
        }

        public DropDownList CarregaDDL(DropDownList pDDL)
        {
            pDDL.DataSource = getDtDados();
            pDDL.DataValueField = "cdCategoria";
            pDDL.DataTextField = "deCategoria";
            pDDL.DataBind();
            pDDL.SelectedIndex = 0;

            return pDDL;
        }
    }
}