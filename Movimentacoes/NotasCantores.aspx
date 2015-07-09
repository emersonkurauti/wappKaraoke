<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page/mpKaraoke.Master" AutoEventWireup="true" CodeBehind="NotasCantores.aspx.cs" Inherits="wappKaraoke.Movimentacoes.NotasCantores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="panel-title">Notas do Cantor</span>
                </div><!--Panel Header-->
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:Literal ID="ltMensagem" runat="server"></asp:Literal>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:TextBox ID="nuCantor" class="form-control" runat="server" 
                                placeholder="Número do Cantor..." Visible="True">
                            </asp:TextBox>
                        </div>
                    </div>
                    <br/>
                    <!--Notas-->
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:Literal ID="ltNotasJurados" runat="server"></asp:Literal>
                                </div>
                            </div><!--Fim Panel Body Notas-->
                        </div>
                        <div class="col-sm-6">
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:TextBox ID="deFormulaPontuacao" class="form-control" runat="server" 
                                        placeholder="Fórmula..." Visible="True">
                                    </asp:TextBox>
                                </div>
                            </div>
                            <br/>
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:TextBox ID="nuNotaFinal" class="form-control" runat="server" 
                                        placeholder="Nota Final..." Visible="True" ReadOnly="true">
                                    </asp:TextBox>
                                </div>
                            </div>
                            <br/>
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:TextBox ID="deObservacao" class="form-control" runat="server" 
                                        placeholder="Observação..." Visible="True">
                                    </asp:TextBox>
                                </div>
                            </div>
                            <br/>
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:TextBox ID="pcDesconto" class="form-control" runat="server" 
                                        placeholder="Desconto..." Visible="True">
                                    </asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--Fim Notas-->                    
                    <br />
                    <div class="row">
                        <div class="col-sm-2">
                            <asp:LinkButton ID="btnAnterior" 
                                    runat="server" 
                                    CssClass="btn btn-primary btn-block btn-primary">
                                <i aria-hidden="true" class="glyphicon glyphicon-step-backward"></i>&nbsp;&nbsp;Anterior
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-2">
                        </div>
                        <div class="col-sm-4">
                            <asp:LinkButton ID="btnSalvar" 
                                    runat="server" 
                                    CssClass="btn btn-primary btn-block btn-success">
                                <i aria-hidden="true" class="glyphicon glyphicon-save"></i>&nbsp;&nbsp;Salvar
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-2">
                        </div>
                        <div class="col-sm-2">
                            <asp:LinkButton ID="btnProximo" 
                                    runat="server" 
                                    CssClass="btn btn-primary btn-block btn-primary">
                                Próximo&nbsp;&nbsp;<i aria-hidden="true" class="glyphicon glyphicon-step-forward"></i>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div><!--Panel Body-->
            </div><!--Panel principal-->
        </div>
    </div>
</asp:Content>
