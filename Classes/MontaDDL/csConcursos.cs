using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using wappKaraoke.Classes.Controller;
using wappKaraoke.Classes.Model.Concursos;
using wappKaraoke.Classes.Paginas_Default;

namespace wappKaraoke.Classes
{
    public class csConcursos : Paginas_Default.csMontaDDL
    {
        public csConcursos()
        {
            strTextoCombo = "o Concurso";
            tobjCa = typeof(caConcursos);
            objCon = new conConcursos();
        }
    }
}