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
            _dtDados.Rows.Add("--Selecione a cor--");
            _dtDados.Rows.Add("AMARELO");
            _dtDados.Rows.Add("AZUL");
            _dtDados.Rows.Add("BRANCO");
            _dtDados.Rows.Add("LARANJADO");
            _dtDados.Rows.Add("VERDE");
            _dtDados.Rows.Add("VERMELHO");
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