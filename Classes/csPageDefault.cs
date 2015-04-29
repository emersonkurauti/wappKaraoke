using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wappKaraoke.Classes
{
    public class csPageDefault : System.Web.UI.Page
    {
        public virtual void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                InicializaSessions();
            }
            ConfirarGridView();
        }

        public virtual void ConfirarGridView()
        { 
        }

        public virtual void InicializaSessions()
        { 
        }
    }
}