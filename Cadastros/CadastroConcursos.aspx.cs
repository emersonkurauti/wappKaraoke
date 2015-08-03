using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wappKaraoke.Classes;
using System.Data;
using System.Text;
using System.Collections;
using wappKaraoke.Classes.Model.Concursos;
using wappKaraoke.Classes.Controller;
using wappKaraoke.Classes.Mensagem;
using wappKaraoke.Classes.Model.Arquivos;
using wappKaraoke.Classes.Model.ConcursosAssociacoes;
using wappKaraoke.Classes.Model.Grupos;
using wappKaraoke.Classes.Model.Jurados;
using wappKaraoke.Classes.Model.CantoresFases;
using wappKaraoke.Classes.Model.Categorias;
using wappKaraoke.Classes.Model.Cantores;
using wappKaraoke.Classes.Model.Musicas;
using wappKaraoke.Classes.Model.Associacoes;
using wappKaraoke.Classes.Model.Fases;
using wappKaraoke.Classes.Model.TipoStatus;
using wappKaraoke.Classes.Model.CantoresConcursos;
using wappKaraoke.Classes.Model.ConcursosOrdemCategorias;

namespace wappKaraoke.Cadastros
{
    public partial class CadastroConcursos : csPageCadastro
    {
        private DataTable _dtDocumentos;
        private DataTable _dtDocumentosExc;
        private DataTable _dtImagens;
        private DataTable _dtImagensExc;
        private DataTable _dtAssociacoes;
        private DataTable _dtAssociacoesExc;
        private DataTable _dtGruposJurados;
        private DataTable _dtGruposJuradosExc;
        private DataTable _dtCantoresConcurso;
        private DataTable _dtCantoresConcursoExc;
        private DataTable _dtCantoresFases;
        private DataTable _dtCantoresFasesExc;
        private DataTable _dtOrdemCategoria;
        private DataTable _dtOrdemCategoriaExc;

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
                if (Request["__EVENTARGUMENT"].Contains("RemoveImagem") ||
                    Request["__EVENTARGUMENT"].Contains("EditarImagem") ||
                    Request["__EVENTARGUMENT"].Contains("SalvarImagem"))
                {
                    if (Request["__EVENTARGUMENT"].Contains("RemoveImagem"))
                    {
                        RemoverImagem();
                    }
                    else if (Request["__EVENTARGUMENT"].Contains("EditarImagem"))
                    {
                        EditarImagem();
                    }
                    else if (Request["__EVENTARGUMENT"].Contains("SalvarImagem"))
                    {
                        SalvarImagem();
                    }
                }

                if (Request["__EVENTTARGET"].Contains("lnkEditCantor"))
                {
                    EditarCantor();
                }
                else if (Request["__EVENTTARGET"].Contains("lnkDeleteCantor"))
                {
                    RemoverCantor();
                }

                if (Session["_dtDocumentos"] != null && ((DataTable)Session["_dtDocumentos"]).Rows.Count > 0)
                    ConfigurarGridView();

                if (Request["__EVENTTARGET"] != null && !Request["__EVENTTARGET"].ToString().Contains("btnAdicionarArquivo"))
                {
                    RegistrarScriptLoaded();
                }

                return;
            }
            ltMensagemArquivos.Text = "";

            ltMensagemDefault = ltMensagem;
            tobjCa = typeof(caConcursos);
            objCon = new conConcursos();

            if (!this.IsPostBack)
            {
                LimpaSessions();
                LimpaMensagens();

                PegarChaveConcurso();

                CarregarArquivos();
                CarregarAssociacoes();
                CarregarGruposJurados();
                CarregarCantoresCategorias();

                CarregarDDL();
            }

            base.Page_Load(sender, e);

