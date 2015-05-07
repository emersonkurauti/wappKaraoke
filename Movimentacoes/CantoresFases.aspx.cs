using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using wappKaraoke.Classes;

namespace wappKaraoke.Movimentacoes
{
    public partial class CantoresFases : csPageDefault
    {
        public override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dtFase = new DataTable();
                dtFase.Columns.Add("nmCantor", typeof(string));
                DataTable dtProxFase = new DataTable();
                dtProxFase.Columns.Add("nmCantor", typeof(string));
                
                for (int i = 1; i < 6; i++)
                {
                    DataRow drFase = dtFase.NewRow();
                    DataRow drProxFase = dtProxFase.NewRow();

                    drFase["nmCantor"] = "cantor - " + i;
                    drProxFase["nmCantor"] = "cantor - " + i;

                    dtFase.Rows.Add(drFase);
                    dtProxFase.Rows.Add(drProxFase);
                }

                gvFase.DataSource = dtFase;
                gvFase.DataBind();

                gvProxFase.DataSource = dtProxFase;
                gvProxFase.DataBind();
            }

            base.Page_Load(sender, e);
        }

        public override void ConfirarGridView()
        {
            base.ConfirarGridView();

            //gvFase.HeaderRow.TableSection = TableRowSection.TableHeader;
            //gvProxFase.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
}