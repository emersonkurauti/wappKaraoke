<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page/mpKaraoke.Master" AutoEventWireup="true" CodeBehind="CantoresFases.aspx.cs" Inherits="wappKaraoke.Movimentacoes.CantoresFases" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $('[id*=gvFase]').footable();
            $('[id*=gvProxFase]').footable();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="panel-title">Cantores Fases</span>
                </div><!--Panel Header-->
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:Literal ID="ltMensagem" runat="server"></asp:Literal>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:DropDownList ID="cdCategoria" class="form-control selectpicker" style="text-align:left" 
                                runat="server" Width="100%" AutoPostBack="True" 
                                onselectedindexchanged="cdCategoria_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <br/>
                    <div class="row">
                        <div class="col-sm-5">
                            <asp:DropDownList ID="cdFase" class="form-control selectpicker" style="text-align:left" 
                                runat="server" Width="100%" AutoPostBack="True" 
                                onselectedindexchanged="cdFase_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-2">
                            <h4 style="text-align: center">PARA >></h4>
                        </div>
                        <div class="col-sm-5">
                            <asp:DropDownList ID="cdProxFase" class="form-control selectpicker" style="text-align:left" 
                                runat="server" Width="100%" AutoPostBack="True" 
                                onselectedindexchanged="cdProxFase_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-5">
                            <!--Grid 1-->
                            <asp:GridView ID="gvFase" runat="server"
                                CssClass="footable table table-bordered table-hover footable" 
                                AutoGenerateColumns="False">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkRow" runat="server" Width="5%"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Nome" DataField="nmCantor">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nuNotaFinal" HeaderText="Nota" />
                                    <asp:BoundField DataField="pcDesconto" HeaderText="% Desconto" />
                                </Columns>
                                <HeaderStyle CssClass="info" />
                            </asp:GridView>
                        </div>
                        <div class="col-sm-2">
                            <!--Botões-->
                            <br/>
                            <asp:LinkButton ID="btnProxima" 
                                        runat="server" 
                                        CssClass="btn btn-primary btn-block btn-success" 
                                onclick="btnProxima_Click">
                                Passar&nbsp;&nbsp;<i aria-hidden="true" class="glyphicon glyphicon-arrow-right"></i>
                            </asp:LinkButton>
                            <!--<br/>
                            <asp:LinkButton ID="btnAnterior" 
                                        runat="server" 
                                        CssClass="btn btn-primary btn-block btn-success " 
                                onclick="btnAnterior_Click">
                                <i aria-hidden="true" class="glyphicon glyphicon-arrow-left"></i>&nbsp;&nbsp;Voltar
                            </asp:LinkButton>-->
                            <br/>
                        </div>
                        <div class="col-sm-5">
                            <!--Grid 2-->
                            <asp:GridView ID="gvProxFase" runat="server"
                                CssClass="footable table table-bordered table-hover footable" 
                                AutoGenerateColumns="False">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkRow" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Nome" DataField="nmCantor">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nuNotaFinal" HeaderText="Nota" />
                                    <asp:BoundField DataField="pcDesconto" HeaderText="% Desconto" />
                                </Columns>
                                <HeaderStyle CssClass="info" />
                            </asp:GridView>
                        </div>
                    </div>
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
