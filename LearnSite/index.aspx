<%@ Page Title="" Language="C#" StylesheetTheme="Student" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>信息技术学习平台</title>   
    <link href="../App_Themes/Student/StyleSheet.css" rel="stylesheet" type="text/css" />
    </head>
<body>
    <form id="form1" runat="server">
    <center>
      <div  class="studmasterhead">
            <div  class="banner" >
                <script src="Js/road.js" type="text/javascript"></script>
      	<script type="text/javascript" >
      	    var first = "";
      	    ShowRoad(first);
         </script> 
                </div>
            <center>
            <div  class="menu">            
            </div>
            </center>
        <div class="placeauto" >
        <center>          
         <div id="student">               
        <div class="bg">
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <div  class="indexdiv">
            <div  class="indexheader"> 登录窗口</div>
                <div class="indexlogin">
                <br />                
            <br />
            学号：<asp:TextBox 
                        ID="TextBoxuser" runat="server"  
                        Width="120px" SkinID="TextBox" BorderColor="#C8C8C8" BackColor="#FCECC2" 
                        EnableViewState="False"></asp:TextBox>
                    <br />
            <br />
            密码：<asp:TextBox ID="TextBoxpwd" runat="server"  Width="120px" 
                        TextMode="Password" SkinID="TextBox" BorderColor="#C8C8C8" 
                        BackColor="#FCECC2" EnableViewState="False" AutoCompleteType="Disabled"></asp:TextBox>
                    <br />
            <br />
        <asp:Label ID="Labelmsg" runat="server"  SkinID="LabelMsgRed" ></asp:Label>
                    <br />
            <br />
                    <asp:Button ID="Btnlogin" runat="server"  OnClick="Btnlogin_Click" Text="登录" 
                        BorderStyle="None" CssClass="buttonimg" />
            <br />            
            <br />
            </div>
        </div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <div  class="indexsql">        
                    <asp:HyperLink ID="HyperLinkReg" runat="server" 
                        NavigateUrl="~/Student/register.aspx" SkinID="HyperLink" Target="_self" 
                        CssClass="buttonimg" Width="80px" Height="18px" >学员注册</asp:HyperLink>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:HyperLink ID="HyperLinkSnum" runat="server" 
                        NavigateUrl="~/Student/mynum.aspx" SkinID="HyperLink" Target="_self" 
                        CssClass="buttonimg" Width="80px" Height="18px" >学号查询</asp:HyperLink>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:HyperLink ID="HyperLinkrule" runat="server" 
                        NavigateUrl="~/Student/myrule.aspx" SkinID="HyperLink" 
                        CssClass="buttonimg" Width="80px" Height="18px" >课堂守则</asp:HyperLink>
                </div>
        <br />       
        <br />
        <br />
        <div  class="foot">
        &nbsp;<asp:Label ID="Labelversion" runat="server"  Font-Size="9pt"></asp:Label>
            <asp:HyperLink ID="HLTeacher" runat="server" 
                NavigateUrl="~/Teacher/index.aspx"  Target="_blank" EnableTheming="True" 
                ForeColor="Black" >教师平台</asp:HyperLink>
&nbsp;当前IP:<asp:Label ID="Labelip" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
            &nbsp; 计算机名:<asp:Label ID="Labelhostname" runat="server" Font-Names="Arial" 
                Font-Size="9pt"></asp:Label>
            第<asp:Label ID="Labelterm" runat="server" Font-Names="Arial" 
                Font-Size="9pt"></asp:Label>
            学期<br />
            <br />
            <asp:Label ID="Labelloadtime" runat="server" Font-Italic="True" 
                Font-Size="8pt" ></asp:Label>
            <br />
            </div>
            </div>
<script type="text/javascript">
    function CookieEnable() {
        var result = false;
        if (navigator.cookiesEnabled)
            return true;
        document.cookie = "testcookie=yes;";
        var cookieSet = document.cookie;
        if (cookieSet.indexOf("testcookie=yes") > -1)
            result = true;
        document.cookie = "";
        return result;
    }
    if (!CookieEnable()) {
        alert("对不起，您的浏览器的Cookie功能被禁用，请开启\n\n 开启方法：IE---工具---Internet选项---隐私---中");
    }
    function doubleCheck() {
        if (window.document.readyState != null &&
            window.document.readyState != 'complete') {
            alert("正在处理，请等待！");
            return false;
        }
        else {
            return true;
        }
    }
</script>
        </div>
        </center>   
        </div>
        </div>
       </center>
    </form>
</body>
</html>