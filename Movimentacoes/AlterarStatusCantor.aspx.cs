using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using wappKaraoke.Classes;
using wappKaraoke.Classes.Mensagem;
using wappKaraoke.Classes.Controller;
using wappKaraoke.Classes.Model.CantoresFases;
using wappKaraoke.Classes.Model.Cantores;
using wappKaraoke.Classes.Model.Concursos;

namespace wappKaraoke.Movimentacoes
{
    public partial class AlterarStatusCantor : csPage
    {
        public override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ltMensagem.Text = "";

                csStatus vcsStatus = new csStatus();
                cdStatus = vcsStatus.CarregaDDL(cdStatus);

                conConcursos objConConcursos = new conConcursos();
                objConConcursos.objCoConcursos.LimparAtributos();
                objConConcursos.objCoConcursos.strFiltro = " WHERE flConcursoCorrente = 'S'";

                Session["cdConcursoCorrenteStatusCantor"] = null;
                Session["cdFaseCorrenteStatusCantor"] = null;

                if (conConcursos.Select())
                {
                    if (objConConcursos.dtDados != null && objConConcursos.dtDados.Rows.Count > 0)
                    {
                        Session["cdConcursoCorrenteStatusCantor"] = objConConcursos.dtDados.Rows[0][caConcursos.cdConcurso].ToString();
                        Session["cdFaseCorrenteStatusCantor"] = objConConcursos.dtDados.Rows[0][caConcursos.cdFaseCorrente].ToString();
                    }
                }

                if (Session["cdFaseCorrenteStatusCantor"] == null)
                {
                    ltMensagem.Text = MostraMensagem("Falha!", "Atenção, não existe fase corrende definida!", csMensagem.msgDanger);
                }

                if (Session["cdConcursoCorrenteStatusCantor"] == null)
                {
                    ltMensagem.Text = MostraMensagem("Falha!", "Atenção, não existe concurso corrende definido!", csMensagem.msgDanger);
                }
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
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
            objConCantoresFases.objCoCantoresFases.cdConcurso = Convert.ToInt32(Session["cdConcursoCorrenteStatusCantor"].ToString());
            objConCantoresFases.objCoCantoresFases.cdCantor = Convert.ToInt32(Session["cdCantorStatusCantor"].ToString());
            objConCantoresFases.objCoCantoresFases.cdFase = Convert.ToInt32(Session["cdFaseCorrenteStatusCantor"].ToString());
            objConCantoresFases.objCoCantoresFases.cdCategoria = Convert.ToInt32(Session["cdCategoriaStatusCantor"].ToString());
            objConCantoresFases.objCoCantoresFases.cdTpStatus = Convert.ToInt32(cdStatus.SelectedValue.ToString());

            if (!conCantoresFases.AlterarStatus())
            {
                ltMensagem.Text = MostraMensagem("Falha!", "Não foi possível alterar o status do cantor.", csMensagem.msgDanger);
                return;
            }

            ltMensagem.Text = MostraMensagem("Sucesso!", "Status alterado com sucesso.", csMensagem.msgSucess);
        }

        protected void nuCantor_TextChanged(object sender, EventArgs e)
        {
            ltMensagem.Text = "";
            ConsultarCantor();
        }

        private bool ConsultarCantor()
        {
            conCantoresFases objConCantoresFases = new conCantoresFases();
            objConCantoresFases.objCoCantoresFases.LimparAtributos();
            objConCantoresFases.objCoCantoresFases.cdConcurso = Convert.ToInt32(Session["cdConcursoCorrenteStatusCantor"].ToString());
            objConCantoresFases.objCoCantoresFases.cdFase = Convert.ToInt32(Session["cdFaseCorrenteStatusCantor"].ToString());
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

            Session["cdCantorStatusCantor"] = objConCantoresFases.dtDados.Rows[0][caCantoresFases.cdCantor].ToString();
            Session["cdCategoriaStatusCantor"] = objConCantoresFases.dtDados.Rows[0][caCantoresFases.cdCategoria].ToString();

            ltInfoCantor.Text = MostraMensagem("Detalhes:",
                "Nome: " + objConCantoresFases.dtDados.Rows[0][caCantoresFases.CC_nmCantor].ToString() + "<br/>" +
                "Fase: " + objConCantoresFases.dtDados.Rows[0][caCantoresFases.CC_deFase].ToString() + "<br/>" +
                "Categoria: " + objConCantoresFases.dtDados.Rows[0][caCantoresFases.CC_deCategoria].ToString(),
                csMensagem.msgInfo);

            return true;
        }
    }
}