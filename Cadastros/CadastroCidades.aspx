﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page/mpKaraoke.Master" AutoEventWireup="true" CodeBehind="CadastroCidades.aspx.cs" Inherits="wappKaraoke.Cadastros.CadastroCidades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="panel-title">Cadastro de Cidades</span>
                </div>
                <div class="panel-body">
                    <asp:Literal ID="ltMensagem" runat="server"></asp:Literal>
                    <div class="row">
                        <div class="col-sm-10">
                            <asp:TextBox ID="nmCidade" class="form-control" runat="server" 
                                placeholder="Nome da Cidade..." Visible="True">
                            </asp:TextBox>
                        </div>
                        <div class="col-sm-2">
                            <asp:DropDownList ID="deUF" class="form-control selectpicker" style="text-align:left" 
                                runat="server" Width="100%" AutoPostBack="false">
                            </asp:DropDownList>
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
