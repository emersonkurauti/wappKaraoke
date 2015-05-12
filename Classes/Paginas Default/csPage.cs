using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.UI;

namespace wappKaraoke.Classes
{
    public class csPage : System.Web.UI.Page
    {
        public virtual void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                InicializaSessions();
            }
            ConfirarGridView();
        }

        protected virtual bool ConfirarGridView()
        {
            return true;
        }

        protected virtual void InicializaSessions()
        {
        }

        public virtual string MostraMensagem(string strMensagemTitulo, string strMensagemDesc, string tpMensagem)
        {
            StringBuilder sbMsgSucesso = new StringBuilder();

            sbMsgSucesso.AppendLine("<br />");
            sbMsgSucesso.AppendLine("<div class=\"alert alert-" + tpMensagem + "\" role=\"alert\">");
            sbMsgSucesso.AppendLine("<strong>" + strMensagemTitulo + "</strong>");
            sbMsgSucesso.AppendLine("<br />");
            sbMsgSucesso.AppendLine(strMensagemDesc);
            sbMsgSucesso.AppendLine("</div>");

            return sbMsgSucesso.ToString();
        }

        protected virtual void Mensagem(string sMensagem, Page Pagina)
        {
            ScriptManager.RegisterStartupScript(Pagina,
                this.GetType(), "Aviso", "<script language='javascript'>alert('" +
                sMensagem + "');</script>", false);
        }
    }
}