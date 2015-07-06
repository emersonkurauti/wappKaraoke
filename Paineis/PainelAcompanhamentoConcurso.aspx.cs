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
        private DataTable _dtCantoresFases;

        public override void Page_Load(object sender, EventArgs e)
        {
            conConcursos objConConcursos = new conConcursos();
            objConConcursos.objCoConcursos.LimparAtributos();
            objConConcursos.objCoConcursos.strFiltro = " WHERE flConcursoCorrente = 'S'";

            Session["cdConcursoCorrente"] = null;

            if (conConcursos.Select())
            {
                if (objConConcursos.dtDados != null && objConConcursos.dtDados.Rows.Count > 0)
                {
                    Session["cdConcursoCorrente"] = objConConcursos.dtDados.Rows[0][caConcursos.cdConcurso].ToString();
                }
            }

            if (Session["cdConcursoCorrente"] == null)
            {
                ltMensagem.Text = MostraMensagem("Falha!", "Não foi possível localizar o concurso corrente.", csMensagem.msgDanger);
                return;
            }

            conCantoresFases objConCantoresFases = new conCantoresFases();
            objConCantoresFases.objCoCantoresFases.LimparAtributos();
            objConCantoresFases.objCoCantoresFases.cdConcurso = Convert.ToInt32(Session["cdConcursoCorrente"].ToString());

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
            }

            Session["_dtCantoresFases"] = objConCantoresFases.dtDados;

            gvAcompanhamentoConcurso.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void gvAcompanhamentoConcurso_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Session["_dtCantoresFases"] != null)
                {
                    _dtCantoresFases = (DataTable)Session["_dtCantoresFases"];

                    foreach (DataRow dr in _dtCantoresFases.Rows)
                    {
                        if (DataBinder.Eval(e.Row.DataItem, "nuCantor").ToString() == dr[caCantoresFases.nuCantor].ToString())
                        {
                            if (dr[caTipoStatus.deCor].ToString() == "AZUL")
                            {
                                e.Row.BackColor = System.Drawing.Color.LightBlue;
                            }
                            else if (dr[caTipoStatus.deCor].ToString() == "VERDE")
                            {
                                e.Row.BackColor = System.Drawing.Color.LightGreen;
                            }
                            else if (dr[caTipoStatus.deCor].ToString() == "VERMELHO")
                            {
                                e.Row.BackColor = System.Drawing.Color.Salmon;
                            }
                            else if (dr[caTipoStatus.deCor].ToString() == "AMARELO")
                            {
                                e.Row.BackColor = System.Drawing.Color.Yellow;
                            }
                            else if (dr[caTipoStatus.deCor].ToString() == "LARANJADO")
                            {
                                e.Row.BackColor = System.Drawing.Color.Orange;
                            }
                        }
                    }
                }
            }
        }
    }
}