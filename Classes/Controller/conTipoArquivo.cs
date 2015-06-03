using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using wappKaraoke.Classes.Model.TipoArquivo;
using wappKaraoke.Classes.Mensagem;

namespace wappKaraoke.Classes.Controller
{
    public class conTipoArquivo : KuraFrameWork.ClasseBase.csControllerBase
    {
        private static coTipoArquivo _objCo;
        public static coTipoArquivo  objCo
        {
            get { return conTipoArquivo._objCoTipoArquivo; }
            set { conTipoArquivo._objCoTipoArquivo = value; }
        }

        private static coTipoArquivo _objCoTipoArquivo;
        public coTipoArquivo objCoTipoArquivo
        {
            get { return _objCoTipoArquivo; }
            set { _objCoTipoArquivo = value; }
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public conTipoArquivo()
        {
            _objCoTipoArquivo = new coTipoArquivo();
            _objCo = _objCoTipoArquivo;
        }

        /// <summary>
        /// Select
        /// </summary>
        /// <returns></returns>
        public static bool Select()
        {
            _strMensagemErro = "";

            if (!_objCoTipoArquivo.Select(out _dtDados))
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

            if (!_objCoTipoArquivo.Inserir())
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

            if (!_objCoTipoArquivo.Alterar())
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

            if (!_objCoTipoArquivo.Excluir())
            {
                _strMensagemErro = csMensagem.msgRemover;
                return false;
            }
            return true;
        }
    }
}