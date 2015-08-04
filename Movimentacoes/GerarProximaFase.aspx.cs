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

                Session["cdFaseAlteracao"] = 0;

                CarregarCantoresCategorias();

                CarregarDDL();
            }

            base.Page_Load(sender, e);
        }

        private void CarregarDDL()
        {
            conFases objConFases = new conFases();
            objConFases.objCoFases.LimparAtributos();
            objConFases.objCoFases.strFiltro = " WHERE cdFase <> " + Session["cdFaseCorrente"];

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

        private void LimpaSessionCantoresCategorias()
        {
            if (Session["_dtOrdemCategoria"] != null)
            {
                _dtOrdemCategoria = (DataTable)Session["_dtOrdemCategoria"];

                foreach (DataRow dr in _dtOrdemCategoria.Rows)
                {
                    Session["dvCantores_" + dr[caConcursosOrdemCategorias.cdCategoria].ToString()] = null;
                }
            }
        }

        private void LimpaSessions()
        {
            LimpaSessionCantoresCategorias();

            Session["strLista"] = null;
            Session["strDivs"] = null;
            Session["_dtAssociacoes"] = null;
            Session["_dtCantoresConcurso"] = null;
            Session["_dtCantoresFases"] = null;
            Session["_dtOrdemCategoria"] = null;
        }

        private void CarregarAssociacoes()
        {
            if (Session["_dtAssociacoes"] != null)
                _dtAssociacoes = (DataTable)Session["_dtAssociacoes"];

            conConcursosAssociacoes objConConcursosAssociacoes = new conConcursosAssociacoes();
            objConConcursosAssociacoes.objCoConcursosAssociacoes.LimparAtributos();
            objConConcursosAssociacoes.objCoConcursosAssociacoes.cdConcurso = Convert.ToInt32(Session["cdConcurso"].ToString());

            if (!conConcursosAssociacoes.Select())
            {
                ltMensagem.Text = MostraMensagem("Falha", "Problemas ao carregar Associações.", csMensagem.msgDanger);
                return;
            }

            _dtAssociacoes = objConConcursosAssociacoes.dtDados;
            Session["_dtAssociacoes"] = _dtAssociacoes;
        }

        private void OrdenaDataTable(ref DataTable dtOrdena, string strOrdenacao)
        {
            DataView dv = dtOrdena.DefaultView;
            dv.Sort = strOrdenacao;
            dtOrdena = dv.ToTable();
        }

        protected void cdFase_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["cdFaseAlteracao"] = cdFase.SelectedValue;
            CarregarCantoresCategorias();
        }

        protected void btnlnkSortearOrdemApresentacao_OnClick(object sender, EventArgs e)
        {
            if (Session["_dtOrdemCategoria"] != null)
            {
                int nuSeq;
                int qtdCantoresFasesCategoria;
                ArrayList alNumSorteados = new ArrayList();
                _dtCantoresFases = (DataTable)Session["_dtCantoresFases"];
                _dtOrdemCategoria = (DataTable)Session["_dtOrdemCategoria"];

                foreach (DataRow drCategoria in _dtOrdemCategoria.Rows)
                {
                    alNumSorteados.Clear();
                    qtdCantoresFasesCategoria =
                        ContarCantoresFaseCategoria(_dtCantoresFases, Convert.ToInt32(drCategoria[caCantoresFases.cdCategoria]));

                    if (Session["dvCantores_" + drCategoria[caConcursosOrdemCategorias.cdCategoria].ToString()] != null)
                    {
                        DataTable dt = (DataTable)Session["dvCantores_" + drCategoria[caConcursosOrdemCategorias.cdCategoria].ToString()];
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

                        Session["dvCantores_" + drCategoria[caConcursosOrdemCategorias.cdCategoria].ToString()] = dt;
                    }
                }

                OrdenaDataTable(ref _dtCantoresFases, caCantoresFases.nuOrdemApresentacao.ToString());
                Session["_dtCantoresFases"] = _dtCantoresFases;
                GerarSeqNuCantor();
                MontaCantoresCategorias(false, false);
            }
        }

        protected void btnSalvar_OnClick(object sender, EventArgs e)
        {
            if (Session["_dtCantoresFases"] != null)
            {
                GerarSeqNuCantor();

                conConcursos objConConcursos = new conConcursos();
                objConConcursos.objCoConcursos.LimparAtributos();
                objConConcursos.objCoConcursos.dtConcursoFases = (DataTable)Session["_dtCantoresFases"];

                if (!conConcursos.AtualizarProximaFase())
                {
                    ltMensagem.Text = MostraMensagem("Falha", "Problemas ao salvar alterações na próxima fase.", csMensagem.msgDanger);
                    return;
                }
            }
        }

        protected void btnConfirmarEdicaoCantor_Click(object sender, EventArgs e)
        {
            if (cdAssociacaoEdit.SelectedIndex > 0 && cdMusicaEdit.SelectedIndex > 0)
            {
                int indexCantorFase = Convert.ToInt32(Session["indexCantorFase"].ToString());
                int indexCantorConcurso = Convert.ToInt32(Session["indexCantorConcurso"].ToString());

                _dtCantoresFases = (DataTable)Session["_dtCantoresFases"];
                _dtCantoresFases.Rows[indexCantorFase][caCantoresFases.CC_Controle] = KuraFrameWork.csConstantes.sAlterando;
                _dtCantoresFases.Rows[indexCantorFase][caCantoresFases.cdMusica] = cdMusicaEdit.SelectedValue;

                AtualizarNuOrdemApresentacao(ref _dtCantoresFases, Convert.ToInt32(nuOrdemApresentacaoEdit.Text));

                if ((nuOrdemApresentacaoEdit.Text.Trim() != "") && (Convert.ToInt32(nuOrdemApresentacaoEdit.Text) > 0))
                    _dtCantoresFases.Rows[indexCantorFase][caCantoresFases.nuOrdemApresentacao] =
                        Convert.ToInt32(nuOrdemApresentacaoEdit.Text);
                else
                {
                    ltMensagemEdicaoCantor.Text = MostraMensagem("Validação!", "Selecione uma Associação e uma Música.", csMensagem.msgWarning);
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "AtivaEdicaoCantor('" + Session["cdCategoriaEdit"].ToString() + "');", true);
                    return;
                }

                DataTable dt;
                CarregaDataTableOrdemCategoriasSession();

                foreach (DataRow drCategoria in _dtOrdemCategoria.Rows)
                {
                    dt = (DataTable)Session["dvCantores_" + drCategoria[caConcursosOrdemCategorias.cdCategoria].ToString()];
                    dt.Columns[caAssociacoes.cdAssociacao].ReadOnly = false;
                    dt.Columns[caAssociacoes.nmAssociacao].ReadOnly = false;
                    dt.Columns[caMusicas.cdMusica].ReadOnly = false;
                    dt.Columns[caMusicas.nmMusica].ReadOnly = false;
                    dt.Columns[caMusicas.nmMusicaKanji].ReadOnly = false;

                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr[caCantores.cdCantor].ToString() == Session["cdCantorEdit"].ToString())
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

                    Session["dvCantores_" + drCategoria[caConcursosOrdemCategorias.cdCategoria].ToString()] = dt;
                }

                Session["_dtCantoresFases"] = _dtCantoresFases;

                PreencheLiteral(Session["cdCategoriaEdit"].ToString().ToString(), false, false);

                AtualizaCantoresCategorias();
            }
            else
            {
                ltMensagemEdicaoCantor.Text = MostraMensagem("Validação!", "Selecione uma Associação e uma Música.", csMensagem.msgWarning);
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "AtivaEdicaoCantor('" + Session["cdCategoriaEdit"].ToString() + "');", true);
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
                int nuOrdemAnterior = Convert.ToInt32(Session["nuOrdemApresentacaoEdit"]);

                if (Session["dvCantores_" + Session["cdCategoriaEdit"].ToString()] != null)
                    dtDados = (DataTable)Session["dvCantores_" + Session["cdCategoriaEdit"].ToString().ToString()];

                dtDados.Columns[caCantoresFases.nuOrdemApresentacao].ReadOnly = false;
                DataRow drAtual = dtDados.NewRow();

                if (pnuOrdemAtual > nuOrdemAnterior)
                {
                    foreach (DataRow dr in pdtCantores.Rows)
                    {
                        if ((Convert.ToInt32(dr[caCantoresFases.nuOrdemApresentacao].ToString()) <= pnuOrdemAtual) &&
                            (Convert.ToInt32(dr[caCantoresFases.nuOrdemApresentacao].ToString()) > nuOrdemAnterior) &&
                            (Convert.ToInt32(Session["cdCategoriaEdit"])) == (Convert.ToInt32(dr[caCantoresFases.cdCategoria].ToString())))
                            dr[caCantoresFases.nuOrdemApresentacao] = Convert.ToInt32(dr[caCantoresFases.nuOrdemApresentacao].ToString()) - 1;
                    }

                    foreach (DataRow dr in dtDados.Rows)
                    {
                        if (Convert.ToInt32(dr[caCantoresFases.nuOrdemApresentacao].ToString()) == nuOrdemAnterior)
                        {
                            drAtual = dr;
                            break;
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
                            (Convert.ToInt32(Session["cdCategoriaEdit"])) == (Convert.ToInt32(dr[caCantoresFases.cdCategoria].ToString())))
                            dr[caCantoresFases.nuOrdemApresentacao] = Convert.ToInt32(dr[caCantoresFases.nuOrdemApresentacao].ToString()) + 1;
                    }

                    foreach (DataRow dr in dtDados.Rows)
                    {
                        if (Convert.ToInt32(dr[caCantoresFases.nuOrdemApresentacao].ToString()) == nuOrdemAnterior)
                        {
                            drAtual = dr;
                            break;
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
                Session["dvCantores_" + Session["cdCategoriaEdit"].ToString().ToString()] = dtDados;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void GerarSeqNuCantor()
        {
            if (Session["_dtOrdemCategoria"] != null)
            {
                int nuCantor = 1;
                int nuSeq;
                _dtCantoresFases = (DataTable)Session["_dtCantoresFases"];
                _dtOrdemCategoria = (DataTable)Session["_dtOrdemCategoria"];
                foreach (DataRow drCategoria in _dtOrdemCategoria.Rows)
                {
                    nuSeq = 1;
                    if (Session["dvCantores_" + drCategoria[caConcursosOrdemCategorias.cdCategoria].ToString()] != null)
                    {
                        DataTable dt = (DataTable)Session["dvCantores_" + drCategoria[caConcursosOrdemCategorias.cdCategoria].ToString()];
                        dt.Columns[caCantoresFases.nuOrdemApresentacao].ReadOnly = false;
                        dt.Columns[caCantoresFases.nuCantor].ReadOnly = false;

                        foreach (DataRow drCantor in _dtCantoresFases.Rows)
                        {
                            if (drCategoria[caConcursosOrdemCategorias.cdCategoria].ToString() == drCantor[caCantoresFases.cdCategoria].ToString())
                            {
                                foreach (DataRow drCantCat in dt.Rows)
                                {
                                    if (drCantor[caCantoresFases.cdCantor].ToString() == drCantCat[caCantoresFases.cdCantor].ToString())
                                    {
                                        drCantCat[caCantoresFases.nuOrdemApresentacao] = nuSeq;
                                        drCantCat[caCantoresFases.nuCantor] = (nuCantor).ToString().PadLeft(3, '0');
                                        break;
                                    }
                                }

                                drCantor[caCantoresFases.CC_Controle] = KuraFrameWork.csConstantes.sAlterando;
                                drCantor[caCantoresFases.nuOrdemApresentacao] = nuSeq++;
                                drCantor[caCantoresFases.nuCantor] = (nuCantor++).ToString().PadLeft(3, '0');
                            }
                        }

                        Session["dvCantores_" + drCategoria[caConcursosOrdemCategorias.cdCategoria].ToString()] = dt;
                    }
                }

                Session["_dtCantoresFases"] = _dtCantoresFases;
                MontaCantoresCategorias(false, false);
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

            _dtCantoresConcurso = (DataTable)Session["_dtCantoresConcurso"];
            _dtCantoresFases = (DataTable)Session["_dtCantoresFases"];
            int cdCantorEdit = Convert.ToInt32(strParam[0]);
            int cdCategoriaEdit = Convert.ToInt32(strParam[1]);

            Session["cdCantorEdit"] = cdCantorEdit;
            Session["cdCategoriaEdit"] = cdCategoriaEdit;

            csAssociacoes vcsAssociacoesCancores = new csAssociacoes();
            vcsAssociacoesCancores.bUtilizaDadosExternos = true;
            vcsAssociacoesCancores.dtDadosExternos = (DataTable)Session["_dtAssociacoes"] == null ?
                conAssociacoes.objCo.RetornaEstruturaDT() : (DataTable)Session["_dtAssociacoes"];
            cdAssociacaoEdit = vcsAssociacoesCancores.CarregaDDL(cdAssociacaoEdit);

            csMusicas vcsMusicas = new csMusicas();
            cdMusicaEdit = vcsMusicas.CarregaDDL(cdMusicaEdit);

            for (int i = 0; i < _dtCantoresFases.Rows.Count; i++)
            {
                if ((Convert.ToInt32(_dtCantoresFases.Rows[i][caCantoresFases.cdCantor].ToString()) == cdCantorEdit) &&
                    (Convert.ToInt32(_dtCantoresFases.Rows[i][caCantoresFases.cdCategoria].ToString()) == cdCategoriaEdit))
                {
                    Session["indexCantorFase"] = i;

                    ltTituloEdicaoCantor.Text = _dtCantoresFases.Rows[i][caCantoresFases.CC_deCategoria].ToString() + "<br/><br/>"
                        + _dtCantoresFases.Rows[i][caCantoresFases.CC_nmCantor].ToString() + "<br/>"
                        + _dtCantoresFases.Rows[i][caCantoresFases.CC_nmNomeKanji].ToString();

                    cdMusicaEdit.SelectedValue = _dtCantoresFases.Rows[i][caCantoresFases.cdMusica].ToString();
                    nuCantorEdit.Text = _dtCantoresFases.Rows[i][caCantoresFases.nuCantor].ToString();
                    nuOrdemApresentacaoEdit.Text = _dtCantoresFases.Rows[i][caCantoresFases.nuOrdemApresentacao].ToString();
                    Session["nuOrdemApresentacaoEdit"] = nuOrdemApresentacaoEdit.Text;
                }

                for (int k = 0; k < _dtCantoresConcurso.Rows.Count; k++)
                {
                    if (Convert.ToInt32(_dtCantoresConcurso.Rows[k][caCantoresConcursos.cdCantor].ToString()) == cdCantorEdit)
                    {
                        Session["cdAssociacaoEdit"] = Convert.ToInt32(_dtCantoresConcurso.Rows[k][caCantoresConcursos.cdAssociacao].ToString());
                        Session["indexCantorConcurso"] = k;

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
            objConCantoresConcursos.objCoCantoresConcursos.cdConcurso = Convert.ToInt32(Session["cdConcurso"].ToString());

            if (!conCantoresConcursos.Select())
            {
                ltMensagem.Text = MostraMensagem("Falha!", "Falha ao carregar os cantores do concurso.", csMensagem.msgDanger);
                return;
            }

            _dtCantoresConcurso = objConCantoresConcursos.dtDados;
            Session["_dtCantoresConcurso"] = _dtCantoresConcurso;

            conCantoresFases objConCantoresFases = new conCantoresFases();
            objConCantoresFases.objCoCantoresFases.LimparAtributos();
            objConCantoresFases.objCoCantoresFases.cdConcurso = Convert.ToInt32(Session["cdConcurso"].ToString());
            objConCantoresFases.objCoCantoresFases.cdFase = Convert.ToInt32(Session["cdFaseAlteracao"]);

            if (!conCantoresFases.Select())
            {
                ltMensagem.Text = MostraMensagem("Falha!", "Falha ao carregar as categorias do concurso.", csMensagem.msgDanger);
                return;
            }

            _dtCantoresFases = objConCantoresFases.dtDados;
            Session["_dtCantoresFases"] = _dtCantoresFases;

            CarregarCategorias();
        }

        private void CarregarCategorias()
        {
            conConcursosOrdemCategorias objConOrdemCategorias = new conConcursosOrdemCategorias();
            objConOrdemCategorias.objCoConcursosOrdemCategorias.LimparAtributos();
            objConOrdemCategorias.objCoConcursosOrdemCategorias.cdConcurso = Convert.ToInt32(Session["cdConcurso"].ToString());

            if (!conConcursosOrdemCategorias.Select())
            {
                ltMensagem.Text = MostraMensagem("Falha!", "Falha ao carregar as categorias do concurso.", csMensagem.msgDanger);
                return;
            }

            _dtOrdemCategoria = objConOrdemCategorias.dtDados;
            OrdenaDataTable(ref _dtOrdemCategoria, caConcursosOrdemCategorias.nuOrdem + KuraFrameWork.csConstantes.sCrescente);
            Session["_dtOrdemCategoria"] = _dtOrdemCategoria;

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
                dr[caConcursosOrdemCategorias.cdConcurso] = Convert.ToInt32(Session["cdConcurso"].ToString());
                dr[caConcursosOrdemCategorias.cdCategoria] = strCdCategoria;
                dr[caConcursosOrdemCategorias.CC_deCategoria] = strDeCategoria;
                dr[caConcursosOrdemCategorias.nuOrdem] = nuOrdemCategoria;

                _dtOrdemCategoria.Rows.Add(dr);
                OrdenaDataTable(ref _dtOrdemCategoria, caConcursosOrdemCategorias.nuOrdem + KuraFrameWork.csConstantes.sCrescente);
                Session["_dtOrdemCategoria"] = _dtOrdemCategoria;
            }
        }

        private void CarregaDataTableOrdemCategoriasSession()
        {
            conConcursosOrdemCategorias objConOrdemCategoria = new conConcursosOrdemCategorias();
            _dtOrdemCategoria = objConOrdemCategoria.objCoConcursosOrdemCategorias.RetornaEstruturaDT();

            if (Session["_dtOrdemCategoria"] != null)
                _dtOrdemCategoria = (DataTable)Session["_dtOrdemCategoria"];
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
                                if (Session["dvCantores_" + _dtOrdemCategoria.Rows[i][caConcursosOrdemCategorias.cdCategoria].ToString()] != null)
                                    dtDados = (DataTable)Session["dvCantores_" +
                                        _dtOrdemCategoria.Rows[i][caConcursosOrdemCategorias.cdCategoria].ToString()];
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
                                    objConCantoresFases.objCoCantoresFases.cdConcurso = Convert.ToInt32(Session["cdConcurso"].ToString());
                                    objConCantoresFases.objCoCantoresFases.cdFase =
                                        Convert.ToInt32(Session["cdFaseAlteracao"]);
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

                                Session["dvCantores_" + _dtOrdemCategoria.Rows[i][caConcursosOrdemCategorias.cdCategoria].ToString()] = dtDados;

                                OrdenaDataTable(ref dtDados, "nuOrdemApresentacao");

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

            Session["strScriptGridView"] = strScriptGridView;
            ltCategorias.Text = strInicio + strLista + strMeio + strDivs + strFim;
        }
    }
}