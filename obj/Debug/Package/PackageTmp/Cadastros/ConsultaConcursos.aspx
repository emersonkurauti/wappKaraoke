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
                                        placeholder="Cód. Concurso..." Visible="True"
                                        ontextchanged="TextChanged" AutoPostBack="True">
                                    </asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="nmConcurso" class="form-control" runat="server" 
                                        placeholder="Nome do Concurso..." Visible="True"
                                        ontextchanged="TextChanged" AutoPostBack="True">
                                    </asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox ID="nmConcursoKanji" class="form-control" runat="server" 
                                        placeholder="Nome do Concurso Kanji..." Visible="True"
                                        ontextchanged="TextChanged" AutoPostBack="True">
                                    </asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="input-group date">
                                        <asp:TextBox ID="dtIniConcurso" runat="server" class="form-control"
                                            placeholder="Ex. 01/01/2015" Visible="True" AutoPostBack="True" 
                                            ontextchanged="dtIniConcurso_TextChanged">
                                        </asp:TextBox>
                                        <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span>
                                            Dt. Início
                                        </span>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="input-group date">
                                        <asp:TextBox ID="dtFimConcurso" runat="server" class="form-control"
                                            placeholder="Ex. 01/01/2015" Visible="True" AutoPostBack="True" 
                                            ontextchanged="dtFimConcurso_TextChanged">
                                        </asp:TextBox>
                                        <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span>
                                            Dt. Fim&nbsp;&nbsp;&nbsp;
                                        </span>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cdCidade" class="form-control selectpicker" style="text-align:left" 
                                        runat="server" Width="100%"
                                        ontextchanged="TextChanged" AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                </div>
                                <div class="col-md-2">
                                    <asp:LinkButton ID="btnLimpar" 
                                                runat="server" 
                                                CssClass="btn btn-primary btn-block btn-warning"
                                                onclick="btnLimpar_Click">
                                        <i aria-hidden="true" class="glyphicon glyphicon-erase"></i>&nbsp;&nbsp;Limpar Filtro
                                    </asp:LinkButton>
                                </div>
                                <div class="col-md-2">
                                    <asp:LinkButton ID="btnBuscar" 
                                                runat="server" 
                                                CssClass="btn btn-primary btn-block btn-info"
                                                onclick="btnBuscar_Click">
                                        <i aria-hidden="true" class="glyphicon glyphicon-search"></i>&nbsp;&nbsp;Buscar
                                    </asp:LinkButton>
                                </div>
                                <div class="col-md-2">
                                    <asp:LinkButton ID="btnNovo1" 
                                                runat="server" 
                                                CssClass="btn btn-primary btn-block btn-success"
                                                onclick="btnNovo1_Click">
                                        <i aria-hidden="true" class="glyphicon glyphicon-plus"></i>&nbsp;&nbsp;Novo Concurso
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br/>
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:Literal ID="ltMensagem" runat="server"></asp:Literal>
                            <br />
                            <asp:GridView ID="gvDados" runat="server"
                                CssClass="footable table table-bordered table-hover"
                                AutoGenerateColumns="False"
                                OnRowDataBound="gvDados_RowDataBound" 
                                OnRowCommand="gvDados_RowCommand" 
                                onrowdeleting="gvDados_RowDeleting">
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
                                    <asp:BoundField HeaderText="Dt. Ini. Concurso" DataField="dtIniConcurso" 
                                        DataFormatString="{0:dd/MM/yyyy}">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Dt. Fim Concurso" DataField="dtFimConcurso"
                                        DataFormatString="{0:dd/MM/yyyy}">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Cidade" DataField="CC_nmCidade">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Finalizado?" DataField="flFinalizado">
                                    </asp:BoundField>
                                    <asp:TemplateField>
				                        <ItemTemplate>
					                        <asp:LinkButton ID="lnkEdit" runat="server"
						                        CssClass="btn btn-primary btn-block" Text = "Editar"
						                        CommandName='Edit'>
						                        <i aria-hidden="true" class="glyphicon glyphicon-edit"></i>
					                        </asp:LinkButton>
				                        </ItemTemplate>
				                        <ItemStyle Width="5%" />
			                        </asp:TemplateField>
			                        <asp:TemplateField>
				                        <ItemTemplate>
					                        <asp:LinkButton ID="lnkDelete" runat="server"
						                        CssClass="btn btn-primary btn-block btn-danger" Text = "Excluir"
						                        CommandArgument='<%# Eval("cdConcurso") + "$" + Eval("nmConcurso") %>'
						                        CommandName='Delete'>
						                        <i aria-hidden="true" class="glyphicon glyphicon-trash"></i>
					                        </asp:LinkButton>
				                        </ItemTemplate>
				                        <ItemStyle Width="5%" />
			                        </asp:TemplateField>
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
                                    CssClass="btn btn-primary btn-block btn-success"
                                    onclick="btnNovo1_Click">
                            <i aria-hidden="true" class="glyphicon glyphicon-plus"></i>&nbsp;&nbsp;Novo Concurso
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
