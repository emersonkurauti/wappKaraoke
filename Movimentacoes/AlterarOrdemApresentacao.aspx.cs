using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wappKaraoke.Classes.Controller;
using wappKaraoke.Classes;
using wappKaraoke.Classes.Model.Concursos;
using wappKaraoke.Classes.Mensagem;
using wappKaraoke.Classes.Model.CantoresFases;

namespace wappKaraoke.Movimentacoes
{
    public partial class AlterarOrdemApresentacao : csPage
    {
        public override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ltMensagem.Text = "";

                conConcursos objConConcursos = new conConcursos();
                objConConcursos.objCoConcursos.LimparAtributos();
                objConConcursos.objCoConcursos.strFiltro = " WHERE flConcursoCorrente = 'S'";

                Session["cdConcursoCorrenteOrdemApres"] = null;
                Session["cdFaseCorrenteOdemApres"] = null;

                if (conConcursos.Select())
                {
                    if (objConConcursos.dtDados != null && objConConcursos.dtDados.Rows.Count > 0)
                    {
                        Session["cdConcursoCorrenteOrdemApres"] = objConConcursos.dtDados.Rows[0][caConcursos.cdConcurso].ToString();
                        Session["cdFaseCorrenteOdemApres"] = objConConcursos.dtDados.Rows[0][caConcursos.cdFaseCorrente].ToString();
                    }
                }

                if (Session["cdConcursoCorrenteOrdemApres"] == null)
                {
                    ltMensagem.Text = MostraMensagem("Falha!", "Atenção, não existe concurso corrende definido!", csMensagem.msgDanger);
                }
            }
        }

        private bool ConsultarCantor()
        {
            csStatus vcsStatus = new csStatus();
            cdStatus = vcsStatus.CarregaDDL(cdStatus);

            conCantoresFases objConCantoresFases = new conCantoresFases();
            objConCantoresFases.objCoCantoresFases.LimparAtributos();
            objConCantoresFases.objCoCantoresFases.cdConcurso = Convert.ToInt32(Session["cdConcursoCorrenteOrdemApres"].ToString());
            objConCantoresFases.objCoCantoresFases.cdFase = Convert.ToInt32(Session["cdFaseCorrenteOdemApres"].ToString());
            objConCantoresFases.objCoCantoresFases.nuCantor = nuCantor.Text;

            if (!conCantoresFases.Select())
            {
                ltMensagem.Text = MostraMensagem("Falha!", "Não foi possível localizar o cantor.", csMensagem.msgDanger);
                nuCantor.Text = "";
                return false;
            }

            if (objConCantoresFases.dtDados.Rows.Count == 0)
            {
                ltMensagem.Text = MostraMensagem("Aviso!", "Não foi possível localizar o cantor pelo número informado.", csMensagem.msgWarning);
                nuCantor.Text = "";
                return false;
            }

            Session["cdCantorOrdemApres"] = objConCantoresFases.dtDados.Rows[0][caCantoresFases.cdCantor].ToString();
            Session["cdCategoriaOrdemApres"] = objConCantoresFases.dtDados.Rows[0][caCantoresFases.cdCategoria].ToString();

            ltInfoCantor.Text = MostraMensagem("Detalhes:",
                "Nome: " + objConCantoresFases.dtDados.Rows[0][caCantoresFases.CC_nmCantor].ToString() + "<br/>" +
                "Fase: " + objConCantoresFases.dtDados.Rows[0][caCantoresFases.CC_deFase].ToString() + "<br/>" +
                "Categoria: " + objConCantoresFases.dtDados.Rows[0][caCantoresFases.CC_deCategoria].ToString(),
                csMensagem.msgInfo);

            return true;
        }

        protected void nuCantor_TextChanged(object sender, EventArgs e)
        {
            ltMensagem.Text = "";
            ConsultarCantor();

            cdStatus.SelectedValue = wappKaraoke.Properties.Settings.Default.sCodStatusInicial;
        }

        protected void btnUltimoDaCategoria_Click(object sender, EventArgs e)
        {
            ltMensagem.Text = "";

            if (nuCantor.Text.Trim() == "")
            {
                ltMensagem.Text = MostraMensagem("Validação", "Informe o número do cantor.", csMensagem.msgWarning);
                return;
            }

            if (cdStatus.SelectedIndex <= 0)
            {
                ltMensagem.Text = MostraMensagem("Validação", "Selecione o status.", csMensagem.msgWarning);
                return;
            }

            conCantoresFases objConCantoresFases = new conCantoresFases();
            objConCantoresFases.objCoCantoresFases.cdConcurso = Convert.ToInt32(Session["cdConcursoCorrenteOrdemApres"].ToString());
            objConCantoresFases.objCoCantoresFases.cdCantor = Convert.ToInt32(Session["cdCantorOrdemApres"].ToString());
            objConCantoresFases.objCoCantoresFases.cdFase = Convert.ToInt32(Session["cdFaseCorrenteOdemApres"].ToString());
            objConCantoresFases.objCoCantoresFases.cdCategoria = Convert.ToInt32(Session["cdCategoriaOrdemApres"].ToString());
            objConCantoresFases.objCoCantoresFases.cdTpStatus = Convert.ToInt32(cdStatus.SelectedValue.ToString());

            if (!conCantoresFases.AlterarOrdemApresentacao())
            {
                ltMensagem.Text = MostraMensagem("Falha!", "Não foi possível alterar a ordem de apresentação do cantor.", csMensagem.msgDanger);
                return;
            }

            ltMensagem.Text = MostraMensagem("Sucesso!", "Ordem de apresentação alterada com sucesso.", csMensagem.msgSucess);
        }
    }
}