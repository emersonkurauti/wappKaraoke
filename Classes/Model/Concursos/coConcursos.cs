using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using wappKaraoke.Classes.Controller;
using wappKaraoke.Classes.Model.Cidades;
using wappKaraoke.Classes.Model.Arquivos;
using wappKaraoke.Classes.Model.ConcursosAssociacoes;
using wappKaraoke.Classes.Model.Grupos;
using wappKaraoke.Classes.Model.CantoresConcursos;
using wappKaraoke.Classes.Model.CantoresFases;
using System.IO;

namespace wappKaraoke.Classes.Model.Concursos
{
    public class coConcursos : KuraFrameWork.ClasseBase.csModelBase
    {
        private string strPastaRaiz = System.Web.Hosting.HostingEnvironment.MapPath("/");

        private DataTable _dtArquivos;
        public DataTable dtArquivos
        {
            get { return _dtArquivos; }
            set { _dtArquivos = value; }
        }

        private DataTable _dtAssociacoes;
        public DataTable dtAssociacoes
        {
            get { return _dtAssociacoes; }
            set { _dtAssociacoes = value; }
        }

        private DataTable _dtGrupoJurados;
        public DataTable dtGrupoJurados
        {
            get { return _dtGrupoJurados; }
            set { _dtGrupoJurados = value; }
        }

        private DataTable _dtConcursoCantores;
        public DataTable dtConcursoCantores
        {
            get { return _dtConcursoCantores; }
            set { _dtConcursoCantores = value; }
        }

        private DataTable _dtConcursoFases;
        public DataTable dtConcursoFases
        {
            get { return _dtConcursoFases; }
            set { _dtConcursoFases = value; }
        }

        private static  int _CC_cdRegistro;
        public static int CC_cdRegistro
        {
            get { return coConcursos._cdConcurso; }
            set { coConcursos._cdConcurso = value; }
        }

		/// <summary>
		/// Atributos
		/// </summary>        
		private static int _cdCidade;
        public int cdCidade
        {
            get { return _cdCidade; }
            set { _cdCidade = value; }
        }

		private static DateTime _dtIniConcurso;
        public DateTime dtIniConcurso
        {
            get { return _dtIniConcurso; }
            set { _dtIniConcurso = value; }
        }

		private static string _flFinalizado = "";
        public string flFinalizado
        {
            get { return _flFinalizado; }
            set { _flFinalizado = value; }
        }

        private static DateTime _dtFimConcurso;
        public DateTime dtFimConcurso
        {
            get { return _dtFimConcurso; }
            set { _dtFimConcurso = value; }
        }

		private static int _cdConcurso;
        public int cdConcurso
        {
            get { return _cdConcurso; }
            set { _cdConcurso = value; }
        }

		private static string _nmConcursoKanji = "";
        public string nmConcursoKanji
        {
            get { return _nmConcursoKanji; }
            set { _nmConcursoKanji = value; }
        }

		private static string _nmConcurso = "";
        public string nmConcurso
        {
            get { return _nmConcurso; }
            set { _nmConcurso = value; }
        }

