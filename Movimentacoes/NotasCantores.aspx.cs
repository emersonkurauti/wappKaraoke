﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wappKaraoke.Classes;
using wappKaraoke.Classes.Mensagem;
using wappKaraoke.Classes.Controller;
using wappKaraoke.Classes.Model.Concursos;
using wappKaraoke.Classes.Model.Grupos;
using wappKaraoke.Classes.Model.Cantores;
using wappKaraoke.Classes.Model.Categorias;
using wappKaraoke.Classes.Model.Fases;
using wappKaraoke.Classes.Model.Notas;
using wappKaraoke.Classes.Model.CantoresFases;
using System.Data;
using System.Collections;

namespace wappKaraoke.Movimentacoes
{
    public partial class NotasCantores : csPage
    {
        public override void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                conConcursos objConConcursos = new conConcursos();
                objConConcursos.objCoConcursos.LimparAtributos();
                objConConcursos.objCoConcursos.strFiltro = " WHERE flConcursoCorrente = 'S'";

                if (conConcursos.Select())
                {
                    Session["cdConcursoNC"] = objConConcursos.dtDados.Rows[0][caConcursos.cdConcurso].ToString();
                    Session["cdFaseCorrenteNC"] = objConConcursos.dtDados.Rows[0][caConcursos.cdFaseCorrente].ToString();
                }

                if (Session["cdConcursoNC"] == null)
                {
                    ltMensagem.Text = MostraMensagem("Falha!", "Não existe concurso corrente definido.", csMensagem.msgDanger);
                    return;
                }

                if (Session["cdFaseCorrenteNC"] == null)
                {
                    ltMensagem.Text = MostraMensagem("Falha!", "Não existe fase corrente definida.", csMensagem.msgDanger);
                    return;
                }

