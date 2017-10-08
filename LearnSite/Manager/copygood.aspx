<%@ Page Title="" Language="C#" MasterPageFile="~/Manager/Manage.master" StylesheetTheme="Teacher"  AutoEventWireup="true" CodeFile="copygood.aspx.cs" Inherits="Manager_copygood" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <div>
    
        <br />
        <br />
        <strong>操作说明</strong><br />
        <br />
        <br />
        将所有12分的推荐作品复制到网站GoodStore目录下<br />
        <br />
        按入学年度、学案分级保存，方便提取独立展示。<br />
        <br />
        <br />
        <br />
                    <asp:Button ID="Btnbackup" runat="server" BackColor="#E6E6E6" 
                        BorderColor="#D4D4D4" BorderWidth="1px" Font-Names="Arial" 
                Font-Size="9pt" Text="备份优秀作品" Width="100px" Height="20px" 
                onclick="Btnbackup_Click" />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:Label ID="Labelmsg" runat="server" ForeColor="#009900" Width="300px"></asp:Label>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    
    </div>
</asp:Content>

