using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using wappKaraoke.Classes.Paginas_Default;
using wappKaraoke.Classes.Model.Cantores;
using wappKaraoke.Classes.Controller;

namespace wappKaraoke.Classes
{
    public class csCantores: Paginas_Default.csMontaDDL
    {
        public csCantores()
        {
            strTextoCombo = "o Cantor";
            tobjCa = typeof(caCantores);
            objCon = new conCantores();
        }
    }
}