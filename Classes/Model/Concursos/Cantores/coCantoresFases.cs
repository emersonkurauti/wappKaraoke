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
using wappKaraoke.Classes.Model.Notas;

namespace wappKaraoke.Classes.Model.CantoresFases
{
    public class coCantoresFases : KuraFrameWork.ClasseBase.csModelBase
    {
        private DataTable _dtCantoresProxFase;
        public DataTable dtCantoresProxFase
        {
            get { return _dtCantoresProxFase; }
            set { _dtCantoresProxFase = value; }
        }

        private DataTable _dtNotas;
        public DataTable dtNotas
        {
            get { return _dtNotas; }
            set { _dtNotas = value; }
        }

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

        private static string _flFaseCorrente;
        public string flFaseCorrente
        {
            get { return _flFaseCorrente; }
            set { _flFaseCorrente = value; }
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

        private static string _CC_deFormulaPontuacao = "";
        public string CC_deFormulaPontuacao
        {
            get { return _CC_deFormulaPontuacao; }
            set { _CC_deFormulaPontuacao = value; }
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
                dtDados.Columns[caCantoresFases.CC_deFormulaPontuacao].ReadOnly = false;
                dtDados.Columns[caCantoresFases.CC_deFormulaPontuacao].MaxLength = 100;
                

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
                            dr[caCantoresFases.CC_deFormulaPontuacao] = objConCategorias.dtDados.Rows[0][caCategorias.deFormulaPontuacao].ToString();
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
                                 "   AND CF.cdFase = " + _cdFase +
                                 "   AND CF.cdCategoria = " + _cdCategoria;

            return objBanco.SelectPersonalizado(out dtDados, strComando);
        }

        /// <summary>
        /// Retorna os cantores da fase e categoria do concurso
        /// </summary>
        /// <param name="dtDados"></param>
        /// <returns></returns>
        public bool SelectCantoresFasesCategoriasConcurso(out DataTable dtDados)
        {
            string strComando = @"SELECT CF.cdMusica, CANT.cdCantor, CANT.nmCantor, CF.nuNotaFinal, CF.pcDesconto" +
                                 "  FROM CANTORESFASES CF " +
                                 " INNER JOIN CANTORES CANT on CANT.cdCantor = CF.cdCantor" +
                                 " WHERE CF.cdConcurso = " + _cdConcurso +
                                 "   AND CF.cdFase = " + _cdFase +
                                 "   AND CF.cdCategoria = " + _cdCategoria +
                                 "   AND CF.flFaseCorrente = 'S'" +
                                 " ORDER BY CF.nuNotaFinal DESC";

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
            string strComando = @"select * from " +
                                 "(select cf.nuCantor, cf.cdCantor, cf.cdFase, cf.cdCategoria, cant.nmCantor as CC_nmCantor, " +
                                 "        cat.deCategoria as CC_deCategoria " +
                                 "   from cantoresfases cf " +
                                 "  inner join concursosordemcategorias coc on coc.cdCategoria = cf.cdCategoria " +
                                 "  inner join cantores cant on cant.cdCantor = cf.cdCantor " +
                                 "  inner join categorias cat on cat.cdCategoria = coc.cdCategoria " +
                                 "  where cf.cdConcurso = " + _cdConcurso +
                                 "    and cf.cdTpStatus = " + _cdTpStatus +
                                 "    and cf.cdFase = " + _cdFase +
                                 "  order by coc.nuOrdem, cf.nuOrdemApresentacao) " +
                                 " where rownum = 1 ";

            return objBanco.SelectPersonalizado(out dtDados, strComando);
        }

        /// <summary>
        /// Retorna o primeiro cantor que cantou e que não possui nota
        /// </summary>
        /// <param name="dtDados"></param>
        /// <returns></returns>
        public bool SelectProximoCantorSemNota(out DataTable dtDados)
        {
            string strComando = @"select * from " +
                                 "(Select cf.nuCantor, cf.cdCantor, can.nmCantor, can.nmNomeKanji, " +
                                 "        cat.cdCategoria, cat.deCategoria, cat.deFormulaPontuacao, f.cdFase, f.deFase " +
                                 "   from cantoresfases cf " +
                                 "  inner join concursosordemcategorias coc on coc.cdConcurso = cf.cdConcurso and coc.cdCategoria = cf.cdCategoria " +
                                 "  inner join cantores can on can.cdCantor = cf.cdCantor " +
                                 "  inner join categorias cat on cat.cdCategoria = cf.cdCategoria " +
                                 "  inner join fases f on f.cdFase = cf.cdFase " +
                                 "  where cf.nuNotaFinal = 0 " +
                                 "    and cf.cdTpStatus = " + _cdTpStatus +
                                 "    and cf.cdConcurso = " + _cdConcurso +
                                 "    and cf.cdFase = " + _cdFase +
                                 "  order by coc.nuOrdem, cf.nuOrdemApresentacao) " +
                                 " where rownum = 1 ";

            return objBanco.SelectPersonalizado(out dtDados, strComando);
        }

        /// <summary>
        /// Retorna o acompanhamento do concurso
        /// </summary>
        /// <param name="dtDados"></param>
        /// <returns></returns>
        public bool SelectPainelAcompanhamentoConcurso(out DataTable dtDados)
        {
            string strComando = @"select cat.deCategoria, nuCantor, ass.nmAssociacao, ca.nmCantor, ca.nmNomeKanji, m.nmMusica, m.nmMusicaKanji, " +
                                 "       ts.deTpStatus, ts.deCor " +
                                 "  from concursosordemcategorias coc  " +
                                 " inner join cantoresfases cf on cf.cdConcurso = coc.cdConcurso and cf.cdCategoria = coc.cdCategoria " +
                                 " inner join cantoresconcursos cc on cc.cdConcurso = cf.cdConcurso and cc.cdCantor = cf.cdCantor " +
                                 " inner join associacoes ass on ass.cdAssociacao = cc.cdAssociacao " +
                                 " inner join cantores ca on ca.cdCantor = cf.cdCantor " +
                                 " inner join musicas m on m.cdMusica = cf.cdMusica " +
                                 " inner join categorias cat on cat.cdCategoria = cf.cdCategoria " +
                                 " inner join tipostatus ts on ts.cdTpStatus = cf.cdTpStatus " +
                                 " where coc.cdConcurso = " + _cdConcurso +
                                 " order by coc.nuOrdem, cf.nuOrdemApresentacao, cf.nuCantor";

            return objBanco.SelectPersonalizado(out dtDados, strComando);
        }

        /// <summary>
        /// Retorna somete as fases existentes no concurso
        /// </summary>
        /// <param name="dtDados"></param>
        /// <returns></returns>
        public bool SelectFasesCategoriasCantoresConcurso(out DataTable dtDados)
        {
            string strComando = @"SELECT 1" +
                                 "  FROM CANTORESFASES CF " +
                                 " WHERE CF.cdConcurso = " + _cdConcurso +
                                 "   AND CF.cdCategoria = " + _cdCategoria +
                                 "   AND CF.cdCantor = " + _cdCantor +
                                 "   AND CF.cdFase = " + _cdFase;

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
        /// Atualiza as notas do cantor
        /// </summary>
        /// <returns></returns>
        public bool AtualizarNotaCantor()
        {
            if (_dtNotas == null || _dtNotas.Rows.Count == 0)
                return false;

            try
            {
                conNotas objConNotas = new conNotas();

                foreach (DataRow dr in _dtNotas.Rows)
                {
                    objConNotas.objCoNotas.LimparAtributos();
                    objConNotas.objCoNotas.cdConcurso = Convert.ToInt32(dr[caNotas.cdConcurso].ToString());
                    objConNotas.objCoNotas.cdCantor = Convert.ToInt32(dr[caNotas.cdCantor].ToString());
                    objConNotas.objCoNotas.cdCategoria = Convert.ToInt32(dr[caNotas.cdCategoria].ToString());
                    objConNotas.objCoNotas.cdFase = Convert.ToInt32(dr[caNotas.cdFase].ToString());
                    objConNotas.objCoNotas.cdJurado = Convert.ToInt32(dr[caNotas.cdJurado].ToString());
                    objConNotas.objCoNotas.nuNota = Convert.ToInt32(dr[caNotas.nuNota].ToString());
                    objConNotas.objCoNotas.deObservacao = dr[caNotas.deObservacao].ToString();

                    if (!conNotas.Excluir())
                        return false;

                    if (!conNotas.Inserir())
                        return false;
                }

                return true;
            }
            catch 
            {
                return false;
            }
        }

        /// <summary>
        /// Alterar o status do cantor
        /// </summary>
        /// <returns></returns>
        public bool AlterarStatus()
        {
            try
            {
                string strComando = @"UPDATE " + caCantoresFases.nmTabela + " SET " + caCantoresFases.cdTpStatus + "=" + _cdTpStatus +
                    " WHERE " + caCantoresFases.cdCantor + "=" + _cdCantor +
                    "  AND " + caCantoresFases.cdConcurso + "=" + _cdConcurso +
                    "  AND " + caCantoresFases.cdCategoria + "=" + _cdCategoria +
                    "  AND " + caCantoresFases.cdFase + "=" + _cdFase;

                objBanco.ExecutarSQLPersonalizado(strComando);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Alterar a nota final do cantor
        /// </summary>
        /// <returns></returns>
        public bool AlterarNotaCantor()
        {
            try
            {
                string strComando = @"UPDATE " + caCantoresFases.nmTabela +
                    " SET " + caCantoresFases.nuNotafinal + "=" + _nuNotafinal.ToString().Replace(',', '.') + ", " +
                    caCantoresFases.pcDesconto + "=" + _pcDesconto.ToString().Replace(',', '.') +
                    " WHERE " + caCantoresFases.cdCantor + "=" + _cdCantor +
                    "  AND " + caCantoresFases.cdConcurso + "=" + _cdConcurso +
                    "  AND " + caCantoresFases.cdCategoria + "=" + _cdCategoria +
                    "  AND " + caCantoresFases.cdFase + "=" + _cdFase;

                objBanco.BeginTransaction();

                if (!objBanco.ExecutarSQLPersonalizado(strComando, false))
                {
                    objBanco.RollbackTransaction();
                    return false;
                }

                if (!AtualizarNotaCantor())
                {
                    objBanco.RollbackTransaction();
                    return false;
                }

                objBanco.CommitTransaction();
                return true;
            }
            catch
            {
                objBanco.RollbackTransaction();
                return false;
            }
        }

        /// <summary>
        /// Altera a fase corrente do cantor
        /// </summary>
        /// <returns></returns>
        public bool AlterarFaseCorrente(bool bControlaTransacao = true)
        {
            try
            {
                string strComando = @"UPDATE " + caCantoresFases.nmTabela +
                    " SET " + caCantoresFases.flFaseCorrente + "= '" + _flFaseCorrente + "'" +
                    " WHERE " + caCantoresFases.cdCantor + "=" + _cdCantor +
                    "  AND " + caCantoresFases.cdConcurso + "=" + _cdConcurso +
                    "  AND " + caCantoresFases.cdCategoria + "=" + _cdCategoria;

                objBanco.ExecutarSQLPersonalizado(strComando, bControlaTransacao);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Movimenta os cantores entre as fases
        /// </summary>
        /// <returns></returns>
        public bool MovimentarCantoresEntreFases()
        {
            if (_dtCantoresProxFase == null)
                return false;

            DataTable dtCantorFase;

            try
            {
                objBanco.BeginTransaction();

                foreach (DataRow dr in _dtCantoresProxFase.Rows)
                {
                    bool bControlaTransacao = false;
                    _cdCantor = Convert.ToInt32(dr[caCantoresFases.cdCantor].ToString());
                    _cdMusica = Convert.ToInt32(dr[caCantoresFases.cdMusica].ToString());

                    _flFaseCorrente = "N";

                    if (!AlterarFaseCorrente(bControlaTransacao))
                    {
                        objBanco.RollbackTransaction();
                        return false;
                    }


                    SelectFasesCategoriasCantoresConcurso(out dtCantorFase);

                    if (dtCantorFase.Rows.Count == 1)
                    {
                        _flFaseCorrente = "S";

                        if (!AlterarFaseCorrente(bControlaTransacao))
                        {
                            objBanco.RollbackTransaction();
                            return false;
                        }
                    }
                    else
                    {
                        _flFaseCorrente = "S";

                        if (!Inserir())
                        {
                            objBanco.RollbackTransaction();
                            return false;
                        }
                    }
                }

                objBanco.CommitTransaction();
                return true;
            }
            catch
            {
                objBanco.RollbackTransaction();
                return false;
            }
        }
    }
}