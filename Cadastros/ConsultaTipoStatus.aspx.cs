using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using wappKaraoke.Classes;

namespace wappKaraoke.Cadastros
{
    public partial class ConsultaTipoStatus : csPageDefault
    {
        public override void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                csCorStatus vcsCorStatus = new csCorStatus();
                deCor = vcsCorStatus.CarregaDDL(deCor);
            }

            base.Page_Load(sender, e);
        }

        public override void ConfirarGridView()
        {
            base.ConfirarGridView();
            //Attribute to show the Plus Minus Button.
            gvDados.HeaderRow.Cells[1].Attributes["data-class"] = "expand";

            //Attribute to hide column in Phone.
            gvDados.HeaderRow.Cells[0].Attributes["data-hide"] = "phone";
            gvDados.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";

            //Adds THEAD and TBODY to GridView.
            gvDados.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {

        }
    }
}