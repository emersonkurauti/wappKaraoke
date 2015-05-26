using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wappKaraoke.Classes;
using wappKaraoke.Classes.Model.Categorias;
using wappKaraoke.Classes.Controller; 

namespace wappKaraoke.Cadastros
{
    public partial class CadastroCategorias : csPageCadastro
    {
        public override void Page_Load(object sender, EventArgs e)
        {
            ltMensagemDefault = ltMensagem;
            tobjCa = typeof(caCategorias);
            objCon = new conCategorias();

            base.Page_Load(sender, e);
        }
    }
}