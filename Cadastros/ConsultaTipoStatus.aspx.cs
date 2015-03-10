using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using wappKaraoke.Classes;

namespace wappKaraoke.Cadastros
{
    public partial class ConsultaTipoStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                csCorStatus vcsCorStatus = new csCorStatus();
                deCor = vcsCorStatus.CarregaDDLCor(deCor);

                DataTable dt = new DataTable();
                dt.Columns.Add("cdTpStatus", typeof(int));
                dt.Columns.Add("deTpStatus", typeof(string));
                dt.Columns.Add("deCor", typeof(string));

                for (int i = 1; i < 15; i++)
                {
                    DataRow dr = dt.NewRow();

                    dr["cdTpStatus"] = i;
                    dr["deTpStatus"] = "Tipo Status de teste - " + i;
                    dr["deCor"] = "Azul";

                    dt.Rows.Add(dr);
                }

                gvTpStatus.DataSource = dt;
                gvTpStatus.DataBind();

                //Attribute to show the Plus Minus Button.
                gvTpStatus.HeaderRow.Cells[1].Attributes["data-class"] = "expand";

                //Attribute to hide column in Phone.
                gvTpStatus.HeaderRow.Cells[0].Attributes["data-hide"] = "phone";
                gvTpStatus.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
                
                //Adds THEAD and TBODY to GridView.
                gvTpStatus.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
    }
}