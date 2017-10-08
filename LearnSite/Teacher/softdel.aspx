<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master" StylesheetTheme="Teacher" AutoEventWireup="true" CodeFile="softdel.aspx.cs" Inherits="Teacher_softdel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div  class="placehold">
         <br />
         <br />
         <br />
         <br />
         <div  class="pattitude">
             <br />
             <asp:Label ID="Labelurl" runat="server"></asp:Label>
             <br />
             <br />
             确定要删除本资源吗？
             <br />
             <br />
             <asp:LinkButton ID="LinkBtnDel" runat="server"  OnClick="LinkBtnDel_Click" SkinID="LinkBtn" >确定</asp:LinkButton>
             &nbsp; &nbsp;&nbsp; &nbsp;
             <asp:LinkButton ID="LinkBtncancel" runat="server"  OnClick="LinkBtncancel_Click"  SkinID="LinkBtn">返回</asp:LinkButton>
             <br />
             <br />
         </div>
         <br />
         <br />
    </div>
</asp:Content>

