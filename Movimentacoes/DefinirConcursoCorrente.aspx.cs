﻿using System;
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
                csFases vcsFases = new csFases();
                cdFase = vcsFases.CarregaDDL(cdFase);

                csConcursos vcsConcursos = new csConcursos();
                cdConcurso = vcsConcursos.CarregaDDL(cdConcurso);

                conConcursos objConConcursos = new conConcursos();
                objConConcursos.objCoConcursos.strFiltro = " WHERE flConcursoCorrente = 'S'";

                Session["cdConcursoCorrenteDefCorrente"] = null;
                Session["cdFaseCorrenteDefCorrente"] = null;

                if (conConcursos.Select())
                {
                    if (objConConcursos.dtDados != null && objConConcursos.dtDados.Rows.Count > 0)
                    {
                        cdConcurso.SelectedValue = objConConcursos.dtDados.Rows[0][caConcursos.cdConcurso].ToString();
                        Session["cdConcursoCorrenteDefCorrente"] = cdConcurso.SelectedValue;

                        cdFase.SelectedValue = objConConcursos.dtDados.Rows[0][caConcursos.cdFaseCorrente].ToString();
                        Session["cdFaseCorrenteDefCorrente"] = cdFase.SelectedValue;
                    }
                }
            }
        }

        protected void btnDefinirConcursoCorrente_Click(object sender, EventArgs e)
        {
            if (cdConcurso.SelectedIndex == 0 || cdFase.SelectedIndex == 0)
            {
                ltMensagem.Text = MostraMensagem("Validação!", "Selecione o concurso e a fase corrente.", csMensagem.msgWarning);
                return;
            }

            conConcursos objConConcursos = new conConcursos();

            if (Session["cdConcursoCorrenteDefCorrente"] != null)
            {
                if (cdConcurso.SelectedValue != Session["cdConcursoCorrenteDefCorrente"].ToString())
                {
                    objConConcursos.objCoConcursos.LimparAtributos();
                    objConConcursos.objCoConcursos.cdConcurso = Convert.ToInt32(Session["cdConcursoCorrenteDefCorrente"].ToString());
                    objConConcursos.objCoConcursos.flConcursoCorrente = "N";
                    objConConcursos.objCoConcursos.cdFaseCorrente = Convert.ToInt32(cdFase.SelectedValue);

                    if (!conConcursos.AlterarConcursoCorrente())
                    {
                        ltMensagem.Text = MostraMensagem("Falha!", "Não foi possível alterar o concurso corrente.", csMensagem.msgDanger);
                        return;
                    }

                    objConConcursos.objCoConcursos.LimparAtributos();
                    objConConcursos.objCoConcursos.cdConcurso = Convert.ToInt32(cdConcurso.SelectedValue.ToString());
                    objConConcursos.objCoConcursos.flConcursoCorrente = "S";
                    objConConcursos.objCoConcursos.cdFaseCorrente = Convert.ToInt32(cdFase.SelectedValue);

                    if (!conConcursos.AlterarConcursoCorrente())
                    {
                        ltMensagem.Text = MostraMensagem("Falha!", "Não foi possível alterar o concurso corrente.", csMensagem.msgDanger);
                        return;
                    }
                }
                else
                {
                    objConConcursos.objCoConcursos.LimparAtributos();
                    objConConcursos.objCoConcursos.cdConcurso = Convert.ToInt32(cdConcurso.SelectedValue.ToString());
                    objConConcursos.objCoConcursos.flConcursoCorrente = "S";
                    objConConcursos.objCoConcursos.cdFaseCorrente = Convert.ToInt32(cdFase.SelectedValue);

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
                objConConcursos.objCoConcursos.cdFaseCorrente = Convert.ToInt32(cdFase.SelectedValue);

                if (!conConcursos.AlterarConcursoCorrente())
                {
                    ltMensagem.Text = MostraMensagem("Falha!", "Não foi possível alterar o concurso corrente.", csMensagem.msgDanger);
                    return;
                }
            }

            Session["cdConcursoCorrenteDefCorrente"] = cdConcurso.SelectedValue.ToString();
            Session["cdFaseCorrenteDefCorrente"] = cdFase.SelectedValue.ToString();
            ltMensagem.Text = MostraMensagem("Sucesso!", "Concurso corrente definido com sucesso.", csMensagem.msgSucess);
        }
    }
}