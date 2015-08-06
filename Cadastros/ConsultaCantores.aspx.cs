using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wappKaraoke.Classes;
using System.Data;
using wappKaraoke.Classes.Model.Cantores;
using wappKaraoke.Classes.Controller;
using wappKaraoke.Classes.Mensagem;

namespace wappKaraoke.Cadastros
{
    public partial class ConsultaCantores : csPageDefault
    {
        public override void Page_Load(object sender, EventArgs e)
        {
            lnkBuscar = btnBuscar;
            gvDadosDefault = gvDados;
            ltMensagemDefault = ltMensagem;
            tobjCa = typeof(caCantores);
            objCon = new conCantores();

            base.Page_Load(sender, e);

            NomeKanji();
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

        protected override void btnBuscar_Click(object sender, EventArgs e)
        {
            base.btnBuscar_Click(sender, e);

            NomeKanji();
        }

        protected void NomeKanji()
        {
            if (dtDados != null)
            {
                csCidades vcsCidades = new csCidades();
                cdCidade = vcsCidades.CarregaDDL(cdCidade);

                for (int i = 0; i < dtDados.Rows.Count; i++)
                {
                    ((Literal)gvDados.Rows[i].FindControl("ltNomeKanji")).Text = @"" + dtDados.Rows[i]["nmCantor"].ToString() +
                        " <br/> " + dtDados.Rows[i]["nmNomeKanji"].ToString();
                }
            }
        }

        protected override void gvDados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            base.gvDados_RowDataBound(sender, e);

            string sData;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                sData = DataBinder.Eval(e.Row.DataItem, "dtNascimento").ToString();

                if (sData.Substring(0, 10).Equals("01/01/0001"))
                    e.Row.Cells[4].Text = "";
            }
        }

        protected void dtNascimento_TextChanged(object sender, EventArgs e)
        {
            if (!DataValida(dtNascimento.Text))
                dtNascimento.Text = "";

            TextChanged(sender, e);
        }
    }
}