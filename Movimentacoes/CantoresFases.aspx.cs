using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using wappKaraoke.Classes;
using wappKaraoke.Classes.Controller;
using wappKaraoke.Classes.Model.Concursos;
using wappKaraoke.Classes.Mensagem;

namespace wappKaraoke.Movimentacoes
{
    public partial class CantoresFases : csPageDefault
    {
        private DataTable dtCantoresFase;
        private DataTable dtCantoresProxFase;

        public override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                conConcursos objConConcursos = new conConcursos();
                objConConcursos.objCoConcursos.LimparAtributos();
                objConConcursos.objCoConcursos.strFiltro = " WHERE flConcursoCorrente = 'S'";

                if (conConcursos.Select())
                {
                    Session["cdConcurso"] = objConConcursos.dtDados.Rows[0][caConcursos.cdConcurso].ToString();
                }

                if (Session["cdConcurso"] == null)
                {
                    ltMensagem.Text = MostraMensagem("Falha!", "Não existe concurso corrente definido.", csMensagem.msgDanger);
                    return;
                }

                conCantoresFases objConCantoresFases = new conCantoresFases();
                objConCantoresFases.objCoCantoresFases.LimparAtributos();
                objConCantoresFases.objCoCantoresFases.cdConcurso = Convert.ToInt32(Session["cdConcurso"].ToString());

                if (!conCantoresFases.SelectCategoriasConcurso())
                {
                    ltMensagem.Text = MostraMensagem("Falha!", "Não foi possível carregar as categorias do concurso.", csMensagem.msgDanger);
                    return;
                }

                csCategorias vcsCategorias = new csCategorias();
                vcsCategorias.bUtilizaDadosExternos = true;
                vcsCategorias.dtDadosExternos = objConCantoresFases.dtDados;
                cdCategoria = vcsCategorias.CarregaDDL(cdCategoria);

                csFases vcsFases = new csFases();
                cdFase = vcsFases.CarregaDDL(cdFase);

                vcsFases = new csFases();
                cdProxFase = vcsFases.CarregaDDL(cdProxFase);
            }

