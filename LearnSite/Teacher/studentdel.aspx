<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master" StylesheetTheme="Teacher" AutoEventWireup="true" CodeFile="studentdel.aspx.cs" Inherits="Teacher_studentdel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
 <div  class="placehold">
         <br />
         <br />
         <br />
         <br />
         <div  class="pattitude">
             <br />
             <br />
             确定要删除 &nbsp;<asp:Label ID="LabeSname" runat="server" BackColor="#D5ECDE" 
                 Font-Bold="True"></asp:Label> &nbsp;学生吗？<br />
             <br />
             <br />
             <asp:LinkButton ID="LinkBtnDel" runat="server" OnClick="LinkBtnDel_Click"  SkinID="LinkBtn">确定</asp:LinkButton>
             &nbsp; &nbsp;&nbsp; &nbsp;
             <asp:LinkButton ID="LinkBtncancel" runat="server"  OnClick="LinkBtncancel_Click" SkinID="LinkBtn">返回</asp:LinkButton>
             <br />
             <br />
             <br />
         </div> 
         <br />
         <br />
         <br />
         <br />
    </div>
</asp:Content>

