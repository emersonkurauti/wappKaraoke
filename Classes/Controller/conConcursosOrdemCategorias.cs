using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using wappKaraoke.Classes.Model.ConcursosOrdemCategorias;
using wappKaraoke.Classes.Mensagem;

namespace wappKaraoke.Classes.Controller
{
    public class conConcursosOrdemCategorias : KuraFrameWork.ClasseBase.csControllerBase
    {
        private static coConcursosOrdemCategorias _objCo;
        public static coConcursosOrdemCategorias  objCo
        {
            get { return conConcursosOrdemCategorias._objCoConcursosOrdemCategorias; }
            set { conConcursosOrdemCategorias._objCoConcursosOrdemCategorias = value; }
        }

        private static coConcursosOrdemCategorias _objCoConcursosOrdemCategorias;
        public coConcursosOrdemCategorias objCoConcursosOrdemCategorias
        {
            get { return _objCoConcursosOrdemCategorias; }
            set { _objCoConcursosOrdemCategorias = value; }
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public conConcursosOrdemCategorias()
        {
            _objCoConcursosOrdemCategorias = new coConcursosOrdemCategorias();
            _objCo = _objCoConcursosOrdemCategorias;
        }

        /// <summary>
        /// Select
        /// </summary>
        /// <returns></returns>
        public static bool Select()
        {
            _strMensagemErro = "";

            if (!_objCoConcursosOrdemCategorias.Select(out _dtDados))
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

            if (!_objCoConcursosOrdemCategorias.Inserir())
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

            if (!_objCoConcursosOrdemCategorias.Alterar())
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

            if (!_objCoConcursosOrdemCategorias.Excluir())
            {
                _strMensagemErro = csMensagem.msgRemover;
                return false;
            }
            return true;
        }
    }
}