<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master" StylesheetTheme="Teacher" AutoEventWireup="true" CodeFile="typedel.aspx.cs" Inherits="Teacher_typedel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div  class="placehold">
         <br />
         <br />
         <br />
         <br />
         <div  class="pattitude">
             <br />
             <br />
             确定要删除ID为 
             <asp:Label ID="LabelTid" runat="server" BackColor="#E1EEE4" Font-Bold="True" 
                 Font-Names="Arial"></asp:Label>
&nbsp;的文章吗？<br />
             <br />
             <asp:LinkButton ID="LinkBtnDel" runat="server" OnClick="LinkBtnDel_Click"  SkinID="LinkBtn">确定</asp:LinkButton>
             &nbsp; &nbsp; &nbsp; 
             <asp:LinkButton ID="LinkBtncancel" runat="server"  OnClick="LinkBtncancel_Click"  SkinID="LinkBtn">返回</asp:LinkButton>
             <br />
             <br />
             <br />
         </div>
             <br />
         <br />
             <br />            
    </div>
</asp:Content>

