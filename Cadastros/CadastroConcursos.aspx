<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page/mpKaraoke.Master" AutoEventWireup="true" CodeBehind="CadastroConcursos.aspx.cs" Inherits="wappKaraoke.Cadastros.CadastroConcursos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $('[id*=gvAssociacoes]').footable();
            $('[id*=gvGrupoJuradoConcurso]').footable();
            $('[id*=gvDocumentos]').footable();
        });

        function PegaNomeArquivo() {
            var sNomeArquivo = document.getElementById('<%=fluArquivo.ClientID%>').value;
            document.getElementById('<%=nmArquivo.ClientID%>').setAttribute("value", sNomeArquivo);
            document.getElementById("<%= hdfNmArquivo.ClientID.ToString() %>").value = sNomeArquivo;

            if (EhImagem(sNomeArquivo)) {
                document.getElementById('cdTpArquivoImagem').checked = true;
                document.getElementById("<%= hdfCdTpArquivo.ClientID.ToString() %>").value = 1;
            } else {
                document.getElementById('cdTpArquivoDocumento').checked = true;
                document.getElementById("<%= hdfCdTpArquivo.ClientID.ToString() %>").value = 2;
            }
        }

        function EhImagem(sNomeArquivo) {
            var vExtensoes, sExtensao, bEhImagem;

            vExtensoes = new Array('.jpg', '.jpeg', '.bpm', '.gif', '.png');
            sExtensao = sNomeArquivo.substring(sNomeArquivo.lastIndexOf(".")).toLowerCase();
            bEhImagem = false;

            for (var i = 0; i <= sNomeArquivo.length; i++) {
                if (vExtensoes[i] == sExtensao) {
                    bEhImagem = true;
                    break;
                }
            }

            return bEhImagem;
        }

        function AtivaAbaArquivosImagens() {
            document.getElementById('lnkArquivos').click();
            document.getElementById('lnkImagens').click();
        }

        function AtivaAbaArquivosDocumentos() {
            document.getElementById('lnkArquivos').click();
            document.getElementById('lnkDocumentos').click();
        }
    </script>
    <asp:Literal ID="ltJavaScript" runat="server"></asp:Literal> <!--Caso precise de agrupamento nas tabelas-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"/>
    <asp:HiddenField ID="hdfDescImg" runat="server" />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="panel-title">Cadastro de Concursos</span>
                </div>
                <div class="panel-body">
                    <asp:Literal ID="ltMensagem" runat="server"></asp:Literal>
                    <ul class="nav nav-tabs">
                        <li id="MenuConcurso" class="active"><a id="lnkConcurso" href="#Concurso" data-toggle="tab">Concurso</a></li>
                        <li id="MenuArquivos"><a id="lnkArquivos" href="#Arquivos" data-toggle="tab">Arquivos</a></li>
                        <li id="MenuAssociacoes"><a id="lnkAssociacoes" href="#Associacoes" data-toggle="tab">Associações</a></li>
                        <li id="MenuJurados"><a id="lnkJurados" href="#Jurados" data-toggle="tab">Jurados</a></li>
                        <li id="MenuCategorias"><a id="lnkCategorias" href="#Categorias" data-toggle="tab">Categorias</a></li>
                    </ul>
                    <div id="my-tab-content" class="tab-content">
                        <div class="tab-pane active" id="Concurso">
                            <div class="panel panel-default" style="border-top: 0px">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="nmConcurso" class="form-control" runat="server" 
                                                placeholder="Nome do Concurso..." Visible="True">
                                            </asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="input-group">
                                                <span class="input-group-addon">
                                                    <asp:CheckBox ID="flFinalizado" runat="server" Enabled="false"/>
                                                </span>
                                                <asp:label class="form-control" runat="server">
                                                    Finalizado?
                                                </asp:label>
                                            </div>
                                        </div>
                                    </div>                                
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="nmConcursoKanji" class="form-control" runat="server" 
                                                placeholder="Nome do Concurso Kanji..." Visible="True">
                                            </asp:TextBox> 
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="cdCidade" class="form-control selectpicker" style="text-align:left" 
                                                runat="server" Width="100%">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">                        
                                        <div class="col-sm-4">
                                            <div class='input-group date'>
                                                <asp:TextBox ID="dtIniConcurso" runat="server" class="form-control"
                                                    placeholder="Ex. 13/01/2015" Visible="True">
                                                </asp:TextBox>
                                                <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span>
                                                    Dt. Início
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class='input-group date'>
                                                <asp:TextBox ID="dtFimConcurso" runat="server" class="form-control"
                                                    placeholder="Ex. 13/01/2015" Visible="True">
                                                </asp:TextBox>
                                                <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span>
                                                    Dt. Fim&nbsp;&nbsp;&nbsp;
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:LinkButton ID="btnFechar" 
                                                    runat="server" 
                                                    CssClass="btn btn-primary btn-block"
                                                    onclick="btnFechar_Click">
                                                <i class="glyphicon glyphicon-saved"></i>&nbsp;&nbsp;Fechar
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:LinkButton ID="btnReabrir" 
                                                    runat="server" 
                                                    CssClass="btn btn-primary btn-block btn-warning"
                                                    onclick="btnReabrir_Click">
                                                <i class="glyphicon glyphicon-open"></i>&nbsp;&nbsp;Reabrir
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <br />
                                </div><!--<div class="panel-body">-->
                            </div> <!--<div class="panel panel-default" style="border-top: 0px">-->                            
                        </div> <!--<div class="tab-pane active" id="Concurso">-->
                        <div class="tab-pane" id="Arquivos">
                            <div class="panel panel-default" style="border-top: 0px">
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="upArquivos" runat="server" UpdateMode="Conditional" 
                                        onprerender="upArquivos_PreRender">
                                        <ContentTemplate>
                                            <asp:Literal ID="ltMensagemArquivos" runat="server"></asp:Literal>
                                            <div class="row">
                                                <div class="col-sm-2">
                                                   <span class="btn btn-default btn-file">
                                                        Selecionar Arquivo...
                                                        <asp:FileUpload ID="fluArquivo" runat="server"
                                                        onchange="PegaNomeArquivo();"/>
                                                    </span>                  
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="nmArquivo" class="form-control" runat="server" 
                                                        placeholder="Nome Arquivo..." Visible="True"
                                                        ReadOnly="true">
                                                    </asp:TextBox>
                                                    <asp:HiddenField ID="hdfNmArquivo" runat="server" />
                                                </div>
                                                <div class="col-sm-3">
                                                    <span class="form-control">
                                                        <label class="radio-inline">
                                                            <input type="radio" name="cdTpArquivo" id="cdTpArquivoImagem" disabled="disabled">Imagem
                                                        </label>
                                                        <label class="radio-inline"><input type="radio" name="cdTpArquivo" id="cdTpArquivoDocumento" disabled="disabled">Documento
                                                        </label> 
                                                        <asp:HiddenField ID="hdfCdTpArquivo" runat="server" />
                                                    </span>
                                                </div>
                                            </div>
                                            <br/>
                                            <div class="row">
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="deArquivo" class="form-control" runat="server" 
                                                        placeholder="Descrição do arquivo..." Visible="True">
                                                    </asp:TextBox>
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:LinkButton ID="btnAdicionarArquivo" 
                                                        runat="server" 
                                                        CssClass="btn btn-success btn-block" 
                                                        onclick="btnAdicionarArquivo_Click">
                                                        <i class="glyphicon glyphicon-plus"></i>&nbsp;&nbsp;Adicionar
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <ul class="nav nav-tabs">
                                                        <li id="MenuImagem" class="active"><a id="lnkImagens" href="#Imagens" data-toggle="tab">Imagens</a></li>
                                                        <li id="MenuDocumento"><a id="lnkDocumentos" href="#Documentos" data-toggle="tab">Documentos</a></li>
                                                    </ul>
                                                    <div id="my-tab-content-2" class="tab-content"> 
                                                        <div class="tab-pane active" id="Imagens">
                                                            <div class="panel panel-default" style="border-top: 0px">
                                                                <div class="panel-body">
                                                                    <div class="row">
                                                                        <div class="col-sm-12">
                                                                            <link href="../css/blueimp-gallery.min.css" rel="stylesheet" type="text/css" />
                                                                            <link href="../css/bootstrap-image-gallery.css" rel="stylesheet" type="text/css" />
                                                                            <asp:Literal ID="ltImagens" runat="server"></asp:Literal>
                                                                            <script src="../js/jquery.blueimp-gallery.min.js" type="text/javascript"></script>
                                                                            <script src="../js/bootstrap-image-gallery.js" type="text/javascript"></script>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div> <!--<div class="tab-pane active" id="Imagens-->
                                                        <div class="tab-pane" id="Documentos">
                                                            <div class="panel panel-default" style="border-top: 0px">
                                                                <div class="panel-body">
                                                                    <div class="row">
                                                                        <div class="col-sm-12">
                                                                            <asp:GridView ID="gvDocumentos" runat="server"
                                                                                CssClass="footable table table-bordered table-hover footable" 
                                                                                AutoGenerateColumns="False">
                                                                                <Columns>
                                                                                    <asp:BoundField HeaderText="Cód." DataField="cdArquivo">
                                                                                        <ItemStyle Width="5%" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Nome" DataField="nmArquivo">
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Descrição" DataField="deArquivo">
                                                                                    </asp:BoundField>
                                                                                    <asp:TemplateField>
	                                                                                    <ItemTemplate>
		                                                                                    <asp:LinkButton ID="lnkEdit" runat="server"
			                                                                                    CssClass="btn btn-primary btn-block" Text = "Editar"
			                                                                                    CommandName='Edit'>
			                                                                                    <i class="glyphicon glyphicon-edit"></i>
		                                                                                    </asp:LinkButton>
	                                                                                    </ItemTemplate>
	                                                                                    <ItemStyle Width="5%" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField>
	                                                                                    <ItemTemplate>
		                                                                                    <asp:LinkButton ID="lnkDelete" runat="server"
			                                                                                    CssClass="btn btn-primary btn-block btn-danger" Text = "Excluir"
			                                                                                    CommandArgument='<%# Eval("cdArquivo") + "$" + Eval("nmArquivo") %>'
			                                                                                    CommandName='Delete'>
			                                                                                    <i class="glyphicon glyphicon-trash"></i>
		                                                                                    </asp:LinkButton>
	                                                                                    </ItemTemplate>
	                                                                                    <ItemStyle Width="5%" />
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <HeaderStyle CssClass="info" />
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div> <!--<div class="tab-pane active" id="Documentos">-->
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div> <!--<div class="tab-pane active" id="Arquivos">-->
                        <div class="tab-pane" id="Associacoes">
                            <asp:UpdatePanel ID="upConcursoAssociacoes" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="panel panel-default" style="border-top: 0px">
                                        <div class="panel-body">
                                            <asp:Literal ID="ltMensagemAssociacoes" runat="server"></asp:Literal>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="cdAssociacao" class="form-control selectpicker" style="text-align:left" 
                                                        runat="server" Width="100%" AutoPostBack="False">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="nmRepresentante" class="form-control" runat="server" 
                                                        placeholder="Nome do Representante..." Visible="True">
                                                    </asp:TextBox>
                                                </div>
                                            </div> <!--<div class="row">-->
                                            <br/>
                                            <div class="row">
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="deEmail" class="form-control" runat="server" 
                                                        placeholder="E-mail do Representante..." Visible="True">
                                                    </asp:TextBox>
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:LinkButton ID="btnAdicionarAssociacao" 
                                                        runat="server" 
                                                        CssClass="btn btn-success btn-block" 
                                                        onclick="btnAdicionarAssociacao_Click">
                                                        <i class="glyphicon glyphicon-plus"></i>&nbsp;&nbsp;Adicionar
                                                    </asp:LinkButton>
                                                </div>
                                            </div> <!--<div class="row">-->
                                            <br/>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="gvAssociacoes" runat="server"
                                                        CssClass="footable table table-bordered table-hover" AutoGenerateColumns="False">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Cód." DataField="cdAssociacao">
                                                                <ItemStyle Width="5%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CC_nmAssociacao" HeaderText="Associação" />
                                                            <asp:BoundField DataField="nmRepresentante" HeaderText="Representante" />
                                                            <asp:BoundField DataField="deEmail" HeaderText="E-mail" />
                                                            <asp:TemplateField>
	                                                            <ItemTemplate>
		                                                            <asp:LinkButton ID="lnkEdit" runat="server"
			                                                            CssClass="btn btn-primary btn-block" Text = "Editar"
			                                                            CommandName='Edit'>
			                                                            <i class="glyphicon glyphicon-edit"></i>
		                                                            </asp:LinkButton>
	                                                            </ItemTemplate>
	                                                            <ItemStyle Width="5%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
	                                                            <ItemTemplate>
		                                                            <asp:LinkButton ID="lnkDelete" runat="server"
			                                                            CssClass="btn btn-primary btn-block btn-danger" Text = "Excluir"
			                                                            CommandArgument='<%# Eval("cdAssociacao") + "$" + Eval("CC_nmAssociacao") %>'
			                                                            CommandName='Delete'>
			                                                            <i class="glyphicon glyphicon-trash"></i>
		                                                            </asp:LinkButton>
	                                                            </ItemTemplate>
	                                                            <ItemStyle Width="5%" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="info" />
                                                    </asp:GridView>
                                                </div>
                                            </div> <!--<div class="row">-->
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div> <!--<div class="tab-pane" id="Associações">-->
                        <div class="tab-pane" id="Jurados">
                            <asp:UpdatePanel ID="upConcursoJurados" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="panel panel-default" style="border-top: 0px">
                                        <div class="panel-body">
                                            <asp:Literal ID="ltMensagemJurados" runat="server"></asp:Literal>
                                            <div class="row">
                                                <div class="col-sm-5">
                                                    <asp:DropDownList ID="cdJurado" class="form-control selectpicker" style="text-align:left" 
                                                        runat="server" Width="100%" AutoPostBack="False">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-5">
                                                    <asp:TextBox ID="deGrupo" class="form-control" runat="server" 
                                                        placeholder="Grupo do Jurado..." Visible="True">
                                                    </asp:TextBox>
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:LinkButton ID="btnAdicionarGrupoJurado" 
                                                        runat="server" 
                                                        CssClass="btn btn-success btn-block" 
                                                        onclick="btnAdicionarGrupoJurado_Click">
                                                        <i class="glyphicon glyphicon-plus"></i>&nbsp;&nbsp;Adicionar
                                                    </asp:LinkButton>
                                                </div>
                                            </div> <!--<div class="row">-->
                                            <br/>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="gvGrupoJuradoConcurso" runat="server"
                                                        CssClass="footable table table-bordered table-hover" AutoGenerateColumns="False">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Cód." DataField="cdJurado">
                                                                <ItemStyle Width="5%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="deGrupo" HeaderText="Grupo" />
                                                            <asp:TemplateField HeaderText="Nome - Kanji">
                                                                <ItemTemplate>
                                                                    <asp:Literal ID="ltNomeKanji" runat="server">
                                                                    </asp:Literal>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
	                                                            <ItemTemplate>
		                                                            <asp:LinkButton ID="lnkEdit" runat="server"
			                                                            CssClass="btn btn-primary btn-block" Text = "Editar"
			                                                            CommandName='Edit'>
			                                                            <i class="glyphicon glyphicon-edit"></i>
		                                                            </asp:LinkButton>
	                                                            </ItemTemplate>
	                                                            <ItemStyle Width="5%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
	                                                            <ItemTemplate>
		                                                            <asp:LinkButton ID="lnkDelete" runat="server"
			                                                            CssClass="btn btn-primary btn-block btn-danger" Text = "Excluir"
			                                                            CommandArgument='<%# Eval("cdJurado") %>'
			                                                            CommandName='Delete'>
			                                                            <i class="glyphicon glyphicon-trash"></i>
		                                                            </asp:LinkButton>
	                                                            </ItemTemplate>
	                                                            <ItemStyle Width="5%" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="info" />
                                                    </asp:GridView>
                                                </div>
                                            </div> <!--<div class="row">-->
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div> <!--<div class="tab-pane" id="Jurados">-->
                        <div class="tab-pane" id="Categorias">
                            <asp:UpdatePanel ID="upCantoresCategorias" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="panel panel-default" style="border-top: 0px">
                                        <div class="panel-body">
                                            <asp:Literal ID="ltMensagensCategorias" runat="server"></asp:Literal>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="cdCategoria" class="form-control selectpicker" style="text-align:left" 
                                                        runat="server" Width="100%" AutoPostBack="False">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="cdCantor" class="form-control selectpicker" style="text-align:left" 
                                                        runat="server" Width="100%" AutoPostBack="False">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <br/>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="cdMusica" class="form-control selectpicker" style="text-align:left" 
                                                        runat="server" Width="100%" AutoPostBack="False">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="cdAssociacaoCantor" class="form-control selectpicker" style="text-align:left" 
                                                        runat="server" Width="100%" AutoPostBack="False">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <br/>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="cdFaseCantor" class="form-control selectpicker" style="text-align:left" 
                                                        runat="server" Width="100%" AutoPostBack="False">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="cdStatus" class="form-control selectpicker" style="text-align:left" 
                                                        runat="server" Width="100%" AutoPostBack="False">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <br/>
                                            <div class="row">
                                                <div class="col-sm-10">
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:LinkButton ID="btnAdicionarCategoria" 
                                                            runat="server" 
                                                            CssClass="btn btn-success btn-block"
                                                            OnClick="btnAdicionarCategoria_OnClick">
                                                        <i class="glyphicon glyphicon-plus"></i>&nbsp;&nbsp;Adicionar
                                                    </asp:LinkButton>
                                                </div>
                                            </div><!--<div class="row">-->
                                            <br/>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:Literal ID="ltCategorias" runat="server"></asp:Literal>
                                                </div>
                                            </div> <!--<div class="row">-->
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div> <!--<div class="tab-pane" id="Cantores">-->
                    </div> <!--<div id="my-tab-content" class="tab-content">-->
                    <div class="row">
                        <div class="col-sm-2" align="left" style="float: left">
                            <asp:LinkButton ID="btnSalvar" 
                                    runat="server" 
                                    CssClass="btn btn-primary btn-block btn-success"
                                    onclick="btnSalvar_Click">
                                <i class="glyphicon glyphicon-save"></i>&nbsp;&nbsp;Salvar
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-2" align="center" style="float: left">
                            <asp:LinkButton ID="btnCancelar" 
                                    runat="server" 
                                    CssClass="btn btn-primary btn-block btn-danger"
                                    onclick="btnCancelar_Click">
                                <i class="glyphicon glyphicon-remove"></i>&nbsp;&nbsp;Cancelar
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-8" align="right" style="float: right">
                            <asp:Label id="Label1" runat="server" />
                        </div>                        
                    </div>                    
                </div><!--<div class="panel-body">-->
            </div>
        </div> 
    </div>

    <!-- The Bootstrap Image Gallery lightbox, should be a child element of the document body -->
	<div id="blueimp-gallery" class="blueimp-gallery">
		<!-- The container for the modal slides -->
		<div class="slides"></div>
		<!-- Controls for the borderless lightbox -->
		<h3 class="title"></h3>
		<a class="prev">‹</a>
		<a class="next">›</a>
		<a class="close">×</a>
		<a class="play-pause"></a>
		<ol class="indicator"></ol>
		<!-- The modal dialog, which will be used to wrap the lightbox content -->
		<div class="modal fade">
			<div class="modal-dialog">
				<div class="modal-content">
					<div class="modal-header">
						<button type="button" class="close" aria-hidden="true">&times;</button>
						<h4 class="modal-title"></h4>
					</div>
					<div class="modal-body next"></div>
					<div class="modal-footer">
						<button type="button" class="btn btn-default pull-left prev">
							<i class="glyphicon glyphicon-chevron-left"></i>
							Previous
						</button>
						<button type="button" class="btn btn-primary next">
							Next
							<i class="glyphicon glyphicon-chevron-right"></i>
						</button>
					</div>
				</div>
			</div>
		</div>
	</div>
</asp:Content>
