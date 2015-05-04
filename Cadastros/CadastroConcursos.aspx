<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page/mpKaraoke.Master" AutoEventWireup="true" CodeBehind="CadastroConcursos.aspx.cs" Inherits="wappKaraoke.Cadastros.CadastroConcursos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $('[id*=gvAssociacoes]').footable();
        });

        $(function () {
            $('[id*=gvGrupoJuradoConcurso]').footable();
        });

        $(function () {
            $('[id*=gvFasesConcurso]').footable();
        });
    </script>
    <asp:Literal ID="ltJavaScript" runat="server"></asp:Literal> <!--Caso precise de agrupamento nas tabelas-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"/>
    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="panel-title">Cadastro de Concursos</span>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs">
                        <li class="active"><a href="#Concurso" data-toggle="tab">Concurso</a></li>
                        <li><a href="#Arquivos" data-toggle="tab">Arquivos</a></li>
                        <li><a href="#Associacoes" data-toggle="tab">Associações</a></li>
                        <li><a href="#Jurados" data-toggle="tab">Jurados</a></li>
                        <li><a href="#Fases" data-toggle="tab">Fases</a></li>
                        <li><a href="#Categorias" data-toggle="tab">Categorias</a></li>
                    </ul>
                    <div id="my-tab-content" class="tab-content">
                        <div class="tab-pane active" id="Concurso">
                            <div class="panel panel-default" style="border-top: 0px">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="nmConcurso" class="form-control" runat="server" 
                                                placeholder="Nome do Concurso..." Visible="True">
                                            </asp:TextBox>
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
                                                runat="server" Width="100%" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">                        
                                        <div class="col-sm-4">
                                            <div class='input-group date'>
                                                <asp:TextBox ID="dtIniConsurso" runat="server" class="form-control"
                                                    placeholder="Ex. 13/01/2015" Visible="True">
                                                </asp:TextBox>
                                                <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span>
                                                    Dt. Início
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class='input-group date'>
                                                <asp:TextBox ID="dtFimConsurso" runat="server" class="form-control"
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
                                                    CssClass="btn btn-primary btn-block">
                                                <i class="glyphicon glyphicon-saved"></i>&nbsp;&nbsp;Fechar
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:LinkButton ID="btnReabrir" 
                                                    runat="server" 
                                                    CssClass="btn btn-primary btn-block btn-warning">
                                                <i class="glyphicon glyphicon-open"></i>&nbsp;&nbsp;Reabrir
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <br />
                                </div><!--<div class="panel-body">-->
                            </div> <!--<div class="panel panel-default" style="border-top: 0px">-->                            
                        </div> <!--<div class="tab-pane active" id="Concurso">-->
                        <div class="tab-pane" id="Arquivos">
                            <asp:UpdatePanel ID="upArquivos" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="panel panel-default" style="border-top: 0px">
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <ul class="nav nav-tabs">
                                                        <li class="active"><a href="#Imagens" data-toggle="tab">Imagens</a></li>
                                                        <li><a href="#Documentos" data-toggle="tab">Documentos</a></li>
                                                    </ul>
                                                    <div id="my-tab-content-2" class="tab-content">
                                                        <div class="tab-pane active" id="Imagens">
                                                            <div class="panel panel-default" style="border-top: 0px">
                                                                <div class="panel-body">
                                                                    <div class="row">
                                                                        <div class="col-sm-12">
                                                                            <div class="row-fluid">
                                                                                <ul class="thumbnails">
                                                                                    <li class="span4">
                                                                                        <div class="thumbnail">
                                                                                            <img src="../assets/img/bootstrap-mdo-sfmoma-02.jpg" />
                                                                                            <div class="caption">
                                                                                                <h4>Título IMG 1</h4>
                                                                                                <p>descrição da imagem</p>
                                                                                                <div class="row">
                                                                                                    <div class="col-sm-6">
                                                                                                        <asp:LinkButton ID="btnAmplia1" 
                                                                                                                runat="server" 
                                                                                                                CssClass="btn btn-primary btn-block">
                                                                                                            <i class="glyphicon glyphicon-zoom-in"></i>&nbsp;&nbsp;Ampliar
                                                                                                        </asp:LinkButton>
                                                                                                    </div>
                                                                                                    <div class="col-sm-6">
                                                                                                        <asp:LinkButton ID="btnRemove1" 
                                                                                                                runat="server" 
                                                                                                                CssClass="btn btn-primary btn-block btn-danger">
                                                                                                            <i class="glyphicon glyphicon-trash"></i>&nbsp;&nbsp;Remover
                                                                                                        </asp:LinkButton>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </li>
                                                                                    <li class="span4">
                                                                                        <div class="thumbnail">
                                                                                            <img src="../assets/img/bootstrap-mdo-sfmoma-01.jpg" />
                                                                                            <div class="caption">
                                                                                                <h4>Título IMG 2</h4>
                                                                                                <p>descrição da imagem</p>
                                                                                                <div class="row">
                                                                                                    <div class="col-sm-6">
                                                                                                        <asp:LinkButton ID="btnAmplia2" 
                                                                                                                runat="server" 
                                                                                                                CssClass="btn btn-primary btn-block">
                                                                                                            <i class="glyphicon glyphicon-zoom-in"></i>&nbsp;&nbsp;Ampliar
                                                                                                        </asp:LinkButton>
                                                                                                    </div>
                                                                                                    <div class="col-sm-6">
                                                                                                        <asp:LinkButton ID="btnRemove2" 
                                                                                                                runat="server" 
                                                                                                                CssClass="btn btn-primary btn-block btn-danger">
                                                                                                            <i class="glyphicon glyphicon-trash"></i>&nbsp;&nbsp;Remover
                                                                                                        </asp:LinkButton>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </li>
                                                                                    <li class="span4">
                                                                                        <div class="thumbnail">
                                                                                            <img src="../assets/img/bootstrap-mdo-sfmoma-03.jpg" />
                                                                                            <div class="caption">
                                                                                                <h4>Título IMG 3</h4>
                                                                                                <p>descrição da imagem</p>
                                                                                                <div class="row">
                                                                                                    <div class="col-sm-6">
                                                                                                        <asp:LinkButton ID="btnAmplia3" 
                                                                                                                runat="server" 
                                                                                                                CssClass="btn btn-primary btn-block">
                                                                                                            <i class="glyphicon glyphicon-zoom-in"></i>&nbsp;&nbsp;Ampliar
                                                                                                        </asp:LinkButton>
                                                                                                    </div>
                                                                                                    <div class="col-sm-6">
                                                                                                        <asp:LinkButton ID="btnRemove3" 
                                                                                                                runat="server" 
                                                                                                                CssClass="btn btn-primary btn-block btn-danger">
                                                                                                            <i class="glyphicon glyphicon-trash"></i>&nbsp;&nbsp;Remover
                                                                                                        </asp:LinkButton>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </li>
                                                                                </ul>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-12">
                                                                            <div class="row-fluid">
                                                                                <ul class="thumbnails">
                                                                                    <li class="span4">
                                                                                        <div class="thumbnail">
                                                                                            <img src="../assets/img/bootstrap-mdo-sfmoma-03.jpg" />
                                                                                            <div class="caption">
                                                                                                <h4>Título IMG 4</h4>
                                                                                                <p>descrição da imagem</p>
                                                                                                <div class="row">
                                                                                                    <div class="col-sm-6">
                                                                                                        <asp:LinkButton ID="LinkButton1" 
                                                                                                                runat="server" 
                                                                                                                CssClass="btn btn-primary btn-block">
                                                                                                            <i class="glyphicon glyphicon-zoom-in"></i>&nbsp;&nbsp;Ampliar
                                                                                                        </asp:LinkButton>
                                                                                                    </div>
                                                                                                    <div class="col-sm-6">
                                                                                                        <asp:LinkButton ID="LinkButton2" 
                                                                                                                runat="server" 
                                                                                                                CssClass="btn btn-primary btn-block btn-danger">
                                                                                                            <i class="glyphicon glyphicon-trash"></i>&nbsp;&nbsp;Remover
                                                                                                        </asp:LinkButton>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </li>
                                                                                    <li class="span4">
                                                                                        <div class="thumbnail">
                                                                                            <img src="../assets/img/bootstrap-mdo-sfmoma-03.jpg" />
                                                                                            <div class="caption">
                                                                                                <h4>Título IMG 4</h4>
                                                                                                <p>descrição da imagem</p>
                                                                                                <div class="row">
                                                                                                    <div class="col-sm-6">
                                                                                                        <asp:LinkButton ID="LinkButton3" 
                                                                                                                runat="server" 
                                                                                                                CssClass="btn btn-primary btn-block">
                                                                                                            <i class="glyphicon glyphicon-zoom-in"></i>&nbsp;&nbsp;Ampliar
                                                                                                        </asp:LinkButton>
                                                                                                    </div>
                                                                                                    <div class="col-sm-6">
                                                                                                        <asp:LinkButton ID="LinkButton4" 
                                                                                                                runat="server" 
                                                                                                                CssClass="btn btn-primary btn-block btn-danger">
                                                                                                            <i class="glyphicon glyphicon-trash"></i>&nbsp;&nbsp;Remover
                                                                                                        </asp:LinkButton>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </li>
                                                                                    <li class="span4">
                                                                                        <div class="thumbnail">
                                                                                            <img src="../assets/img/bootstrap-mdo-sfmoma-03.jpg" />
                                                                                            <div class="caption">
                                                                                                <h4>Título IMG 4</h4>
                                                                                                <p>descrição da imagem</p>
                                                                                                <div class="row">
                                                                                                    <div class="col-sm-6">
                                                                                                        <asp:LinkButton ID="LinkButton5" 
                                                                                                                runat="server" 
                                                                                                                CssClass="btn btn-primary btn-block">
                                                                                                            <i class="glyphicon glyphicon-zoom-in"></i>&nbsp;&nbsp;Ampliar
                                                                                                        </asp:LinkButton>
                                                                                                    </div>
                                                                                                    <div class="col-sm-6">
                                                                                                        <asp:LinkButton ID="LinkButton6" 
                                                                                                                runat="server" 
                                                                                                                CssClass="btn btn-primary btn-block btn-danger">
                                                                                                            <i class="glyphicon glyphicon-trash"></i>&nbsp;&nbsp;Remover
                                                                                                        </asp:LinkButton>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </li>
                                                                                </ul>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div> <!--<div class="tab-pane active" id="Imagens">-->
                                                        <div class="tab-pane" id="Documentos">
                                                            <div class="panel panel-default" style="border-top: 0px">
                                                                <div class="panel-body">
                                                                    <div class="row">
                                                                        <div class="col-sm-12">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div> <!--<div class="tab-pane active" id="Documentos">-->
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div> <!--<div class="tab-pane active" id="Arquivos">-->
                        <div class="tab-pane" id="Associacoes">
                            <asp:UpdatePanel ID="upConcursoAssociacoes" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="panel panel-default" style="border-top: 0px">
                                        <div class="panel-body">
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
                                                    <asp:LinkButton ID="btnAdidiconarAssociacao" 
                                                            runat="server" 
                                                            CssClass="btn btn-success btn-block">
                                                        <i aria-hidden="true" class="glyphicon glyphicon-plus"></i>&nbsp;&nbsp;Adicionar
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
                                                            <asp:BoundField DataField="nmAssociacao" HeaderText="Associação" />
                                                            <asp:BoundField DataField="nmRepresentante" HeaderText="Representante" />
                                                            <asp:BoundField DataField="deEmail" HeaderText="E-mail" />
                                                            <asp:CommandField ButtonType="Button" SelectText="Editar" 
                                                                ShowSelectButton="True">
                                                                <ControlStyle CssClass="btn btn-primary btn-block" />
                                                                <ItemStyle Width="15%" />
                                                            </asp:CommandField>
                                                            <asp:CommandField ButtonType="Button" SelectText="Excluir" 
                                                                ShowSelectButton="True">
                                                                <ControlStyle CssClass="btn btn-primary btn-block btn-danger" />
                                                                <ItemStyle Width="15%" />
                                                            </asp:CommandField>
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
                                                            CssClass="btn btn-success btn-block">
                                                        <i aria-hidden="true" class="glyphicon glyphicon-plus"></i>&nbsp;&nbsp;Adicionar
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
                                                            <asp:CommandField ButtonType="Button" SelectText="Editar" 
                                                                ShowSelectButton="True">
                                                                <ControlStyle CssClass="btn btn-primary btn-block" />
                                                                <ItemStyle Width="15%" />
                                                            </asp:CommandField>
                                                            <asp:CommandField ButtonType="Button" SelectText="Excluir" 
                                                                ShowSelectButton="True">
                                                                <ControlStyle CssClass="btn btn-primary btn-block btn-danger" />
                                                                <ItemStyle Width="15%" />
                                                            </asp:CommandField>
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
                        <div class="tab-pane" id="Fases">
                            <asp:UpdatePanel ID="upConcursoFases" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="panel panel-default" style="border-top: 0px">
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-sm-10">
                                                    <asp:DropDownList ID="cdFase" class="form-control selectpicker" style="text-align:left" 
                                                        runat="server" Width="100%" AutoPostBack="False">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:LinkButton ID="btnAdicionarFase" 
                                                            runat="server" 
                                                            CssClass="btn btn-success btn-block">
                                                        <i class="glyphicon glyphicon-plus"></i>&nbsp;&nbsp;Adicionar
                                                    </asp:LinkButton>
                                                </div>
                                            </div><!--<div class="row">-->
                                            <br/>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="gvFasesConcurso" runat="server"
                                                        CssClass="footable table table-bordered table-hover" AutoGenerateColumns="False">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Cód." DataField="cdFase">
                                                                <ItemStyle Width="5%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="deFase" HeaderText="Fase" />
                                                            <asp:CommandField ButtonType="Button" SelectText="Editar" 
                                                                ShowSelectButton="True">
                                                                <ControlStyle CssClass="btn btn-primary btn-block" />
                                                                <ItemStyle Width="15%" />
                                                            </asp:CommandField>
                                                            <asp:CommandField ButtonType="Button" SelectText="Excluir" 
                                                                ShowSelectButton="True">
                                                                <ControlStyle CssClass="btn btn-primary btn-block btn-danger" />
                                                                <ItemStyle Width="15%" />
                                                            </asp:CommandField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="info" />
                                                    </asp:GridView>
                                                </div>
                                            </div> <!--<div class="row">-->
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div> <!--<div class="tab-pane" id="Fases">-->
                        <div class="tab-pane" id="Categorias">
                            <asp:UpdatePanel ID="upCantoresCategorias" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="panel panel-default" style="border-top: 0px">
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-sm-10">
                                                    <asp:DropDownList ID="cdCategoria" class="form-control selectpicker" style="text-align:left" 
                                                        runat="server" Width="100%" AutoPostBack="False">
                                                    </asp:DropDownList>
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
                                    CssClass="btn btn-primary btn-block btn-success">
                                <i class="glyphicon glyphicon-save"></i>&nbsp;&nbsp;Salvar
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-2" align="center" style="float: left">
                            <asp:LinkButton ID="btnCancelar" 
                                    runat="server" 
                                    CssClass="btn btn-primary btn-block btn-danger">
                                <i class="glyphicon glyphicon-remove"></i>&nbsp;&nbsp;Cancelar
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-8" align="right" style="float: right">                            
                        </div>                        
                    </div>                    
                </div><!--<div class="panel-body">-->
            </div>
        </div> 
    </div>
</asp:Content>
