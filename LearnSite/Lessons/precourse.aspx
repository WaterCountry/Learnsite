<%@ page title="" language="C#" masterpagefile="~/lessons/prescm.master" stylesheettheme="Student" validaterequest="false" autoeventwireup="true" inherits="Lessons_precourse, LearnSite" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Ppcm" Runat="Server">
<div id="showcontent">
<div class="left"> 
    <br />  	
    <asp:Label ID="LabelCtitle" runat="server"  CssClass="coursetitle"></asp:Label><br /><br />
    <div class="courseother">
                 ���ڣ�[<asp:Label ID="LabelCdate"  runat="server" ></asp:Label>]&nbsp;&nbsp;
			     ѧ�����ͣ�[<asp:Label ID="LabelCclass"  runat="server" ></asp:Label>] &nbsp;   
                 ѧϰ�꼶��[<asp:Label ID="LabelCobj"  runat="server" ></asp:Label>]&nbsp;
                 ��[<asp:Label ID="LabelCterm"  runat="server" ></asp:Label>]ѧ��&nbsp;&nbsp;
                 [��ʱ��<asp:Label ID="LabelCks"  runat="server" ></asp:Label>]			
    </div>
    <div  id="Ccontent" class="coursecontent" runat ="server">   
    </div>
    <br/>
    <br />        
</div>

<div class="right">  
<br />
<asp:Label ID="LabelWelcome"  runat="server" ></asp:Label>
<br /><br />
    <asp:Calendar ID="Calendar1" runat="server" BackColor="White" 
        BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" 
        Font-Names="Arial" Font-Size="9pt" ForeColor="Black" Height="180px" 
        Width="170px" BorderStyle="None">
        <DayHeaderStyle BackColor="#D7E3FF" Font-Bold="False" Font-Names="Arial" />
        <NextPrevStyle VerticalAlign="Bottom" />
        <OtherMonthDayStyle ForeColor="#808080" />
        <SelectedDayStyle BackColor="#F3B185" Font-Bold="True" ForeColor="White" />
        <SelectorStyle BackColor="#CCCCCC" />
        <TitleStyle BackColor="#B3CBFF" BorderColor="Black" />
        <TodayDayStyle BackColor="#CEDDBB" ForeColor="Black" />
        <WeekendDayStyle BackColor="#EDF9DF" />
    </asp:Calendar>
    <br />    
</div>

<br />
</div>
</asp:Content>

