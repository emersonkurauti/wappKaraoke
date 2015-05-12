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
    public partial class ConsultaAssociacoes : csPageDefault
    {
        public override void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("cdAssociacao", typeof(int));
                dt.Columns.Add("nmAssociacao", typeof(string));
                dt.Columns.Add("nmPresidente", typeof(string));
                dt.Columns.Add("nmRepresentante", typeof(string));

                for (int i = 1; i < 15; i++)
                {
                    DataRow dr = dt.NewRow();

                    dr["cdAssociacao"] = i;
                    dr["nmAssociacao"] = "Associação de teste - " + i;
                    dr["nmPresidente"] = "Presidente de teste - " + i;
                    dr["nmRepresentante"] = "Representante de teste - " + i;

                    dt.Rows.Add(dr);
                }

                gvDados.DataSource = dt;
                gvDados.DataBind();
            }
            
            base.Page_Load(sender, e);
        }

        protected override bool ConfirarGridView()
        {
            if (!base.ConfirarGridView())
                return false;

            try
            {
                //Attribute to show the Plus Minus Button.
                gvDados.HeaderRow.Cells[1].Attributes["data-class"] = "expand";

                //Attribute to hide column in Phone.
                gvDados.HeaderRow.Cells[0].Attributes["data-hide"] = "phone";
                gvDados.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
                gvDados.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";

                //Adds THEAD and TBODY to GridView.
                gvDados.HeaderRow.TableSection = TableRowSection.TableHeader;

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}