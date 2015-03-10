using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wappKaraoke.Cadastros
{
    public partial class ConsultaFases : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("cdFase", typeof(int));
            dt.Columns.Add("deFase", typeof(string));

            for (int i = 1; i < 15; i++)
            {
                DataRow dr = dt.NewRow();

                dr["cdFase"] = i;
                dr["deFase"] = "Fase de teste - " + i;

                dt.Rows.Add(dr);
            }

            gvFases.DataSource = dt;
            gvFases.DataBind();
        }
    }
}