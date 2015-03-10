<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page/mpKaraoke.Master" AutoEventWireup="true" CodeBehind="ConsultaGrupos.aspx.cs" Inherits="wappKaraoke.Cadastros.ConsultaGrupos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="panel-title">Consulta de Grupos</span>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:TextBox ID="cdGrupo" class="form-control" runat="server" 
                                placeholder="Cód. Grupo..." Visible="True">
                            </asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <asp:TextBox ID="deGrupo" class="form-control" runat="server" 
                                placeholder="Descrição do Grupo..." Visible="True">
                            </asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:LinkButton ID="btnBuscar" 
                                        runat="server" 
                                        CssClass="btn btn-primary btn-block btn-info">
                                <i aria-hidden="true" class="glyphicon glyphicon-search"></i>&nbsp;&nbsp;Buscar
                            </asp:LinkButton>
                        </div>
                        <div class="col-md-2">
                            <asp:LinkButton ID="btnNovo1" 
                                        runat="server" 
                                        CssClass="btn btn-primary btn-block btn-success">
                                <i aria-hidden="true" class="glyphicon glyphicon-plus"></i>&nbsp;&nbsp;Novo Grupo
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <br/>
                            <asp:GridView ID="gvDados" runat="server"
                                CssClass="footable table table-bordered table-hover" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField HeaderText="Cód." DataField="cdGrupo">
                                        <ItemStyle Width="5%" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Descrição" DataField="deGrupo">
                                    </asp:BoundField>
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
                    </div>
                    <div class="row">
                        <div class="col-sm-5">
                        </div>
                        <div class="col-sm-5">
                        </div>
                        <div class="col-sm-2">
                               <asp:LinkButton ID="btnNovo" 
                                        runat="server" 
                                        CssClass="btn btn-primary btn-block btn-success">
                                <i aria-hidden="true" class="glyphicon glyphicon-plus"></i>&nbsp;&nbsp;Novo Grupo
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
