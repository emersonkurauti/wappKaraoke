<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page/mpKaraoke.Master" AutoEventWireup="true" CodeBehind="ConsultaConcursos.aspx.cs" Inherits="wappKaraoke.Cadastros.ConsultaConcursos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="panel-title">Consulta de Concurso</span>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    <asp:TextBox ID="cdConcurso" class="form-control" runat="server" 
                                        placeholder="Cód. Concurso..." Visible="True">
                                    </asp:TextBox>
                                </div>
                                <div class="col-md-5">
                                    <asp:TextBox ID="nmConcurso" class="form-control" runat="server" 
                                        placeholder="Nome do Concurso..." Visible="True">
                                    </asp:TextBox>
                                </div>
                                <div class="col-md-5">
                                    <asp:TextBox ID="nmConcursoKanji" class="form-control" runat="server" 
                                        placeholder="Nome do Concurso Kanji..." Visible="True">
                                    </asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    <div class='input-group date' id='datetimepicker1'>
                                        <asp:TextBox ID="dtConsurso" runat="server" class="form-control"
                                            placeholder="Ex. 13/01/2015" Visible="True">
                                        </asp:TextBox>
                                        <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-md-10">
                                    <asp:DropDownList ID="cdCidade" class="form-control selectpicker" style="text-align:left" 
                                        runat="server" Width="100%" AutoPostBack="False">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-8">
                                </div>
                                <div class="col-md-2">
                                    <asp:LinkButton ID="btnBuscar" 
                                                runat="server" 
                                                CssClass="btn btn-primary btn-block btn-info">
                                        <i aria-hidden="true" class="glyphicon glyphicon-search"></i>&nbsp;&nbsp;Buscar
                                    </asp:LinkButton>
                                </div>
                                <div class="col-md-2">
                                    <asp:LinkButton ID="btnNovo1" 
                                                runat="server" 
                                                CssClass="btn btn-primary btn-block btn-success">
                                        <i aria-hidden="true" class="glyphicon glyphicon-plus"></i>&nbsp;&nbsp;Novo Concurso
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br/>
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:GridView ID="gvDados" runat="server"
                                CssClass="footable table table-bordered table-hover" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField HeaderText="Cód." DataField="cdConcurso">
                                        <ItemStyle Width="5%" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Nome - Kanji">
                                        <ItemTemplate>
                                            <asp:Literal ID="ltNomeKanji" runat="server">
                                            </asp:Literal>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Nome" DataField="nmConcurso" Visible="false">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Kanji" DataField="nmConcursoKanji" Visible="false">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Dt. Concurso" DataField="dtConcurso">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Cidade" DataField="cdCidade">
                                    </asp:BoundField>
                                    <asp:CommandField ButtonType="Button" SelectText="Editar" 
                                        ShowSelectButton="True">
                                        <ControlStyle CssClass="btn btn-primary btn-block" />
                                        <ItemStyle Width="15%" />
                                    </asp:CommandField>
                                    <asp:CommandField ButtonType="Button" SelectText="Excluir" 
                                        ShowSelectButton="True">
                                        <ControlStyle CssClass="btn btn-primary btn-block btn-danger" />
                                        <ItemStyle Width="15%" />
                                    </asp:CommandField>
                                </Columns>
                                <HeaderStyle CssClass="info" />
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-5">
                        </div>
                        <div class="col-sm-5">
                        </div>
                        <div class="col-sm-2">
                            <asp:LinkButton ID="btnNovo" 
                                    runat="server" 
                                    CssClass="btn btn-primary btn-block btn-success">
                            <i aria-hidden="true" class="glyphicon glyphicon-plus"></i>&nbsp;&nbsp;Novo Concurso
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
