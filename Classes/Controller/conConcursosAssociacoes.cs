using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using wappKaraoke.Classes.Model.ConcursosAssociacoes;
using wappKaraoke.Classes.Mensagem;

namespace wappKaraoke.Classes.Controller
{
    public class conConcursosAssociacoes : KuraFrameWork.ClasseBase.csControllerBase
    {
        private static coConcursosAssociacoes _objCo;
        public static coConcursosAssociacoes  objCo
        {
            get { return conConcursosAssociacoes._objCoConcursosAssociacoes; }
            set { conConcursosAssociacoes._objCoConcursosAssociacoes = value; }
        }

        private static coConcursosAssociacoes _objCoConcursosAssociacoes;
        public coConcursosAssociacoes objCoConcursosAssociacoes
        {
            get { return _objCoConcursosAssociacoes; }
            set { _objCoConcursosAssociacoes = value; }
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public conConcursosAssociacoes()
        {
            _objCoConcursosAssociacoes = new coConcursosAssociacoes();
            _objCo = _objCoConcursosAssociacoes;
        }

        /// <summary>
        /// Select
        /// </summary>
        /// <returns></returns>
        public static bool Select()
        {
            _strMensagemErro = "";

            if (!_objCoConcursosAssociacoes.Select(out _dtDados))
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

            if (!_objCoConcursosAssociacoes.Inserir())
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

            if (!_objCoConcursosAssociacoes.Alterar())
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

            if (!_objCoConcursosAssociacoes.Excluir())
            {
                _strMensagemErro = csMensagem.msgRemover;
                return false;
            }
            return true;
        }
    }
}