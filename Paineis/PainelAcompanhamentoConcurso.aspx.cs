using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wappKaraoke.Classes;
using wappKaraoke.Classes.Mensagem;
using wappKaraoke.Classes.Controller;
using wappKaraoke.Classes.Model.CantoresFases;
using wappKaraoke.Classes.Model.Concursos;
using wappKaraoke.Classes.Model.TipoStatus;
using System.Data;

namespace wappKaraoke.Paineis
{
    public partial class PainelAcompanhamentoConcurso : csPage
    {
        public override void Page_Load(object sender, EventArgs e)
        {
            ltRefresh.Text = "<meta http-equiv=\"refresh\" content=\"" + 
                wappKaraoke.Properties.Settings.Default.sTempoAtualizaçãoPainel + "\" />";

            conConcursos objConConcursos = new conConcursos();
            objConConcursos.objCoConcursos.LimparAtributos();
            objConConcursos.objCoConcursos.strFiltro = " WHERE flConcursoCorrente = 'S'";

            Session["cdConcursoCorrente"] = null;

            if (conConcursos.Select())
            {
                if (objConConcursos.dtDados != null && objConConcursos.dtDados.Rows.Count > 0)
                {
                    Session["cdConcursoCorrente"] = objConConcursos.dtDados.Rows[0][caConcursos.cdConcurso].ToString();
                    Session["cdFaseCorrente"] = objConConcursos.dtDados.Rows[0][caConcursos.cdFaseCorrente].ToString();
                }
            }

            if (Session["cdConcursoCorrente"] == null)
            {
                ltMensagem.Text = MostraMensagem("Falha!", "Não foi possível localizar o concurso corrente.", csMensagem.msgDanger);
                return;
            }

            if (Session["cdFaseCorrente"] == null)
            {
                ltMensagem.Text = MostraMensagem("Falha!", "Não foi possível localizar a fase corrente.", csMensagem.msgDanger);
                return;
            }

            conCantoresFases objConCantoresFases = new conCantoresFases();
            objConCantoresFases.objCoCantoresFases.LimparAtributos();
            objConCantoresFases.objCoCantoresFases.cdConcurso = Convert.ToInt32(Session["cdConcursoCorrente"].ToString());
            objConCantoresFases.objCoCantoresFases.cdFase = Convert.ToInt32(Session["cdFaseCorrente"].ToString());

            if (!conCantoresFases.SelectPainelAcompanhamentoConcurso())
            {
                ltMensagem.Text = MostraMensagem("Falha!", "Não foi possível carregar o acompanhamento do concurso corrente.", csMensagem.msgDanger);
                return;
            }

            gvAcompanhamentoConcurso.AutoGenerateColumns = false;
            gvAcompanhamentoConcurso.DataSource = objConCantoresFases.dtDados;
            gvAcompanhamentoConcurso.DataBind();

            for (int i = 0; i < objConCantoresFases.dtDados.Rows.Count; i++)
            {
                ((Literal)gvAcompanhamentoConcurso.Rows[i].FindControl("ltCantorKanji")).Text = @"" +
                    objConCantoresFases.dtDados.Rows[i]["nmCantor"].ToString() +
                    " <br/> " + objConCantoresFases.dtDados.Rows[i]["nmNomeKanji"].ToString();
                ((Literal)gvAcompanhamentoConcurso.Rows[i].FindControl("ltMusicaKanji")).Text = @"" +
                    objConCantoresFases.dtDados.Rows[i]["nmMusica"].ToString() +
                    " <br/> " + objConCantoresFases.dtDados.Rows[i]["nmMusicaKanji"].ToString();

                PintaLinha(gvAcompanhamentoConcurso.Rows[i], objConCantoresFases.dtDados.Rows[i]["deCor"].ToString());
            }

            Session["_dtCantoresFases"] = objConCantoresFases.dtDados;

            gvAcompanhamentoConcurso.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        private void PintaLinha(GridViewRow gvRow, string strCor)
        {
            if (strCor == "AZUL")
            {
                gvRow.BackColor = System.Drawing.Color.LightBlue;
            }
            else if (strCor == "VERDE")
            {
                gvRow.BackColor = System.Drawing.Color.LightGreen;
            }
            else if (strCor == "VERMELHO")
            {
                gvRow.BackColor = System.Drawing.Color.Salmon;
            }
            else if (strCor == "AMARELO")
            {
                gvRow.BackColor = System.Drawing.Color.Yellow;
            }
            else if (strCor == "LARANJADO")
            {
                gvRow.BackColor = System.Drawing.Color.Orange;
            }
            else if (strCor == "BRANCO")
            {
                gvRow.BackColor = System.Drawing.Color.White;
            }
        }
    }
}