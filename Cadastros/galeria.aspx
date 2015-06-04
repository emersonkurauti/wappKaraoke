<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="galeria.aspx.cs" Inherits="wappKaraoke.Cadastros.galeria" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html lang="en">
<head>
<!--[if IE]>
<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
<![endif]-->
<meta charset="utf-8">
<title>Bootstrap Image Gallery</title>
<meta name="description" content="Bootstrap Image Gallery is an extension to blueimp Gallery, a touch-enabled, responsive and customizable image and video gallery. It displays images and videos in the modal dialog of the Bootstrap framework, features swipe, mouse and keyboard navigation, transition effects, fullscreen support and on-demand content loading and can be extended to display additional content types.">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="../css/bootstrap-3-0-2-gallery.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/blueimp-gallery.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/bootstrap-image-gallery.css" rel="stylesheet" type="text/css" />
</head>
<body>
	<div class="navbar navbar-default navbar-fixed-top navbar-inverse">
	</div>
	<div class="container">
		<h1>Bootstrap Image Gallery Demo</h1>
		<form class="form-inline">
		<asp:ScriptManager ID="ScriptManager1"/>
		<br>
		<!-- The container for the list of example images -->
        <asp:UpdatePanel ID="upArquivos" UpdateMode="Conditional">
            <ContentTemplate>
		<div id="links"></div>
            <div class="row-fluid">
                <ul class="thumbnails">
					<li class="span4">
						<div class="thumbnail">
                            <a href="../assets/img/bootstrap-mdo-sfmoma-03.jpg" title="Teste" data-gallery="">
							    <img src="../assets/img/bootstrap-mdo-sfmoma-03.jpg" />
                            </a>
							<div class="caption">[
								<p>descrição da imagem</p>
								<div class="row">
									<div class="col-sm-6">
										<a href="../assets/img/bootstrap-mdo-sfmoma-03.jpg" title="Teste" 
                                            data-gallery="" Class="btn btn-primary btn-block">
											<i class="glyphicon glyphicon-zoom-in"></i>&nbsp;&nbsp;Ampliar
										</a>
									</div>
									<div class="col-sm-6">
										<asp:LinkButton ID="LinkButton2" 
												CssClass="btn btn-primary btn-block btn-danger">
											<i class="glyphicon glyphicon-trash"></i>&nbsp;&nbsp;Remover
										</asp:LinkButton>
									</div>
								</div>
							</div>
						</div>
					</li>
				</ul>
            </div>
		<br>
            </ContentTemplate>
        </asp:UpdatePanel>
        </form>
	</div>

	<!-- The Bootstrap Image Gallery lightbox, should be a child element of the document body -->
	<div id="blueimp-gallery" class="blueimp-gallery">
		<!-- The container for the modal slides -->
		<div class="slides"></div>
		<!-- Controls for the borderless lightbox -->
		<h3 class="title"></h3>
		<a class="prev">--</a>
		<a class="next">--</a>
		<a class="close">------</a>
		<a class="play-pause"></a>
		<ol class="indicator"></ol>
		<!-- The modal dialog, which will be used to wrap the lightbox content -->
		<div class="modal fade">
			<div class="modal-dialog">
				<div class="modal-content">
					<div class="modal-header">
						<button type="button" class="close" aria-hidden="true">&times;</button>
						<h4 class="modal-title"></h4>
					</div>
					<div class="modal-body next"></div>
					<div class="modal-footer">
						<button type="button" class="btn btn-default pull-left prev">
							<i class="glyphicon glyphicon-chevron-left"></i>
							Previous
						</button>
						<button type="button" class="btn btn-primary next">
							Next
							<i class="glyphicon glyphicon-chevron-right"></i>
						</button>
					</div>
				</div>
			</div>
		</div>
	</div>
        <script src="../js/jquery-10-0-2-gallery.min.js" type="text/javascript"></script>
        <script src="../js/jquery.blueimp-gallery.min.js" type="text/javascript"></script>
        <script src="../js/bootstrap-image-gallery.js" type="text/javascript"></script>
</body> 
</html>