            MostrarAvisoConcursoFinalizado();
        }

        /// <summary>
        /// GERAL
        /// </summary>
        protected override void InicializaSessions()
        {
            base.InicializaSessions();
        }

        protected override bool ConfigurarGridView()
        {
            if (!base.ConfigurarGridView())
                return false;

            try
            {
                ConfiguraGridAssociacoes();
                ConfiguraGridJurados();
                ConfiguraGridDocumentos();
                ConfiguraGridOrdemApres();

                return true;
            }
            catch
            {
                return false;
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
            //Session["alCdCategoria"] = null;
            //Session["alDeCategoria"] = null;
            Session["_dtCantoresConcursoExc"] = null;
            Session["_dtCantoresConcurso"] = null;
            Session["_dtCantoresFasesExc"] = null;
            Session["_dtCantoresFases"] = null;

            Session["_dtDocumentosExc"] = null;
            Session["_dtDocumentos"] = null;
            Session["_dtImagensExc"] = null;
            Session["_dtImagens"] = null;
            Session["_dtAssociacoesExc"] = null;
            Session["_dtAssociacoes"] = null;
            Session["_dtGruposJuradosExc"] = null;
            Session["_dtGruposJurados"] = null;
            Session["_dtOrdemCategoria"] = null;
            Session["_dtOrdemCategoriaExc"] = null;
        }

        private void LimpaMensagens()
        {
            ltMensagem.Text = "";
            ltMensagemArquivos.Text = "";
            ltMensagemAssociacoes.Text = "";
            ltMensagemJurados.Text = "";
            ltMensagensCategorias.Text = "";
        }

        private void ConfiguraGridAssociacoes()
        {
            gvAssociacoes.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        private void ConfiguraGridJurados()
        {
            _dtGruposJurados = (DataTable)Session["_dtGruposJurados"];

            for (int i = 0; i < _dtGruposJurados.Rows.Count; i++)
            {
                ((Literal)gvGrupoJuradoConcurso.Rows[i].FindControl("ltNomeKanji")).Text = @"" +
                    _dtGruposJurados.Rows[i]["CC_nmJurado"].ToString() + " <br/> " +
                    _dtGruposJurados.Rows[i]["CC_nmNomeKanji"].ToString();
            }

            //Attribute to show the Plus Minus Button.
            //gvGrupoJuradoConcurso.HeaderRow.Cells[2].Attributes["data-class"] = "expand";

            //Attribute to hide column in Phone.
            //gvGrupoJuradoConcurso.HeaderRow.Cells[0].Attributes["data-hide"] = "phone";
            //gvGrupoJuradoConcurso.HeaderRow.Cells[1].Attributes["data-hide"] = "phone";
            //gvGrupoJuradoConcurso.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
            //gvGrupoJuradoConcurso.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";

            //Adds THEAD and TBODY to GridView.
            gvGrupoJuradoConcurso.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        private void ConfiguraGridDocumentos()
        {
            //Attribute to show the Plus Minus Button.
            //gvDocumentos.HeaderRow.Cells[1].Attributes["data-class"] = "expand";

            //Attribute to hide column in Phone.
            //gvDocumentos.HeaderRow.Cells[0].Attributes["data-hide"] = "phone";
            //gvDocumentos.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";

            //Adds THEAD and TBODY to GridView.
            gvDocumentos.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        private void ConfiguraGridOrdemApres()
        {
            gvOrdemApres.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        private void RegistrarScript()
        {
            string strScriptGridView = Session["strScriptGridView"] != null ? Session["strScriptGridView"].ToString() : "";
            string strScriptImagens = Session["strScriptImagens"] != null ? Session["strScriptImagens"].ToString() : "";

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", strScriptGridView + strScriptImagens, true);
        }

        private void RegistrarScriptLoaded()
        {
            string strScriptGridView = Session["strScriptGridView"] != null ? Session["strScriptGridView"].ToString() : "";
            string strScriptImagens = Session["strScriptImagens"] != null ? Session["strScriptImagens"].ToString() : "";

            ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "", strScriptGridView + strScriptImagens, true);
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

        public void btnFechar_Click(Object sender, EventArgs e)
        {
            flFinalizado.Checked = true;            
            GerarSeqNuCantor();
            MostrarAvisoConcursoFinalizado();
        }

        public void btnReabrir_Click(Object sender, EventArgs e)
        {
            flFinalizado.Checked = false;
            MostrarAvisoConcursoFinalizado();
        }

        private void PegarChaveConcurso()
        {
            if (Session["IndexRowDados"] != null)
            {
                IndexRowDados = (int)Session["IndexRowDados"];
                dtDados = (DataTable)Session["dtDados"];

                Session["cdConcurso"] = dtDados.Rows[IndexRowDados][caConcursos.nmCampoChave.ToString()].ToString();
            }
            else
                Session["cdConcurso"] = 0;
        }

        private void CarregarDDL()
        {
            csCidades vcsCidades = new csCidades();
            cdCidade = vcsCidades.CarregaDDL(cdCidade);

            csAssociacoes vcsAssociacoes = new csAssociacoes();
            cdAssociacao = vcsAssociacoes.CarregaDDL(cdAssociacao);

            csJurados vcsJurados = new csJurados();
            cdJurado = vcsJurados.CarregaDDL(cdJurado);

            csCategorias vcsCategorias = new csCategorias();
            cdCategoria = vcsCategorias.CarregaDDL(cdCategoria);

            csCantores vcsCancotres = new csCantores();
            cdCantor = vcsCancotres.CarregaDDL(cdCantor);

            csMusicas vcsMusicas = new csMusicas();
            cdMusica = vcsMusicas.CarregaDDL(cdMusica);

            CarregaDDLAssociacoesCantores();
        }

        private DataTable UnionDataTable(DataTable dt1, DataTable dt2, DataTable dtResult)
        {
            foreach (DataRow dr in dt1.Rows)
            {
                dtResult.ImportRow(dr);
            }
            foreach (DataRow dr in dt2.Rows)
            {
                dtResult.ImportRow(dr);
            }

            return dtResult;
        }

        private void PreencheObjetos(coConcursos objCoConcurso)
        {
            DateTime dtData;
            objCoConcurso.cdConcurso = Session["IndexRowDados"] == null ? 0 : Convert.ToInt32(Session["cdConcurso"].ToString());
            objCoConcurso.nmConcurso = nmConcurso.Text;
            objCoConcurso.nmConcursoKanji = nmConcursoKanji.Text;
            objCoConcurso.cdCidade = Convert.ToInt32(cdCidade.SelectedValue.ToString());
            objCoConcurso.flFinalizado = flFinalizado.Checked ? "S" : "N";
            objCoConcurso.flConcursoCorrente = flConcursoCorrente.Checked ? "S" : "N";
            objCoConcurso.cdFaseCorrente = Convert.ToInt32(wappKaraoke.Properties.Settings.Default.sCodFaseInicial);
            DateTime.TryParse(dtIniConcurso.Text, out dtData);
            objCoConcurso.dtIniConcurso = dtData;
            DateTime.TryParse(dtFimConcurso.Text, out dtData);
            objCoConcurso.dtFimConcurso = dtData;

            DataTable dtResult;

            //Arquivos
            if (Session["_dtDocumentos"] != null)
            {
                _dtDocumentos = (DataTable)Session["_dtDocumentos"];
                if (Session["_dtDocumentosExc"] != null)
                {
                    dtResult = conArquivos.objCo.RetornaEstruturaDT();
                    _dtDocumentos = UnionDataTable(((DataTable)Session["_dtDocumentosExc"]), ((DataTable)Session["_dtDocumentos"]), dtResult);
                }
            }

            if (Session["_dtImagens"] != null)
            {
                _dtImagens = (DataTable)Session["_dtImagens"];
                if (Session["_dtImagensExc"] != null)
                {
                    dtResult = conArquivos.objCo.RetornaEstruturaDT();
                    _dtImagens = UnionDataTable(((DataTable)Session["_dtImagensExc"]), ((DataTable)Session["_dtImagens"]), dtResult);
                }
            }

            //Associacoes
            if (Session["_dtAssociacoes"] != null)
            {
                _dtAssociacoes = (DataTable)Session["_dtAssociacoes"];
                if (Session["_dtAssociacoesExc"] != null)
                {
                    dtResult = conConcursosAssociacoes.objCo.RetornaEstruturaDT();
                    _dtAssociacoes = UnionDataTable(((DataTable)Session["_dtAssociacoesExc"]), ((DataTable)Session["_dtAssociacoes"]), dtResult);
                }
            }

            //Grupo Jurados
            if (Session["_dtGruposJurados"] != null)
            {
                _dtGruposJurados = (DataTable)Session["_dtGruposJurados"];
                if (Session["_dtGruposJuradosExc"] != null)
                {
                    dtResult = conGrupos.objCo.RetornaEstruturaDT();
                    _dtGruposJurados = UnionDataTable(((DataTable)Session["_dtGruposJuradosExc"]), ((DataTable)Session["_dtGruposJurados"]), dtResult);
                }
            }

            //Cantores Concursos
            if (Session["_dtCantoresConcurso"] != null)
            {
                _dtCantoresConcurso = (DataTable)Session["_dtCantoresConcurso"];
                if (Session["_dtCantoresConcursoExc"] != null)
                {
                    dtResult = conCantoresConcursos.objCo.RetornaEstruturaDT();
                    _dtCantoresConcurso = UnionDataTable(((DataTable)Session["_dtCantoresConcursoExc"]), ((DataTable)Session["_dtCantoresConcurso"]), dtResult);
                }
            }

            //Cantores Fases
            if (Session["_dtCantoresFases"] != null)
            {
                _dtCantoresFases = (DataTable)Session["_dtCantoresFases"];
                if (Session["_dtCantoresFasesExc"] != null)
                {
                    dtResult = conCantoresFases.objCo.RetornaEstruturaDT();
                    _dtCantoresFases = UnionDataTable(((DataTable)Session["_dtCantoresFasesExc"]), ((DataTable)Session["_dtCantoresFases"]), dtResult);
                }
            }

            //Categorias Concursos
            if (Session["_dtOrdemCategoria"] != null)
            {
                _dtOrdemCategoria = (DataTable)Session["_dtOrdemCategoria"];
                if (Session["_dtOrdemCategoriaExc"] != null)
                {
                    dtResult = conConcursosOrdemCategorias.objCo.RetornaEstruturaDT();
                    _dtOrdemCategoria = UnionDataTable(((DataTable)Session["_dtOrdemCategoriaExc"]), ((DataTable)Session["_dtOrdemCategoria"]), dtResult);
                }
            }

            dtResult = conArquivos.objCo.RetornaEstruturaDT();
            objCoConcurso.dtArquivos = UnionDataTable(_dtDocumentos, _dtImagens, dtResult);
            objCoConcurso.dtAssociacoes = _dtAssociacoes;
            objCoConcurso.dtGrupoJurados = _dtGruposJurados;
            objCoConcurso.dtConcursoCantores = _dtCantoresConcurso;
            objCoConcurso.dtConcursoFases = _dtCantoresFases;
            objCoConcurso.dtOrdemCategoria = _dtOrdemCategoria;
        }

        private bool ValidarCamposPreenchidos()
        {
            if (nmConcurso.Text.Trim() == "")
            {
                ltMensagem.Text = MostraMensagem("Validação", "Informe o nome do Concurso.", csMensagem.msgWarning);
                return false;
            }

            if (nmConcursoKanji.Text.Trim() == "")
            {
                ltMensagem.Text = MostraMensagem("Validação", "Informe o nome em Kanji do Concurso.", csMensagem.msgWarning);
                return false;
            }

            if (cdCidade.SelectedIndex <= 0)
            {
                ltMensagem.Text = MostraMensagem("Validação", "Informe a cidade do Concurso.", csMensagem.msgWarning);
                return false;
            }

            if (dtIniConcurso.Text.Trim() == "")
            {
                ltMensagem.Text = MostraMensagem("Validação", "Informe a data de início do Concurso.", csMensagem.msgWarning);
                return false;
            }

            return true;
        }

        protected override void btnSalvar_Click(object sender, EventArgs e)
        {
            conConcursos objConConcursos = new conConcursos();
            bool bInserindo = Session["IndexRowDados"] == null;

            objConConcursos.objCoConcursos.LimparAtributos();

            if (!bErro)
            {
                if (!ValidarCamposPreenchidos())
                {
                    bErro = true;
                }
                else
                {
                    PreencheObjetos(objConConcursos.objCoConcursos);

                    if (bInserindo)
                    {
                        if (conConcursos.Inserir())
                        {
                            ltMensagem.Text = base.MostraMensagem(csMensagem.msgOperacaoComSucesso, csMensagem.msgRegistroInserido, csMensagem.msgSucess);
                        }
                        else
                        {
                            bErro = true;
                            ltMensagem.Text = base.MostraMensagem(csMensagem.msgTitFalhaGenerica, objConConcursos.strMensagemErro, csMensagem.msgWarning);
                        }
                    }
                    else
                    {
                        if (conConcursos.Alterar())
                        {
                            ltMensagem.Text = base.MostraMensagem(csMensagem.msgOperacaoComSucesso, csMensagem.msgRegistroAlterado, csMensagem.msgSucess);
                        }
                        else
                        {
                            bErro = true;
                            ltMensagem.Text = base.MostraMensagem(csMensagem.msgTitFalhaGenerica, objConConcursos.strMensagemErro, csMensagem.msgWarning);
                        }
                    }
                }

                Session["ltMensagemDefault"] = ltMensagem;
            }

            if (!bErro)
            {
                string strPagina = Session["_strPaginaConsulta"].ToString();
                Session["_strPaginaConsulta"] = null;
                Response.Redirect(strPagina.Replace("Cadastro", "Consulta"));
            }
        }

        private void CarregaDDLAssociacoesCantores()
        {
            csAssociacoes vcsAssociacoesCancores = new csAssociacoes();
            vcsAssociacoesCancores.bUtilizaDadosExternos = true;
            vcsAssociacoesCancores.dtDadosExternos = (DataTable)Session["_dtAssociacoes"] == null ?
                conAssociacoes.objCo.RetornaEstruturaDT() : (DataTable)Session["_dtAssociacoes"];
            cdAssociacaoCantor = vcsAssociacoesCancores.CarregaDDL(cdAssociacaoCantor);
        }

        private bool VaidaRegistroExistente(DataTable pdtDados, string psChaveBusca, string psColunaBusca)
        {
            foreach (DataRow dr in pdtDados.Rows)
            {
                if (dr[psColunaBusca].ToString() == psChaveBusca)
                    return true;
            }

            return false;
        }

        private void OrdenaDataTable(ref DataTable dtOrdena, string strOrdenacao)
        {
            DataView dv = dtOrdena.DefaultView;
            dv.Sort = strOrdenacao;
            dtOrdena = dv.ToTable();
        }

        private bool ValidarConcursoFechado()
        {
            return flFinalizado.Checked; 
        }

        private void MostrarAvisoConcursoFinalizado()
        {
            btnFechar.Enabled = !flFinalizado.Checked;
            btnFechar.CssClass += " disabled";

            btnSalvar.Enabled = !flFinalizado.Checked;
            btnSalvar.CssClass += " disabled";

            btnSortearOrdemApresentacao.Enabled = !flFinalizado.Checked;
            btnSortearOrdemApresentacao.CssClass += " disabled";

            if (btnFechar.Enabled)
            {
                btnFechar.CssClass = btnFechar.CssClass.Replace("disabled", "");
                btnSalvar.CssClass = btnSalvar.CssClass.Replace("disabled", "");
                btnSortearOrdemApresentacao.CssClass = btnSortearOrdemApresentacao.CssClass.Replace("disabled", "");   
            }

            ltMensagem.Text = "";

            if (ValidarConcursoFechado())
            {
                ltMensagem.Text = MostraMensagem("CONCURSO FINALIZADO!", "Concurso finalizado não permite a edição dos dados," +
                        " apenas de arquivos (Imagens e Documentos).", csMensagem.msgInfo);
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
                GerarSeqNuCantor();
                Session["_dtCantoresFases"] = _dtCantoresFases;
                MontaCantoresCategorias(false, false);
            }
        }

        /// <summary>
        /// Imagens/Documentos
        /// </summary>
        private void RemoverImagem()
        {
            if (Session["_dtImagensExc"] != null)
                _dtImagensExc = (DataTable)Session["_dtImagensExc"];
            else
                _dtImagensExc = conArquivos.objCo.RetornaEstruturaDT();

            int intTamanhoParam = Request["__EVENTARGUMENT"].ToString().IndexOf(';') + 1;
            string strParametro = Request["__EVENTARGUMENT"].ToString().Substring(intTamanhoParam, Request["__EVENTARGUMENT"].Length - intTamanhoParam);
            
            _dtImagens = (DataTable)Session["_dtImagens"];
            _dtImagens.Rows[Convert.ToInt32(strParametro)][caArquivos.CC_Controle] = KuraFrameWork.csConstantes.sTpExcluido;

            _dtImagensExc.ImportRow(_dtImagens.Rows[Convert.ToInt32(strParametro)]);
            _dtImagens.Rows.Remove(_dtImagens.Rows[Convert.ToInt32(strParametro)]);

            Session["_dtImagens"] = _dtImagens;
            Session["_dtImagensExc"] = _dtImagensExc;

            CarregarImagens();
        }

        private void EditarImagem()
        {
            int intTamanhoParam = Request["__EVENTARGUMENT"].ToString().IndexOf(';') + 1;
            string strParametro = Request["__EVENTARGUMENT"].ToString().Substring(intTamanhoParam, Request["__EVENTARGUMENT"].Length - intTamanhoParam);
            CarregarImagens(Convert.ToInt32(strParametro));
        }

        private void SalvarImagem()
        {
            int intPosIni = Request["__EVENTARGUMENT"].ToString().IndexOf(';') + 1;
            int intPosFim = Request["__EVENTARGUMENT"].ToString().LastIndexOf(';');

            string strParametro = Request["__EVENTARGUMENT"].ToString().Substring(intPosIni, intPosFim - intPosIni);
            string strValor = Request["__EVENTARGUMENT"].ToString().Substring(intPosFim + 1, Request["__EVENTARGUMENT"].ToString().Length - (intPosFim + 1));
            _dtImagens = (DataTable)Session["_dtImagens"];

            if (_dtImagens.Rows[Convert.ToInt32(strParametro)][caArquivos.CC_Controle].ToString() == KuraFrameWork.csConstantes.sTpCarregado)
                _dtImagens.Rows[Convert.ToInt32(strParametro)][caArquivos.CC_Controle] = KuraFrameWork.csConstantes.sTpAlterado;

            _dtImagens.Rows[Convert.ToInt32(strParametro)][caArquivos.deArquivo] = strValor;

            Session["_dtImagens"] = _dtImagens;

            CarregarImagens();
        }

        private void CopiarArquivoParaTemp()
        {
            string strCaminhoTemp = Request.PhysicalApplicationPath + wappKaraoke.Properties.Settings.Default.sCaminhoTemp;
            fluArquivo.SaveAs(strCaminhoTemp + fluArquivo.FileName);
        }

        private void AddArquivo(ref DataTable pdtArquivo)
        {
            DataRow dr = pdtArquivo.NewRow();

            dr[caArquivos.CC_Controle] = KuraFrameWork.csConstantes.sTpInserido;
            dr[caArquivos.cdArquivo] = 0;
            dr[caArquivos.cdConcurso] = Convert.ToInt32(Session["cdConcurso"].ToString());
            dr[caArquivos.cdTipoArquivo] = Convert.ToInt32(hdfCdTpArquivo.Value.ToString());
            dr[caArquivos.nmArquivo] = hdfNmArquivo.Value.ToString();
            dr[caArquivos.deArquivo] = deArquivo.Text;

            pdtArquivo.Rows.Add(dr);
        }

        private void CarregarArquivos()
        {
            conArquivos objConArquivos = new conArquivos();
            objConArquivos.objCoArquivos.LimparAtributos();
            objConArquivos.objCoArquivos.cdConcurso = Convert.ToInt32(Session["cdConcurso"].ToString());

            if (Session["_dtImagens"] != null)
                _dtImagens = (DataTable)Session["_dtImagens"];
            else
            {
                objConArquivos.objCoArquivos.cdTipoArquivo = csConstantes.cCdTipoArquivoImagem;

                if (!conArquivos.Select())
                    ltMensagemArquivos.Text = MostraMensagem("Falha", "Problemas ao carregar imagens.", csMensagem.msgDanger);

                Session["_dtImagens"] = objConArquivos.dtDados;
            }

            CarregarImagens();

            if (Session["_dtDocumentos"] != null)
                _dtDocumentos = (DataTable)Session["_dtDocumentos"];
            else
            {
                objConArquivos.objCoArquivos.cdTipoArquivo = csConstantes.cCdTipoArquivoDocumento;

                if (!conArquivos.Select())
                    ltMensagemArquivos.Text = MostraMensagem("Falha", "Problemas ao carregar documentos.", csMensagem.msgDanger);

                _dtDocumentos = objConArquivos.dtDados;
            }

            gvDocumentos.DataSource = _dtDocumentos;
            gvDocumentos.DataBind();

            Session["_dtDocumentos"] = _dtDocumentos;

            if (_dtDocumentos.Rows.Count > 0)
                ConfigurarGridView();
        }

        protected void btnAdicionarArquivo_Click(object sender, EventArgs e)
        {
            ltMensagemArquivos.Text = "";

            if (hdfNmArquivo.Value.ToString() != "")
            {
                _dtDocumentos = conArquivos.objCo.RetornaEstruturaDT();
                _dtImagens = conArquivos.objCo.RetornaEstruturaDT();

                if (Session["_dtDocumentos"] != null)
                {
                    _dtDocumentos = (DataTable)Session["_dtDocumentos"];
                }
                if (Session["_dtImagens"] != null)
                {
                    _dtImagens = (DataTable)Session["_dtImagens"];
                }

                DataTable dtArquivos = UnionDataTable(_dtDocumentos, _dtImagens, conArquivos.objCo.RetornaEstruturaDT());

                if (VaidaRegistroExistente(dtArquivos, hdfNmArquivo.Value.ToString(), caArquivos.nmArquivo))
                {
                    ltMensagemArquivos.Text = MostraMensagem("Validação!", "Arquivo com mesmo nome já inserido.", csMensagem.msgWarning);
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "AtivaAbaArquivosImagens();", true);
                    return;
                }

                if (Convert.ToInt32(hdfCdTpArquivo.Value.ToString()) == csConstantes.cCdTipoArquivoImagem)
                {
                    if (Session["_dtImagens"] != null)
                    {
                        CopiarArquivoParaTemp();
                        _dtImagens = (DataTable)Session["_dtImagens"];
                        AddArquivo(ref _dtImagens);
                    }

                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "AtivaAbaArquivosImagens();", true);
                }
                else if (Convert.ToInt32(hdfCdTpArquivo.Value.ToString()) == csConstantes.cCdTipoArquivoDocumento)
                {
                    if (Session["_dtDocumentos"] != null)
                    {
                        CopiarArquivoParaTemp();
                        _dtDocumentos = (DataTable)Session["_dtDocumentos"];
                        AddArquivo(ref _dtDocumentos);
                    }

                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "AtivaAbaArquivosDocumentos();", true);
                }

                deArquivo.Text = "";
                CarregarArquivos();
            }
            else
            {
                ltMensagemArquivos.Text = MostraMensagem("Validação!", "Selecione um arquivo para adicionar.", csMensagem.msgWarning);
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "AtivaAbaArquivosImagens();", true);
            }

            hdfNmArquivo.Value = "";
            hdfCdTpArquivo.Value = "";
        }

        private void CarregarImagens(int pnIndexImgEditar = -1)
        {
            if (Session["_dtImagens"] != null)
            {
                int seq = 0;
                string strCaminho = "../" + wappKaraoke.Properties.Settings.Default.sCaminhoArqImagens.Replace("\\", "/");
                string strCaminhoTemp = "../" + wappKaraoke.Properties.Settings.Default.sCaminhoTemp.Replace("\\", "/");
                string strCaminhoImg = "";
                string strScriptImagens = "";

                _dtImagens = (DataTable)Session["_dtImagens"];

                ltImagens.Text = csDinamico.strInicioLista;

                foreach (DataRow dr in _dtImagens.Rows)
                {
                    if (dr[caArquivos.CC_Controle].ToString() != KuraFrameWork.csConstantes.sTpExcluido)
                    {
                        string strDivImagem = pnIndexImgEditar == seq ? csDinamico.strDivImagemEdit : csDinamico.strDivImagem;

                        if (seq != 0 && seq % 3 == 0)
                        {
                            ltImagens.Text += csDinamico.strFinalLista;
                            ltImagens.Text += csDinamico.strInicioLista;
                        }
                        if (Convert.ToInt32(dr[caArquivos.cdArquivo].ToString()) != 0)
                            strCaminhoImg = strCaminho;
                        else
                            strCaminhoImg = strCaminhoTemp;

                        ltImagens.Text += strDivImagem.Replace("[strCaminhoImagem]", strCaminhoImg + dr[caArquivos.nmArquivo].ToString()).Replace("[strDescImagem]",
                                dr[caArquivos.deArquivo].ToString()).Replace("[strSeqImagem]", (seq).ToString());

                        if (pnIndexImgEditar == seq)
                        {
                            strScriptImagens += "function lnkSalvar_" + seq.ToString() + "_Click() {" + "\n" +
                                "  __doPostBack('lnkSalvar_" + seq.ToString() + "', 'SalvarImagem;" + seq.ToString() + ";'+ document.getElementById('deArquivoEdit').value);" + "\n" +
                                "} \n";
                        }
                        else
                        {
                            strScriptImagens += "function lnkEditar_" + seq.ToString() + "_Click() {" + "\n" +
                                "  __doPostBack('lnkEditar_" + seq.ToString() + "', 'EditarImagem;" + seq.ToString() + "');" + "\n" +
                                "} \n";
                        }

                        strScriptImagens += "function lnkRemover_" + seq.ToString() + "_Click() {" + "\n" +
                            "  __doPostBack('lnkRemover_" + seq.ToString() + "', 'RemoveImagem;" + seq.ToString() + "');" + "\n" +
                            "} \n";
                    }

                    seq++;
                }

                Session["strScriptImagens"] = strScriptImagens;
                ltImagens.Text += csDinamico.strFinalLista;

                RegistrarScriptLoaded();
            }
        }

        protected void upArquivos_PreRender(object sender, EventArgs e)
        {
            this.ScriptManager1.RegisterPostBackControl(btnAdicionarArquivo);

            foreach (GridViewRow gvr in gvDocumentos.Rows)
            {
                LinkButton lnkEditDoc = gvr.FindControl("lnkEditDoc") as LinkButton;
                ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkEditDoc);
            }
        }

        protected void RemoveDocumento(int pintIndice)
        {
            _dtDocumentos = (DataTable)Session["_dtDocumentos"];
            _dtDocumentos.Rows[pintIndice][caArquivos.CC_Controle] = KuraFrameWork.csConstantes.sTpExcluido;

            if (Session["_dtDocumentosExc"] != null)
                _dtDocumentosExc = (DataTable)Session["_dtDocumentosExc"];
            else
                _dtDocumentosExc = conArquivos.objCo.RetornaEstruturaDT();

            _dtDocumentosExc.ImportRow(_dtDocumentos.Rows[pintIndice]);
            _dtDocumentos.Rows.Remove(_dtDocumentos.Rows[pintIndice]);

            Session["_dtDocumentosExc"] = _dtDocumentosExc;

            gvDocumentos.DataSource = _dtDocumentos;
            gvDocumentos.DataBind();

            Session["_dtDocumentos"] = _dtDocumentos;

            if (_dtDocumentos.Rows.Count > 0)
                ConfigurarGridView();
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "AtivaAbaArquivosDocumentos();", true);
        }

        protected void gvDocumentos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                RemoveDocumento(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void gvDocumentos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDeleteDoc");
                lnkDelete.CommandArgument = e.Row.RowIndex.ToString();
                lnkDelete.Attributes.Add("OnClick", "javascript:return " +
                    "confirm('O registro será removido!')");
                /*"confirm('O registro \"" + DataBinder.Eval(e.Row.DataItem, obj.dePrincipal) + "\" será removido!')");*/

                LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEditDoc");
                lnkEdit.CommandArgument = e.Row.RowIndex.ToString();

                this.ScriptManager1.RegisterPostBackControl(lnkEdit);
            }
        }

        protected void gvDocumentos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvDocumentos_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void lnkEditDoc_Click(object sender, EventArgs e)
        {
            _dtDocumentos = (DataTable)Session["_dtDocumentos"];
            int indexDocumento = Convert.ToInt32(((LinkButton)sender).CommandArgument);

            Session["indexDocumento"] = indexDocumento;
            ltTituloEdicao.Text = _dtDocumentos.Rows[indexDocumento][caArquivos.nmArquivo].ToString();
            deArquivoEdit.Text = _dtDocumentos.Rows[indexDocumento][caArquivos.deArquivo].ToString();

            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "AtivaEdicao();", true);
        }

        protected void btnConfirmarEdicao_Click(object sender, EventArgs e)
        {
            int indexDocumento = Convert.ToInt32(Session["indexDocumento"].ToString());
            _dtDocumentos = (DataTable)Session["_dtDocumentos"];
            _dtDocumentos.Rows[indexDocumento][caArquivos.CC_Controle] = KuraFrameWork.csConstantes.sAlterando;
            _dtDocumentos.Rows[indexDocumento][caArquivos.deArquivo] = deArquivoEdit.Text;

            gvDocumentos.DataSource = _dtDocumentos;
            gvDocumentos.DataBind();

            Session["_dtDocumentos"] = _dtDocumentos;

            if (_dtDocumentos.Rows.Count > 0)
                ConfigurarGridView();

            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "AtivaAbaArquivosDocumentos();", true);
        }
        
        /// <summary>
        /// Cantores Categorias
        /// </summary>
        private void CarregaDataTableOrdemCategoriasSession()
        {
            conConcursosOrdemCategorias objConOrdemCategoria = new conConcursosOrdemCategorias();
            _dtOrdemCategoria = objConOrdemCategoria.objCoConcursosOrdemCategorias.RetornaEstruturaDT();
            _dtOrdemCategoriaExc = objConOrdemCategoria.objCoConcursosOrdemCategorias.RetornaEstruturaDT();

            if (Session["_dtOrdemCategoria"] != null)
                _dtOrdemCategoria = (DataTable)Session["_dtOrdemCategoria"];

            if (Session["_dtOrdemCategoriaExc"] != null)
                _dtOrdemCategoriaExc = (DataTable)Session["_dtOrdemCategoriaExc"];
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

                    for (int k = 0; k < _dtCantoresConcurso.Rows.Count; k++)
                    {
                        if (Convert.ToInt32(_dtCantoresConcurso.Rows[k][caCantoresConcursos.cdCantor].ToString()) == cdCantorEdit)
                        {
                            Session["cdAssociacaoEdit"] = Convert.ToInt32(_dtCantoresConcurso.Rows[k][caCantoresConcursos.cdAssociacao].ToString());
                            Session["indexCantorConcurso"] = k;

                            cdAssociacaoEdit.SelectedValue = _dtCantoresConcurso.Rows[k][caCantoresConcursos.cdAssociacao].ToString();
                        }
                    }

                    ltMensagemInfoCantor.Text = MostraMensagem("**IMPORTANTE", "Ao alterar a Associação do cantor a mesma será alterada " +
                        "para as demais categorias que o mesmo estiver inscrito.", csMensagem.msgInfo);
                }
            }

            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "AtivaEdicaoCantor('" + cdCategoriaEdit + "');", true);
        }

        protected void btnConfirmarEdicaoCantor_Click(object sender, EventArgs e)
        {
            if (ValidarConcursoFechado())
                return;

            if (cdAssociacaoEdit.SelectedIndex > 0 && cdMusicaEdit.SelectedIndex > 0)
            {
                int indexCantorFase = Convert.ToInt32(Session["indexCantorFase"].ToString());
                int indexCantorConcurso = Convert.ToInt32(Session["indexCantorConcurso"].ToString());

                _dtCantoresFases = (DataTable)Session["_dtCantoresFases"];
                _dtCantoresFases.Rows[indexCantorFase][caCantoresFases.CC_Controle] = KuraFrameWork.csConstantes.sAlterando;
                _dtCantoresFases.Rows[indexCantorFase][caCantoresFases.cdMusica] = cdMusicaEdit.SelectedValue;

                _dtCantoresConcurso = (DataTable)Session["_dtCantoresConcurso"];
                _dtCantoresConcurso.Rows[indexCantorConcurso][caCantoresConcursos.CC_Controle] = KuraFrameWork.csConstantes.sAlterando;
                _dtCantoresConcurso.Rows[indexCantorConcurso][caCantoresConcursos.cdAssociacao] = cdAssociacaoEdit.SelectedValue;

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
                Session["_dtCantoresConcurso"] = _dtCantoresConcurso;

                PreencheLiteral(Session["cdCategoriaEdit"].ToString().ToString(), false, false);

                AtualizaCantoresCategorias();

                AtivaAbaCategoria(Session["cdCategoriaEdit"].ToString());
            }
            else
            {
                ltMensagemEdicaoCantor.Text = MostraMensagem("Validação!", "Selecione uma Associação e uma Música.", csMensagem.msgWarning);
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "AtivaEdicaoCantor('" + Session["cdCategoriaEdit"].ToString() + "');", true);
            }
        }

        private void RemoverCantor()
        {
            string[] strParam = Request["__EVENTARGUMENT"].Split(';');

            if (ValidarConcursoFechado())
            {
                AtivaAbaCategoria(strParam[1]);
                return;
            }

            if (Session["_dtCantoresFases"] != null)
                _dtCantoresFases = (DataTable)Session["_dtCantoresFases"];
            else
                _dtCantoresFases = conCantoresFases.objCo.RetornaEstruturaDT();

            if (Session["_dtCantoresFasesExc"] != null)
                _dtCantoresFasesExc = (DataTable)Session["_dtCantoresFasesExc"];
            else
                _dtCantoresFasesExc = conCantoresFases.objCo.RetornaEstruturaDT();

            foreach (DataRow dr in _dtCantoresFases.Rows)
            {
                if ((dr[caCantoresFases.cdCantor].ToString() == strParam[0].ToString()) &&
                    (dr[caCantoresFases.cdCategoria].ToString() == strParam[1].ToString()))
                {
                    dr[caCantoresFases.CC_Controle] = KuraFrameWork.csConstantes.sTpExcluido;

                    _dtCantoresFasesExc.ImportRow(dr);
                    _dtCantoresFases.Rows.Remove(dr);
                    break;
                }
            }

            Session["_dtCantoresFases"] = _dtCantoresFases;
            Session["_dtCantoresFasesExc"] = _dtCantoresFasesExc;

            RemoveCantorGvEspecifico(strParam[0], "dvCantores_" + strParam[1]);

            RemoveCategoria(strParam[1], "dvCantores_" + strParam[1]);

            RemoveCantorConcursoAssociacao(strParam[0]);

            AtualizaCantoresCategorias();

            AtivaAbaCategoria(strParam[1]);
        }

        private void AtivaAbaCategoria(string pcdCategoria)
        {
            if (!ExisteCategoria(pcdCategoria))
            {
                pcdCategoria = "";

                CarregaDataTableOrdemCategoriasSession();

                if (_dtOrdemCategoria.Rows.Count > 0)
                    pcdCategoria = _dtOrdemCategoria.Rows[0][caConcursosOrdemCategorias.cdCategoria].ToString();
            }

            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "AtivaAbaCantores(" + pcdCategoria + ");", true);
        }

        private void RemoveCantorGvEspecifico(string pcdCantor, string psNomeGridView)
        {
            if (Session[psNomeGridView] != null)
            {
                dtDados = (DataTable)Session[psNomeGridView];

                foreach (DataRow dr in dtDados.Rows)
                {
                    if (dr[caCantoresFases.cdCantor].ToString() == pcdCantor)
                    {
                        dtDados.Rows.Remove(dr);
                        Session[psNomeGridView] = dtDados;
                        return;
                    }
                }
            }
        }

        private void RemoveCantorConcursoAssociacao(string pcdCantor)
        {
            if (ValidarConcursoFechado())
                return;

            if (!ExisteCantorFase(pcdCantor))
            {
                if (Session["_dtCantoresConcurso"] != null)
                    _dtCantoresConcurso = (DataTable)Session["_dtCantoresConcurso"];
                else
                    _dtCantoresConcurso = conCantoresConcursos.objCo.RetornaEstruturaDT();

                if (Session["_dtCantoresConcursoExc"] != null)
                    _dtCantoresConcursoExc = (DataTable)Session["_dtCantoresConcursoExc"];
                else
                    _dtCantoresConcursoExc = conCantoresConcursos.objCo.RetornaEstruturaDT();

                foreach (DataRow dr in _dtCantoresConcurso.Rows)
                {
                    if (dr[caCantoresConcursos.cdCantor].ToString() == pcdCantor.ToString())
                    {
                        dr[caCantoresConcursos.CC_Controle] = KuraFrameWork.csConstantes.sTpExcluido;

                        _dtCantoresConcursoExc.ImportRow(dr);
                        _dtCantoresConcurso.Rows.Remove(dr);

                        Session["_dtCantoresConcurso"] = _dtCantoresConcurso;
                        Session["_dtCantoresConcursoExc"] = _dtCantoresConcursoExc;
                        return;
                    }
                }
            }
        }

        private void RemanejaOrdemApresentacao()
        {
            foreach (DataRow dr in _dtOrdemCategoria.Rows)
            {
                dr[caConcursosOrdemCategorias.nuOrdem] = _dtOrdemCategoria.Rows.IndexOf(dr) + 1;
            }
        }

        private void RemoveCategoria(string pcdCategoria, string psNomeGridView)
        {
            if (ValidarConcursoFechado())
                return;

            if (!ExisteCategoria(pcdCategoria))
            {
                CarregaDataTableOrdemCategoriasSession();

                foreach (DataRow dr in _dtOrdemCategoria.Rows)
                {
                    if (dr[caConcursosOrdemCategorias.cdCategoria].ToString() == pcdCategoria)
                    {
                        dr[caConcursosOrdemCategorias.CC_Controle] = KuraFrameWork.csConstantes.sExcluindo;

                        _dtOrdemCategoriaExc.ImportRow(dr);
                        _dtOrdemCategoria.Rows.Remove(dr);

                        Session[psNomeGridView] = null;
                        OrdenaDataTable(ref _dtOrdemCategoria, caConcursosOrdemCategorias.nuOrdem + KuraFrameWork.csConstantes.sCrescente);

                        RemanejaOrdemApresentacao();

                        Session["_dtOrdemCategoria"] = _dtOrdemCategoria;
                        Session["_dtOrdemCategoriaExc"] = _dtOrdemCategoriaExc;

                        gvOrdemApres.DataSource = _dtOrdemCategoria;
                        gvOrdemApres.DataBind();

                        return;
                    }
                }
            }
        }

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

        private bool ExisteCategoria(string pcdCategoria)
        {
            if (Session["_dtCantoresFases"] != null)
                _dtCantoresFases = (DataTable)Session["_dtCantoresFases"];

            foreach (DataRow dr in _dtCantoresFases.Rows)
            {
                if (dr[caCantoresFases.cdCategoria].ToString() == pcdCategoria)
                    return true;
            }
            
            return false;
        }

        private bool ExisteCantorFase(string pcdCantor)
        {
            if (Session["_dtCantoresFases"] != null)
                _dtCantoresFases = (DataTable)Session["_dtCantoresFases"];
            else
                _dtCantoresFases = conCantoresFases.objCo.RetornaEstruturaDT();

            foreach (DataRow dr in _dtCantoresFases.Rows)
            {
                if (dr[caCantoresFases.cdCantor].ToString() == pcdCantor)
                    return true;
            }

            return false;
        }

        private void CarregarCantoresCategorias()
        {
            conCantoresConcursos objConCantoresConcursos = new conCantoresConcursos();
            objConCantoresConcursos.objCoCantoresConcursos.LimparAtributos();
            objConCantoresConcursos.objCoCantoresConcursos.cdConcurso = Convert.ToInt32(Session["cdConcurso"].ToString());

            if (!conCantoresConcursos.Select())
            {
                ltMensagensCategorias.Text = MostraMensagem("Falha!", "Falha ao carregar os cantores do concurso.", csMensagem.msgDanger);
                return;
            }

            _dtCantoresConcurso = objConCantoresConcursos.dtDados;
            Session["_dtCantoresConcurso"] = _dtCantoresConcurso;

            conCantoresFases objConCantoresFases = new conCantoresFases();
            objConCantoresFases.objCoCantoresFases.LimparAtributos();
            objConCantoresFases.objCoCantoresFases.cdConcurso = Convert.ToInt32(Session["cdConcurso"].ToString());
            objConCantoresFases.objCoCantoresFases.cdFase = Convert.ToInt32(wappKaraoke.Properties.Settings.Default.sCodStatusInicial);

            if (!conCantoresFases.Select())
            {
                ltMensagensCategorias.Text = MostraMensagem("Falha!", "Falha ao carregar as categorias do concurso.", csMensagem.msgDanger);
                return;
            }

            _dtCantoresFases = objConCantoresFases.dtDados;
            Session["_dtCantoresFases"] = _dtCantoresFases;

            CarregarCategorias();

            RegistrarScript();
        }

        private void CarregarCategorias()
        {
            conConcursosOrdemCategorias objConOrdemCategorias = new conConcursosOrdemCategorias();
            objConOrdemCategorias.objCoConcursosOrdemCategorias.LimparAtributos();
            objConOrdemCategorias.objCoConcursosOrdemCategorias.cdConcurso = Convert.ToInt32(Session["cdConcurso"].ToString());

            if (!conConcursosOrdemCategorias.Select())
            {
                ltMensagensCategorias.Text = MostraMensagem("Falha!", "Falha ao carregar as categorias do concurso.", csMensagem.msgDanger);
                return;
            }

            _dtOrdemCategoria = objConOrdemCategorias.dtDados;
            OrdenaDataTable(ref _dtOrdemCategoria, caConcursosOrdemCategorias.nuOrdem + KuraFrameWork.csConstantes.sCrescente);
            Session["_dtOrdemCategoria"] = _dtOrdemCategoria;

            MontaCantoresCategorias(true, false);

            gvOrdemApres.DataSource = _dtOrdemCategoria;
            gvOrdemApres.DataBind();
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

        public void btnAdicionarCategoria_OnClick(Object sender, EventArgs e)
        {
            if (ValidarConcursoFechado())
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "AtivaAbaCantores(" + cdCategoria.SelectedValue.ToString() + ");", true);
                return;
            }

            ltMensagensCategorias.Text = "";

            if (cdAssociacaoCantor.SelectedIndex == 0)
            {
                ltMensagensCategorias.Text = MostraMensagem("Validação!", "Deve ser selecionada a Associação.", csMensagem.msgWarning);
                return;
            }
            else if (cdCategoria.SelectedIndex == 0)
            {
                ltMensagensCategorias.Text = MostraMensagem("Validação!", "Deve ser selecionada a Categoria.", csMensagem.msgWarning);
                return;
            }
            else if (cdCantor.SelectedIndex == 0)
            {
                ltMensagensCategorias.Text = MostraMensagem("Validação!", "Deve ser selecionado o Cantor.", csMensagem.msgWarning);
                return;
            }
            else if (cdMusica.SelectedIndex == 0)
            {
                ltMensagensCategorias.Text = MostraMensagem("Validação!", "Deve ser selecionada a Música.", csMensagem.msgWarning);
                return;
            }
            else
            {
                AdicionaCantorCategoriaConcurso(cdCategoria.SelectedValue.ToString(), cdCategoria.SelectedItem.ToString());
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "AtivaAbaCantores(" + cdCategoria.SelectedValue.ToString() + ");", true);
                ConfigurarGridView();
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

                gvOrdemApres.DataSource = _dtOrdemCategoria;
                gvOrdemApres.DataBind();
            }
        }

        private bool ExisteCantorAssociacao()
        {
            if (Session["_dtCantoresConcurso"] != null)
                _dtCantoresConcurso = (DataTable)Session["_dtCantoresConcurso"];
            else
                _dtCantoresConcurso = conCantoresConcursos.objCo.RetornaEstruturaDT();

            foreach (DataRow dr in _dtCantoresConcurso.Rows)
            {
                if ((dr[caCantoresConcursos.cdCantor].ToString() == cdCantor.SelectedValue) &&
                    (dr[caCantoresConcursos.cdAssociacao].ToString() == cdAssociacaoCantor.SelectedValue))
                    return true;
            }

            return false;
        }

        private void AdicionaCantorFaseCategoria()
        {
            DataRow dr;

            if (!ExisteCantorAssociacao())
            {
                if (Session["_dtCantoresConcurso"] != null)
                    _dtCantoresConcurso = (DataTable)Session["_dtCantoresConcurso"];
                else
                    _dtCantoresConcurso = conCantoresConcursos.objCo.RetornaEstruturaDT();

                dr = _dtCantoresConcurso.NewRow();
                dr[caCantoresConcursos.CC_Controle] = KuraFrameWork.csConstantes.sInserindo;
                dr[caCantoresConcursos.cdConcurso] = Convert.ToInt32(Session["cdConcurso"].ToString());
                dr[caCantoresConcursos.cdAssociacao] = cdAssociacaoCantor.SelectedValue;
                dr[caCantoresConcursos.cdCantor] = cdCantor.SelectedValue;
                _dtCantoresConcurso.Rows.Add(dr);

                Session["_dtCantoresConcurso"] = _dtCantoresConcurso;
            }

            if (Session["_dtCantoresFases"] != null)
                _dtCantoresFases = (DataTable)Session["_dtCantoresFases"];
            else
                _dtCantoresFases = conCantoresFases.objCo.RetornaEstruturaDT();

            dr = _dtCantoresFases.NewRow();
            dr[caCantoresFases.CC_Controle] = KuraFrameWork.csConstantes.sInserindo; 
            dr[caCantoresFases.cdConcurso] = Convert.ToInt32(Session["cdConcurso"].ToString());
            dr[caCantoresFases.cdCategoria] = cdCategoria.SelectedValue;
            dr[caCantoresFases.cdCantor] = cdCantor.SelectedValue;
            dr[caCantoresFases.cdFase] = wappKaraoke.Properties.Settings.Default.sCodFaseInicial;
            dr[caCantoresFases.cdMusica] = cdMusica.SelectedValue;
            dr[caCantoresFases.cdTpStatus] = wappKaraoke.Properties.Settings.Default.sCodStatusInicial;
            dr[caCantoresFases.nuOrdemApresentacao] = 0;
            _dtCantoresFases.Rows.Add(dr);

            Session["_dtCantoresFases"] = _dtCantoresFases;
        }

        private bool ExisteCantorCategoria()
        {
            if (Session["_dtCantoresFases"] != null)
                _dtCantoresFases = (DataTable)Session["_dtCantoresFases"];
            else
                _dtCantoresFases = conCantoresFases.objCo.RetornaEstruturaDT();

            foreach (DataRow dr in _dtCantoresFases.Rows)
            {
                if ((dr[caCantoresFases.cdCantor].ToString() == cdCantor.SelectedValue) && 
                    (dr[caCantoresFases.cdCategoria].ToString() == cdCategoria.SelectedValue))
                    return true;
            }

            return false;
        }

        private bool CantorAssociacaoDivergente()
        {
            if (Session["_dtCantoresConcurso"] != null)
                _dtCantoresConcurso = (DataTable)Session["_dtCantoresConcurso"];
            else
                _dtCantoresConcurso = conCantoresConcursos.objCo.RetornaEstruturaDT();

            foreach (DataRow dr in _dtCantoresConcurso.Rows)
            {
                if ((dr[caCantoresConcursos.cdCantor].ToString() == cdCantor.SelectedValue) && 
                    (dr[caCantoresConcursos.cdAssociacao].ToString() != cdAssociacaoCantor.SelectedValue))
                    return true;
            }

            return false;
        }

        private string RetornaNomeCantorKanji()
        {
            conCantores objConCantores = new conCantores();
            objConCantores.objCoCantores.LimparAtributos();
            objConCantores.objCoCantores.cdCantor = Convert.ToInt32(cdCantor.SelectedValue);

            if(conCantores.Select())
                return objConCantores.dtDados.Rows[0][caCantores.nmNomeKanji].ToString();

            return "";
        }

        private string RetornaNomeMusicaKanji()
        {
            conMusicas objConMusicas = new conMusicas();
            objConMusicas.objCoMusicas.LimparAtributos();
            objConMusicas.objCoMusicas.cdMusica = Convert.ToInt32(cdMusica.SelectedValue);

            if (conMusicas.Select())
                return objConMusicas.dtDados.Rows[0][caMusicas.nmMusicaKanji].ToString();

            return "";
        }

        private void PreencheLiteral(string strCdCategoria, bool bEstaCarregando = false,  bool bEstaInserindo = true)
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
                                        Convert.ToInt32(wappKaraoke.Properties.Settings.Default.sCodStatusInicial);
                                    objConCantoresFases.objCoCantoresFases.cdCategoria = 
                                        Convert.ToInt32(_dtOrdemCategoria.Rows[i][caConcursosOrdemCategorias.cdCategoria].ToString());

                                    if (!conCantoresFases.SelectCantoresCategoriasConcurso())
                                    {
                                        ltMensagensCategorias.Text = MostraMensagem("Falha!", "Falha ao carregar os cantores do concurso.", csMensagem.msgDanger);
                                        return;
                                    }

                                    if (objConCantoresFases.dtDados != null)
                                        dtDados = objConCantoresFases.dtDados;
                                }
                                else if (bEstaInserindo)
                                {
                                    if (_dtOrdemCategoria.Rows[i][caConcursosOrdemCategorias.cdCategoria].ToString() == strCdCategoria)
                                    {
                                        if (ExisteCantorCategoria())
                                        {
                                            ltMensagensCategorias.Text = MostraMensagem("Validação!", "Cantor já adicionado para esta categoria.", 
                                                csMensagem.msgWarning);
                                            return;
                                        }

                                        if (CantorAssociacaoDivergente())
                                        {
                                            ltMensagensCategorias.Text = MostraMensagem("Validação!", "Cantor relacionado à outra associação neste concurso.", 
                                                csMensagem.msgWarning);
                                            return;
                                        }                                        

                                        DataRow dr = dtDados.NewRow();
                                        dr[caConcursos.cdConcurso] = Convert.ToInt32(Session["cdConcurso"].ToString());
                                        //Cantor
                                        dr[caCantores.cdCantor] = cdCantor.SelectedValue;
                                        dr[caCantores.nmCantor] = cdCantor.SelectedItem.Text;
                                        dr[caCantores.nmNomeKanji] = RetornaNomeCantorKanji();
                                        //Associação
                                        dr[caAssociacoes.cdAssociacao] = cdAssociacaoCantor.SelectedValue;
                                        dr[caAssociacoes.nmAssociacao] = cdAssociacaoCantor.SelectedItem.Text;
                                        //Música
                                        dr[caMusicas.cdMusica] = cdMusica.SelectedValue;
                                        dr[caMusicas.nmMusica] = cdMusica.SelectedItem.Text;
                                        dr[caMusicas.nmMusicaKanji] = RetornaNomeMusicaKanji();
                                        //Fase
                                        dr[caFases.cdFase] = wappKaraoke.Properties.Settings.Default.sCodFaseInicial;
                                        dr[caCategorias.cdCategoria] = cdCategoria.SelectedValue;
                                        dr[caTipoStatus.cdTpStatus] = wappKaraoke.Properties.Settings.Default.sCodStatusInicial;
                                        dr[caCantoresFases.nuCantor] = "";
                                        dr[caCantoresFases.nuOrdemApresentacao] = 0;
                                        dtDados.Rows.Add(dr);

                                        AdicionaCantorFaseCategoria();
                                    }
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

        protected void upCantoresCategorias_PreRender(object sender, EventArgs e)
        {
            this.ScriptManager1.RegisterPostBackControl(btnAdicionarAssociacao);

            foreach (GridViewRow gvr in gvAssociacoes.Rows)
            {
                LinkButton lnkDeleteAss = gvr.FindControl("lnkDeleteAss") as LinkButton;
                ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkDeleteAss);
            }
        }

        protected void upOrdemApres_PreRender(object sender, EventArgs e)
        {
            this.ScriptManager1.RegisterPostBackControl(btnAdicionarCategoria);
        }

        /// <summary>
        /// Jurados
        /// </summary>
        private void CarregarGruposJurados()
        {
            conGrupos objConGrupos = new conGrupos();
            objConGrupos.objCoGrupos.LimparAtributos();
            objConGrupos.objCoGrupos.cdConcurso = Convert.ToInt32(Session["cdConcurso"].ToString());

            if (!conGrupos.Select())
            {
                ltMensagemJurados.Text = MostraMensagem("Falha", "Problemas ao carregar Grupos/Jurados.", csMensagem.msgDanger);
                return;
            }

            gvGrupoJuradoConcurso.DataSource = objConGrupos.dtDados;
            gvGrupoJuradoConcurso.DataBind();

            Session["_dtGruposJurados"] = objConGrupos.dtDados;

            if (objConGrupos.dtDados.Rows.Count > 0)
                ConfigurarGridView();
        }

        protected void btnAdicionarGrupoJurado_Click(object sender, EventArgs e)
        {
            if (ValidarConcursoFechado())
                return;

            ltMensagemJurados.Text = "";

            if (cdJurado.SelectedIndex > 0)
            {
                if (deGrupo.Text.Trim() != "")
                {
                    if (Session["_dtGruposJurados"] != null)
                    {
                        _dtGruposJurados = (DataTable)Session["_dtGruposJurados"];
                    }

                    if (VaidaRegistroExistente(_dtGruposJurados, cdJurado.SelectedValue, caGrupos.cdJurado))
                    {
                        ltMensagemJurados.Text = MostraMensagem("Validação!", "Jurado selecionado já inserido.", csMensagem.msgWarning);
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "AtivaAbaJurados();", true);
                        return;
                    }

                    conJurados objConJurados = new conJurados();
                    objConJurados.objCoJurados.LimparAtributos();
                    objConJurados.objCoJurados.cdJurado = Convert.ToInt32(cdJurado.SelectedValue);

                    if (!conJurados.Select())
                    {
                        ltMensagemJurados.Text = MostraMensagem("Falha", "Problemas ao carregar dados do Jurado.", csMensagem.msgDanger);
                        return;
                    }

                    _dtGruposJurados = (DataTable)Session["_dtGruposJurados"];
                    DataRow dr = _dtGruposJurados.NewRow();

                    dr[caGrupos.CC_Controle] = KuraFrameWork.csConstantes.sInserindo;
                    dr[caGrupos.cdJurado] = cdJurado.SelectedValue;
                    dr[caGrupos.cdConcurso] = Convert.ToInt32(Session["cdConcurso"].ToString());
                    dr[caGrupos.deGrupo] = deGrupo.Text;
                    dr[caGrupos.CC_nmJurado] = objConJurados.dtDados.Rows[0][caJurados.nmJurado].ToString();
                    dr[caGrupos.CC_nmNomeKanji] = objConJurados.dtDados.Rows[0][caJurados.nmNomeKanji].ToString();

                    _dtGruposJurados.Rows.Add(dr);

                    gvGrupoJuradoConcurso.DataSource = _dtGruposJurados;
                    gvGrupoJuradoConcurso.DataBind();

                    Session["_dtGruposJurados"] = _dtGruposJurados;
                    ConfigurarGridView();
                }
                else
                    ltMensagemJurados.Text = MostraMensagem("Validação!", "Deve ser informado o Grupo do Jurado.", csMensagem.msgWarning);
            }
            else
                ltMensagemJurados.Text = MostraMensagem("Validação!", "Deve ser selecionado o Jurado.", csMensagem.msgWarning);
        }

        protected void RemoveJurados(int pintIndice)
        {
            if (ValidarConcursoFechado())
                return;

            _dtGruposJurados = (DataTable)Session["_dtGruposJurados"];
            _dtGruposJurados.Rows[pintIndice][caGrupos.CC_Controle] = KuraFrameWork.csConstantes.sTpExcluido;

            if (Session["_dtGruposJuradosExc"] != null)
                _dtGruposJuradosExc = (DataTable)Session["_dtGruposJuradosExc"];
            else
                _dtGruposJuradosExc = conGrupos.objCo.RetornaEstruturaDT();

            _dtGruposJuradosExc.ImportRow(_dtGruposJurados.Rows[pintIndice]);
            _dtGruposJurados.Rows.Remove(_dtGruposJurados.Rows[pintIndice]);

            Session["_dtGruposJuradosExc"] = _dtGruposJuradosExc;

            gvGrupoJuradoConcurso.DataSource = _dtGruposJurados;
            gvGrupoJuradoConcurso.DataBind();

            Session["_dtGruposJurados"] = _dtGruposJurados;

            if (_dtGruposJurados.Rows.Count > 0)
                ConfigurarGridView();
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "AtivaAbaJurados();", true);
        }

        protected void gvGrupoJuradoConcurso_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                RemoveJurados(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void gvGrupoJuradoConcurso_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDeleteJur");
                lnkDelete.CommandArgument = e.Row.RowIndex.ToString();
                lnkDelete.Attributes.Add("OnClick", "javascript:return " +
                    "confirm('O registro será removido!')");
                /*"confirm('O registro \"" + DataBinder.Eval(e.Row.DataItem, obj.dePrincipal) + "\" será removido!')");*/

                LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEditJur");
                lnkEdit.CommandArgument = e.Row.RowIndex.ToString();

                this.ScriptManager1.RegisterPostBackControl(lnkEdit);
            }
        }

        protected void gvGrupoJuradoConcurso_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvGrupoJuradoConcurso_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void lnkEditJur_Click(object sender, EventArgs e)
        {
            _dtGruposJurados = (DataTable)Session["_dtGruposJurados"];
            int indexJurado = Convert.ToInt32(((LinkButton)sender).CommandArgument);

            Session["indexJurado"] = indexJurado;
            ltTituloEdicaoJurado.Text = _dtGruposJurados.Rows[indexJurado][caGrupos.CC_nmJurado].ToString() + "<br/>"
                + _dtGruposJurados.Rows[indexJurado][caGrupos.CC_nmNomeKanji].ToString();
            deGrupoEdit.Text = _dtGruposJurados.Rows[indexJurado][caGrupos.deGrupo].ToString();

            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "AtivaEdicaoJur();", true);
        }

        protected void btnConfirmarEdicaoJur_Click(object sender, EventArgs e)
        {
            if (ValidarConcursoFechado())
                return;

            if (deGrupoEdit.Text.Trim() != "")
            {
                int indexJurado = Convert.ToInt32(Session["indexJurado"].ToString());
                _dtGruposJurados = (DataTable)Session["_dtGruposJurados"];
                _dtGruposJurados.Rows[indexJurado][caGrupos.CC_Controle] = KuraFrameWork.csConstantes.sAlterando;
                _dtGruposJurados.Rows[indexJurado][caGrupos.deGrupo] = deGrupoEdit.Text;

                gvGrupoJuradoConcurso.DataSource = _dtGruposJurados;
                gvGrupoJuradoConcurso.DataBind();

                Session["_dtGruposJurados"] = _dtGruposJurados;

                if (_dtGruposJurados.Rows.Count > 0)
                    ConfigurarGridView();

                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "AtivaAbaJurados();", true);
            }
            else
            {
                ltMensagemEdicaoJur.Text = MostraMensagem("Validação!", "Preenha a descrição do Grupo do Jurado.", csMensagem.msgWarning);
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "AtivaEdicaoJur();", true);
            }
        }

        protected void upConcursoJurados_PreRender(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in gvGrupoJuradoConcurso.Rows)
            {
                LinkButton lnkEditJur = gvr.FindControl("lnkEditJur") as LinkButton;
                ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkEditJur);
            }
        }

        /// <summary>
        /// Associacoes
        /// </summary>
        private void CarregarAssociacoes()
        {
            if (Session["_dtAssociacoes"] != null)
                _dtAssociacoes = (DataTable)Session["_dtAssociacoes"];

            conConcursosAssociacoes objConConcursosAssociacoes = new conConcursosAssociacoes();
            objConConcursosAssociacoes.objCoConcursosAssociacoes.LimparAtributos();
            objConConcursosAssociacoes.objCoConcursosAssociacoes.cdConcurso = Convert.ToInt32(Session["cdConcurso"].ToString());

            if (!conConcursosAssociacoes.Select())
            {
                ltMensagemAssociacoes.Text = MostraMensagem("Falha", "Problemas ao carregar Associações.", csMensagem.msgDanger);
                return;
            }

            _dtAssociacoes = objConConcursosAssociacoes.dtDados;

            gvAssociacoes.DataSource = _dtAssociacoes;
            gvAssociacoes.DataBind();

            Session["_dtAssociacoes"] = _dtAssociacoes;
            ConfigurarGridView();
        }

        protected void btnAdicionarAssociacao_Click(object sender, EventArgs e)
        {
            if (ValidarConcursoFechado())
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "AtivaAbaAssociacoes();", true);
                return;
            }

            ltMensagemAssociacoes.Text = "";

            if (cdAssociacao.SelectedIndex > 0)
            {
                if (nmRepresentante.Text.Trim() != "")
                {
                    if (deEmail.Text.Trim() != "")
                    {
                        if (Session["_dtAssociacoes"] != null)
                            _dtAssociacoes = (DataTable)Session["_dtAssociacoes"];
                        else
                            _dtAssociacoes = conConcursosAssociacoes.objCo.RetornaEstruturaDT();

                        if (VaidaRegistroExistente(_dtAssociacoes, cdAssociacao.SelectedValue, caAssociacoes.cdAssociacao))
                        {
                            ltMensagemAssociacoes.Text = MostraMensagem("Validação!", "Associação selecionada já inserida.", csMensagem.msgWarning);
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "AtivaAbaAssociacoes();", true);
                            return;
                        }

                        DataRow dr = _dtAssociacoes.NewRow();

                        dr[caConcursosAssociacoes.CC_Controle] = KuraFrameWork.csConstantes.sInserindo;
                        dr[caConcursosAssociacoes.cdAssociacao] = cdAssociacao.SelectedValue;
                        dr[caConcursosAssociacoes.cdConcurso] = Convert.ToInt32(Session["cdConcurso"].ToString());
                        dr[caConcursosAssociacoes.deEmail] = deEmail.Text;
                        dr[caConcursosAssociacoes.nmRepresentante] = nmRepresentante.Text;
                        dr[caConcursosAssociacoes.CC_nmAssociacao] = cdAssociacao.SelectedItem.ToString();

                        _dtAssociacoes.Rows.Add(dr);

                        gvAssociacoes.DataSource = _dtAssociacoes;
                        gvAssociacoes.DataBind();

                        Session["_dtAssociacoes"] = _dtAssociacoes;
                        ConfigurarGridView();

                        CarregaDDLAssociacoesCantores();
                    }
                    else
                        ltMensagemAssociacoes.Text = MostraMensagem("Validação!", "Informe o e-mail do Representante.", csMensagem.msgWarning);
                }
                else
                    ltMensagemAssociacoes.Text = MostraMensagem("Validação!", "Informe o nome do Representante.", csMensagem.msgWarning);
            }
            else
                ltMensagemAssociacoes.Text = MostraMensagem("Validação!", "Deve ser selecionada a Associação.", csMensagem.msgWarning);

            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "AtivaAbaAssociacoes();", true);
        }

        private bool ExisteCantorAssociacao(string pscdAssociacao)
        {
            if (Session["_dtCantoresConcurso"] != null)
            {
                _dtCantoresConcurso = (DataTable)Session["_dtCantoresConcurso"];
                foreach (DataRow dr in _dtCantoresConcurso.Rows)
                {
                    if (dr[caCantoresConcursos.cdAssociacao].ToString() == pscdAssociacao)
                        return true;
                }
            }
            return false;
        }

        protected void RemoveAssociacao(int pintIndice)
        {
            if (ValidarConcursoFechado())
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "AtivaAbaAssociacoes();", true);
                return;
            }

            ltMensagemAssociacoes.Text = "";

            _dtAssociacoes = (DataTable)Session["_dtAssociacoes"];

            if (ExisteCantorAssociacao(_dtAssociacoes.Rows[pintIndice][caConcursosAssociacoes.cdAssociacao].ToString()))
            {
                ltMensagemAssociacoes.Text = MostraMensagem("Validação!", "Existe(m) cantor(es) representando esta associação. " +
                    "Altere a associação dos cantores ou os remova.", csMensagem.msgWarning);
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "AtivaAbaAssociacoes();", true);
                return;
            }

            _dtAssociacoes.Rows[pintIndice][caConcursosAssociacoes.CC_Controle] = KuraFrameWork.csConstantes.sTpExcluido;

            if (Session["_dtAssociacoesExc"] != null)
                _dtAssociacoesExc = (DataTable)Session["_dtAssociacoesExc"];
            else
                _dtAssociacoesExc = conConcursosAssociacoes.objCo.RetornaEstruturaDT();

            _dtAssociacoesExc.ImportRow(_dtAssociacoes.Rows[pintIndice]);
            _dtAssociacoes.Rows.Remove(_dtAssociacoes.Rows[pintIndice]);

            Session["_dtAssociacoesExc"] = _dtAssociacoesExc;

            gvAssociacoes.DataSource = _dtAssociacoes;
            gvAssociacoes.DataBind();

            Session["_dtAssociacoes"] = _dtAssociacoes;

            if (_dtAssociacoes.Rows.Count > 0)
                ConfigurarGridView();

            CarregaDDLAssociacoesCantores();
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "AtivaAbaAssociacoes();", true);
        }

        protected void gvAssociacoes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                RemoveAssociacao(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void gvAssociacoes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDeleteAss");
                lnkDelete.CommandArgument = e.Row.RowIndex.ToString();
                lnkDelete.Attributes.Add("OnClick", "javascript:return " +
                    "confirm('O registro será removido!')");
                /*"confirm('O registro \"" + DataBinder.Eval(e.Row.DataItem, obj.dePrincipal) + "\" será removido!')");*/

                LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEditAss");
                lnkEdit.CommandArgument = e.Row.RowIndex.ToString();

                this.ScriptManager1.RegisterPostBackControl(lnkEdit);
            }
        }

        protected void gvAssociacoes_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvAssociacoes_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void lnkEditAss_Click(object sender, EventArgs e)
        {
            _dtAssociacoes = (DataTable)Session["_dtAssociacoes"];
            int indexAssociacao = Convert.ToInt32(((LinkButton)sender).CommandArgument);

            Session["indexAssociacao"] = indexAssociacao;
            ltTituloEdicaoAssociacao.Text = _dtAssociacoes.Rows[indexAssociacao][caConcursosAssociacoes.CC_nmAssociacao].ToString();
            nmRepresentanteEdit.Text = _dtAssociacoes.Rows[indexAssociacao][caConcursosAssociacoes.nmRepresentante].ToString();
            deEmailRepresentanteEdit.Text = _dtAssociacoes.Rows[indexAssociacao][caConcursosAssociacoes.deEmail].ToString();

            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "AtivaEdicaoAss();", true);
        }

        protected void btnConfirmarEdicaoAss_Click(object sender, EventArgs e)
        {
            if (ValidarConcursoFechado())
                return;

            if (nmRepresentanteEdit.Text.Trim() != "")
            {
                if (deEmailRepresentanteEdit.Text.Trim() != "")
                {
                    int indexAssociacao = Convert.ToInt32(Session["indexAssociacao"].ToString());
                    _dtAssociacoes = (DataTable)Session["_dtAssociacoes"];
                    _dtAssociacoes.Rows[indexAssociacao][caConcursosAssociacoes.CC_Controle] = KuraFrameWork.csConstantes.sAlterando;
                    _dtAssociacoes.Rows[indexAssociacao][caConcursosAssociacoes.nmRepresentante] = nmRepresentanteEdit.Text;
                    _dtAssociacoes.Rows[indexAssociacao][caConcursosAssociacoes.deEmail] = deEmailRepresentanteEdit.Text;

                    gvAssociacoes.DataSource = _dtAssociacoes;
                    gvAssociacoes.DataBind();

                    Session["_dtAssociacoes"] = _dtAssociacoes;

                    if (_dtAssociacoes.Rows.Count > 0)
                        ConfigurarGridView();

                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "AtivaAbaAssociacoes();", true);
                }
                else
                {
                    ltMensagemEdicaoAss.Text = MostraMensagem("Validação!", "Informe o e-mail do Representante.", csMensagem.msgWarning);
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "AtivaEdicaoAss();", true);
                }
            }
            else
            {
                ltMensagemEdicaoAss.Text = MostraMensagem("Validação!", "Informe o nome do Representante.", csMensagem.msgWarning);
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "AtivaEdicaoAss();", true);
            }
        }

        protected void upConcursoAssociacoes_PreRender(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in gvAssociacoes.Rows)
            {
                LinkButton lnkEditAss = gvr.FindControl("lnkEditAss") as LinkButton;
                ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkEditAss);
            }
        }

        protected void cdAssociacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Carregar dados
            conAssociacoes objConAssociacoes = new conAssociacoes();
            objConAssociacoes.objCoAssociacoes.LimparAtributos();
            objConAssociacoes.objCoAssociacoes.cdAssociacao = Convert.ToInt32(cdAssociacao.SelectedValue);

            if (!conAssociacoes.Select())
            {
                ltMensagemAssociacoes.Text = MostraMensagem("Falha!", "Não foi possível carregar os dados do representante.", csMensagem.msgWarning);
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "AtivaAbaAssociacoes();", true);
            }

            nmRepresentante.Text = objConAssociacoes.dtDados.Rows[0][caAssociacoes.nmRepresentante].ToString();
        }

        /// <summary>
        /// Ordem de apresentação
        /// </summary>
        protected void gvOrdemApres_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
        }

        protected void gvOrdemApres_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkUpCategoria = (LinkButton)e.Row.FindControl("lnkUpCategoria");
                lnkUpCategoria.CommandArgument = e.Row.RowIndex.ToString();

                LinkButton lnkDownCategoria = (LinkButton)e.Row.FindControl("lnkDownCategoria");
                lnkDownCategoria.CommandArgument = e.Row.RowIndex.ToString();

                this.ScriptManager1.RegisterPostBackControl(lnkUpCategoria);
                this.ScriptManager1.RegisterPostBackControl(lnkDownCategoria);
            }
        }

        protected void gvOrdemApres_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvOrdemApres_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void lnkUpCategoria_Click(object sender, EventArgs e)
        {
            if (ValidarConcursoFechado())
                return;

            _dtOrdemCategoria = (DataTable)Session["_dtOrdemCategoria"];
            int indexCategoria = Convert.ToInt32(((LinkButton)sender).CommandArgument);

            if (indexCategoria > 0)
            {
                _dtOrdemCategoria.Rows[indexCategoria][caConcursosOrdemCategorias.nuOrdem] =
                    Convert.ToInt32(_dtOrdemCategoria.Rows[indexCategoria][caConcursosOrdemCategorias.nuOrdem].ToString()) - 1;

                _dtOrdemCategoria.Rows[indexCategoria - 1][caConcursosOrdemCategorias.nuOrdem] =
                    Convert.ToInt32(_dtOrdemCategoria.Rows[indexCategoria][caConcursosOrdemCategorias.nuOrdem].ToString()) + 1;

                OrdenaDataTable(ref _dtOrdemCategoria, caConcursosOrdemCategorias.nuOrdem + KuraFrameWork.csConstantes.sCrescente);
                Session["_dtOrdemCategoria"] = _dtOrdemCategoria;

                gvOrdemApres.DataSource = _dtOrdemCategoria;
                gvOrdemApres.DataBind();

                MontaCantoresCategorias(false, false);
                ConfigurarGridView();
            }

            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "AtivaAbaOrdemApres();", true);
        }

        protected void lnkDownCategoria_Click(object sender, EventArgs e)
        {
            if (ValidarConcursoFechado())
                return;

            _dtOrdemCategoria = (DataTable)Session["_dtOrdemCategoria"];
            int indexCategoria = Convert.ToInt32(((LinkButton)sender).CommandArgument);

            if (indexCategoria < _dtOrdemCategoria.Rows.Count - 1)
            {
                _dtOrdemCategoria.Rows[indexCategoria][caConcursosOrdemCategorias.nuOrdem] =
                    Convert.ToInt32(_dtOrdemCategoria.Rows[indexCategoria][caConcursosOrdemCategorias.nuOrdem].ToString()) + 1;

                _dtOrdemCategoria.Rows[indexCategoria + 1][caConcursosOrdemCategorias.nuOrdem] =
                    Convert.ToInt32(_dtOrdemCategoria.Rows[indexCategoria][caConcursosOrdemCategorias.nuOrdem].ToString()) - 1;

                OrdenaDataTable(ref _dtOrdemCategoria, caConcursosOrdemCategorias.nuOrdem + KuraFrameWork.csConstantes.sCrescente);
                Session["_dtOrdemCategoria"] = _dtOrdemCategoria;

                gvOrdemApres.DataSource = _dtOrdemCategoria;
                gvOrdemApres.DataBind();

                MontaCantoresCategorias(false, false);
                ConfigurarGridView();
            }

            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "AtivaAbaOrdemApres();", true);
        }
    }
}