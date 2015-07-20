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
                    Session["cdConcurso"] = objConConcursos.dtDados.Rows[0][caConcursos.cdConcurso].ToString();
                }

                if (Session["cdConcurso"] == null)
                {
                    ltMensagem.Text = MostraMensagem("Falha!", "Não existe concurso corrente definido.", csMensagem.msgDanger);
                    return;
                }

                CarregarProximoCantorSemNota();
            }
        }

        public void CarregaPainelNotaJurados()
        {
            ltNotasJurados.Text = "";

            conGrupos objConGrupos = new conGrupos();
            objConGrupos.objCoGrupos.LimparAtributos();
            objConGrupos.objCoGrupos.cdConcurso = Convert.ToInt32(Session["cdConcurso"].ToString());

            if (conGrupos.Select())
            {
                if (objConGrupos.dtDados.Rows.Count == 0)
                {
                    ltNotasJurados.Text = MostraMensagem("Validação!", "Não existem jurados no concurso.", csMensagem.msgDanger);
                    return;
                }

                Session["QtdJurados"] = objConGrupos.dtDados.Rows.Count;

                bool bTemNotaGravada = false;
                string strNota = "";
                string strObs = "";
                int index = 1;

                foreach (DataRow dr in objConGrupos.dtDados.Rows)
                {
                    if ((Session["cdCantor"] != null) && (Session["cdCategoria"] != null) &&
                        (Session["cdFase"] != null) && (Session["cdConcurso"] != null))
                    {
                        conNotas objConNotas = new conNotas();
                        objConNotas.objCoNotas.LimparAtributos();
                        objConNotas.objCoNotas.cdCantor = Convert.ToInt32(Session["cdCantor"].ToString());
                        objConNotas.objCoNotas.cdCategoria = Convert.ToInt32(Session["cdCategoria"].ToString());
                        objConNotas.objCoNotas.cdFase= Convert.ToInt32(Session["cdFase"].ToString());
                        objConNotas.objCoNotas.cdConcurso = Convert.ToInt32(Session["cdConcurso"].ToString());
                        objConNotas.objCoNotas.cdJurado = Convert.ToInt32(dr[caGrupos.cdJurado].ToString());

                        if (!conNotas.Select())
                        {
                            ltMensagem.Text = MostraMensagem("Falha!", "Erro ao carregar nota do cantor do banco de dados.", csMensagem.msgDanger);
                            return;
                        }


                        if (objConNotas.dtDados.Rows.Count > 0)
                        {
                            bTemNotaGravada = true;
                            strNota = objConNotas.dtDados.Rows[0][caNotas.nuNota].ToString();
                            strObs = objConNotas.dtDados.Rows[0][caNotas.deObservacao].ToString();
                        }
                    }


                    if (!bTemNotaGravada)
                    {
                        if (Session[csDinamico.strCampoNotaJurado.Replace("[cdJurado]", "N" + (index))] != null)
                            strNota = Session[csDinamico.strCampoNotaJurado.Replace("[cdJurado]", "N" + (index))].ToString();

                        if (Session[csDinamico.strCampoObsJurado.Replace("[cdJurado]", "N" + (index))] != null)
                            strObs = Session[csDinamico.strCampoObsJurado.Replace("[cdJurado]", "N" + (index))].ToString();
                    }

                    ltNotasJurados.Text += csDinamico.strLinhaNotaJurado.Replace("[CC_nmJurado]",
                        dr[caGrupos.CC_nmJurado].ToString() + " [N" + (index) + "]").Replace("[cdJurado]", "N" + (index));

                    ltNotasJurados.Text = ltNotasJurados.Text.Replace("[Nota]", strNota);
                    ltNotasJurados.Text = ltNotasJurados.Text.Replace("[Obs]", strObs);

                    if (Session["NotasJurados"] == null)
                        Session["NotasJurados"] = dr[caGrupos.cdJurado].ToString() + "=" + strNota + "=" + strObs;
                    else
                        Session["NotasJurados"] = ";" + dr[caGrupos.cdJurado].ToString() + "=" + strNota + "=" + strObs;

                    index++;
                }
            }
        }

        private void CarregarProximoCantorSemNota()
        {
            ltMensagem.Text = "";

            conCantoresFases objConCantoresFases = new conCantoresFases();
            objConCantoresFases.objCoCantoresFases.LimparAtributos();
            objConCantoresFases.objCoCantoresFases.cdConcurso = Convert.ToInt32(Session["cdConcurso"].ToString());
            objConCantoresFases.objCoCantoresFases.cdTpStatus = Convert.ToInt32(wappKaraoke.Properties.Settings.Default.sCodStatusCantou);

            if (!conCantoresFases.SelectProximoCantorSemNota())
            {
                ltMensagem.Text = MostraMensagem("Falha!", "Não foi possível carregar o próximo cantor sem nota.", csMensagem.msgDanger);
                return;
            }

            if (objConCantoresFases.dtDados.Rows.Count == 0)
            {
                ltMensagem.Text = MostraMensagem("Validação!", "Não existe cantor pronto para receber sua pontuação.", csMensagem.msgWarning);
                return;
            }

            Session["nuCantor"] = objConCantoresFases.dtDados.Rows[0][caCantoresFases.nuCantor].ToString();
            Session["cdCantor"] = objConCantoresFases.dtDados.Rows[0][caCantores.cdCantor].ToString();
            Session["cdCategoria"] = objConCantoresFases.dtDados.Rows[0][caCategorias.cdCategoria].ToString();
            Session["cdFase"] = objConCantoresFases.dtDados.Rows[0][caFases.cdFase].ToString();

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
            objConCantoresFases.objCoCantoresFases.cdConcurso = Convert.ToInt32(Session["cdConcurso"].ToString());
            //objConCantoresFases.objCoCantoresFases.cdFase = Convert.ToInt32(Session["cdFase"].ToString());
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

            Session["nuCantor"] = objConCantoresFases.dtDados.Rows[0][caCantoresFases.nuCantor].ToString();
            Session["cdCantor"] = objConCantoresFases.dtDados.Rows[0][caCantoresFases.cdCantor].ToString();
            Session["cdCategoria"] = objConCantoresFases.dtDados.Rows[0][caCantoresFases.cdCategoria].ToString();
            Session["cdFase"] = objConCantoresFases.dtDados.Rows[0][caCantoresFases.cdFase].ToString();

            deFormulaPontuacao.Text = objConCantoresFases.dtDados.Rows[0][caCantoresFases.CC_deFormulaPontuacao].ToString();

            ltMensagem.Text = MostraMensagem("Nº: " + objConCantoresFases.dtDados.Rows[0][caCantoresFases.nuCantor].ToString(),
                "Cantor: " + objConCantoresFases.dtDados.Rows[0][caCantoresFases.CC_nmCantor].ToString() + "<br/>" +
                "Categoria: " + objConCantoresFases.dtDados.Rows[0][caCantoresFases.CC_deCategoria].ToString(),
                csMensagem.msgInfo);

            CarregaPainelNotaJurados();
        }

        private void CalculaNota()
        {
            hfNotas.Value = hfNotas.Value.ToString().Substring(0, hfNotas.Value.Length - 1);

            string strFormula = deFormulaPontuacao.Text;
            string[] vNotasJurados = hfNotas.Value.ToString().Split(';');
            string[] vNotas;

            foreach (string sNota in vNotasJurados)
            {
                vNotas = sNota.Split('=');
                strFormula = strFormula.Replace("[" + vNotas[0] + "]", vNotas[1]);

                Session[csDinamico.strCampoNotaJurado.Replace("[cdJurado]", vNotas[0])] = vNotas[1];
                Session[csDinamico.strCampoObsJurado.Replace("[cdJurado]", vNotas[0])] = vNotas[2];
            }

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
            CarregaPainelNotaJurados();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            CalculaNota();

            if (Session["NotasJurados"] == null)
            {
                ltMensagem.Text = MostraMensagem("Validação!", "Notas não informadas.", csMensagem.msgWarning);
                return;
            }

            if (Session["QtdJurados"] == null)
            { 
                return;
            }

            string[] vNotasJurados = Session["NotasJurados"].ToString().Split(';');

            if (vNotasJurados.Length != Convert.ToInt32(Session["QtdJurados"].ToString()))
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

                _drNota[caNotas.cdConcurso] = Convert.ToInt32(Session["cdConcurso"].ToString());
                _drNota[caNotas.cdCantor] = Convert.ToInt32(Session["cdCantor"].ToString());
                _drNota[caNotas.cdCategoria] = Convert.ToInt32(Session["cdCategoria"].ToString());
                _drNota[caNotas.cdFase] = Convert.ToInt32(Session["cdFase"].ToString());
                _drNota[caNotas.cdJurado] = Convert.ToInt32(vNotas[0]);
                _drNota[caNotas.nuNota] = Convert.ToInt32(vNotas[1]);
                _drNota[caNotas.deObservacao] = vNotas[2];
            }

            conCantoresFases objConCantoresFases = new conCantoresFases();
            objConCantoresFases.objCoCantoresFases.LimparAtributos();
            objConCantoresFases.objCoCantoresFases.cdConcurso = Convert.ToInt32(Session["cdConcurso"].ToString());
            objConCantoresFases.objCoCantoresFases.cdCantor = Convert.ToInt32(Session["cdCantor"].ToString());
            objConCantoresFases.objCoCantoresFases.cdCategoria = Convert.ToInt32(Session["cdCategoria"].ToString());
            objConCantoresFases.objCoCantoresFases.cdFase = Convert.ToInt32(Session["cdFase"].ToString());
            objConCantoresFases.objCoCantoresFases.nuNotafinal = Convert.ToInt32(nuNotaFinal.Text);
            objConCantoresFases.objCoCantoresFases.pcDesconto = Convert.ToInt32(pcDesconto.Text);
            objConCantoresFases.objCoCantoresFases.dtNotas = _dtNotas;

            if (!conCantoresFases.AlterarNotaCantor())
            {
                ltMensagem.Text = MostraMensagem("Falha!", "Não foi possível inserir as notas do cantor.", csMensagem.msgDanger);
                return;
            }

            CarregarNotasCantor(Session["nuCantor"].ToString());
        }

        protected void btnAtualizar_Click(object sender, EventArgs e)
        {
            CarregarProximoCantorSemNota();
        }

        protected void nuCantor_TextChanged(object sender, EventArgs e)
        {
            CarregarNotasCantor(nuCantor.Text);
        }
        
        //Salvar -> calcula Salva: nota final, desconto, notas dos jurados
        //Atualiza -> Recarrega buscanco o primeiro cantor cantado sem nota, caso nao exista, carrega o ultimo cantado
        //Digitar o numero do cantor carrega ele
    }
}