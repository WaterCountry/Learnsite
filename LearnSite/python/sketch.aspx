﻿<%@ page language="C#" autoeventwireup="true" inherits="Python_sketch, LearnSite" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Python 绘画编程</title>
    <link href="../js/bootstrap.min.css" rel="stylesheet" type="text/css" />
	<style>
		body{			
			margin:10px;
		}
		.divthumb{
			width:220px;
			height:220px;
			text-align:center;
		}
		img{
			max-height:160px; max-width:160px;
		}
		img:hover{			
			background-color: #fef8de;
		}
	</style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container ">	
    <div class="row">
        <div class="col-md-8">
            <a class="btn btn-default" href="index.aspx" >首页</a>&nbsp;&nbsp;	          
            <a class="btn btn-default" href="match.aspx" >比赛</a>&nbsp;&nbsp; 
        </div>
        <div class="col-md-4">           
        </div>
    </div>
            <hr />
    <div class="row">
        <asp:DataList ID="DlTurtle" runat="server"  RepeatColumns="5"   CellPadding="20" CellSpacing="20" RepeatDirection="Horizontal" >
        <ItemTemplate>
			<div class="divthumb">
            <a href='<%# Eval("Tpage") %>' >
			<div >
			  <%# Container.ItemIndex+1%>.<%# Eval("Ttilte") %>
			</div>
                    <img src='<%# Eval("Turl") %>'  alt=" " />
			<div>
            </a>
        </ItemTemplate>
        </asp:DataList>
    </div>
    </div>
    </form>
    <script type="text/javascript">
        function returnurl() {
            window.history.back(-1);
        }

</script>
</body>
</html>