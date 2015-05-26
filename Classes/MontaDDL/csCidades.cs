using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using wappKaraoke.Classes.Controller;
using wappKaraoke.Classes.Model.Cidades;
using wappKaraoke.Classes.Paginas_Default;

namespace wappKaraoke.Classes
{
    public class csCidades : Paginas_Default.csMontaDDL
    {
        public csCidades()
        {
            strTextoCombo = "a Cidade";
            tobjCa = typeof(caCidades);
            objCon = new conCidades();
        }
    }
}