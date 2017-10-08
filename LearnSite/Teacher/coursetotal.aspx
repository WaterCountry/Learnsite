<%@ Page Title="" Language="C#" EnableEventValidation = "false" StylesheetTheme="Teacher" AutoEventWireup="true"
    CodeFile="coursetotal.aspx.cs" Inherits="Teacher_coursetotal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="text-align: center;">        
            <asp:Label ID="LabelGradeClass" runat="server" Font-Bold="True" Font-Size="12pt"></asp:Label>
            <asp:DropDownList ID="DDLCid" runat="server" Font-Names="Arial" Font-Size="9pt" AutoPostBack="True" 
              onselectedindexchanged="DDLCid_SelectedIndexChanged">
          </asp:DropDownList>
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="12pt">学习汇总</asp:Label>
            <br />
            <div style="margin: auto; text-align: center">
            <center>
                <asp:DataList ID="DataList1" runat="server" RepeatColumns="6" 
                    RepeatDirection="Horizontal" Font-Size="9pt" CellPadding="3" 
                    onitemdatabound="DataList1_ItemDataBound">
                    <ItemTemplate>
                    <div style="border: 1px solid #E3E3E3; margin: auto; padding: 2px; width: 120px; font-size: 9pt; background-color: #E7F2FE; text-align: left; font-family: Arial;">
                        <asp:Label ID="LabelName" runat="server" Text='<%# Eval("Sgtitle") %>'  ForeColor="#0033CC" Font-Bold="True"></asp:Label>
                        <br />&nbsp;总分：<asp:Label ID="LabelGroup" runat="server" Text='<%# Eval("Sgscore") %>'></asp:Label>                        
                        <br />&nbsp;平均：<asp:Label ID="LabelAvg" runat="server" Text='<%# Eval("Svscore") %>'></asp:Label>                        
                        <br />&nbsp;合作：<asp:Label ID="LabelCooperation" runat="server" Text='<%# Eval("Sgwork") %>'></asp:Label>
                        <br />&nbsp;表现：<asp:Label ID="Labelattitude" runat="server" Text='<%# Eval("Sgattitude") %>'></asp:Label>                        
                        </div>
                    </ItemTemplate>
                </asp:DataList>
        <asp:RadioButtonList ID="RBsortGroup" runat="server" AutoPostBack="True" 
            Font-Size="9pt" onselectedindexchanged="RBsortGroup_SelectedIndexChanged" 
            RepeatDirection="Horizontal" RepeatLayout="Flow">
            <Items>
                <asp:ListItem Value="0" Selected="True">默认排序</asp:ListItem>
                <asp:ListItem Value="1">总分排序</asp:ListItem>
                <asp:ListItem Value="2">均分排序</asp:ListItem>
                <asp:ListItem Value="3">合作排序</asp:ListItem>
                <asp:ListItem Value="4">表现排序</asp:ListItem>
            </Items>
        </asp:RadioButtonList>
                <br />
                <asp:GridView ID="GridViewclass" runat="server" OnRowDataBound="GridViewclass_RowDataBound"
                    TabIndex="1" CellPadding="3" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None"
                    BorderWidth="1px" Font-Names="Arial" Font-Size="9pt" HorizontalAlign="Center"
                    EnableModelValidation="True">
                    <RowStyle ForeColor="#000066" />
                    <Columns>
                        <asp:BoundField HeaderText="序号">
                            <ItemStyle Width="30px" />
                        </asp:BoundField>
                    </Columns>
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#305E9C" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
        <asp:RadioButtonList ID="RBsort" runat="server" AutoPostBack="True" 
            Font-Size="9pt" onselectedindexchanged="RBsort_SelectedIndexChanged" 
            RepeatDirection="Horizontal" RepeatLayout="Flow">
            <Items>
            <asp:ListItem Value="学号" Selected="True">学号排序</asp:ListItem>
            <asp:ListItem Value="汇总">汇总排序</asp:ListItem>
            </Items>
        </asp:RadioButtonList>

                &nbsp;<asp:ImageButton ID="ImageBtnExcel" runat="server" ImageUrl="~/Images/down.gif" 
                onclick="ImageBtnExcel_Click" ToolTip="将汇总表导出为Excel格式" />
&nbsp;<asp:ImageButton ID="Btnreflash" runat="server" ImageUrl="~/Images/refresh.gif" OnClick="Btnreflash_Click" />

                <br />
            </center>
            </div>
            <br />
            <asp:Button ID="Btnreturn" runat="server" Text="关闭" SkinID="buttonSkin" Height="20px"
                Width="80px" BackColor="#305E9C" BorderStyle="None" Font-Size="9pt" ForeColor="White" />
            <br />
            <br />
            <asp:Label ID="Labelmsg" runat="server" SkinID="LabelMsgBlack"></asp:Label>
            <br />
        </div>
    </div>
    </form>
</body>
</html>
