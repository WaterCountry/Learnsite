<%@ page language="C#" autoeventwireup="true" inherits="python_match, LearnSite" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>比赛</title>
    <link href="../js/bootstrap.min.css" rel="stylesheet" type="text/css" />
	<style>
		body{			
			margin:10px;
		}
        .txtcenter{
             text-align:center;
        }
	</style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container ">	
    <div class="row">
        <div class="col-md-4">
            <a class="btn btn-default" href="index.aspx" >首页</a>&nbsp;&nbsp;      	          
            <a class="btn btn-default" href="match.aspx" >比赛</a>&nbsp;&nbsp; 
        </div>
        <div class="col-md-4">
        </div>
        <div class="col-md-4">
            <p style=" text-align:right;">
                <asp:HyperLink ID="Hlmatch" class="btn btn-default" runat="server" NavigateUrl="~/python/matchnew.aspx">创建比赛</asp:HyperLink>
            </p>  
        </div>
    </div>
            <hr />
    <div class="row">
            <asp:GridView ID="GVMatch" runat="server" AllowPaging="True"
                AutoGenerateColumns="False"  DataKeyNames="Mhid" Width="80%" CellPadding="6" 
                EnableModelValidation="True"  ForeColor="#111111" GridLines="None" 
                HorizontalAlign="Center" onpageindexchanging="GVMatch_PageIndexChanging" 
                onrowdatabound="GVMatch_RowDataBound" >
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField HeaderText="编号" HeaderStyle-CssClass="txtcenter">
                    <HeaderStyle VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:BoundField>
                    <asp:HyperLinkField DataNavigateUrlFields="Mid" 
                        DataNavigateUrlFormatString="question.aspx?mid={0}" 
                        DataTextField="Mtitle" HeaderText="标题" />
                    <asp:TemplateField >
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLinkEdit" runat="server" 
                                NavigateUrl='<%# Eval("Mid", "matchshow.aspx?mid={0}") %>' Text="编辑"></asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle Width="60px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="Mdate" HeaderText="日期" HeaderStyle-CssClass="txtcenter">
                    <ItemStyle Width="160px" />
                    </asp:BoundField>
                </Columns>
                <FooterStyle BackColor="#E7E7E7" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#9EA9B1" Font-Bold="True" ForeColor="#111111" 
                    VerticalAlign="Middle" />
                <PagerStyle BackColor="#EFEFEF" ForeColor="#111111" HorizontalAlign="Center" />
                <pagertemplate>
                    <div style="width:100%; height:13px; text-align:right">
                        第<asp:Label ID="lblPageIndex" runat="server" 
                            text="<%# ((GridView)Container.Parent.Parent).PageIndex + 1  %>" />
                        页 共<asp:Label ID="lblPageCount" runat="server" 
                            text="<%# ((GridView)Container.Parent.Parent).PageCount  %>" />
                        页 
                        <asp:LinkButton ID="btnFirst" runat="server" causesvalidation="False" 
                            commandargument="First" commandname="Page" Font-Underline="False" 
                            ForeColor="Black" text="首页" />
                        <asp:LinkButton ID="btnPrev" runat="server" causesvalidation="False" 
                            commandargument="Prev" commandname="Page" Font-Underline="False" 
                            ForeColor="Black" text="上一页" />
                        <asp:LinkButton ID="btnNext" runat="server" causesvalidation="False" 
                            commandargument="Next" commandname="Page" Font-Underline="False" 
                            ForeColor="Black" text="下一页" />
                        <asp:LinkButton ID="btnLast" runat="server" causesvalidation="False" 
                            commandargument="Last" commandname="Page" Font-Underline="False" 
                            ForeColor="Black" text="尾页" />
                    </div>
                </pagertemplate>
                <RowStyle BackColor="#F3F3F3" Height="50px" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            </asp:GridView>
    </div>
    </div>
    </form>
</body>
</html>
