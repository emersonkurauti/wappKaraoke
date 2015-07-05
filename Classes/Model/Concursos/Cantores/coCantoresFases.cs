using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using wappKaraoke.Classes.Controller;
using wappKaraoke.Classes.Model.Fases;
using wappKaraoke.Classes.Model.Categorias;
using wappKaraoke.Classes.Model.Musicas;
using wappKaraoke.Classes.Model.TipoStatus;
using wappKaraoke.Classes.Model.Cantores;

namespace wappKaraoke.Classes.Model.CantoresFases
{
    public class coCantoresFases : KuraFrameWork.ClasseBase.csModelBase
    {
        private static  int _CC_cdRegistro;
        public static int CC_cdRegistro
        {
            get { return coCantoresFases._cdConcurso; }
            set { coCantoresFases._cdConcurso = value; }
        }

		/// <summary>
		/// Atributos
		/// </summary>        
        private static string _CC_Controle = "";
        public string CC_Controle
        {
            get { return _CC_Controle; }
            set { _CC_Controle = value; }
        }

		private static int _cdConcurso;
        public int cdConcurso
        {
            get { return _cdConcurso; }
            set { _cdConcurso = value; }
        }

		private static int _cdCantor;
        public int cdCantor
        {
            get { return _cdCantor; }
            set { _cdCantor = value; }
        }

		private static int _cdMusica;
        public int cdMusica
        {
            get { return _cdMusica; }
            set { _cdMusica = value; }
        }

		private static int _cdTpStatus;
        public int cdTpStatus
        {
            get { return _cdTpStatus; }
            set { _cdTpStatus = value; }
        }

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

		private static string _nuCantor = "";
        public string nuCantor
        {
            get { return _nuCantor; }
            set { _nuCantor = value; }
        }

		private static string _deCaminhoMusica = "";
        public string deCaminhoMusica
        {
            get { return _deCaminhoMusica; }
            set { _deCaminhoMusica = value; }
        }

		private static int _nuOrdemApresentacao;
        public int nuOrdemApresentacao
        {
            get { return _nuOrdemApresentacao; }
            set { _nuOrdemApresentacao = value; }
        }

		private static decimal _nuNotafinal;
        public decimal nuNotafinal
        {
            get { return _nuNotafinal; }
            set { _nuNotafinal = value; }
        }

		private static decimal _pcDesconto;
        public decimal pcDesconto
        {
            get { return _pcDesconto; }
            set { _pcDesconto = value; }
        }

		private static string _CC_nmCantor = "";
        public string CC_nmCantor
        {
            get { return _CC_nmCantor; }
            set { _CC_nmCantor = value; }
        }

		private static string _CC_nmMusica = "";
        public string CC_nmMusica
        {
            get { return _CC_nmMusica; }
            set { _CC_nmMusica = value; }
        }

		private static string _CC_deTpStatus = "";
        public string CC_deTpStatus
        {
            get { return _CC_deTpStatus; }
            set { _CC_deTpStatus = value; }
        }

		private static string _CC_deFase = "";
        public string CC_deFase
        {
            get { return _CC_deFase; }
            set { _CC_deFase = value; }
        }

		private static string _CC_deCategoria = "";
        public string CC_deCategoria
        {
            get { return _CC_deCategoria; }
            set { _CC_deCategoria = value; }
        }

		private static string _CC_nmNomeKanji = "";
        public string CC_nmNomeKanji
        {
            get { return _CC_nmNomeKanji; }
            set { _CC_nmNomeKanji = value; }
        }

