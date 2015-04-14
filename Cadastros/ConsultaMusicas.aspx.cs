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
    public partial class ConsultaMusicas : csPageDefault
    {
        public override void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("cdMusica", typeof(int));
                dt.Columns.Add("nmMusica", typeof(string));
                dt.Columns.Add("nmCantor", typeof(string));
                dt.Columns.Add("nuAnoLanc", typeof(int));
                dt.Columns.Add("nmMusicaKanji", typeof(string));

                for (int i = 0; i < 15; i++)
                {
                    DataRow dr = dt.NewRow();

                    dr["cdMusica"] = i;
                    dr["nmMusica"] = "Nome Música de teste - " + i;
                    dr["nmCantor"] = "Nome Cantor de teste - " + i;
                    dr["nuAnoLanc"] = 2000 + i;
                    dr["nmMusicaKanji"] = "KANJI - " + i;

                    dt.Rows.Add(dr);
                }

                gvDados.DataSource = dt;
                gvDados.DataBind();

                for (int i = 0; i < 15; i++)
                {
                    ((Literal)gvDados.Rows[i].FindControl("ltNomeKanji")).Text = @"" + dt.Rows[i]["nmMusica"].ToString() + " <br/> " + dt.Rows[i]["nmMusicaKanji"].ToString();
                }
            }

            base.Page_Load(sender, e);
        }

        public override void ConfirarGridView()
        {
            base.ConfirarGridView();

            //Attribute to show the Plus Minus Button.
            gvDados.HeaderRow.Cells[1].Attributes["data-class"] = "expand";

            //Attribute to hide column in Phone.
            gvDados.HeaderRow.Cells[0].Attributes["data-hide"] = "phone";
            gvDados.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
            gvDados.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
            gvDados.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
            gvDados.HeaderRow.Cells[7].Attributes["data-hide"] = "phone";

            //Adds THEAD and TBODY to GridView.
            gvDados.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
}