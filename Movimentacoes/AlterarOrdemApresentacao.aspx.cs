﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wappKaraoke.Classes.Controller;
using wappKaraoke.Classes;
using wappKaraoke.Classes.Model.Concursos;
using wappKaraoke.Classes.Mensagem;
using wappKaraoke.Classes.Model.CantoresFases;

namespace wappKaraoke.Movimentacoes
{
    public partial class AlterarOrdemApresentacao : csPage
    {
        public override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ltMensagem.Text = "";

                conConcursos objConConcursos = new conConcursos();
                objConConcursos.objCoConcursos.LimparAtributos();
                objConConcursos.objCoConcursos.strFiltro = " WHERE flConcursoCorrente = 'S'";

                Session["cdConcursoCorrente"] = null;
                Session["cdFaseCorrente"] = null;

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
                    ltMensagem.Text = MostraMensagem("Falha!", "Atenção, não existe concurso corrende definido!", csMensagem.msgDanger);
                }
            }
        }

        private bool ConsultarCantor()
        {
            csStatus vcsStatus = new csStatus();
            cdStatus = vcsStatus.CarregaDDL(cdStatus);

            conCantoresFases objConCantoresFases = new conCantoresFases();
            objConCantoresFases.objCoCantoresFases.LimparAtributos();
            objConCantoresFases.objCoCantoresFases.cdConcurso = Convert.ToInt32(Session["cdConcursoCorrente"].ToString());
            objConCantoresFases.objCoCantoresFases.cdFase = Convert.ToInt32(Session["cdFaseCorrente"].ToString());
            objConCantoresFases.objCoCantoresFases.nuCantor = nuCantor.Text;

            if (!conCantoresFases.Select())
            {
                ltMensagem.Text = MostraMensagem("Falha!", "Não foi possível localizar o cantor.", csMensagem.msgDanger);
                nuCantor.Text = "";
                return false;
            }

            if (objConCantoresFases.dtDados.Rows.Count == 0)
            {
                ltMensagem.Text = MostraMensagem("Aviso!", "Não foi possível localizar o cantor pelo número informado.", csMensagem.msgWarning);
                nuCantor.Text = "";
                return false;
            }

            Session["cdCantor"] = objConCantoresFases.dtDados.Rows[0][caCantoresFases.cdCantor].ToString();
            Session["cdCategoria"] = objConCantoresFases.dtDados.Rows[0][caCantoresFases.cdCategoria].ToString();

            ltInfoCantor.Text = MostraMensagem("Detalhes:",
                "Nome: " + objConCantoresFases.dtDados.Rows[0][caCantoresFases.CC_nmCantor].ToString() + "<br/>" +
                "Fase: " + objConCantoresFases.dtDados.Rows[0][caCantoresFases.CC_deFase].ToString() + "<br/>" +
                "Categoria: " + objConCantoresFases.dtDados.Rows[0][caCantoresFases.CC_deCategoria].ToString(),
                csMensagem.msgInfo);

            return true;
        }

        protected void nuCantor_TextChanged(object sender, EventArgs e)
        {
            ltMensagem.Text = "";
            ConsultarCantor();

            cdStatus.SelectedValue = wappKaraoke.Properties.Settings.Default.sCodStatusInicial;
        }

        protected void btnUltimoDaCategoria_Click(object sender, EventArgs e)
        {
            ltMensagem.Text = "";

            if (nuCantor.Text.Trim() == "")
            {
                ltMensagem.Text = MostraMensagem("Validação", "Informe o número do cantor.", csMensagem.msgWarning);
                return;
            }

            if (cdStatus.SelectedIndex <= 0)
            {
                ltMensagem.Text = MostraMensagem("Validação", "Selecione o status.", csMensagem.msgWarning);
                return;
            }

            conCantoresFases objConCantoresFases = new conCantoresFases();
            objConCantoresFases.objCoCantoresFases.cdConcurso = Convert.ToInt32(Session["cdConcursoCorrente"].ToString());
            objConCantoresFases.objCoCantoresFases.cdCantor = Convert.ToInt32(Session["cdCantor"].ToString());
            objConCantoresFases.objCoCantoresFases.cdFase = Convert.ToInt32(Session["cdFaseCorrente"].ToString());
            objConCantoresFases.objCoCantoresFases.cdCategoria = Convert.ToInt32(Session["cdCategoria"].ToString());
            objConCantoresFases.objCoCantoresFases.cdTpStatus = Convert.ToInt32(cdStatus.SelectedValue.ToString());

            if (!conCantoresFases.AlterarOrdemApresentacao())
            {
                ltMensagem.Text = MostraMensagem("Falha!", "Não foi possível alterar a ordem de apresentação do cantor.", csMensagem.msgDanger);
                return;
            }

            ltMensagem.Text = MostraMensagem("Sucesso!", "Ordem de apresentação alterada com sucesso.", csMensagem.msgSucess);
        }
    }
}