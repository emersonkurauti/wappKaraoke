using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using wappKaraoke.Classes.Model.Jurados;
using wappKaraoke.Classes.Mensagem;

namespace wappKaraoke.Classes.Controller
{
    public class conJurados : KuraFrameWork.ClasseBase.csControllerBase
    {
        private static coJurados _objCo;
        public static coJurados  objCo
        {
            get { return conJurados._objCoJurados; }
            set { conJurados._objCoJurados = value; }
        }

        private static coJurados _objCoJurados;
        public coJurados objCoJurados
        {
            get { return _objCoJurados; }
            set { _objCoJurados = value; }
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public conJurados()
        {
            _objCoJurados = new coJurados();
            _objCo = _objCoJurados;
        }

        /// <summary>
        /// Select
        /// </summary>
        /// <returns></returns>
        public static bool Select()
        {
            _strMensagemErro = "";

            if (!_objCoJurados.Select(out _dtDados))
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

            if (!_objCoJurados.Inserir())
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

            if (!_objCoJurados.Alterar())
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

            if (!_objCoJurados.Excluir())
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
            if (_objCo.nmJurado.Trim().Equals(""))
            {
                _strMensagemErro = "Informe o nome do Jurado.";
                return false;
            }

            return true;
        }
    }
}