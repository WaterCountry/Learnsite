<%@ Page Title="" Language="C#" MasterPageFile="~/Manager/Manage.master" AutoEventWireup="true" CodeFile="studentimport.aspx.cs" Inherits="Manager_studentimport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
       <div class="manageplace" >        
        <div style="border: 1px solid #D8D8D8; width: 283px;  margin: auto; ">
        <div style="font-size: 9pt; background-color: #EEEEEE; height: 18px;  margin: auto;">
        学生信息导入</div>
            <br />
            <br />
            <asp:FileUpload ID="FileUpExcel" runat="server" BorderColor="Gainsboro" BorderStyle="Solid"
                BorderWidth="1px" Font-Names="Arial" Font-Size="9pt" Height="20px" 
                Width="160px" BackColor="White" />
            <br />
           <br />
            <asp:CheckBox ID="CheckBox1" runat="server" Text="密码转换为姓名拼音缩写" 
                ToolTip="是否在获取数据时自动将密码转换为学生姓名拼音缩写" />
            <br />
            <br />
            <asp:Button ID="ButtonInsert" runat="server" BorderColor="Silver" BorderStyle="None"
                BorderWidth="1px" Font-Names="Arial" Font-Size="9pt" Height="20px" OnClick="ButtonInsert_Click"
                TabIndex="1" Text="1 上传Excel" Width="100px" ToolTip="上传并导入临时学生表" />            
            <br />
            <br />
            <br />
            <asp:Button ID="ButtonAppend" runat="server" BorderColor="Silver"
                BorderStyle="None" BorderWidth="1px" Font-Names="Arial" Font-Size="9pt" Height="20px"
                OnClick="ButtonAppend_Click" TabIndex="1" Text="2 导入数据" Width="100px" 
                ToolTip="将上传的学生临时表数据导入平台学生表中" Enabled="False" />
            <br />
            <br />
            <div id="Loading" style=" display:none ;text-align: center; font-family: 宋体, Arial, Helvetica, sans-serif; font-size: 9pt; color: #FF0000;">
            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/load2.gif" />
            <input id="Textcmd" style="border-style: none" type="text" /></div>  
            <br />
            <br />
        </div>
        <br />
        <asp:Label ID="Labelmsg" runat="server" Font-Names="Arial" Font-Size="9pt" 
               ForeColor="Red">**导入Excel数据中必须要有学号、入学年度、年级、班级、姓名、密码、性别**<br />**入学年度、年级、班级必须为数字；学号必须为数字且尽量不超过12位**</asp:Label>
           <br />
           <br />
           <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/说明必读/学生导入模板.xls" 
               Target="_blank">请参考网站目录下的说明必读目录中的学生信息Excel模板</asp:HyperLink>
           <br />
        <br />
            <asp:Button ID="ButtonClear" runat="server" BorderColor="#CCCCCC"
                BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" 
            Font-Size="9pt" Height="20px"
                OnClick="ButtonClear_Click" TabIndex="1" Text="清除最近导入数据" Width="120px" 
            BackColor="#DDDDDD" ToolTip="只删除刚才导入的数据，以方便重新导入！" Font-Bold="False" 
               ForeColor="Red" />
           <br />
        <br />
           <asp:GridView ID="GVrepeat" runat="server" BorderColor="#E1E1E1"
            BorderStyle="Solid" BorderWidth="1px" Font-Size="9pt"
            Width="280px" GridLines="None" CellPadding="2" PageSize="25" 
               Caption="导入数据检验重复列表" HorizontalAlign="Center" 
               EnableTheming="False" EnableViewState="False">
                            <RowStyle Font-Size="9pt" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#EFEFEF" Font-Size="9pt" ForeColor="Black" 
                                HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#EFEFEF" Font-Bold="False" Font-Size="9pt" 
                                ForeColor="#222222" />
                            <AlternatingRowStyle BackColor="#EFEFEF" BorderStyle="None" />
        </asp:GridView>

           <br />
        <br />
        </div>
</asp:Content>

