using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Data;

namespace wappKaraoke.Classes
{
    public class csPageCadastro : csPage
    {
        protected int IndexRowDados;
        protected DataTable dtDados;
        protected ControlCollection ccControles;

        private string _strPaginaConsulta;
        protected string strPaginaConsulta
        {
            get { return _strPaginaConsulta; }
            set { _strPaginaConsulta = value; }
        }

        public override void Page_Load(object sender, EventArgs e)
        {
            CarregarDados(((Page)sender).Controls);
            _strPaginaConsulta = Request.Path.Substring(Request.Path.LastIndexOf("/") + 1);
            base.Page_Load(sender, e);
        }

        protected virtual void btnSalvar_Click(object sender, EventArgs e)
        {
            object obj = new int();
            PreencheObjeto(((LinkButton)sender).Parent.Controls, obj, "deTpStatus");
        }

        protected virtual void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect(_strPaginaConsulta.Replace("Cadastro", "Consulta"));
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
                                ((TextBox)c).Text = dtDados.Rows[IndexRowDados][i].ToString();
                            }
                            else
                                if (c is DropDownList)
                                {
                                    ((DropDownList)c).SelectedValue = dtDados.Rows[IndexRowDados][i].ToString();
                                }
                        }
                    }
                }
            }
        }

        protected virtual void PreencheObjeto(ControlCollection pControles, object pObjeto, string psNome)
        {
            foreach (Control c in pControles)
            {
                if ((c is TextBox) && (((TextBox)c).ID == psNome))
                {
                    if (pObjeto is string)
                    {
                        pObjeto = ((TextBox)c).Text;
                    }
                    else
                        if (pObjeto is int)
                        {
                            int iNumero;
                            Int32.TryParse(((TextBox)c).Text, out iNumero);
                            pObjeto = iNumero;
                        }

                }
                else
                    if ((c is DropDownList) && (((DropDownList)c).ID == psNome))
                    {
                    }
            }
        }
    }
}