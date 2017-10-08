<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sitelog.aspx.cs" Inherits="Teacher_sitelog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>网站异常信息记录页面</title>
    <style type="text/css">
        body{font-size: 11pt; font-family: Arial;}
        .syl
        {
            padding: 2px; border-bottom-style: dashed; border-width: 1px; border-color: #CCCCCC;
        }
        .note
        { text-align:left; padding:2px; margin:10px;  color:Red;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center;">    
        <img alt="" src="../Images/inquiry.png" /><strong>网站运行异常信息记录列表</strong>：<br />
        <br />
&nbsp;<asp:Label ID="Labelmsg" runat="server"></asp:Label>
        <br />
        <div style="margin: auto; padding-left: 2px; width: 90%;">
        <asp:GridView ID="GvLog" runat="server" AutoGenerateColumns="False" 
            EnableModelValidation="True" GridLines="None" Width="100%" 
                BorderColor="#CCCCCC" BorderStyle="Solid" 
                BorderWidth="1px" >
            <Columns>
                <asp:BoundField DataField="Fid" >
                <ItemStyle Width="30px" />
                </asp:BoundField>
                <asp:BoundField DataField="Fdate" HeaderText="日期" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="120px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="内容">
                    <ItemTemplate>
                    <div class="note" >
                        <asp:Literal ID="Literalnote" runat="server" Text='<%# Bind("Fnote") %>'  ></asp:Literal>
                     </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle BackColor="#EEEEEE" Height="20px" />
            <RowStyle BorderColor="Silver" BorderStyle="Dashed" BorderWidth="1px" 
                Height="20px" />
        </asp:GridView>
        </div>
        <br />
        说明：异常信息记录保存在网站Log目录<br />
    </div>
    </form>
</body>
</html>
