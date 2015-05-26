using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using wappKaraoke.Classes.Controller;
using wappKaraoke.Classes.Model.Cidades;

namespace wappKaraoke.Classes
{
    public class csCidades
    {
        private DataTable _dtDados;
        public DataTable dtDados
        {
            get { return getDtDados(); }
            set { _dtDados = value; }
        }

        private DataTable getDtDados()
        {
            conCidades objCon = new conCidades();

            objCon.objCoCidades.LimparAtributos();
            objCon.objCoCidades.Select(out _dtDados);

            return _dtDados;
        }

        public DropDownList CarregaDDL(DropDownList pDDL)
        {
            pDDL.DataSource = getDtDados();
            pDDL.DataValueField = caCidades.nmCampoChave;
            pDDL.DataTextField = caCidades.dePrincipal;
            pDDL.DataBind();
            pDDL.SelectedIndex = 0;

            return pDDL;
        }
    }
}