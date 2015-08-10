<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page/mpKaraoke.Master" AutoEventWireup="true" CodeBehind="AlterarOrdemApresentacao.aspx.cs" Inherits="wappKaraoke.Movimentacoes.AlterarOrdemApresentacao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="panel-title">Alterar Status do Cantor</span>
                </div><!--Panel Header-->
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-3">
                        </div>
                        <div class="col-sm-6">
                            <asp:Literal ID="ltMensagem" runat="server"></asp:Literal>
                        </div>
                        <div class="col-sm-3">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-3">
                        </div>
                        <div class="col-sm-6">
                            <asp:TextBox ID="nuCantor" class="form-control" runat="server" 
                                placeholder="Número do Cantor..." Visible="True"
                                AutoPostBack="True" ontextchanged="nuCantor_TextChanged"></asp:TextBox>
                            <br />
                            <asp:Literal ID="ltInfoCantor" runat="server"></asp:Literal>  
                            <asp:DropDownList ID="cdStatus" class="form-control selectpicker" style="text-align:left" 
                                runat="server" Width="100%" AutoPostBack="false">
                            </asp:DropDownList>             
                            <br/>
                            <asp:LinkButton ID="btnUltimoDaCategoria" 
                                runat="server" 
                                CssClass="btn btn-primary btn-block btn-success" 
                                onclick="btnUltimoDaCategoria_Click">
                                <i aria-hidden="true" class="glyphicon glyphicon-hand-down"></i>&nbsp;&nbsp;Final da Categoria
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-3">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
