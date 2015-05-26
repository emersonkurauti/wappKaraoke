using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using wappKaraoke.Classes.Controller;
using wappKaraoke.Classes.Model.Cidades;

namespace wappKaraoke.Classes.Model.Jurados
{
    public class coJurados : KuraFrameWork.ClasseBase.csModelBase
    {
        private static  int _CC_cdRegistro;
        public static int CC_cdRegistro
        {
            get { return coJurados._cdJurado; }
            set { coJurados._cdJurado = value; }
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

		private static string _nuTelefone = "";
        public string nuTelefone
        {
            get { return _nuTelefone; }
            set { _nuTelefone = value; }
        }

		private static string _nmJurado = "";
        public string nmJurado
        {
            get { return _nmJurado; }
            set { _nmJurado = value; }
        }

		private static string _nmNomeKanji = "";
        public string nmNomeKanji
        {
            get { return _nmNomeKanji; }
            set { _nmNomeKanji = value; }
        }

		private static int _cdJurado;
        public int cdJurado
        {
            get { return _cdJurado; }
            set { _cdJurado = value; }
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
        public coJurados()
        {
            AtualizaObj();
            LimparAtributos();
            _CC_cdRegistro = cdJurado;
            tobjCA = typeof(caJurados);
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

               dtDados.Columns[caJurados.CC_nmCidade].ReadOnly = false;
               dtDados.Columns[caJurados.CC_nmCidade].MaxLength = 100;

               foreach (DataRow dr in dtAux.Rows)
               {
                   objConCidades.objCoCidades.LimparAtributos();
                   objConCidades.objCoCidades.cdCidade = Convert.ToInt32(dr[caJurados.cdCidade].ToString());


                   if (conCidades.Select())
                   {
                       if (objConCidades.dtDados.Rows.Count > 0)
                       {
                           dr[caJurados.CC_nmCidade] = objConCidades.dtDados.Rows[0][caCidades.nmCidade].ToString();

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
                cdJurado = objBanco.cdChave;
                return true;
            }

            return false;
        }
    }
}