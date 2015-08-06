using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using wappKaraoke.Classes;
using wappKaraoke.Classes.Model.Concursos;
using wappKaraoke.Classes.Controller;
using wappKaraoke.Classes.Mensagem;

namespace wappKaraoke.Cadastros
{
    public partial class ConsultaConcursos : csPageDefault
    {
        public override void Page_Load(object sender, EventArgs e)
        {
            lnkBuscar = btnBuscar;
            gvDadosDefault = gvDados;
            ltMensagemDefault = ltMensagem;
            tobjCa = typeof(caConcursos);
            objCon = new conConcursos();

            base.Page_Load(sender, e);

            if (!this.IsPostBack)
            {
                csCidades vcsCidades = new csCidades();
                cdCidade = vcsCidades.CarregaDDL(cdCidade);

                NomeKanji();
            }
        }

        protected override bool ConfigurarGridView()
        {
            if (!base.ConfigurarGridView())
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

        protected override void btnBuscar_Click(object sender, EventArgs e)
        {
            base.btnBuscar_Click(sender, e);

            NomeKanji();
        }

        protected void NomeKanji()
        {
            if (dtDados != null)
            {
                for (int i = 0; i < dtDados.Rows.Count; i++)
                {
                    ((Literal)gvDados.Rows[i].FindControl("ltNomeKanji")).Text = @"" + dtDados.Rows[i]["nmConcurso"].ToString() +
                        " <br/> " + dtDados.Rows[i]["nmConcursoKanji"].ToString();
                }
            }
        }

        protected override void gvDados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            base.gvDados_RowDataBound(sender, e);

            string sData;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                sData = DataBinder.Eval(e.Row.DataItem, "dtIniConcurso").ToString();
                
                if (sData.Substring(0, 10).Equals("01/01/0001"))
                    e.Row.Cells[4].Text = "";

                sData = DataBinder.Eval(e.Row.DataItem, "dtFimConcurso").ToString();

                if (sData.Substring(0, 10).Equals("01/01/0001"))
                    e.Row.Cells[5].Text = "";
            }
        }

        protected void dtFimConcurso_TextChanged(object sender, EventArgs e)
        {
            if (!DataValida(dtFimConcurso.Text))
            {
                dtFimConcurso.Text = "";
            }

            TextChanged(sender, e);
        }

        protected void dtIniConcurso_TextChanged(object sender, EventArgs e)
        {
            if (!DataValida(dtIniConcurso.Text))
            {
                dtIniConcurso.Text = "";
            }

            TextChanged(sender, e);
        }
    }
}