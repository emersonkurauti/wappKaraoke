using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wappKaraoke.Classes;
using wappKaraoke.Classes.Controller;
using wappKaraoke.Classes.Model.Concursos;
using wappKaraoke.Classes.Mensagem;
using System.Data;
using wappKaraoke.Classes.Model.CantoresFases;
using wappKaraoke.Classes.Model.Cantores;
using wappKaraoke.Classes.Model.Musicas;
using wappKaraoke.Classes.Model.Associacoes;
using wappKaraoke.Classes.Model.TipoStatus;
using wappKaraoke.Classes.Model.CantoresConcursos;

namespace wappKaraoke.Movimentacoes
{
    public partial class InserirCantorConcursoCorrente : csPageDefault
    {
        private DataTable dtCantoresCategoria;

        public override void Page_Load(object sender, EventArgs e)
        {
            ltMensagemDefault = ltMensagem;

            if (!this.IsPostBack)
            {
                InicializaPagina();
            }

            base.Page_Load(sender, e);
        }

        protected override bool ConfigurarGridView()
        {
            try
            {
                gvCantores.HeaderRow.TableSection = TableRowSection.TableHeader;

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void InicializaPagina()
        {
            ltMensagem.Text = "";

            cdFase.DataSource = null;
            cdCategoria.DataSource = null;
            cdCantor.DataSource = null;
            cdMusica.DataSource = null;
            cdTpStatus.DataSource = null;
            cdAssociacao.DataSource = null;
            nuCantor.Text = "";

            PegarChaveConcurso();
            CarregarDDL();

            LimparGvCantores();
        }

        private void LimparCamposCarregados()
        {
            cdAssociacao.SelectedIndex = 0;
            cdMusica.SelectedIndex = 0;
            cdTpStatus.SelectedValue = wappKaraoke.Properties.Settings.Default.sCodStatusInicial;
            nuCantor.Text = "";
            HabilitarCampos();
        }

        private void LimparGvCantores()
        {
            Session["dtCantoresCategoria"] = null;
            gvCantores.DataSource = null;
            gvCantores.DataBind();
        }

        private void CarregarDDL()
        {
            csFases vcsFases = new csFases();
            cdFase = vcsFases.CarregaDDL(cdFase);

            if (Session["cdFaseCorrente"] != null)
                cdFase.SelectedValue = Session["cdFaseCorrente"].ToString();

            csCategorias vcsCategorias = new csCategorias();
            cdCategoria = vcsCategorias.CarregaDDL(cdCategoria);

            csCantores vcsCantores = new csCantores();
            cdCantor = vcsCantores.CarregaDDL(cdCantor);

            csMusicas vcsMusicas = new csMusicas();
            cdMusica = vcsMusicas.CarregaDDL(cdMusica);

            csAssociacoes vcsAssociacoes = new csAssociacoes();
            cdAssociacao = vcsAssociacoes.CarregaDDL(cdAssociacao);

            csStatus vcsStatus = new csStatus();
            cdTpStatus = vcsStatus.CarregaDDL(cdTpStatus);

            cdTpStatus.SelectedValue = wappKaraoke.Properties.Settings.Default.sCodStatusInicial;
        }

        private void PegarChaveConcurso()
        {
            conConcursos objConConcursos = new conConcursos();
            objConConcursos.objCoConcursos.LimparAtributos();
            objConConcursos.objCoConcursos.strFiltro = " WHERE flConcursoCorrente = 'S'";

            if (conConcursos.Select())
            {
                Session["cdConcurso"] = objConConcursos.dtDados.Rows[0][caConcursos.cdConcurso].ToString();
                Session["cdFaseCorrente"] = objConConcursos.dtDados.Rows[0][caConcursos.cdFaseCorrente].ToString();
            }

            if (Session["cdConcurso"] == null)
            {
                ltMensagem.Text = MostraMensagem("Falha!", "Não existe concurso corrente definido.", csMensagem.msgDanger);
                return;
            }

            if (Session["cdFaseCorrente"] == null || Session["cdFaseCorrente"].ToString() == "")
            {
                ltMensagem.Text = MostraMensagem("Falha!", "Não existe fase corrente definida.", csMensagem.msgDanger);
                return;
            }
        }

        private void CarregarCantoresFasesCategoria()
        {
            if (cdCategoria.SelectedIndex > 0)
            {
                LimparCamposCarregados();

                conCantoresFases objConCantoresFases = new conCantoresFases();
                objConCantoresFases.objCoCantoresFases.LimparAtributos();
                objConCantoresFases.objCoCantoresFases.cdConcurso = Convert.ToInt32(Session["cdConcurso"]);
                objConCantoresFases.objCoCantoresFases.cdFase = Convert.ToInt32(cdFase.SelectedValue);
                objConCantoresFases.objCoCantoresFases.cdCategoria = Convert.ToInt32(cdCategoria.SelectedValue);

                if (!conCantoresFases.SelectCantoresCategoriasConcurso())
                {
                    ltMensagem.Text = MostraMensagem("Falha!", "Não foi possível carregar os cantores desta categoria.", csMensagem.msgDanger);
                }

                if (objConCantoresFases.dtDados == null || objConCantoresFases.dtDados.Rows.Count == 0)
                {
                    ltMensagem.Text = MostraMensagem("Aviso!", "Não foram localizados cantores desta categoria.", csMensagem.msgWarning);
                }

                dtCantoresCategoria = objConCantoresFases.dtDados;
                OrdenaDataTable(ref dtCantoresCategoria, "nuOrdemApresentacao");
                Session["dtCantoresCategoria"] = dtCantoresCategoria;
                gvCantores.DataSource = dtCantoresCategoria;
                gvCantores.DataBind();

                NomeKanji();
                ConfigurarGridView();
            }
        }

        protected void NomeKanji()
        {
            if (Session["dtCantoresCategoria"] != null)
            {
                dtCantoresCategoria = (DataTable)Session["dtCantoresCategoria"];

                for (int i = 0; i < dtCantoresCategoria.Rows.Count; i++)
                {
                    ((Literal)gvCantores.Rows[i].FindControl("ltNomeKanji")).Text = @"" + dtCantoresCategoria.Rows[i]["nmCantor"].ToString() +
                        " <br/> " + dtCantoresCategoria.Rows[i]["nmNomeKanji"].ToString();

                    ((Literal)gvCantores.Rows[i].FindControl("ltMusicaKanji")).Text = @"" + dtCantoresCategoria.Rows[i]["nmMusica"].ToString() +
                        " <br/> " + dtCantoresCategoria.Rows[i]["nmMusicaKanji"].ToString();
                }
            }
        }

        protected void cdFase_SelectedIndexChanged(object sender, EventArgs e)
        {
            cdCategoria.SelectedIndex = 0;
            cdCantor.SelectedIndex = 0;
            LimparCamposCarregados();
            LimparGvCantores();
        }

        protected void cdCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            cdCantor.SelectedIndex = 0;
            CarregarCantoresFasesCategoria();
        }

        protected void cdCantor_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimparCamposCarregados();

            if (cdFase.SelectedIndex == 0)
            {
                ltMensagem.Text = MostraMensagem("Validação!", "Selecione a fase.", csMensagem.msgWarning);
                return;
            }

            if (cdCategoria.SelectedIndex == 0)
            {
                ltMensagem.Text = MostraMensagem("Validação!", "Selecione a categoria.", csMensagem.msgWarning);
                return;
            }

            if (!CantorEmEdicao())
            {
                conCantoresFases objConCantoresFases = new conCantoresFases();
                objConCantoresFases.objCoCantoresFases.LimparAtributos();
                objConCantoresFases.objCoCantoresFases.cdConcurso = Convert.ToInt32(Session["cdConcurso"]);
                objConCantoresFases.objCoCantoresFases.cdFase = Convert.ToInt32(cdFase.SelectedValue);
                objConCantoresFases.objCoCantoresFases.cdCategoria = Convert.ToInt32(cdCategoria.SelectedValue);
                objConCantoresFases.objCoCantoresFases.cdCantor = Convert.ToInt32(cdCantor.SelectedValue);

                if (!conCantoresFases.SelectFasesCategoriasCantoresConcurso())
                {
                    ltMensagem.Text = MostraMensagem("Falha!", "Não foi possível consultar cantor.", csMensagem.msgWarning);
                    return;
                }

                if (objConCantoresFases.dtDados.Rows.Count == 0)
                {
                    cdAssociacao.SelectedValue = BuscarAssociacaoCantor().ToString();
                    return;
                }

                cdMusica.SelectedValue = objConCantoresFases.dtDados.Rows[0][caCantoresFases.cdMusica].ToString();
                cdAssociacao.SelectedValue = objConCantoresFases.dtDados.Rows[0][caCantoresConcursos.cdAssociacao].ToString();
                cdTpStatus.SelectedValue = objConCantoresFases.dtDados.Rows[0][caCantoresFases.cdTpStatus].ToString();
                nuCantor.Text = objConCantoresFases.dtDados.Rows[0][caCantoresFases.nuCantor].ToString();
            }

            DesabilitarCampos();
        }

