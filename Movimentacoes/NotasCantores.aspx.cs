using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wappKaraoke.Classes;
using wappKaraoke.Classes.Mensagem;
using wappKaraoke.Classes.Controller;
using wappKaraoke.Classes.Model.Concursos;
using wappKaraoke.Classes.Model.Grupos;
using wappKaraoke.Classes.Model.Cantores;
using wappKaraoke.Classes.Model.Categorias;
using wappKaraoke.Classes.Model.Fases;
using System.Data;

namespace wappKaraoke.Movimentacoes
{
    public partial class NotasCantores : csPage
    {
        public override void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
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

                CarregarProximoCantorSemNota();

                CarregaPainelNotaJurados();
            }
        }

        public void CarregaPainelNotaJurados()
        {
            ltNotasJurados.Text = "";

            conGrupos objConGrupos = new conGrupos();
            objConGrupos.objCoGrupos.LimparAtributos();
            objConGrupos.objCoGrupos.cdConcurso = Convert.ToInt32(Session["cdConcurso"].ToString());

            if (conGrupos.Select())
            {
                if (objConGrupos.dtDados.Rows.Count == 0)
                {
                    ltNotasJurados.Text = MostraMensagem("Validação!", "Não existem jurados no concurso.", csMensagem.msgDanger);
                    return;
                }

                foreach (DataRow dr in objConGrupos.dtDados.Rows)
                {
                    ltNotasJurados.Text += csDinamico.strLinhaNotaJurado.Replace("[CC_nmJurado]", dr[caGrupos.CC_nmJurado].ToString()).Replace("[cdJurado]", dr[caGrupos.cdJurado].ToString());
                }
            }
        }

        private void CarregarProximoCantorSemNota()
        {
            ltMensagem.Text = "";

            conCantoresFases objConCantoresFases = new conCantoresFases();
            objConCantoresFases.objCoCantoresFases.LimparAtributos();
            objConCantoresFases.objCoCantoresFases.cdConcurso = Convert.ToInt32(Session["cdConcurso"].ToString());
            objConCantoresFases.objCoCantoresFases.cdTpStatus = Convert.ToInt32(wappKaraoke.Properties.Settings.Default.sCodStatusCantou);

            if (!conCantoresFases.SelectProximoCantorSemNota())
            {
                ltMensagem.Text = MostraMensagem("Falha!", "Não foi possível carregar o próximo cantor sem nota.", csMensagem.msgDanger);
                return;
            }

            if (objConCantoresFases.dtDados.Rows.Count == 0)
            {
                ltMensagem.Text = MostraMensagem("Validação!", "Não existe cantor pronto para receber sua pontuação.", csMensagem.msgWarning);
                return;
            }

            Session["cdCantor"] = objConCantoresFases.dtDados.Rows[0][caCantores.cdCantor].ToString();
            Session["cdCategoria"] = objConCantoresFases.dtDados.Rows[0][caCategorias.cdCategoria].ToString();
            Session["cdFase"] = objConCantoresFases.dtDados.Rows[0][caFases.cdFase].ToString();

            deFormulaPontuacao.Text = objConCantoresFases.dtDados.Rows[0][caCategorias.deFormulaPontuacao].ToString();

            ltMensagem.Text = MostraMensagem("Detalhes:",
                "Cantor: " + objConCantoresFases.dtDados.Rows[0][caCantores.nmCantor].ToString() + "<br/>" +
                "Categoria: " + objConCantoresFases.dtDados.Rows[0][caCategorias.deCategoria].ToString(),
                csMensagem.msgInfo);
        }

        //Salvar -> Salva e passa o próximo
        //Anterior -> volta
        //Proximo -> vai pro proóximo número
        //Digitar o numero do cantor carrega ele
    }
}