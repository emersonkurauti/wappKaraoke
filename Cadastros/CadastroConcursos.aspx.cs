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

namespace wappKaraoke.Cadastros
{
    public partial class CadastroConcursos : csPageCadastro
    {
        private DataTable _dtDocumentos;
        private DataTable _dtImagens;

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

            base.Page_Load(sender, e);

            if (!this.IsPostBack)
            {
                PegarChaveConcurso();

                CarregarDDL();

                CarregarArquivos();

                //Associações
                DataTable dt = new DataTable();
                dt.Columns.Add("cdAssociacao", typeof(int));
                dt.Columns.Add("nmAssociacao", typeof(string));
                dt.Columns.Add("nmRepresentante", typeof(string));
                dt.Columns.Add("deEmail", typeof(string));

                for (int i = 0; i < 4; i++)
                {
                    DataRow dr = dt.NewRow();

                    dr["cdAssociacao"] = i;
                    dr["nmAssociacao"] = "Nome Associação de teste - " + i;
                    dr["nmRepresentante"] = "Nome Representante de teste - " + i;
                    dr["deEmail"] = "emailteste" + i + "@hotmail.com";

                    dt.Rows.Add(dr);
                }

                gvAssociacoes.DataSource = dt;
                gvAssociacoes.DataBind();

                //Jurados
                dt = new DataTable();
                dt.Columns.Add("cdJurado", typeof(int));
                dt.Columns.Add("nmJurado", typeof(string));
                dt.Columns.Add("nmNomeKanji", typeof(string));
                dt.Columns.Add("deGrupo", typeof(string));

                for (int i = 0; i < 4; i++)
                {
                    DataRow dr = dt.NewRow();

                    dr["cdJurado"] = i;
                    dr["nmJurado"] = "Nome Jurado de teste - " + i;
                    dr["nmNomeKanji"] = "Nome KANJI" + i;
                    dr["deGrupo"] = "GRUPO " + i;

                    dt.Rows.Add(dr);
                }

                gvGrupoJuradoConcurso.DataSource = dt;
                gvGrupoJuradoConcurso.DataBind();

                for (int i = 0; i < 4; i++)
                {
                    ((Literal)gvGrupoJuradoConcurso.Rows[i].FindControl("ltNomeKanji")).Text = @"" + dt.Rows[i]["nmJurado"].ToString() + " <br/> " + dt.Rows[i]["nmNomeKanji"].ToString();
                }

                //Fases
                dt = new DataTable();
                dt.Columns.Add("cdFase", typeof(int));
                dt.Columns.Add("deFase", typeof(string));

                for (int i = 0; i < 4; i++)
                {
                    DataRow dr = dt.NewRow();

                    dr["cdFase"] = i;
                    dr["deFase"] = "Fase Teste - " + i;

                    dt.Rows.Add(dr);
                }

                gvFasesConcurso.DataSource = dt;
                gvFasesConcurso.DataBind();

                //Cantores
                dt = new DataTable();
                dt.Columns.Add("cdCantor", typeof(int));
                dt.Columns.Add("nmCantor", typeof(string));
                dt.Columns.Add("nmAssociacao", typeof(string));

                for (int i = 0; i < 4; i++)
                {
                    DataRow dr = dt.NewRow();

                    dr["cdCantor"] = i;
                    dr["nmCantor"] = "Cantor de Teste - " + i;
                    dr["nmAssociacao"] = "Associacao de Teste - " + i;

                    dt.Rows.Add(dr);
                }

                //gvCantoresConcurso.DataSource = dt;
                //gvCantoresConcurso.DataBind();
            }
        }

