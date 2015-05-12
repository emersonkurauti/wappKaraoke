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
    public partial class ConsultaConcursos : csPageDefault
    {
        public override void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                csCidades vcsCidades = new csCidades();
                cdCidade = vcsCidades.CarregaDDL(cdCidade);

                DataTable dt = new DataTable();
                dt.Columns.Add("cdConcurso", typeof(int));
                dt.Columns.Add("nmConcurso", typeof(string));
                dt.Columns.Add("nmConcursoKanji", typeof(string));
                dt.Columns.Add("dtIniConcurso", typeof(string));
                dt.Columns.Add("dtFimConcurso", typeof(string));
                dt.Columns.Add("cdCidade", typeof(string));
                dt.Columns.Add("flFinalizado", typeof(string));

                for (int i = 0; i < 15; i++)
                {
                    DataRow dr = dt.NewRow();

                    dr["cdConcurso"] = i;
                    dr["nmConcurso"] = "Nome Concurso de teste - " + i;
                    dr["nmConcursoKanji"] = "Nome Kanji de teste - " + i;
                    dr["dtIniConcurso"] = "01/10/" + (i + 200);
                    dr["dtFimConcurso"] = (i + 1) + "/10/" + (i + 200);
                    dr["cdCidade"] = "Cidade teste - " + i;
                    dr["flFinalizado"] = "Sim";

                    dt.Rows.Add(dr);
                }

                gvDados.DataSource = dt;
                gvDados.DataBind();

                for (int i = 0; i < 15; i++)
                {
                    ((Literal)gvDados.Rows[i].FindControl("ltNomeKanji")).Text = @"" + dt.Rows[i]["nmConcurso"].ToString() +
                        " <br/> " + dt.Rows[i]["nmConcursoKanji"].ToString();
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
                gvDados.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
                gvDados.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
                gvDados.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
                gvDados.HeaderRow.Cells[5].Attributes["data-hide"] = "phone";
                gvDados.HeaderRow.Cells[6].Attributes["data-hide"] = "phone";
                gvDados.HeaderRow.Cells[7].Attributes["data-hide"] = "phone";

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