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

        $(function () {
            $('[id*=gvCantoresConcurso]').footable();
        })

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="panel-title">Cadastro de Concursos</span>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs">
                        <li role="presentation" class="active"><a href="#Concurso" data-toggle="tab">Concurso</a></li>
                        <li role="presentation"><a href="#Associacoes" data-toggle="tab">Associações</a></li>
                        <li role="presentation"><a href="#Jurados" data-toggle="tab">Jurados</a></li>
                        <li role="presentation"><a href="#Fases" data-toggle="tab">Fases</a></li>
                        <li role="presentation"><a href="#Cantores" data-toggle="tab">Cantores</a></li>
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
                                            <div class='input-group date' id='datetimepicker1'>
                                                <asp:TextBox ID="dtIniConsurso" runat="server" class="form-control"
                                                    placeholder="Ex. 13/01/2015" Visible="True">
                                                </asp:TextBox>
                                                <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span>
                                                    Dt. Início
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class='input-group date' id='Div1'>
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
                                                <i aria-hidden="true" class="glyphicon glyphicon-saved"></i>&nbsp;&nbsp;Fechar
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:LinkButton ID="btnReabrir" 
                                                    runat="server" 
                                                    CssClass="btn btn-primary btn-block btn-warning">
                                                <i aria-hidden="true" class="glyphicon glyphicon-open"></i>&nbsp;&nbsp;Reabrir
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <br />
                                </div><!--<div class="panel-body">-->
                            </div> <!--<div class="panel panel-default" style="border-top: 0px">-->                            
                        </div> <!--<div class="tab-pane active" id="Concurso">-->
                        <div class="tab-pane" id="Associacoes">
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
                        </div> <!--<div class="tab-pane" id="Associações">-->
                        <div class="tab-pane" id="Jurados">
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
                        </div> <!--<div class="tab-pane" id="Jurados">-->
                        <div class="tab-pane" id="Fases">
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
                                                <i aria-hidden="true" class="glyphicon glyphicon-plus"></i>&nbsp;&nbsp;Adicionar
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
                        </div> <!--<div class="tab-pane" id="Fases">-->
                        <div class="tab-pane" id="Cantores">
                            <div class="panel panel-default" style="border-top: 0px">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-5">
                                            <asp:DropDownList ID="cdCantor" class="form-control selectpicker" style="text-align:left" 
                                                runat="server" Width="100%" AutoPostBack="False">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-5">
                                            <asp:DropDownList ID="cdAssociacaoCantor" class="form-control selectpicker" style="text-align:left" 
                                                runat="server" Width="100%" AutoPostBack="False">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:LinkButton ID="btnAdicionarCantor" 
                                                    runat="server" 
                                                    CssClass="btn btn-success btn-block">
                                                <i aria-hidden="true" class="glyphicon glyphicon-plus"></i>&nbsp;&nbsp;Adicionar
                                            </asp:LinkButton>
                                        </div>
                                    </div><!--<div class="row">-->
                                    <br/>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <asp:GridView ID="gvCantoresConcurso" runat="server"
                                                CssClass="footable table table-bordered table-hover" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Cód." DataField="cdCantor">
                                                        <ItemStyle Width="5%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="nmCantor" HeaderText="Cantor" />
                                                    <asp:BoundField DataField="nmAssociacao" HeaderText="Associação" />
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
                        </div> <!--<div class="tab-pane" id="Cantores">-->
                    </div> <!--<div id="my-tab-content" class="tab-content">-->
                    <div class="row">
                        <div class="col-sm-2" align="left" style="float: left">
                            <asp:LinkButton ID="btnSalvar" 
                                    runat="server" 
                                    CssClass="btn btn-primary btn-block btn-success">
                                <i aria-hidden="true" class="glyphicon glyphicon-save"></i>&nbsp;&nbsp;Salvar
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-2" align="center" style="float: left">
                            <asp:LinkButton ID="btnCancelar" 
                                    runat="server" 
                                    CssClass="btn btn-primary btn-block btn-danger">
                                <i aria-hidden="true" class="glyphicon glyphicon-remove"></i>&nbsp;&nbsp;Cancelar
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
