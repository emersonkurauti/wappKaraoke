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
        private DataTable _dtCorStatus = new DataTable();
        public DataTable dtCorStatus
        {
            get { return getDtCorStatus(); }
            set { _dtCorStatus = value; }
        }

        private DataTable getDtCorStatus()
        {
            _dtCorStatus.Columns.Add("deCor", typeof(string));
            _dtCorStatus.Rows.Add("Amarelo");
            _dtCorStatus.Rows.Add("Azul");
            _dtCorStatus.Rows.Add("Laranja");
            _dtCorStatus.Rows.Add("Magenta");
            _dtCorStatus.Rows.Add("Verde");
            _dtCorStatus.Rows.Add("Vermelho");

            return _dtCorStatus;
        }

        public DropDownList CarregaDDLCor(DropDownList pDDL)
        {
            pDDL.DataSource = getDtCorStatus();
            pDDL.DataValueField = "deCor";
            pDDL.DataTextField = "deCor";
            pDDL.DataBind();
            pDDL.SelectedIndex = 0;

            return pDDL;
        }
    }
}