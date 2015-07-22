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
            if (!base.ConfigurarGridView())
                return false;

            try
            {
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

        protected void cdFase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cdFase.SelectedIndex == 0)
                dtCantoresFase = null;
            else
                dtCantoresFase = RetornarCantoresFases(Convert.ToInt32(cdFase.SelectedValue));

            Session["dtCantoresFase"] = dtCantoresFase;
            gvFase.DataSource = dtCantoresFase;
            gvFase.DataBind();
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
        }

        protected void cdCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            cdFase.SelectedIndex = 0;
            Session["dtCantoresFase"] = null;
            gvFase.DataSource = null;
            gvFase.DataBind();

            cdProxFase.SelectedIndex = 0;
            Session["dtCantoresProxFase"] = null;
            gvProxFase.DataSource = null;
            gvProxFase.DataBind();
        }

    }
}