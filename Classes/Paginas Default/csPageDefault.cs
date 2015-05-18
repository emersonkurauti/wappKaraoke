using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Reflection;
using System.Web.UI.WebControls;
using wappKaraoke.Classes.Mensagem;
using System.Web.UI;

namespace wappKaraoke.Classes
{
    public class csPageDefault : csPage
    {
        private string _strPaginaCadastro;
        protected string strPaginaCadastro
        {
            get { return _strPaginaCadastro; }
            set { _strPaginaCadastro = value; }
        }

        private DataTable _dtDados;
        public DataTable dtDados
        {
            get { return _dtDados; }
            set { _dtDados = value; }
        }

        private GridView _gvDadosDefault;
        public GridView gvDadosDefault
        {
            get { return _gvDadosDefault; }
            set { _gvDadosDefault = value; }
        }

        private HiddenField _hdConfirmacao;
        public HiddenField hdConfirmacao
        {
            get { return _hdConfirmacao; }
            set { _hdConfirmacao = value; }
        }

        public override void Page_Load(object sender, EventArgs e)
        {
            if (ltMensagemDefault != null)
                ltMensagemDefault.Text = "";

            if (!IsPostBack)
            {
                if (objCon != null)
                {
                    AtualizaGridView();
                }
            }

            if (Session["ltMensagemDefault"] != null)
            {
                ltMensagemDefault.Text = ((Literal)Session["ltMensagemDefault"]).Text;
                Session["ltMensagemDefault"] = null;
            }

            _strPaginaCadastro = Request.Path.Substring(Request.Path.LastIndexOf("/") + 1);
            base.Page_Load(sender, e);
        }

        protected override void InicializaSessions()
        {
            base.InicializaSessions();
        }

        protected override bool ConfigurarGridView()
        {
            if (!base.ConfigurarGridView())
                return false;

            if (_dtDados == null)
                return false;

            if (_dtDados.Columns.Count == 0)
                return false;

            if (_dtDados.Rows.Count == 0)
                return false;

            return true;
        }

        protected virtual void AtualizaGridView()
        {
            tobjCon = objCon.GetType();
            objCo = tobjCon.GetProperty("objCo").GetValue(objCon, null);
            tobjCo = objCo.GetType();

            MethodInfo LimparAtributos = tobjCo.GetMethod("LimparAtributos");
            object obj = LimparAtributos.Invoke(objCo, new object[] { });

            MethodInfo Select = tobjCon.GetMethod("Select");
            object bSelect = Select.Invoke(tobjCon, new object[] { });

            if ((bool)bSelect)
            {
                PropertyInfo pdtDados = objCon.GetType().GetProperty("dtDados");
                dtDados = (DataTable)pdtDados.GetValue(objCon, null);

                if (dtDados.Rows.Count == 0)
                    ltMensagemDefault.Text = base.MostraMensagem("Nenhum registro encontrado", "Realize o cadastro!", csMensagem.msgInfo);
            }
            else
            {
                string strMensagemErro = tobjCon.GetProperty("strMensagemErro").GetValue(objCon, null).ToString();
                ltMensagemDefault.Text = base.MostraMensagem(csMensagem.msgTitFalhaGenerica, strMensagemErro, csMensagem.msgDanger);
            }

            _gvDadosDefault.DataSource = _dtDados;
            _gvDadosDefault.DataBind();

            Session["dtDados"] = dtDados;

            ConfigurarGridView();
        }

        protected virtual void btnNovo1_Click(object sender, EventArgs e)
        {
            Session["IndexRowDados"] = null;
            Response.Redirect(_strPaginaCadastro.Replace("Consulta", "Cadastro"));
        }

