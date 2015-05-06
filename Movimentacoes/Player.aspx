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
                        <div class="col-sm-6">
                            <audio controls>
                                <source src="C:\Users\Emerson\Music\brunna\here without you.mp3" type="autio/mp3">
                                <a href="C:\Users\Emerson\Music\brunna\here without you.mp3">Musica</a>
                            </audio>
                        </div>
                        <div class="col-sm-6">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
