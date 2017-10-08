<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master"  StylesheetTheme="Teacher" AutoEventWireup="true" CodeFile="courseimport.aspx.cs" Inherits="Teacher_courseimport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div  class="placehold">
        <br />
        <br />
        <div  class="cimport">
            <div  class="phead" style="font-weight: bold">平台专用学案包导入</div>
            <br />
            当前选择：<asp:Label ID="Labelgrade" runat="server" Font-Bold="False"></asp:Label>
            年级<br />
            <br />
            <asp:FileUpload ID="FudPackage" runat="server" Font-Size="9pt" />
&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Btnimport" runat="server" onclick="Btnimport_Click" Text="导入" SkinID="BtnNormal" />
&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Btnreturn" runat="server" onclick="Btnreturn_Click" Text="返回"  SkinID="BtnNormal" />
            <br />
            <br />
        <asp:Label ID="Labelmsg" runat="server" Font-Size="9pt" ForeColor="Red" 
            Height="38px">*必须使用本平台生成的学案包*<br /><br />注意：学案包导入功能为版本向下兼容，不向上兼容！</asp:Label>
            <br />
                    <asp:GridView ID="GVCourse" runat="server"
                            AutoGenerateColumns="False"  DataKeyNames="Cid"  
                            PageSize="20" Width="100%" CellPadding="3" 
                            EnableModelValidation="True" Font-Size="9pt" ForeColor="#111111"    
                GridLines="None" Caption="当前导入的学案列表：" CaptionAlign="Left" >
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="Cobj" HeaderText="年级">
                                <ItemStyle Width="30px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Cks" HeaderText="课节">
                                <ItemStyle Width="30px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Ctitle" HeaderText="学案" />
                                <asp:BoundField DataField="Cclass" HeaderText="类型" SortExpression="Cclass" >
                                <ItemStyle Width="50px" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle BackColor="#9EA9B1" ForeColor="#111111" />                            
                            <RowStyle BackColor="#E7E7E7" Height="24px" />
                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        </asp:GridView>
            <br />
            <asp:Label ID="LabelnewCids" runat="server" Visible="False"></asp:Label>
            <br />
         </div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />

</div>
</asp:Content>

