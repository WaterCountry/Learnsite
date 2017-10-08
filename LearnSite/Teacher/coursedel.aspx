<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master" StylesheetTheme="Teacher" AutoEventWireup="true" CodeFile="coursedel.aspx.cs" Inherits="Teacher_coursedel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
  <div  class="placehold">
        <br />
        <br />
        <br />
        <br />
        <br />
        <div  class="pattitude">
            <br />
            <asp:Label ID="LabelID" runat="server" Font-Bold="True"></asp:Label>
            <br />
            <br />
            <br />
            确定删除该学案吗？<br />
            <br />
            <br />
            <asp:Button ID="ButtonDel" runat="server" Text="确定" EnableViewState="False"  OnClick="ButtonDel_Click"  SkinID="BtnNormal" />
            &nbsp; &nbsp;
            <asp:Button ID="ButtonCancle" runat="server"  Text="取消"  OnClick="ButtonCancle_Click" SkinID="BtnNormal" />
                        <br />
            <br />
                        </div>
                        <br />
        <br />
                        <br />
    </div>
</asp:Content>

