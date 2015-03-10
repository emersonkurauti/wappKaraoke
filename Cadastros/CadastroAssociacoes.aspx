<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page/mpKaraoke.Master" AutoEventWireup="true" CodeBehind="CadastroAssociacoes.aspx.cs" Inherits="wappKaraoke.Cadastros.CadastroAssociacoes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Cadastro de Associações</h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:TextBox ID="nmAssociacao" class="form-control" runat="server" 
                                placeholder="Descrição da Associação..." Visible="True">
                            </asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-2" align="left" style="float: left">
                            <asp:LinkButton ID="btnSalvar" 
                                    runat="server" 
                                    CssClass="btn btn-primary btn-block btn-success">
                                <i aria-hidden="true" class="glyphicon glyphicon-save"></i>&nbsp;&nbsp;Salvar
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-2" align="center" style="float: left">
                            <asp:LinkButton ID="btnCancelar" 
                                    runat="server" 
                                    CssClass="btn btn-primary btn-block btn-danger">
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
