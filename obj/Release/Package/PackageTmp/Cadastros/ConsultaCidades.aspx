<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page/mpKaraoke.Master" AutoEventWireup="true" CodeBehind="ConsultaCidades.aspx.cs" Inherits="wappKaraoke.Cadastros.ConsultaCidades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../css/footable-0.1.css" rel="stylesheet" type="text/css" />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="panel-title">Consulta de Cidades</span>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-2">
                            <asp:TextBox ID="cdCidade" class="form-control" runat="server" 
                                placeholder="Cód. Cidade..." Visible="True"
                                ontextchanged="TextChanged" AutoPostBack="True">
                            </asp:TextBox>
                        </div>
                        <div class="col-sm-8">
                            <asp:TextBox ID="nmCidade" class="form-control" runat="server" 
                                placeholder="Nome da Cidade..." Visible="True"
                                ontextchanged="TextChanged" AutoPostBack="True">
                            </asp:TextBox>
                        </div>
                        <div class="col-sm-2">
                            <asp:DropDownList ID="deUF" class="form-control selectpicker" style="text-align:left" 
                                runat="server" Width="100%"
                                ontextchanged="TextChanged" AutoPostBack="True">
                            </asp:DropDownList>
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
                                <i aria-hidden="true" class="glyphicon glyphicon-plus"></i>&nbsp;&nbsp;Nova Cidade
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:Literal ID="ltMensagem" runat="server"></asp:Literal>
                            <br/>
                            <asp:GridView ID="gvDados" runat="server"
                                CssClass="footable table table-bordered table-hover footable" 
                                AutoGenerateColumns="False"
                                OnRowDataBound="gvDados_RowDataBound" 
                                OnRowCommand="gvDados_RowCommand" 
                                onrowdeleting="gvDados_RowDeleting">
                                <Columns>
                                    <asp:BoundField HeaderText="Cód." DataField="cdCidade">
                                        <ItemStyle Width="5%" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Nome" DataField="nmCidade">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="UF" DataField="deUF">
                                        <ItemStyle Width="5%" />
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
			                                    CommandArgument='<%# Eval("cdCidade") + "$" + Eval("nmCidade") %>'
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
                                <i aria-hidden="true" class="glyphicon glyphicon-plus"></i>&nbsp;&nbsp;Nova Cidade
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
