﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Data;
using System.Reflection;
using wappKaraoke.Classes.Mensagem;

namespace wappKaraoke.Classes
{
    public class csPageCadastro : csPage
    {
        private bool _bErro = false;
        public bool bErro
        {
            get { return _bErro; }
            set { _bErro = value; }
        }

        protected int IndexRowDados;
        protected DataTable dtDados;
        protected ControlCollection ccControles;

        public override void Page_Load(object sender, EventArgs e)
        {
            if (ltMensagemDefault != null)
                ltMensagemDefault.Text = "";

            if (!IsPostBack)
            {
                CarregarDados(((Page)sender).Controls);
            }

            if (Session["ltMensagemDefault"] != null)
            {
                ltMensagemDefault.Text = ((Literal)Session["ltMensagemDefault"]).Text;
                Session["ltMensagemDefault"] = null;
            }

            if (Session["_strPaginaConsulta"] == null)
                Session["_strPaginaConsulta"] = Request.Path.Substring(Request.Path.LastIndexOf("/") + 1);

            base.Page_Load(sender, e);
        }

        protected virtual void btnSalvar_Click(object sender, EventArgs e)
        {
            object vobjCon;
            bool bInserindo = Session["IndexRowDados"] == null;

            if (!_bErro)
            {
                PreencheObjeto(((LinkButton)sender).Parent.Controls, out vobjCon);

                tobjCon = vobjCon.GetType();

                if (bInserindo)
                {
                    MethodInfo Inserir = tobjCon.GetMethod("Inserir");
                    object bInserir = Inserir.Invoke(Inserir, new object[] { });

                    if ((bool)bInserir)
                    {
                        ltMensagemDefault.Text = base.MostraMensagem(csMensagem.msgOperacaoComSucesso, csMensagem.msgRegistroInserido, csMensagem.msgSucess);
                    }
                    else
                    {
                        _bErro = true;
                        string strMensagemErro = tobjCon.GetProperty("strMensagemErro").GetValue(objCon, null).ToString();
                        ltMensagemDefault.Text = base.MostraMensagem(csMensagem.msgTitFalhaGenerica, strMensagemErro, csMensagem.msgWarning);
                    }
                }
                else
                {
                    CarregaChave(ref vobjCon);

                    MethodInfo Alterar = tobjCon.GetMethod("Alterar");
                    object bAlterar = Alterar.Invoke(Alterar, new object[] { });

                    if ((bool)bAlterar)
                    {
                        ltMensagemDefault.Text = base.MostraMensagem(csMensagem.msgOperacaoComSucesso, csMensagem.msgRegistroAlterado, csMensagem.msgSucess);
                    }
                    else
                    {
                        _bErro = true;
                        string strMensagemErro = tobjCon.GetProperty("strMensagemErro").GetValue(objCon, null).ToString();
                        ltMensagemDefault.Text = base.MostraMensagem(csMensagem.msgTitFalhaGenerica, strMensagemErro, csMensagem.msgWarning);
                    }
                }

                Session["ltMensagemDefault"] = ltMensagemDefault;
            }

            if (!_bErro)
            {
                string strPagina = Session["_strPaginaConsulta"].ToString();
                Session["_strPaginaConsulta"] = null;
                Response.Redirect(strPagina.Replace("Cadastro", "Consulta"));
            }
        }

        protected virtual void btnCancelar_Click(object sender, EventArgs e)
        {
            string strPagina = Session["_strPaginaConsulta"].ToString();
            Session["_strPaginaConsulta"] = null;
            Response.Redirect(strPagina.Replace("Cadastro", "Consulta"));
        }

