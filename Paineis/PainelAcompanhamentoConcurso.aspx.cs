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
using wappKaraoke.Classes.Paginas_Default;
using wappKaraoke.Classes.Model.Categorias;
using wappKaraoke.Classes.Model.ConcursosOrdemCategorias;

namespace wappKaraoke.Paineis
{
    public partial class PainelAcompanhamentoConcurso : csPainelAcompanhamento
    {
        public override void Page_Load(object sender, EventArgs e)
        {
            string strMensagemErro = "";
            DataTable dtConcursoOrdemCategoria;

            base.Page_Load(sender, e);

            if (!ValidaConcursoFaseCorrente(out strMensagemErro))
            {
                ltMensagem.Text = strMensagemErro;
                return;
            }

            ltRefresh.Text = "<meta http-equiv=\"refresh\" content=\"" + 
                wappKaraoke.Properties.Settings.Default.sTempoAtualizaçãoPainel + "\" />";

            if (!CarregarCategoriasConcursos(out strMensagemErro, out dtConcursoOrdemCategoria))
            {
                ltMensagem.Text = strMensagemErro;
                return;
            }

            MontarPainelAcompanhamento(dtConcursoOrdemCategoria);
        }

        private void MontarPainelAcompanhamento(DataTable dtConcursoOrdemCategoria)
        {
            string strPainelCompleto = "";
            ltListaCantores.Text = "";

            foreach (DataRow dr in dtConcursoOrdemCategoria.Rows)
            {
                strPainelCompleto = strPainel.Replace("[deCategoria]", dr[caConcursosOrdemCategorias.CC_deCategoria].ToString());

                Session["cdCategoriaPainel"] = Convert.ToInt32(dr[caConcursosOrdemCategorias.cdCategoria].ToString());

                strPainelCompleto = strPainelCompleto.Replace("[TableCantores]", MontarTabelaCantores());

                ltListaCantores.Text += strPainelCompleto;
            }
        }

        private string  MontarTabelaCantores()
        {
            string strTabelaCantores = "";

            conCantoresFases objConCantoresFases = new conCantoresFases();
            objConCantoresFases.objCoCantoresFases.LimparAtributos();
            objConCantoresFases.objCoCantoresFases.cdConcurso = Convert.ToInt32(Session["cdConcursoCorrentePainel"]);
            objConCantoresFases.objCoCantoresFases.cdFase = Convert.ToInt32(Session["cdFaseCorrentePainel"]);
            objConCantoresFases.objCoCantoresFases.cdCategoria = Convert.ToInt32(Session["cdCategoriaPainel"]);

            if (!conCantoresFases.SelectPainelAcompanhamentoConcurso())
            {
                ltMensagem.Text = MostraMensagem("Falha!", "Não foi possível carregar o acompanhamento do concurso corrente.", csMensagem.msgDanger); ;
                return "";
            }

            if (objConCantoresFases.dtDados != null && objConCantoresFases.dtDados.Rows.Count > 0)
            {
                strTabelaCantores = MontaEstruturaTabela(objConCantoresFases.dtDados);
            }

            return strTabelaCantores;
        }
    }
}