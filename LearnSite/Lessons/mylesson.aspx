<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mylesson.aspx.cs" Inherits="Lessons_mylesson" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>教案备课-常规检查</title>
    <style type="text/css">
        .stylehead
        {
            height: 20px;
            width: 660px;
            font-weight: bold; font-family: 宋体, Arial, Helvetica, sans-serif; text-align: center;
            margin:auto;
        }
         .stylegrid
        {
            width: 660px;
            font-weight: bold; font-family: 宋体, Arial, Helvetica, sans-serif; text-align: center;
            margin:auto;
        }
        .selectdiv
        {
            height: 24px;
            width: 660px;
            font-family: 宋体, Arial, Helvetica, sans-serif; font-size: 9pt;
            margin:auto;  
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="text-align: center">
    <div class="stylehead">
        <span lang="zh-cn">教学活动</span>备课-常规检查
    </div>
    <div  class="selectdiv">
        选择教师<asp:DropDownList 
            ID="DDLhid" runat="server" Font-Size="9pt" 
            Width="100px" EnableTheming="True" AutoPostBack="True" 
            onselectedindexchanged="DDLhid_SelectedIndexChanged">
        </asp:DropDownList>
        &nbsp;选择学期<asp:DropDownList ID="DDLterm" runat="server" Font-Size="9pt" 
            Width="50px" EnableTheming="True" AutoPostBack="True" 
            onselectedindexchanged="DDLterm_SelectedIndexChanged">
            <asp:ListItem Selected="True">1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
        </asp:DropDownList>
        &nbsp;选择年级<asp:DropDownList ID="DDLgrade" runat="server" Font-Size="9pt" 
            Width="50px" EnableTheming="True" AutoPostBack="True" 
                onselectedindexchanged="DDLgrade_SelectedIndexChanged">
        </asp:DropDownList>
        </div>
    <div  class="stylegrid">
     <asp:GridView ID="GVCourse" runat="server" AutoGenerateColumns="False" 
            BorderColor="#E7E7E7" BorderStyle="Solid" BorderWidth="1px"
            CellPadding="4" DataKeyNames="Cid" GridLines="None"
            PageSize="15" Width="100%" Font-Size="9pt" 
        onrowdatabound="GVCourse_RowDataBound" EnableModelValidation="True">
            <FooterStyle BackColor="White" ForeColor="#333333" />
            <Columns>
                <asp:BoundField HeaderText="序号">
                    <ControlStyle Width="20px" />
                </asp:BoundField>
                <asp:BoundField DataField="Cterm" HeaderText="学期" />
                <asp:BoundField DataField="Cobj" HeaderText="年级">
                    <ControlStyle Width="20px" />
                </asp:BoundField>
                <asp:BoundField DataField="Cks" HeaderText="课时">
                    <ControlStyle Width="20px" />
                </asp:BoundField>
                <asp:HyperLinkField DataNavigateUrlFields="Cid" DataNavigateUrlFormatString="~/Lessons/LessonShow.aspx?Cid={0}"
                    DataTextField="Ctitle" HeaderText="学案" Target="_blank"  />
                <asp:HyperLinkField DataNavigateUrlFields="Cid" 
                    DataNavigateUrlFormatString="~/Lessons/precourse.aspx?Cid={0}" Text="在线预览" />
                <asp:BoundField DataField="Cclass" HeaderText="类型" SortExpression="Cclass" />
                <asp:TemplateField HeaderText="日期" SortExpression="Cdate">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Cdate","{0:d}")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

            <RowStyle ForeColor="#333333" Font-Size="9pt" HorizontalAlign="Left" />
            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />            
            <HeaderStyle BackColor="#3C3C3C" Font-Bold="False" ForeColor="White" 
                HorizontalAlign="Left" />
            <AlternatingRowStyle BackColor="#EAEAEA" />
        </asp:GridView>
        <br />
        </div>
    </form>
    </body>
</html>