        protected void nuCantor_TextChanged(object sender, EventArgs e)
        {
            if (cdFase.SelectedIndex == 0)
            {
                ltMensagem.Text = MostraMensagem("Validação!", "Selecione a fase.", csMensagem.msgWarning);
                return;
            }

            if (nuCantor.Text.Trim() == "")
                return;

            conCantoresFases objConCantoresFases = new conCantoresFases();
            objConCantoresFases.objCoCantoresFases.cdConcurso = Convert.ToInt32(Session["cdConcurso"]);
            objConCantoresFases.objCoCantoresFases.nuCantor = nuCantor.Text;

            if (!conCantoresFases.SelectCantoresConcursoPorNumero())
            {
                ltMensagem.Text = MostraMensagem("Falha!", "Não foi possível consultar o cantor.", csMensagem.msgDanger);
                return;
            }

            if (objConCantoresFases.dtDados.Rows.Count > 0)
            {
                nuCantor.Text = "";
                ltMensagem.Text = MostraMensagem("Validação!", "Número já atribuído a outro cantor neste concurso.", csMensagem.msgDanger);
                return;
            }
        }

        private int BuscarAssociacaoCantor()
        {
            conCantoresConcursos objConCantoresConcursos = new conCantoresConcursos();
            objConCantoresConcursos.objCoCantoresConcursos.LimparAtributos();
            objConCantoresConcursos.objCoCantoresConcursos.cdConcurso = Convert.ToInt32(Session["cdConcurso"]);
            objConCantoresConcursos.objCoCantoresConcursos.cdCantor = Convert.ToInt32(cdCantor.SelectedValue);

            if (!conCantoresConcursos.Select())
            {
                ltMensagem.Text = MostraMensagem("Falha!", "Não foi possível consultar a associação do cantor.", csMensagem.msgWarning);
                return 0;
            }

            if (objConCantoresConcursos.dtDados.Rows.Count == 0)
                return 0;

            DesabilitarCdAssociacao();
            return Convert.ToInt32(objConCantoresConcursos.dtDados.Rows[0][caCantoresConcursos.cdAssociacao].ToString());
        }

