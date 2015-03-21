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
    public partial class ConsultaAssociacoes : csPageDefault
    {
        public override void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("cdAssociacao", typeof(int));
                dt.Columns.Add("nmAssociacao", typeof(string));

                for (int i = 1; i < 15; i++)
                {
                    DataRow dr = dt.NewRow();

                    dr["cdAssociacao"] = i;
                    dr["nmAssociacao"] = "Associação de teste - " + i;

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

            //Adds THEAD and TBODY to GridView.
            gvDados.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
}