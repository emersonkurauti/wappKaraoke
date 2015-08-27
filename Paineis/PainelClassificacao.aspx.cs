using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wappKaraoke.Classes;
using System.Data;
using wappKaraoke.Classes.Model.ConcursosOrdemCategorias;
using wappKaraoke.Classes.Mensagem;
using wappKaraoke.Classes.Controller;
using wappKaraoke.Classes.Model.Concursos;
using wappKaraoke.Classes.Model.CantoresFases;

namespace wappKaraoke.Paineis
{
    public partial class PainelClassificacao : csPageCadastro
    {
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
            ltMensagemDefault = ltMensagem;

            if (!this.IsPostBack)
            {
                LimpaSessions();
                ltMensagem.Text = "";

                PegarChaveConcurso();

                CarregarDDL();

                CarregarCantoresCategorias();
            }

            base.Page_Load(sender, e);
        }

        private void CarregarDDL()
        {
            csFases vcsFases = new csFases();
            cdFase = vcsFases.CarregaDDL(cdFase);
            cdFase.SelectedValue = Session["cdFaseCorrente"].ToString();
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
            Session["_dtCantoresFases"] = null;
            Session["_dtOrdemCategoria"] = null;
        }

        private void CarregarCantoresCategorias()
        {
            conCantoresFases objConCantoresFases = new conCantoresFases();
            objConCantoresFases.objCoCantoresFases.LimparAtributos();
            objConCantoresFases.objCoCantoresFases.cdConcurso = Convert.ToInt32(Session["cdConcurso"].ToString());
            objConCantoresFases.objCoCantoresFases.cdFase = Convert.ToInt32(cdFase.SelectedValue);

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
                                        Convert.ToInt32(cdFase.SelectedValue);
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

                                OrdenaDataTable(ref dtDados, caCantoresFases.nuNotafinal + " DESC");

                                ocsMontaTable.bExibirColocacao = true;
                                ocsMontaTable.bExibirSequencial = false;
                                ocsMontaTable.bExibirNotaFinal = true;
                                ocsMontaTable.bExibirDesconto = true;
                                ocsMontaTable.bExibirCodigos = false;
                                ocsMontaTable.bExibirbtnEditar = false;
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

            Session["strScriptGridView"] = strScriptGridView;
            ltCategorias.Text = strInicio + strLista + strMeio + strDivs + strFim;
        }

        protected void cdFase_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarCantoresCategorias();
        }

        protected void btnAtualizar_Click(object sender, EventArgs e)
        {
            CarregarCantoresCategorias();
        }
    }
}