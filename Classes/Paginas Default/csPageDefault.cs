using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace wappKaraoke.Classes
{
    public class csPageDefault : csPage
    {
        private string _strPaginaCadastro;
        protected string strPaginaCadastro
        {
            get { return _strPaginaCadastro; }
            set { _strPaginaCadastro = value; }
        }

        private DataTable _dtDados;

        public override void Page_Load(object sender, EventArgs e)
        {
            _strPaginaCadastro = Request.Path.Substring(Request.Path.LastIndexOf("/") + 1);
            base.Page_Load(sender, e);
        }

        protected virtual void btnNovo1_Click(object sender, EventArgs e)
        {
            Response.Redirect(_strPaginaCadastro.Replace("Consulta", "Cadastro"));
        }
    }
}