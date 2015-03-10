using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wappKaraoke.Cadastros
{
    public partial class ConsultaCidades : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("cdCidade", typeof(int));
            dt.Columns.Add("nmCidade", typeof(string));
            dt.Columns.Add("deUF", typeof(string));

            for (int i = 1; i < 15; i++)
            {
                DataRow dr = dt.NewRow();

                dr["cdCidade"] = i;
                dr["nmCidade"] = "Cidade de teste - " + i;
                dr["deUF"] = "SP";

                dt.Rows.Add(dr);
            }

            gvCidade.DataSource = dt;
            gvCidade.DataBind();

            //Attribute to show the Plus Minus Button.
            gvCidade.HeaderRow.Cells[1].Attributes["data-class"] = "expand";

            //Attribute to hide column in Phone.
            gvCidade.HeaderRow.Cells[0].Attributes["data-hide"] = "phone";
            gvCidade.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";

            //Adds THEAD and TBODY to GridView.
            gvCidade.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
}