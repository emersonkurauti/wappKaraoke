using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using wappKaraoke.Classes.Model.Musicas;
using wappKaraoke.Classes.Mensagem;

namespace wappKaraoke.Classes.Controller
{
    public class conMusicas : KuraFrameWork.ClasseBase.csControllerBase
    {
        private static coMusicas _objCo;
        public static coMusicas  objCo
        {
            get { return conMusicas._objCoMusicas; }
            set { conMusicas._objCoMusicas = value; }
        }

        private static coMusicas _objCoMusicas;
        public coMusicas objCoMusicas
        {
            get { return _objCoMusicas; }
            set { _objCoMusicas = value; }
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public conMusicas()
        {
            _objCoMusicas = new coMusicas();
            _objCo = _objCoMusicas;
        }

        /// <summary>
        /// Select
        /// </summary>
        /// <returns></returns>
        public static bool Select()
        {
            _strMensagemErro = "";

            if (!_objCoMusicas.Select(out _dtDados))
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

            if (!_objCoMusicas.Inserir())
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

            if (!_objCoMusicas.Alterar())
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

            if (!_objCoMusicas.Excluir())
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
            if (_objCo.nmMusica.Trim().Equals(""))
            {
                _strMensagemErro = "Informe o nome da Música.";
                return false;
            }
            
            return true;
        }
    }
}