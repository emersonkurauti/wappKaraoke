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
    public partial class ConsultaGrupos : csPageDefault
    {
        public override void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("cdGrupo", typeof(int));
                dt.Columns.Add("deGrupo", typeof(string));

                for (int i = 1; i < 15; i++)
                {
                    DataRow dr = dt.NewRow();

                    dr["cdGrupo"] = i;
                    dr["deGrupo"] = "Grupo de teste - " + i;

                    dt.Rows.Add(dr);
                }

                gvDados.DataSource = dt;
                gvDados.DataBind();
            }

            base.Page_Load(sender, e);
        }

        protected override bool ConfigurarGridView()
        {
            if (!base.ConfigurarGridView())
                return false;

            try
            {
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