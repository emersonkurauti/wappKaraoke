<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page/mpKaraoke.Master" AutoEventWireup="true" CodeBehind="PainelClassificacao.aspx.cs" Inherits="wappKaraoke.Paineis.PainelClassificacao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function AtivaAbaCantores(strIdCategoria) {
            document.getElementById('lnkDivCantores_' + strIdCategoria).click();
        }

        function AtivaEdicaoCantor(strIdCategoria) {
            document.getElementById('divEdicaoCantor').setAttribute('style', 'display: block;');
            AtivaAbaCantores(strIdCategoria);
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
                    <span class="panel-title">Painel de classificação</span>
                </div><!--Panel Header-->
                <div class="panel-body">
                    <asp:Literal ID="ltMensagem" runat="server"></asp:Literal>
                    <div class="row">
                        <div class="col-sm-10">
                            <asp:DropDownList ID="cdFase" class="form-control selectpicker" style="text-align:left" 
                                runat="server" Width="100%" AutoPostBack="True" 
                                onselectedindexchanged="cdFase_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-2">
                            <asp:LinkButton ID="btnAtualizar" 
                                runat="server" 
                                CssClass="btn btn-info btn-block" onclick="btnAtualizar_Click">
                                <i class="glyphicon glyphicon-refresh"></i>&nbsp;&nbsp;Atualizar
                            </asp:LinkButton>
                        </div>
                    </div>
                    <br/>
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:Literal ID="ltCategorias" runat="server"></asp:Literal>
                        </div>
                    </div> <!--<div class="row">-->
                </div>
            </div>
        </div>
    </div>
</asp:Content>
