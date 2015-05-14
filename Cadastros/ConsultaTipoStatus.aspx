<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page/mpKaraoke.Master" AutoEventWireup="true" CodeBehind="ConsultaTipoStatus.aspx.cs" Inherits="wappKaraoke.Cadastros.ConsultaTipoStatus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="panel-title">Consulta de Tipos de Status</span>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:TextBox ID="cdTpStatus" class="form-control" runat="server" 
                                placeholder="Cód. Status..." Visible="True">
                            </asp:TextBox>
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ID="deTpStatus" class="form-control" runat="server" 
                                placeholder="Descrição do Status..." Visible="True">
                            </asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="deCor" class="form-control selectpicker" style="text-align:left" 
                                runat="server" Width="100%" AutoPostBack="False">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <br />
                    <div class="row">
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
                                <i aria-hidden="true" class="glyphicon glyphicon-plus"></i>&nbsp;&nbsp;Novo Status
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:Literal ID="ltMensagem" runat="server"></asp:Literal>
                            <br/>
                            <asp:GridView DataKeyNames="cdTpStatus" ID="gvDados" runat="server" 
                                CssClass="footable table table-bordered table-hover" 
                                AutoGenerateColumns="False" 
                                OnRowDataBound="gvDados_RowDataBound" 
                                OnRowCommand="gvDados_RowCommand" onrowdeleting="gvDados_RowDeleting">
                                <Columns>
                                    <asp:BoundField HeaderText="Cód." DataField="cdTpStatus">
                                        <ItemStyle Width="5%" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Descrição" DataField="deTpStatus" />
                                    <asp:BoundField HeaderText="Cor" DataField="deCor">
                                        <ItemStyle Width="15%" />
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
                                                CommandArgument='<%# Eval("cdTpStatus") + "$" + Eval("deTpStatus") %>'
                                                CommandName='Delete'>
                                                <i aria-hidden="true" class="glyphicon glyphicon-trash"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                </Columns>
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
                                <i aria-hidden="true" class="glyphicon glyphicon-plus"></i>&nbsp;&nbsp;Novo Status
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
