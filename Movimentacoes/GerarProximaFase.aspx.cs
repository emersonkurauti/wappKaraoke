using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wappKaraoke.Classes;
using System.Data;
using wappKaraoke.Classes.Model.ConcursosOrdemCategorias;
using wappKaraoke.Classes.Controller;
using wappKaraoke.Classes.Model.Concursos;
using wappKaraoke.Classes.Model.CantoresFases;
using wappKaraoke.Classes.Mensagem;
using wappKaraoke.Classes.Model.Cantores;
using wappKaraoke.Classes.Model.Associacoes;
using System.Collections;
using wappKaraoke.Classes.Model.Musicas;
using wappKaraoke.Classes.Model.CantoresConcursos;

namespace wappKaraoke.Movimentacoes
{
    public partial class GerarProximaFase : csPageCadastro
    {
        private DataTable _dtAssociacoes;
        private DataTable _dtCantoresConcurso;
        private DataTable _dtCantoresFases;
        private DataTable _dtOrdemCategoria;

        private string ScriptGridView = @"$(function () { $('[id*=[idGridView]').footable(); });";

        private string strInicio = "<div class=\"tabbable tabs-left\"> \n <ul class=\"nav nav-tabs\">\n";
        private string strMeio = "</ul> \n <div class=\"tab-content\">\n";
        private string strFim = "</div> \n </div>\n";

        private string strListaMenu = "<li class=\"active\"><a id=\"lnkDivCantores_[idLista]\" href=\"#div[idLista]\" data-toggle=\"tab\">[Nome]</a></li>\n";
        private string strAbrePanel = "<div class=\"tab-pane active\" id=\"div[idPanel]\">\n";
        private string strFechaPanel = "</div>\n";
        private string strUpdatePanelIni = "<asp:UpdatePanel ID=\"up[UpdatePanel]\" runat=\"server\" UpdateMode=\"Conditional\"> \n <ContentTemplate>\n";
        private string strUpdatePanelFim = "</ContentTemplate> \n </asp:UpdatePanel>\n";
        private string strRowIni = "<div class=\"row\">\n";
        private string strRowFim = "</div>\n";
        private string strPanelBodyIni = "<div class=\"panel-body\">";
        private string strPanelBodyFim = "</div>";

        public override void Page_Load(object sender, EventArgs e)
        {
            if (Request["__EVENTARGUMENT"] != null)
            {
                if (Request["__EVENTTARGET"].Contains("lnkEditCantor"))
                {
                    EditarCantor();
                }
                return;
            }

            ltMensagemDefault = ltMensagem;

            if (!this.IsPostBack)
            {
                LimpaSessions();
                ltMensagem.Text = "";

                PegarChaveConcurso();

                Session["cdFaseAlteracaoGP"] = 0;

                CarregarCantoresCategorias();

                CarregarDDL();
            }

            base.Page_Load(sender, e);
        }

        private void CarregarDDL()
        {
            conFases objConFases = new conFases();
            objConFases.objCoFases.LimparAtributos();
            objConFases.objCoFases.strFiltro = " WHERE cdFase <> " + Session["cdFaseCorrenteGerarProxFase"];

            conFases.Select();

            csFases vcsFases = new csFases();
            vcsFases.bUtilizaDadosExternos = true;
            vcsFases.dtDadosExternos = objConFases.dtDados;
            cdFase = vcsFases.CarregaDDL(cdFase);
        }

        private void PegarChaveConcurso()
        {
            conConcursos objConConcursos = new conConcursos();
            objConConcursos.objCoConcursos.LimparAtributos();
            objConConcursos.objCoConcursos.strFiltro = " WHERE flConcursoCorrente = 'S'";

            if (conConcursos.Select())
            {
                Session["cdConcursoGerarProxFase"] = objConConcursos.dtDados.Rows[0][caConcursos.cdConcurso].ToString();
                Session["cdFaseCorrenteGerarProxFase"] = objConConcursos.dtDados.Rows[0][caConcursos.cdFaseCorrente].ToString();
            }

            if (Session["cdConcursoGerarProxFase"] == null)
            {
                ltMensagem.Text = MostraMensagem("Falha!", "Não existe concurso corrente definido.", csMensagem.msgDanger);
                return;
            }

            if (Session["cdFaseCorrenteGerarProxFase"] == null || Session["cdFaseCorrenteGerarProxFase"].ToString() == "")
            {
                ltMensagem.Text = MostraMensagem("Falha!", "Não existe fase corrente definida.", csMensagem.msgDanger);
                return;
            }
        }