		private static string _CC_nmCidade = "";
        public string CC_nmCidade
        {
            get { return _CC_nmCidade; }
            set { _CC_nmCidade = value; }
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public coConcursos()
        {
            AtualizaObj();
            LimparAtributos();
            _CC_cdRegistro = cdConcurso;
            tobjCA = typeof(caConcursos);
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
                conCidades objConCidades = new conCidades();

                DataTable dtAux = dtDados;

                dtDados.Columns[caConcursos.CC_nmCidade].ReadOnly = false;
                dtDados.Columns[caConcursos.CC_nmCidade].MaxLength = 100;

                foreach (DataRow dr in dtAux.Rows)
                {
                    objConCidades.objCoCidades.LimparAtributos();
                    objConCidades.objCoCidades.cdCidade = Convert.ToInt32(dr[caConcursos.cdCidade].ToString());


                    if (conCidades.Select())
                    {
                        if (objConCidades.dtDados.Rows.Count > 0)
                        {
                            dr[caConcursos.CC_nmCidade] = objConCidades.dtDados.Rows[0][caCidades.nmCidade].ToString();

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
        /// Sobrescrito para retornar a chave
        /// </summary>
        /// <returns></returns>
        public override bool Inserir()
        {
            objBanco.BeginTransaction();

            try
            {
                if (base.Inserir())
                {
                    cdConcurso = objBanco.cdChave;

                    if (!AtualizarArquivos())
                    {
                        objBanco.RollbackTransaction();
                        return false;
                    }

                    if (!AtualizarAssociacoes())
                    {
                        objBanco.RollbackTransaction();
                        return false;
                    }

                    if (!AtualizarGrupoJurado())
                    {
                        objBanco.RollbackTransaction();
                        return false;
                    }

                    if (!AtualizarCantoresConcurso())
                    {
                        objBanco.RollbackTransaction();
                        return false;
                    }

                    if (!AtualizarCantoresFases())
                    {
                        objBanco.RollbackTransaction();
                        return false;
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

        /// <summary>
        /// Sobrescrito para Aleterar as tabelas vinculadas
        /// </summary>
        /// <returns></returns>
        public override bool Alterar()
        {
            objBanco.BeginTransaction();

            try
            {
                if (base.Alterar())
                {
                    if (!AtualizarArquivos())
                    {
                        objBanco.RollbackTransaction();
                        return false;
                    }

                    if (!AtualizarAssociacoes())
                    {
                        objBanco.RollbackTransaction();
                        return false;
                    }

                    if (!AtualizarGrupoJurado())
                    {
                        objBanco.RollbackTransaction();
                        return false;
                    }

                    if (!AtualizarCantoresConcurso())
                    {
                        objBanco.RollbackTransaction();
                        return false;
                    }

                    if (!AtualizarCantoresFases())
                    {
                        objBanco.RollbackTransaction();
                        return false;
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

        /// <summary>
        /// Move arquivo entre pastas
        /// </summary>
        /// <param name="strOrigem"></param>
        /// <param name="strDestino"></param>
        /// <returns></returns>
        private bool MoveArquivo(string strOrigem, string strDestino)
        {
            try
            {
                string strCaminhoOrigem = strPastaRaiz + strOrigem;
                string strCaminhoDestino = strPastaRaiz + strDestino;

                File.Copy(strCaminhoOrigem, strCaminhoDestino, true);
                File.Delete(strCaminhoOrigem);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Remove o arquivo
        /// </summary>
        /// <param name="strArquivo"></param>
        /// <returns></returns>
        private bool RemoveArquivo(string strArquivo)
        {
            try
            {
                File.Delete(strPastaRaiz + strArquivo);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Atualiza os Arquivos 
        /// </summary>
        /// <returns></returns>
        private bool AtualizarArquivos()
        {
            string strArquivoTemp = "";

            conArquivos objConArquivos = new conArquivos();
            try
            {
                foreach (DataRow dr in _dtArquivos.Rows)
                {
                    if (dr[caArquivos.CC_Controle].ToString() != KuraFrameWork.csConstantes.sTpCarregado)
                    {
                        objConArquivos.objCoArquivos.LimparAtributos();
                        objConArquivos.objCoArquivos.nmArquivo = dr[caArquivos.nmArquivo].ToString();
                        objConArquivos.objCoArquivos.deArquivo = dr[caArquivos.deArquivo].ToString();
                        objConArquivos.objCoArquivos.cdConcurso = Convert.ToInt32(dr[caArquivos.cdConcurso].ToString());
                        objConArquivos.objCoArquivos.cdTipoArquivo = Convert.ToInt32(dr[caArquivos.cdTipoArquivo].ToString());

                        if (dr[caArquivos.CC_Controle].ToString() != KuraFrameWork.csConstantes.sTpInserido)
                        {
                            objConArquivos.objCoArquivos.cdArquivo = Convert.ToInt32(dr[caArquivos.cdArquivo].ToString());

                            if (dr[caArquivos.CC_Controle].ToString() == KuraFrameWork.csConstantes.sTpAlterado)
                            {
                                if (!conArquivos.Alterar())
                                {
                                    return false;
                                }
                            }
                            else if (dr[caArquivos.CC_Controle].ToString() == KuraFrameWork.csConstantes.sTpExcluido)
                            {
                                if (objConArquivos.objCoArquivos.cdArquivo != 0)
                                {
                                    if (!conArquivos.Excluir())
                                    {
                                        return false;
                                    }

                                    objConArquivos.objCoArquivos.LimparAtributos();
                                    objConArquivos.objCoArquivos.nmArquivo = dr[caArquivos.nmArquivo].ToString();
                                    objConArquivos.objCoArquivos.deArquivo = dr[caArquivos.deArquivo].ToString();
                                    objConArquivos.objCoArquivos.cdConcurso = Convert.ToInt32(dr[caArquivos.cdConcurso].ToString());
                                    objConArquivos.objCoArquivos.cdTipoArquivo = Convert.ToInt32(dr[caArquivos.cdTipoArquivo].ToString());
                                    objConArquivos.objCoArquivos.cdArquivo = Convert.ToInt32(dr[caArquivos.cdArquivo].ToString());

                                    if (objConArquivos.objCoArquivos.cdTipoArquivo == csConstantes.cCdTipoArquivoDocumento)
                                    {
                                        RemoveArquivo(wappKaraoke.Properties.Settings.Default.sCaminhoArqDocumentos +
                                            objConArquivos.objCoArquivos.nmArquivo);
                                    }
                                    else if (objConArquivos.objCoArquivos.cdTipoArquivo == csConstantes.cCdTipoArquivoImagem)
                                    {
                                        RemoveArquivo(wappKaraoke.Properties.Settings.Default.sCaminhoArqImagens +
                                                                                   objConArquivos.objCoArquivos.nmArquivo);
                                    }
                                }
                                else
                                {
                                    RemoveArquivo(wappKaraoke.Properties.Settings.Default.sCaminhoTemp +
                                            objConArquivos.objCoArquivos.nmArquivo);
                                }
                            }
                        }
                        else
                        {
                            if (!conArquivos.Inserir())
                            {
                                return false;
                            }

                            objConArquivos.objCoArquivos.LimparAtributos();
                            objConArquivos.objCoArquivos.nmArquivo = dr[caArquivos.nmArquivo].ToString();
                            objConArquivos.objCoArquivos.deArquivo = dr[caArquivos.deArquivo].ToString();
                            objConArquivos.objCoArquivos.cdConcurso = Convert.ToInt32(dr[caArquivos.cdConcurso].ToString());
                            objConArquivos.objCoArquivos.cdTipoArquivo = Convert.ToInt32(dr[caArquivos.cdTipoArquivo].ToString());

                            strArquivoTemp = wappKaraoke.Properties.Settings.Default.sCaminhoTemp +
                                    objConArquivos.objCoArquivos.nmArquivo;

                            if (objConArquivos.objCoArquivos.cdTipoArquivo == csConstantes.cCdTipoArquivoDocumento)
                            {
                                MoveArquivo(strArquivoTemp, wappKaraoke.Properties.Settings.Default.sCaminhoArqDocumentos +
                                    objConArquivos.objCoArquivos.nmArquivo);
                            }
                            else if (objConArquivos.objCoArquivos.cdTipoArquivo == csConstantes.cCdTipoArquivoImagem)
                            {
                                MoveArquivo(strArquivoTemp, wappKaraoke.Properties.Settings.Default.sCaminhoArqImagens +
                                    objConArquivos.objCoArquivos.nmArquivo);
                            }
                        }
                    }

                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Atualiza as Associações
        /// </summary>
        /// <returns></returns>
        private bool AtualizarAssociacoes()
        {
            conConcursosAssociacoes objConConcursosAssociacoes = new conConcursosAssociacoes();
            try
            {
                foreach (DataRow dr in _dtAssociacoes.Rows)
                {
                    if (dr[caConcursosAssociacoes.CC_Controle].ToString() != KuraFrameWork.csConstantes.sTpCarregado)
                    {
                        objConConcursosAssociacoes.objCoConcursosAssociacoes.LimparAtributos();
                        objConConcursosAssociacoes.objCoConcursosAssociacoes.cdConcurso = Convert.ToInt32(dr[caConcursosAssociacoes.cdConcurso].ToString());
                        objConConcursosAssociacoes.objCoConcursosAssociacoes.cdAssociacao = Convert.ToInt32(dr[caConcursosAssociacoes.cdAssociacao].ToString());
                        objConConcursosAssociacoes.objCoConcursosAssociacoes.deEmail = dr[caConcursosAssociacoes.deEmail].ToString();
                        objConConcursosAssociacoes.objCoConcursosAssociacoes.nmRepresentante = dr[caConcursosAssociacoes.nmRepresentante].ToString();

                        if (dr[caConcursosAssociacoes.CC_Controle].ToString() != KuraFrameWork.csConstantes.sTpInserido)
                        {
                            if (dr[caConcursosAssociacoes.CC_Controle].ToString() == KuraFrameWork.csConstantes.sTpAlterado)
                            {
                                if (!conConcursosAssociacoes.Alterar())
                                {
                                    return false;
                                }
                            }
                            else if (dr[caConcursosAssociacoes.CC_Controle].ToString() == KuraFrameWork.csConstantes.sTpExcluido)
                            {
                                if (!conConcursosAssociacoes.Excluir())
                                {
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            if (!conConcursosAssociacoes.Inserir())
                            {
                                return false;
                            }
                        }
                    }

                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Atualiza os Grupos Jurados
        /// </summary>
        /// <returns></returns>
        private bool AtualizarGrupoJurado()
        {
            conGrupos objConconGrupos = new conGrupos();
            try
            {
                foreach (DataRow dr in _dtGrupoJurados.Rows)
                {
                    if (dr[caGrupos.CC_Controle].ToString() != KuraFrameWork.csConstantes.sTpCarregado)
                    {
                        objConconGrupos.objCoGrupos.LimparAtributos();
                        objConconGrupos.objCoGrupos.cdConcurso = Convert.ToInt32(dr[caGrupos.cdConcurso].ToString());
                        objConconGrupos.objCoGrupos.cdJurado = Convert.ToInt32(dr[caGrupos.cdJurado].ToString());
                        objConconGrupos.objCoGrupos.deGrupo = dr[caGrupos.deGrupo].ToString();

                        if (dr[caGrupos.CC_Controle].ToString() != KuraFrameWork.csConstantes.sTpInserido)
                        {
                            if (dr[caGrupos.CC_Controle].ToString() == KuraFrameWork.csConstantes.sTpAlterado)
                            {
                                if (!conGrupos.Alterar())
                                {
                                    return false;
                                }
                            }
                            else if (dr[caGrupos.CC_Controle].ToString() == KuraFrameWork.csConstantes.sTpExcluido)
                            {
                                if (!conGrupos.Excluir())
                                {
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            if (!conGrupos.Inserir())
                            {
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Atualiza os cantores concursos
        /// </summary>
        /// <returns></returns>
        private bool AtualizarCantoresConcurso()
        {
            conCantoresConcursos objConCantoresConcursos = new conCantoresConcursos();
            try
            {
                foreach (DataRow dr in _dtConcursoCantores.Rows)
                {
                    if (dr[caCantoresConcursos.CC_Controle].ToString() != KuraFrameWork.csConstantes.sTpCarregado)
                    {
                        objConCantoresConcursos.objCoCantoresConcursos.LimparAtributos();
                        objConCantoresConcursos.objCoCantoresConcursos.cdConcurso = Convert.ToInt32(dr[caCantoresConcursos.cdConcurso].ToString());
                        objConCantoresConcursos.objCoCantoresConcursos.cdCantor = Convert.ToInt32(dr[caCantoresConcursos.cdCantor].ToString());
                        objConCantoresConcursos.objCoCantoresConcursos.cdAssociacao = Convert.ToInt32(dr[caCantoresConcursos.cdAssociacao].ToString());

                        if (dr[caCantoresConcursos.CC_Controle].ToString() != KuraFrameWork.csConstantes.sTpInserido)
                        {
                            if (dr[caCantoresConcursos.CC_Controle].ToString() == KuraFrameWork.csConstantes.sTpAlterado)
                            {
                                if (!conCantoresConcursos.Alterar())
                                {
                                    return false;
                                }
                            }
                            else if (dr[caCantoresConcursos.CC_Controle].ToString() == KuraFrameWork.csConstantes.sTpExcluido)
                            {
                                if (!conCantoresConcursos.Excluir())
                                {
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            if (!conCantoresConcursos.Inserir())
                            {
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Atualiza os cantores fases
        /// </summary>
        /// <returns></returns>
        private bool AtualizarCantoresFases()
        {
            conCantoresFases objConCantoresFases = new conCantoresFases();
            try
            {
                foreach (DataRow dr in _dtConcursoFases.Rows)
                {
                    if (dr[caCantoresFases.CC_Controle].ToString() != KuraFrameWork.csConstantes.sTpCarregado)
                    {
                        objConCantoresFases.objCoCantoresFases.LimparAtributos();
                        objConCantoresFases.objCoCantoresFases.cdConcurso = Convert.ToInt32(dr[caCantoresFases.cdConcurso].ToString());
                        objConCantoresFases.objCoCantoresFases.cdCantor = Convert.ToInt32(dr[caCantoresFases.cdCantor].ToString());
                        objConCantoresFases.objCoCantoresFases.cdMusica = Convert.ToInt32(dr[caCantoresFases.cdMusica].ToString());
                        objConCantoresFases.objCoCantoresFases.cdCategoria = Convert.ToInt32(dr[caCantoresFases.cdCategoria].ToString());
                        objConCantoresFases.objCoCantoresFases.cdTpStatus = Convert.ToInt32(dr[caCantoresFases.cdTpStatus].ToString());
                        objConCantoresFases.objCoCantoresFases.cdFase = Convert.ToInt32(dr[caCantoresFases.cdFase].ToString());

                        if (dr[caCantoresFases.CC_Controle].ToString() != KuraFrameWork.csConstantes.sTpInserido)
                        {
                            if (dr[caCantoresFases.CC_Controle].ToString() == KuraFrameWork.csConstantes.sTpAlterado)
                            {
                                if (!conCantoresFases.Alterar())
                                {
                                    return false;
                                }
                            }
                            else if (dr[caCantoresFases.CC_Controle].ToString() == KuraFrameWork.csConstantes.sTpExcluido)
                            {
                                if (!conCantoresFases.Excluir())
                                {
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            if (!conCantoresFases.Inserir())
                            {
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}