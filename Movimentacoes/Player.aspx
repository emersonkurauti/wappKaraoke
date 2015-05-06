<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page/mpKaraoke.Master" AutoEventWireup="true" CodeBehind="Player.aspx.cs" Inherits="wappKaraoke.Movimentacoes.Player" %>
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
                        <div class="col-sm-6">
                        </div>
                        <div class="col-sm-6" style="text-align:center">
                            <audio id="Audio"src="../hold_my_hand.mp3"/>
                        </div>
                        <div class="col-sm-6">
                            <asp:Button ID="btnPlay" OnClientClick="document.getElementById('Audio').play(); return false;" runat="server" Text="Paly"/>
                            <asp:Button ID="btnPause" OnClientClick="document.getElementById('Audio').pause(); return false;" runat="server" Text="Pause"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