        private void LimpaSessionCantoresCategorias()
        {
            if (Session["_dtOrdemCategoriaGP"] != null)
            {
                _dtOrdemCategoria = (DataTable)Session["_dtOrdemCategoriaGP"];

                foreach (DataRow dr in _dtOrdemCategoria.Rows)
                {
                    Session["dvCantores_" + dr[caConcursosOrdemCategorias.cdCategoria].ToString() + "GP"] = null;
                }
            }
        }

        private void LimpaSessions()
        {
            LimpaSessionCantoresCategorias();

            Session["strListaGP"] = null;
            Session["strDivsGP"] = null;
            Session["_dtAssociacoesGP"] = null;
            Session["_dtCantoresConcursoGP"] = null;
            Session["_dtCantoresFasesGP"] = null;
            Session["_dtOrdemCategoriaGP"] = null;
        }

        private void CarregarAssociacoes()
        {
            if (Session["_dtAssociacoesGP"] != null)
                _dtAssociacoes = (DataTable)Session["_dtAssociacoesGP"];

            conConcursosAssociacoes objConConcursosAssociacoes = new conConcursosAssociacoes();
            objConConcursosAssociacoes.objCoConcursosAssociacoes.LimparAtributos();
            objConConcursosAssociacoes.objCoConcursosAssociacoes.cdConcurso = Convert.ToInt32(Session["cdConcursoGerarProxFase"].ToString());

            if (!conConcursosAssociacoes.Select())
            {
                ltMensagem.Text = MostraMensagem("Falha", "Problemas ao carregar Associações.", csMensagem.msgDanger);
                return;
            }

            _dtAssociacoes = objConConcursosAssociacoes.dtDados;
            Session["_dtAssociacoesGP"] = _dtAssociacoes;
        }

        protected void cdFase_SelectedIndexChanged(object sender, EventArgs e)
        {
            ltMensagem.Text = "";
            Session["cdFaseAlteracaoGP"] = cdFase.SelectedValue;
            CarregarCantoresCategorias();
        }

        protected void btnlnkSortearOrdemApresentacao_OnClick(object sender, EventArgs e)
        {
            ltMensagem.Text = "";
            if (Session["_dtOrdemCategoriaGP"] != null)
            {
                int nuSeq;
                int qtdCantoresFasesCategoria;
                ArrayList alNumSorteados = new ArrayList();
                _dtCantoresFases = (DataTable)Session["_dtCantoresFasesGP"];
                _dtOrdemCategoria = (DataTable)Session["_dtOrdemCategoriaGP"];

                foreach (DataRow drCategoria in _dtOrdemCategoria.Rows)
                {
                    alNumSorteados.Clear();
                    qtdCantoresFasesCategoria =
                        ContarCantoresFaseCategoria(_dtCantoresFases, Convert.ToInt32(drCategoria[caCantoresFases.cdCategoria]));

                    if (Session["dvCantores_" + drCategoria[caConcursosOrdemCategorias.cdCategoria].ToString() + "GP"] != null)
                    {
                        DataTable dt = (DataTable)Session["dvCantores_" + drCategoria[caConcursosOrdemCategorias.cdCategoria].ToString() + "GP"];
                        dt.Columns[caCantoresFases.nuOrdemApresentacao].ReadOnly = false;
                        dt.Columns[caCantoresFases.nuCantor].ReadOnly = false;

                        foreach (DataRow drCantor in _dtCantoresFases.Rows)
                        {
                            if (drCategoria[caConcursosOrdemCategorias.cdCategoria].ToString() == drCantor[caCantoresFases.cdCategoria].ToString())
                            {
                                Random rndNuOrdemApres = new Random(DateTime.Now.Millisecond);

                                do
                                {
                                    nuSeq = Convert.ToInt32(rndNuOrdemApres.Next(1, qtdCantoresFasesCategoria + 1).ToString());
                                } while (!ValidarNumeroValido(alNumSorteados, nuSeq));

                                foreach (DataRow drCantCat in dt.Rows)
                                {
                                    if (drCantor[caCantoresFases.cdCantor].ToString() == drCantCat[caCantoresFases.cdCantor].ToString())
                                    {
                                        alNumSorteados.Add(nuSeq);
                                        drCantCat[caCantoresFases.nuOrdemApresentacao] = nuSeq;
                                        break;
                                    }
                                }

                                drCantor[caCantoresFases.CC_Controle] = KuraFrameWork.csConstantes.sAlterando;
                                drCantor[caCantoresFases.nuOrdemApresentacao] = nuSeq;
                            }
                        }

                        Session["dvCantores_" + drCategoria[caConcursosOrdemCategorias.cdCategoria].ToString() + "GP"] = dt;
                    }
                }

                OrdenaDataTable(ref _dtCantoresFases, caCantoresFases.nuOrdemApresentacao.ToString());
                Session["_dtCantoresFasesGP"] = _dtCantoresFases;
                MontaCantoresCategorias(false, false);
            }
        }

