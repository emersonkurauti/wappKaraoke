using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wappKaraoke.Classes;

namespace wappKaraoke.Movimentacoes
{
    public partial class AlterarStatusCantor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                csFases vcsFases = new csFases();
                cdFase = vcsFases.CarregaDDL(cdFase);

                csStatus vcsStatus = new csStatus();
                cdStatus = vcsStatus.CarregaDDL(cdStatus);
            }
        }
    }
}