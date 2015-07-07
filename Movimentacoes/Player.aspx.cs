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
                    Session["cdConcurso"] = objConConcursos.dtDados.Rows[0][caConcursos.cdConcurso].ToString();
                }

                if (Session["cdConcurso"] == null)
                {
                    ltMensagem.Text = MostraMensagem("Falha!", "Não existe concurso corrente definido.", csMensagem.msgDanger);
                    return;
                }

                CarregaProximoCantor();

                ocsDisplay = new csDisplay(wappKaraoke.Properties.Settings.Default.sPortaCOM);
                int iNumero = Convert.ToInt32(Session["NumeroAtual"].ToString());
                //ocsDisplay.MudarNumero(iNumero.ToString());
                Session["ocsDisplay"] = ocsDisplay;
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
            objConCantoresFases.objCoCantoresFases.cdConcurso = Convert.ToInt32(Session["cdConcurso"].ToString());
            objConCantoresFases.objCoCantoresFases.cdTpStatus = Convert.ToInt32(wappKaraoke.Properties.Settings.Default.sCodStatusPronto);

            if (!conCantoresFases.SelectProximoCantor())
            {
                ltMensagem.Text = MostraMensagem("Falha!", "Não foi possível buscar o proximo cantor.", csMensagem.msgDanger);
                return;
            }

            Session["NumeroAtual"] = objConCantoresFases.dtDados.Rows[0][caCantoresFases.nuCantor].ToString();
            Session["cdCantor"] = objConCantoresFases.dtDados.Rows[0][caCantoresFases.cdCantor].ToString();
            Session["cdFase"] = objConCantoresFases.dtDados.Rows[0][caCantoresFases.cdFase].ToString();
            Session["cdCategoria"] = objConCantoresFases.dtDados.Rows[0][caCantoresFases.cdCategoria].ToString();

            ltMensagem.Text = MostraMensagem("Detalhes Cantor:", 
                "<br/>"+
                "Número: "+Session["NumeroAtual"]+"<br/>"+
                "Nome: " + objConCantoresFases.dtDados.Rows[0][caCantoresFases.CC_nmCantor].ToString() + "<br/>" +
                "Categoria: " + objConCantoresFases.dtDados.Rows[0][caCantoresFases.CC_deCategoria].ToString() + "<br/>" +
                "", csMensagem.msgInfo);
        }

        private void MudarStatusCantor(int pcdStatus)
        {
            conCantoresFases objConCantoresFases = new conCantoresFases();
            objConCantoresFases.objCoCantoresFases.LimparAtributos();
            objConCantoresFases.objCoCantoresFases.cdConcurso=Convert.ToInt32(Session["cdConcurso"].ToString());
            objConCantoresFases.objCoCantoresFases.cdFase = Convert.ToInt32(Session["cdFase"].ToString());
            objConCantoresFases.objCoCantoresFases.cdCategoria = Convert.ToInt32(Session["cdCategoria"].ToString());
            objConCantoresFases.objCoCantoresFases.cdCantor = Convert.ToInt32(Session["cdCantor"].ToString());
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

            ocsDisplay = (csDisplay)Session["ocsDisplay"];
            int iNumero = Convert.ToInt32(Session["NumeroAtual"].ToString());
            //ocsDisplay.MudarNumero(iNumero.ToString());
            Session["ocsDisplay"] = ocsDisplay;

            MudarStatusCantor(Convert.ToInt32(wappKaraoke.Properties.Settings.Default.sCodStatusCantando));
        }
    }
}