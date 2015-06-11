using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using wappKaraoke.Classes.Paginas_Default;
using wappKaraoke.Classes.Model.Associacoes;
using wappKaraoke.Classes.Controller;
using wappKaraoke.Classes.Model.ConcursosAssociacoes;

namespace wappKaraoke.Classes
{
    public class csAssociacoes : Paginas_Default.csMontaDDL
    {
        private int _cdConcurso;
        public int cdConcurso
        {
            get { return _cdConcurso; }
            set { _cdConcurso = value; }
        }

        private bool _bFiltraConcurso;
        public bool bFiltraConcurso
        {
            get { return _bFiltraConcurso; }
            set { _bFiltraConcurso = value; }
        }

        public csAssociacoes()
        {
            strTextoCombo = "a Associação";
            tobjCa = typeof(caAssociacoes);
            objCon = new conAssociacoes();
        }

        public override DataTable getDtDados()
        {
            if (bFiltraConcurso)
            {
                DataTable dt = new DataTable();
                conConcursosAssociacoes objConConcursosAssociacoes = new conConcursosAssociacoes();
                objConConcursosAssociacoes.objCoConcursosAssociacoes.cdConcurso = _cdConcurso;

                if (!conConcursosAssociacoes.SelectAssociacoesConcurso())
                    return null;

                dt = conAssociacoes.objCo.RetornaEstruturaDT();

                DataRow dr = dt.NewRow();
                dr[caAssociacoes.nmCampoChave.ToString()] = 0;
                dr[caAssociacoes.dePrincipal.ToString()] = "--Selecione " + strTextoCombo + " do Cantor--";

                dt.Rows.Add(dr);

                foreach (DataRow drCopy in objConConcursosAssociacoes.dtDados.Rows)
                {
                    dt.ImportRow(drCopy);
                }

                return dt;
            }
            else
                return base.getDtDados();
        }
    }
}