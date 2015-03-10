<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page/mpKaraoke.Master" AutoEventWireup="true" CodeBehind="ConsultaAssociacoes.aspx.cs" Inherits="wappKaraoke.Cadastros.ConsultaAssociacoes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Consulta de Associações</h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-2">
                            <asp:TextBox ID="cdAssociacao" class="form-control" runat="server" 
                                placeholder="Cód. Associação..." Visible="True">
                            </asp:TextBox>
                        </div>
                        <div class="col-sm-6">
                            <asp:TextBox ID="nmAssociacao" class="form-control" runat="server" 
                                placeholder="Nome da Associação..." Visible="True">
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
                                <i aria-hidden="true" class="glyphicon glyphicon-plus"></i>&nbsp;&nbsp;Nova Associação
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <br/>
                            <asp:GridView ID="gvAssociacao" runat="server"
                                CssClass="footable table table-bordered table-hover" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField HeaderText="Cód." DataField="cdAssociacao">
                                        <ItemStyle Width="5%" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Nome" DataField="nmAssociacao">
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

                            <script type="text/javascript">
                                $(function () {
                                    $('[id*=gvCidade]').footable();
                                });
                            </script>

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
                                <i aria-hidden="true" class="glyphicon glyphicon-plus"></i>&nbsp;&nbsp;Nova Associação
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
