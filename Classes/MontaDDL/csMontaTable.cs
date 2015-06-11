using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using wappKaraoke.Classes.Model.CantoresFases;
using wappKaraoke.Classes.Model.Associacoes;

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
                    //+ "  <th data-hide=\"phone\" scope=\"col\" visible=\"false\">Cód.</th>" //Cód. Cantor
                    + "  <th data-class=\"expand\" scope=\"col\">Cantor</th>" //Nome Cantor
                    //+ "  <th data-hide=\"all\" scope=\"col\" visible=\"false\">Cód. Associação</th>"//Cód. Associação
                    + "  <th data-hide=\"all\" scope=\"col\">Associação</th>" //Nome Associação
                    //+ "  <th data-hide=\"all\" scope=\"col\" visible=\"false\">Cód. Música</th>"//Cód. Música
                    + "  <th data-hide=\"all\" scope=\"col\">Música</th>" //Nome Música
                    //+ "  <th data-hide=\"all\" scope=\"col\" visible=\"false\">Cód. Fase</th>"//Cód. Fase
                    //+ "  <th data-hide=\"all\" scope=\"col\" visible=\"false\">Fase</th>" //Nome Fase
                    //+ "  <th data-hide=\"all\" scope=\"col\" visible=\"false\">Cód. Status</th>"//Cód. Status
                    //+ "  <th data-hide=\"all\" scope=\"col\" visible=\"false\">Status</th>" //Nome Status
                    + "  <th data-hide=\"phone\" scope=\"col\">&nbsp;</th>" //Editar
                    + "  <th data-hide=\"phone\" scope=\"col\">&nbsp;</th>" //Excluir
                    + "</tr>"
                    + "</thead><tbody>"
                    + MontaLinhasGridView("gvCantores_" + strIdGV)
                    + "</tbody>"
                    + "</table>";
        }

        public string MontaLinhasGridView(string strIdGV)
        {
            string strLinhas = "";

            foreach (DataRow dr in _dtDados.Rows)
            {
                strLinhas += "<tr>";
                //Cantor
                //strLinhas += "<td style=\"width:5%;\">" + dr["cdCantor"] + "</td>";
                strLinhas += "<td>" + dr[caCantoresFases.CC_nmCantor] + "</td>";

                //Associação
                //strLinhas += "<td style=\"width:5%;\">" + dr["cdAssociacao"] + "</td>";
                //strLinhas += "<td>" + dr["CC_nmAssociacao"] + "</td>";

                //Fase
                //strLinhas += "<td style=\"width:5%;\">" + dr["cdFase"] + "</td>";
                //strLinhas += "<td>" + dr["deFase"] + "</td>";

                //Música
                //strLinhas += "<td style=\"width:5%;\">" + dr["cdMusica"] + "</td>";
                strLinhas += "<td>" + dr[caCantoresFases.CC_nmMusica] + "</td>";

                //Status
                //strLinhas += "<td style=\"width:5%;\">" + dr["cdStatus"] + "</td>";
                //strLinhas += "<td>" + dr["deStatus"] + "</td>";

                //Buttons
                strLinhas += "<td style=\"width:15%;\"><input type=\"button\" value=\"Editar\" onclick=\"javascript:__doPostBack(&#39;ctl00$ContentPlaceHolder1$" + strIdGV + "&#39;,&#39;Select$1&#39;)\" class=\"btn btn-primary btn-block\" /></td>";
                strLinhas += "<td style=\"width:15%;\"><input type=\"button\" value=\"Excluir\" onclick=\"javascript:__doPostBack(&#39;ctl00$ContentPlaceHolder1$" + strIdGV + "&#39;,&#39;Select$1&#39;)\" class=\"btn btn-primary btn-block btn-danger\" /></td>";

                strLinhas += "</tr>";
            }

            return strLinhas;
        }

        public DataTable RetornaDTCantores()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("cdCantor", typeof(int));
            dt.Columns.Add("CC_nmCantor", typeof(string));
            dt.Columns.Add("cdAssociacao", typeof(int));
            dt.Columns.Add("nmAssociacao", typeof(string));
            dt.Columns.Add("cdFase", typeof(int));
            dt.Columns.Add("CC_deFase", typeof(string));
            dt.Columns.Add("cdMusica", typeof(int));
            dt.Columns.Add("CC_nmMusica", typeof(string));
            dt.Columns.Add("cdStatus", typeof(int));
            dt.Columns.Add("CC_deStatus", typeof(string));

            return dt;
        }
    }
}