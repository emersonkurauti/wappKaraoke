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
        private bool _bExibirbtnEditar = true;
        public bool bExibirbtnEditar
        {
            get { return _bExibirbtnEditar; }
            set { _bExibirbtnEditar = value; }
        }

        private bool _bExibirbtnExcluir = true;
        public bool bExibirbtnExcluir
        {
            get { return _bExibirbtnExcluir; }
            set { _bExibirbtnExcluir = value; }
        }

        private bool _bExibirCodigos = true;
        public bool bExibirCodigos
        {
            get { return _bExibirCodigos; }
            set { _bExibirCodigos = value; }
        }

        private bool _bExibirNotaFinal = false;
        public bool bExibirNotaFinal
        {
            get { return _bExibirNotaFinal; }
            set { _bExibirNotaFinal = value; }
        }

        private bool _bExibirDesconto = false;
        public bool bExibirDesconto
        {
            get { return _bExibirDesconto; }
            set { _bExibirDesconto = value; }
        }

        private DataTable _dtDados;
        public DataTable dtDados
        {
            get { return _dtDados; }
            set { _dtDados = value; }
        }

        public string MontaDataGridView(string strIdGV)
        {
            string sSequencial = "      <th data-hide=\"all\" scope=\"col\" visible=\"false\">Seq.</th>";
            string sCodCantor = "      <th data-hide=\"all\" scope=\"col\" visible=\"false\">Cód. Cantor</th>";
            string sCodAssociacao = "      <th data-hide=\"all\" scope=\"col\">Cód. Associação</th>";
            string sCodMusica = "      <th data-hide=\"all\" scope=\"col\">Cód. Música</th>";
            string sEditar = "      <th data-hide=\"phone\" scope=\"col\">&nbsp;</th>";
            string sExcluir = "      <th data-hide=\"phone\" scope=\"col\">&nbsp;</th>";
            string sNotaFinal = "      <th data-hide=\"all\" scope=\"col\">Nota Final</th>";
            string sDesconto = "      <th data-hide=\"all\" scope=\"col\">% Desconto</th>";

            if (!_bExibirCodigos)
            {
                sSequencial = "";
                sCodCantor = "";
                sCodAssociacao = "";
                sCodMusica = "";
            }

            if (!_bExibirbtnEditar)
                sEditar = "";

            if (!_bExibirbtnExcluir)
                sExcluir = "";

            if (!_bExibirNotaFinal)
                sNotaFinal = "";

            if (!_bExibirDesconto)
                sDesconto = "";

            return "<table class=\"footable table table-bordered table-hover default footable-loaded\" cellspacing=\"0\" rules=\"all\" border=\"1\" id=\"gvCantores_" + strIdGV + "\" style=\"border-collapse:collapse;\">"
                 + "  <thead>"
                 + "    <tr class=\"info\">"
                 + sSequencial
                 + sNotaFinal
                 + sDesconto
                 + "      <th data-hide=\"all\" scope=\"col\" visible=\"false\">Nº Cantor</th>" //nuCantor
                 + sCodCantor
                 + "      <th data-class=\"expand\" scope=\"col\">Cantor</th>" //Nome Cantor com Kanji
                 + sCodAssociacao
                 + "      <th data-hide=\"all\" scope=\"col\">Associação</th>" //Nome Associação
                 + sCodMusica
                 + "      <th data-hide=\"all\" scope=\"col\">Música</th>" //Nome Música com kanji
                 + sEditar
                 + sExcluir
                 + "    </tr>"
                 + "  </thead>"
                 + "  <tbody>"
                 + MontaLinhasGridView("gvCantores_" + strIdGV, strIdGV)
                 + "  </tbody>"
                 + "</table>";
        }

        public string MontaLinhasGridView(string strIdGV, string strIDCategoria)
        {
            string strLinhas = "";

            foreach (DataRow dr in _dtDados.Rows)
            {
                strLinhas += "<tr>";
                //Seq
                if (_bExibirCodigos)
                    strLinhas += "<td style=\"width:5%;\">" + dr[caCantoresFases.nuOrdemApresentacao] + "</td>";

                //Nota Final
                if(_bExibirNotaFinal)
                    strLinhas += "<td style=\"width:5%;\">" + dr[caCantoresFases.nuNotafinal] + "</td>";

                //% Desconto
                if (_bExibirDesconto)
                    strLinhas += "<td style=\"width:5%;\">" + dr[caCantoresFases.pcDesconto] + "</td>";

                //nuCantor
                strLinhas += "<td style=\"width:5%;\">" + dr[caCantoresFases.nuCantor] + "</td>";

                //Cantor
                if (_bExibirCodigos)
                    strLinhas += "<td style=\"width:5%;\">" + dr[caCantores.cdCantor] + "</td>";
                strLinhas += "<td class=\"expand\">" + dr[caCantores.nmCantor] + "<br/>" + dr[caCantores.nmNomeKanji] + "</td>";

                //Associação
                if (_bExibirCodigos)
                    strLinhas += "<td style=\"width:5%;\">" + dr[caAssociacoes.cdAssociacao] + "</td>";
                strLinhas += "<td>" + dr[caAssociacoes.nmAssociacao] + "</td>";

                //Música
                if (_bExibirCodigos)
                    strLinhas += "<td style=\"width:5%;\">" + dr[caMusicas.cdMusica] + "</td>";
                strLinhas += "<td>" + dr[caMusicas.nmMusica] + "<br/>" + dr[caMusicas.nmMusicaKanji] + "</td>";

                //Buttons
                if (_bExibirbtnEditar)
                {
                    strLinhas += "<td style=\"width: 5%;\">" +
                                 "  <a id=\"gvCantores_lnkEdit_" + strIdGV + "\" class=\"btn btn-primary btn-block phone footable-loaded\"" +
                                 "    href=\"javascript:__doPostBack('lnkEditCantor','" +
                                                dr[caCantores.cdCantor].ToString() + ";" + strIDCategoria + ";" + strIdGV + "')\">" +
                                 "    <i class=\"glyphicon glyphicon-edit\" aria-hidden=\"true\">" +
                                 "    </i>" +
                                 "  </a>" +
                                 "</td>";
                }

                if (_bExibirbtnExcluir)
                {
                    strLinhas += "<td style=\"width: 5%; display: table-cell;\">" +
                                 "  <a id=\"gvCantores_lnkDelete_" + strIdGV + "\" class=\"btn btn-primary btn-block btn-danger footable-loaded phone\"" +
                                 "    href=\"javascript:if(confirm('O registro será removido!')) __doPostBack('lnkDeleteCantor','" +
                                                dr[caCantores.cdCantor].ToString() + ";" + strIDCategoria + ";" + strIdGV + "');\"> " +
                                 "    <i class=\"glyphicon glyphicon-trash\"></i>" +
                                 "  </a>" +
                                 "</td>";
                }

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