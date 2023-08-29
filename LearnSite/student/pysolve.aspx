<%@ page language="C#" autoeventwireup="true" inherits="Student_pysolve, LearnSite" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
        <asp:Label ID="Labeltitle" runat="server" Font-Bold="True" ></asp:Label>
        <br />
        <br />   
        <div style="text-align: center; margin:auto;">     
        <asp:GridView ID="GVsolve" runat="server" Font-Size="11pt" 
            HorizontalAlign="Center">
        </asp:GridView>
        </div>
    </div>
    </form>
</body>
</html>
