<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master" StylesheetTheme="Teacher" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Teacher_index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <div  class="placehold">
    <center>
        <br />
        <br />
        <br />
        <br />
        <br />
        <div  class="indexdiv">
            <div  class="phead">
                教师登录</div>
            <br />
            <br />
            <br />
                    账号：<asp:TextBox ID="Textname" runat="server"  Width="120px"  SkinID="TextBoxNormal"></asp:TextBox>
                    <br />
                    <br />
                    密码：<asp:TextBox ID="Textpwd" runat="server"  TextMode="Password" Width="120px" SkinID="TextBoxNormal"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Label ID="Labelmsg" runat="server" SkinID="LabelMsgRed" Width="178px"></asp:Label>
                    <br />
                    <br />
                    <asp:Button ID="Btnlogin" runat="server"  Text="登录"  SkinID="BtnNormal" onclick="Btnlogin_Click" />
                    </div>
        <br />
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
</script>
        <br />
        </center>
</div>
</asp:Content>

