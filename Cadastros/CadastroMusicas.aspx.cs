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
            if (Request["__EVENTARGUMENT"] != null && Request["__EVENTARGUMENT"].Contains("CarregaMusica"))
            {
                string strParametro = Request["__EVENTARGUMENT"].ToString().Substring(0, Request["__EVENTARGUMENT"].ToString().IndexOf(';'));
                string strCaminhoTemp = Request.PhysicalApplicationPath +
                    wappKaraoke.Properties.Settings.Default.sCaminhoTemp;
                int intTamanhoParam;
                string strMusica;

                if (strParametro.Equals("CarregaMusicaCantado"))
                {
                    intTamanhoParam = Request["__EVENTARGUMENT"].ToString().IndexOf(';') + 1;
                    strMusica = Request["__EVENTARGUMENT"].ToString().Substring(intTamanhoParam, Request["__EVENTARGUMENT"].Length - intTamanhoParam);

                    ltAudioCantado.Text = "<div><audio id=\"AudioCantado\" src=\"../"
                            + wappKaraoke.Properties.Settings.Default.sCaminhoTemp.Replace("\\", "/")
                            + strMusica + "\"/></div>";

                    fluArquivoCantado.SaveAs(strCaminhoTemp + fluArquivoCantado.FileName);
                    deCaminhoMusica.Text = strMusica;
                }
                else
                    if (strParametro.Equals("CarregaMusicaKaraoke"))
                    {
                        intTamanhoParam = Request["__EVENTARGUMENT"].ToString().IndexOf(';') + 1;
                        strMusica = Request["__EVENTARGUMENT"].ToString().Substring(intTamanhoParam, Request["__EVENTARGUMENT"].Length - intTamanhoParam);
                        ltAudioKaraoke.Text = "<div><audio id=\"AudioKaraoke\" src=\"../" +
                                wappKaraoke.Properties.Settings.Default.sCaminhoTemp.Replace("\\", "/")
                                + strMusica + "\"/></div>";

                        fluArquivoKaraoke.SaveAs(strCaminhoTemp + fluArquivoKaraoke.FileName);
                        deCaminhoMusicaKaraoke.Text = strMusica;
                    }

                return;
            }

            ltMensagemDefault = ltMensagem;
            tobjCa = typeof(caMusicas);
            objCon = new conMusicas();

            base.Page_Load(sender, e);

            if (!IsPostBack)
            {
                Session["_deCaminhoMusicaCantado"] = deCaminhoMusica.Text;
                Session["_deCaminhoMusicaKaraoke"] = deCaminhoMusicaKaraoke.Text;
            }

            ltAudioCantado.Text = "<div><audio id=\"AudioCantado\" src=\"../"
                + wappKaraoke.Properties.Settings.Default.sCaminhoCantado.Replace("\\", "/")
                + deCaminhoMusica.Text + "\"/></div>";

            ltAudioKaraoke.Text = "<div><audio id=\"AudioKaraoke\" src=\"../"
                + wappKaraoke.Properties.Settings.Default.sCaminhoKaraoke.Replace("\\", "/")
                + deCaminhoMusicaKaraoke.Text + "\"/></div>";
        }

        protected override void CarregarDados(ControlCollection pControles)
        {
            base.CarregarDados(pControles);

            if (nuAnoLanc.Text.ToString() == "0")
                nuAnoLanc.Text = "";
        }

        protected override void btnSalvar_Click(object sender, EventArgs e)
        {
            bool bAlterouCantado = false;
            bool bAlterouKaraoke = false;
            
            bErro = false;

            string strCaminhoTemp = Request.PhysicalApplicationPath +
                wappKaraoke.Properties.Settings.Default.sCaminhoTemp;
            string strCaminhoCantado = Request.PhysicalApplicationPath +
                wappKaraoke.Properties.Settings.Default.sCaminhoCantado;
            string strCaminhoKaraoke = Request.PhysicalApplicationPath +
                wappKaraoke.Properties.Settings.Default.sCaminhoKaraoke;

            //Salva Arquivos
            if (deCaminhoMusica.Text != "")
            {
                try
                {
                    if (System.IO.File.Exists(strCaminhoTemp + deCaminhoMusica.Text))
                    {
                        try
                        {
                            if (Session["_deCaminhoMusicaCantado"] != null && Session["_deCaminhoMusicaCantado"].ToString() != "")
                                System.IO.File.Delete(strCaminhoCantado + Session["_deCaminhoMusicaCantado"].ToString());
                        }
                        catch { }

                        System.IO.File.Move(strCaminhoTemp + deCaminhoMusica.Text,
                            strCaminhoCantado + deCaminhoMusica.Text);
                        bAlterouCantado = true;
                    }
                }
                catch
                {
                    deCaminhoMusica.Text = "";
                    bErro = true;
                }
            }

            if (deCaminhoMusicaKaraoke.Text != "")
            {
                try
                {
                    if (System.IO.File.Exists(strCaminhoTemp + deCaminhoMusicaKaraoke.Text))
                    {
                        try
                        {
                            if (Session["_deCaminhoMusicaKaraoke"] != null && Session["_deCaminhoMusicaKaraoke"].ToString() != "")
                                System.IO.File.Delete(strCaminhoKaraoke + Session["_deCaminhoMusicaKaraoke"].ToString());
                        }
                        catch { }

                        System.IO.File.Move(strCaminhoTemp + deCaminhoMusicaKaraoke.Text,
                            strCaminhoKaraoke + deCaminhoMusicaKaraoke.Text);
                        bAlterouKaraoke = true;
                    }
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