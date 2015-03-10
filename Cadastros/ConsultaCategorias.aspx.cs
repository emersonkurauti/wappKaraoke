using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wappKaraoke.Cadastros
{
    public partial class ConsultaCategorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("cdCategoria", typeof(int));
            dt.Columns.Add("deCategoria", typeof(string));

            for (int i = 1; i < 15; i++)
            {
                DataRow dr = dt.NewRow();

                dr["cdCategoria"] = i;
                dr["deCategoria"] = "Categoria de teste - " + i;

                dt.Rows.Add(dr);
            }

            gvCategorias.DataSource = dt;
            gvCategorias.DataBind();
        }
    }
}