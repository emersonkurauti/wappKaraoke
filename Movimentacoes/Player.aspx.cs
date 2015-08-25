using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wappKaraoke.Display;
using wappKaraoke.Classes;
using wappKaraoke.Classes.Mensagem;
using wappKaraoke.Classes.Controller;
using wappKaraoke.Classes.Model.CantoresFases;
using wappKaraoke.Classes.Model.Concursos;

namespace wappKaraoke.Movimentacoes
{
    public partial class Player : csPage
    {
        private csDisplay ocsDisplay;

        public override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                conConcursos objConConcursos = new conConcursos();
                objConConcursos.objCoConcursos.LimparAtributos();
                objConConcursos.objCoConcursos.strFiltro = " WHERE flConcursoCorrente = 'S'";

                if (conConcursos.Select())
                {
                    Session["cdConcursoPlayer"] = objConConcursos.dtDados.Rows[0][caConcursos.cdConcurso].ToString();
                    Session["cdFaseCorrentePlayer"] = objConConcursos.dtDados.Rows[0][caConcursos.cdFaseCorrente].ToString();
                }

                if (Session["cdConcursoPlayer"] == null)
                {
                    ltMensagem.Text = MostraMensagem("Falha!", "Não existe concurso corrente definido.", csMensagem.msgDanger);
                    return;
                }

                if (Session["cdFaseCorrentePlayer"] == null)
                {
                    ltMensagem.Text = MostraMensagem("Falha!", "Não existe fase corrente definida.", csMensagem.msgDanger);
                    return;
                }

                conCantoresFases objConCantoresFases = new conCantoresFases();
                objConCantoresFases.objCoCantoresFases.LimparAtributos();
                objConCantoresFases.objCoCantoresFases.cdConcurso = Convert.ToInt32(Session["cdConcursoPlayer"].ToString());
                objConCantoresFases.objCoCantoresFases.cdFase = Convert.ToInt32(Session["cdFaseCorrentePlayer"].ToString());
                objConCantoresFases.objCoCantoresFases.cdTpStatus = Convert.ToInt32(wappKaraoke.Properties.Settings.Default.sCodStatusCantando);

