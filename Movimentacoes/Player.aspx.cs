using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wappKaraoke.Display;

namespace wappKaraoke.Movimentacoes
{
    public partial class Player : System.Web.UI.Page
    {
        private csDisplay ocsDisplay;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ocsDisplay = new csDisplay("COM5");
                Session["ocsDisplay"] = ocsDisplay;
                Session["NumeroAtual"] = 49;
            }
        }

        protected void btnFinalizado_Click(object sender, EventArgs e)
        {
            ocsDisplay = (csDisplay)Session["ocsDisplay"];
            int iNumero = Convert.ToInt32(Session["NumeroAtual"].ToString());
            iNumero++;
            Session["NumeroAtual"] = iNumero;
            ocsDisplay.MudarNumero(iNumero.ToString());
        }
    }
}