<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page/mpKaraoke.Master" AutoEventWireup="true" CodeBehind="CadastroCantores.aspx.cs" Inherits="wappKaraoke.Cadastros.CadastroCantores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="panel-title">Cadastro de Cantores</span>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:TextBox ID="nmCantor" class="form-control" runat="server" 
                                placeholder="Nome do Cantor..." Visible="True">
                            </asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-12">
                           <asp:TextBox ID="nmNomeKanji" class="form-control" runat="server" 
                                placeholder="Nome do Cantor Kanji..." Visible="True">
                           </asp:TextBox> 
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-2">
                            <div class='input-group date' id='datetimepicker1'>
                                <asp:TextBox ID="dtNascimento" runat="server" class="form-control"
                                    placeholder="Ex. 13/01/2015" Visible="True">
                                </asp:TextBox>
                                <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>
                        <div class="col-sm-10">
                           <asp:TextBox ID="nmNomeArtistico" class="form-control" runat="server" 
                                placeholder="Nome Artísitico..." Visible="True">
                           </asp:TextBox> 
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-2">
                           <asp:TextBox ID="nuTelefone" class="form-control" runat="server" 
                                placeholder="Telefone..." Visible="True">
                           </asp:TextBox> 
                        </div>
                        <div class="col-sm-10">
                           <asp:TextBox ID="deEmail" class="form-control" runat="server" 
                                placeholder="E-Mail..." Visible="True">
                           </asp:TextBox> 
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-2">
                           <asp:TextBox ID="nuRG" class="form-control" runat="server" 
                                placeholder="RG..." Visible="True">
                           </asp:TextBox> 
                        </div>                        
                        <div class="col-sm-10">
                           <asp:DropDownList ID="cdCidade" class="form-control selectpicker" style="text-align:left" 
                                runat="server" Width="100%" AutoPostBack="True">
                            </asp:DropDownList>
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
