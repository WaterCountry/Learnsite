<%@ Page Language="C#" AutoEventWireup="true" CodeFile="en.aspx.cs" Inherits="en" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>指法练习——字典重新设置</title>
    <style type="text/css">
        .style1
        {
            color: #006666;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center;">    
        <br />
        <strong>指法练习——字典重新设置</strong><br />
        <br />
        <div style="margin: auto; border-width: 1px; border-color: #333333; font-size: 9pt; border-top-style: dashed; background-color: #EEF2F2; width: 500px;">
        <br />
        根目录下的en.xls中Elevel表示英语级别说明：<br />
            <br />
            0 表示小学英语<br />
            1 表示初中英语<br />
            2 表示高中英语<br />
            <br />
            <span class="style1">可以自己根据需要修改或添加en.xls中的Elevel值</span><br class="style1" />
            <span class="style1">并在myfinger.aspx中下拉列表框中做相应的级别值</span><br />
            <br />
            <br />
            <br />
            请替换网站根目录下的en.xls后，再按下面的按钮<br />
            <br />
            <br />
            <asp:Button ID="Buttonen" runat="server" BackColor="#EBEBEB" 
                BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Font-Size="9pt" 
                onclick="Buttonen_Click" Text="英文字典重新导入" ToolTip="将清空原字典，请导入新字典！" />
            <br />
            <br />
            <br />
            <asp:Label ID="Labelmsg" runat="server" ForeColor="Red"></asp:Label>
            <br />
            <br />
            <br />
            <br />
            <br />
            注意：本页面只能在服务器上浏览才能操作，以防误操作！<br />
            <br />
        <br />
    </div>
    </div>
    </form>
</body>
</html>
