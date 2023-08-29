<%@ page language="C#" autoeventwireup="true" inherits="python_matchshow, LearnSite" %>

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
	        filter:invert(30%);
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
        </div>
    </div>
            <hr />
    <div class="row">   
       <div style=" text-align:center;" >
               <h3><asp:Label ID="Labeltitle" runat="server" ></asp:Label></h3>
                <br />

    <div style="margin: auto; width: 90%; ">
         <asp:Button ID="Btnadd" runat="server" onclick="Btnadd_Click" 
             SkinID="BtnNormal" Text="添加试题" />
					&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         <asp:Button ID="Btnreturn" runat="server" onclick="Btnreturn_Click" 
             SkinID="BtnNormal" Text="返回" />
		 <p style="text-align:right;">
		 <asp:Label ID="Labeldate" runat="server" ></asp:Label>
            <asp:CheckBox ID="Checkcpublish" runat="server" Text="已发布"  Checked="True" Enabled="False" />
		 </p>
    </div>
    <div>
              <asp:GridView ID="GVProblem" runat="server"  
                            AutoGenerateColumns="False"   Width="90%" CellPadding="4" CellSpacing="2"  Font-Size="11pt" 
                        EnableModelValidation="True" HorizontalAlign="Center" 
                        onrowcommand="GVProblem_RowCommand" 
                        onrowdatabound="GVProblem_RowDataBound" DataKeyNames="Qid" 
                  >
                            <Columns>
                                <asp:BoundField HeaderText="序号">
                                <HeaderStyle Width="40px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="绘制图形">
                                    <ItemTemplate>
                                        <asp:Image ID="Imageurl" runat="server" CssClass="mapimg" ImageUrl='<%# Eval("Qurl") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="题目描述">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelPtitle" runat="server" Text='<%# HttpUtility.HtmlDecode(DataBinder.Eval(Container.DataItem,"Qtitle").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="600px" />
                                </asp:TemplateField>

                                
                             <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="ImageBtnTop" runat="server" CausesValidation="False" 
                                        CommandName="Top"  CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                        Text="上" ToolTip="向上移" Font-Underline="False"></asp:LinkButton>
                                </ItemTemplate>
                                 <ItemStyle Width="20px" />
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="ImageBtnBottom" runat="server" CausesValidation="False" 
                                        CommandName="Bottom"  CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                        Text="下" ToolTip="向下移" Font-Underline="False"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="20px" />
                            </asp:TemplateField>

                                <asp:BoundField DataField="Qscore" HeaderText="分值">
                                <HeaderStyle Width="40px" />
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLinkPid" runat="server"  Text="编辑"></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle Width="40px" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="BtnDel" runat="server" CausesValidation="false" 
                                          CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="Del" Text="删除"></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="40px" />
                                </asp:TemplateField>
                            </Columns>                            
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
