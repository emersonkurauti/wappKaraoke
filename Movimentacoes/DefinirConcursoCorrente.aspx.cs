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

namespace wappKaraoke.Movimentacoes
{
    public partial class DefinirConcursoCorrente : csPage
    {
        public override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                csConcursos vcsConcursos = new csConcursos();
                cdConcurso = vcsConcursos.CarregaDDL(cdConcurso);

                conConcursos objConConcursos = new conConcursos();
                objConConcursos.objCoConcursos.strFiltro = " WHERE flConcursoCorrente = 'S'";

                Session["cdConcursoCorrente"] = null;

                if (conConcursos.Select())
                {
                    if (objConConcursos.dtDados != null && objConConcursos.dtDados.Rows.Count > 0)
                    {
                        cdConcurso.SelectedValue = objConConcursos.dtDados.Rows[0][caConcursos.cdConcurso].ToString();
                        Session["cdConcursoCorrente"] = cdConcurso.SelectedValue;
                    }
                }
            }
        }

        protected void btnDefinirConcursoCorrente_Click(object sender, EventArgs e)
        {
            if (cdConcurso.SelectedIndex > 0)
            {
                conConcursos objConConcursos = new conConcursos();

                if (Session["cdConcursoCorrente"] != null)
                {
                    if (cdConcurso.SelectedValue != Session["cdConcursoCorrente"].ToString())
                    {
                        objConConcursos.objCoConcursos.LimparAtributos();
                        objConConcursos.objCoConcursos.cdConcurso = Convert.ToInt32(Session["cdConcursoCorrente"].ToString());
                        objConConcursos.objCoConcursos.flConcursoCorrente = "N";

                        if (!conConcursos.AlterarConcursoCorrente())
                        {
                            ltMensagem.Text = MostraMensagem("Falha!", "Não foi possível alterar o concurso corrente.", csMensagem.msgDanger);
                            return;
                        }

                        objConConcursos.objCoConcursos.LimparAtributos();
                        objConConcursos.objCoConcursos.cdConcurso = Convert.ToInt32(cdConcurso.SelectedValue.ToString());
                        objConConcursos.objCoConcursos.flConcursoCorrente = "S";

                        if (!conConcursos.AlterarConcursoCorrente())
                        {
                            ltMensagem.Text = MostraMensagem("Falha!", "Não foi possível alterar o concurso corrente.", csMensagem.msgDanger);
                            return;
                        }
                    }
                }
                else
                {
                    objConConcursos.objCoConcursos.LimparAtributos();
                    objConConcursos.objCoConcursos.cdConcurso = Convert.ToInt32(cdConcurso.SelectedValue.ToString());
                    objConConcursos.objCoConcursos.flConcursoCorrente = "S";

                    if (!conConcursos.AlterarConcursoCorrente())
                    {
                        ltMensagem.Text = MostraMensagem("Falha!", "Não foi possível alterar o concurso corrente.", csMensagem.msgDanger);
                        return;
                    }
                }

                Session["cdConcursoCorrente"] = cdConcurso.SelectedValue.ToString();
                ltMensagem.Text = MostraMensagem("Sucesso!", "Concurso corrente definido com sucesso.", csMensagem.msgSucess);
            }
        }

        protected void cdConcurso_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}