<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Research.aspx.cs" Inherits="Spec_Research" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="padding-top:0px;">
    <form id="form1" runat="server">
    <div id="researchlogo" 
        style="background-image: url('../Images/research.jpg'); background-repeat: no-repeat; height: 80px;">
    &nbsp;</div>
    <div id="researchform"  runat="server" style="text-align: center; background-color: #FFFFD7;">
         
        <br />
        <br />
       
        在一整天24小时的学习生活里，你在以下各方面所花费的时间占多少，请根据实际情况，如实完成调查
        ：<br />
    <ul>
    <li>学习时间：<asp:TextBox ID="TextBox1" runat="server" CausesValidation="True"></asp:TextBox>
        小时<asp:RangeValidator ID="RangeValidator1" runat="server" 
            ControlToValidate="TextBox1" ErrorMessage="学习时间大约在6到16小时之间" MaximumValue="16" 
            MinimumValue="6" Type="Double"></asp:RangeValidator>
        <br /><br />
    </li> 
    <li>锻炼时间：<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        小时<asp:RangeValidator ID="RangeValidator2" runat="server" 
            ControlToValidate="TextBox2" ErrorMessage="锻炼时间大约在1到 3小时之间" MaximumValue="3" 
            MinimumValue="1" Type="Double"></asp:RangeValidator>
        <br /><br />
        </li>
    <li>睡觉时间：<asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        小时<asp:RangeValidator ID="RangeValidator3" runat="server" 
            ControlToValidate="TextBox3" ErrorMessage="睡觉时间大约在5到10小时之间" MaximumValue="10" 
            MinimumValue="5" Type="Double"></asp:RangeValidator>
        <br /><br />
        </li>
    <li>空闲时间：<asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
        小时<asp:RangeValidator ID="RangeValidator4" runat="server" 
            ControlToValidate="TextBox4" ErrorMessage="空闲时间大约在1到 6小时之间" MaximumValue="6" 
            MinimumValue="1" Type="Double"></asp:RangeValidator>
        <br /><br />
        </li>
    </ul>
        <br />
        <asp:Button ID="Button1" runat="server" Font-Size="Medium" Text="提交" 
            Width="114px" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" 
            onclick="Button1_Click" />
    <br /><br />
    </div>
    <div id="researchgriw" style="text-align: center">
    调查列表:
        <br />
        <br />
        <asp:GridView ID="GVresearch" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" 
            CellPadding="3" EnableModelValidation="True" ForeColor="Black" 
            GridLines="Vertical" Width="600px" Font-Names="Arial" Font-Size="Medium" 
            HorizontalAlign="Center">
            <AlternatingRowStyle BackColor="#CCCCCC" />
            <Columns>
                <asp:BoundField DataField="Sname" HeaderText="姓名">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="学习时间" DataField="Rlearn" DataFormatString="{0:N1}" 
                    HtmlEncode="False" >
                <ItemStyle Height="24px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="锻炼时间" DataField="Rplay" DataFormatString="{0:N1}" 
                    HtmlEncode="False" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="睡觉时间" DataField="Rsleep" DataFormatString="{0:N1}" 
                    HtmlEncode="False" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="空闲时间" DataField="Rfree" DataFormatString="{0:N1}" 
                    HtmlEncode="False" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
        <br />
        <br />
        <asp:Button ID="Button3" runat="server" Font-Size="Medium" Text="刷新" 
            Width="114px" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" 
            onclick="Button3_Click" />
    </div>

    </form>
</body>
</html>
