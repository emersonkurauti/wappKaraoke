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
        private DataTable _dtImagens;
        private DataTable _dtAssociacoes;
        private DataTable _dtGruposJurados;

        private string strScriptGridView = "";
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
            ltMensagemArquivos.Text = "";

            ltMensagemDefault = ltMensagem;
            tobjCa = typeof(caConcursos);
            objCon = new conConcursos();            

            if (!this.IsPostBack)
            {
                PegarChaveConcurso();

                CarregarDDL();

                CarregarArquivos();
                CarregarAssociacoes();
                CarregarGruposJurados();
                CarregarCantoresCategorias();
            }

            base.Page_Load(sender, e);
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
            gvAssociacoes.HeaderRow.Cells[1].Attributes["data-class"] = "expand";

            //Attribute to hide column in Phone.
            gvAssociacoes.HeaderRow.Cells[0].Attributes["data-hide"] = "phone";
            gvAssociacoes.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
            gvAssociacoes.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
            gvAssociacoes.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
            gvAssociacoes.HeaderRow.Cells[5].Attributes["data-hide"] = "phone";

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
            gvGrupoJuradoConcurso.HeaderRow.Cells[2].Attributes["data-class"] = "expand";

            //Attribute to hide column in Phone.
            gvGrupoJuradoConcurso.HeaderRow.Cells[0].Attributes["data-hide"] = "phone";
            gvGrupoJuradoConcurso.HeaderRow.Cells[1].Attributes["data-hide"] = "phone";
            gvGrupoJuradoConcurso.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
            gvGrupoJuradoConcurso.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";

            //Adds THEAD and TBODY to GridView.
            gvGrupoJuradoConcurso.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        
        private void ConfiguraGridDocumentos()
        {
            //Attribute to show the Plus Minus Button.
            gvDocumentos.HeaderRow.Cells[1].Attributes["data-class"] = "expand";

            //Attribute to hide column in Phone.
            gvDocumentos.HeaderRow.Cells[0].Attributes["data-hide"] = "phone";
            gvDocumentos.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";

            //Adds THEAD and TBODY to GridView.
            gvDocumentos.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected override void InicializaSessions()
        {
            base.InicializaSessions();

            Session["strLista"] = null;
            Session["strDivs"] = null;
            Session["alCdCategoria"] = null;
            Session["alDeCategoria"] = null;            
        }

        public void btnFechar_Click(Object sender, EventArgs e)
        {
            flFinalizado.Checked = true;
        }

        public void btnReabrir_Click(Object sender, EventArgs e)
        {
            flFinalizado.Checked = false;
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

            Page.ClientScript.RegisterStartupScript(this.GetType(), "", strScriptGridView, true);

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

            ltCategorias.Text = strInicio + strLista + strMeio + strDivs + strFim;
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

        private void CopiarArquivoParaTemp()
        {
            string strCaminhoTemp = Request.PhysicalApplicationPath + wappKaraoke.Properties.Settings.Default.sCaminhoTemp;
            fluArquivo.SaveAs(strCaminhoTemp + fluArquivo.FileName);
        }

        private void AddArquivo(ref DataTable pdtArquivo)
        {
            DataRow dr = pdtArquivo.NewRow();

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
            ConfiguraGridDocumentos();
        }

        protected void btnAdicionarArquivo_Click(object sender, EventArgs e)
        {
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

        private void CarregarImagens() 
        {
            if (Session["_dtImagens"] != null)
            {
                int seq = 0;
                string strCaminho = "../" + wappKaraoke.Properties.Settings.Default.sCaminhoArqImagens.Replace("\\", "/");
                string strCaminhoTemp = "../" + wappKaraoke.Properties.Settings.Default.sCaminhoTemp.Replace("\\", "/");
                string strCaminhoImg = "";

                _dtImagens = (DataTable)Session["_dtImagens"];

                ltImagens.Text = csDinamico.strInicioLista;

                foreach (DataRow dr in _dtImagens.Rows)
                {
                    if (seq!=0 && seq % 3 == 0)
                    {
                        ltImagens.Text += csDinamico.strFinalLista;
                        ltImagens.Text += csDinamico.strInicioLista;
                    }
                    if (Convert.ToInt32(dr[caArquivos.cdArquivo].ToString()) != 0)
                        strCaminhoImg = strCaminho;
                    else
                        strCaminhoImg = strCaminhoTemp;

                    ltImagens.Text += csDinamico.strDivImagem.Replace("[strCaminhoImagem]", strCaminhoImg + dr[caArquivos.nmArquivo].ToString()).Replace("[strDescImagem]",
                            dr[caArquivos.deArquivo].ToString()).Replace("[strSeqImagem]", (seq++).ToString());
                }

                ltImagens.Text += csDinamico.strFinalLista;
            }
        }

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
            ConfiguraGridAssociacoes();
        }

        protected void btnAdicionarAssociacao_Click(object sender, EventArgs e)
        {
            if (cdAssociacao.SelectedIndex > 0)
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
                ConfiguraGridAssociacoes();
            }
            else
                ltMensagemAssociacoes.Text = MostraMensagem("Validação!", "Deve ser selecionada a Associação.", csMensagem.msgWarning);
        }

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
            ConfiguraGridJurados();
        }

        protected void btnAdicionarGrupoJurado_Click(object sender, EventArgs e)
        {
            if (cdJurado.SelectedIndex > 0)
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
                ConfiguraGridJurados();
            }
            else
                ltMensagemJurados.Text = MostraMensagem("Validação!", "Deve ser selecionado o Jurado.", csMensagem.msgWarning);
        }

        protected void upArquivos_PreRender(object sender, EventArgs e)
        {
            this.ScriptManager1.RegisterPostBackControl(btnAdicionarArquivo);
        }

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


            Page.ClientScript.RegisterStartupScript(this.GetType(), "", strScriptGridView, true);
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
            objCoConcurso.dtArquivos = UnionDataTable(((DataTable)Session["_dtDocumentos"]), ((DataTable)Session["_dtImagens"]));
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
                        ltMensagemDefault.Text = base.MostraMensagem(csMensagem.msgOperacaoComSucesso, csMensagem.msgRegistroInserido, csMensagem.msgSucess);
                    }
                    else
                    {
                        bErro = true;
                        ltMensagemDefault.Text = base.MostraMensagem(csMensagem.msgTitFalhaGenerica, objConConcursos.strMensagemErro, csMensagem.msgWarning);
                    }
                }
                else
                {
                    if (conConcursos.Alterar())
                    {
                        ltMensagemDefault.Text = base.MostraMensagem(csMensagem.msgOperacaoComSucesso, csMensagem.msgRegistroAlterado, csMensagem.msgSucess);
                    }
                    else
                    {
                        bErro = true;
                        ltMensagemDefault.Text = base.MostraMensagem(csMensagem.msgTitFalhaGenerica, objConConcursos.strMensagemErro, csMensagem.msgWarning);
                    }
                }

                Session["ltMensagemDefault"] = ltMensagemDefault;
            }

            if (!bErro)
                Response.Redirect(strPaginaConsulta.Replace("Cadastro", "Consulta"));
        }
    }
}