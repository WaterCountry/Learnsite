<%@ Page Language="C#" AutoEventWireup="true"  StylesheetTheme="Student" CodeFile="autonomiccategory.aspx.cs" Inherits="Student_autonomiccategory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>   
    <link href="../App_Themes/Student/StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .divcate{margin:auto; padding: 2px; background-color: #E0ECFE; font-size: 9pt; font-weight: bold; text-align: left; height: 24px; width:360px;}
        .licss{font-size: 9pt; height:30px; width:360px; text-align: left; border-width: 1px; border-bottom-style: dashed; border-color: #CCCCCC}
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
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        Caption="资源分类" EnableModelValidation="True" Width="98%" AllowPaging="True" 
        CellPadding="3" onrowdatabound="GridView1_RowDataBound" PageSize="20" 
        onpageindexchanging="GridView1_PageIndexChanging" BorderColor="#999999" 
        BorderStyle="Solid" BorderWidth="1px">
        <Columns>
            <asp:BoundField HeaderText="序号" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Image ID="Imagetype" runat="server" ImageUrl='<%# GetfileType(Eval("Atype").ToString()) %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="作品">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLinkUrl" runat="server" 
                        NavigateUrl='<%# GetdownUrl(Eval("Aurl").ToString()) %>' 
                        Text='<%# Eval("Ftitle") %>'  Target="_blank" ForeColor="Blue"></asp:HyperLink>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" Width="400px" />
            </asp:TemplateField>
            <asp:BoundField DataField="Ascore" HeaderText="学分" />
            <asp:TemplateField HeaderText="姓名">
                <ItemTemplate>
                    <asp:Label ID="LabelAname" runat="server" Text='<%# HttpUtility.UrlDecode(Eval("Aname").ToString()) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="日期">
                <ItemTemplate>
                    <asp:Label ID="LabelAdate" runat="server" Text='<%# Bind("Adate") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Image ID="Imagegood" runat="server" ImageUrl="~/Images/new_none.gif" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField Visible="False">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBoxgood" runat="server" Checked='<%# Bind("Agood") %>' 
                        Enabled="false" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerTemplate>
            <div >
                <asp:Label ID="lblPageIndex" runat="server" Text="<%# ((GridView)Container.Parent.Parent).PageIndex + 1  %>" />
                /<asp:Label ID="lblPageCount" runat="server" Text="<%# ((GridView)Container.Parent.Parent).PageCount  %>" /> &nbsp; &nbsp;              
                <asp:LinkButton ID="btnFirst" runat="server" CausesValidation="False" CommandArgument="First"
                    CommandName="Page" Font-Underline="False" ForeColor="Black" Text="首页" />&nbsp;
                <asp:LinkButton ID="btnPrev" runat="server" CausesValidation="False" CommandArgument="Prev"
                    CommandName="Page" Font-Underline="False" ForeColor="Black" Text="上一页" />&nbsp;
                <asp:LinkButton ID="btnNext" runat="server" CausesValidation="False" CommandArgument="Next"
                    CommandName="Page" Font-Underline="False" ForeColor="Black" Text="下一页" />&nbsp;
                <asp:LinkButton ID="btnLast" runat="server" CausesValidation="False" CommandArgument="Last"
                    CommandName="Page" Font-Underline="False" ForeColor="Black" Text="尾页" />
            </div>
        </PagerTemplate>
        <RowStyle BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
    </asp:GridView>
    &nbsp;<br />
</div>
<div class="right">
    <br />优秀作品榜<br />
    <div style="width: 98%">
    <ul>
    <asp:Repeater ID="RepMy" runat="server" >
    <ItemTemplate>
    <li class="licss1"><a href='<%#Viewurl(Eval("Aurl").ToString())%>'  target="_blank"><%#Strcut( Eval("Ftitle").ToString())%></a></li>
    </ItemTemplate>
    <AlternatingItemTemplate>
    <li class="licss2"><a href='<%#Viewurl(Eval("Aurl").ToString())%>'  target="_blank"><%#Strcut( Eval("Ftitle").ToString())%></a></li>
    </AlternatingItemTemplate>
    </asp:Repeater>
    </ul>
    </div>
    <br />
    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="txts20center" 
        Height="20px" NavigateUrl="~/Student/autonomic.aspx" SkinID="HyperLinkGray" 
        Width="80px" Target="_self">在线资源</asp:HyperLink>
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
