using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Reflection;
using System.Web.UI.WebControls;

namespace wappKaraoke.Classes.Paginas_Default
{
    public class csMontaDDL
    {
        private DataTable _dtDadosExternos;
        public DataTable dtDadosExternos
        {
            get { return _dtDadosExternos; }
            set { _dtDadosExternos = value; }
        }

        private bool _bUtilizaDadosExternos;
        public bool bUtilizaDadosExternos
        {
            get { return _bUtilizaDadosExternos; }
            set { _bUtilizaDadosExternos = value; }
        }

        private string _strTextoCombo;
        public string strTextoCombo
        {
            get { return _strTextoCombo; }
            set { _strTextoCombo = value; }
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

        private Type _tobjCon;
        public Type tobjCon
        {
            get { return _tobjCon; }
            set { _tobjCon = value; }
        }

        private object _objCo;
        public object objCo
        {
            get { return _objCo; }
            set { _objCo = value; }
        }

        private Type _tobjCo;
        public Type tobjCo
        {
            get { return _tobjCo; }
            set { _tobjCo = value; }
        }

        private DataTable _dtDados;
        public DataTable dtDados
        {
            get { return getDtDados(); }
            set { _dtDados = value; }
        }

        public virtual DataTable getDtDados()
        {
            DataTable dt;

            PreparaObjetos();

            MethodInfo LimparAtributos = tobjCo.GetMethod("LimparAtributos");
            object obj = LimparAtributos.Invoke(objCo, new object[] { });

            MethodInfo Select = objCon.GetType().GetMethod("Select");
            object bSelect = Select.Invoke(Select, new object[] { });

            if ((bool)bSelect)
            {
                PropertyInfo pdtDados = objCon.GetType().GetProperty("dtDados");
                dt = (DataTable)pdtDados.GetValue(objCon, null);

                MontarEstrutura();

                AdicionaPrimeiraLinha(dt);
            }
            else
                return null;

            return _dtDados;
        }

        public virtual DropDownList CarregaDDL(DropDownList pDDL)
        {
            object nmCampoChave = tobjCa.GetProperty("nmCampoChave").GetValue(tobjCa, null);
            object dePrincipal = tobjCa.GetProperty("dePrincipal").GetValue(tobjCa, null);

            pDDL.DataSource = getDtDados();
            pDDL.DataValueField = nmCampoChave.ToString();
            pDDL.DataTextField = dePrincipal.ToString();
            pDDL.DataBind();
            pDDL.SelectedIndex = 0;

            return pDDL;
        }

        public virtual string MontaSelect(string psId)
        {
            object nmCampoChave = tobjCa.GetProperty("nmCampoChave").GetValue(tobjCa, null);

            string strSelect = "<select name=\"" + nmCampoChave.ToString() + "_" +
                                psId + "\" id=\"" + nmCampoChave.ToString() + "_" + psId + "\" class=\"form-control selectpicker\" style=\"width:100%;text-align:left\">";

            strSelect += MontaOptions();

            strSelect += "</select>";

            return strSelect;
        }

        protected virtual string MontaOptions()
        {
            getDtDados();
            string strOprions = "";

            object nmCampoChave = tobjCa.GetProperty("nmCampoChave").GetValue(tobjCa, null);
            object dePrincipal = tobjCa.GetProperty("dePrincipal").GetValue(tobjCa, null);

            if (_dtDados.Rows.Count > 0)
            {
                strOprions += "<option value=\"" + _dtDados.Rows[0][nmCampoChave.ToString()].ToString() +
                                "\">" + _dtDados.Rows[0][dePrincipal.ToString()].ToString() + "</option>";

                for (int i = 1; i < _dtDados.Rows.Count; i++)
                {
                    strOprions += "<option value=\"" + _dtDados.Rows[i][nmCampoChave.ToString()].ToString() +
                                    "\">" + _dtDados.Rows[i][dePrincipal.ToString()].ToString() + "</option>";
                }
            }
            return strOprions;
        }

        public virtual void AdicionaPrimeiraLinha(DataTable pdtDadosACopiar)
        {
            object nmCampoChave = tobjCa.GetProperty("nmCampoChave").GetValue(tobjCa, null);
            object dePrincipal = tobjCa.GetProperty("dePrincipal").GetValue(tobjCa, null);

            DataRow dr = _dtDados.NewRow();
            dr[nmCampoChave.ToString()] = 0;
            dr[dePrincipal.ToString()] = "--Selecione " + _strTextoCombo + "--";

            _dtDados.Rows.Add(dr);

            foreach (DataRow drCopy in pdtDadosACopiar.Rows)
            {
                _dtDados.ImportRow(drCopy);
            }
        }

        public virtual void MontarEstrutura()
        {
            MethodInfo RetornaEstruturaDT = tobjCo.GetMethod("RetornaEstruturaDT");
            object objEstruturaDT = RetornaEstruturaDT.Invoke(objCo, new object[] { });

            _dtDados = (DataTable)objEstruturaDT;
        }

        public virtual void PreparaObjetos()
        {
            tobjCon = objCon.GetType();
            objCo = tobjCon.GetProperty("objCo").GetValue(objCon, null);
            tobjCo = objCo.GetType();
        }
    }
}