using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using wappKaraoke.Classes.Paginas_Default;
using wappKaraoke.Classes.Model.Jurados;
using wappKaraoke.Classes.Controller;

namespace wappKaraoke.Classes
{
    public class csJurados : Paginas_Default.csMontaDDL
    {
        public csJurados()
        {
            strTextoCombo = "o Jurado";
            tobjCa = typeof(caJurados);
            objCon = new conJurados();
        }
    }
}