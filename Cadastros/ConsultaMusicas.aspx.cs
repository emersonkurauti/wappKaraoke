using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using wappKaraoke.Classes;
using wappKaraoke.Classes.Model.Musicas;
using wappKaraoke.Classes.Controller;
using wappKaraoke.Classes.Mensagem;

namespace wappKaraoke.Cadastros
{
    public partial class ConsultaMusicas : csPageDefault
    {
        public override void Page_Load(object sender, EventArgs e)
        {
            lnkBuscar = btnBuscar;
            gvDadosDefault = gvDados;
            ltMensagemDefault = ltMensagem;
            tobjCa = typeof(caMusicas);
            objCon = new conMusicas();

            base.Page_Load(sender, e);

            NomeKanji();
        }

        protected override bool ConfigurarGridView()
        {
            if (!base.ConfigurarGridView())
                return false;

            try
            {
                //Attribute to show the Plus Minus Button.
                gvDados.HeaderRow.Cells[1].Attributes["data-class"] = "expand";

                //Attribute to hide column in Phone.
                gvDados.HeaderRow.Cells[0].Attributes["data-hide"] = "phone";
                gvDados.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
                gvDados.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
                gvDados.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";

                //Adds THEAD and TBODY to GridView.
                gvDados.HeaderRow.TableSection = TableRowSection.TableHeader;

                return true;
            }
            catch
            {
                return false;
            }
        }

        protected override void btnBuscar_Click(object sender, EventArgs e)
        {
            base.btnBuscar_Click(sender, e);

            NomeKanji();
        }

        protected void NomeKanji()
        {
            if (dtDados != null)
            {
                for (int i = 0; i < dtDados.Rows.Count; i++)
                {
                    ((Literal)gvDados.Rows[i].FindControl("ltNomeKanji")).Text = @"" + dtDados.Rows[i]["nmMusica"].ToString() + " <br/> " +
                        dtDados.Rows[i]["nmMusicaKanji"].ToString();

                    ((Literal)gvDados.Rows[i].FindControl("ltNuAnoLanc")).Text = dtDados.Rows[i][caMusicas.nuAnoLanc].ToString();
                    
                    if (dtDados.Rows[i][caMusicas.nuAnoLanc].ToString() == "0")
                        ((Literal)gvDados.Rows[i].FindControl("ltNuAnoLanc")).Text = "";
                }
            }
        }
    }
}