using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using wappKaraoke.Classes.Model.Arquivos;
using wappKaraoke.Classes.Mensagem;

namespace wappKaraoke.Classes.Controller
{
    public class conArquivos : KuraFrameWork.ClasseBase.csControllerBase
    {
        private static coArquivos _objCo;
        public static coArquivos  objCo
        {
            get { return conArquivos._objCoArquivos; }
            set { conArquivos._objCoArquivos = value; }
        }

        private static coArquivos _objCoArquivos;
        public coArquivos objCoArquivos
        {
            get { return _objCoArquivos; }
            set { _objCoArquivos = value; }
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public conArquivos()
        {
            _objCoArquivos = new coArquivos();
            _objCo = _objCoArquivos;
        }

        /// <summary>
        /// Select
        /// </summary>
        /// <returns></returns>
        public static bool Select()
        {
            _strMensagemErro = "";

            if (!_objCoArquivos.Select(out _dtDados))
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

            if (!_objCoArquivos.Inserir())
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

            if (!_objCoArquivos.Alterar())
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

            if (!_objCoArquivos.Excluir())
            {
                _strMensagemErro = csMensagem.msgRemover;
                return false;
            }
            return true;
        }
    }
}