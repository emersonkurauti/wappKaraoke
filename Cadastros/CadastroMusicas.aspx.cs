using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wappKaraoke.Classes;
using wappKaraoke.Classes.Model.Musicas;
using wappKaraoke.Classes.Controller; 

namespace wappKaraoke.Cadastros
{
    public partial class CadastroMusicas : csPageCadastro
    {
        public override void Page_Load(object sender, EventArgs e)
        {
            ltMensagemDefault = ltMensagem;
            tobjCa = typeof(caMusicas);
            objCon = new conMusicas();

            base.Page_Load(sender, e);
        }
    }
}