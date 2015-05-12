using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wappKaraoke.Mensagem
{
    static class csMensagem
    {
        //Padrão cadastro
        public const string msgInserir = "Erro ao inserir o registro.";
        public const string msgRemover = "Erro ao remover o registro.";
        public const string msgAlterar = "Erro ao alterar o registro.";
        public const string msgConsultar = "Erro ao consultar.";
        public const string msgSelecionarRegistro = "Erro ao selecionar o registro.";

        //Padrão Tipo de Mensagens
        public const string msgWarning = "warning";
        public const string msgDanger = "danger";
        public const string msgSucess = "success";
        public const string msgInfo = "info";

        //Títulos
        public const string msgTitFalhaGenerica = "FALHA!";
        public const string msgTitFalaAoConsultar = "FALHA AO CONSULTAR!";
        public const string msgOperacaoComSucesso = "SUCESSO!";

        //Mensagem Padrão Operação
        public const string msgFalhaAoConsultarFiltro = "Ocorreu uma falhar ao consultar com o filtro informado.";
        public const string msgRegistroExcluido = "O registro foi excluído com sucesso!";
    }
}