        private void DesabilitarCdAssociacao()
        {
            cdAssociacao.CssClass += " disabled";
            cdAssociacao.Enabled = false;
        }

        private void DesabilitarCampos()
        {
            btnAdicionar.Enabled = false;
            btnAdicionar.CssClass += " disabled";

            DesabilitarCdAssociacao();

            cdTpStatus.CssClass += " disabled";
            cdTpStatus.Enabled = false;

            cdMusica.CssClass += " disabled";
            cdMusica.Enabled = false;

            nuCantor.ReadOnly = true;
        }

        private void HabilitarCampos()
        {
            btnAdicionar.Enabled = true;
            btnAdicionar.CssClass = btnAdicionar.CssClass.Replace(" disabled", "");
            cdAssociacao.Enabled = true;
            cdAssociacao.CssClass = cdAssociacao.CssClass.Replace(" disabled", "");
            cdTpStatus.Enabled = true;
            cdTpStatus.CssClass = cdAssociacao.CssClass.Replace(" disabled", "");
            cdMusica.Enabled = true;
            cdMusica.CssClass = cdAssociacao.CssClass.Replace(" disabled", "");

            nuCantor.ReadOnly = false;
        }

        private bool CantorEmEdicao()
        {
            dtCantoresCategoria = (DataTable)Session["dtCantoresCategoria"];

            //foreach (DataRow dr in dtCantoresCategoria.Rows)
            //{
            //    if(dr[caCantoresFases.
            //}

            return false;
        }

        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (cdFase.SelectedIndex == 0)
            {
                ltMensagem.Text = MostraMensagem("Validação!", "Selecione a fase.", csMensagem.msgWarning);
                return;
            }

            if (cdCategoria.SelectedIndex == 0)
            {
                ltMensagem.Text = MostraMensagem("Validação!", "Selecione a categoria.", csMensagem.msgWarning);
                return;
            }

