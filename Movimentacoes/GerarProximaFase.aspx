<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page/mpKaraoke.Master" AutoEventWireup="true" CodeBehind="GerarProximaFase.aspx.cs" Inherits="wappKaraoke.Movimentacoes.GerarProximaFase" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function AtivaEdicaoCantor(strIdCategoria) {
            document.getElementById('divEdicaoCantor').setAttribute('style', 'display: block;');
        }

        function DesativaEdicaoCantor() {
            document.getElementById('divEdicaoCantor').setAttribute('style', 'display: none;');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="panel-title">Gerar Próxima Fase</span>
                </div><!--Panel Header-->
                <div class="panel-body">
                    <asp:Literal ID="ltMensagem" runat="server"></asp:Literal>
                    <div class="row">
                        <div class="col-sm-6">
                            <asp:DropDownList ID="cdFase" class="form-control selectpicker" style="text-align:left" 
                                runat="server" Width="100%" AutoPostBack="True" 
                                onselectedindexchanged="cdFase_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-3">
                            <asp:LinkButton ID="btnSortearOrdemApresentacao" 
                                runat="server" 
                                CssClass="btn btn-warning btn-block"
                                OnClick="btnlnkSortearOrdemApresentacao_OnClick">
                                <i class="glyphicon glyphicon-refresh"></i>&nbsp;&nbsp;Sorteio
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-3">
                            <asp:LinkButton ID="btnFinalizarGeracao" 
                                runat="server" 
                                CssClass="btn btn-success btn-block"
                                OnClick="btnFinalizarGeracao_OnClick">
                                <i class="glyphicon glyphicon-ok"></i>&nbsp;&nbsp;Salvar
                            </asp:LinkButton>
                        </div>
                    </div>
                    <br/>
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:Literal ID="ltCategorias" runat="server"></asp:Literal>
                        </div>
                    </div> <!--<div class="row">-->

                    <link href="../css/blueimp-gallery.min.css" rel="stylesheet" type="text/css" />
                    <link href="../css/bootstrap-image-gallery.css" rel="stylesheet" type="text/css" />
                    <script src="../js/jquery.blueimp-gallery.min.js" type="text/javascript"></script>
                    <script src="../js/bootstrap-image-gallery.js" type="text/javascript"></script>


                    <div id="divEdicaoCantor" class="blueimp-gallery blueimp-gallery-display blueimp-gallery-left" style="display: none;">
                        <div class="slides" style="width: 2732px;">
                            <div class="slide" data-index="0" style="width: 1366px; left: 0px; transition-duration: 0ms; 
                                transform: translate(0px, 0px) translateZ(0px);">
                                <div class="modal fade slide-content in" style="display: block; overflow-y: scroll;">
                                    <div class="modal-dialog">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <h4 class="modal-title">
                                                    <asp:Literal ID="ltTituloEdicaoCantor" runat="server"></asp:Literal>
                                                </h4>
                                                <br/>
                                                <div class="panel panel-default">
                                                    <div class="panel-body">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <asp:Literal id="ltMensagemEdicaoCantor" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-6">
                                                                <asp:TextBox ID="nuOrdemApresentacaoEdit" class="form-control" 
                                                                    runat="server"
                                                                    placeholder="Odem Apresentação..." Visible="True">
                                                                </asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <asp:TextBox ID="nuCantorEdit" class="form-control" 
                                                                    runat="server" ReadOnly="true"
                                                                    placeholder="Nº Cantor..." Visible="True">
                                                                </asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <asp:DropDownList ID="cdAssociacaoEdit" class="form-control selectpicker disabled" 
                                                                    style="text-align:left" 
                                                                    runat="server" Width="100%" AutoPostBack="False"
                                                                    ReadOnly="true"
                                                                    Enabled="false">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <asp:DropDownList ID="cdMusicaEdit" class="form-control selectpicker" style="text-align:left" 
                                                                    runat="server" Width="100%" AutoPostBack="False">
                                                                </asp:DropDownList>
                                                            </div>        
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="modal-footer">
                                                <div class="row">
                                                    <div class="col-sm-2" align="left" style="float: left">
                                                        <asp:LinkButton ID="btnConfirmarEdicaoCantor" runat="server"
                                                            CssClass="btn btn-primary btn-block" 
                                                            OnClick="btnConfirmarEdicaoCantor_Click">
                                                            <i class="glyphicon glyphicon-save"></i>
                                                            Confirmar
                                                        </asp:LinkButton>
                                                    </div>
                                                    <div class="col-sm-2" align="center" style="float: left">
                                                        <button class="btn btn-primary btn-block btn-danger" 
                                                            type="button"
                                                            onClick="javascript:DesativaEdicaoCantor(); return false;">
                                                            <i class="glyphicon glyphicon-remove"></i>
                                                            Cancelar
                                                        </button>
                                                    </div>
                                                    <div class="col-sm-8" align="left" style="float: right">
                                                    </div>                        
                                                </div>        
                                                <div class="row">
                                                    <div class="col-sm-12" align="left">
                                                        <asp:Literal ID="ltMensagemInfoCantor" runat="server"></asp:Literal>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal fade"></div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
