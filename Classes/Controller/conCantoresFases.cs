using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using wappKaraoke.Classes.Model.CantoresFases;
using wappKaraoke.Classes.Mensagem;

namespace wappKaraoke.Classes.Controller
{
    public class conCantoresFases : KuraFrameWork.ClasseBase.csControllerBase
    {
        private static coCantoresFases _objCo;
        public static coCantoresFases  objCo
        {
            get { return conCantoresFases._objCoCantoresFases; }
            set { conCantoresFases._objCoCantoresFases = value; }
        }

        private static coCantoresFases _objCoCantoresFases;
        public coCantoresFases objCoCantoresFases
        {
            get { return _objCoCantoresFases; }
            set { _objCoCantoresFases = value; }
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public conCantoresFases()
        {
            _objCoCantoresFases = new coCantoresFases();
            _objCo = _objCoCantoresFases;
        }

        /// <summary>
        /// Select
        /// </summary>
        /// <returns></returns>
        public static bool Select()
        {
            _strMensagemErro = "";

            if (!_objCoCantoresFases.Select(out _dtDados))
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

            if (!_objCoCantoresFases.Inserir())
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

            if (!_objCoCantoresFases.Alterar())
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

            if (!_objCoCantoresFases.Excluir())
            {
                _strMensagemErro = csMensagem.msgRemover;
                return false;
            }
            return true;
        }
    }
}