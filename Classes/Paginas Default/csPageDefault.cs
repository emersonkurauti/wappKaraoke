using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Reflection;
using System.Web.UI.WebControls;
using wappKaraoke.Mensagem;

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

        protected virtual void btnNovo1_Click(object sender, EventArgs e)
        {
            Response.Redirect(_strPaginaCadastro.Replace("Consulta", "Cadastro"));
        }

        protected virtual void gvDados_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gvRow = ((GridView)sender).SelectedRow;
            Session["gvRow"] = gvRow;
            Response.Redirect(_strPaginaCadastro.Replace("Consulta", "Cadastro"));
        }

        protected void gvDados_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //Mensagem("Deseja realmente exclir?", Page);

            _dtDados = (DataTable)Session["dtDados"];

            _tobjCon = _objCon.GetType();
            _objCon = Activator.CreateInstance(_tobjCon);
            _objCo = _tobjCon.GetProperty("objCo").GetValue(_objCon, null);
            _tobjCo = _objCo.GetType();

            object nmCampoChave = _tobjCa.GetProperty("nmCampoChave").GetValue(_tobjCa, null);

            PropertyInfo pCampoChave = _tobjCo.GetProperty(nmCampoChave.ToString());
            pCampoChave.SetValue(_objCo, Convert.ToInt32(_dtDados.Rows[e.RowIndex][nmCampoChave.ToString()].ToString()), null);

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

        protected virtual string AndWhere(string sComando)
        {
            if (sComando.Contains("WHERE"))
                    return " AND ";
            else
                return " WHERE ";
        }
    }
}