        protected virtual void CarregarDados(ControlCollection pControles)
        {
            if (Session["IndexRowDados"] != null)
            {
                IndexRowDados = (int)Session["IndexRowDados"];
                dtDados = (DataTable)Session["dtDados"];

                foreach (Control c in pControles[0].Controls[3].Controls[1].Controls)
                {
                    for (int i = 0; i < dtDados.Columns.Count; i++)
                    {
                        if ((c.ID != null) && (c.ID.ToUpper() == dtDados.Columns[i].ColumnName.ToUpper()))
                        {
                            if (c is TextBox)
                            {
                                if (c.ID.ToUpper().Substring(0, 2).Equals("DT"))
                                {
                                    if (!dtDados.Rows[IndexRowDados][i].ToString().Substring(0, 10).Equals("01/01/0001"))
                                        ((TextBox)c).Text = dtDados.Rows[IndexRowDados][i].ToString().Substring(0, 10);
                                }
                                else
                                    ((TextBox)c).Text = dtDados.Rows[IndexRowDados][i].ToString();
                            }
                            else
                                if (c is DropDownList)
                                {
                                    ((DropDownList)c).SelectedValue = dtDados.Rows[IndexRowDados][i].ToString();
                                }
                                else
                                    if (c is CheckBox)
                                    {
                                        ((CheckBox)c).Checked = dtDados.Rows[IndexRowDados][i].ToString() == "S" ? true : false;
                                    }
                        }
                    }
                }
            }
        }

        protected virtual void PreencheObjeto(ControlCollection pControles, out object pObjCon)
        {
            object nmCampoChave = tobjCa.GetProperty("nmCampoChave").GetValue(tobjCa, null);

            tobjCon = objCon.GetType();
            object vobjCon = Activator.CreateInstance(tobjCon);
            objCo = vobjCon.GetType().GetProperty("objCo").GetValue(objCon, null);
            tobjCo = objCo.GetType();

            PropertyInfo pCampo;
            object temp;

            foreach (Control c in pControles)
            {
                if (c is TextBox)
                {
                    pCampo = tobjCo.GetProperty(((TextBox)c).ID);
                    temp = tobjCo.GetProperty(((TextBox)c).ID).GetValue(objCo, null);

                    if (temp is int && ((TextBox)c).Text.Trim() != "")
                        pCampo.SetValue(objCo, Convert.ToInt32(((TextBox)c).Text), null);
                    else
                        if (temp is string)
                            pCampo.SetValue(objCo, ((TextBox)c).Text, null);
                        else
                            if (temp is DateTime && ((TextBox)c).Text.Trim() != "")
                                pCampo.SetValue(objCo, Convert.ToDateTime(((TextBox)c).Text), null);
                }
                else
                    if (c is DropDownList)
                    {
                        pCampo = tobjCo.GetProperty(((DropDownList)c).ID);
                        temp = tobjCo.GetProperty(((DropDownList)c).ID).GetValue(objCo, null);

                        if (temp is int)
                            pCampo.SetValue(objCo, Convert.ToInt32(((DropDownList)c).SelectedValue), null);
                        else
                            if (temp is string)
                                pCampo.SetValue(objCo, ((DropDownList)c).SelectedValue, null);
                    }
                    else
                        if (c is CheckBox)
                        {
                            pCampo = tobjCo.GetProperty(((CheckBox)c).ID);
                            temp = tobjCo.GetProperty(((CheckBox)c).ID).GetValue(objCo, null);

                            pCampo.SetValue(objCo, ((CheckBox)c).Checked ? "S" : "N", null);
                        }
            }

            pObjCon = vobjCon;
        }

        protected virtual void CarregaChave(ref object vObjcon)
        {
            IndexRowDados = (int)Session["IndexRowDados"];
            dtDados = (DataTable)Session["dtDados"];

            object nmCampoChave = tobjCa.GetProperty("nmCampoChave").GetValue(tobjCa, null);

            objCo = vObjcon.GetType().GetProperty("objCo").GetValue(objCon, null);
            tobjCo = objCo.GetType();
            PropertyInfo pCampo = tobjCo.GetProperty(nmCampoChave.ToString());
            object temp = tobjCo.GetProperty(nmCampoChave.ToString()).GetValue(objCo, null);

            if (temp is string)
            {
                pCampo.SetValue(objCo, dtDados.Rows[IndexRowDados][nmCampoChave.ToString()].ToString(), null);
            }
            else
                if (temp is int)
                {
                    pCampo.SetValue(objCo, Convert.ToInt32(dtDados.Rows[IndexRowDados][nmCampoChave.ToString()].ToString()), null);
                }
        }
    }
}