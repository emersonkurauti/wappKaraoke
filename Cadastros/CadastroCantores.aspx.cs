using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wappKaraoke.Classes;
using wappKaraoke.Classes.Model.Cantores;
using wappKaraoke.Classes.Controller;
using wappKaraoke.Classes.Mensagem; 

namespace wappKaraoke.Cadastros
{
    public partial class CadastroCantores : csPageCadastro
    {
        public override void Page_Load(object sender, EventArgs e)
        {
            ltMensagemDefault = ltMensagem;
            tobjCa = typeof(caCantores);
            objCon = new conCantores();

            if (!this.IsPostBack)
            {
                csCidades vcsCidades = new csCidades();
                cdCidade = vcsCidades.CarregaDDL(cdCidade);
            }

            base.Page_Load(sender, e);
        }

        protected override void btnSalvar_Click(object sender, EventArgs e)
        {
            if (nmNomeKanji.Text.Trim() == "")
            {
                ltMensagem.Text = MostraMensagem("Validação!", "Informe o nome em Kanji do cantor.", csMensagem.msgDanger);
                return;
            }

            base.btnSalvar_Click(sender, e);
        }

        protected void dtNascimento_TextChanged(object sender, EventArgs e)
        {
            if (!DataValida(dtNascimento.Text))
                dtNascimento.Text = "";
        }
    }
}