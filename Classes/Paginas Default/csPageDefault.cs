using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Reflection;
using System.Web.UI.WebControls;
using wappKaraoke.Mensagem;
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

        private Type _tobjCa;
        public Type tobjCa
        {
            get { return _tobjCa; }
            set { _tobjCa = value; }
        }

        private object _objCon;
        public object objCon
        {
            get { return _objCon; }
            set { _objCon = value; }
        }

        private GridView _gvDadosDefault;
        public GridView gvDadosDefault
        {
            get { return _gvDadosDefault; }
            set { _gvDadosDefault = value; }
        }

        private Type _tobjCon;
        private object _objCo;
        private Type _tobjCo;

        private Literal _ltMensagemDefault;
        public Literal ltMensagemDefault
        {
            get { return _ltMensagemDefault; }
            set { _ltMensagemDefault = value; }
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
                if (_objCon != null)
                {
                    AtualizaGridView();
                }
            }

            _strPaginaCadastro = Request.Path.Substring(Request.Path.LastIndexOf("/") + 1);
            base.Page_Load(sender, e);
        }

        protected override void InicializaSessions()
        {
            base.InicializaSessions();

            Session["cdRegistro"] = null;
        }

        protected override bool ConfirarGridView()
        {
            if (!base.ConfirarGridView())
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
            _tobjCon = _objCon.GetType();
            _objCo = _tobjCon.GetProperty("objCo").GetValue(_objCon, null);
            _tobjCo = _objCo.GetType();

            MethodInfo LimparAtributos = _tobjCo.GetMethod("LimparAtributos");
            object obj = LimparAtributos.Invoke(_objCo, new object[] { });

            MethodInfo Select = _tobjCon.GetMethod("Select");
            object bSelect = Select.Invoke(_tobjCon, new object[] { });

            if ((bool)bSelect)
            {
                PropertyInfo pdtDados = _objCon.GetType().GetProperty("dtDados");
                dtDados = (DataTable)pdtDados.GetValue(_objCon, null);

                if (dtDados.Rows.Count == 0)
                    _ltMensagemDefault.Text = base.MostraMensagem("Nenhum registro encontrado", "Realize o cadastro!", csMensagem.msgInfo);
            }
            else
            {
                string strMensagemErro = _tobjCon.GetProperty("strMensagemErro").GetValue(_objCon, null).ToString();
                _ltMensagemDefault.Text = base.MostraMensagem(csMensagem.msgTitFalhaGenerica, strMensagemErro, csMensagem.msgDanger);
            }

            _gvDadosDefault.DataSource = _dtDados;
            _gvDadosDefault.DataBind();

            Session["dtDados"] = dtDados;

            ConfirarGridView();
        }

        protected virtual void btnNovo1_Click(object sender, EventArgs e)
        {           
            Response.Redirect(_strPaginaCadastro.Replace("Consulta", "Cadastro"));
        }

        protected virtual void btnBuscar_Click(object sender, EventArgs e)
        {
            string strFiltro = "";
            int intCodigo;

            _dtDados = (DataTable)Session["dtDados"];

            _tobjCon = _objCon.GetType();
            _objCo = _tobjCon.GetProperty("objCo").GetValue(_objCon, null);
            _tobjCo = _objCo.GetType();

            MethodInfo LimparAtributos = _tobjCo.GetMethod("LimparAtributos");
            object obj = LimparAtributos.Invoke(_objCo, new object[] { });

            try
            {
                object nmCampoChave = _tobjCa.GetProperty("nmCampoChave").GetValue(_tobjCa, null);

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
                PropertyInfo pstrFiltro = _tobjCo.GetProperty("strFiltro");
                pstrFiltro.SetValue(_objCo, strFiltro, null);


                MethodInfo Select = _objCon.GetType().GetMethod("Select");
                object bSelect = Select.Invoke(Select, new object[] { });

                if ((bool)bSelect)
                {
                    PropertyInfo pdtDados = _objCon.GetType().GetProperty("dtDados");
                    dtDados = (DataTable)pdtDados.GetValue(_objCon, null);

                    if (dtDados.Rows.Count == 0)
                        _ltMensagemDefault.Text = base.MostraMensagem("Nenhum registro encontrado", "Realize o cadastro!", csMensagem.msgInfo);
                }
                else
                {
                    string strMensagemErro = _tobjCon.GetProperty("strMensagemErro").GetValue(_objCon, null).ToString();
                    _ltMensagemDefault.Text = base.MostraMensagem(csMensagem.msgTitFalhaGenerica, strMensagemErro, csMensagem.msgDanger);
                }

                _gvDadosDefault.DataSource = _dtDados;
                _gvDadosDefault.DataBind();

                Session["dtDados"] = dtDados;

                ConfirarGridView();
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

            _tobjCon = _objCon.GetType();
            _objCon = Activator.CreateInstance(_tobjCon);
            _objCo = _tobjCon.GetProperty("objCo").GetValue(_objCon, null);
            _tobjCo = _objCo.GetType();

            object nmCampoChave = _tobjCa.GetProperty("nmCampoChave").GetValue(_tobjCa, null);

            PropertyInfo pCampoChave = _tobjCo.GetProperty(nmCampoChave.ToString());
            pCampoChave.SetValue(_objCo, intCodigo, null);

            MethodInfo Excluir = _tobjCon.GetMethod("Excluir");
            object bExcluir = Excluir.Invoke(_tobjCon, new object[] { });

            if ((bool)bExcluir)
            {
                _ltMensagemDefault.Text = base.MostraMensagem(csMensagem.msgOperacaoComSucesso, csMensagem.msgRegistroExcluido, csMensagem.msgSucess);
            }
            else
            {
                string strMensagemErro = _tobjCon.GetProperty("strMensagemErro").GetValue(_objCon, null).ToString();
                _ltMensagemDefault.Text = base.MostraMensagem(csMensagem.msgTitFalhaGenerica, strMensagemErro, csMensagem.msgDanger);
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
                    Session["cdRegistro"] = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect(_strPaginaCadastro.Replace("Consulta", "Cadastro"));
                }
        }

        protected virtual void gvDados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton l = (LinkButton)e.Row.FindControl("lnkDelete");
                l.Attributes.Add("OnClick", "javascript:return "+
                                 "confirm('O registro \"" + DataBinder.Eval(e.Row.DataItem, "deTpStatus") + "\" será removido!')"); 
            }
        }

        protected virtual void gvDados_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
    }
}