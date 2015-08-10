<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page/mpKaraoke.Master" AutoEventWireup="true" CodeBehind="Player.aspx.cs" Inherits="wappKaraoke.Movimentacoes.Player" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="panel-title">Player</span>
                </div><!--Panel Header-->
                <div class="panel-body">
                    <div class="row" style="text-align:center">
                        <div class="col-sm-12">
                            <asp:Literal ID="ltMensagem" runat="server"></asp:Literal>
                        </div>
                        <asp:Literal ID="ltAudio" runat="server"></asp:Literal>
                    </div>
                    <br/>
                    <div class="row">
                        <div class="col-sm-2">
                            <asp:LinkButton ID="btnVoltar" 
                                runat="server" 
                                CssClass="btn btn-primary btn-block btn-warning"
                                OnClick="btnVoltar_Click">
                                <i aria-hidden="true" class="glyphicon glyphicon-backward"></i>&nbsp;&nbsp;Voltar
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-1">
                            <asp:LinkButton ID="btnPause" 
                                runat="server" 
                                CssClass="btn btn-primary btn-block btn-primary"
                                OnClientClick="document.getElementById('Audio').pause(); return false;">
                                &nbsp;&nbsp;<i aria-hidden="true" class="glyphicon glyphicon-pause"></i>&nbsp;&nbsp;
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-1">
                            <asp:LinkButton ID="btnPlay" 
                                runat="server" 
                                CssClass="btn btn-primary btn-block btn-primary"
                                OnClientClick="document.getElementById('Audio').play(); return false;">
                                &nbsp;&nbsp;<i aria-hidden="true" class="glyphicon glyphicon-play"></i>&nbsp;&nbsp;
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-2">
                            <asp:LinkButton ID="btnAvancar" 
                                runat="server" 
                                CssClass="btn btn-primary btn-block btn-danger"
                                OnClick="btnAvancar_Click">
                                Avançar&nbsp;&nbsp;<i aria-hidden="true" class="glyphicon glyphicon-forward"></i>
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-3" style="text-align:center">
                            <asp:TextBox ID="nuCantor" class="form-control" runat="server" 
                                placeholder="Número do Cantor..." Visible="True" 
                                ontextchanged="nuCantor_TextChanged">
                            </asp:TextBox>
                        </div>
                        <div class="col-sm-3">
                            <asp:LinkButton ID="btnFinalizado" 
                                runat="server" 
                                CssClass="btn btn-primary btn-block btn-success " 
                                OnClick="btnFinalizado_Click">
                                <i aria-hidden="true" class="glyphicon glyphicon-ok"></i>&nbsp;&nbsp;Finalizado
                            </asp:LinkButton>                     
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
