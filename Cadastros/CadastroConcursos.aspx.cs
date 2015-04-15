using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wappKaraoke.Classes;
using System.Data;

namespace wappKaraoke.Cadastros
{
    public partial class CadastroConcursos : csPageDefault
    {
        public override void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                csCidades vcsCidades = new csCidades();
                cdCidade = vcsCidades.CarregaDDL(cdCidade);

                csAssociacoes vcsAssociacoes = new csAssociacoes();
                cdAssociacao = vcsAssociacoes.CarregaDDL(cdAssociacao);

                csJurados vcsJurados = new csJurados();
                cdJurado = vcsJurados.CarregaDDL(cdJurado);

                csFases vcsFases = new csFases();
                cdFase = vcsFases.CarregaDDL(cdFase);

                //Filtrar somente as associações adicionadas no concurso
                csAssociacoes vcsAssociacoesCantor = new csAssociacoes();
                cdAssociacaoCantor = vcsAssociacoesCantor.CarregaDDL(cdAssociacaoCantor);

                csCantores vcsCantores = new csCantores();
                cdCantor = vcsCantores.CarregaDDL(cdCantor);

                //Associações
                DataTable dt = new DataTable();
                dt.Columns.Add("cdAssociacao", typeof(int));
                dt.Columns.Add("nmAssociacao", typeof(string));
                dt.Columns.Add("nmRepresentante", typeof(string));
                dt.Columns.Add("deEmail", typeof(string));

                for (int i = 0; i < 4; i++)
                {
                    DataRow dr = dt.NewRow();

                    dr["cdAssociacao"] = i;
                    dr["nmAssociacao"] = "Nome Associação de teste - " + i;
                    dr["nmRepresentante"] = "Nome Representante de teste - " + i;
                    dr["deEmail"] = "emailteste" + i + "@hotmail.com";

                    dt.Rows.Add(dr);
                }

                gvAssociacoes.DataSource = dt;
                gvAssociacoes.DataBind();

                //Jurados
                dt = new DataTable();
                dt.Columns.Add("cdJurado", typeof(int));
                dt.Columns.Add("nmJurado", typeof(string));
                dt.Columns.Add("nmNomeKanji", typeof(string));
                dt.Columns.Add("deGrupo", typeof(string));

                for (int i = 0; i < 4; i++)
                {
                    DataRow dr = dt.NewRow();

                    dr["cdJurado"] = i;
                    dr["nmJurado"] = "Nome Jurado de teste - " + i;
                    dr["nmNomeKanji"] = "Nome KANJI" + i;
                    dr["deGrupo"] = "GRUPO " + i;

                    dt.Rows.Add(dr);
                }

                gvGrupoJuradoConcurso.DataSource = dt;
                gvGrupoJuradoConcurso.DataBind();

                for (int i = 0; i < 4; i++)
                {
                    ((Literal)gvGrupoJuradoConcurso.Rows[i].FindControl("ltNomeKanji")).Text = @"" + dt.Rows[i]["nmJurado"].ToString() + " <br/> " + dt.Rows[i]["nmNomeKanji"].ToString();
                }

                //Fases
                dt = new DataTable();
                dt.Columns.Add("cdFase", typeof(int));
                dt.Columns.Add("deFase", typeof(string));

                for (int i = 0; i < 4; i++)
                {
                    DataRow dr = dt.NewRow();

                    dr["cdFase"] = i;
                    dr["deFase"] = "Fase Teste - " + i;

                    dt.Rows.Add(dr);
                }

                gvFasesConcurso.DataSource = dt;
                gvFasesConcurso.DataBind();

                //Cantores
                dt = new DataTable();
                dt.Columns.Add("cdCantor", typeof(int));
                dt.Columns.Add("nmCantor", typeof(string));
                dt.Columns.Add("nmAssociacao", typeof(string));

                for (int i = 0; i < 4; i++)
                {
                    DataRow dr = dt.NewRow();

                    dr["cdCantor"] = i;
                    dr["nmCantor"] = "Cantor de Teste - " + i;
                    dr["nmAssociacao"] = "Associacao de Teste - " + i;

                    dt.Rows.Add(dr);
                }

                gvCantoresConcurso.DataSource = dt;
                gvCantoresConcurso.DataBind();
            }

            base.Page_Load(sender, e);
        }

        public override void ConfirarGridView()
        {
            base.ConfirarGridView();

            //Associações
            //Attribute to show the Plus Minus Button.
            gvAssociacoes.HeaderRow.Cells[1].Attributes["data-class"] = "expand";

            //Attribute to hide column in Phone.
            gvAssociacoes.HeaderRow.Cells[0].Attributes["data-hide"] = "phone";
            gvAssociacoes.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
            gvAssociacoes.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
            gvAssociacoes.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
            gvAssociacoes.HeaderRow.Cells[5].Attributes["data-hide"] = "phone";

            //Adds THEAD and TBODY to GridView.
            gvAssociacoes.HeaderRow.TableSection = TableRowSection.TableHeader;

            //Jurados
            //Attribute to show the Plus Minus Button.
            gvGrupoJuradoConcurso.HeaderRow.Cells[2].Attributes["data-class"] = "expand";

            //Attribute to hide column in Phone.
            gvGrupoJuradoConcurso.HeaderRow.Cells[0].Attributes["data-hide"] = "phone";
            gvGrupoJuradoConcurso.HeaderRow.Cells[1].Attributes["data-hide"] = "phone";
            gvGrupoJuradoConcurso.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
            gvGrupoJuradoConcurso.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";

            //Adds THEAD and TBODY to GridView.
            gvGrupoJuradoConcurso.HeaderRow.TableSection = TableRowSection.TableHeader;

            //Fases
            //Attribute to show the Plus Minus Button.
            gvFasesConcurso.HeaderRow.Cells[1].Attributes["data-class"] = "expand";

            //Attribute to hide column in Phone.
            gvFasesConcurso.HeaderRow.Cells[0].Attributes["data-hide"] = "phone";
            gvFasesConcurso.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
            gvFasesConcurso.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";

            //Adds THEAD and TBODY to GridView.
            gvFasesConcurso.HeaderRow.TableSection = TableRowSection.TableHeader;

            //Cantores
            //Attribute to show the Plus Minus Button.
            gvCantoresConcurso.HeaderRow.Cells[1].Attributes["data-class"] = "expand";

            //Attribute to hide column in Phone.
            gvCantoresConcurso.HeaderRow.Cells[0].Attributes["data-hide"] = "phone";
            gvCantoresConcurso.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
            gvCantoresConcurso.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
            gvCantoresConcurso.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";

            //Adds THEAD and TBODY to GridView.
            gvCantoresConcurso.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
}