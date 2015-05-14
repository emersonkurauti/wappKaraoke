using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wappKaraoke.Classes;
using wappKaraoke.Classes.Model.TipoStatus;
using wappKaraoke.Classes.Controller;

namespace wappKaraoke.Cadastros
{
    public partial class CadastroTipoStatus : csPageCadastro
    {
        public override void Page_Load(object sender, EventArgs e)
        {
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
    }
}