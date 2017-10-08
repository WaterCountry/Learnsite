<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master"  StylesheetTheme="Teacher" AutoEventWireup="true" CodeFile="overallmerit.aspx.cs" Inherits="Teacher_overallmerit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div  class="placehold"> 
        <div  >        
            <div style="font-family: Arial; font-size: 12pt; font-weight: bold">
                <asp:DropDownList ID="DDLYear" runat="server" 
                Font-Size="9pt" Font-Bold="False" Font-Names="Arial"> </asp:DropDownList>
                级各学期综合评定汇总表</div>
            <br />
            选择要显示的年级<asp:CheckBoxList ID="CBgrades" runat="server" 
                RepeatDirection="Horizontal" RepeatLayout="Flow">
            </asp:CheckBoxList>
            <br />
            <br />
            <asp:Button ID="Btnsearch" runat="server" onclick="Btnsearch_Click" 
                SkinID="BtnSmall" Text="查询" />
&nbsp;&nbsp;&nbsp; <asp:Button ID="Btnback" runat="server"  Text="返回"  OnClick="Btnback_Click" SkinID="BtnSmall" />
            <br />
            <div>
            <asp:GridView ID="GVapes" runat="server" BorderStyle="None" 
                    HorizontalAlign="Center" Width="90%">
            </asp:GridView>
            </div>
            <br />
            <asp:FileUpload ID="FileUpload1" runat="server" Font-Size="9pt" />
            <asp:Button ID="BtnImport" runat="server" onclick="BtnImport_Click" 
                SkinID="BtnLong" Text="导入报名序号Excel" ToolTip="报名序号和姓名Excel" />
            <br />
            <br />
            说明：报名序号Excel里只有两列为报名序号和姓名对应表<br />
            <br />
            <asp:Button ID="BtnExcel" runat="server" onclick="BtnExcel_Click" 
                SkinID="BtnNormal" Text="导出Excel" />
            <br />
            <br />            
            提示：级为入学年度
            <asp:Label ID="Labelmsg" runat="server"></asp:Label>
            <br />
            </div>
        </div>
</asp:Content>

