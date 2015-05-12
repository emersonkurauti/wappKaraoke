using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wappKaraoke.Classes;
using System.Data;

namespace wappKaraoke.Cadastros
{
    public partial class ConsultaCantores : csPageDefault
    {
        public override void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                csCidades vcsCidades = new csCidades();
                cdCidade = vcsCidades.CarregaDDL(cdCidade);

                DataTable dt = new DataTable();
                dt.Columns.Add("cdCantor", typeof(int));
                dt.Columns.Add("nmCantor", typeof(string));
                dt.Columns.Add("nmNomeKanji", typeof(string));
                dt.Columns.Add("nmNomeArtistico", typeof(string));
                dt.Columns.Add("nuRG", typeof(string));
                dt.Columns.Add("nuTelefone", typeof(string));
                dt.Columns.Add("deEmail", typeof(string));
                dt.Columns.Add("dtNascimento", typeof(string));
                dt.Columns.Add("cdCidade", typeof(string));

                for (int i = 0; i < 15; i++)
                {
                    DataRow dr = dt.NewRow();

                    dr["cdCantor"] = i;
                    dr["nmCantor"] = "Nome Cantor de teste - " + i;
                    dr["nmNomeKanji"] = "Nome Kanji de teste - " + i;
                    dr["nmNomeArtistico"] = "Nome Artistico - " + i;
                    dr["nuRG"] = 493996575 + i;
                    dr["nuTelefone"] = "3903-234 " + i;
                    dr["deEmail"] = "emailteste" + i + "@hotmail.com ";
                    dr["dtNascimento"] = "12/01/" + (i + 1990);
                    dr["cdCidade"] = "Cidade teste - " + i;

                    dt.Rows.Add(dr);
                }

                gvDados.DataSource = dt;
                gvDados.DataBind();

                for (int i = 0; i < 15; i++)
                {
                    ((Literal)gvDados.Rows[i].FindControl("ltNomeKanji")).Text = @"" + dt.Rows[i]["nmCantor"].ToString() +
                        " <br/> " + dt.Rows[i]["nmNomeKanji"].ToString();
                }
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

                gvDados.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
                gvDados.HeaderRow.Cells[5].Attributes["data-hide"] = "all";
                gvDados.HeaderRow.Cells[6].Attributes["data-hide"] = "phone";
                gvDados.HeaderRow.Cells[7].Attributes["data-hide"] = "all";
                gvDados.HeaderRow.Cells[8].Attributes["data-hide"] = "phone";
                gvDados.HeaderRow.Cells[9].Attributes["data-hide"] = "all";

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