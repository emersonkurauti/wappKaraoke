﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using wappKaraoke.Classes;

namespace wappKaraoke.Cadastros
{
    public partial class ConsultaCategorias : csPageDefault
    {
        public override void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("cdCategoria", typeof(int));
                dt.Columns.Add("deCategoria", typeof(string));
                dt.Columns.Add("deFormulaPontuacao", typeof(string));

                for (int i = 1; i < 15; i++)
                {
                    DataRow dr = dt.NewRow();

                    dr["cdCategoria"] = i;
                    dr["deCategoria"] = "Categoria de teste - " + i;
                    dr["deFormulaPontuacao"] = "N1*1 + N2*3 + N3*7";

                    dt.Rows.Add(dr);
                }

                gvDados.DataSource = dt;
                gvDados.DataBind();
            }

            base.Page_Load(sender, e);
        }

        public override void ConfirarGridView()
        {
            base.ConfirarGridView();

            //Attribute to show the Plus Minus Button.
            gvDados.HeaderRow.Cells[1].Attributes["data-class"] = "expand";

            //Attribute to hide column in Phone.
            gvDados.HeaderRow.Cells[0].Attributes["data-hide"] = "phone";
            gvDados.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";

            //Adds THEAD and TBODY to GridView.
            gvDados.HeaderRow.TableSection = TableRowSection.TableHeader;            
        }
    }
}