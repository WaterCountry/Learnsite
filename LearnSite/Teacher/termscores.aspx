<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master" StylesheetTheme="Teacher" AutoEventWireup="true" CodeFile="termscores.aspx.cs" Inherits="Teacher_termscores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <div  class="placehold"> 
        <div  >        
           年级<asp:DropDownList ID="DDLgrade" runat="server" 
                Font-Size="9pt" Width="50px" 
                onselectedindexchanged="DDLgrade_SelectedIndexChanged" AutoPostBack="True"> </asp:DropDownList>
            班级<asp:DropDownList ID="DDLclass" runat="server" Font-Size="9pt" 
                Width="50px" AutoPostBack="True" 
                onselectedindexchanged="DDLclass_SelectedIndexChanged">
            </asp:DropDownList>&nbsp;第<asp:Label ID="Lbterm" runat="server"></asp:Label>
            学期&nbsp;
            <asp:Button   ID="BtnScoresNo" runat="server"  OnClick="BtnScoresNo_Click" 
                Text=" 未评设置C"  SkinID="BtnNormal" ToolTip="所教班级未评作品全部设置为C，即分值6" />
            &nbsp;&nbsp;
            <asp:Button   ID="BtnScores" runat="server"  OnClick="BtnScore_Click" 
                Text="总分折算"  SkinID="BtnSmall" ToolTip="先统计总分，再得出折算总分" />
            &nbsp;&nbsp; 
            <asp:Button ID="Btnape" runat="server"  onclick="Btnape_Click" Text="期末总评" 
                SkinID="BtnSmall" />
