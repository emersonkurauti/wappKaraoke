using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wappKaraoke.Classes;
using System.Data;
using System.Text;

namespace wappKaraoke.Cadastros
{
    public partial class CadastroConcursos : csPageDefault
    {
        private string strInicio = "<div class=\"tabbable tabs-left\"> \n <ul class=\"nav nav-tabs\">\n";
        private string strMeio = "</ul> \n <div class=\"tab-content\">\n";
        private string strFim = "</div> \n </div>\n";

        private string strListaMenu = "<li class=\"active\"><a href=\"#div[idLista]\" data-toggle=\"tab\">[Nome]</a></li>\n";
        private string strAbrePanel = "<div class=\"tab-pane active\" id=\"div[idPanel]\">\n";
        private string strFechaPanel = "</div>\n";
        private string strUpdatePanelIni = "<asp:UpdatePanel ID=\"up[UpdatePanel]\" runat=\"server\" UpdateMode=\"Conditional\"> \n <ContentTemplate>\n";
        private string strUpdatePanelFim = "</ContentTemplate> \n </asp:UpdatePanel>\n";
        private string strButtonAdd = "<asp:LinkButton id=\"btnAdd[idBtn]\" class=\"btn btn-success btn-block\" runat=\"server\">" +
                                      "<i class=\"glyphicon glyphicon-plus\"></i>&nbsp;&nbsp;Adicionar" +
                                      "</asp:LinkButton>";
        private string strRowIni = "<div class=\"row\">\n";
        private string strRowFim = "</div>\n";
        private string strColIni = "<div class=\"col-sm-[Col]\">";
        private string strColFim = "</div>\n";
        private string strQuebraLinha = "<br//>";
        private string strPanelBodyIni = "<div class=\"panel-body\">";
        private string strPanelBodyFim = "</div>";

        private string strLista;
        private string strDivs;

        public override void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
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
                //csAssociacoes vcsAssociacoesCantor = new csAssociacoes();
                //cdAssociacaoCantor = vcsAssociacoesCantor.CarregaDDL(cdAssociacaoCantor);

                //csCantores vcsCantores = new csCantores();
                //cdCantor = vcsCantores.CarregaDDL(cdCantor);

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

            base.Page_Load(sender, e);
        }

        public override void ConfirarGridView()
        {
            base.ConfirarGridView();

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

            //Jurados
            //Attribute to show the Plus Minus Button.
            gvGrupoJuradoConcurso.HeaderRow.Cells[2].Attributes["data-class"] = "expand";

            //Attribute to hide column in Phone.
            gvGrupoJuradoConcurso.HeaderRow.Cells[0].Attributes["data-hide"] = "phone";
            gvGrupoJuradoConcurso.HeaderRow.Cells[1].Attributes["data-hide"] = "phone";
            gvGrupoJuradoConcurso.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
            gvGrupoJuradoConcurso.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";

            //Adds THEAD and TBODY to GridView.
            gvGrupoJuradoConcurso.HeaderRow.TableSection = TableRowSection.TableHeader;

            //Fases
            //Attribute to show the Plus Minus Button.
            gvFasesConcurso.HeaderRow.Cells[1].Attributes["data-class"] = "expand";

            //Attribute to hide column in Phone.
            gvFasesConcurso.HeaderRow.Cells[0].Attributes["data-hide"] = "phone";
            gvFasesConcurso.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
            gvFasesConcurso.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";

            //Adds THEAD and TBODY to GridView.
            gvFasesConcurso.HeaderRow.TableSection = TableRowSection.TableHeader;

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
        }

        public override void InicializaSessions()
        {
            base.InicializaSessions();

            Session["strLista"] = null;
            Session["strDivs"] = null;
        }

        public void btnAdicionarCategoria_OnClick(Object sender, EventArgs e)
        {
            csCantores ocsCancotres = new csCantores();
            csAssociacoes ocsAssociacoes = new csAssociacoes();
            csMusicas ocsMusicas = new csMusicas();
            csFases ocsFases = new csFases();
            csStatus ocsStatus = new csStatus();

            if (Session["strLista"] != null)
                strLista = Session["strLista"].ToString().Replace("<li class=\"active\">", "<li>");

            if (Session["strDivs"] != null)
                strDivs = Session["strDivs"].ToString().Replace("class=\"tab-pane active\"", "class=\"tab-pane\"");

            strLista += strListaMenu.Replace("[Nome]", cdCategoria.SelectedItem.Text).Replace("[idLista]", cdCategoria.SelectedItem.ToString());

            strDivs += strAbrePanel.Replace("[idPanel]", cdCategoria.SelectedItem.ToString());
            {
                strDivs += strUpdatePanelIni.Replace("[UpdatePanel]", cdCategoria.SelectedIndex.ToString());
                {
                    strDivs += strPanelBodyIni;
                    {
                        strDivs += strRowIni;
                        {
                            //Cantor
                            strDivs += strColIni.Replace("[Col]", "6");
                            {
                                strDivs += ocsCancotres.MontaSelect(cdCategoria.SelectedIndex.ToString());
                            }
                            strDivs += strColFim;
                            //Associação
                            strDivs += strColIni.Replace("[Col]", "6");
                            {
                                strDivs += ocsAssociacoes.MontaSelect(cdCategoria.SelectedIndex.ToString());
                            }
                            strDivs += strColFim;
                        }
                        strDivs += strRowFim;

                        strDivs += strQuebraLinha;

                        strDivs += strRowIni;
                        {
                            //Musica
                            strDivs += strColIni.Replace("[Col]", "6");
                            {
                                strDivs += ocsMusicas.MontaSelect(cdCategoria.SelectedIndex.ToString());
                            }
                            strDivs += strColFim;
                            //Fase
                            strDivs += strColIni.Replace("[Col]", "6");
                            {
                                strDivs += ocsFases.MontaSelect(cdCategoria.SelectedIndex.ToString());
                            }
                            strDivs += strColFim;
                        }
                        strDivs += strRowFim;

                        strDivs += strQuebraLinha;

                        strDivs += strRowIni;
                        {
                            //Status
                            strDivs += strColIni.Replace("[Col]", "6");
                            {
                                strDivs += ocsStatus.MontaSelect(cdCategoria.SelectedIndex.ToString());
                            }
                            strDivs += strColFim;
                            //Adicionar
                            strDivs += strColIni.Replace("[Col]", "6");
                            {
                                strDivs += strButtonAdd.Replace("[idBtn]", cdCategoria.SelectedIndex.ToString());
                            }
                            strDivs += strColFim;
                        }
                        strDivs += strRowFim;
                    }
                    strDivs += strPanelBodyFim;

                }
                strDivs += strUpdatePanelFim;
            }
            strDivs += strFechaPanel;

            Session["strLista"] = strLista;
            Session["strDivs"] = strDivs;

            ltCategorias.Text = strInicio + strLista + strMeio + strDivs + strFim;

            Session["ltCategorias"] = ltCategorias.Text;
        }
    }
}