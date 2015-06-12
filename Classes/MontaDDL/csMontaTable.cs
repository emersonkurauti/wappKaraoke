using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using wappKaraoke.Classes.Controller;
using wappKaraoke.Classes.Model.CantoresFases;
using wappKaraoke.Classes.Model.Associacoes;
using wappKaraoke.Classes.Model.Cantores;
using wappKaraoke.Classes.Model.Musicas;


namespace wappKaraoke.Classes
{
    public class csMontaTable
    {
        private DataTable _dtDados;
        public DataTable dtDados
        {
            get { return _dtDados; }
            set { _dtDados = value; }
        }

        public string MontaDataGridView(string strIdGV)
        {
            return "<table class=\"footable table table-bordered table-hover default footable-loaded\" cellspacing=\"0\" rules=\"all\" border=\"1\" id=\"gvCantores_" + strIdGV + "\" style=\"border-collapse:collapse;\">"
                 + "  <thead>"
                 + "    <tr class=\"info\">"
                 + "      <th data-hide=\"all\" scope=\"col\" visible=\"false\">Cód. Cantor</th>" //Cód. Cantor
                 + "      <th data-class=\"expand\" scope=\"col\">Cantor</th>" //Nome Cantor com Kanji
                 + "      <th data-hide=\"all\" scope=\"col\">Cód. Associação</th>"//Cód. Associação
                 + "      <th data-hide=\"all\" scope=\"col\">Associação</th>" //Nome Associação
                 + "      <th data-hide=\"all\" scope=\"col\">Cód. Música</th>"//Cód. Música
                 + "      <th data-hide=\"all\" scope=\"col\">Música</th>" //Nome Música com kanji
                 + "      <th data-hide=\"phone\" scope=\"col\">&nbsp;</th>" //Editar
                 + "      <th data-hide=\"phone\" scope=\"col\">&nbsp;</th>" //Excluir
                 + "    </tr>"
                 + "  </thead>"
                 + "  <tbody>"
                 + MontaLinhasGridView("gvCantores_" + strIdGV)
                 + "  </tbody>"
                 + "</table>";
        }

        public string MontaLinhasGridView(string strIdGV)
        {
            string strLinhas = "";

            foreach (DataRow dr in _dtDados.Rows)
            {
                strLinhas += "<tr>";
                //Cantor
                strLinhas += "<td style=\"width:5%;\">" + dr[caCantores.cdCantor] + "</td>";
                strLinhas += "<td class=\"expand\">" + dr[caCantores.nmCantor] + "<br/>" + dr[caCantores.nmNomeKanji] + "</td>";

                //Associação
                strLinhas += "<td style=\"width:5%;\">" + dr[caAssociacoes.cdAssociacao] + "</td>";
                strLinhas += "<td>" + dr[caAssociacoes.nmAssociacao] + "</td>";

                //Música
                strLinhas += "<td style=\"width:5%;\">" + dr[caMusicas.cdMusica] + "</td>";
                strLinhas += "<td>" + dr[caMusicas.nmMusica] + "<br/>" + dr[caMusicas.nmMusicaKanji] + "</td>";

                //Buttons
                strLinhas += "<td style=\"width: 5%;\">" +
                             "  <a id=\"gvCantores_lnkEdit_" + strIdGV + "\" class=\"btn btn-primary btn-block phone footable-loaded\"" +
                             "    href=\"javascript:__doPostBack('ctl00$ContentPlaceHolder1$gvDados$ctl02$lnkEdit','')\">" +
                             "    <i class=\"glyphicon glyphicon-edit\" aria-hidden=\"true\">" +
                             "    </i>" +
                             "  </a>" +
                             "</td>";
                strLinhas += "<td style=\"width: 5%; display: table-cell;\">" +
                             "  <a id=\"gvCantores_lnkDelete_" + strIdGV + "\" class=\"btn btn-primary btn-block btn-danger footable-loaded phone\"" +
                             "    href=\"javascript:__doPostBack('ctl00$ContentPlaceHolder1$gvGrupoJuradoConcurso$ctl02$lnkDelete','')\">" +
                             "    <i class=\"glyphicon glyphicon-trash\"></i>" +
                             "  </a>" +
                             "</td>";
                
                strLinhas += "</tr>";
            }

            return strLinhas;
        }

        public DataTable RetornaDTCantores()
        {
            return conCantoresFases.objCo.RetornaEstruturaDtCantoresFasesConcursos();
        }
    }
}