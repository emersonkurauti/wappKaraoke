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

        protected override void btnSalvar_Click(object sender, EventArgs e)
        {
            bool bAlterouCantado = false;
            bool bAlterouKaraoke = false;
            
            bErro = false;

            string strCaminhoCantado = Request.PhysicalApplicationPath +
                wappKaraoke.Properties.Settings.Default.sCaminhoCantado;
            string strCaminhoKaraoke = Request.PhysicalApplicationPath +
                wappKaraoke.Properties.Settings.Default.sCaminhoKaraoke;

            //Salva Arquivos
            if (fluArquivoCantado.PostedFile != null && fluArquivoCantado.PostedFile.FileName != "")
            {
                try
                {
                    fluArquivoCantado.SaveAs(strCaminhoCantado + fluArquivoCantado.PostedFile.FileName);
                    bAlterouCantado = true;
                }
                catch
                {
                    deCaminhoMusica.Text = "";
                    bErro = true;
                }
            }

            if (fluArquivoKaraoke.PostedFile != null && fluArquivoKaraoke.PostedFile.FileName != "")
            {
                try
                {
                    fluArquivoKaraoke.SaveAs(strCaminhoKaraoke + fluArquivoKaraoke.PostedFile.FileName);
                    bAlterouKaraoke = true;
                }
                catch
                {
                    deCaminhoMusicaKaraoke.Text = "";
                    bErro = true;
                }
            }

            if (bErro)
                ltMensagem.Text = MostraMensagem("Falha ao salvar músicas", "Não foi possível enviar os arquivos das músicas.", Classes.Mensagem.csMensagem.msgDanger);

            base.btnSalvar_Click(sender, e);

            //Remove se der erro
            if (bErro)
            {
                if (bAlterouCantado)
                    System.IO.File.Delete(strCaminhoCantado + deCaminhoMusica.Text);

                if (bAlterouKaraoke)
                    System.IO.File.Delete(strCaminhoKaraoke + deCaminhoMusicaKaraoke.Text);
            }
        }
    }
}