                if (conCantoresFases.Select())
                {
                    if (objConCantoresFases.dtDados.Rows.Count > 0)
                    {
                        PreencheDadosCantor(objConCantoresFases);
                    }
                    else
                        CarregaProximoCantor();
                }
                else
                    CarregaProximoCantor();
            }
            else
            {
                if (Request["__EVENTARGUMENT"].Contains("AlterarStatusCantando"))
                {
                    if (Session["cdCantorCantandoPlayer"] != null)
                    {
                        MudarStatusCantor(Convert.ToInt32(wappKaraoke.Properties.Settings.Default.sCodStatusCantando));
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "TocarMusica();", true);
                    }
                }
            }
        }

        protected void btnFinalizado_Click(object sender, EventArgs e)
        {
            ProximoCantor();
        }

        protected void btnAvancar_Click(object sender, EventArgs e)
        {
            ProximoCantor(false);
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
        }

        private void CarregaProximoCantor()
        {
            conCantoresFases objConCantoresFases = new conCantoresFases();
            objConCantoresFases.objCoCantoresFases.LimparAtributos();
            objConCantoresFases.objCoCantoresFases.cdConcurso = Convert.ToInt32(Session["cdConcursoPlayer"].ToString());
            objConCantoresFases.objCoCantoresFases.cdFase = Convert.ToInt32(Session["cdFaseCorrentePlayer"].ToString());
            objConCantoresFases.objCoCantoresFases.cdTpStatus = Convert.ToInt32(wappKaraoke.Properties.Settings.Default.sCodStatusPronto);

            if (!conCantoresFases.SelectProximoCantor())
            {
                Session["cdCantorCantandoPlayer"] = null;
                ltMensagem.Text = MostraMensagem("Falha!", "Não foi possível buscar o proximo cantor.", csMensagem.msgDanger);
                return;
            }

            if (objConCantoresFases.dtDados.Rows.Count == 0)
            {
                Session["cdCantorCantandoPlayer"] = null;
                ltMensagem.Text = MostraMensagem("Validação!", "Não existe cantor pronto para cantar.", csMensagem.msgWarning);
                return;
            }

            PreencheDadosCantor(objConCantoresFases);
        }

        private void MudarStatusCantor(int pcdStatus)
        {
            conCantoresFases objConCantoresFases = new conCantoresFases();
            objConCantoresFases.objCoCantoresFases.LimparAtributos();
            objConCantoresFases.objCoCantoresFases.cdConcurso = Convert.ToInt32(Session["cdConcursoPlayer"].ToString());
            objConCantoresFases.objCoCantoresFases.cdFase = Convert.ToInt32(Session["cdFaseCorrentePlayer"].ToString());
            objConCantoresFases.objCoCantoresFases.cdCategoria = Convert.ToInt32(Session["cdCategoriaPlayer"].ToString());
            objConCantoresFases.objCoCantoresFases.cdCantor = Convert.ToInt32(Session["cdCantorCantandoPlayer"].ToString());
            objConCantoresFases.objCoCantoresFases.cdTpStatus = pcdStatus;

            if (!conCantoresFases.AlterarStatus())
            {
                ltMensagem.Text = MostraMensagem("Falha!", "Não foi possível alterar o status do cantor!", csMensagem.msgDanger);
            }

        }

        private void ProximoCantor(bool pbAtualFinalizado = true)
        {
            if (pbAtualFinalizado)
            {
                MudarStatusCantor(Convert.ToInt32(wappKaraoke.Properties.Settings.Default.sCodStatusCantou));
            }
            else
            {
                MudarStatusCantor(Convert.ToInt32(wappKaraoke.Properties.Settings.Default.sCodStatusMudouOrdem));
            }

            CarregaProximoCantor();
        }

        private void PreencheDadosCantor(conCantoresFases objConCantoresFases)
        {
            Session["NumeroAtualPlayer"] = objConCantoresFases.dtDados.Rows[0][caCantoresFases.nuCantor].ToString();
            Session["cdCantorCantandoPlayer"] = objConCantoresFases.dtDados.Rows[0][caCantoresFases.cdCantor].ToString();
            Session["cdCategoriaPlayer"] = objConCantoresFases.dtDados.Rows[0][caCantoresFases.cdCategoria].ToString();

            ltMensagem.Text = MostraMensagem("Detalhes Cantor:",
                "<br/>" +
                "Número: " + Session["NumeroAtualPlayer"] + "<br/>" +
                "Nome: " + objConCantoresFases.dtDados.Rows[0][caCantoresFases.CC_nmCantor].ToString() + "<br/>" +
                "Categoria: " + objConCantoresFases.dtDados.Rows[0][caCantoresFases.CC_deCategoria].ToString() + "<br/>" +
                "", csMensagem.msgInfo);

            CarregarMusica(objConCantoresFases);

            TrocarNumetoDisplay();
        }

        private void CarregarMusica(conCantoresFases objConCantoresFases)
        {
            string strArquivoMusica = wappKaraoke.Properties.Settings.Default.sCaminhoKaraokeConcurso +
                Session["NumeroAtualPlayer"].ToString();

            ltAudio.Text = "<audio id=\"Audio\"src=\"" + strArquivoMusica + ".mp3\" controls=\"true\"/>";
        }

        protected void nuCantor_TextChanged(object sender, EventArgs e)
        {
            conCantoresFases objConCantoresFases = new conCantoresFases();
            objConCantoresFases.objCoCantoresFases.LimparAtributos();
            objConCantoresFases.objCoCantoresFases.cdConcurso = Convert.ToInt32(Session["cdConcursoPlayer"].ToString());
            objConCantoresFases.objCoCantoresFases.cdFase = Convert.ToInt32(Session["cdFaseCorrentePlayer"].ToString());
            objConCantoresFases.objCoCantoresFases.nuCantor = nuCantor.Text;

            if (!conCantoresFases.Select())
            {
                ltMensagem.Text = MostraMensagem("Falha!", "Não foi possível localizar o cantor pelo número.", csMensagem.msgDanger);
                return;
            }

            if (objConCantoresFases.dtDados.Rows.Count == 0)
            {
                ltMensagem.Text = MostraMensagem("Validação!", "Não existe cantor com o número informado.", csMensagem.msgWarning);
                return;
            }

            PreencheDadosCantor(objConCantoresFases);
        }

        private void TrocarNumetoDisplay()
        {
            ocsDisplay = (csDisplay)Session["ocsDisplayPlayer"];
            int iNumero = Convert.ToInt32(Session["NumeroAtualPlayer"].ToString().Substring(0, 3));
            //ocsDisplay.MudarNumero(iNumero.ToString());
            Session["ocsDisplayPlayer"] = ocsDisplay;
        }
    }
}