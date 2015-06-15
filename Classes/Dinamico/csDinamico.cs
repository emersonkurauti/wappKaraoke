using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wappKaraoke.Classes
{
    public static class csDinamico
    {
        public static string strInicioLista = "\n" + "<div class=\"row-fluid\">";
        public static string strFinalLista = "\n" + "</div>";

        public static string strDivImagem = "\n" +
            "   <li class=\"span4\">" + "\n" +
            "        <div class=\"thumbnail\">" + "\n" +
            "          <a href=\"[strCaminhoImagem]\" title=\"[strDescImagem]\" data-gallery=\"\">" + "\n" +
            "   	        <img src=\"[strCaminhoImagem]\" alert=\"[strDescImagem]\" />" + "\n" +
            "          </a>" + "\n" +
            "   	    <div class=\"caption\">" + "\n" +
            "   		    <div class=\"well well-sm\">[strDescImagem]</div>" + "\n" +
            "   		    <div class=\"row\">" + "\n" +
            "   			    <div class=\"col-sm-6\">" + "\n" +
            "	    			    <a ID=\"lnkEditar_[strSeqImagem]\" class=\"btn btn-primary btn-block btn-info\" "+"\n"+
            "                           onClick=\"lnkEditar_[strSeqImagem]_Click();\">" + "\n" +
            "	    				    <i class=\"glyphicon glyphicon-edit\"></i>&nbsp;&nbsp;Editar" + "\n" +
            "	    			    </a>" + "\n" +
            "	    		    </div>" + "\n" +
            "	    		    <div class=\"col-sm-6\">" + "\n" +
            "                       <a ID=\"lnkRemover_[strSeqImagem]\" class=\"btn btn-primary btn-block btn-danger\" "+ "\n" +
            "                           onClick=\"lnkRemover_[strSeqImagem]_Click();\">" + "\n" +
            "	    				    <i class=\"glyphicon glyphicon-trash\"></i>&nbsp;&nbsp;Remover" + "\n" +
            "	    			    </a>" + "\n" +
            "	    		    </div>" + "\n" +
            "	    	    </div>" + "\n" +
            "	     </div>" + "\n" +
            "       </div>" + "\n" +
            "   </li>";

        public static string strDivImagemEdit = "\n" +
            "   <li class=\"span4\">" + "\n" +
            "        <div class=\"thumbnail\">" + "\n" +
            "          <a href=\"[strCaminhoImagem]\" title=\"[strDescImagem]\" data-gallery=\"\">" + "\n" +
            "   	        <img src=\"[strCaminhoImagem]\" alert=\"[strDescImagem]\" />" + "\n" +
            "          </a>" + "\n" +
            "   	    <div class=\"caption\">" + "\n" +
            "   		    <input type=\"text\" ID=\"deArquivoEdit\" class=\"form-control\" " + "\n" +
            "                 placeholder=\"Descrição da Imagem...\" value=\"[strDescImagem]\" />" + "\n" +
            "               <br/>" + "\n" +
            "   		    <div class=\"row\">" + "\n" +
            "   			    <div class=\"col-sm-6\">" + "\n" +
            "	    			    <a ID=\"lnkSalvar_[strSeqImagem]\" class=\"btn btn-primary btn-block btn-sucess\" " + "\n" +
            "                           onClick=\"lnkSalvar_[strSeqImagem]_Click();\">" + "\n" +
            "	    				    <i class=\"glyphicon glyphicon-save\"></i>&nbsp;&nbsp;Confirmar" + "\n" +
            "	    			    </a>" + "\n" +
            "	    		    </div>" + "\n" +
            "	    		    <div class=\"col-sm-6\">" + "\n" +
            "                       <a ID=\"lnkRemover_[strSeqImagem]\" class=\"btn btn-primary btn-block btn-danger\" " + "\n" +
            "                           onClick=\"lnkRemover_[strSeqImagem]_Click();\">" + "\n" +
            "	    				    <i class=\"glyphicon glyphicon-trash\"></i>&nbsp;&nbsp;Remover" + "\n" +
            "	    			    </a>" + "\n" +
            "	    		    </div>" + "\n" +
            "	    	    </div>" + "\n" +
            "	     </div>" + "\n" +
            "       </div>" + "\n" +
            "   </li>";
    }
}