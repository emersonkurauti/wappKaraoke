﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page/mpKaraoke.Master" AutoEventWireup="true" CodeBehind="CadastroMusicas.aspx.cs" Inherits="wappKaraoke.Cadastros.CadastroMusicas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" >
        function PegaNomeArquivoCantado() {
            document.getElementById('<%=deCaminhoMusica.ClientID%>').value = document.getElementById('<%=fluArquivoCantado.ClientID%>').value;
            __doPostBack('fluArquivoCantado', 'CarregaMusicaCantado;' + document.getElementById('<%=deCaminhoMusica.ClientID%>').value);
        }
        function PegaNomeArquivoKaraoke() {
            document.getElementById('<%=deCaminhoMusicaKaraoke.ClientID%>').value = document.getElementById('<%=fluArquivoKaraoke.ClientID%>').value;
            __doPostBack('fluArquivoKaraoke', 'CarregaMusicaKaraoke;' + document.getElementById('<%=deCaminhoMusicaKaraoke.ClientID%>').value);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="panel-title">Cadastro de Músicas</span>
                </div>
                <div class="panel-body">
                    <asp:Literal ID="ltMensagem" runat="server"></asp:Literal>
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:TextBox ID="nmMusica" class="form-control" runat="server" 
                                placeholder="Nome da Música..." Visible="True">
                            </asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:TextBox ID="nmMusicaKanji" class="form-control" runat="server" 
                                placeholder="Nome da Música em Kanji..." Visible="True">
                           </asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-2">
                           <asp:TextBox ID="nuAnoLanc" class="form-control" runat="server" 
                                placeholder="Ex. 2003" Visible="True">
                           </asp:TextBox>
                        </div>
                        <div class="col-sm-10">
                           <asp:TextBox ID="nmCantor" class="form-control" runat="server" 
                                placeholder="Nome do Cantor..." Visible="True">
                           </asp:TextBox> 
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-2">
                            <span class="btn btn-default btn-file">
                                Selecionar Cantado...
                                <asp:FileUpload ID="fluArquivoCantado" 
                                runat="server" 
                                onchange="PegaNomeArquivoCantado()"/>
                            </span>                                
                        </div>
                        <div class="col-sm-8">
                            <asp:TextBox ID="deCaminhoMusica" class="form-control" runat="server" 
                                placeholder="Música Cantada..." Visible="True"
                                ReadOnly="true">
                            </asp:TextBox>
                        </div>
                        <div class="col-sm-1">
                            <asp:Literal ID="ltAudioCantado" runat="server"></asp:Literal>
                            <asp:LinkButton ID="btnPlayCantado" 
                                        runat="server" 
                                        CssClass="btn btn-primary btn-block btn-primary "
                                        OnClientClick="document.getElementById('AudioCantado').play(); return false;">
                                &nbsp;&nbsp;<i aria-hidden="true" class="glyphicon glyphicon-play"></i>&nbsp;&nbsp;
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-1">
                            <asp:LinkButton ID="btnPauseCantado" 
                                        runat="server" 
                                        CssClass="btn btn-primary btn-block btn-primary "
                                        OnClientClick="document.getElementById('AudioCantado').pause(); return false;">
                                &nbsp;&nbsp;<i aria-hidden="true" class="glyphicon glyphicon-pause"></i>&nbsp;&nbsp;
                            </asp:LinkButton>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-2">
                            <span class="btn btn-default btn-file">
                                Selecionar Karaoke...
                                <asp:FileUpload ID="fluArquivoKaraoke" 
                                runat="server" 
                                onchange="PegaNomeArquivoKaraoke()"/>
                            </span>                                                    
                        </div>
                        <div class="col-sm-8">
                            <asp:TextBox ID="deCaminhoMusicaKaraoke" class="form-control" runat="server" 
                                placeholder="Música do Karaokê..." Visible="True"
                                ReadOnly="true">
                            </asp:TextBox>
                        </div>
                        <div class="col-sm-1">
                            <asp:Literal ID="ltAudioKaraoke" runat="server"></asp:Literal>
                            <asp:LinkButton ID="btnPlayKaraoke" 
                                        runat="server" 
                                        CssClass="btn btn-primary btn-block btn-primary "
                                        onclientclick="document.getElementById('AudioKaraoke').play(); return false;">
                                &nbsp;&nbsp;<i aria-hidden="true" class="glyphicon glyphicon-play"></i>&nbsp;&nbsp;
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-1">
                            <asp:LinkButton ID="btnPauseKaraoke" 
                                        runat="server" 
                                        CssClass="btn btn-primary btn-block btn-primary "
                                        OnClientClick="document.getElementById('AudioKaraoke').pause(); return false;">
                                &nbsp;&nbsp;<i aria-hidden="true" class="glyphicon glyphicon-pause"></i>&nbsp;&nbsp;
                            </asp:LinkButton>
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
