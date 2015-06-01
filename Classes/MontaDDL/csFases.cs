using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using wappKaraoke.Classes.Paginas_Default;
using wappKaraoke.Classes.Model.Fases;
using wappKaraoke.Classes.Controller;

namespace wappKaraoke.Classes
{
    public class csFases: Paginas_Default.csMontaDDL
    {
        public csFases()
        {
            strTextoCombo = "a Fase";
            tobjCa = typeof(caFases);
            objCon = new conFases();
        }
    }
}