        protected void btnSalvar_OnClick(object sender, EventArgs e)
        {
            ltMensagem.Text = "";
            if (Session["_dtCantoresFasesGP"] != null)
            {
                conConcursos objConConcursos = new conConcursos();
                objConConcursos.objCoConcursos.LimparAtributos();
                objConConcursos.objCoConcursos.dtConcursoFases = (DataTable)Session["_dtCantoresFasesGP"];

                if (!conConcursos.AtualizarProximaFase())
                {
                    ltMensagem.Text = MostraMensagem("Falha!", "Problemas ao salvar alterações na próxima fase.", csMensagem.msgDanger);
                    return;
                }

                ltMensagem.Text = MostraMensagem("Sucesso!", "Alterações realizadas com sucesso.", csMensagem.msgSucess);
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "AtivaAbaCantores('" + Session["cdCategoriaEditGP"] + "');", true);
            }
        }

        protected void btnConfirmarEdicaoCantor_Click(object sender, EventArgs e)
        {
            ltMensagem.Text = "";
            if (cdAssociacaoEdit.SelectedIndex > 0 && cdMusicaEdit.SelectedIndex > 0)
            {
                int indexCantorFase = Convert.ToInt32(Session["indexCantorFaseGP"].ToString());
                int indexCantorConcurso = Convert.ToInt32(Session["indexCantorConcursoGP"].ToString());

                if ((nuOrdemApresentacaoEdit.Text.Trim() == "") || (Convert.ToInt32(nuOrdemApresentacaoEdit.Text) == 0))
                {
                    ltMensagemEdicaoCantor.Text = MostraMensagem("Validação!", "Selecione uma Associação e uma Música.", csMensagem.msgWarning);
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "AtivaEdicaoCantor('" + Session["cdCategoriaEditGP"].ToString() + "');", true);
                    return;
                }

                _dtCantoresFases = (DataTable)Session["_dtCantoresFasesGP"];
                _dtCantoresFases.Rows[indexCantorFase][caCantoresFases.CC_Controle] = KuraFrameWork.csConstantes.sAlterando;
                _dtCantoresFases.Rows[indexCantorFase][caCantoresFases.cdMusica] = cdMusicaEdit.SelectedValue;

                AtualizarNuOrdemApresentacao(ref _dtCantoresFases, Convert.ToInt32(nuOrdemApresentacaoEdit.Text));

                DataTable dt;
                CarregaDataTableOrdemCategoriasSession();

                foreach (DataRow drCategoria in _dtOrdemCategoria.Rows)
                {
                    dt = (DataTable)Session["dvCantores_" + drCategoria[caConcursosOrdemCategorias.cdCategoria].ToString() + "GP"];
                    dt.Columns[caAssociacoes.cdAssociacao].ReadOnly = false;
                    dt.Columns[caAssociacoes.nmAssociacao].ReadOnly = false;
                    dt.Columns[caMusicas.cdMusica].ReadOnly = false;
                    dt.Columns[caMusicas.nmMusica].ReadOnly = false;
                    dt.Columns[caMusicas.nmMusicaKanji].ReadOnly = false;

                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr[caCantores.cdCantor].ToString() == Session["cdCantorEditGP"].ToString())
                        {
                            dr[caAssociacoes.cdAssociacao] = cdAssociacaoEdit.SelectedValue;
                            dr[caAssociacoes.nmAssociacao] = cdAssociacaoEdit.SelectedItem;


                            conMusicas objConMusicas = new conMusicas();
                            objConMusicas.objCoMusicas.LimparAtributos();
                            objConMusicas.objCoMusicas.cdMusica = Convert.ToInt32(cdMusicaEdit.SelectedValue);
                            conMusicas.Select();

                            dr[caMusicas.cdMusica] = cdMusicaEdit.SelectedValue;
                            dr[caMusicas.nmMusica] = cdMusicaEdit.SelectedItem;
                            dr[caMusicas.nmMusicaKanji] = objConMusicas.dtDados.Rows[0][caMusicas.nmMusicaKanji].ToString();
                        }
                    }

