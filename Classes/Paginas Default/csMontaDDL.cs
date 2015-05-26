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

        private DataTable getDtDados()
        {
            DataTable dt;

            tobjCon = objCon.GetType();
            objCo = tobjCon.GetProperty("objCo").GetValue(objCon, null);
            tobjCo = objCo.GetType();

            MethodInfo LimparAtributos = tobjCo.GetMethod("LimparAtributos");
            object obj = LimparAtributos.Invoke(objCo, new object[] { });

            MethodInfo Select = objCon.GetType().GetMethod("Select");
            object bSelect = Select.Invoke(Select, new object[] { });

            if ((bool)bSelect)
            {
                PropertyInfo pdtDados = objCon.GetType().GetProperty("dtDados");
                dt = (DataTable)pdtDados.GetValue(objCon, null);

                MethodInfo RetornaEstruturaDT = tobjCo.GetMethod("RetornaEstruturaDT");
                object objEstruturaDT = RetornaEstruturaDT.Invoke(objCo, new object[] { });

                _dtDados = (DataTable)objEstruturaDT;

                object nmCampoChave = tobjCa.GetProperty("nmCampoChave").GetValue(tobjCa, null);
                object dePrincipal = tobjCa.GetProperty("dePrincipal").GetValue(tobjCa, null);

                DataRow dr = _dtDados.NewRow();
                dr[nmCampoChave.ToString()] = 0;
                dr[dePrincipal.ToString()] = "--Selecione " + _strTextoCombo + "--";

                _dtDados.Rows.Add(dr);

                foreach (DataRow drCopy in dt.Rows)
                {
                    _dtDados.ImportRow(drCopy);
                }
            }
            else
                return null;

            return _dtDados;
        }

        public DropDownList CarregaDDL(DropDownList pDDL)
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
    }
}