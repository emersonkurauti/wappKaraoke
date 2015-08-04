using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using wappKaraoke.Classes.Model.Concursos;
using wappKaraoke.Classes.Mensagem;

namespace wappKaraoke.Classes.Controller
{
    public class conConcursos : KuraFrameWork.ClasseBase.csControllerBase
    {
        private static coConcursos _objCo;
        public static coConcursos  objCo
        {
            get { return conConcursos._objCoConcursos; }
            set { conConcursos._objCoConcursos = value; }
        }

        private static coConcursos _objCoConcursos;
        public coConcursos objCoConcursos
        {
            get { return _objCoConcursos; }
            set { _objCoConcursos = value; }
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public conConcursos()
        {
            _objCoConcursos = new coConcursos();
            _objCo = _objCoConcursos;
        }

        /// <summary>
        /// Select
        /// </summary>
        /// <returns></returns>
        public static bool Select()
        {
            _strMensagemErro = "";

            if (!_objCoConcursos.Select(out _dtDados))
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

            if (!_objCoConcursos.Inserir())
            {
            _strMensagemErro = "";

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

            if (!_objCoConcursos.Alterar())
            {
                _strMensagemErro = csMensagem.msgAlterar;
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

            if (!_objCoConcursos.Excluir())
            {
                _strMensagemErro = csMensagem.msgRemover;
                return false;
            }
            return true;
        }

        public static bool AlterarConcursoCorrente()
        {
            _strMensagemErro = "";

            if (!_objCoConcursos.AlterarConcursoCorrente())
            {
                _strMensagemErro = csMensagem.msgAlterar;
                return false;
            }
            return true;
        }

        public static bool AtualizarProximaFase()
        {
            _strMensagemErro = "";

            if (!_objCoConcursos.AtualizarProximaFase())
            {
                _strMensagemErro = csMensagem.msgAlterar;
                return false;
            }
            return true;
        }
    }
}