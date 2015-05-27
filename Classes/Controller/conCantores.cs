using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using wappKaraoke.Classes.Model.Cantores;
using wappKaraoke.Classes.Mensagem;

namespace wappKaraoke.Classes.Controller
{
    public class conCantores : KuraFrameWork.ClasseBase.csControllerBase
    {
        private static coCantores _objCo;
        public static coCantores  objCo
        {
            get { return conCantores._objCoCantores; }
            set { conCantores._objCoCantores = value; }
        }

        private static coCantores _objCoCantores;
        public coCantores objCoCantores
        {
            get { return _objCoCantores; }
            set { _objCoCantores = value; }
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public conCantores()
        {
            _objCoCantores = new coCantores();
            _objCo = _objCoCantores;
        }

        /// <summary>
        /// Select
        /// </summary>
        /// <returns></returns>
        public static bool Select()
        {
            _strMensagemErro = "";

            if (!_objCoCantores.Select(out _dtDados))
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

            if (!_objCoCantores.Inserir())
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

            if (!_objCoCantores.Alterar())
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

            if (!_objCoCantores.Excluir())
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
            if (_objCo.nmCantor.Trim().Equals(""))
            {
                _strMensagemErro = "Informe o nome do Cantor.";
                return false;
            }

            if (_objCo.cdCidade == 0)
            {
                _strMensagemErro = "Selecione a cidade do Cantor.";
                return false;
            }

            if (_objCo.dtNascimento.CompareTo(Convert.ToDateTime(null)) == 0)
            {
                _strMensagemErro = "Informe a data de nascimento.";
                return false;
            }

            return true;
        }
    }
}