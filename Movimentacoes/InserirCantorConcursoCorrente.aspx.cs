using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wappKaraoke.Classes;
using wappKaraoke.Classes.Controller;
using wappKaraoke.Classes.Model.Concursos;
using wappKaraoke.Classes.Mensagem;
using System.Data;

namespace wappKaraoke.Movimentacoes
{
    public partial class InserirCantorConcursoCorrente : csPageDefault
    {
        private DataTable dtCantoresCategoria;

        public override void Page_Load(object sender, EventArgs e)
        {
            ltMensagemDefault = ltMensagem;

            if (!this.IsPostBack)
            {
                InicializaPagina();
            }

            base.Page_Load(sender, e);
        }

        protected override bool ConfigurarGridView()
        {
            try
            {
                gvCantores.HeaderRow.TableSection = TableRowSection.TableHeader;

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void InicializaPagina()
        {
            ltMensagem.Text = "";

            cdFase.DataSource = null;
            cdCategoria.DataSource = null;
            cdCantor.DataSource = null;
            cdMusica.DataSource = null;
            cdTpStatus.DataSource = null;
            nuCantor.Text = "";

            PegarChaveConcurso();
            CarregarDDL();

            Session["dtCantoresCategoria"] = null;
            gvCantores.DataSource = null;
            gvCantores.DataBind();
        }

        private void CarregarDDL()
        {
            csFases vcsFases = new csFases();
            cdFase = vcsFases.CarregaDDL(cdFase);

            if (Session["cdFaseCorrente"] != null)
                cdFase.SelectedValue = Session["cdFaseCorrente"].ToString();

            csCategorias vcsCategorias = new csCategorias();
            cdCategoria = vcsCategorias.CarregaDDL(cdCategoria);

            csCantores vcsCantores = new csCantores();
            cdCantor = vcsCantores.CarregaDDL(cdCantor);

            csMusicas vcsMusicas = new csMusicas();
            cdMusica = vcsMusicas.CarregaDDL(cdMusica);

            csStatus vcsStatus = new csStatus();
            cdTpStatus = vcsStatus.CarregaDDL(cdTpStatus);

            cdTpStatus.SelectedValue = wappKaraoke.Properties.Settings.Default.sCodStatusInicial;
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

        protected void cdCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarCantoresFasesCategoria();
        }

        private void CarregarCantoresFasesCategoria()
        {
            if (cdCategoria.SelectedIndex > 0)
            {
                conCantoresFases objConCantoresFases = new conCantoresFases();
                objConCantoresFases.objCoCantoresFases.LimparAtributos();
                objConCantoresFases.objCoCantoresFases.cdConcurso = Convert.ToInt32(Session["cdConcurso"]);
                objConCantoresFases.objCoCantoresFases.cdFase = Convert.ToInt32(Session["cdFaseCorrente"]);
                objConCantoresFases.objCoCantoresFases.cdCategoria = Convert.ToInt32(cdCategoria.SelectedValue);

                if (!conCantoresFases.SelectCantoresCategoriasConcurso())
                {
                    ltMensagem.Text = MostraMensagem("Falha!", "Não foi possível carregar os cantores desta categoria.", csMensagem.msgDanger);
                }

                if (objConCantoresFases.dtDados != null && objConCantoresFases.dtDados.Rows.Count == 0)
                {
                    ltMensagem.Text = MostraMensagem("Aviso!", "Não foram localizados cantores desta categoria.", csMensagem.msgWarning);
                }

                dtCantoresCategoria = objConCantoresFases.dtDados;
                OrdenaDataTable(ref dtCantoresCategoria, "nuOrdemApresentacao");
                Session["dtCantoresCategoria"] = dtCantoresCategoria;
                gvCantores.DataSource = dtCantoresCategoria;
                gvCantores.DataBind();

                NomeKanji();
                ConfigurarGridView();
            }
        }

        protected void NomeKanji()
        {
            if (Session["dtCantoresCategoria"] != null)
            {
                dtCantoresCategoria = (DataTable)Session["dtCantoresCategoria"];

                for (int i = 0; i < dtCantoresCategoria.Rows.Count; i++)
                {
                    ((Literal)gvCantores.Rows[i].FindControl("ltNomeKanji")).Text = @"" + dtCantoresCategoria.Rows[i]["nmCantor"].ToString() +
                        " <br/> " + dtCantoresCategoria.Rows[i]["nmNomeKanji"].ToString();

                    ((Literal)gvCantores.Rows[i].FindControl("ltMusicaKanji")).Text = @"" + dtCantoresCategoria.Rows[i]["nmMusica"].ToString() +
                        " <br/> " + dtCantoresCategoria.Rows[i]["nmMusicaKanji"].ToString();
                }
            }
        }

        protected void nuCantor_TextChanged(object sender, EventArgs e)
        {
            //Consultar pelo número no concurso/fase corrente
            //Caso encontre:
                //Carregar em uma caixa do tipo INFO os dados encontrados, Categoria, ordem apres, nuCantor, Status, nome cantor
                //perguntar se deve alterá-lo para os dados informados acima
                //Exibir botões, sim e não (criar novo mostra mensagem onde conterá os botões e os clicks retornarão a opção escolhida);
                    //Se sim, alterar o cdCantor para o encontrado
                    //Se não, limpar o número do cantor
            //Caso não exista:
                //Consultar pelo cdCantor no concurso/fase corrente e categria selecionada 
                //Caso encontre:
                    //Carregar em uma caixa do tipo INFO os dados encontrados, Categoria, ordem apres, nuCantor, Status, nome cantor
                    //perguntar se deve alterá-lo para os dados informados acima
                    //Exibir botões, sim e não (criar novo mostra mensagem onde conterá os botões e os clicks retornarão a opção escolhida);
                        //Se sim, trocar o número do cantor para o encontrado
                        //Se não, limpar os dados
        }

        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (cdFase.SelectedIndex == 0)
            {
                ltMensagem.Text = MostraMensagem("Validação!", "Selecione a fase.", csMensagem.msgWarning);
                return;
            }

            if (cdCategoria.SelectedIndex == 0)
            {
                ltMensagem.Text = MostraMensagem("Validação!", "Selecione a categoria.", csMensagem.msgWarning);
                return;
            }

            if (cdCantor.SelectedIndex == 0)
            {
                ltMensagem.Text = MostraMensagem("Validação!", "Selecione o cantor.", csMensagem.msgWarning);
                return;
            }

            if (cdMusica.SelectedIndex == 0)
            {
                ltMensagem.Text = MostraMensagem("Validação!", "Selecione a música.", csMensagem.msgWarning);
                return;
            }

            if (cdTpStatus.SelectedIndex == 0)
            {
                ltMensagem.Text = MostraMensagem("Validação!", "Selecione o status.", csMensagem.msgWarning);
                return;
            }

            if (nuCantor.Text.Trim() == "")
            {
                ltMensagem.Text = MostraMensagem("Validação!", "Informe o número do cantor.", csMensagem.msgWarning);
                return;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            InicializaPagina();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {

        }
    }
}