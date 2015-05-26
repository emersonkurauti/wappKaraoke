using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using wappKaraoke.Classes.Model.Associacoes;
using wappKaraoke.Classes.Mensagem;

namespace wappKaraoke.Classes.Controller
{
    public class conAssociacoes : KuraFrameWork.ClasseBase.csControllerBase
    {
        private static coAssociacoes _objCo;
        public static coAssociacoes  objCo
        {
            get { return conAssociacoes._objCoAssociacoes; }
            set { conAssociacoes._objCoAssociacoes = value; }
        }

        private static coAssociacoes _objCoAssociacoes;
        public coAssociacoes objCoAssociacoes
        {
            get { return _objCoAssociacoes; }
            set { _objCoAssociacoes = value; }
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public conAssociacoes()
        {
            _objCoAssociacoes = new coAssociacoes();
            _objCo = _objCoAssociacoes;
        }

        /// <summary>
        /// Select
        /// </summary>
        /// <returns></returns>
        public static bool Select()
        {
            _strMensagemErro = "";

            if (!_objCoAssociacoes.Select(out _dtDados))
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

            if (!ValidaCampoObrigatorio())
                return false;

            if (!_objCoAssociacoes.Inserir())
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

            if (!ValidaCampoObrigatorio())
                return false;

            if (!_objCoAssociacoes.Alterar())
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

            if (!_objCoAssociacoes.Excluir())
            {
                _strMensagemErro = csMensagem.msgRemover;
                return false;
            }
            return true;
        }

        /// <summary>
        /// ValidaCampoObrigatório
        /// </summary>
        /// <returns></returns>
        protected static bool ValidaCampoObrigatorio()
        {
            if (_objCo.nmAssociacao.Trim().Equals(""))
            {
                _strMensagemErro = "Informe o nome da Associação.";
                return false;
            }

            if (_objCo.nmPresidente.Trim().Equals(""))
            {
                _strMensagemErro = "Informe o nome do Presidente.";
                return false;
            }

            if (_objCo.nmRepresentante.Trim().Equals(""))
            {
                _strMensagemErro = "Informe o nome do Representante.";
                return false;
            }

            return true;
        }
    }
}