        protected virtual void btnBuscar_Click(object sender, EventArgs e)
        {
            string strFiltro = "";
            int intCodigo;

            _dtDados = (DataTable)Session["dtDados"];

            tobjCon = objCon.GetType();
            objCo = tobjCon.GetProperty("objCo").GetValue(objCon, null);
            tobjCo = objCo.GetType();

            MethodInfo LimparAtributos = tobjCo.GetMethod("LimparAtributos");
            object obj = LimparAtributos.Invoke(objCo, new object[] { });

            try
            {
                object nmCampoChave = tobjCa.GetProperty("nmCampoChave").GetValue(tobjCa, null);

                foreach (Control c in ((LinkButton)sender).Parent.Controls)
                {
                    foreach (DataColumn dc in _dtDados.Columns)
                    {
                        if ((c is TextBox) && (((TextBox)c).ID.ToUpper() == dc.ColumnName.ToUpper()) && (((TextBox)c).Text.Trim() != ""))
                        {
                            if (nmCampoChave.ToString().ToUpper() == dc.ColumnName.ToUpper())
                            {
                                Int32.TryParse(((TextBox)c).Text.Trim(), out intCodigo);
                                strFiltro += AndWhere(strFiltro) + " " + dc.ColumnName + " = " + intCodigo.ToString();
                            }
                            else
                            {
                                strFiltro += AndWhere(strFiltro) + " " + dc.ColumnName + " LIKE '%" + ((TextBox)c).Text.Trim() + "%'";
                            }
                        }
                        if ((c is DropDownList) && (((DropDownList)c).ID.ToUpper() == dc.ColumnName.ToUpper()) && (((DropDownList)c).SelectedIndex != 0))
                        {
                            Int32.TryParse(((DropDownList)c).SelectedValue, out intCodigo);

                            if (intCodigo == 0)
                                strFiltro += AndWhere(strFiltro) + " " + dc.ColumnName + " = '" + ((DropDownList)c).SelectedValue.ToString() + "'";
                            else
                                strFiltro += AndWhere(strFiltro) + " " + dc.ColumnName + " = " + ((DropDownList)c).SelectedValue;
                        }
                    }

                }

                //Seta o Filtro
                PropertyInfo pstrFiltro = tobjCo.GetProperty("strFiltro");
                pstrFiltro.SetValue(objCo, strFiltro, null);


                MethodInfo Select = objCon.GetType().GetMethod("Select");
                object bSelect = Select.Invoke(Select, new object[] { });

                if ((bool)bSelect)
                {
                    PropertyInfo pdtDados = objCon.GetType().GetProperty("dtDados");
                    dtDados = (DataTable)pdtDados.GetValue(objCon, null);

                    if (dtDados.Rows.Count == 0)
                        ltMensagemDefault.Text = base.MostraMensagem("Nenhum registro encontrado", "Realize o cadastro!", csMensagem.msgInfo);
                }
                else
                {
                    string strMensagemErro = tobjCon.GetProperty("strMensagemErro").GetValue(objCon, null).ToString();
                    ltMensagemDefault.Text = base.MostraMensagem(csMensagem.msgTitFalhaGenerica, strMensagemErro, csMensagem.msgDanger);
                }

                _gvDadosDefault.DataSource = _dtDados;
                _gvDadosDefault.DataBind();

                Session["dtDados"] = dtDados;

                ConfigurarGridView();
            }
            catch
            {
                ltMensagemDefault.Text = MostraMensagem(csMensagem.msgTitFalaAoConsultar, csMensagem.msgFalhaAoConsultarFiltro, csMensagem.msgDanger);
            }
        }

        protected virtual string AndWhere(string sComando)
        {
            if (sComando.Contains("WHERE"))
                    return " AND ";
            else
                return " WHERE ";
        }

        protected virtual void RemoveRegistro(int intCodigo)
        {
            _dtDados = (DataTable)Session["dtDados"];

            tobjCon = objCon.GetType();
            objCon = Activator.CreateInstance(tobjCon);
            objCo = tobjCon.GetProperty("objCo").GetValue(objCon, null);
            tobjCo = objCo.GetType();

            object nmCampoChave = tobjCa.GetProperty("nmCampoChave").GetValue(tobjCa, null);

            PropertyInfo pCampoChave = tobjCo.GetProperty(nmCampoChave.ToString());
            pCampoChave.SetValue(objCo, intCodigo, null);

            MethodInfo Excluir = tobjCon.GetMethod("Excluir");
            object bExcluir = Excluir.Invoke(tobjCon, new object[] { });

            if ((bool)bExcluir)
            {
                ltMensagemDefault.Text = base.MostraMensagem(csMensagem.msgOperacaoComSucesso, csMensagem.msgRegistroExcluido, csMensagem.msgSucess);
            }
            else
            {
                string strMensagemErro = tobjCon.GetProperty("strMensagemErro").GetValue(objCon, null).ToString();
                ltMensagemDefault.Text = base.MostraMensagem(csMensagem.msgTitFalhaGenerica, strMensagemErro, csMensagem.msgDanger);
            }

            AtualizaGridView();
        }

        protected virtual void gvDados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                string[] strValores = e.CommandArgument.ToString().Split('$');
                int intCodigo = Convert.ToInt32(strValores[0]);
                RemoveRegistro(intCodigo);
            }
            else
                if(e.CommandName == "Edit")
                {
                    Session["IndexRowDados"] = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect(_strPaginaCadastro.Replace("Consulta", "Cadastro"));
                }
        }

        protected virtual void gvDados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
                lnkDelete.Attributes.Add("OnClick", "javascript:return "+
                                 "confirm('O registro \"" + DataBinder.Eval(e.Row.DataItem, "deTpStatus") + "\" será removido!')");

                LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
                lnkEdit.CommandArgument = e.Row.RowIndex.ToString();
            }
        }

        protected virtual void gvDados_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
    }
}