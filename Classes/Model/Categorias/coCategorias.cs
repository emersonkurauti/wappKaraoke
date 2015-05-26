using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using wappKaraoke.Classes.Controller;

namespace wappKaraoke.Classes.Model.Categorias
{
    public class coCategorias : KuraFrameWork.ClasseBase.csModelBase
    {
        private static  int _CC_cdRegistro;
        public static int CC_cdRegistro
        {
            get { return coCategorias._cdCategoria; }
            set { coCategorias._cdCategoria = value; }
        }

		/// <summary>
		/// Atributos
		/// </summary>        
		private static string _deFormulaPontuacao = "";
        public string deFormulaPontuacao
        {
            get { return _deFormulaPontuacao; }
            set { _deFormulaPontuacao = value; }
        }

		private static string _deCategoria = "";
        public string deCategoria
        {
            get { return _deCategoria; }
            set { _deCategoria = value; }
        }

		private static int _cdCategoria;
        public int cdCategoria
        {
            get { return _cdCategoria; }
            set { _cdCategoria = value; }
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public coCategorias()
        {
            AtualizaObj();
            LimparAtributos();
            _CC_cdRegistro = cdCategoria;
            tobjCA = typeof(caCategorias);
        }

        /// <summary>
        /// Sobrescrito para retornar a chave
        /// </summary>
        /// <returns></returns>
        public override bool Inserir()
        {
            if (base.Inserir())
            {
                cdCategoria = objBanco.cdChave;
                return true;
            }

            return false;
        }
    }
}