<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page/mpKaraoke.Master" AutoEventWireup="true" CodeBehind="PainelAcompanhamentoConcurso.aspx.cs" Inherits="wappKaraoke.Paineis.PainelAcompanhamentoConcurso" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<asp:Literal ID="ltRefresh" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="panel-title">Acompanhamento Concurso</span>
                </div><!--Panel Header-->
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:Literal ID="ltMensagem" runat="server"></asp:Literal>
                            <asp:GridView ID="gvAcompanhamentoConcurso" runat="server"
                                CssClass="footable table table-bordered table-hover">
                                <Columns>
                                    <asp:BoundField DataField="deCategoria" HeaderText="Categoria" />
                                    <asp:BoundField DataField="nuCantor" HeaderText="Nº Cantor" />
                                    <asp:BoundField DataField="nmAssociacao" HeaderText="Associação" />
                                    <asp:TemplateField HeaderText="Cantor">
                                        <ItemTemplate>
                                            <asp:Literal ID="ltCantorKanji" runat="server">
                                            </asp:Literal>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Música">
                                        <ItemTemplate>
                                            <asp:Literal ID="ltMusicaKanji" runat="server">
                                            </asp:Literal>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="deTpStatus" HeaderText="Status" />
                                </Columns>
                                <HeaderStyle CssClass="info" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
