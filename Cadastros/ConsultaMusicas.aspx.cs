using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wappKaraoke.Cadastros
{
    public partial class ConsultaMusicas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("cdMusica", typeof(int));
            dt.Columns.Add("nmMusica", typeof(string));
            dt.Columns.Add("nmCantor", typeof(string));
            dt.Columns.Add("nuAnoLanc", typeof(int));
            dt.Columns.Add("nmMusicaKanji", typeof(string));

            for (int i = 1; i < 15; i++)
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

            //Attribute to show the Plus Minus Button.
            gvDados.HeaderRow.Cells[1].Attributes["data-class"] = "expand";

            //Attribute to hide column in Phone.
            gvDados.HeaderRow.Cells[0].Attributes["data-hide"] = "phone";
            gvDados.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
            gvDados.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
            gvDados.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";

            //Adds THEAD and TBODY to GridView.
            gvDados.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
}