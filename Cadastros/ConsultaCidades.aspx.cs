using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using wappKaraoke.Classes;
using wappKaraoke.Classes.Model.Cidades;
using wappKaraoke.Classes.Controller;
using wappKaraoke.Classes.Mensagem;

namespace wappKaraoke.Cadastros
{
    public partial class ConsultaCidades : csPageDefault
    {
        public override void Page_Load(object sender, EventArgs e)
        {
            lnkBuscar = btnBuscar;
            gvDadosDefault = gvDados;
            ltMensagemDefault = ltMensagem;
            tobjCa = typeof(caCidades);
            objCon = new conCidades();

            base.Page_Load(sender, e);

            if (!this.IsPostBack)
            {
                csEstados vcsEstados = new csEstados();
                deUF = vcsEstados.CarregaDDL(deUF);
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