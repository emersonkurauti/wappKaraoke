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
            "   <li class=\"span4\">" +"\n"+
            "        <div class=\"thumbnail\">" + "\n" +
            "          <a href=\"[strCaminhoImagem]\" title=\"[strDescImagem]\" data-gallery=\"\">" + "\n" +
            "   	        <img src=\"[strCaminhoImagem]\" />" + "\n" +
            "          </a>" + "\n" +
            "   	    <div class=\"caption\">" + "\n" +
            "   		    <p>[strDescImagem]</p>" + "\n" +
            "   		    <div class=\"row\">" + "\n" +
            "   			    <div class=\"col-sm-6\">" + "\n" +
            "	    			    <a href=\"[strCaminhoImagem]\" title=\"[strDescImagem]\"" + "\n" +
            "                        data-gallery=\"\" Class=\"btn btn-primary btn-block\">" + "\n" +
            "	    					    <i class=\"glyphicon glyphicon-zoom-in\"></i>&nbsp;&nbsp;Ampliar" + "\n" +
            "	    				</a>" + "\n" +
            "	    		    </div>" + "\n" +
            "	    		    <div class=\"col-sm-6\">" + "\n" +
            "                       <a ID=\"lnkRemover_[strSeqImagem]\" class=\"btn btn-primary btn-block btn-danger\">" + "\n" +
            "	    				    <i class=\"glyphicon glyphicon-trash\"></i>&nbsp;&nbsp;Remover" + "\n" +
            "	    			    </a>" + "\n" +
            "	    		    </div>" + "\n" +
            "	    	    </div>" + "\n" +
            "	     </div>" + "\n" +
            "       </div>" + "\n" +
            "   </li>";
    }
}