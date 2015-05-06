using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wappKaraoke.Classes;

namespace wappKaraoke.Movimentacoes
{
    public partial class NotasCantores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                csCategorias vcsCategorias = new csCategorias();
                cdCategoria = vcsCategorias.CarregaDDL(cdCategoria);

                csCantores vcsCantores = new csCantores();
                cdCantor = vcsCantores.CarregaDDL(cdCantor);
            }
        }

        public void CarregaNotasJurados()
        {
 
        }
    }
}