<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page/mpKaraoke.Master" AutoEventWireup="true" CodeBehind="NotasCantores.aspx.cs" Inherits="wappKaraoke.Movimentacoes.NotasCantores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function AtualizaNota() {
            var vNotas = document.getElementsByName('txtNota');
            var vObs = document.getElementsByName('txtObs');
            var hfNotas = '';
            var strNomeCampo = '';
            var strCodNota = '';

            for (i = 0; i < vNotas.length; i++) {
                strNomeCampo = vNotas[i].id;

                strCodNota = strNomeCampo.substring(strNomeCampo.indexOf('_') + 1, strNomeCampo.lenght);

                hfNotas += strCodNota + "=" + vNotas[i].value + "=" + vObs[i].value + ';';
            }

            document.getElementById('<%=hfNotas.ClientID%>').value = hfNotas;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="panel-title">Notas do Cantor</span>
                </div><!--Panel Header-->
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:Literal ID="ltMensagem" runat="server"></asp:Literal>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:TextBox ID="nuCantor" class="form-control" runat="server" 
                                placeholder="Número do Cantor..." Visible="True" AutoPostBack="True"
                                ontextchanged="nuCantor_TextChanged">
                            </asp:TextBox>
                        </div>
                    </div>
                    <br/>
                    <!--Notas-->
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:Literal ID="ltNotasJurados" runat="server"></asp:Literal>
                                    <asp:HiddenField ID="hfNotas" runat="server" />
                                </div>
                            </div><!--Fim Panel Body Notas-->
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4">
                            <asp:TextBox ID="deFormulaPontuacao" class="form-control" runat="server" 
                                placeholder="Fórmula..." Visible="True">
                            </asp:TextBox>
                        </div>
                        <div class="col-sm-4">
                            <asp:TextBox ID="pcDesconto" class="form-control" runat="server" 
                                placeholder="% Desconto..." Visible="True">
                            </asp:TextBox>
                        </div>
                        <div class="col-sm-4">
                            <asp:TextBox ID="nuNotaFinal" class="form-control" runat="server" 
                                placeholder="Nota Final..." Visible="True" ReadOnly="true">
                            </asp:TextBox>
                        </div>
                    </div>
                    <!--Fim Notas-->                    
                    <br />
                    <div class="row">
                        <div class="col-sm-4">
                            <asp:LinkButton ID="btnSalvar" 
                                runat="server" 
                                CssClass="btn btn-primary btn-block btn-success"
                                onclientclick="AtualizaNota();"
                                onclick="btnSalvar_Click">
                                <i aria-hidden="true" class="glyphicon glyphicon-save"></i>&nbsp;&nbsp;Salvar
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-4">
                           <asp:LinkButton ID="btnAtualizar" 
                                runat="server" 
                                CssClass="btn btn-primary btn-block btn-primary" 
                                onclick="btnAtualizar_Click">
                                <i aria-hidden="true" class="glyphicon glyphicon-refresh"></i>&nbsp;&nbsp;Atualizar
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-2">
                        </div>
                        <div class="col-sm-2">
                        </div>
                    </div>
                </div><!--Panel Body-->
            </div><!--Panel principal-->
        </div>
    </div>
</asp:Content>
