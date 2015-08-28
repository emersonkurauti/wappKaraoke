using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using wappKaraoke.Classes.Controller;
using wappKaraoke.Classes.Model.Concursos;
using wappKaraoke.Classes.Mensagem;
using System.Data;
using wappKaraoke.Classes.Model.CantoresFases;
using wappKaraoke.Classes.Model.Cantores;
using wappKaraoke.Classes.Model.Associacoes;
using wappKaraoke.Classes.Model.Musicas;
using wappKaraoke.Classes.Model.TipoStatus;

namespace wappKaraoke.Classes.Paginas_Default
{
    public class csPainelAcompanhamento : csPage
    {
        public string strPainel =
            "                <div class=\"panel panel-primary\">" +
            "                    <div class=\"panel-heading\">"+
            "                        <span class=\"panel-title\">[deCategoria]</span>"+
            "                    </div>"+
            "                    <div class=\"panel-body\">"+
            "                        <div class=\"row\">"+
            "                            <div class=\"col-md-12\">"+
            "                              [TableCantores]"+
            "                            </div>"+
            "                        </div>"+
            "                    </div>"+
            "                </div>";

        public override void Page_Load(object sender, EventArgs e)
        {
            CarregaConcursoFaseCorrente();
        }

        protected bool CarregarCantoresFases(out string strMensagemErro, out DataTable dtDados)
        {
            strMensagemErro = "";
            dtDados = null;

            conCantoresFases objConCantoresFases = new conCantoresFases();
            objConCantoresFases.objCoCantoresFases.LimparAtributos();
            objConCantoresFases.objCoCantoresFases.cdConcurso = Convert.ToInt32(Session["cdConcursoCorrentePainel"].ToString());
            objConCantoresFases.objCoCantoresFases.cdFase = Convert.ToInt32(Session["cdFaseCorrentePainel"].ToString());
            objConCantoresFases.objCoCantoresFases.cdCategoria = Convert.ToInt32(Session["cdCategoriaPainel"].ToString());

            if (!conCantoresFases.SelectPainelAcompanhamentoConcurso())
            {
                strMensagemErro = MostraMensagem("Falha!", "Não foi possível carregar o acompanhamento do concurso corrente.", csMensagem.msgDanger);
                return false;
            }

            dtDados = objConCantoresFases.dtDados;
            return true;
        }

        protected bool CarregarCategoriasConcursos(out string strMensagemErro, out DataTable dtDados)
        {
            strMensagemErro = "";
            dtDados = null;

            conConcursosOrdemCategorias objConConcursosOrdemCategorias = new conConcursosOrdemCategorias();
            objConConcursosOrdemCategorias.objCoConcursosOrdemCategorias.LimparAtributos();
            objConConcursosOrdemCategorias.objCoConcursosOrdemCategorias.cdConcurso = Convert.ToInt32(Session["cdConcursoCorrentePainel"].ToString());
            
            if (!conConcursosOrdemCategorias.Select())
            {
                strMensagemErro = MostraMensagem("Falha!", "Não foi possível carregar o acompanhamento do concurso corrente.", csMensagem.msgDanger);
                return false;
            }

            dtDados = objConConcursosOrdemCategorias.dtDados;

            OrdenaDataTable(ref dtDados, " nuOrdem");

            return true;
        }

        protected void CarregaConcursoFaseCorrente()
        {
            conConcursos objConConcursos = new conConcursos();
            objConConcursos.objCoConcursos.LimparAtributos();
            objConConcursos.objCoConcursos.strFiltro = " WHERE flConcursoCorrente = 'S'";

            Session["cdConcursoCorrentePainel"] = null;

            if (conConcursos.Select())
            {
                if (objConConcursos.dtDados != null && objConConcursos.dtDados.Rows.Count > 0)
                {
                    Session["cdConcursoCorrentePainel"] = objConConcursos.dtDados.Rows[0][caConcursos.cdConcurso].ToString();
                    Session["cdFaseCorrentePainel"] = objConConcursos.dtDados.Rows[0][caConcursos.cdFaseCorrente].ToString();
                }
            }
        }

