using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using wappKaraoke.Classes.Paginas_Default;
using wappKaraoke.Classes.Model.Categorias;
using wappKaraoke.Classes.Controller;

namespace wappKaraoke.Classes
{
    public class csCategorias : Paginas_Default.csMontaDDL
    {
        public csCategorias()
        {
            strTextoCombo = "a Categoria";
            tobjCa = typeof(caCategorias);
            objCon = new conCategorias();
        }
    }
}