<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page/mpKaraoke.Master" AutoEventWireup="true" CodeBehind="InserirCantorConcursoCorrente.aspx.cs" Inherits="wappKaraoke.Movimentacoes.InserirCantorConcursoCorrente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $('[id*=gvCantores]').footable();
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="panel-title">Inserir Cantor no Concurso Corrente</span>
                </div>
                <div class="panel-body">
                    <asp:Literal ID="ltMensagem" runat="server"></asp:Literal>
                    <div class="row">
                        <div class="col-sm-6">
                            <asp:DropDownList ID="cdFase" class="form-control selectpicker" style="text-align:left" 
                                runat="server" Width="100%" AutoPostBack="True" 
                                onselectedindexchanged="cdFase_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="cdCategoria" class="form-control selectpicker" style="text-align:left" 
                                runat="server" Width="100%" AutoPostBack="True" 
                                onselectedindexchanged="cdCategoria_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-6">
                            <asp:DropDownList ID="cdCantor" class="form-control selectpicker" style="text-align:left" 
                                runat="server" Width="100%" AutoPostBack="True" 
                                onselectedindexchanged="cdCantor_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="cdMusica" class="form-control selectpicker" style="text-align:left" 
                                runat="server" Width="100%" AutoPostBack="True">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-4">
                            <asp:DropDownList ID="cdAssociacao" class="form-control selectpicker" style="text-align:left" 
                                runat="server" Width="100%" AutoPostBack="True">
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-4">
                            <asp:DropDownList ID="cdTpStatus" class="form-control selectpicker" style="text-align:left" 
                                runat="server" Width="100%" AutoPostBack="True">
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-2">
                            <asp:TextBox ID="nuCantor" class="form-control" runat="server" 
                                placeholder="Nº do Cantor..." Visible="True" 
                                ontextchanged="nuCantor_TextChanged" AutoPostBack="True"></asp:TextBox>
                        </div>
                        <div class="col-sm-2">
                            <asp:LinkButton ID="btnAdicionar" 
                                    runat="server" 
                                    CssClass="btn btn-primary btn-block btn-success" 
                                onclick="btnAdicionar_Click">
                                <i aria-hidden="true" class="glyphicon glyphicon-plus"></i>&nbsp;&nbsp;Adicionar
                            </asp:LinkButton>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:GridView ID="gvCantores" runat="server"
                                CssClass="footable table table-bordered table-hover" 
                                AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField HeaderText="Ordem Apres." DataField="nuOrdemApresentacao">
                                        <ItemStyle Width="5%" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Nº Cantor" DataField="nuCantor">
                                        <ItemStyle Width="5%" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Associação" DataField="nmAssociacao">
                                        <ItemStyle Width="10%" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Nome - Kanji">
                                        <ItemTemplate>
                                            <asp:Literal ID="ltNomeKanji" runat="server">
                                            </asp:Literal>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Música - Kanji">
                                        <ItemTemplate>
                                            <asp:Literal ID="ltMusicaKanji" runat="server">
                                            </asp:Literal>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="info" />
                            </asp:GridView>                          
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
