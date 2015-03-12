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
    public partial class ConsultaConcursos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                csCidades vcsCidades = new csCidades();
                cdCidade = vcsCidades.CarregaDDL(cdCidade);

                DataTable dt = new DataTable();
                dt.Columns.Add("cdConcurso", typeof(int));
                dt.Columns.Add("nmConcurso", typeof(string));
                dt.Columns.Add("nmConcursoKanji", typeof(string));
                dt.Columns.Add("dtConcurso", typeof(string));
                dt.Columns.Add("cdCidade", typeof(string));

                for (int i = 0; i < 15; i++)
                {
                    DataRow dr = dt.NewRow();

                    dr["cdConcurso"] = i;
                    dr["nmConcurso"] = "Nome Concurso de teste - " + i;
                    dr["nmConcursoKanji"] = "Nome Kanji de teste - " + i;
                    dr["dtConcurso"] = "01/10/2014";
                    dr["cdCidade"] = "Cidade teste - " + i;

                    dt.Rows.Add(dr);
                }

                gvDados.DataSource = dt;
                gvDados.DataBind();

                for (int i = 0; i < 15; i++)
                {
                    ((Literal)gvDados.Rows[i].FindControl("ltNomeKanji")).Text = @"" + dt.Rows[i]["nmConcurso"].ToString() +
                        " <br/> " + dt.Rows[i]["nmConcursoKanji"].ToString();
                }

                //Attribute to show the Plus Minus Button.
                gvDados.HeaderRow.Cells[1].Attributes["data-class"] = "expand";

                //Attribute to hide column in Phone.
                gvDados.HeaderRow.Cells[0].Attributes["data-hide"] = "phone";
                gvDados.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
                gvDados.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
                gvDados.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
                gvDados.HeaderRow.Cells[5].Attributes["data-hide"] = "phone";

                //Adds THEAD and TBODY to GridView.
                gvDados.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
    }
}