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

namespace wappKaraoke.Movimentacoes
{
    public partial class AlterarStatusCantor : csPage
    {
        public override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ltMensagem.Text = "";

                csConcursos vcsConcursos = new csConcursos();
                cdConcurso = vcsConcursos.CarregaDDL(cdConcurso);

                csStatus vcsStatus = new csStatus();
                cdStatus = vcsStatus.CarregaDDL(cdStatus);
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            ltMensagem.Text = "";

            if (cdConcurso.SelectedIndex <= 0)
            {
                ltMensagem.Text = MostraMensagem("Validação", "Selecione o concurso.", csMensagem.msgWarning);
                return;
            }

            if (cdFase.SelectedIndex <= 0)
            {
                ltMensagem.Text = MostraMensagem("Validação", "Selecione a fase.", csMensagem.msgWarning);
                return;
            }

            if (cdCategoria.SelectedIndex <= 0)
            {
                ltMensagem.Text = MostraMensagem("Validação", "Selecione a categoria.", csMensagem.msgWarning);
                return;
            }

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
            objConCantoresFases.objCoCantoresFases.cdConcurso = Convert.ToInt32(cdConcurso.SelectedValue.ToString());
            objConCantoresFases.objCoCantoresFases.cdCantor = Convert.ToInt32(Session["cdCantor"].ToString());
            objConCantoresFases.objCoCantoresFases.cdFase = Convert.ToInt32(cdFase.SelectedValue.ToString());
            objConCantoresFases.objCoCantoresFases.cdCategoria = Convert.ToInt32(cdCategoria.SelectedValue.ToString());
            objConCantoresFases.objCoCantoresFases.cdTpStatus = Convert.ToInt32(cdStatus.SelectedValue.ToString());
            objConCantoresFases.objCoCantoresFases.cdMusica = Convert.ToInt32(Session["cdMusica"].ToString());
            objConCantoresFases.objCoCantoresFases.nuCantor = Session["nuCantor"].ToString();
            objConCantoresFases.objCoCantoresFases.nuOrdemApresentacao = Convert.ToInt32(Session["nuOrdemApresentacao"].ToString());
            objConCantoresFases.objCoCantoresFases.nuNotafinal = Convert.ToDecimal(Session["nuNotafinal"].ToString());
            objConCantoresFases.objCoCantoresFases.pcDesconto = Convert.ToDecimal(Session["pcDesconto"].ToString());
            objConCantoresFases.objCoCantoresFases.deCaminhoMusica = Session["deCaminhoMusica"].ToString();

            if (!conCantoresFases.Alterar())
            {
                ltMensagem.Text = MostraMensagem("Falha!", "Não foi possível alterar o status do cantor.", csMensagem.msgDanger);
                return;
            }

            ltMensagem.Text = MostraMensagem("Sucesso!", "Status alterado com sucesso.", csMensagem.msgSucess);
        }

        protected void nuCantor_TextChanged(object sender, EventArgs e)
        {
            ltMensagem.Text = "";

            if (ConsultarCantor())
            {
                CarregarDDLFases();
            }
        }

        private bool ConsultarCantor()
        {
            conCantoresFases objConCantoresFases = new conCantoresFases();
            objConCantoresFases.objCoCantoresFases.LimparAtributos();
            objConCantoresFases.objCoCantoresFases.cdConcurso = Convert.ToInt32(cdConcurso.SelectedValue.ToString());
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

            Session["cdFase"] = objConCantoresFases.dtDados.Rows[0][caCantoresFases.cdFase].ToString();
            Session["cdMusica"] = objConCantoresFases.dtDados.Rows[0][caCantoresFases.cdMusica].ToString();
            Session["cdCategoria"] = objConCantoresFases.dtDados.Rows[0][caCantoresFases.cdCategoria].ToString();
            Session["pcDesconto"] = objConCantoresFases.dtDados.Rows[0][caCantoresFases.pcDesconto].ToString();
            Session["nuNotafinal"] = objConCantoresFases.dtDados.Rows[0][caCantoresFases.nuNotafinal].ToString();
            Session["nuOrdemApresentacao"] = objConCantoresFases.dtDados.Rows[0][caCantoresFases.nuOrdemApresentacao].ToString();
            Session["nuCantor"] = objConCantoresFases.dtDados.Rows[0][caCantoresFases.nuCantor].ToString();
            Session["deCaminhoMusica"] = objConCantoresFases.dtDados.Rows[0][caCantoresFases.deCaminhoMusica].ToString();
            Session["cdCantor"] = objConCantoresFases.dtDados.Rows[0][caCantoresFases.cdCantor].ToString();

            return true;
        }

        private void CarregarDDLFases()
        {
            conCantoresFases objConCantoresFases = new conCantoresFases();
            objConCantoresFases.objCoCantoresFases.LimparAtributos();
            objConCantoresFases.objCoCantoresFases.cdConcurso = Convert.ToInt32(cdConcurso.SelectedValue.ToString());
            objConCantoresFases.objCoCantoresFases.cdCantor = Convert.ToInt32(Session["cdCantor"].ToString());

            if (!conCantoresFases.SelectFasesConcurso())
            {
                ltMensagem.Text = MostraMensagem("Falha!", "Não foi possível localizar as fases do cantor no concurso.", csMensagem.msgDanger);
                return;
            }

            csFases vcsFases = new csFases();
            vcsFases.bUtilizaDadosExternos = true;
            vcsFases.dtDadosExternos = objConCantoresFases.dtDados;
            cdFase = vcsFases.CarregaDDL(cdFase);
        }

        protected void cdConcurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            ltMensagem.Text = "";
        }

        protected void cdFase_SelectedIndexChanged(object sender, EventArgs e)
        {
            ltMensagem.Text = "";

            conCantoresFases objConCantoresFases = new conCantoresFases();
            objConCantoresFases.objCoCantoresFases.LimparAtributos();
            objConCantoresFases.objCoCantoresFases.cdConcurso = Convert.ToInt32(cdConcurso.SelectedValue.ToString());
            objConCantoresFases.objCoCantoresFases.cdCantor = Convert.ToInt32(Session["cdCantor"].ToString());
            objConCantoresFases.objCoCantoresFases.cdFase = Convert.ToInt32(cdFase.SelectedValue.ToString());

            if (!conCantoresFases.SelectCantoresCategoriasFasesConcurso())
            {
                ltMensagem.Text = MostraMensagem("Aviso!", "Não foi possível localizar categorias da fase.", csMensagem.msgDanger);
                return;
            }

            csCategorias vcsCategorias = new csCategorias();
            vcsCategorias.bUtilizaDadosExternos = true;
            vcsCategorias.dtDadosExternos = objConCantoresFases.dtDados;
            cdCategoria = vcsCategorias.CarregaDDL(cdCategoria);
        }
    }
}