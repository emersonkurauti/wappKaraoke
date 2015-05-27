<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page/mpKaraoke.Master" AutoEventWireup="true" CodeBehind="ConsultaCantores.aspx.cs" Inherits="wappKaraoke.Cadastros.ConsultaCantores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../css/footable-0.1.css" rel="stylesheet" type="text/css" />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="panel-title">Consulta de Cantores</span>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    <asp:TextBox ID="cdCantor" class="form-control" runat="server" 
                                        placeholder="Cód. Cantor..." Visible="True">
                                    </asp:TextBox>
                                </div>
                                <div class="col-md-5">
                                    <asp:TextBox ID="nmCantor" class="form-control" runat="server" 
                                        placeholder="Nome do Cantor..." Visible="True">
                                    </asp:TextBox>
                                </div>
                                <div class="col-md-5">
                                    <asp:TextBox ID="nmNomeKanji" class="form-control" runat="server" 
                                        placeholder="Nome do Cantor Kanji..." Visible="True">
                                    </asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    <div class='input-group date' id='datetimepicker1'>
                                        <asp:TextBox ID="dtNascimento" runat="server" class="form-control"
                                            placeholder="Ex. 13/01/2015" Visible="True">
                                        </asp:TextBox>
                                        <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-md-10">
                                    <asp:TextBox ID="nmNomeArtistico" class="form-control" runat="server" 
                                        placeholder="Nome Artístico..." Visible="True">
                                    </asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    <asp:TextBox ID="nuRG" class="form-control" runat="server" 
                                        placeholder="RG do Cantor..." Visible="True">
                                    </asp:TextBox>
                                </div>
                                <div class="col-md-10">
                                    <asp:DropDownList ID="cdCidade" class="form-control selectpicker" style="text-align:left" 
                                        runat="server" Width="100%" AutoPostBack="False">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-8">
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
                                        <i aria-hidden="true" class="glyphicon glyphicon-plus"></i>&nbsp;&nbsp;Novo Cantor
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br/>
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:Literal ID="ltMensagem" runat="server"></asp:Literal>
                            <br />
                            <asp:GridView ID="gvDados" runat="server"
                                CssClass="footable table table-bordered table-hover" 
                                AutoGenerateColumns="False"
                                OnRowDataBound="gvDados_RowDataBound" 
                                OnRowCommand="gvDados_RowCommand" 
                                onrowdeleting="gvDados_RowDeleting">
                                <Columns>
                                    <asp:BoundField HeaderText="Cód." DataField="cdCantor">
                                        <ItemStyle Width="5%" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Nome - Kanji">
                                        <ItemTemplate>
                                            <asp:Literal ID="ltNomeKanji" runat="server">
                                            </asp:Literal>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Nome" DataField="nmCantor" Visible="False">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Kanji" DataField="nmNomeKanji" Visible="False">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Nome Artístico" DataField="nmNomeArtistico">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="RG" DataField="nuRG">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Telefone" DataField="nuTelefone">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="E-Mail" DataField="deEmail">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Dt. Nasc." DataField="dtNascimento">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Cidade" DataField="cdCidade">
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
						                        CommandArgument='<%# Eval("cdCantor") + "$" + Eval("nmCantor") %>'
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
                            <i aria-hidden="true" class="glyphicon glyphicon-plus"></i>&nbsp;&nbsp;Novo Cantor
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
