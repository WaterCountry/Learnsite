<%@ page language="C#" autoeventwireup="true" inherits="python_matchrank, LearnSite" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <style type="text/css">
*
{
    margin-right: 0px;
    margin-left: 0px;
}
    </style>
</head>
<body>
<form id="form1" runat="server">
<div>
<center>
<br />
<asp:Label ID="Labeltitle" runat="server" Font-Bold="True" Font-Size="12pt" ></asp:Label>
                        <br />
                        <br /> 
                        <asp:GridView ID="GridViewclass" runat="server" 
                            AutoGenerateColumns="False"  onrowdatabound="GridViewclass_RowDataBound" 
                            Width="800px" TabIndex="1" Font-Names="Arial"  Font-Size="11pt" HorizontalAlign="Center" EnableModelValidation="True">
                            <Columns>
                                <asp:BoundField HeaderText="序号" />
                                <asp:BoundField DataField="Sgrade" HeaderText="年级" />
                                <asp:BoundField DataField="Sclass" HeaderText="班级" />
                                <asp:BoundField DataField="Sname" HeaderText="姓名" />
                                <asp:BoundField DataField="Scores" HeaderText="积分" />
                            </Columns>  
                        </asp:GridView>
                <br />
             <asp:Button ID="Btnreturn" runat="server" onclick="Btnreturn_Click" 
             SkinID="BtnNormal" Text="返回" />

</center>
</div>
</form>
</body>
</html>
