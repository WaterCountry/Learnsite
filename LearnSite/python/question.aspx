<%@ page language="C#" autoeventwireup="true" inherits="python_question, LearnSite" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../js/bootstrap.min.css" rel="stylesheet" type="text/css" />
	<style>
		body{			
			margin:10px;
		}
        .mapimg{
	        max-height:60px;
			margin:4px;
        }
        .mapimg:hover{
			background-color: #fef8de;
			box-shadow:2px 2px 8px #666;
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
        <div class="col-md-8">
            <a class="btn btn-default" href="index.aspx" >首页</a>&nbsp;&nbsp;         	          
            <a class="btn btn-default" href="match.aspx" >比赛</a>&nbsp;&nbsp; 
        </div>
        <div class="col-md-4"> 
            <p style=" text-align:right;">  
                <asp:Image ID="Imageface" runat="server" Height="50px" />
            </p>
        </div>
    </div>
            <hr />
    <div class="row">   
       <div style=" text-align:center;" >
               <h3><asp:Label ID="Labeltitle" runat="server" ></asp:Label></h3>
		 <asp:Label ID="Labeldate" runat="server" ></asp:Label>
    <div style="margin: auto; width: 90%; ">
         <p style="text-align:right;">
             <asp:Button ID="Buttonrank" runat="server"
             SkinID="BtnNormal" Text="排行" onclick="Buttonrank_Click" />             
             &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 
             <asp:Button ID="Btnreturn" runat="server" onclick="Btnreturn_Click" 
             SkinID="BtnNormal" Text="返回" />
		 </p>
    </div>
    <div>
              <asp:GridView ID="GVProblem" runat="server"  
                            AutoGenerateColumns="False"   Width="90%" CellPadding="4" CellSpacing="2"  Font-Size="11pt" 
                        EnableModelValidation="True" HorizontalAlign="Center" 
                        onrowdatabound="GVProblem_RowDataBound" DataKeyNames="Qid" >
                            <Columns>
                                <asp:BoundField HeaderText="序号" HeaderStyle-CssClass="txtcenter">
                                <ItemStyle Width="40px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="绘制图形" HeaderStyle-CssClass="txtcenter">
                                    <ItemTemplate>
                                        <asp:Image ID="Imageurl" runat="server" CssClass="mapimg" ImageUrl='<%# Eval("Qurl") %>'  />
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="题目描述" HeaderStyle-CssClass="txtcenter">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelPtitle" runat="server" Text='<%# HttpUtility.HtmlDecode(DataBinder.Eval(Container.DataItem,"Qtitle").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="600px" />
                                </asp:TemplateField>                                
                                <asp:TemplateField HeaderText="编写代码" HeaderStyle-CssClass="txtcenter">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLinkPid" runat="server"  Text="开始"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="状态" HeaderStyle-CssClass="txtcenter">
                                    <ItemTemplate>
                                        <asp:Image ID="Imagedone" runat="server"  ImageUrl="~/python/thumbnail/none.png" />
                                    </ItemTemplate>
                                    <ItemStyle Width="60px" />
                                </asp:TemplateField>
                            </Columns>                            
                            <HeaderStyle VerticalAlign="Middle" />
                        </asp:GridView>
                    </div>
                <br />
                <br />
        </div>
    
       </div>
    </div>
    </form>
</body>
</html>