        protected bool ValidaConcursoFaseCorrente(out string strMensagemErro)
        {
            strMensagemErro = "";

            if (Session["cdConcursoCorrentePainel"] == null)
            {
                strMensagemErro = MostraMensagem("Falha!", "Não foi possível localizar o concurso corrente.", csMensagem.msgDanger);
                return false;
            }

            if (Session["cdFaseCorrentePainel"] == null)
            {
                strMensagemErro = MostraMensagem("Falha!", "Não foi possível localizar a fase corrente.", csMensagem.msgDanger);
                return false;
            }

            return true;
        }

        public string MontaEstruturaTabela(DataTable dtCantoresFases)
        {
            return "<table class=\"footable table table-bordered table-hover default footable-loaded\" cellspacing=\"0\" rules=\"all\" border=\"1\" id=\"gvCantores_" + Session["cdCategoriaPainel"].ToString() + "\" style=\"border-collapse:collapse;\">"
                 + "  <thead>"
                 + "    <tr class=\"info\">"
                 + "      <th data-hide=\"all\" scope=\"col\" visible=\"false\">Nº Cantor</th>" //nuCantor
                 + "      <th data-class=\"expand\" scope=\"col\">Cantor</th>" //Nome Cantor com Kanji
                 + "      <th data-hide=\"all\" scope=\"col\">Associação</th>" //Nome Associação
                 + "      <th data-hide=\"all\" scope=\"col\">Música</th>" //Nome Música com kanji
                 + "      <th data-hide=\"all\" scope=\"col\">Status</th>" //Status
                 + "    </tr>"
                 + "  </thead>"
                 + "  <tbody>"
                 + MontaLinhasTabela("gvCantores_" + Session["cdCategoriaPainel"].ToString(), Session["cdCategoriaPainel"].ToString(), dtCantoresFases)
                 + "  </tbody>"
                 + "</table>";
        }

        public string MontaLinhasTabela(string strIdGV, string strIDCategoria, DataTable dtCantoresFases)
        {
            string strLinhas = "";

            foreach (DataRow dr in dtCantoresFases.Rows)
            {
                if (dr[caTipoStatus.deCor].ToString() == "AMARELO")
                    strLinhas += "<tr bgColor = #FFD700>";
                else if (dr[caTipoStatus.deCor].ToString() == "AZUL")
                    strLinhas += "<tr bgColor = #87CEFA>";
                else if (dr[caTipoStatus.deCor].ToString() == "BRANCO")
                    strLinhas += "<tr bgColor = #FFFFFF>";
                else if (dr[caTipoStatus.deCor].ToString() == "CINZA")
                    strLinhas += "<tr bgColor = #808080>";
                else if (dr[caTipoStatus.deCor].ToString() == "LARANJADO")
                    strLinhas += "<tr bgColor = #FFA500>";
                else if (dr[caTipoStatus.deCor].ToString() == "VERDE")
                    strLinhas += "<tr bgColor = #00FF7F>";
                else if (dr[caTipoStatus.deCor].ToString() == "VERMELHO")
                    strLinhas += "<tr bgColor = #FF6347>";
                else if (dr[caTipoStatus.deCor].ToString() == "ROXO")
                    strLinhas += "<tr bgColor = #9370DB>";

                //nuCantor
                strLinhas += "<td style=\"width:5%;\"><font color=\"black\">" + dr[caCantoresFases.nuCantor] + "</font></td>";

                //Associação
                strLinhas += "<td><font color=\"black\">" + dr[caAssociacoes.nmAssociacao] + "</font></td>";

                //Cantor
                strLinhas += "<td class=\"expand\"><font color=\"black\">" + dr[caCantores.nmCantor] + "<br/>" + dr[caCantores.nmNomeKanji] + "</font></td>";

                //Música
                strLinhas += "<td><font color=\"black\">" + dr[caMusicas.nmMusica] + "<br/>" + dr[caMusicas.nmMusicaKanji] + "</font></td>";

                //Status
                strLinhas += "<td><font color=\"black\">" + dr[caTipoStatus.deTpStatus] + "</font></td>";

                strLinhas += "</tr>";
            }

            return strLinhas;
        }
    }
}