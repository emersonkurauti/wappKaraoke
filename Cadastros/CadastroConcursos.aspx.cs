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

        private string ScriptGridView = @"$(function () { $('[id*=[idGridView]').footable(); });";

        private string strInicio = "<div class=\"tabbable tabs-left\"> \n <ul class=\"nav nav-tabs\">\n";
        private string strMeio = "</ul> \n <div class=\"tab-content\">\n";
        private string strFim = "</div> \n </div>\n";

        private string strListaMenu = "<li class=\"active\"><a href=\"#div[idLista]\" data-toggle=\"tab\">[Nome]</a></li>\n";
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
                Session["_dtDocumentosExc"] = null;
                Session["_dtDocumentos"] = null;
                Session["_dtImagensExc"] = null;
                Session["_dtImagens"] = null;
                Session["_dtAssociacoesExc"] = null;
                Session["_dtAssociacoes"] = null;
                Session["_dtGruposJuradosExc"] = null;
                Session["_dtGruposJurados"] = null;

                ltMensagem.Text = "";
                ltMensagemArquivos.Text = "";
                ltMensagemAssociacoes.Text = "";
                ltMensagemJurados.Text = "";
                ltMensagensCategorias.Text = "";

                PegarChaveConcurso();

                CarregarDDL();

                CarregarArquivos();
                CarregarAssociacoes();
                CarregarGruposJurados();
                CarregarCantoresCategorias();
            }

            base.Page_Load(sender, e);
        }

        /// <summary>
        /// GERAL
        /// </summary>
        protected override void InicializaSessions()
        {
            base.InicializaSessions();

            Session["strLista"] = null;
            Session["strDivs"] = null;
            Session["alCdCategoria"] = null;
            Session["alDeCategoria"] = null;
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

                //Cantores
                //Attribute to show the Plus Minus Button.
                //gvCantoresConcurso.HeaderRow.Cells[1].Attributes["data-class"] = "expand";

                //Attribute to hide column in Phone.
                //gvCantoresConcurso.HeaderRow.Cells[0].Attributes["data-hide"] = "phone";
                //gvCantoresConcurso.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
                //gvCantoresConcurso.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
                //gvCantoresConcurso.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";

                //Adds THEAD and TBODY to GridView.
                //gvCantoresConcurso.HeaderRow.TableSection = TableRowSection.TableHeader;

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void ConfiguraGridAssociacoes()
        {
            //Associações
            //Attribute to show the Plus Minus Button.
            //gvAssociacoes.HeaderRow.Cells[1].Attributes["data-class"] = "expand";

            //Attribute to hide column in Phone.
            //gvAssociacoes.HeaderRow.Cells[0].Attributes["data-hide"] = "phone";
            //gvAssociacoes.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
            //gvAssociacoes.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";

            //Adds THEAD and TBODY to GridView.
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

        public void btnFechar_Click(Object sender, EventArgs e)
        {
            flFinalizado.Checked = true;
        }

        public void btnReabrir_Click(Object sender, EventArgs e)
        {
            flFinalizado.Checked = false;
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

            csAssociacoes vcsAssociacoesCancores = new csAssociacoes();
            vcsAssociacoesCancores.bFiltraConcurso = true;
            vcsAssociacoesCancores.cdConcurso = Convert.ToInt32(Session["cdConcurso"].ToString());
            cdAssociacaoCantor = vcsAssociacoesCancores.CarregaDDL(cdAssociacaoCantor);

            csFases vcsFasesConcurso = new csFases();
            cdFaseCantor = vcsFasesConcurso.CarregaDDL(cdFaseCantor);

            csCantores vcsCancotres = new csCantores();
            cdCantor = vcsCancotres.CarregaDDL(cdCantor);

            csMusicas vcsMusicas = new csMusicas();
            cdMusica = vcsMusicas.CarregaDDL(cdMusica);

            csStatus vcsStatus = new csStatus();
            cdStatus = vcsStatus.CarregaDDL(cdStatus);
        }

        private DataTable UnionDataTable(DataTable dt1, DataTable dt2)
        {
            DataTable dtResult = new DataTable();

            dtResult = conArquivos.objCo.RetornaEstruturaDT();

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
            DateTime.TryParse(dtIniConcurso.Text, out dtData);
            objCoConcurso.dtIniConcurso = dtData;
            DateTime.TryParse(dtFimConcurso.Text, out dtData);
            objCoConcurso.dtFimConcurso = dtData;

            //Arquivos
            if (Session["_dtDocumentos"] != null)
            {
                _dtDocumentos = (DataTable)Session["_dtDocumentos"];
                if (Session["_dtDocumentosExc"] != null)
                    _dtDocumentos = UnionDataTable(((DataTable)Session["_dtDocumentos"]), ((DataTable)Session["_dtDocumentosExc"]));
            }

            if (Session["_dtImagens"] != null)
            {
                _dtImagens = (DataTable)Session["_dtImagens"];
                if (Session["_dtImagensExc"] != null)
                    _dtImagens = UnionDataTable(((DataTable)Session["_dtImagens"]), ((DataTable)Session["_dtImagensExc"]));
            }

            //Associacoes
            if (Session["_dtAssociacoes"] != null)
            {
                _dtAssociacoes = (DataTable)Session["_dtAssociacoes"];
                if (Session["_dtAssociacoesExc"] != null)
                    _dtAssociacoes = UnionDataTable(((DataTable)Session["_dtAssociacoes"]), ((DataTable)Session["_dtAssociacoesExc"]));
            }

            //Grupo Jurados
            if (Session["_dtGruposJurados"] != null)
            {
                _dtGruposJurados = (DataTable)Session["_dtGruposJurados"];
                if (Session["_dtGruposJuradosExc"] != null)
                    _dtGruposJurados = UnionDataTable(((DataTable)Session["_dtGruposJurados"]), ((DataTable)Session["_dtGruposJuradosExc"]));
            }

            objCoConcurso.dtArquivos = UnionDataTable(_dtDocumentos, _dtImagens);
            objCoConcurso.dtAssociacoes = _dtAssociacoes;
            objCoConcurso.dtGrupoJurados = _dtGruposJurados;
        }

        protected override void btnSalvar_Click(object sender, EventArgs e)
        {
            conConcursos objConConcursos = new conConcursos();
            bool bInserindo = Session["IndexRowDados"] == null;

            objConConcursos.objCoConcursos.LimparAtributos();

            if (!bErro)
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

                Session["ltMensagemDefault"] = ltMensagem;
            }

            if (!bErro)
            {
                string strPagina = Session["_strPaginaConsulta"].ToString();
                Session["_strPaginaConsulta"] = null;
                Response.Redirect(strPagina.Replace("Cadastro", "Consulta"));
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
                ltMensagemArquivos.Text = MostraMensagem("Falha", "Selecione um arquivo para adicionar.", csMensagem.msgWarning);
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
        private void CarregarCantoresCategorias()
        {
            conCantoresFases objConCantoresFases = new conCantoresFases();
            objConCantoresFases.objCoCantoresFases.LimparAtributos();
            objConCantoresFases.objCoCantoresFases.cdConcurso = Convert.ToInt32(Session["cdConcurso"].ToString());

            if (!conCantoresFases.SelectCategoriasConcurso())
            {
                ltMensagensCategorias.Text = MostraMensagem("Falha!", "Falha ao carregar as categorias do concurso.", csMensagem.msgDanger);
                return;
            }

            foreach (DataRow dr in objConCantoresFases.dtDados.Rows)
            {
                AdicionaCantorCategoriaConcurso(dr[caCategorias.cdCategoria].ToString(), dr[caCategorias.deCategoria].ToString(), true);
            }

            RegistrarScript();
            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", Session["strScriptGridView"].ToString(), true);
        }

        public void btnAdicionarCategoria_OnClick(Object sender, EventArgs e)
        {
            AdicionaCantorCategoriaConcurso(cdCategoria.SelectedValue.ToString(), cdCategoria.SelectedItem.ToString());
        }

        private void AdicionaCantorCategoriaConcurso(string strCdCategoria, string strDeCategoria, bool bEstaCarregando = false)
        {
            ArrayList alCdCategoria = new ArrayList();
            ArrayList alDeCategoria = new ArrayList();

            if (Session["alCdCategoria"] != null)
                alCdCategoria = (ArrayList)Session["alCdCategoria"];

            if (Session["alDeCategoria"] != null)
                alDeCategoria = (ArrayList)Session["alDeCategoria"];

            InsereCategoria(strCdCategoria, strDeCategoria, ref alCdCategoria, ref alDeCategoria);

            PreencheLiteral(strCdCategoria, alCdCategoria, alDeCategoria, bEstaCarregando);

            Session["alCdCategoria"] = alCdCategoria;
            Session["alDeCategoria"] = alDeCategoria;
        }

        private void InsereCategoria(string strCdCategoria, string strDeCategoria, ref ArrayList palCdCategoria, ref ArrayList palDeCategoria)
        {
            bool bAchou = false;
            for (int i = 0; i < palCdCategoria.Count; i++)
            {
                if (palCdCategoria[i].ToString() == strCdCategoria)
                    bAchou = true;
            }

            if (!bAchou)
            {
                palCdCategoria.Add(strCdCategoria);
                palDeCategoria.Add(strDeCategoria);
            }
        }

        private void PreencheLiteral(string strCdCategoria, ArrayList palCdCategoria, ArrayList palDeCategoria, bool bEstaCarregando = false)
        {
            csMontaTable ocsMontaTable = new csMontaTable();
            DataTable dtDados;
            string strLista = "";
            string strDivs = "";
            string strScriptGridView = "";

            for (int i = 0; i < palCdCategoria.Count; i++)
            {
                if (palCdCategoria[i].ToString() != strCdCategoria)
                    strLista += strListaMenu.Replace("[Nome]", palDeCategoria[i].ToString()).Replace("[idLista]", palCdCategoria[i].ToString()).Replace("<li class=\"active\">", "<li>");
                else
                    strLista += strListaMenu.Replace("[Nome]", palDeCategoria[i].ToString()).Replace("[idLista]", palCdCategoria[i].ToString());

                if (palCdCategoria[i].ToString() != strCdCategoria)
                    strDivs += strAbrePanel.Replace("[idPanel]", palCdCategoria[i].ToString()).Replace("class=\"tab-pane active\"", "class=\"tab-pane\"");
                else
                    strDivs += strAbrePanel.Replace("[idPanel]", palCdCategoria[i].ToString());
                {
                    strDivs += strUpdatePanelIni.Replace("[UpdatePanel]", palCdCategoria[i].ToString());
                    {
                        strDivs += strPanelBodyIni;
                        {
                            strDivs += strRowIni;
                            {
                                if (Session["dvCantores_" + palCdCategoria[i].ToString()] != null)
                                    dtDados = (DataTable)Session["dvCantores_" + palCdCategoria[i].ToString()];
                                else
                                {
                                    dtDados = ocsMontaTable.RetornaDTCantores();
                                    strScriptGridView += ScriptGridView.Replace("[idGridView]", "dvCantores_" + palCdCategoria[i].ToString());
                                }

                                if (bEstaCarregando)
                                {
                                    conCantoresFases objConCantoresFases = new conCantoresFases();
                                    objConCantoresFases.objCoCantoresFases.LimparAtributos();
                                    objConCantoresFases.objCoCantoresFases.cdConcurso = Convert.ToInt32(Session["cdConcurso"].ToString());
                                    objConCantoresFases.objCoCantoresFases.cdCategoria = Convert.ToInt32(strCdCategoria);

                                    if (!conCantoresFases.SelectCantoresFasesConcurso())
                                    {
                                        ltMensagensCategorias.Text = MostraMensagem("Falha!", "Falha ao carregar os cantores do concurso.", csMensagem.msgDanger);
                                        return;
                                    }

                                    if (objConCantoresFases.dtDados != null)
                                        dtDados = objConCantoresFases.dtDados;
                                }
                                else
                                {
                                    if (palCdCategoria[i].ToString() == strCdCategoria)
                                    {
                                        DataRow dr = dtDados.NewRow();
                                        dr[caConcursos.cdConcurso] = Convert.ToInt32(Session["cdConcurso"].ToString());
                                        //Cantor
                                        dr[caCantores.cdCantor] = cdCantor.SelectedValue;
                                        dr[caCantores.nmCantor] = cdCantor.SelectedItem.Text;
                                        dr[caCantores.nmNomeKanji] = "";
                                        //Associação
                                        dr[caAssociacoes.cdAssociacao] = cdAssociacaoCantor.SelectedValue;
                                        dr[caAssociacoes.nmAssociacao] = cdAssociacaoCantor.SelectedItem.Text;
                                        //Música
                                        dr[caMusicas.cdMusica] = cdMusica.SelectedValue;
                                        dr[caMusicas.nmMusica] = cdMusica.SelectedItem.Text;
                                        dr[caMusicas.nmMusicaKanji] = "";
                                        //Fase
                                        dr[caFases.cdFase] = cdFaseCantor.SelectedValue;
                                        dr[caCategorias.cdCategoria] = cdCategoria.SelectedValue;
                                        dr[caTipoStatus.cdTpStatus] = cdStatus.SelectedValue;
                                        dr[caCantoresFases.nuCantor] = "";
                                        dr[caCantoresFases.nuOrdemApresentacao] = 0;
                                        dtDados.Rows.Add(dr);
                                    }
                                }

                                Session["dvCantores_" + palCdCategoria[i].ToString()] = dtDados;

                                ocsMontaTable.dtDados = dtDados;
                                strDivs += ocsMontaTable.MontaDataGridView(palCdCategoria[i].ToString());
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
            ltMensagemJurados.Text = "";

            if (cdJurado.SelectedIndex > 0)
            {
                if (deGrupo.Text.Trim() != "")
                {
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
            ltTituloEdicaoJurado.Text = _dtGruposJurados.Rows[indexJurado][caGrupos.CC_nmNomeKanji].ToString();
            deGrupoEdit.Text = _dtGruposJurados.Rows[indexJurado][caGrupos.deGrupo].ToString();

            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "AtivaEdicaoJur();", true);
        }

        protected void btnConfirmarEdicaoJur_Click(object sender, EventArgs e)
        {
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
            //this.ScriptManager1.RegisterPostBackControl(btnAdicionarAssociacao);

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
            conConcursosAssociacoes objConConcursosAssociacoes = new conConcursosAssociacoes();
            objConConcursosAssociacoes.objCoConcursosAssociacoes.LimparAtributos();
            objConConcursosAssociacoes.objCoConcursosAssociacoes.cdConcurso = Convert.ToInt32(Session["cdConcurso"].ToString());

            if (!conConcursosAssociacoes.Select())
            {
                ltMensagemAssociacoes.Text = MostraMensagem("Falha", "Problemas ao carregar Associações.", csMensagem.msgDanger);
                return;
            }

            gvAssociacoes.DataSource = objConConcursosAssociacoes.dtDados;
            gvAssociacoes.DataBind();

            Session["_dtAssociacoes"] = objConConcursosAssociacoes.dtDados;
            ConfigurarGridView();
        }

        protected void btnAdicionarAssociacao_Click(object sender, EventArgs e)
        {
            ltMensagemAssociacoes.Text = "";

            if (cdAssociacao.SelectedIndex > 0)
            {
                if (nmRepresentante.Text.Trim() != "")
                {
                    if (deEmail.Text.Trim() != "")
                    {
                        _dtAssociacoes = (DataTable)Session["_dtAssociacoes"];
                        DataRow dr = _dtAssociacoes.NewRow();

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
                    }
                    else
                        ltMensagemAssociacoes.Text = MostraMensagem("Validação!", "Informe o e-mail do Representante.", csMensagem.msgWarning);
                }
                else
                    ltMensagemAssociacoes.Text = MostraMensagem("Validação!", "Informe o nome do Representante.", csMensagem.msgWarning);
            }
            else
                ltMensagemAssociacoes.Text = MostraMensagem("Validação!", "Deve ser selecionada a Associação.", csMensagem.msgWarning);
        }

        protected void RemoveAssociacao(int pintIndice)
        {
            _dtAssociacoes = (DataTable)Session["_dtAssociacoes"];
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
            if (nmRepresentante.Text.Trim() != "")
            {
                if (deEmail.Text.Trim() != "")
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
            //this.ScriptManager1.RegisterPostBackControl(btnAdicionarAssociacao);

            foreach (GridViewRow gvr in gvAssociacoes.Rows)
            {
                LinkButton lnkEditAss = gvr.FindControl("lnkEditAss") as LinkButton;
                ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkEditAss);
            }
        }
    }
}