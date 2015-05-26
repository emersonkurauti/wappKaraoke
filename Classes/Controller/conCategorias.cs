using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using wappKaraoke.Classes.Model.Categorias;
using wappKaraoke.Classes.Mensagem;

namespace wappKaraoke.Classes.Controller
{
    public class conCategorias : KuraFrameWork.ClasseBase.csControllerBase
    {
        private static coCategorias _objCo;
        public static coCategorias  objCo
        {
            get { return conCategorias._objCoCategorias; }
            set { conCategorias._objCoCategorias = value; }
        }

        private static coCategorias _objCoCategorias;
        public coCategorias objCoCategorias
        {
            get { return _objCoCategorias; }
            set { _objCoCategorias = value; }
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public conCategorias()
        {
            _objCoCategorias = new coCategorias();
            _objCo = _objCoCategorias;
        }

        /// <summary>
        /// Select
        /// </summary>
        /// <returns></returns>
        public static bool Select()
        {
            _strMensagemErro = "";

            if (!_objCoCategorias.Select(out _dtDados))
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

            if (!_objCoCategorias.Inserir())
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

            if (!_objCoCategorias.Alterar())
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

            if (!_objCoCategorias.Excluir())
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
            if (_objCo.deCategoria.Trim().Equals(""))
            {
                _strMensagemErro = "Informe a descrição da Categoria.";
                return false;
            }

            if (_objCo.deFormulaPontuacao.Trim().Equals(""))
            {
                _strMensagemErro = "Informe a formula da pontuação.";
                return false;
            }

            return true;
        }
    }
}