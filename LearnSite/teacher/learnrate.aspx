<%@ page language="C#" autoeventwireup="true" inherits="teacher_learnrate, LearnSite" %>

<%@ Register assembly="Anthem" namespace="Anthem" tagprefix="anthem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="text-align: center;">        
            <asp:Label ID="LabelGradeClass" runat="server" Font-Size="11pt"></asp:Label>
            <asp:DropDownList ID="DDLCid" runat="server" Font-Names="Arial"  AutoPostBack="True" 
              onselectedindexchanged="DDLCid_SelectedIndexChanged">
          </asp:DropDownList>
            <asp:Label ID="Label1" runat="server"  Font-Size="11pt">学习进度</asp:Label>
            <div style="margin: auto; text-align: center">
            <center>
                <anthem:GridView ID="GridViewclass" runat="server" OnRowDataBound="GridViewclass_RowDataBound"
                    TabIndex="1" CellPadding="2" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None"
                    BorderWidth="2px" Font-Names="Arial"  HorizontalAlign="Center"
                    EnableModelValidation="True">
                    <RowStyle HorizontalAlign="Center" BorderStyle="None" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#305E9C" Font-Bold="True" ForeColor="White" />
                </anthem:GridView>
                <br />
                <asp:ImageButton ID="Btnreflash" runat="server" ImageUrl="~/images/none.gif" OnClick="Btnreflash_Click" />
                <br />
            </center>
            </div>
            <asp:Label ID="Labelmsg" runat="server"  style="font-size: xx-small" ForeColor="White"></asp:Label>
            
        <script type ="text/javascript" >
            function myrefresh() {
                document.getElementById("<%= Btnreflash.ClientID %>").click();
            }
            setTimeout("myrefresh()", 5000); //指定5秒刷新一次作品

        </script>
        </div>
    </div>
    </form>
</body>
</html>