		private static string _CC_nmMusicaKanji = "";
        public string CC_nmMusicaKanji
        {
            get { return _CC_nmMusicaKanji; }
            set { _CC_nmMusicaKanji = value; }
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public coCantoresFases()
        {
            AtualizaObj();
            LimparAtributos();
            _CC_cdRegistro = cdConcurso;
            tobjCA = typeof(caCantoresFases);
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
                conCantores objConCantores = new conCantores();
                conMusicas objConMusicas = new conMusicas();
                conTipoStatus objConTipoStatus = new conTipoStatus();
                conFases objConFases = new conFases();
                conCategorias objConCategorias = new conCategorias();

                DataTable dtAux = dtDados;

                dtDados.Columns[caCantoresFases.CC_Controle].ReadOnly = false;
                dtDados.Columns[caCantoresFases.CC_nmCantor].ReadOnly = false;
                dtDados.Columns[caCantoresFases.CC_nmCantor].MaxLength = 100;
                dtDados.Columns[caCantoresFases.CC_nmMusica].ReadOnly = false;
                dtDados.Columns[caCantoresFases.CC_nmMusica].MaxLength = 100;
                dtDados.Columns[caCantoresFases.CC_deTpStatus].ReadOnly = false;
                dtDados.Columns[caCantoresFases.CC_deTpStatus].MaxLength = 100;
                dtDados.Columns[caCantoresFases.CC_deFase].ReadOnly = false;
                dtDados.Columns[caCantoresFases.CC_deFase].MaxLength = 100;
                dtDados.Columns[caCantoresFases.CC_deCategoria].ReadOnly = false;
                dtDados.Columns[caCantoresFases.CC_deCategoria].MaxLength = 100;
                dtDados.Columns[caCantoresFases.CC_nmNomeKanji].ReadOnly = false;
                dtDados.Columns[caCantoresFases.CC_nmNomeKanji].MaxLength = 100;
                dtDados.Columns[caCantoresFases.CC_nmMusicaKanji].ReadOnly = false;
                dtDados.Columns[caCantoresFases.CC_nmMusicaKanji].MaxLength = 100;

                foreach (DataRow dr in dtAux.Rows)
                {
                    objConCantores.objCoCantores.LimparAtributos();
                    objConCantores.objCoCantores.cdCantor = Convert.ToInt32(dr[caCantoresFases.cdCantor].ToString());
                    objConMusicas.objCoMusicas.LimparAtributos();
                    objConMusicas.objCoMusicas.cdMusica = Convert.ToInt32(dr[caCantoresFases.cdMusica].ToString());
                    objConTipoStatus.objCoTipoStatus.LimparAtributos();
                    objConTipoStatus.objCoTipoStatus.cdTpStatus = Convert.ToInt32(dr[caCantoresFases.cdTpStatus].ToString());
                    objConFases.objCoFases.LimparAtributos();
                    objConFases.objCoFases.cdFase = Convert.ToInt32(dr[caCantoresFases.cdFase].ToString());
                    objConCategorias.objCoCategorias.LimparAtributos();
                    objConCategorias.objCoCategorias.cdCategoria = Convert.ToInt32(dr[caCantoresFases.cdCategoria].ToString());

                    if (conTipoStatus.Select())
                    {
                        if (objConTipoStatus.dtDados.Rows.Count > 0)
                        {
                            dr[caCantoresFases.CC_deTpStatus] = objConTipoStatus.dtDados.Rows[0][caTipoStatus.deTpStatus].ToString();
                        }
                    }
                    if (conFases.Select())
                    {
                        if (objConFases.dtDados.Rows.Count > 0)
                        {
                            dr[caCantoresFases.CC_deFase] = objConFases.dtDados.Rows[0][caFases.deFase].ToString();
                        }
                    }
                    if (conCategorias.Select())
                    {
                        if (objConCategorias.dtDados.Rows.Count > 0)
                        {
                            dr[caCantoresFases.CC_deCategoria] = objConCategorias.dtDados.Rows[0][caCategorias.deCategoria].ToString();
                        }
                    }
                    if (conCantores.Select())
                    {
                        if (objConCantores.dtDados.Rows.Count > 0)
                        {
                            dr[caCantoresFases.CC_nmCantor] = objConCantores.dtDados.Rows[0][caCantores.nmCantor].ToString();
                            dr[caCantoresFases.CC_nmNomeKanji] = objConCantores.dtDados.Rows[0][caCantores.nmNomeKanji].ToString();
                        }
                    }
                    if (conMusicas.Select())
                    {
                        if (objConMusicas.dtDados.Rows.Count > 0)
                        {
                            dr[caCantoresFases.CC_nmMusica] = objConMusicas.dtDados.Rows[0][caMusicas.nmMusica].ToString();
                            dr[caCantoresFases.CC_nmMusicaKanji] = objConMusicas.dtDados.Rows[0][caMusicas.nmMusicaKanji].ToString();
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
        /// Retorna somete as categorias existentes no concurso
        /// </summary>
        /// <param name="dtDados"></param>
        /// <returns></returns>
        public bool SelectCategoriasConcurso(out DataTable dtDados)
        {
            string strComando = @"SELECT C.cdCategoria, C.deCategoria FROM CANTORESFASES CF " +
                                 " INNER JOIN CATEGORIAS C on C.cdCategoria = CF.cdCategoria " +
                                 " WHERE CF.cdConcurso = " + _cdConcurso +
                                 " GROUP BY C.cdCategoria, C.deCategoria";

            return objBanco.SelectPersonalizado(out dtDados, strComando);
        }

        /// <summary>
        /// Retorna somete as categorias existentes no concurso
        /// </summary>
        /// <param name="dtDados"></param>
        /// <returns></returns>
        public bool SelectCantoresCategoriasFasesConcurso(out DataTable dtDados)
        {
            string strComando = @"SELECT C.cdCategoria, C.deCategoria" +
                                 "  FROM CANTORESFASES CF " +
                                 " INNER JOIN CATEGORIAS C on C.cdCategoria = CF.cdCategoria" +
                                 " WHERE CF.cdConcurso = " + _cdConcurso +
                                 "   AND CF.cdFase = " + _cdFase +
                                 "   AND CF.cdCantor = " + _cdCantor +
                                 "   AND CF.nuCantor = '" + _nuCantor + "'" +
                                 " GROUP BY C.cdCategoria, C.deCategoria";

            return objBanco.SelectPersonalizado(out dtDados, strComando);
        }

        /// <summary>
        /// Retorna somete as categorias existentes no concurso
        /// </summary>
        /// <param name="dtDados"></param>
        /// <returns></returns>
        public bool SelectCantoresCategoriasConcurso(out DataTable dtDados)
        {
            string strComando = @"SELECT CF.cdConcurso, CF.nuOrdemApresentacao, CF.nuCantor, CAN.cdCantor, CAN.nmCantor, " +
                                 "       CAN.nmNomeKanji, MUS.cdMusica, MUS.nmMusica, MUS.nmMusicaKanji, " +
                                 "       ASS.cdAssociacao, ASS.nmAssociacao, CF.cdFase, " +
                                 "       CF.cdCategoria, CF.cdTpStatus " +
                                 "  FROM CANTORESFASES CF " +
                                 " INNER JOIN CANTORES CAN on CAN.cdCantor = CF.cdCantor " +
                                 " INNER JOIN MUSICAS MUS on MUS.cdMusica = CF.cdMusica " +
                                 " INNER JOIN CANTORESCONCURSOS CC on CC.cdConcurso = CF.cdConcurso AND CC.cdCantor = CF.cdCantor" +
                                 " INNER JOIN ASSOCIACOES ASS on ASS.cdAssociacao = CC.cdAssociacao" +
                                 " WHERE CF.cdConcurso = " + _cdConcurso +
                                 "   AND CF.cdCategoria = " + _cdCategoria;

            return objBanco.SelectPersonalizado(out dtDados, strComando);
        }

        /// <summary>
        /// Retorna somete as fases existentes no concurso
        /// </summary>
        /// <param name="dtDados"></param>
        /// <returns></returns>
        public bool SelectFasesConcurso(out DataTable dtDados)
        {
            string strComando = @"SELECT F.cdFase, F.deFase" +
                                 "  FROM CANTORESFASES CF " +
                                 " INNER JOIN FASES F ON F.CDFASE = CF.CDFASE" +
                                 " WHERE CF.cdConcurso = " + _cdConcurso;

            return objBanco.SelectPersonalizado(out dtDados, strComando);
        }
        
        /// <summary>
        /// Retorna somente o proximo cantor a cantar
        /// </summary>
        /// <param name="dtDados"></param>
        /// <returns></returns>
        public bool SelectProximoCantor(out DataTable dtDados)
        {
            string strComando = @"select cf.nuCantor, cf.cdCantor " +
                                 "  from cantoresfases cf " +
                                 " where cf.cdConcurso = " + _cdConcurso +
                                 "   and cf.cdTpStatus = " + _cdTpStatus +
                                 "   and rownum = 1 " +
                                 " order by cf.nuCanto ";

            return objBanco.SelectPersonalizado(out dtDados, strComando);
        }

        /// <summary>
        /// Retorna a estrtura para a tabela de Cantores Fases Concursos
        /// </summary>
        /// <returns></returns>
        public DataTable RetornaEstruturaDtCantoresFasesConcursos()
        { 
            DataTable dt = new DataTable();
            DataColumn dc;

            dc = new DataColumn();
            dc.DataType = typeof(int);
            dc.ColumnName = "cdConcurso";
            dc.ReadOnly = false;

            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = typeof(int);
            dc.ColumnName = "nuOrdemApresentacao";
            dc.ReadOnly = false;

            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = typeof(string);
            dc.ColumnName = "nuCantor";
            dc.ReadOnly = false;

            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = typeof(int);
            dc.ColumnName = "cdCantor";
            dc.ReadOnly = false;

            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = typeof(string);
            dc.ColumnName = "nmCantor";
            dc.ReadOnly = false;

            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = typeof(string);
            dc.ColumnName = "nmNomeKanji";
            dc.ReadOnly = false;

            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = typeof(int);
            dc.ColumnName = "cdMusica";
            dc.ReadOnly = false;

            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = typeof(string);
            dc.ColumnName = "nmMusica";
            dc.ReadOnly = false;

            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = typeof(string);
            dc.ColumnName = "nmMusicaKanji";
            dc.ReadOnly = false;

            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = typeof(int);
            dc.ColumnName = "cdAssociacao";
            dc.ReadOnly = false;

            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = typeof(string);
            dc.ColumnName = "nmAssociacao";
            dc.ReadOnly = false;

            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = typeof(int);
            dc.ColumnName = "cdFase";
            dc.ReadOnly = false;

            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = typeof(int);
            dc.ColumnName = "cdCategoria";
            dc.ReadOnly = false;

            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = typeof(int);
            dc.ColumnName = "cdTpStatus";
            dc.ReadOnly = false;

            dt.Columns.Add(dc);

            return dt;
        }

        /// <summary>
        /// Sobrescrito para retornar a chave
        /// </summary>
        /// <returns></returns>
        public override bool Inserir()
        {
            if (base.Inserir())
            {
                cdConcurso = objBanco.cdChave;
                return true;
            }

            return false;
        }
    }
}