<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page/mpKaraoke.Master" AutoEventWireup="true" CodeBehind="CadastroAssociacoes.aspx.cs" Inherits="wappKaraoke.Cadastros.CadastroAssociacoes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="panel-title">Cadastro de Associações</span>
                </div>
                <div class="panel-body">
                    <asp:Literal ID="ltMensagem" runat="server"></asp:Literal>
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:TextBox ID="nmAssociacao" class="form-control" runat="server" 
                                placeholder="Descrição da Associação..." Visible="True">
                            </asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <span class="panel-title">Presidente</span>
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="nmPresidente" class="form-control" runat="server" 
                                                placeholder="Nome do Presidente..." Visible="True">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="deRuaPresidente" class="form-control" runat="server" 
                                                placeholder="Rua..." Visible="True">
                                            </asp:TextBox>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="nuEnderecoPresidente" class="form-control" runat="server" 
                                                placeholder="Número..." Visible="True">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="deBairroPresidente" class="form-control" runat="server" 
                                                placeholder="Bairro..." Visible="True">
                                            </asp:TextBox>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="nuCEPPresidente" class="form-control" runat="server" 
                                                placeholder="CEP..." Visible="True">
                                            </asp:TextBox>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="deComplementoPresidente" class="form-control" runat="server" 
                                                placeholder="Complemento..." Visible="True">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <span class="panel-title">Representante</span>
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="nmRepresentante" class="form-control" runat="server" 
                                                placeholder="Nome do Representante..." Visible="True">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="deRuaRepresentante" class="form-control" runat="server" 
                                                placeholder="Rua..." Visible="True">
                                            </asp:TextBox>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="nuEnderecoRepresentante" class="form-control" runat="server" 
                                                placeholder="Número..." Visible="True">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="deBairroRepresentante" class="form-control" runat="server" 
                                                placeholder="Bairro..." Visible="True">
                                            </asp:TextBox>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="nuCEPRepresentante" class="form-control" runat="server" 
                                                placeholder="CEP..." Visible="True">
                                            </asp:TextBox>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="deComplementoRepresentante" class="form-control" runat="server" 
                                                placeholder="Complemento..." Visible="True">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-2" align="left" style="float: left">
                            <asp:LinkButton ID="btnSalvar" 
                                    runat="server" 
                                    CssClass="btn btn-primary btn-block btn-success"
                                    onclick="btnSalvar_Click">
                                <i aria-hidden="true" class="glyphicon glyphicon-save"></i>&nbsp;&nbsp;Salvar
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-2" align="center" style="float: left">
                            <asp:LinkButton ID="btnCancelar" 
                                    runat="server" 
                                    CssClass="btn btn-primary btn-block btn-danger"
                                    onclick="btnCancelar_Click">
                                <i aria-hidden="true" class="glyphicon glyphicon-remove"></i>&nbsp;&nbsp;Cancelar
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-8" align="right" style="float: right">                            
                        </div>                         
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
