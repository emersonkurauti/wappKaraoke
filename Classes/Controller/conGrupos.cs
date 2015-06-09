using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using wappKaraoke.Classes.Model.Grupos;
using wappKaraoke.Classes.Mensagem;

namespace wappKaraoke.Classes.Controller
{
    public class conGrupos : KuraFrameWork.ClasseBase.csControllerBase
    {
        private static coGrupos _objCo;
        public static coGrupos  objCo
        {
            get { return conGrupos._objCoGrupos; }
            set { conGrupos._objCoGrupos = value; }
        }

        private static coGrupos _objCoGrupos;
        public coGrupos objCoGrupos
        {
            get { return _objCoGrupos; }
            set { _objCoGrupos = value; }
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public conGrupos()
        {
            _objCoGrupos = new coGrupos();
            _objCo = _objCoGrupos;
        }

        /// <summary>
        /// Select
        /// </summary>
        /// <returns></returns>
        public static bool Select()
        {
            _strMensagemErro = "";

            if (!_objCoGrupos.Select(out _dtDados))
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

            if (!_objCoGrupos.Inserir())
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

            if (!_objCoGrupos.Alterar())
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

            if (!_objCoGrupos.Excluir())
            {
                _strMensagemErro = csMensagem.msgRemover;
                return false;
            }
            return true;
        }
    }
}