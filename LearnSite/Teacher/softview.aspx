<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master"  StylesheetTheme="Teacher"  AutoEventWireup="true" CodeFile="softview.aspx.cs" Inherits="Teacher_softview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <div class="left">
        <br />
        <asp:Label ID="Labeltitle" runat="server" 
        Width="550px" CssClass="textcenter" Height="24px" BackColor="#E0E7FE" 
            BorderColor="#CAD6FD" BorderStyle="Solid" BorderWidth="1px"></asp:Label>
        <br />
        <div style="padding: 2px; margin: auto; border-bottom-style: dashed; border-width: 1px; border-color: #CCCCCC">资源：<asp:Label ID="Labelclass" runat="server"  SkinID="LabelFileShow"></asp:Label>
            格式：<asp:Image ID="ImageType" runat="server" />
        <asp:Label ID="Labelfiletype" runat="server"  ></asp:Label>
    点击率：<asp:Label ID="Labelhit" runat="server"  ></asp:Label>
    更新日期：<asp:Label ID="Labeldate" runat="server"  ></asp:Label>
    学分：<asp:Label ID="Labelopen" runat="server"  ></asp:Label>
            &nbsp;
            <asp:ImageButton ID="BtnEdit" runat="server" ToolTip="点击修改" 
            ImageUrl="~/Images/edit.gif" onclick="BtnEdit_Click" 
           style="width: 16px" />
        &nbsp;&nbsp;&nbsp;&nbsp; <asp:ImageButton ID="BtnReturnSmall" runat="server" ToolTip="返回" 
            ImageUrl="~/Images/return.gif" onclick="BtnReturnSmall_Click" 
           style="width: 16px" />
        </div>
        <br />
        <center>
            <div >
                <br />
                <div style="padding: 2px; margin: auto; text-align: left; line-height: 18px; width: 780px;" >
                    <asp:Literal ID="Labelcontent" runat="server"></asp:Literal>
                </div>
                <br />
            </div>
        </center>
        <br />
        <asp:Image ID="ImageDown" runat="server" ImageUrl="~/Images/down1.gif" />
        <asp:LinkButton ID="LBtnfile" runat="server" 
        OnClick="LBtnfile_Click" Font-Underline="False" 
        BorderColor="#7DBF80" BorderStyle="Dashed" BorderWidth="1px" 
        CssClass="txtszcenter" Height="18px" BackColor="#E2F3E3" Width="80px">点击下载</asp:LinkButton>
        <br />
    <asp:HyperLink ID="HLurl" runat="server" Visible="false" ></asp:HyperLink>
        <br />
        <br />
              <asp:Button ID="Btnreturn" runat="server"  Text="返回" OnClick="Btnreturn_Click"  
                  SkinID="BtnNormal" />
        <br />
        <br />
        <br />
    </div>
</asp:Content>

