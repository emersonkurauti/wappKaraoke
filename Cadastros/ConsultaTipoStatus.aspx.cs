using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using wappKaraoke.Classes;
using wappKaraoke.Classes.Model.TipoStatus;
using wappKaraoke.Classes.Controller;
using wappKaraoke.Mensagem;

namespace wappKaraoke.Cadastros
{
    public partial class ConsultaTipoStatus : csPageDefault
    {
        public override void Page_Load(object sender, EventArgs e)
        {
            gvDadosDefault = gvDados;
            ltMensagemDefault = ltMensagem;
            tobjCa = typeof(caTipoStatus);
            objCon = new conTipoStatus();

            if (!this.IsPostBack)
            {
                csCorStatus vcsCorStatus = new csCorStatus();
                deCor = vcsCorStatus.CarregaDDL(deCor);
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

                //Adds THEAD and TBODY to GridView.
                gvDados.HeaderRow.TableSection = TableRowSection.TableHeader;

                return true;
            }
            catch
            {
                return false;
            }
        }

        protected override void InicializaSessions()
        {
            base.InicializaSessions();

            Session["gvRow"] = null;
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            conTipoStatus objconTipoStatus = new conTipoStatus();
            DataTable dt;
            bool bFiltro = false;

            objconTipoStatus.objCoTipoStatus.LimparAtributos();
            int intCodigo;

            try
            {
                if (cdTpStatus.Text != "")
                {
                    Int32.TryParse(cdTpStatus.Text, out intCodigo);
                    objconTipoStatus.objCoTipoStatus.strFiltro += AndWhere(objconTipoStatus.objCoTipoStatus.strFiltro) + " cdTpStatus = " + intCodigo.ToString();
                    bFiltro = true;
                }

                if (deCor.SelectedIndex != 0)
                {
                    objconTipoStatus.objCoTipoStatus.strFiltro += AndWhere(objconTipoStatus.objCoTipoStatus.strFiltro) + " deCor = '" + deCor.SelectedItem.Text + "'";
                    bFiltro = true;
                }

                if (deTpStatus.Text != "")
                {
                    if (bFiltro)
                        objconTipoStatus.objCoTipoStatus.strFiltro += AndWhere(objconTipoStatus.objCoTipoStatus.strFiltro) + " deTpStatus LIKE " + "'%" + deTpStatus.Text + "%'";
                    else
                        objconTipoStatus.objCoTipoStatus.strFiltro += AndWhere(objconTipoStatus.objCoTipoStatus.strFiltro) + " deTpStatus LIKE " + "'%" + deTpStatus.Text + "%'";
                }



                if (objconTipoStatus.objCoTipoStatus.Select(out dt))
                {
                    dtDados = dt;
                    gvDados.DataSource = dtDados;
                    gvDados.DataBind();
                }

                ConfirarGridView();

                Session["dtDados"] = dtDados;
            }
            catch
            {
                ltMensagem.Text = MostraMensagem(csMensagem.msgTitFalaAoConsultar, csMensagem.msgFalhaAoConsultarFiltro, csMensagem.msgDanger);
            }
        }
    }
}