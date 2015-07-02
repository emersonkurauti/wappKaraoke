<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="wappKaraoke.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">
    <title>Login</title>
    <link href="css/bootstrap.min.css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <div class="page-header">
        </div>
        <div class="container theme-showcase" role="main">
            <div class="row">
                <div class="col-sm-4">
                </div>
                <div class="col-sm-4">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">Entrar</h3>
                        </div>
                        <div class="panel-body">
                            <div class="input-group col-sm-12">
                                <span class="input-group-addon"><span class="glyphicon glyphicon-user"></span></span>
                                <asp:TextBox ID="txtUsuario" class="form-control" runat="server" 
                                    placeholder="Usuário" required autofocus></asp:TextBox>
                            </div>
                            <br />
                            <div class="input-group col-sm-12">
                                <span class="input-group-addon"><span class="glyphicon glyphicon-lock"></span></span>
                                <asp:TextBox ID="txtSenha" class="form-control" runat="server" 
                                    placeholder="Senha" required TextMode="Password"></asp:TextBox>
                            </div>
                            <br />
                            <div class="input-group col-sm-12">
                                <asp:LinkButton ID="btnEntrar" runat="server" 
                                    CssClass="btn btn-lg btn-primary btn-block">
                                    Entrar&nbsp;&nbsp;<i aria-hidden="true" class="glyphicon glyphicon-log-in"></i>
                                </asp:LinkButton>
                            </div>
                            <br />
                            <div class="input-group col-sm-12">
                                <asp:Literal ID="ltMensagem" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-4">
                </div>
            </div>
        </div>
    </form>
</body>
</html>