&nbsp;&nbsp;
            <asp:Button ID="BtnExcel" runat="server"  OnClick="BtnExcel_Click" 
                Text="导出Excel"  SkinID="BtnSmall" ToolTip="将学生期末成绩以Excel表格导出" />&nbsp;&nbsp;<asp:Button 
                ID="Btntermview" runat="server"  Text="学期查询"  OnClick="Btntermview_Click" 
                SkinID="BtnSmall" />
            &nbsp;&nbsp;<asp:Button ID="Btnback" runat="server"  Text="返回"  OnClick="Btnback_Click" SkinID="BtnSmall" />
            <br />
            <br />
            总分折算设置== 作品+小组+讨论+表单：<asp:DropDownList ID="DDLwork" runat="server" Font-Size="9pt" 
                Width="50px">
                <asp:ListItem Selected="True">100</asp:ListItem>
                <asp:ListItem>90</asp:ListItem>
                <asp:ListItem>80</asp:ListItem>
                <asp:ListItem>70</asp:ListItem>
                <asp:ListItem>60</asp:ListItem>
                <asp:ListItem>50</asp:ListItem>
                <asp:ListItem>40</asp:ListItem>
                <asp:ListItem>30</asp:ListItem>
                <asp:ListItem>20</asp:ListItem>
                <asp:ListItem>10</asp:ListItem>
                <asp:ListItem>0</asp:ListItem>
            </asp:DropDownList>&nbsp;调查：<asp:DropDownList ID="DDLsurvey" runat="server" Font-Size="9pt" 
                Width="50px">
                <asp:ListItem Selected="True">100</asp:ListItem>
                <asp:ListItem>90</asp:ListItem>
                <asp:ListItem>80</asp:ListItem>
                <asp:ListItem>70</asp:ListItem>
                <asp:ListItem>60</asp:ListItem>
                <asp:ListItem>50</asp:ListItem>
                <asp:ListItem>40</asp:ListItem>
                <asp:ListItem>30</asp:ListItem>
                <asp:ListItem>20</asp:ListItem>
                <asp:ListItem>10</asp:ListItem>
                <asp:ListItem>0</asp:ListItem>
            </asp:DropDownList>网页：<asp:DropDownList ID="DDLweb" runat="server" 
                Font-Size="9pt" Width="50px">
                <asp:ListItem>100</asp:ListItem>
                <asp:ListItem>90</asp:ListItem>
                <asp:ListItem>80</asp:ListItem>
                <asp:ListItem>70</asp:ListItem>
                <asp:ListItem>60</asp:ListItem>
                <asp:ListItem>50</asp:ListItem>
                <asp:ListItem>40</asp:ListItem>
                <asp:ListItem>30</asp:ListItem>
                <asp:ListItem>20</asp:ListItem>
                <asp:ListItem>10</asp:ListItem>
                <asp:ListItem Selected="True">0</asp:ListItem>
            </asp:DropDownList>测验：<asp:DropDownList ID="DDLquiz" runat="server" 
                Font-Size="9pt" Width="50px">
                <asp:ListItem>100</asp:ListItem>
                <asp:ListItem>90</asp:ListItem>
                <asp:ListItem>80</asp:ListItem>
                <asp:ListItem>70</asp:ListItem>
                <asp:ListItem>60</asp:ListItem>
                <asp:ListItem>50</asp:ListItem>
                <asp:ListItem>40</asp:ListItem>
                <asp:ListItem>30</asp:ListItem>
                <asp:ListItem Selected="True">20</asp:ListItem>
                <asp:ListItem>10</asp:ListItem>
                <asp:ListItem>0</asp:ListItem>
            </asp:DropDownList>中文：<asp:DropDownList ID="DDLtyper" runat="server" 
                Font-Size="9pt" Width="50px">
                <asp:ListItem>100</asp:ListItem>
                <asp:ListItem>90</asp:ListItem>
                <asp:ListItem>80</asp:ListItem>
                <asp:ListItem>70</asp:ListItem>
                <asp:ListItem>60</asp:ListItem>
                <asp:ListItem>50</asp:ListItem>
                <asp:ListItem>40</asp:ListItem>
                <asp:ListItem>30</asp:ListItem>
                <asp:ListItem>20</asp:ListItem>
                <asp:ListItem Selected="True">10</asp:ListItem>
                <asp:ListItem>0</asp:ListItem>
            </asp:DropDownList>
            表现：<asp:DropDownList ID="DDLattitude" runat="server" 
                Font-Size="9pt" Width="50px">
                <asp:ListItem Selected="True">100</asp:ListItem>
                <asp:ListItem>90</asp:ListItem>
                <asp:ListItem>80</asp:ListItem>
                <asp:ListItem>70</asp:ListItem>
                <asp:ListItem>60</asp:ListItem>
                <asp:ListItem>50</asp:ListItem>
                <asp:ListItem>40</asp:ListItem>
                <asp:ListItem>30</asp:ListItem>
                <asp:ListItem>20</asp:ListItem>
                <asp:ListItem>10</asp:ListItem>
                <asp:ListItem>0</asp:ListItem>
            </asp:DropDownList>
            &nbsp;&nbsp;
            <br />
            <br />
            期末总评每个班级的APE比例设置=== A占：<asp:DropDownList ID="DDLA" runat="server" Font-Size="9pt" 
                Width="50px">
                <asp:ListItem>100</asp:ListItem>
                <asp:ListItem>90</asp:ListItem>
                <asp:ListItem>80</asp:ListItem>
                <asp:ListItem>70</asp:ListItem>
                <asp:ListItem>60</asp:ListItem>
                <asp:ListItem Selected="True">50</asp:ListItem>
                <asp:ListItem>40</asp:ListItem>
                <asp:ListItem>30</asp:ListItem>
                <asp:ListItem>20</asp:ListItem>
                <asp:ListItem>10</asp:ListItem>
            </asp:DropDownList>&nbsp;&nbsp;　　总分在百分之<asp:DropDownList ID="DDLE" runat="server" Font-Size="9pt" 
                Width="50px">
                <asp:ListItem>50</asp:ListItem>
                <asp:ListItem>40</asp:ListItem>
                <asp:ListItem Selected="True">30</asp:ListItem>
                <asp:ListItem>20</asp:ListItem>
                <asp:ListItem>10</asp:ListItem>
                <asp:ListItem>5</asp:ListItem>
            </asp:DropDownList>
            &nbsp;以下自动评Ｅ<br />
            <asp:Label ID="Labelmsg" runat="server"  SkinID="LabelMsgRed" 
                ></asp:Label>
            </div>
            <asp:GridView ID="GVCourse" runat="server" AutoGenerateColumns="False"
                 DataKeyNames="Sid"  SkinID="GVmission" OnRowDataBound="GVCourse_RowDataBound"
                PageSize="25" Width="98%" EnableModelValidation="True" >
                <Columns>
                    <asp:BoundField HeaderText="编号" />
                    <asp:BoundField DataField="Snum" HeaderText="学号" />
                    <asp:BoundField DataField="Sgradeclass" HeaderText="班级" />
                    <asp:HyperLinkField DataNavigateUrlFields="Snum" 
                        DataNavigateUrlFormatString="studentwork.aspx?Snum={0}" DataTextField="Sname" 
                        HeaderText="姓名" Target="_blank"  />
                    <asp:BoundField DataField="Sscore" HeaderText="作品" />
                    <asp:BoundField DataField="Sgscore" HeaderText="小组" />
                    <asp:BoundField DataField="Spscore" HeaderText="讨论" />
                    <asp:BoundField DataField="Stxtform" HeaderText="表单" />
                    <asp:BoundField DataField="Svscore" HeaderText="调查" />
                    <asp:BoundField DataField="Swscore" HeaderText="网页" />
                    <asp:BoundField DataField="Squiz" HeaderText="测验" />
                    <asp:BoundField DataField="Schinese" HeaderText="拼音" />
                    <asp:BoundField DataField="Sfscore" HeaderText="英语" />
                    <asp:BoundField DataField="Stscore" HeaderText="中文" />
                    <asp:BoundField DataField="Sattitude" HeaderText="表现" />
                    <asp:BoundField DataField="Sallscore" HeaderText="总分" />
                    <asp:BoundField DataField="Sape" HeaderText="评定" />
                </Columns>
            </asp:GridView>
            <br />
            <br />
        </div>
</asp:Content>

