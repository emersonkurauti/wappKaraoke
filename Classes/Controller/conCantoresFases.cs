using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using wappKaraoke.Classes.Model.CantoresFases;
using wappKaraoke.Classes.Mensagem;

namespace wappKaraoke.Classes.Controller
{
    public class conCantoresFases : KuraFrameWork.ClasseBase.csControllerBase
    {
        private static coCantoresFases _objCo;
        public static coCantoresFases  objCo
        {
            get { return conCantoresFases._objCoCantoresFases; }
            set { conCantoresFases._objCoCantoresFases = value; }
        }

        private static coCantoresFases _objCoCantoresFases;
        public coCantoresFases objCoCantoresFases
        {
            get { return _objCoCantoresFases; }
            set { _objCoCantoresFases = value; }
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public conCantoresFases()
        {
            _objCoCantoresFases = new coCantoresFases();
            _objCo = _objCoCantoresFases;
        }

        /// <summary>
        /// Select
        /// </summary>
        /// <returns></returns>
        public static bool Select()
        {
            _strMensagemErro = "";

            if (!_objCoCantoresFases.Select(out _dtDados))
            {
                _strMensagemErro = csMensagem.msgConsultar;
                return false;
            }

            return true;
        }

        public static bool SelectCantoresCategoriasFasesConcurso()
        {
            _strMensagemErro = "";

            if (!_objCoCantoresFases.SelectCantoresCategoriasFasesConcurso(out _dtDados))
            {
                _strMensagemErro = csMensagem.msgConsultar;
                return false;
            }

            return true;
        }

        public static bool SelectCategoriasConcurso()
        {
            _strMensagemErro = "";

            if (!_objCoCantoresFases.SelectCategoriasConcurso(out _dtDados))
            {
                _strMensagemErro = csMensagem.msgConsultar;
                return false;
            }

            return true;
        }

        public static bool SelectCantoresCategoriasConcurso()
        {
            _strMensagemErro = "";

            if (!_objCoCantoresFases.SelectCantoresCategoriasConcurso(out _dtDados))
            {
                _strMensagemErro = csMensagem.msgConsultar;
                return false;
            }

            return true;
        }

        public static bool SelectFasesConcurso()
        {
            _strMensagemErro = "";

            if (!_objCoCantoresFases.SelectFasesConcurso(out _dtDados))
            {
                _strMensagemErro = csMensagem.msgConsultar;
                return false;
            }

            return true;
        }

        public static bool SelectProximoCantor()
        {
            _strMensagemErro = "";

            if (!_objCoCantoresFases.SelectProximoCantor(out _dtDados))
            {
                _strMensagemErro = csMensagem.msgConsultar;
                return false;
            }

            return true;
        }

        public static bool SelectPainelAcompanhamentoConcurso()
        {
            _strMensagemErro = "";

            if (!_objCoCantoresFases.SelectPainelAcompanhamentoConcurso(out _dtDados))
            {
                _strMensagemErro = csMensagem.msgConsultar;
                return false;
            }

            return true;
        }

        public static bool SelectProximoCantorSemNota()
        {
            _strMensagemErro = "";

            if (!_objCoCantoresFases.SelectProximoCantorSemNota(out _dtDados))
            {
                _strMensagemErro = csMensagem.msgConsultar;
                return false;
            }

            return true;
        }

        public static bool SelectCantoresFasesCategoriasConcurso()
        {
            _strMensagemErro = "";

            if (!_objCoCantoresFases.SelectCantoresFasesCategoriasConcurso(out _dtDados))
            {
                _strMensagemErro = csMensagem.msgConsultar;
                return false;
            }

            return true;
        }

        public static bool SelectFasesCategoriasCantoresConcurso()
        {
            _strMensagemErro = "";

            if (!_objCoCantoresFases.SelectFasesCategoriasCantoresConcurso(out _dtDados))
            {
                _strMensagemErro = csMensagem.msgConsultar;
                return false;
            }

            return true;
        }

        public static bool SelectCantoresConcursoPorNumero()
        {
            _strMensagemErro = "";

            if (!_objCoCantoresFases.SelectCantoresConcursoPorNumero(out _dtDados))
            {
                _strMensagemErro = csMensagem.msgConsultar;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Inserir
        /// </summary>
        /// <returns></returns>
        public static bool Inserir()
        {
            _strMensagemErro = "";

            if (!_objCoCantoresFases.Inserir())
            {
                _strMensagemErro = csMensagem.msgInserir;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Alterar
        /// </summary>
        /// <returns></returns>
        public static bool Alterar()
        {
            _strMensagemErro = "";

            if (!_objCoCantoresFases.Alterar())
            {
                _strMensagemErro = csMensagem.msgAlterar;
                return false;
            }
            return true;
        }

        public static bool AlterarStatus()
        {
            _strMensagemErro = "";

            if (!_objCoCantoresFases.AlterarStatus())
            {
                _strMensagemErro = csMensagem.msgAlterar;
                return false;
            }
            return true;
        }

        public static bool AlterarNotaCantor()
        {
            _strMensagemErro = "";

            if (!_objCoCantoresFases.AlterarNotaCantor())
            {
                _strMensagemErro = csMensagem.msgAlterar;
                return false;
            }
            return true;
        }

        public static bool AlterarOrdemApresentacao()
        {
            _strMensagemErro = "";

            if (!_objCoCantoresFases.AlterarOrdemApresentacao())
            {
                _strMensagemErro = csMensagem.msgAlterar;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Movimenta cantores entre fases
        /// </summary>
        /// <returns></returns>
        public static bool MovimentarCantoresEntreFases()
        {
            _strMensagemErro = "";

            if (!_objCoCantoresFases.MovimentarCantoresEntreFases())
            {
                _strMensagemErro = csMensagem.msgAlterar;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Insere cantores no meio do concurso
        /// </summary>
        /// <returns></returns>
        public static bool InserirCantoresConcurso()
        {
            _strMensagemErro = "";

            if (!_objCoCantoresFases.InserirCantoresConcurso())
            {
                _strMensagemErro = csMensagem.msgInserir;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Excluir
        /// </summary>
        /// <returns></returns>
        public static bool Excluir()
        {
            _strMensagemErro = "";

            if (!_objCoCantoresFases.Excluir())
            {
                _strMensagemErro = csMensagem.msgRemover;
                return false;
            }
            return true;
        }
    }
}