                Session["NotasJuradosNC"] = null;
                CarregarProximoCantorSemNota();
            }
        }

        public void CarregaPainelNotaJurados()
        {
            ltNotasJurados.Text = "";

            conGrupos objConGrupos = new conGrupos();
            objConGrupos.objCoGrupos.LimparAtributos();
            objConGrupos.objCoGrupos.cdConcurso = Convert.ToInt32(Session["cdConcursoNC"].ToString());

            if (conGrupos.Select())
            {
                if (objConGrupos.dtDados.Rows.Count == 0)
                {
                    ltNotasJurados.Text = MostraMensagem("Validação!", "Não existem jurados no concurso.", csMensagem.msgDanger);
                    return;
                }

                Session["QtdJuradosNC"] = objConGrupos.dtDados.Rows.Count;
                Session["NotasJuradosNC"] = null;

                string strNota = "";
                string strObs = "";
                int index = 1;

                foreach (DataRow dr in objConGrupos.dtDados.Rows)
                {
                    if ((Session["cdCantorNC"] != null) && (Session["cdCategoriaNC"] != null) &&
                        (Session["cdFaseCorrenteNC"] != null) && (Session["cdConcursoNC"] != null))
                    {
                        conNotas objConNotas = new conNotas();
                        objConNotas.objCoNotas.LimparAtributos();
                        objConNotas.objCoNotas.cdCantor = Convert.ToInt32(Session["cdCantorNC"].ToString());
                        objConNotas.objCoNotas.cdCategoria = Convert.ToInt32(Session["cdCategoriaNC"].ToString());
                        objConNotas.objCoNotas.cdFase = Convert.ToInt32(Session["cdFaseCorrenteNC"].ToString());
                        objConNotas.objCoNotas.cdConcurso = Convert.ToInt32(Session["cdConcursoNC"].ToString());
                        objConNotas.objCoNotas.cdJurado = Convert.ToInt32(dr[caGrupos.cdJurado].ToString());

                        if (!conNotas.Select())
                        {
                            ltMensagem.Text = MostraMensagem("Falha!", "Erro ao carregar nota do cantor do banco de dados.", csMensagem.msgDanger);
                            return;
                        }


                        if (objConNotas.dtDados.Rows.Count > 0)
                        {
                            strNota = objConNotas.dtDados.Rows[0][caNotas.nuNota].ToString();
                            strObs = objConNotas.dtDados.Rows[0][caNotas.deObservacao].ToString();
                        }
                    }

                    ltNotasJurados.Text += csDinamico.strLinhaNotaJurado.Replace("[CC_nmJurado]",
                        dr[caGrupos.CC_nmJurado].ToString() + " [N" + dr[caGrupos.cdJurado].ToString() + "]").Replace("[cdJurado]", dr[caGrupos.cdJurado].ToString());

                    ltNotasJurados.Text = ltNotasJurados.Text.Replace("[Nota]", strNota);
                    ltNotasJurados.Text = ltNotasJurados.Text.Replace("[Obs]", strObs);

                    if (strNota != "")
                    {
                        if (Session["NotasJuradosNC"] == null)
                            Session["NotasJuradosNC"] = dr[caGrupos.cdJurado].ToString() + "=" + strNota + "=" + strObs;
                        else
                            Session["NotasJuradosNC"] += ";" + dr[caGrupos.cdJurado].ToString() + "=" + strNota + "=" + strObs;
                    }

                    index++;
                }
            }
        }

        private void CarregarProximoCantorSemNota()
        {
            ltMensagem.Text = "";

            conCantoresFases objConCantoresFases = new conCantoresFases();
            objConCantoresFases.objCoCantoresFases.LimparAtributos();
            objConCantoresFases.objCoCantoresFases.cdConcurso = Convert.ToInt32(Session["cdConcursoNC"].ToString());
            objConCantoresFases.objCoCantoresFases.cdFase = Convert.ToInt32(Session["cdFaseCorrenteNC"].ToString());
            objConCantoresFases.objCoCantoresFases.cdTpStatus = Convert.ToInt32(wappKaraoke.Properties.Settings.Default.sCodStatusCantou);

            if (!conCantoresFases.SelectProximoCantorSemNota())
            {
                ltMensagem.Text = MostraMensagem("Falha!", "Não foi possível carregar o próximo cantor sem nota.", csMensagem.msgDanger);
                return;
            }

            if (objConCantoresFases.dtDados.Rows.Count == 0)
            {
                Session["cdCantorNC"] = null;
                nuCantor.Text = "";
                pcDesconto.Text = "";
                deFormulaPontuacao.Text = "";
                nuNotaFinal.Text = "";

                CarregaPainelNotaJurados();
                ltMensagem.Text = MostraMensagem("Validação!", "Não existe cantor pronto para receber sua pontuação.", csMensagem.msgWarning);
                return;
            }

            Session["NotasJuradosNC"] = null;
            nuNotaFinal.Text = "";
            pcDesconto.Text = "";
            nuCantor.Text = "";

            Session["nuCantorNC"] = objConCantoresFases.dtDados.Rows[0][caCantoresFases.nuCantor].ToString();
            Session["cdCantorNC"] = objConCantoresFases.dtDados.Rows[0][caCantores.cdCantor].ToString();
            Session["cdCategoriaNC"] = objConCantoresFases.dtDados.Rows[0][caCategorias.cdCategoria].ToString();

            deFormulaPontuacao.Text = objConCantoresFases.dtDados.Rows[0][caCategorias.deFormulaPontuacao].ToString();

            ltMensagem.Text = MostraMensagem("Nº: " + objConCantoresFases.dtDados.Rows[0][caCantoresFases.nuCantor].ToString(),
                "Cantor: " + objConCantoresFases.dtDados.Rows[0][caCantores.nmCantor].ToString() + "<br/>" +
                "Categoria: " + objConCantoresFases.dtDados.Rows[0][caCategorias.deCategoria].ToString(),
                csMensagem.msgInfo);

            CarregaPainelNotaJurados();
        }

        private void CarregarNotasCantor(string psNuCantor)
        {
            if (psNuCantor == "")
            {
                ltMensagem.Text = MostraMensagem("Validação!", "Informe o número do cantor.", csMensagem.msgWarning);
                return;
            }

            ltMensagem.Text = "";

            conCantoresFases objConCantoresFases = new conCantoresFases();
            objConCantoresFases.objCoCantoresFases.LimparAtributos();
            objConCantoresFases.objCoCantoresFases.cdConcurso = Convert.ToInt32(Session["cdConcursoNC"].ToString());
            objConCantoresFases.objCoCantoresFases.cdFase = Convert.ToInt32(Session["cdFaseCorrenteNC"].ToString());
            objConCantoresFases.objCoCantoresFases.nuCantor = psNuCantor;

            if (!conCantoresFases.Select())
            {
                ltMensagem.Text = MostraMensagem("Falha!", "Não foi possível carregar cantor pelo número.", csMensagem.msgDanger);
                return;
            }

            if (objConCantoresFases.dtDados.Rows.Count == 0)
            {
                ltMensagem.Text = MostraMensagem("Validação!", "Cantor não localizado pelo número informado.", csMensagem.msgWarning);
                return;
            }

            Session["nuCantorNC"] = objConCantoresFases.dtDados.Rows[0][caCantoresFases.nuCantor].ToString();
            Session["cdCantorNC"] = objConCantoresFases.dtDados.Rows[0][caCantoresFases.cdCantor].ToString();
            Session["cdCategoriaNC"] = objConCantoresFases.dtDados.Rows[0][caCantoresFases.cdCategoria].ToString();
            Session["cdFaseNC"] = objConCantoresFases.dtDados.Rows[0][caCantoresFases.cdFase].ToString();

            deFormulaPontuacao.Text = objConCantoresFases.dtDados.Rows[0][caCantoresFases.CC_deFormulaPontuacao].ToString();
            nuNotaFinal.Text = objConCantoresFases.dtDados.Rows[0][caCantoresFases.nuNotafinal].ToString();
            pcDesconto.Text = objConCantoresFases.dtDados.Rows[0][caCantoresFases.pcDesconto].ToString();

            if (objConCantoresFases.dtDados.Rows[0][caCantoresFases.cdTpStatus].ToString() !=
                wappKaraoke.Properties.Settings.Default.sCodStatusCantou)
            {
                ltMensagem.Text = MostraMensagem("Nº: " + objConCantoresFases.dtDados.Rows[0][caCantoresFases.nuCantor].ToString(),
                    "Cantor: " + objConCantoresFases.dtDados.Rows[0][caCantoresFases.CC_nmCantor].ToString() + "<br/>" +
                    "Categoria: " + objConCantoresFases.dtDados.Rows[0][caCantoresFases.CC_deCategoria].ToString(),
                    csMensagem.msgDanger);
            }
            else if (objConCantoresFases.dtDados.Rows[0][caCantoresFases.nuNotafinal].ToString() == "" ||
                objConCantoresFases.dtDados.Rows[0][caCantoresFases.nuNotafinal].ToString() == "0")
            {
                ltMensagem.Text = MostraMensagem("Nº: " + objConCantoresFases.dtDados.Rows[0][caCantoresFases.nuCantor].ToString(),
                    "Cantor: " + objConCantoresFases.dtDados.Rows[0][caCantoresFases.CC_nmCantor].ToString() + "<br/>" +
                    "Categoria: " + objConCantoresFases.dtDados.Rows[0][caCantoresFases.CC_deCategoria].ToString(),
                    csMensagem.msgInfo);
            }
            else
            {
                ltMensagem.Text = MostraMensagem("Nº: " + objConCantoresFases.dtDados.Rows[0][caCantoresFases.nuCantor].ToString(),
                    "Cantor: " + objConCantoresFases.dtDados.Rows[0][caCantoresFases.CC_nmCantor].ToString() + "<br/>" +
                    "Categoria: " + objConCantoresFases.dtDados.Rows[0][caCantoresFases.CC_deCategoria].ToString(),
                    csMensagem.msgSucess);
            }

            CarregaPainelNotaJurados();
        }

        private void CalculaNota()
        {
            hfNotas.Value = hfNotas.Value.ToString().Substring(0, hfNotas.Value.Length - 1);

            Session["NotasJuradosNC"] = hfNotas.Value.ToString();

            string strFormula = deFormulaPontuacao.Text;
            string[] vNotasJurados = hfNotas.Value.ToString().Split(';');
            string[] vNotas;

            foreach (string sNota in vNotasJurados)
            {
                vNotas = sNota.Split('=');
                strFormula = strFormula.Replace("[N" + vNotas[0] + "]", vNotas[1]);

                Session[csDinamico.strCampoNotaJurado.Replace("[cdJurado]", vNotas[0])] = vNotas[1];
                Session[csDinamico.strCampoObsJurado.Replace("[cdJurado]", vNotas[0])] = vNotas[2];
            }

            int cdFase = 0, posFase = 0;

            do
            {
                cdFase = 0;
                posFase = 0;

                posFase = strFormula.IndexOf('F');
                if (posFase != -1)
                {
                    cdFase = Convert.ToInt32(strFormula.Substring(posFase + 1, 1));

                    if (cdFase != Convert.ToInt32(Session["cdFaseCorrenteNC"]))
                    {
                        conCantoresFases objConCantoresFases = new conCantoresFases();
                        objConCantoresFases.objCoCantoresFases.LimparAtributos();
                        objConCantoresFases.objCoCantoresFases.cdConcurso = Convert.ToInt32(Session["cdConcursoNC"]);
                        objConCantoresFases.objCoCantoresFases.cdCategoria = Convert.ToInt32(Session["cdCategoriaNC"].ToString());
                        objConCantoresFases.objCoCantoresFases.cdCantor = Convert.ToInt32(Session["cdCantorNC"].ToString());
                        objConCantoresFases.objCoCantoresFases.cdFase = cdFase;

                        if (!conCantoresFases.Select())
                        {
                            ltMensagem.Text = MostraMensagem("Falha!", "Falha ao consultar nota da fase anterior.", csMensagem.msgDanger);
                            return;
                        }

                        if (objConCantoresFases.dtDados != null && objConCantoresFases.dtDados.Rows.Count > 0)
                        {
                            strFormula = strFormula.Replace("[F" + cdFase + "]",
                                objConCantoresFases.dtDados.Rows[0][caCantoresFases.nuNotafinal].ToString());
                        }
                    }
                    else
                    {
                        strFormula = strFormula.Replace("[F" + cdFase + "]", "0");
                    }
                }
            } while (cdFase != 0);

            double dpcDesconto;
            Double.TryParse(pcDesconto.Text, out dpcDesconto);
            dpcDesconto = (100 - dpcDesconto) / 100;
            strFormula += "*" + dpcDesconto;
            strFormula = strFormula.Replace(',', '.');

            conNotas objConNotas = new conNotas();
            objConNotas.objCoNotas.LimparAtributos();
            objConNotas.objCoNotas.CC_deFormula = strFormula;

            if (!conNotas.CalcularNota())
            {
                ltMensagem.Text = MostraMensagem("Falha!", "Erro ao calcular nota do cantor.", csMensagem.msgDanger);
                return;
            }

            nuNotaFinal.Text = objConNotas.dtDados.Rows[0][caNotas.CC_deFormula].ToString();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            CalculaNota();

            if (Session["NotasJuradosNC"] == null)
            {
                ltMensagem.Text = MostraMensagem("Validação!", "Notas não informadas.", csMensagem.msgWarning);
                return;
            }

            if (Session["QtdJuradosNC"] == null)
            { 
                return;
            }

            string[] vNotasJurados = Session["NotasJuradosNC"].ToString().Split(';');

            if (vNotasJurados.Length != Convert.ToInt32(Session["QtdJuradosNC"].ToString()))
            {
                ltMensagem.Text = MostraMensagem("Validação!", "Devem ser informadas todas as notas.", csMensagem.msgWarning);
                return;
            }

            DataTable _dtNotas = conNotas.objCo.RetornaEstruturaDT();
            DataRow _drNota;
            string[] vNotas;

            for (int i = 0; i < vNotasJurados.Length; i++)
            {
                vNotas = vNotasJurados[i].Split('=');
                _drNota = _dtNotas.NewRow();

                _drNota[caNotas.cdConcurso] = Convert.ToInt32(Session["cdConcursoNC"].ToString());
                _drNota[caNotas.cdCantor] = Convert.ToInt32(Session["cdCantorNC"].ToString());
                _drNota[caNotas.cdCategoria] = Convert.ToInt32(Session["cdCategoriaNC"].ToString());
                _drNota[caNotas.cdFase] = Convert.ToInt32(Session["cdFaseCorrenteNC"].ToString());
                _drNota[caNotas.cdJurado] = Convert.ToInt32(vNotas[0]);
                _drNota[caNotas.nuNota] = Convert.ToDecimal(vNotas[1]);
                _drNota[caNotas.deObservacao] = vNotas[2];

                _dtNotas.Rows.Add(_drNota);
            }

            decimal dNuNotaFina;
            decimal dpcDesconto;

            Decimal.TryParse(nuNotaFinal.Text, out dNuNotaFina);
            Decimal.TryParse(pcDesconto.Text, out dpcDesconto);

            conCantoresFases objConCantoresFases = new conCantoresFases();
            objConCantoresFases.objCoCantoresFases.LimparAtributos();
            objConCantoresFases.objCoCantoresFases.cdConcurso = Convert.ToInt32(Session["cdConcursoNC"].ToString());
            objConCantoresFases.objCoCantoresFases.cdCantor = Convert.ToInt32(Session["cdCantorNC"].ToString());
            objConCantoresFases.objCoCantoresFases.cdCategoria = Convert.ToInt32(Session["cdCategoriaNC"].ToString());
            objConCantoresFases.objCoCantoresFases.cdFase = Convert.ToInt32(Session["cdFaseCorrenteNC"].ToString());
            objConCantoresFases.objCoCantoresFases.nuNotafinal = dNuNotaFina;
            objConCantoresFases.objCoCantoresFases.pcDesconto = dpcDesconto;
            objConCantoresFases.objCoCantoresFases.dtNotas = _dtNotas;

            if (!conCantoresFases.AlterarNotaCantor())
            {
                ltMensagem.Text = MostraMensagem("Falha!", "Não foi possível inserir as notas do cantor.", csMensagem.msgDanger);
                return;
            }

            Session["NotasJuradosNC"] = null;
            CarregarNotasCantor(Session["nuCantorNC"].ToString());

            ltMensagem.Text = MostraMensagem("Sucesso!", "As notas foram salvas com sucesso.", csMensagem.msgSucess);
        }

        protected void btnAtualizar_Click(object sender, EventArgs e)
        {
            Session["NotasJuradosNC"] = null;
            CarregarProximoCantorSemNota();
        }

        protected void nuCantor_TextChanged(object sender, EventArgs e)
        {
            Session["NotasJuradosNC"] = null;
            CarregarNotasCantor(nuCantor.Text);
        }
    }
}