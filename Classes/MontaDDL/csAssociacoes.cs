using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using wappKaraoke.Classes.Paginas_Default;
using wappKaraoke.Classes.Model.Associacoes;
using wappKaraoke.Classes.Controller;

namespace wappKaraoke.Classes
{
    public class csAssociacoes : Paginas_Default.csMontaDDL
    {
        public csAssociacoes()
        {
            strTextoCombo = "a Associação";
            tobjCa = typeof(caAssociacoes);
            objCon = new conAssociacoes();
        }
    }
}