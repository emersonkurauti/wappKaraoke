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
using System.Data;

namespace wappKaraoke.Movimentacoes
{
    public partial class NotasCantores : csPage
    {
        //<div class="row">
        //    <div class="col-sm-6">
        //        <h5 style="text-align: right">Jurado de teste 1</h5>
        //    </div>
        //    <div class="col-sm-6">
        //        <asp:TextBox ID="txtNotaJurado_1" class="form-control" runat="server" 
        //            placeholder="Nota..." Visible="True">
        //        </asp:TextBox>
        //    </div>
        //</div>

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

                //Carregar o cantor com menor ordem de apresentação e sem a nota final
                //Carregar a fórmula da categoria

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

        //Salvar -> Salva e passa o próximo
        //Anterior -> volta
        //Proximo -> vai pro proóximo número
        //Digitar o numero do cantor carrega ele
    }
}