            if (cdCantor.SelectedIndex == 0)
            {
                ltMensagem.Text = MostraMensagem("Validação!", "Selecione o cantor.", csMensagem.msgWarning);
                return;
            }

            if (cdMusica.SelectedIndex == 0)
            {
                ltMensagem.Text = MostraMensagem("Validação!", "Selecione a música.", csMensagem.msgWarning);
                return;
            }

            if (cdTpStatus.SelectedIndex == 0)
            {
                ltMensagem.Text = MostraMensagem("Validação!", "Selecione o status.", csMensagem.msgWarning);
                return;
            }

            if (nuCantor.Text.Trim() == "")
            {
                ltMensagem.Text = MostraMensagem("Validação!", "Informe o número do cantor.", csMensagem.msgWarning);
                return;
            }

            dtCantoresCategoria = Session["dtCantoresCategoria"] == null ? null : (DataTable)Session["dtCantoresCategoria"];

            int nuOrdemApresentacao = dtCantoresCategoria == null || dtCantoresCategoria.Rows.Count == 0 ? 1 : 
                Convert.ToInt32(dtCantoresCategoria.Rows[dtCantoresCategoria.Rows.Count-1][caCantoresFases.nuOrdemApresentacao]) + 1;
            
            conCantores objConCantores = new conCantores();
            conMusicas objConMusicas = new conMusicas();
            conAssociacoes objConAssociacoes = new conAssociacoes();
            try
            {
                DataRow dr = dtCantoresCategoria.NewRow();

                dr[caCantoresFases.cdConcurso] = Convert.ToInt32(Session["cdConcurso"]);
                dr[caCantoresFases.nuOrdemApresentacao] = nuOrdemApresentacao;
                dr[caCantoresFases.nuCantor] = nuCantor.Text;

                objConCantores.objCoCantores.LimparAtributos();
                objConCantores.objCoCantores.cdCantor = Convert.ToInt32(cdCantor.SelectedValue);
                conCantores.Select();
                dr[caCantoresFases.cdCantor] = Convert.ToInt32(cdCantor.SelectedValue);
                dr[caCantores.nmCantor] = objConCantores.dtDados.Rows[0][caCantores.nmCantor].ToString();
                dr[caCantores.nmNomeKanji] = objConCantores.dtDados.Rows[0][caCantores.nmNomeKanji].ToString();
                               
                objConMusicas.objCoMusicas.LimparAtributos();
                objConMusicas.objCoMusicas.cdMusica = Convert.ToInt32(cdMusica.SelectedValue);
                conMusicas.Select();
                dr[caCantoresFases.cdMusica] = Convert.ToInt32(cdMusica.SelectedValue);
                dr[caMusicas.nmMusica] = objConMusicas.dtDados.Rows[0][caMusicas.nmMusica].ToString();
                dr[caMusicas.nmMusicaKanji] = objConMusicas.dtDados.Rows[0][caMusicas.nmMusicaKanji].ToString();
                
                objConAssociacoes.objCoAssociacoes.LimparAtributos();
                objConAssociacoes.objCoAssociacoes.cdAssociacao = Convert.ToInt32(cdAssociacao.SelectedValue);
                conAssociacoes.Select();
                dr[caAssociacoes.cdAssociacao] = Convert.ToInt32(cdAssociacao.SelectedValue);
                dr[caAssociacoes.nmAssociacao] = objConAssociacoes.dtDados.Rows[0][caAssociacoes.nmAssociacao].ToString();
                dr[caCantoresFases.cdFase] = Convert.ToInt32(cdFase.SelectedValue);
                dr[caCantoresFases.cdTpStatus] = Convert.ToInt32(cdTpStatus.SelectedValue);
                dr[caCantoresFases.cdCategoria] = Convert.ToInt32(cdCategoria.SelectedValue);

                dtCantoresCategoria.Rows.Add(dr);

                Session["dtCantoresCategoria"] = dtCantoresCategoria;
                gvCantores.DataSource = dtCantoresCategoria;
                gvCantores.DataBind();
                ConfigurarGridView();
                NomeKanji();

                cdCantor.SelectedIndex = 0;
                LimparCamposCarregados();
            }
            catch
            {
                ltMensagem.Text = MostraMensagem("Falha!", "Não foi possível carregar os dados dos registros selecionados.", csMensagem.msgDanger);
                return;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            InicializaPagina();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {

        }
    }
}