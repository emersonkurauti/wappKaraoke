using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using wappKaraoke.Classes.Model.CantoresConcursos;
using wappKaraoke.Classes.Mensagem;

namespace wappKaraoke.Classes.Controller
{
    public class conCantoresConcursos : KuraFrameWork.ClasseBase.csControllerBase
    {
        private static coCantoresConcursos _objCo;
        public static coCantoresConcursos  objCo
        {
            get { return conCantoresConcursos._objCoCantoresConcursos; }
            set { conCantoresConcursos._objCoCantoresConcursos = value; }
        }

        private static coCantoresConcursos _objCoCantoresConcursos;
        public coCantoresConcursos objCoCantoresConcursos
        {
            get { return _objCoCantoresConcursos; }
            set { _objCoCantoresConcursos = value; }
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public conCantoresConcursos()
        {
            _objCoCantoresConcursos = new coCantoresConcursos();
            _objCo = _objCoCantoresConcursos;
        }

        /// <summary>
        /// Select
        /// </summary>
        /// <returns></returns>
        public static bool Select()
        {
            _strMensagemErro = "";

            if (!_objCoCantoresConcursos.Select(out _dtDados))
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

            if (!_objCoCantoresConcursos.Inserir())
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

            if (!_objCoCantoresConcursos.Alterar())
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

            if (!_objCoCantoresConcursos.Excluir())
            {
                _strMensagemErro = csMensagem.msgRemover;
                return false;
            }
            return true;
        }
    }
}