<%@ Page Language="C#" AutoEventWireup="true"  StylesheetTheme="Student" CodeFile="autonomic.aspx.cs" Inherits="Student_autonomic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>   
    <link href="../App_Themes/Student/StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .divcate{margin:auto; padding: 2px; background-color: #E0ECFE; font-size: 9pt; font-weight: bold; text-align: left; height: 24px; width:100%;}
        .divlate{margin:auto; padding: 2px; background-color: #E0ECFE; font-size: 9pt; font-weight: bold; text-align: left; height: 24px; width:100%;}
        .licss{font-size: 9pt; height:30px; width:400px; text-align: left; border-width: 1px; border-bottom-style: dashed; border-color: #CCCCCC}
        .licss1{font-size: 9pt; height:24px; width:98%; text-align: left; border-width: 1px; border-bottom-style: dashed; border-color: #CCCCCC}
        .licss2{font-size: 9pt; height:24px; width:98%; text-align: left; border-width: 1px; border-bottom-style: dashed; border-color: #CCCCCC; background-color:#eeeeee}
    </style>
    </head>
<body>
    <form id="form1" runat="server">
    <center>
      <div  class="studmasterhead">
       <div  class="banner" > <img alt="" src="../Images/autonomic.gif" /></div>
       <div class="path"></div>
      <div id="student">
<div class="left">
    <asp:DataList ID="DLCategory" runat="server" RepeatColumns="1" 
        RepeatDirection="Horizontal" 
        Width="100%" CellPadding="3" CellSpacing="3" 
        DataKeyField="Yid" onitemdatabound="DLCategory_ItemDataBound">
        <ItemTemplate>
        <div>
        <div  class="divcate">
            <img alt="" src="../Images/FileType/read.gif" />
            <asp:HyperLink ID="HLYtitle" runat="server" Text ='<%# Eval("Ytitle") %>'></asp:HyperLink>
        </div>
        <div style="text-align: left">
        <%#ListNews(Eval("Yid"),5, "licss",30)%>    
        </div>
        </div>
        </ItemTemplate>
    </asp:DataList>
    <br />
</div>
<div class="right">
    我的作品<br />
    <br />
    <div style="width: 98%">
    <ul>
    <asp:Repeater ID="RepMy" runat="server" >
    <ItemTemplate>
    <li class="licss1"><a href="<%#GetdownUrl(Eval("Aurl").ToString())%>" target="_blank"><%#strcut( Eval("Ftitle").ToString())%></a></li>
    </ItemTemplate>
    <AlternatingItemTemplate>
    <li class="licss2"><a href="<%#GetdownUrl(Eval("Aurl").ToString())%>"  target="_blank"><%#strcut( Eval("Ftitle").ToString())%></a></li>
    </AlternatingItemTemplate>
    </asp:Repeater>
    </ul>
    </div>
    <br />
    <br />
    <br />
    </div>
</div>      
        </div>
       </center>
        <br />
    </form>
</body>
</html>

