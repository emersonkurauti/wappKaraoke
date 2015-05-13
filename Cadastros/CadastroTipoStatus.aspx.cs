using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wappKaraoke.Classes;

namespace wappKaraoke.Cadastros
{
    public partial class CadastroTipoStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                csCorStatus vcsCorStatus = new csCorStatus();
                deCor = vcsCorStatus.CarregaDDL(deCor);
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {

        }
    }
}