using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

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
            return "<table class=\"footable table table-bordered table-hover\" cellspacing=\"0\" rules=\"all\" border=\"1\" id=\"gvCantores_" + strIdGV + "\" style=\"border-collapse:collapse;\">"
                    + "<thead>"
                    + "<tr class=\"info\">"
                    + "  <th data-hide=\"phone\" scope=\"col\">Cód.</th>" //Cód. Cantor
                    + "  <th data-class=\"expand\" scope=\"col\">Cantor</th>" //Nome Cantor
                    + "  <th data-hide=\"all\" scope=\"col\">Cód. Associação</th>"//Cód. Associação
                    + "  <th data-hide=\"all\" scope=\"col\">Associação</th>" //Nome Associação
                    + "  <th data-hide=\"all\" scope=\"col\">Cód. Música</th>"//Cód. Música
                    + "  <th data-hide=\"all\" scope=\"col\">Música</th>" //Nome Música
                    + "  <th data-hide=\"all\" scope=\"col\">Cód. Fase</th>"//Cód. Fase
                    + "  <th data-hide=\"all\" scope=\"col\">Fase</th>" //Nome Fase
                    + "  <th data-hide=\"all\" scope=\"col\">Cód. Status</th>"//Cód. Status
                    + "  <th data-hide=\"all\" scope=\"col\">Status</th>" //Nome Status
                    + "  <th data-hide=\"phone\" scope=\"col\">&nbsp;</th>" //Editar
                    + "  <th data-hide=\"phone\" scope=\"col\">&nbsp;</th>" //Excluir
                    + "</tr>"
                    + "</thead><tbody>"
                    + MontaLinhasGridView()
                    + "</tbody>"
                    + "</table>";
        }

        public string MontaLinhasGridView()
        {
            string strLinhas = "<tr>";

            foreach (DataRow dr in _dtDados.Rows)
            {
                //Cantor
                strLinhas += "<td style=\"width:5%;\">" + dr["cdCantor"] + "</td>";
                strLinhas += "<td>" + dr["nmCantor"] + "</td>";

                //Associação
                strLinhas += "<td style=\"width:5%;\">" + dr["cdAssociacao"] + "</td>";
                strLinhas += "<td>" + dr["nmAssociacao"] + "</td>";

                //Fase
                strLinhas += "<td style=\"width:5%;\">" + dr["cdFase"] + "</td>";
                strLinhas += "<td>" + dr["deFase"] + "</td>";

                //Música
                strLinhas += "<td style=\"width:5%;\">" + dr["cdMusica"] + "</td>";
                strLinhas += "<td>" + dr["nmMusica"] + "</td>";

                //Status
                strLinhas += "<td style=\"width:5%;\">" + dr["cdStatus"] + "</td>";
                strLinhas += "<td>" + dr["deStatus"] + "</td>";

                //Buttons
                strLinhas += "<td style=\"width:15%;\"><input type=\"button\" value=\"Editar\" onclick=\"javascript:__doPostBack(&#39;ctl00$ContentPlaceHolder1$gvAssociacoes&#39;,&#39;Select$1&#39;)\" class=\"btn btn-primary btn-block\" /></td>";
                strLinhas += "<td style=\"width:15%;\"><input type=\"button\" value=\"Excluir\" onclick=\"javascript:__doPostBack(&#39;ctl00$ContentPlaceHolder1$gvAssociacoes&#39;,&#39;Select$1&#39;)\" class=\"btn btn-primary btn-block btn-danger\" /></td>";
            }


            //        "<td style="width:5%;">0</td><td>Nome Associa&#231;&#227;o de teste - 0</td>
            //        "<td>Nome Representante de teste - 0</td><td>emailteste0@hotmail.com</td><td style="width:15%;"><input type="button" value="Editar" onclick="javascript:__doPostBack(&#39;ctl00$ContentPlaceHolder1$gvAssociacoes&#39;,&#39;Select$0&#39;)" class="btn btn-primary btn-block" /></td><td style="width:15%;"><input type="button" value="Excluir" onclick="javascript:__doPostBack(&#39;ctl00$ContentPlaceHolder1$gvAssociacoes&#39;,&#39;Select$0&#39;)" class="btn btn-primary btn-block btn-danger" /></td>"
            //    "</tr><tr>"
            //        "<td style="width:5%;">1</td><td>Nome Associa&#231;&#227;o de teste - 1</td><td>Nome Representante de teste - 1</td><td>emailteste1@hotmail.com</td><td style="width:15%;"><input type="button" value="Editar" onclick="javascript:__doPostBack(&#39;ctl00$ContentPlaceHolder1$gvAssociacoes&#39;,&#39;Select$1&#39;)" class="btn btn-primary btn-block" /></td><td style="width:15%;"><input type="button" value="Excluir" onclick="javascript:__doPostBack(&#39;ctl00$ContentPlaceHolder1$gvAssociacoes&#39;,&#39;Select$1&#39;)" class="btn btn-primary btn-block btn-danger" /></td>"
            //    "</tr><tr>"
            //        "<td style="width:5%;">2</td><td>Nome Associa&#231;&#227;o de teste - 2</td><td>Nome Representante de teste - 2</td><td>emailteste2@hotmail.com</td><td style="width:15%;"><input type="button" value="Editar" onclick="javascript:__doPostBack(&#39;ctl00$ContentPlaceHolder1$gvAssociacoes&#39;,&#39;Select$2&#39;)" class="btn btn-primary btn-block" /></td><td style="width:15%;"><input type="button" value="Excluir" onclick="javascript:__doPostBack(&#39;ctl00$ContentPlaceHolder1$gvAssociacoes&#39;,&#39;Select$2&#39;)" class="btn btn-primary btn-block btn-danger" /></td>"
            //    "</tr><tr>"
            //        "<td style="width:5%;">3</td><td>Nome Associa&#231;&#227;o de teste - 3</td><td>Nome Representante de teste - 3</td><td>emailteste3@hotmail.com</td><td style="width:15%;"><input type="button" value="Editar" onclick="javascript:__doPostBack(&#39;ctl00$ContentPlaceHolder1$gvAssociacoes&#39;,&#39;Select$3&#39;)" class="btn btn-primary btn-block" /></td><td style="width:15%;"><input type="button" value="Excluir" onclick="javascript:__doPostBack(&#39;ctl00$ContentPlaceHolder1$gvAssociacoes&#39;,&#39;Select$3&#39;)" class="btn btn-primary btn-block btn-danger" /></td>"

            strLinhas += "</tr>";

            return strLinhas;
        }
    }
}