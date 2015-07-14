using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using wappKaraoke.Classes.Controller;
using wappKaraoke.Classes.Model.Fases;
using wappKaraoke.Classes.Model.Concursos;
using wappKaraoke.Classes.Model.Cantores;
using wappKaraoke.Classes.Model.Categorias;
using wappKaraoke.Classes.Model.Jurados;

namespace wappKaraoke.Classes.Model.Notas
{
    public class coNotas : KuraFrameWork.ClasseBase.csModelBase
    {
        private static int _CC_cdRegistro;
        public static int CC_cdRegistro
        {
            get { return coNotas._cdCategoria; }
            set { coNotas._cdCategoria = value; }
        }

        /// <summary>
        /// Atributos
        /// </summary>        
        private static int _cdCategoria;
        public int cdCategoria
        {
            get { return _cdCategoria; }
            set { _cdCategoria = value; }
        }

        private static int _cdFase;
        public int cdFase
        {
            get { return _cdFase; }
            set { _cdFase = value; }
        }

        private static int _cdCantor;
        public int cdCantor
        {
            get { return _cdCantor; }
            set { _cdCantor = value; }
        }

        private static int _cdConcurso;
        public int cdConcurso
        {
            get { return _cdConcurso; }
            set { _cdConcurso = value; }
        }

        private static int _cdJurado;
        public int cdJurado
        {
            get { return _cdJurado; }
            set { _cdJurado = value; }
        }

        private static string _deObservacao = "";
        public string deObservacao
        {
            get { return _deObservacao; }
            set { _deObservacao = value; }
        }

        private static int _nuNota;
        public int nuNota
        {
            get { return _nuNota; }
            set { _nuNota = value; }
        }

        private static string _CC_deCategoria = "";
        public string CC_deCategoria
        {
            get { return _CC_deCategoria; }
            set { _CC_deCategoria = value; }
        }

        private static string _CC_deFase = "";
        public string CC_deFase
        {
            get { return _CC_deFase; }
            set { _CC_deFase = value; }
        }

        private static string _CC_nmCantor = "";
        public string CC_nmCantor
        {
            get { return _CC_nmCantor; }
            set { _CC_nmCantor = value; }
        }

        private static string _CC_nmConcurso = "";
        public string CC_nmConcurso
        {
            get { return _CC_nmConcurso; }
            set { _CC_nmConcurso = value; }
        }

        private static string _CC_nmJurado = "";
        public string CC_nmJurado
        {
            get { return _CC_nmJurado; }
            set { _CC_nmJurado = value; }
        }

        private static string _CC_deFormula = "";
        public string CC_deFormula
        {
            get { return _CC_deFormula; }
            set { _CC_deFormula = value; }
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public coNotas()
        {
            AtualizaObj();
            LimparAtributos();
            _CC_cdRegistro = cdCategoria;
            tobjCA = typeof(caNotas);
        }

        /// <summary>
        /// Método sobrescrito por conta do campo calculado
        /// </summary>
        /// <param name="dtDados"></param>
        /// <returns></returns>
        public override bool Select(out DataTable dtDados)
        {
            if (base.Select(out dtDados))
            {
                conCategorias objConCategorias = new conCategorias();
                conFases objConFases = new conFases();
                conCantores objConCantores = new conCantores();
                conConcursos objConConcursos = new conConcursos();
                conJurados objConJurados = new conJurados();

                DataTable dtAux = dtDados;

                dtDados.Columns[caNotas.CC_deCategoria].ReadOnly = false;
                dtDados.Columns[caNotas.CC_deCategoria].MaxLength = 100;
                dtDados.Columns[caNotas.CC_deFase].ReadOnly = false;
                dtDados.Columns[caNotas.CC_deFase].MaxLength = 100;
                dtDados.Columns[caNotas.CC_nmCantor].ReadOnly = false;
                dtDados.Columns[caNotas.CC_nmCantor].MaxLength = 100;
                dtDados.Columns[caNotas.CC_nmConcurso].ReadOnly = false;
                dtDados.Columns[caNotas.CC_nmConcurso].MaxLength = 100;
                dtDados.Columns[caNotas.CC_nmJurado].ReadOnly = false;
                dtDados.Columns[caNotas.CC_nmJurado].MaxLength = 100;

                foreach (DataRow dr in dtAux.Rows)
                {
                    objConCategorias.objCoCategorias.LimparAtributos();
                    objConCategorias.objCoCategorias.cdCategoria = Convert.ToInt32(dr[caNotas.cdCategoria].ToString());

                    objConFases.objCoFases.LimparAtributos();
                    objConFases.objCoFases.cdFase = Convert.ToInt32(dr[caNotas.cdFase].ToString());

                    objConCantores.objCoCantores.LimparAtributos();
                    objConCantores.objCoCantores.cdCantor = Convert.ToInt32(dr[caNotas.cdCantor].ToString());

                    objConConcursos.objCoConcursos.LimparAtributos();
                    objConConcursos.objCoConcursos.cdConcurso = Convert.ToInt32(dr[caNotas.cdConcurso].ToString());

                    objConJurados.objCoJurados.LimparAtributos();
                    objConJurados.objCoJurados.cdJurado = Convert.ToInt32(dr[caNotas.cdJurado].ToString());


                    if (conFases.Select())
                    {
                        if (objConFases.dtDados.Rows.Count > 0)
                        {
                            dr[caNotas.CC_deFase] = objConFases.dtDados.Rows[0][caFases.deFase].ToString();
                        }
                    }
                    if (conCantores.Select())
                    {
                        if (objConCantores.dtDados.Rows.Count > 0)
                        {
                            dr[caNotas.CC_nmCantor] = objConCantores.dtDados.Rows[0][caCantores.nmCantor].ToString();

                        }
                    }
                    if (conConcursos.Select())
                    {
                        if (objConConcursos.dtDados.Rows.Count > 0)
                        {
                            dr[caNotas.CC_nmConcurso] = objConConcursos.dtDados.Rows[0][caConcursos.nmConcurso].ToString();

                        }
                    }
                    if (conJurados.Select())
                    {
                        if (objConJurados.dtDados.Rows.Count > 0)
                        {
                            dr[caNotas.CC_nmJurado] = objConJurados.dtDados.Rows[0][caJurados.nmJurado].ToString();
                        }
                    }
                }

                dtDados = dtAux;
            }
            else
                return false;

            return true;
        }

        /// <summary>
        /// Calcula a nota do cantor
        /// </summary>
        /// <param name="dtDados"></param>
        /// <returns></returns>
        public bool CalcularNota(out DataTable dtDados)
        {
            string strComando = @"select ROUND(" + _CC_deFormula + ", 2) as " + caNotas.CC_deFormula + " from dual";

            return objBanco.SelectPersonalizado(out dtDados, strComando);
        }

        /// <summary>
        /// Sobrescrito para retornar a chave
        /// </summary>
        /// <returns></returns>
        public override bool Inserir()
        {
            if (base.Inserir())
            {
                cdCategoria = objBanco.cdChave;
                return true;
            }

            return false;
        }
    }
}