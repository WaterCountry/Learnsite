<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master"  StylesheetTheme="Teacher"  AutoEventWireup="true" CodeFile="termview.aspx.cs" Inherits="Teacher_termview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <div  class="placehold"> 
        <div  >        
            请选择入学年度：<asp:DropDownList ID="DDLYear" runat="server" 
                Font-Size="9pt" AutoPostBack="True" 
                onselectedindexchanged="DDLYear_SelectedIndexChanged"> </asp:DropDownList>
            请选择学期：<asp:DropDownList ID="DDLgrade" runat="server" Font-Size="9pt"> </asp:DropDownList>
           年级 &nbsp;<asp:DropDownList ID="DDLclass" runat="server" Font-Size="9pt">
            </asp:DropDownList>班级 &nbsp;<asp:DropDownList ID="DDLterm" runat="server" Font-Size="9pt" 
                    EnableTheming="True" >
                <asp:ListItem Value="1">第一学期</asp:ListItem>
                <asp:ListItem Value="2">第二学期</asp:ListItem>
        </asp:DropDownList>
            <br />
            <br />
          <asp:Button ID="BtnExcel" runat="server"  OnClick="BtnExcel_Click" 
                Text="导出Excel"  SkinID="BtnSmall" ToolTip="将学生期末成绩以Excel表格导出" />&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; <asp:Button 
                ID="Btnmerit" runat="server"  Text="综合评定"  OnClick="Btnmerit_Click" 
                SkinID="BtnSmall" />
            &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
            <asp:Button ID="Btnshow" runat="server"  OnClick="Btnshow_Click" Text="成绩浏览"  
                SkinID="BtnSmall" />
            &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; <asp:Button ID="Btnback" runat="server"  Text="返回"  OnClick="Btnback_Click" SkinID="BtnSmall" />
            <br />
            <asp:Label ID="Labelmsg" runat="server"  SkinID="LabelMsgRed" 
                ></asp:Label>
            </div>
            <div>
            <asp:GridView ID="GVTermScore" runat="server" AutoGenerateColumns="False"
                 DataKeyNames="Tid"  SkinID="GVmission"
                PageSize="25" Width="98%" EnableModelValidation="True" 
            onrowdatabound="GVTermScore_RowDataBound" HorizontalAlign="Center" >
                <Columns>
                    <asp:BoundField HeaderText="编号" />
                    <asp:BoundField DataField="Tnum" HeaderText="学号" />
                    <asp:BoundField DataField="Tgrade" HeaderText="年级" />
                    <asp:BoundField DataField="Tclass" HeaderText="班级" />
                    <asp:HyperLinkField DataNavigateUrlFields="Tnum,Tgrade,Tterm" 
                        DataNavigateUrlFormatString="studentwork.aspx?Snum={0}&amp;Sgrade={1}&amp;Sterm={2}" 
                        DataTextField="Tname" HeaderText="姓名" Target="_blank" />
                    <asp:BoundField DataField="Tscore" HeaderText="作品" />
                    <asp:BoundField DataField="Tgscore" HeaderText="小组" />
                    <asp:BoundField DataField="Tpscore" HeaderText="讨论" />
                    <asp:BoundField DataField="Ttxtform" HeaderText="表单" />
                    <asp:BoundField DataField="Tvscore" HeaderText="调查" />
                    <asp:BoundField DataField="Twscore" HeaderText="网页" />
                    <asp:BoundField DataField="Tquiz" HeaderText="测验" />
                    <asp:BoundField DataField="Tchinese" HeaderText="拼音" />
                    <asp:BoundField DataField="Tfscore" HeaderText="英语" />
                    <asp:BoundField DataField="Ttscore" HeaderText="中文" />
                    <asp:BoundField DataField="Tattitude" HeaderText="表现" />
                    <asp:BoundField DataField="Tallscore" HeaderText="总分" />
                    <asp:BoundField DataField="Tape" HeaderText="评定" />
                </Columns>
            </asp:GridView>
            </div>
        <br />
        </div>
</asp:Content>