            base.Page_Load(sender, e);
        }

        protected override bool ConfigurarGridView()
        {
            try
            {
                gvFase.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvProxFase.HeaderRow.TableSection = TableRowSection.TableHeader;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private DataTable RetornarCantoresFases(int pcdFase)
        {
            DataTable dtResultado = new DataTable();

            conCantoresFases objConCantoresFases = new conCantoresFases();
            objConCantoresFases.objCoCantoresFases.LimparAtributos();
            objConCantoresFases.objCoCantoresFases.cdConcurso = Convert.ToInt32(Session["cdConcurso"].ToString());
            objConCantoresFases.objCoCantoresFases.cdCategoria = Convert.ToInt32(cdCategoria.SelectedValue);
            objConCantoresFases.objCoCantoresFases.cdFase = pcdFase;

            if (!conCantoresFases.SelectCantoresFasesCategoriasConcurso())
            {
                ltMensagem.Text = MostraMensagem("Falha!", "Não foi possível carregar os cantores.", csMensagem.msgDanger);
                return null;
            }

            if (objConCantoresFases.dtDados.Rows.Count > 0)
                dtResultado = objConCantoresFases.dtDados;

            return dtResultado;
        }

        private void RetornaEstruturaDT(ref DataTable dtCantorFase)
        {
            dtCantorFase.Columns.Add("cdCantor", typeof(int));
            dtCantorFase.Columns.Add("nmCantor", typeof(string));
            dtCantorFase.Columns.Add("nuNotaFinal", typeof(decimal));
            dtCantorFase.Columns.Add("pcDesconto", typeof(decimal));
        }

        private void CancelaOperacao()
        {
            cdFase.SelectedIndex = 0;
            Session["dtCantoresFase"] = null;
            gvFase.DataSource = null;
            gvFase.DataBind();

            cdProxFase.SelectedIndex = 0;
            Session["dtCantoresProxFase"] = null;
            gvProxFase.DataSource = null;
            gvProxFase.DataBind();

            ConfigurarGridView();
        }

        protected void cdFase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((cdFase.SelectedIndex != 0) && (cdFase.SelectedValue == cdProxFase.SelectedValue))
            {
                ltMensagem.Text = MostraMensagem("Validação!", "Não pode ser selecionada a mesma fase.", csMensagem.msgWarning);
                return;
            }

            if (cdFase.SelectedIndex == 0)
                dtCantoresFase = null;
            else
                dtCantoresFase = RetornarCantoresFases(Convert.ToInt32(cdFase.SelectedValue));

            Session["dtCantoresFase"] = dtCantoresFase;
            gvFase.DataSource = dtCantoresFase;
            gvFase.DataBind();

            ConfigurarGridView();
        }

        protected void cdProxFase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cdFase.SelectedIndex == 0)
                dtCantoresProxFase = null;
            else
                dtCantoresProxFase = RetornarCantoresFases(Convert.ToInt32(cdProxFase.SelectedValue));

            Session["dtCantoresProxFase"] = dtCantoresProxFase;
            gvProxFase.DataSource = dtCantoresProxFase;
            gvProxFase.DataBind();

            ConfigurarGridView();
        }

        protected void cdCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            CancelaOperacao();
        }

        protected void btnProxima_Click(object sender, EventArgs e)
        {
            dtCantoresFase = Session["dtCantoresFase"] == null ? null : (DataTable)Session["dtCantoresFase"];
            dtCantoresProxFase = Session["dtCantoresProxFase"] == null ? null : (DataTable)Session["dtCantoresProxFase"];

            if (cdProxFase.SelectedIndex == 0)
            {
                ltMensagem.Text = MostraMensagem("Validação!", "Selecione a fila de destino.", csMensagem.msgWarning);
                return;
            }

            if (dtCantoresFase == null)
            {
                ltMensagem.Text = MostraMensagem("Validação!", "Não existem cantores na fila de origem.", csMensagem.msgWarning);
                return;
            }

            if (dtCantoresProxFase == null)
                RetornaEstruturaDT(ref dtCantoresProxFase);

            int i = 0;

            foreach (GridViewRow row in gvFase.Rows)
            {
                CheckBox chSelecionado = (CheckBox)row.FindControl("chkRow");

                if (chSelecionado.Checked)
                {
                    dtCantoresProxFase.ImportRow(dtCantoresFase.Rows[i]);
                    dtCantoresFase.Rows.RemoveAt(i);
                    i--;
                }

                i++;
            }

            Session["dtCantoresFase"] = dtCantoresFase;
            gvFase.DataSource = dtCantoresFase;
            gvFase.DataBind();

            Session["dtCantoresProxFase"] = dtCantoresProxFase;
            gvProxFase.DataSource = dtCantoresProxFase;
            gvProxFase.DataBind();

            ConfigurarGridView();
        }

        protected void btnAnterior_Click(object sender, EventArgs e)
        {
            dtCantoresFase = Session["dtCantoresFase"] == null ? null : (DataTable)Session["dtCantoresFase"];
            dtCantoresProxFase = Session["dtCantoresProxFase"] == null ? null : (DataTable)Session["dtCantoresProxFase"];

            if (cdFase.SelectedIndex == 0)
            {
                ltMensagem.Text = MostraMensagem("Validação!", "Selecione a fila de destino.", csMensagem.msgWarning);
                return;
            }

            if (dtCantoresProxFase == null)
            {
                ltMensagem.Text = MostraMensagem("Validação!", "Não existem cantores na fila de origem.", csMensagem.msgWarning);
                return;
            }

            if (dtCantoresFase == null)
                RetornaEstruturaDT(ref dtCantoresFase);

            int i = 0;

            foreach (GridViewRow row in gvProxFase.Rows)
            {
                CheckBox chSelecionado = (CheckBox)row.FindControl("chkRow");

                if (chSelecionado.Checked)
                {
                    dtCantoresFase.ImportRow(dtCantoresProxFase.Rows[i]);
                    dtCantoresProxFase.Rows.RemoveAt(i);
                    i--;
                }

                i++;
            }

            Session["dtCantoresFase"] = dtCantoresFase;
            gvFase.DataSource = dtCantoresFase;
            gvFase.DataBind();

            Session["dtCantoresProxFase"] = dtCantoresProxFase;
            gvProxFase.DataSource = dtCantoresProxFase;
            gvProxFase.DataBind();

            ConfigurarGridView();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            cdCategoria.SelectedIndex = 0;
            CancelaOperacao();
        }
    }
}