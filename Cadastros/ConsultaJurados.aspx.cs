﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wappKaraoke.Classes;
using System.Data;

namespace wappKaraoke.Cadastros
{
    public partial class ConsultaJurados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                csCidades vcsCidades = new csCidades();
                cdCidade = vcsCidades.CarregaDDL(cdCidade);

                DataTable dt = new DataTable();
                dt.Columns.Add("cdJurado", typeof(int));
                dt.Columns.Add("nmJurado", typeof(string));
                dt.Columns.Add("nmJuradoKanji", typeof(string));
                dt.Columns.Add("cdCidade", typeof(string));

                for (int i = 1; i < 15; i++)
                {
                    DataRow dr = dt.NewRow();

                    dr["cdJurado"] = i;
                    dr["nmJurado"] = "Nome Jurado de teste - " + i;
                    dr["nmJuradoKanji"] = "Nome Kanji de teste - " + i;
                    dr["cdCidade"] = "Cidade teste - " + i;

                    dt.Rows.Add(dr);
                }

                gvDados.DataSource = dt;
                gvDados.DataBind();

                //Attribute to show the Plus Minus Button.
                gvDados.HeaderRow.Cells[1].Attributes["data-class"] = "expand";

                //Attribute to hide column in Phone.
                gvDados.HeaderRow.Cells[0].Attributes["data-hide"] = "phone";
                gvDados.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
                gvDados.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";

                //Adds THEAD and TBODY to GridView.
                gvDados.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
    }
}