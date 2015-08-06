using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;

namespace wappKaraoke.Classes
{
    public class csEstados
    {
        private DataTable _dtDados = new DataTable();
        public DataTable dtDados
        {
            get { return getDtDados(); }
            set { _dtDados = value; }
        }

        public csEstados()
        {
            _dtDados.Columns.Add("deEstado", typeof(string));
            _dtDados.Rows.Add("--Selecione a UF--");
            _dtDados.Rows.Add("AC");
            _dtDados.Rows.Add("AL");
            _dtDados.Rows.Add("AP");
            _dtDados.Rows.Add("AM");
            _dtDados.Rows.Add("BA");
            _dtDados.Rows.Add("CE");
            _dtDados.Rows.Add("DF");
            _dtDados.Rows.Add("ES");
            _dtDados.Rows.Add("GO");
            _dtDados.Rows.Add("MA");
            _dtDados.Rows.Add("MT");
            _dtDados.Rows.Add("MS");
            _dtDados.Rows.Add("MG");
            _dtDados.Rows.Add("PA");
            _dtDados.Rows.Add("PB");
            _dtDados.Rows.Add("PR");
            _dtDados.Rows.Add("PE");
            _dtDados.Rows.Add("PI");
            _dtDados.Rows.Add("RJ");
            _dtDados.Rows.Add("RN");
            _dtDados.Rows.Add("RS");
            _dtDados.Rows.Add("RO");
            _dtDados.Rows.Add("RR");
            _dtDados.Rows.Add("SC");
            _dtDados.Rows.Add("SP");
            _dtDados.Rows.Add("SE");
            _dtDados.Rows.Add("TO");
        }

        private DataTable getDtDados()
        {
            return _dtDados;
        }

        public DropDownList CarregaDDL(DropDownList pDDL)
        {
            pDDL.DataSource = getDtDados();
            pDDL.DataValueField = "deEstado";
            pDDL.DataTextField = "deEstado";
            pDDL.DataBind();
            pDDL.SelectedIndex = 0;

            return pDDL;
        }
    }
}