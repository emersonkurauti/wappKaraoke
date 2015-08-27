using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using wappKaraoke.Classes.Controller;

namespace wappKaraoke.Classes.Model.Associacoes
{
    public class coAssociacoes : KuraFrameWork.ClasseBase.csModelBase
    {
        private static  int _CC_cdRegistro;
        public static int CC_cdRegistro
        {
            get { return coAssociacoes._cdAssociacao; }
            set { coAssociacoes._cdAssociacao = value; }
        }

		/// <summary>
		/// Atributos
		/// </summary>        
		private static string _nuEnderecoRepresentante = "";
        public string nuEnderecoRepresentante
        {
            get { return _nuEnderecoRepresentante; }
            set { _nuEnderecoRepresentante = value; }
        }

		private static string _nmAssociacao = "";
        public string nmAssociacao
        {
            get { return _nmAssociacao; }
            set { _nmAssociacao = value; }
        }

        private static string _deSiglaAssociacao = "";
        public string deSiglaAssociacao
        {
            get { return _deSiglaAssociacao; }
            set { _deSiglaAssociacao = value; }
        }

		private static int _cdAssociacao;
        public int cdAssociacao
        {
            get { return _cdAssociacao; }
            set { _cdAssociacao = value; }
        }

		private static string _nuCEPRepresentante = "";
        public string nuCEPRepresentante
        {
            get { return _nuCEPRepresentante; }
            set { _nuCEPRepresentante = value; }
        }

		private static string _nuEnderecoPresidente = "";
        public string nuEnderecoPresidente
        {
            get { return _nuEnderecoPresidente; }
            set { _nuEnderecoPresidente = value; }
        }

		private static string _nmRepresentante = "";
        public string nmRepresentante
        {
            get { return _nmRepresentante; }
            set { _nmRepresentante = value; }
        }

		private static string _deComplementoRepresentante = "";
        public string deComplementoRepresentante
        {
            get { return _deComplementoRepresentante; }
            set { _deComplementoRepresentante = value; }
        }

		private static string _nmPresidente = "";
        public string nmPresidente
        {
            get { return _nmPresidente; }
            set { _nmPresidente = value; }
        }

		private static string _deRuaRepresentante = "";
        public string deRuaRepresentante
        {
            get { return _deRuaRepresentante; }
            set { _deRuaRepresentante = value; }
        }

		private static string _deBairroRepresentante = "";
        public string deBairroRepresentante
        {
            get { return _deBairroRepresentante; }
            set { _deBairroRepresentante = value; }
        }

		private static string _deBairroPresidente = "";
        public string deBairroPresidente
        {
            get { return _deBairroPresidente; }
            set { _deBairroPresidente = value; }
        }

		private static string _nuCEPPresidente = "";
        public string nuCEPPresidente
        {
            get { return _nuCEPPresidente; }
            set { _nuCEPPresidente = value; }
        }

		private static string _deRuaPresidente = "";
        public string deRuaPresidente
        {
            get { return _deRuaPresidente; }
            set { _deRuaPresidente = value; }
        }

		private static string _deComplementoPresidente = "";
        public string deComplementoPresidente
        {
            get { return _deComplementoPresidente; }
            set { _deComplementoPresidente = value; }
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public coAssociacoes()
        {
            AtualizaObj();
            LimparAtributos();
            _CC_cdRegistro = cdAssociacao;
            tobjCA = typeof(caAssociacoes);
        }

        /// <summary>
        /// Sobrescrito para retornar a chave
        /// </summary>
        /// <returns></returns>
        public override bool Inserir()
        {
            if (base.Inserir())
            {
                cdAssociacao = objBanco.cdChave;
                return true;
            }

            return false;
        }
    }
}