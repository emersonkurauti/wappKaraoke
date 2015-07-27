<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page/mpKaraoke.Master" AutoEventWireup="true" CodeBehind="DefinirConcursoCorrente.aspx.cs" Inherits="wappKaraoke.Movimentacoes.DefinirConcursoCorrente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="panel-title">Definir Concurso Corrente</span>
                </div><!--Panel Header-->
                <div class="panel-body">
                    <div class="col-sm-12">
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
                                    runat="server" Width="100%" AutoPostBack="False" 
                                    onselectedindexchanged="cdConcurso_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-3">
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-3">
                            </div>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="cdFase" class="form-control selectpicker" style="text-align:left" 
                                    runat="server" Width="100%" AutoPostBack="False">
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-3">
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-3">
                            </div>
                            <div class="col-sm-6">
                                <asp:LinkButton ID="btnDefinirConcursoCorrente" 
                                        runat="server" 
                                        OnClick="btnDefinirConcursoCorrente_Click"
                                        CssClass="btn btn-primary btn-block btn-success">
                                    <i aria-hidden="true" class="glyphicon glyphicon-saved"></i>&nbsp;&nbsp;Definir como corrente
                                </asp:LinkButton>
                            </div>
                            <div class="col-sm-3">
                            </div>
                        </div>
                    </div>
                </div><!--Panel Body-->
            </div><!--Panel principal-->
        </div>
    </div>
</asp:Content>
