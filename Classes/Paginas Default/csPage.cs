﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wappKaraoke.Classes
{
    public class csPage : System.Web.UI.Page
    {
        private Type _tobjCa;
        public Type tobjCa
        {
            get { return _tobjCa; }
            set { _tobjCa = value; }
        }

        private object _objCon;
        public object objCon
        {
            get { return _objCon; }
            set { _objCon = value; }
        }

        private Type _tobjCon;
        public Type tobjCon
        {
            get { return _tobjCon; }
            set { _tobjCon = value; }
        }

        private object _objCo;
        public object objCo
        {
            get { return _objCo; }
            set { _objCo = value; }
        }

        private Type _tobjCo;
        public Type tobjCo
        {
            get { return _tobjCo; }
            set { _tobjCo = value; }
        }

        private Literal _ltMensagemDefault;
        public Literal ltMensagemDefault
        {
            get { return _ltMensagemDefault; }
            set { _ltMensagemDefault = value; }
        }

        public virtual void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                InicializaSessions();
            }
            ConfigurarGridView();
        }

        protected virtual bool ConfigurarGridView()
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

        protected virtual void OrdenaDataTable(ref DataTable dtOrdena, string strOrdenacao)
        {
            DataView dv = dtOrdena.DefaultView;
            dv.Sort = strOrdenacao;
            dtOrdena = dv.ToTable();
        }

        protected virtual bool DataValida(string sData)
        {
            try
            {
                DateTime dt;

                dt = Convert.ToDateTime(sData);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}