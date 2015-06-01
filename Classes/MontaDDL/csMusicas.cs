using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using wappKaraoke.Classes.Paginas_Default;
using wappKaraoke.Classes.Model.Musicas;
using wappKaraoke.Classes.Controller;

namespace wappKaraoke.Classes
{
    public class csMusicas : Paginas_Default.csMontaDDL
    {
        public csMusicas()
        {
            strTextoCombo = "a Música";
            tobjCa = typeof(caMusicas);
            objCon = new conMusicas();
        }
    }
}