<%@ Page Language="C#" AutoEventWireup="true" CodeFile="groupshare.aspx.cs" Inherits="Student_groupshare" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
    .by {margin: 0px;background-color: #E6F0E7}
    .disk{margin: auto; text-align: center; width: 600px; font-size: 9pt; font-family: 宋体, Arial, Helvetica, sans-serif;}
    .dhead{border-width: 1px; border-color: #CCCCCC; padding: 2px; background-color: #CFE4D0; border-bottom-style: solid;}
    .dcontext{margin: auto; padding: 2px;background-color: #FFFFDD; width: 580px; height: 320px; overflow-x: hidden;
               overflow-y: auto;scrollbar-face-color:#CFE4D0;scrollbar-3dlight-color:#E6F0E7;scrollbar-highlight-color:#E6F0E7;
	scrollbar-shadow-color:#E6F0E7;scrollbar-darkshadow-color:#E6F0E7;scrollbar-track-color:#FFFFDD;}
    .dfile{border-width: 1px; border-color: #E6E8E6; border-bottom-style: dashed; text-align: left; width:180px;}
    .txt{ line-height:16px; }
    .leftcss{ float:left; left:2px; width:12px;}
    .rightcss{ float:right; right:2px;width:88%;}
    </style>
</head>
<body  class="by">
    <form id="form1" runat="server">
    <div >
    <div  class="disk">
    <div class="dhead">
        <asp:Label ID="Labeltitle" runat="server" Font-Bold="True" Font-Size="11pt"></asp:Label>
        </div>
        <div  class="dcontext"> 
                <asp:DataList ID="Dlfilelist" runat="server" 
                    RepeatColumns="3" RepeatDirection="Horizontal" CellPadding="3" 
                    CellSpacing="3" Width="99%" 
                    HorizontalAlign="Center" onitemcommand="Dlfilelist_ItemCommand" 
                    onitemdatabound="Dlfilelist_ItemDataBound" >
                    <ItemTemplate>
                        <div class="dfile"> 
                           <div>
                            <asp:Image ID="Imageext" runat="server" ImageUrl='<%# Eval("ftype") %>' />
                            <asp:HyperLink ID="HLfname" runat="server" NavigateUrl='<%# Eval("furl") %>' Target="_blank" Text='<%# Eval("fname") %>' Font-Underline="False"></asp:HyperLink>&nbsp;                            
                           </div>
                           <div>
                            <asp:Label ID="Labelfsize" runat="server" Text='<%# Eval("fsize") %>' ToolTip='<%# Eval("fdate") %>' ></asp:Label>                        
                            <asp:Label ID="Labelfdate" runat="server" Text='<%# Eval("fdate") %>' ></asp:Label> 
                            <asp:ImageButton ID="ImgBtnDelete" runat="server" CommandArgument='<%# Eval("furl") %>' 
                                CommandName="D" ImageUrl="~/Images/delete.gif" ToolTip="删除" />
                            </div>
                        </div>
                    </ItemTemplate>
                    <SeparatorStyle BorderColor="Silver" BorderStyle="Dotted" BorderWidth="1px" />
                </asp:DataList>
        
        </div>
        <div id="dleft" class="leftcss">
            <asp:Image ID="Imagedisk" runat="server" Height="50px" Width="60px" 
                ImageUrl="~/Images/diskgreen.gif" />
        </div>
        <div id="dright" class="rightcss">
        <div style="padding: 2px; height:24px; text-align: right;">        
            <asp:FileUpload ID="Fupload" runat="server" Font-Size="9pt" />
            &nbsp;<asp:Button ID="Btnupload" runat="server" Font-Size="9pt" 
                Text="保存" Width="60px" onclick="Btnupload_Click" BackColor="#A4CCA6" BorderStyle="None" 
                TabIndex="99" CssClass="txt" />&nbsp;&nbsp;
    </div>    
        <div style="text-align: left">
         <asp:Button ID="BtnStu" runat="server" BackColor="#CFE4D0" BorderStyle="None" 
             Font-Bold="False" Font-Size="9pt" onclick="BtnStu_Click" Text="我的网盘" />
&nbsp;<asp:Button ID="BtnGroup" runat="server" BackColor="#CFE4D0" BorderStyle="None" 
             Font-Bold="False" Font-Size="9pt" onclick="BtnGroup_Click" Text="小组网盘" />
         <asp:CheckBox ID="CkIsGroup" runat="server" Enabled="False" Visible="False" />
&nbsp; <asp:Label ID="Labeldisk" runat="server" Font-Size="9pt" ForeColor="#3F6159"></asp:Label>
        </div>
        </div>        
     </div>    
    </div>
    </form>
</body>
</html>
