<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page/mpKaraoke.Master" AutoEventWireup="true" CodeBehind="AlterarStatusCantor.aspx.cs" Inherits="wappKaraoke.Movimentacoes.AlterarStatusCantor" %>
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
                            <asp:DropDownList ID="cdConcurso" class="form-control selectpicker" style="text-align:left" 
                                runat="server" Width="100%" AutoPostBack="True" 
                                onselectedindexchanged="cdConcurso_SelectedIndexChanged">
                            </asp:DropDownList>
                            <br/>
                            <asp:TextBox ID="nuCantor" class="form-control" runat="server" 
                                placeholder="Número do Cantor..." Visible="True" 
                                ontextchanged="nuCantor_TextChanged" AutoPostBack="True"></asp:TextBox>
                            <br/>
                            <asp:Literal ID="ltInfoCantor" runat="server"></asp:Literal>
                            <br/>
                            <asp:DropDownList ID="cdFase" class="form-control selectpicker" style="text-align:left" 
                                runat="server" Width="100%" AutoPostBack="True" 
                                onselectedindexchanged="cdFase_SelectedIndexChanged">
                            </asp:DropDownList>
                            <br/>
                            <asp:DropDownList ID="cdCategoria" class="form-control selectpicker" style="text-align:left" 
                                runat="server" Width="100%" AutoPostBack="false">
                            </asp:DropDownList>
                            <br/>                            
                            <asp:DropDownList ID="cdStatus" class="form-control selectpicker" style="text-align:left" 
                                runat="server" Width="100%" AutoPostBack="false">
                            </asp:DropDownList>
                            <br/>
                            <asp:LinkButton ID="btnConfirmar" 
                                runat="server" 
                                CssClass="btn btn-primary btn-block btn-success" 
                                onclick="btnConfirmar_Click">
                                <i aria-hidden="true" class="glyphicon glyphicon-save"></i>&nbsp;&nbsp;Confirmar
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
