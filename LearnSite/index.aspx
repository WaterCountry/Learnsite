<%@ page title="" language="C#" stylesheettheme="Student" autoeventwireup="true" inherits="index, LearnSite" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>信息科技学习网站</title>   
    <link href="../App_Themes/Student/StyleSheet.css" rel="stylesheet" type="text/css" />
    </head>
<body class="ground">
    <form id="form1" runat="server">
    <center>
      <div  class="studmasterhead">
            <div  class="banner" >
                </div>
        <div class="placeauto" >
        <center>          
         <div id="student">               
        <div >
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <div  class="indexdiv">
            <div  class="indexhead"> 登录窗口</div>
                <div class="indexlogin">
                <br />                
            <br />
            学号：<asp:TextBox 
                        ID="TextBoxuser" runat="server"  
                        Width="120px" SkinID="TextBox" class="textbox"
                        EnableViewState="False"></asp:TextBox>
                    <br />
            <br />
            密码：<asp:TextBox ID="TextBoxpwd" runat="server"  Width="120px" 
                        TextMode="Password" SkinID="TextBox" class="textbox" EnableViewState="False" AutoCompleteType="Disabled"></asp:TextBox>
                    <br />
            <br /> 
                    <asp:Button ID="Btnlogin" runat="server"  OnClick="Btnlogin_Click" Text="登录" 
                        BorderStyle="None" CssClass="buttonimg" />
            <br />                   
            <br />
            </div>
        </div>
        <br />
        <asp:Label ID="Labelmsg" runat="server"  SkinID="LabelMsgRed" ></asp:Label>
        <br />
        <br />
        <br />
        <div  class="indexsql">        
                    <asp:HyperLink ID="HyperLinkReg" runat="server" 
                        NavigateUrl="~/student/register.aspx" SkinID="HyperLink" Target="_self" 
                        CssClass="buttonimg" Width="80px" Height="24px" >学员注册</asp:HyperLink>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:HyperLink ID="HyperLinkSnum" runat="server" 
                        NavigateUrl="~/student/mynum.aspx" SkinID="HyperLink" Target="_self" 
                        CssClass="buttonimg" Width="80px" Height="24px" >学号查询</asp:HyperLink>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:HyperLink ID="HyperLinkrule" runat="server" 
                        NavigateUrl="~/student/myrule.aspx" SkinID="HyperLink" 
                        CssClass="buttonimg" Width="80px" Height="24px" >课堂守则</asp:HyperLink>
                </div>
        <br />       
        <br />
        <br />
        <div  class="foot">
        &nbsp;<asp:Label ID="Labelversion" runat="server"  Font-Size="9pt"></asp:Label>
            <asp:HyperLink ID="HLTeacher" runat="server" 
                NavigateUrl="~/teacher/index.aspx"  Target="_blank" EnableTheming="True" 
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