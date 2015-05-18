using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wappKaraoke.Classes;
using wappKaraoke.Classes.Model.Fases;
using wappKaraoke.Classes.Controller; 

namespace wappKaraoke.Cadastros
{
    public partial class CadastroFases : csPageCadastro
    {
        public override void Page_Load(object sender, EventArgs e)
        {
            ltMensagemDefault = ltMensagem;
            tobjCa = typeof(caFases);
            objCon = new conFases();

            base.Page_Load(sender, e);
        }
    }
}