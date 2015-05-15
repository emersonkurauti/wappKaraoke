using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using wappKaraoke.Classes.Model.TipoStatus;
using wappKaraoke.Mensagem;

namespace wappKaraoke.Classes.Controller
{
    public class conTipoStatus : KuraFrameWork.ClasseBase.csControllerBase
    {
        private static coTipoStatus _objCo;
        public static coTipoStatus  objCo
        {
            get { return conTipoStatus._objCoTipoStatus; }
            set { conTipoStatus._objCoTipoStatus = value; }
        }

        private static coTipoStatus _objCoTipoStatus;
        public coTipoStatus objCoTipoStatus
        {
            get { return _objCoTipoStatus; }
            set { _objCoTipoStatus = value; }
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public conTipoStatus()
        {
            _objCoTipoStatus = new coTipoStatus();
            _objCo = _objCoTipoStatus;
        }

        /// <summary>
        /// Select
        /// </summary>
        /// <returns></returns>
        public static bool Select()
        {
            _strMensagemErro = "";

            if (!_objCoTipoStatus.Select(out _dtDados))
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

            if (!_objCoTipoStatus.Inserir())
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

            if (!ValidaCampoObrigatorio())
                return false;

            if (!_objCoTipoStatus.Alterar())
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

            if (!_objCoTipoStatus.Excluir())
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
            if (_objCo.deCor.Contains("--"))
            {
                _strMensagemErro = "Selecione uma cor.";
                return false;
            }
            if (_objCo.deTpStatus.Trim().Equals(""))
            {
                _strMensagemErro = "Informe a descrição da cor.";
                return false;
            }

            return true;
        }
    }
}