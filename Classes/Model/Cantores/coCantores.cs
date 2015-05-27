using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using wappKaraoke.Classes.Controller;
using wappKaraoke.Classes.Model.Cidades;

namespace wappKaraoke.Classes.Model.Cantores
{
    public class coCantores : KuraFrameWork.ClasseBase.csModelBase
    {
        private static  int _CC_cdRegistro;
        public static int CC_cdRegistro
        {
            get { return coCantores._cdCantor; }
            set { coCantores._cdCantor = value; }
        }

		/// <summary>
		/// Atributos
		/// </summary>        
		private static int _cdCidade;
        public int cdCidade
        {
            get { return _cdCidade; }
            set { _cdCidade = value; }
        }

		private static int _cdCantor;
        public int cdCantor
        {
            get { return _cdCantor; }
            set { _cdCantor = value; }
        }

		private static string _nmNomeKanji = "";
        public string nmNomeKanji
        {
            get { return _nmNomeKanji; }
            set { _nmNomeKanji = value; }
        }

		private static string _nuTelefone = "";
        public string nuTelefone
        {
            get { return _nuTelefone; }
            set { _nuTelefone = value; }
        }

		private static string _nuRG = "";
        public string nuRG
        {
            get { return _nuRG; }
            set { _nuRG = value; }
        }

		private static DateTime _dtNascimento;
        public DateTime dtNascimento
        {
            get { return _dtNascimento; }
            set { _dtNascimento = value; }
        }

		private static string _nmCantor = "";
        public string nmCantor
        {
            get { return _nmCantor; }
            set { _nmCantor = value; }
        }

		private static string _deEmail = "";
        public string deEmail
        {
            get { return _deEmail; }
            set { _deEmail = value; }
        }

		private static string _nmNomeArtistico = "";
        public string nmNomeArtistico
        {
            get { return _nmNomeArtistico; }
            set { _nmNomeArtistico = value; }
        }

		private static string _CC_nmCidade = "";
        public string CC_nmCidade
        {
            get { return _CC_nmCidade; }
            set { _CC_nmCidade = value; }
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public coCantores()
        {
            AtualizaObj();
            LimparAtributos();
            _CC_cdRegistro = cdCantor;
            tobjCA = typeof(caCantores);
        }

       /// <summary>
       /// Método sobrescrito por conta do campo calculado
       /// </summary>
       /// <param name="dtDados"></param>
       /// <returns></returns>
       public override bool Select(out DataTable dtDados)
       {
           if (base.Select(out dtDados))
           {
               conCidades objConCidades = new conCidades();

               DataTable dtAux = dtDados;

               dtDados.Columns[caCantores.CC_nmCidade].ReadOnly = false;
               dtDados.Columns[caCantores.CC_nmCidade].MaxLength = 100;

               foreach (DataRow dr in dtAux.Rows)
               {
                   objConCidades.objCoCidades.LimparAtributos();
                   objConCidades.objCoCidades.cdCidade = Convert.ToInt32(dr[caCantores.cdCidade].ToString());


                   if (conCidades.Select())
                   {
                       if (objConCidades.dtDados.Rows.Count > 0)
                       {
                           dr[caCantores.CC_nmCidade] = objConCidades.dtDados.Rows[0][caCidades.nmCidade].ToString();

                       }
                   }
               }

               dtDados = dtAux;
           }
           else
               return false;

           return true;
       }
        /// <summary>
        /// Sobrescrito para retornar a chave
        /// </summary>
        /// <returns></returns>
        public override bool Inserir()
        {
            if (base.Inserir())
            {
                cdCantor = objBanco.cdChave;
                return true;
            }

            return false;
        }
    }
}