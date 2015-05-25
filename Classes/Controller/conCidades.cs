using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using wappKaraoke.Classes.Model.Cidades;
using wappKaraoke.Classes.Mensagem;

namespace wappKaraoke.Classes.Controller
{
    public class conCidades : KuraFrameWork.ClasseBase.csControllerBase
    {
        private static coCidades _objCo;
        public static coCidades  objCo
        {
            get { return conCidades._objCoCidades; }
            set { conCidades._objCoCidades = value; }
        }

        private static coCidades _objCoCidades;
        public coCidades objCoCidades
        {
            get { return _objCoCidades; }
            set { _objCoCidades = value; }
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public conCidades()
        {
            _objCoCidades = new coCidades();
            _objCo = _objCoCidades;
        }

        /// <summary>
        /// Select
        /// </summary>
        /// <returns></returns>
        public static bool Select()
        {
            _strMensagemErro = "";

            if (!_objCoCidades.Select(out _dtDados))
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

            if (!_objCoCidades.Inserir())
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

            if (!_objCoCidades.Alterar())
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

            if (!_objCoCidades.Excluir())
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
            if (_objCo.nmCidade.Trim().Equals(""))
            {
                _strMensagemErro = "Informe o nome da Cidade.";
                return false;
            }

            if (_objCo.deUF.Trim().Equals(""))
            {
                _strMensagemErro = "Informe a UF da Cidade.";
                return false;
            }

            return true;
        }
    }
}