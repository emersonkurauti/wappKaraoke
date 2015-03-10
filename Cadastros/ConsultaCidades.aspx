<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page/mpKaraoke.Master" AutoEventWireup="true" CodeBehind="ConsultaCidades.aspx.cs" Inherits="wappKaraoke.Cadastros.ConsultaCidades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                                placeholder="Cód. Cidade..." Visible="True">
                            </asp:TextBox>
                        </div>
                        <div class="col-sm-5">
                            <asp:TextBox ID="nmCidade" class="form-control" runat="server" 
                                placeholder="Nome da Cidade..." Visible="True">
                            </asp:TextBox>
                        </div>
                        <div class="col-sm-1">
                            <asp:TextBox ID="deUF" class="form-control" runat="server" 
                                placeholder="UF..." Visible="True">
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
                                <i aria-hidden="true" class="glyphicon glyphicon-plus"></i>&nbsp;&nbsp;Nova Cidade
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <br/>
                            <asp:GridView ID="gvDados" runat="server"
                                CssClass="footable table table-bordered table-hover footable" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField HeaderText="Cód." DataField="cdCidade">
                                        <ItemStyle Width="5%" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Nome" DataField="nmCidade">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="UF" DataField="deUF">
                                        <ItemStyle Width="5%" />
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
                                <i aria-hidden="true" class="glyphicon glyphicon-plus"></i>&nbsp;&nbsp;Nova Cidade
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
