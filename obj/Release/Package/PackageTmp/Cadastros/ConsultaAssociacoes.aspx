<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page/mpKaraoke.Master" AutoEventWireup="true" CodeBehind="ConsultaAssociacoes.aspx.cs" Inherits="wappKaraoke.Cadastros.ConsultaAssociacoes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../css/footable-0.1.css" rel="stylesheet" type="text/css" />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="panel-title">Consulta de Associações</span>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-2">
                            <asp:TextBox ID="cdAssociacao" class="form-control" runat="server" 
                                placeholder="Cód. Associação..." Visible="True" 
                                ontextchanged="TextChanged" AutoPostBack="True">
                            </asp:TextBox>
                        </div>
                        <div class="col-sm-10">
                            <asp:TextBox ID="nmAssociacao" class="form-control" runat="server" 
                                placeholder="Nome da Associação..." Visible="True"
                                ontextchanged="TextChanged" AutoPostBack="True">
                            </asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-6">
                            <asp:TextBox ID="nmPresidente" class="form-control" runat="server" 
                                placeholder="Nome do Presidente..." Visible="True"
                                ontextchanged="TextChanged" AutoPostBack="True">
                            </asp:TextBox>
                        </div>
                        <div class="col-sm-6">
                            <asp:TextBox ID="nmRepresentante" class="form-control" runat="server" 
                                placeholder="Nome do Representante..." Visible="True"
                                ontextchanged="TextChanged" AutoPostBack="True">
                            </asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-6">
                        </div>
                        <div class="col-md-2">
                            <asp:LinkButton ID="btnLimpar" 
                                        runat="server" 
                                        CssClass="btn btn-primary btn-block btn-warning"
                                        onclick="btnLimpar_Click">
                                <i aria-hidden="true" class="glyphicon glyphicon-erase"></i>&nbsp;&nbsp;Limpar Filtro
                            </asp:LinkButton>
                        </div>
                        <div class="col-md-2">
                            <asp:LinkButton ID="btnBuscar" 
                                        runat="server" 
                                        CssClass="btn btn-primary btn-block btn-info"
                                        onclick="btnBuscar_Click">
                                <i aria-hidden="true" class="glyphicon glyphicon-search"></i>&nbsp;&nbsp;Buscar
                            </asp:LinkButton>
                        </div>
                        <div class="col-md-2">
                            <asp:LinkButton ID="btnNovo1" 
                                        runat="server" 
                                        CssClass="btn btn-primary btn-block btn-success"
                                        onclick="btnNovo1_Click">
                                <i aria-hidden="true" class="glyphicon glyphicon-plus"></i>&nbsp;&nbsp;Nova Associação
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:Literal ID="ltMensagem" runat="server"></asp:Literal>
                            <br/>
                            <asp:GridView ID="gvDados" runat="server"
                                CssClass="footable table table-bordered table-hover" 
                                AutoGenerateColumns="False"
                                OnRowDataBound="gvDados_RowDataBound" 
                                OnRowCommand="gvDados_RowCommand" 
                                onrowdeleting="gvDados_RowDeleting">
                                <Columns>
                                    <asp:BoundField HeaderText="Cód." DataField="cdAssociacao">
                                        <ItemStyle Width="5%" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Nome" DataField="nmAssociacao">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Presidente" DataField="nmPresidente">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Representante" DataField="nmRepresentante">
                                    </asp:BoundField>
                                    <asp:TemplateField>
	                                    <ItemTemplate>
		                                    <asp:LinkButton ID="lnkEdit" runat="server"
			                                    CssClass="btn btn-primary btn-block" Text = "Editar"
			                                    CommandName='Edit'>
			                                    <i aria-hidden="true" class="glyphicon glyphicon-edit"></i>
		                                    </asp:LinkButton>
	                                    </ItemTemplate>
	                                    <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
	                                    <ItemTemplate>
		                                    <asp:LinkButton ID="lnkDelete" runat="server"
			                                    CssClass="btn btn-primary btn-block btn-danger" Text = "Excluir"
			                                    CommandArgument='<%# Eval("cdAssociacao") + "$" + Eval("nmAssociacao") %>'
			                                    CommandName='Delete'>
			                                    <i aria-hidden="true" class="glyphicon glyphicon-trash"></i>
		                                    </asp:LinkButton>
	                                    </ItemTemplate>
	                                    <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="info" />
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-5">
                        </div>
                        <div class="col-sm-5">
                        </div>
                        <div class="col-sm-2">
                            <asp:LinkButton ID="btnNovo" 
                                        runat="server" 
                                        CssClass="btn btn-primary btn-block btn-success"
                                        onclick="btnNovo1_Click">
                                <i aria-hidden="true" class="glyphicon glyphicon-plus"></i>&nbsp;&nbsp;Nova Associação
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
