<%@ page language="C#" autoeventwireup="true" inherits="python_matchnew, LearnSite" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>创建</title>
        <link href="../js/bootstrap.min.css" rel="stylesheet" type="text/css" />
	<style>
		body{			
			margin:10px;
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

       <div style=" text-align:center;" >
            <br />
            <br />
                比赛名称：<asp:TextBox ID="Texttitle" runat="server" Width="580px"  SkinID="TextBoxNormal"></asp:TextBox>
            &nbsp;<asp:CheckBox ID="Checkcpublish" runat="server" Text="是否发布"  Checked="True" />
            <br />
            <br />
            <br />
            <br />
            <br />
            <asp:Button ID="BtnCreate" runat="server"  Text="确定"  onclick="BtnCreate_Click"  SkinID="BtnNormal"/>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
            <asp:Button ID="Btnreturn" runat="server"  Text="返回" onclick="Btnreturn_Click" SkinID="BtnNormal" />
            <br />
            <br />
        </div>
       </div>
    </div>
    </form>
</body>
</html>
