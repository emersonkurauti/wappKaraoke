using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;

namespace wappKaraoke.Classes
{
    public class csCorStatus
    {
        private DataTable _dtDados = new DataTable();
        public DataTable dtDados
        {
            get { return getDtDados(); }
            set { _dtDados = value; }
        }

        public csCorStatus()
        {
            _dtDados.Columns.Add("deCor", typeof(string));
            _dtDados.Rows.Add("Amarelo");
            _dtDados.Rows.Add("Azul");
            _dtDados.Rows.Add("Laranja");
            _dtDados.Rows.Add("Magenta");
            _dtDados.Rows.Add("Verde");
            _dtDados.Rows.Add("Vermelho");
        }

        private DataTable getDtDados()
        {
            return _dtDados;
        }

        public DropDownList CarregaDDL(DropDownList pDDL)
        {
            pDDL.DataSource = getDtDados();
            pDDL.DataValueField = "deCor";
            pDDL.DataTextField = "deCor";
            pDDL.DataBind();
            pDDL.SelectedIndex = 0;

            return pDDL;
        }
    }
}