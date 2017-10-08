<%@ Page Language="C#" AutoEventWireup="true" CodeFile="studentworks.aspx.cs" Inherits="Workshow_studentworks" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Js/flot/examples.css" rel="stylesheet" type="text/css" />
    <script src="../Js/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../Js/flot/excanvas.min.js" type="text/javascript"></script> 
    <script src="../Js/flot/jquery.flot.min.js" type="text/javascript"></script>    
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center; font-family: 宋体, Arial, Helvetica, sans-serif; font-size: 9pt;">    
        <asp:Label ID="LabelSname" runat="server" Font-Bold="True"></asp:Label>
        <div class="demo-container">
		<div id="placeholder" class="demo-placeholder"></div>
		</div>
        <script type="text/javascript">
            $(function () {               
                var scores = [<%=msg %>];
                $.plot("#placeholder", [scores]);
            });
	    </script>
        <div style="margin: auto; width: 98%; font-size: 9pt;">
    <asp:GridView ID="GridViewworks" runat="server"  Width="100%"  
                AutoGenerateColumns="False" EnableModelValidation="True" 
            BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="2px" 
            GridLines="Horizontal" 
                onrowdatabound="GridViewworks_RowDataBound">
        <Columns>
            <asp:BoundField DataField="Wgrade" HeaderText="年级" />
            <asp:BoundField DataField="Wclass" HeaderText="班级" />
            <asp:BoundField DataField="Wterm" HeaderText="学期" />
            <asp:BoundField DataField="Ctitle" HeaderText="学案" />
            <asp:BoundField DataField="Mtitle" HeaderText="活动" />
            <asp:BoundField DataField="Wscore" HeaderText="学分" />
            <asp:BoundField DataField="Wdscore" HeaderText="加分" />
            <asp:BoundField DataField="Wvote" HeaderText="鲜花"/>
            <asp:TemplateField HeaderText="查看">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLinkWurl" runat="server"  Target="_blank" 
                      ToolTip='<%# Eval("Wurl") %>'  Text="下载"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>            
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLinkView" runat="server" NavigateUrl="" Text="预览" Target="_blank"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="日期">
                <ItemTemplate>
                    <asp:Label ID="LabelWdate" runat="server" Text='<%# Bind("Wdate") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CheckBoxField DataField="Wcheck" HeaderText="评价" ReadOnly="True" />
        </Columns>
        <FooterStyle BackColor="White" ForeColor="#333333" />
        <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Left" />        
        <RowStyle BackColor="White" ForeColor="#333333" HorizontalAlign="Left" />
        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
    </div>
     <asp:Button ID="Btnclose" runat="server"   Text="关闭" BackColor="WhiteSmoke" 
            BorderColor="#CCCCCC" BorderStyle="None" Font-Size="9pt" Height="20px" 
            Width="60px" />
        <br />
    </div>
    </form>
</body>
</html>
