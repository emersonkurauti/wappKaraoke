using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using wappKaraoke.Classes.Paginas_Default;
using wappKaraoke.Classes.Model.TipoStatus;
using wappKaraoke.Classes.Controller;


namespace wappKaraoke.Classes
{
    public class csStatus : Paginas_Default.csMontaDDL
    {
        public csStatus()
        {
            strTextoCombo = "o Status";
            tobjCa = typeof(caTipoStatus);
            objCon = new conTipoStatus();
        }
    }
}