        protected override bool ConfigurarGridView()
        {
            if (!base.ConfigurarGridView())
                return false;

            try
            {
                ConfiguraGridAssociacoes();
                ConfiguraGridJurados();
                ConfiguraGridFases();
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

        private void ConfiguraGridFases()
        {
            //Attribute to show the Plus Minus Button.
            gvFasesConcurso.HeaderRow.Cells[1].Attributes["data-class"] = "expand";

            //Attribute to hide column in Phone.
            gvFasesConcurso.HeaderRow.Cells[0].Attributes["data-hide"] = "phone";
            gvFasesConcurso.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
            gvFasesConcurso.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";

            //Adds THEAD and TBODY to GridView.
            gvFasesConcurso.HeaderRow.TableSection = TableRowSection.TableHeader;
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
            Session["_dtImagens"] = null;
            Session["_dtDocumentos"] = null;
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
            //csMontaTable ocsMontaTable = new csMontaTable();
            //DataTable dtDados;

            //if (Session["strLista"] != null)
            //    strLista = Session["strLista"].ToString().Replace("<li class=\"active\">", "<li>");

            //if (Session["strDivs"] != null)
            //    strDivs = Session["strDivs"].ToString().Replace("class=\"tab-pane active\"", "class=\"tab-pane\"");


            //ltJavaScript.Text = "<script type=\"text/javascript\">";

            //strLista += strListaMenu.Replace("[Nome]", cdCategoria.SelectedItem.Text).Replace("[idLista]", cdCategoria.SelectedValue.ToString());

            //strDivs += strAbrePanel.Replace("[idPanel]", cdCategoria.SelectedValue.ToString());
            //{
            //    strDivs += strUpdatePanelIni.Replace("[UpdatePanel]", cdCategoria.SelectedIndex.ToString());
            //    {
            //        strDivs += strPanelBodyIni;
            //        {
            //            strDivs += strRowIni;
            //            {
            //                if (Session["dvCantores_" + cdCategoria.SelectedIndex.ToString()] != null)
            //                    dtDados = (DataTable)Session["dvCantores_" + cdCategoria.SelectedIndex.ToString()];
            //                else
            //                    dtDados = ocsMontaTable.RetornaDTCantores();

            //                DataRow dr = dtDados.NewRow();
            //                dr["cdCantor"] = cdCantor.SelectedValue;
            //                dr["nmCantor"] = cdCantor.SelectedItem.Text;
            //                dr["cdMusica"] = cdMusica.SelectedValue;
            //                dr["nmMusica"] = cdMusica.SelectedItem.Text;
            //                dr["cdFase"] = cdFaseCantor.SelectedValue;
            //                dr["deFase"] = cdFaseCantor.SelectedItem.Text;
            //                dr["cdStatus"] = cdStatus.SelectedValue;
            //                dr["deStatus"] = cdStatus.SelectedItem.Text;
            //                dr["cdAssociacao"] = cdAssociacaoCantor.SelectedValue;
            //                dr["nmAssociacao"] = cdAssociacaoCantor.SelectedItem.Text;
            //                dtDados.Rows.Add(dr);

            //                ocsMontaTable.dtDados = dtDados;
            //                strDivs += ocsMontaTable.MontaDataGridView(cdCategoria.SelectedIndex.ToString());

            //                ltJavaScript.Text += "$(function () {" +
            //                                     "    $('[id*=gvCantores_" + cdCategoria.SelectedIndex.ToString() + "]').footable();" +
            //                                     "});";
            //            }
            //            strDivs += strRowFim;
            //        }
            //        strDivs += strPanelBodyFim;

            //    }
            //    strDivs += strUpdatePanelFim;
            //}
            //strDivs += strFechaPanel;

            //Session["strLista"] = strLista;
            //Session["strDivs"] = strDivs;

            //ltCategorias.Text = strInicio + strLista + strMeio + strDivs + strFim;
            //Session["ltCategorias"] = ltCategorias.Text;

            //ltJavaScript.Text += "</script>";
        }

        private void AdicionaCantorCategoriaConcurso(string strCdCategoria, string strDeCategoria)
        {
            ArrayList alCdCategoria = new ArrayList();
            ArrayList alDeCategoria = new ArrayList();

            if (Session["alCdCategoria"] != null)
                alCdCategoria = (ArrayList)Session["alCdCategoria"];

            if (Session["alDeCategoria"] != null)
                alDeCategoria = (ArrayList)Session["alDeCategoria"];

            InsereCategoria(strCdCategoria, strDeCategoria, ref alCdCategoria, ref alDeCategoria);

            PreencheLiteral(strCdCategoria, alCdCategoria, alDeCategoria);

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

        private void PreencheLiteral(string strCdCategoria, ArrayList palCdCategoria, ArrayList palDeCategoria)
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
                                    dtDados = ocsMontaTable.RetornaDTCantores();

                                if (palCdCategoria[i].ToString() == strCdCategoria)
                                {
                                    DataRow dr = dtDados.NewRow();
                                    dr["cdCantor"] = cdCantor.SelectedValue;
                                    dr["nmCantor"] = cdCantor.SelectedItem.Text;
                                    dr["cdMusica"] = cdMusica.SelectedValue;
                                    dr["nmMusica"] = cdMusica.SelectedItem.Text;
                                    dr["cdFase"] = cdFaseCantor.SelectedValue;
                                    dr["deFase"] = cdFaseCantor.SelectedItem.Text;
                                    dr["cdStatus"] = cdStatus.SelectedValue;
                                    dr["deStatus"] = cdStatus.SelectedItem.Text;
                                    dr["cdAssociacao"] = cdAssociacaoCantor.SelectedValue;
                                    dr["nmAssociacao"] = cdAssociacaoCantor.SelectedItem.Text;
                                    dtDados.Rows.Add(dr);
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

            csFases vcsFases = new csFases();
            cdFase = vcsFases.CarregaDDL(cdFase);

            csCategorias vcsCategorias = new csCategorias();
            cdCategoria = vcsCategorias.CarregaDDL(cdCategoria);

            //Filtrar somente as associações adicionadas no concurso
            csAssociacoes vcsAssociacoesCancores = new csAssociacoes();
            cdAssociacaoCantor = vcsAssociacoesCancores.CarregaDDL(cdAssociacaoCantor);

            //Filtrar somente fases adicionadas no concurso
            csFases vcsFasesConcurso = new csFases();
            cdFaseCantor = vcsFasesConcurso.CarregaDDL(cdFaseCantor);

            csCantores vcsCancotres = new csCantores();
            cdCantor = vcsCancotres.CarregaDDL(cdCantor);

            csMusicas vcsMusicas = new csMusicas();
            cdMusica = vcsMusicas.CarregaDDL(cdMusica);

            csStatus vcsStatus = new csStatus();
            cdStatus = vcsStatus.CarregaDDL(cdStatus);
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

                CarregarImagens();
            }

            if (Session["_dtDocumentos"] != null)
                _dtDocumentos = (DataTable)Session["_dtDocumentos"];
            else
            {
                objConArquivos.objCoArquivos.cdTipoArquivo = csConstantes.cCdTipoArquivoDocumento;

                if (!conArquivos.Select())
                    ltMensagemArquivos.Text = MostraMensagem("Falha", "Problemas ao carregar documentos.", csMensagem.msgDanger);

                gvDocumentos.DataSource = objConArquivos.dtDados;
                gvDocumentos.DataBind();

                Session["_dtDocumentos"] = objConArquivos.dtDados;
            }
        }

        protected void btnAdicionarArquivo_Click(object sender, EventArgs e)
        {
            if (nmArquivo.Text != "")
            {
                if (Convert.ToInt32(hdfCdTpArquivo.Value.ToString()) == csConstantes.cCdTipoArquivoImagem)
                {
                    if (Session["_dtImagens"] != null)
                    {
                        _dtImagens = (DataTable)Session["_dtImagens"];
                        AddArquivo(ref _dtImagens);
                    }

                }
                else if (Convert.ToInt32(hdfCdTpArquivo.Value.ToString()) == csConstantes.cCdTipoArquivoDocumento)
                {
                    if (Session["_dtDocumentos"] != null)
                    {
                        _dtDocumentos = (DataTable)Session["_dtDocumentos"];
                        AddArquivo(ref _dtDocumentos);                        
                    }
                }

                CarregarArquivos();
            }
            else
            {
                ltMensagemArquivos.Text = MostraMensagem("Falha", "Selecione um arquivo para adicionar.", csMensagem.msgWarning);
            }
        }

        private void AddArquivo(ref DataTable pdtArquivo)
        {
            DataRow dr = pdtArquivo.NewRow();

            dr[caArquivos.cdArquivo] = 0;
            dr[caArquivos.cdConcurso] = Convert.ToInt32(Session["cdConcurso"].ToString());
            dr[caArquivos.cdTipoArquivo] = Convert.ToInt32(hdfCdTpArquivo.ToString());
            dr[caArquivos.nmArquivo] = nmArquivo.Text;
            dr[caArquivos.deArquivo] = deArquivo.Text;

            pdtArquivo.Rows.Add(dr);
        }

        private void CarregarImagens() 
        {
            if (Session["_dtImagens"] != null)
            {
                int seq = 0;
                string strCaminho = "../" + wappKaraoke.Properties.Settings.Default.sCaminhoArqImagens.Replace("\\", "/");

                _dtImagens = (DataTable)Session["_dtImagens"];

                ltImagens.Text = csDinamico.strInicioLista;

                foreach (DataRow dr in _dtImagens.Rows)
                {
                    ltImagens.Text += csDinamico.strDivImagem.Replace("[strCaminhoImagem]", strCaminho + dr[caArquivos.nmArquivo].ToString()).Replace("[strDescImagem]",
                        dr[caArquivos.deArquivo].ToString()).Replace("[strSeqImagem]", (seq++).ToString());
                }

                ltImagens.Text += csDinamico.strFinalLista;
            }
        }
    }
}