                    Session["dvCantores_" + drCategoria[caConcursosOrdemCategorias.cdCategoria].ToString() + "GP"] = dt;
                }

                Session["_dtCantoresFasesGP"] = _dtCantoresFases;

                PreencheLiteral(Session["cdCategoriaEditGP"].ToString().ToString(), false, false);

                AtualizaCantoresCategorias();
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "AtivaAbaCantores('" + Session["cdCategoriaEditGP"] + "');", true);
            }
            else
            {
                ltMensagemEdicaoCantor.Text = MostraMensagem("Validação!", "Selecione uma Associação e uma Música.", csMensagem.msgWarning);
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "AtivaEdicaoCantor('" + Session["cdCategoriaEditGP"].ToString() + "');", true);
            }
        }

        ///
        ///Cantores
        ///

        private void AtualizaCantoresCategorias()
        {
            ltCategorias.Text = "";

            CarregaDataTableOrdemCategoriasSession();


            foreach (DataRow dr in _dtOrdemCategoria.Rows)
            {
                AdicionaCantorCategoriaConcurso(dr[caConcursosOrdemCategorias.cdCategoria].ToString(),
                    dr[caConcursosOrdemCategorias.CC_deCategoria].ToString(), false, false);
            }
        }

        private void AdicionaCantorCategoriaConcurso(string strCdCategoria, string strDeCategoria, bool bEstaCarregando = false, bool bEstaInserindo = true)
        {
            CarregaDataTableOrdemCategoriasSession();

            if (bEstaCarregando || bEstaInserindo)
            {
                InsereCategoria(strCdCategoria, strDeCategoria);
            }

            PreencheLiteral(strCdCategoria, bEstaCarregando, bEstaInserindo);
        }

        private bool AtualizarNuOrdemApresentacao(ref DataTable pdtCantores, int pnuOrdemAtual)
        {
            try
            {
                int nuOrdemAnterior = Convert.ToInt32(Session["nuOrdemApresentacaoEditGP"]);

                if (Session["dvCantores_" + Session["cdCategoriaEditGP"].ToString() + "GP"] != null)
                    dtDados = (DataTable)Session["dvCantores_" + Session["cdCategoriaEditGP"].ToString() + "GP"];

                dtDados.Columns[caCantoresFases.nuOrdemApresentacao].ReadOnly = false;
                DataRow drAtual = dtDados.NewRow();
                DataRow drCantorAtual = pdtCantores.NewRow();

                foreach (DataRow dr in pdtCantores.Rows)
                {
                    if ((Convert.ToInt32(dr[caCantoresFases.nuOrdemApresentacao].ToString()) == nuOrdemAnterior) &&
                        (Convert.ToInt32(Session["cdCategoriaEditGP"])) == (Convert.ToInt32(dr[caCantoresFases.cdCategoria].ToString())))
                    {
                        drCantorAtual = dr;
                        break;
                    }
                }

                foreach (DataRow dr in dtDados.Rows)
                {
                    if (Convert.ToInt32(dr[caCantoresFases.nuOrdemApresentacao].ToString()) == nuOrdemAnterior)
                    {
                        drAtual = dr;
                        break;
                    }
                }

                if (pnuOrdemAtual > nuOrdemAnterior)
                {

                    foreach (DataRow dr in pdtCantores.Rows)
                    {
                        if ((Convert.ToInt32(dr[caCantoresFases.nuOrdemApresentacao].ToString()) <= pnuOrdemAtual) &&
                            (Convert.ToInt32(dr[caCantoresFases.nuOrdemApresentacao].ToString()) > nuOrdemAnterior) &&
                            (Convert.ToInt32(Session["cdCategoriaEditGP"])) == (Convert.ToInt32(dr[caCantoresFases.cdCategoria].ToString())))
                        {
                            dr[caCantoresFases.CC_Controle] = KuraFrameWork.csConstantes.sAlterando;
                            dr[caCantoresFases.nuOrdemApresentacao] = Convert.ToInt32(dr[caCantoresFases.nuOrdemApresentacao].ToString()) - 1;
                        }
                    }

                    foreach (DataRow dr in dtDados.Rows)
                    {
                        if ((Convert.ToInt32(dr[caCantoresFases.nuOrdemApresentacao].ToString()) <= pnuOrdemAtual) &&
                            (Convert.ToInt32(dr[caCantoresFases.nuOrdemApresentacao].ToString()) > nuOrdemAnterior))
                            dr[caCantoresFases.nuOrdemApresentacao] = Convert.ToInt32(dr[caCantoresFases.nuOrdemApresentacao].ToString()) - 1;

                    }
                }
                else if (pnuOrdemAtual < nuOrdemAnterior)
                {
                    foreach (DataRow dr in pdtCantores.Rows)
                    {
                        if ((Convert.ToInt32(dr[caCantoresFases.nuOrdemApresentacao].ToString()) >= pnuOrdemAtual) &&
                            (Convert.ToInt32(dr[caCantoresFases.nuOrdemApresentacao].ToString()) < nuOrdemAnterior) &&
                            (Convert.ToInt32(Session["cdCategoriaEditGP"])) == (Convert.ToInt32(dr[caCantoresFases.cdCategoria].ToString())))
                        {
                            dr[caCantoresFases.CC_Controle] = KuraFrameWork.csConstantes.sAlterando;
                            dr[caCantoresFases.nuOrdemApresentacao] = Convert.ToInt32(dr[caCantoresFases.nuOrdemApresentacao].ToString()) + 1;
                        }
                    }

                    foreach (DataRow dr in dtDados.Rows)
                    {
                        if ((Convert.ToInt32(dr[caCantoresFases.nuOrdemApresentacao].ToString()) >= pnuOrdemAtual) &&
                            (Convert.ToInt32(dr[caCantoresFases.nuOrdemApresentacao].ToString()) < nuOrdemAnterior))
                            dr[caCantoresFases.nuOrdemApresentacao] = Convert.ToInt32(dr[caCantoresFases.nuOrdemApresentacao].ToString()) + 1;
                    }
                }

                drAtual[caCantoresFases.nuOrdemApresentacao] = pnuOrdemAtual;
                //drCantorAtual[caCantoresFases.CC_Controle] = wappKaraoke.Classes.
                drCantorAtual[caCantoresFases.nuOrdemApresentacao] = pnuOrdemAtual;
                Session["dvCantores_" + Session["cdCategoriaEditGP"].ToString() + "GP"] = dtDados;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool ValidarNumeroValido(ArrayList palNumerosSorteados, int pNumero)
        {
            foreach (int strNum in palNumerosSorteados)
            {
                if (pNumero == strNum)
                    return false;
            }
            return true;
        }

        private int ContarCantoresFaseCategoria(DataTable pDtCantores, int pcdCategoria)
        {
            int Contador = 0;

            foreach (DataRow dr in pDtCantores.Rows)
            {
                if (Convert.ToInt32(dr[caCantoresFases.cdCategoria].ToString()) == pcdCategoria)
                    Contador++;
            }

            return Contador;
        }

        private void EditarCantor()
        {
            string[] strParam = Request["__EVENTARGUMENT"].Split(';');

            _dtCantoresConcurso = (DataTable)Session["_dtCantoresConcursoGP"];
            _dtCantoresFases = (DataTable)Session["_dtCantoresFasesGP"];
            int cdCantorEdit = Convert.ToInt32(strParam[0]);
            int cdCategoriaEdit = Convert.ToInt32(strParam[1]);

            Session["cdCantorEditGP"] = cdCantorEdit;
            Session["cdCategoriaEditGP"] = cdCategoriaEdit;

            csAssociacoes vcsAssociacoesCancores = new csAssociacoes();
            vcsAssociacoesCancores.bUtilizaDadosExternos = true;
            vcsAssociacoesCancores.dtDadosExternos = (DataTable)Session["_dtAssociacoesGP"] == null ?
                conAssociacoes.objCo.RetornaEstruturaDT() : (DataTable)Session["_dtAssociacoesGP"];
            cdAssociacaoEdit = vcsAssociacoesCancores.CarregaDDL(cdAssociacaoEdit);

            csMusicas vcsMusicas = new csMusicas();
            cdMusicaEdit = vcsMusicas.CarregaDDL(cdMusicaEdit);

            for (int i = 0; i < _dtCantoresFases.Rows.Count; i++)
            {
                if ((Convert.ToInt32(_dtCantoresFases.Rows[i][caCantoresFases.cdCantor].ToString()) == cdCantorEdit) &&
                    (Convert.ToInt32(_dtCantoresFases.Rows[i][caCantoresFases.cdCategoria].ToString()) == cdCategoriaEdit))
                {
                    Session["indexCantorFaseGP"] = i;

                    ltTituloEdicaoCantor.Text = _dtCantoresFases.Rows[i][caCantoresFases.CC_deCategoria].ToString() + "<br/><br/>"
                        + _dtCantoresFases.Rows[i][caCantoresFases.CC_nmCantor].ToString() + "<br/>"
                        + _dtCantoresFases.Rows[i][caCantoresFases.CC_nmNomeKanji].ToString();

                    cdMusicaEdit.SelectedValue = _dtCantoresFases.Rows[i][caCantoresFases.cdMusica].ToString();
                    nuCantorEdit.Text = _dtCantoresFases.Rows[i][caCantoresFases.nuCantor].ToString();
                    nuOrdemApresentacaoEdit.Text = _dtCantoresFases.Rows[i][caCantoresFases.nuOrdemApresentacao].ToString();
                    Session["nuOrdemApresentacaoEditGP"] = nuOrdemApresentacaoEdit.Text;
                }

                for (int k = 0; k < _dtCantoresConcurso.Rows.Count; k++)
                {
                    if (Convert.ToInt32(_dtCantoresConcurso.Rows[k][caCantoresConcursos.cdCantor].ToString()) == cdCantorEdit)
                    {
                        Session["cdAssociacaoEditGP"] = Convert.ToInt32(_dtCantoresConcurso.Rows[k][caCantoresConcursos.cdAssociacao].ToString());
                        Session["indexCantorConcursoGP"] = k;

                        cdAssociacaoEdit.SelectedValue = _dtCantoresConcurso.Rows[k][caCantoresConcursos.cdAssociacao].ToString();
                    }
                }
            }

            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "AtivaEdicaoCantor('" + cdCategoriaEdit + "');", true);
        }

        private void CarregarCantoresCategorias()
        {
            CarregarAssociacoes();

            conCantoresConcursos objConCantoresConcursos = new conCantoresConcursos();
            objConCantoresConcursos.objCoCantoresConcursos.LimparAtributos();
            objConCantoresConcursos.objCoCantoresConcursos.cdConcurso = Convert.ToInt32(Session["cdConcursoGerarProxFaseGP"].ToString());

            if (!conCantoresConcursos.Select())
            {
                ltMensagem.Text = MostraMensagem("Falha!", "Falha ao carregar os cantores do concurso.", csMensagem.msgDanger);
                return;
            }

            _dtCantoresConcurso = objConCantoresConcursos.dtDados;
            Session["_dtCantoresConcursoGP"] = _dtCantoresConcurso;

            conCantoresFases objConCantoresFases = new conCantoresFases();
            objConCantoresFases.objCoCantoresFases.LimparAtributos();
            objConCantoresFases.objCoCantoresFases.cdConcurso = Convert.ToInt32(Session["cdConcursoGerarProxFase"].ToString());
            objConCantoresFases.objCoCantoresFases.cdFase = Convert.ToInt32(Session["cdFaseAlteracaoGP"]);

            if (!conCantoresFases.Select())
            {
                ltMensagem.Text = MostraMensagem("Falha!", "Falha ao carregar as categorias do concurso.", csMensagem.msgDanger);
                return;
            }

            _dtCantoresFases = objConCantoresFases.dtDados;
            Session["_dtCantoresFasesGP"] = _dtCantoresFases;

            CarregarCategorias();
        }

        private void CarregarCategorias()
        {
            conConcursosOrdemCategorias objConOrdemCategorias = new conConcursosOrdemCategorias();
            objConOrdemCategorias.objCoConcursosOrdemCategorias.LimparAtributos();
            objConOrdemCategorias.objCoConcursosOrdemCategorias.cdConcurso = Convert.ToInt32(Session["cdConcursoGerarProxFase"].ToString());

            if (!conConcursosOrdemCategorias.Select())
            {
                ltMensagem.Text = MostraMensagem("Falha!", "Falha ao carregar as categorias do concurso.", csMensagem.msgDanger);
                return;
            }

            _dtOrdemCategoria = objConOrdemCategorias.dtDados;
            OrdenaDataTable(ref _dtOrdemCategoria, caConcursosOrdemCategorias.nuOrdem + KuraFrameWork.csConstantes.sCrescente);
            Session["_dtOrdemCategoriaGP"] = _dtOrdemCategoria;

            MontaCantoresCategorias(true, false);
        }

        private void MontaCantoresCategorias(bool bEstacarregando, bool bEstaInserindo)
        {
            foreach (DataRow dr in _dtOrdemCategoria.Rows)
            {
                InsereCategoria(dr[caConcursosOrdemCategorias.cdCategoria].ToString(), dr[caConcursosOrdemCategorias.CC_deCategoria].ToString());
            }

            if (_dtOrdemCategoria.Rows.Count > 0)
            {
                PreencheLiteral(_dtOrdemCategoria.Rows[0][caConcursosOrdemCategorias.cdCategoria].ToString(), bEstacarregando, bEstaInserindo);
            }
        }

        private void InsereCategoria(string strCdCategoria, string strDeCategoria)
        {
            bool bAchou = false;
            int nuOrdemCategoria;

            CarregaDataTableOrdemCategoriasSession();

            foreach (DataRow dr in _dtOrdemCategoria.Rows)
            {
                if (dr[caConcursosOrdemCategorias.cdCategoria].ToString() == strCdCategoria)
                    bAchou = true;
            }

            if (!bAchou)
            {
                nuOrdemCategoria = 1;

                if (_dtOrdemCategoria.Rows.Count > 0)
                    nuOrdemCategoria =
                        Convert.ToInt32(_dtOrdemCategoria.Rows[_dtOrdemCategoria.Rows.Count - 1][caConcursosOrdemCategorias.nuOrdem].ToString()) + 1;

                DataRow dr = _dtOrdemCategoria.NewRow();
                dr[caConcursosOrdemCategorias.CC_Controle] = KuraFrameWork.csConstantes.sInserindo;
                dr[caConcursosOrdemCategorias.cdConcurso] = Convert.ToInt32(Session["cdConcursoGerarProxFase"].ToString());
                dr[caConcursosOrdemCategorias.cdCategoria] = strCdCategoria;
                dr[caConcursosOrdemCategorias.CC_deCategoria] = strDeCategoria;
                dr[caConcursosOrdemCategorias.nuOrdem] = nuOrdemCategoria;

                _dtOrdemCategoria.Rows.Add(dr);
                OrdenaDataTable(ref _dtOrdemCategoria, caConcursosOrdemCategorias.nuOrdem + KuraFrameWork.csConstantes.sCrescente);
                Session["_dtOrdemCategoriaGP"] = _dtOrdemCategoria;
            }
        }

        private void CarregaDataTableOrdemCategoriasSession()
        {
            conConcursosOrdemCategorias objConOrdemCategoria = new conConcursosOrdemCategorias();
            _dtOrdemCategoria = objConOrdemCategoria.objCoConcursosOrdemCategorias.RetornaEstruturaDT();

            if (Session["_dtOrdemCategoriaGP"] != null)
                _dtOrdemCategoria = (DataTable)Session["_dtOrdemCategoriaGP"];
        }

        private void PreencheLiteral(string strCdCategoria, bool bEstaCarregando = false, bool bEstaInserindo = true)
        {
            CarregaDataTableOrdemCategoriasSession();

            csMontaTable ocsMontaTable = new csMontaTable();
            DataTable dtDados;
            string strLista = "";
            string strDivs = "";
            string strScriptGridView = "";

            for (int i = 0; i < _dtOrdemCategoria.Rows.Count; i++)
            {
                if (_dtOrdemCategoria.Rows[i][caConcursosOrdemCategorias.cdCategoria].ToString() != strCdCategoria)
                    strLista += strListaMenu.Replace("[Nome]",
                        _dtOrdemCategoria.Rows[i][caConcursosOrdemCategorias.CC_deCategoria].ToString()).Replace("[idLista]",
                        _dtOrdemCategoria.Rows[i][caConcursosOrdemCategorias.cdCategoria].ToString()).Replace("<li class=\"active\">", "<li>");
                else
                    strLista += strListaMenu.Replace("[Nome]",
                        _dtOrdemCategoria.Rows[i][caConcursosOrdemCategorias.CC_deCategoria].ToString()).Replace("[idLista]",
                        _dtOrdemCategoria.Rows[i][caConcursosOrdemCategorias.cdCategoria].ToString());

                if (_dtOrdemCategoria.Rows[i][caConcursosOrdemCategorias.cdCategoria].ToString() != strCdCategoria)
                    strDivs += strAbrePanel.Replace("[idPanel]",
                        _dtOrdemCategoria.Rows[i][caConcursosOrdemCategorias.cdCategoria].ToString()).Replace("class=\"tab-pane active\"", "class=\"tab-pane\"");
                else
                    strDivs += strAbrePanel.Replace("[idPanel]", _dtOrdemCategoria.Rows[i][caConcursosOrdemCategorias.cdCategoria].ToString());
                {
                    strDivs += strUpdatePanelIni.Replace("[UpdatePanel]", _dtOrdemCategoria.Rows[i][caConcursosOrdemCategorias.cdCategoria].ToString());
                    {
                        strDivs += strPanelBodyIni;
                        {
                            strDivs += strRowIni;
                            {
                                if (Session["dvCantores_" + _dtOrdemCategoria.Rows[i][caConcursosOrdemCategorias.cdCategoria].ToString() + "GP"] != null)
                                    dtDados = (DataTable)Session["dvCantores_" +
                                        _dtOrdemCategoria.Rows[i][caConcursosOrdemCategorias.cdCategoria].ToString() + "GP"];
                                else
                                {
                                    dtDados = ocsMontaTable.RetornaDTCantores();
                                    strScriptGridView += ScriptGridView.Replace("[idGridView]", "dvCantores_" +
                                        _dtOrdemCategoria.Rows[i][caConcursosOrdemCategorias.cdCategoria].ToString());
                                }

                                if (bEstaCarregando)
                                {
                                    conCantoresFases objConCantoresFases = new conCantoresFases();
                                    objConCantoresFases.objCoCantoresFases.LimparAtributos();
                                    objConCantoresFases.objCoCantoresFases.cdConcurso = Convert.ToInt32(Session["cdConcursoGerarProxFase"].ToString());
                                    objConCantoresFases.objCoCantoresFases.cdFase =
                                        Convert.ToInt32(Session["cdFaseAlteracaoGP"]);
                                    objConCantoresFases.objCoCantoresFases.cdCategoria =
                                        Convert.ToInt32(_dtOrdemCategoria.Rows[i][caConcursosOrdemCategorias.cdCategoria].ToString());

                                    if (!conCantoresFases.SelectCantoresCategoriasConcurso())
                                    {
                                        ltMensagem.Text = MostraMensagem("Falha!", "Falha ao carregar os cantores do concurso.", csMensagem.msgDanger);
                                        return;
                                    }

                                    if (objConCantoresFases.dtDados != null)
                                        dtDados = objConCantoresFases.dtDados;
                                }

                                Session["dvCantores_" + _dtOrdemCategoria.Rows[i][caConcursosOrdemCategorias.cdCategoria].ToString() + "GP"] = dtDados;

                                OrdenaDataTable(ref dtDados, "nuOrdemApresentacao");

                                ocsMontaTable.bExibirCodigos = false;
                                ocsMontaTable.bExibirbtnExcluir = false;
                                ocsMontaTable.dtDados = dtDados;
                                strDivs += ocsMontaTable.MontaDataGridView(_dtOrdemCategoria.Rows[i][caConcursosOrdemCategorias.cdCategoria].ToString());
                            }
                            strDivs += strRowFim;
                        }
                        strDivs += strPanelBodyFim;

                    }
                    strDivs += strUpdatePanelFim;
                }
                strDivs += strFechaPanel;
            }

            Session["strScriptGridViewGP"] = strScriptGridView;
            ltCategorias.Text = strInicio + strLista + strMeio + strDivs + strFim;
        }
    }
}