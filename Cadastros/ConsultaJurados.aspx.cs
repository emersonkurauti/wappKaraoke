using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wappKaraoke.Classes;
using System.Data;
using wappKaraoke.Classes.Model.Jurados;
using wappKaraoke.Classes.Controller;
using wappKaraoke.Classes.Mensagem;

namespace wappKaraoke.Cadastros
{
    public partial class ConsultaJurados : csPageDefault
    {
        public override void Page_Load(object sender, EventArgs e)
        {
            gvDadosDefault = gvDados;
            ltMensagemDefault = ltMensagem;
            tobjCa = typeof(caJurados);
            objCon = new conJurados();

            base.Page_Load(sender, e);

            if (!this.IsPostBack)
            {
                csCidades vcsCidades = new csCidades();
                cdCidade = vcsCidades.CarregaDDL(cdCidade);

                for (int i = 0; i < dtDados.Rows.Count; i++)
                {
                    ((Literal)gvDados.Rows[i].FindControl("ltNomeKanji")).Text = @"" + dtDados.Rows[i]["nmJurado"].ToString() +
                        " <br/> " + dtDados.Rows[i]["nmNomeKanji"].ToString();
                }
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