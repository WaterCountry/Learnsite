<%@ page language="C#" autoeventwireup="true" inherits="Student_groupshare, LearnSite" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
    .by {margin: 0px;background-color: #E6F0E7}
    .disk{margin: auto; text-align: center; width: 600px; font-size: 11pt; font-family: 宋体, Arial, Helvetica, sans-serif;}
    .dhead{border-width: 1px; border-color: #CCCCCC; padding: 2px; background-color: #CFE4D0; border-bottom-style: solid;}
    .dcontext{margin: auto; padding: 2px;background-color: #FFFFDD; height: 340px; overflow-x: hidden;}
    .dfile{border-width: 1px; border-color: #E6E8E6; border-bottom-style: dashed; text-align: left; width:270px;}
    .txt{ line-height:16px; }
    .leftcss{ float:left; left:10px; width:12px;margin:auto;}
    .rightcss{ float:right; right:2px;width:88%;}
    </style>
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script src="../js/dropzone/dropzone-min.js" type="text/javascript"></script>
</head>
<body  class="by">
    <form id="form1" class="dropzone" runat="server">
    <div >
    <div id="doc_area" class="disk" title="请将文件拖放到这里上传">
    <div class="dhead">
        <asp:Label ID="Labeltitle" runat="server" Font-Bold="True" Font-Size="11pt"></asp:Label>
        </div>
        <div  class="dcontext"> 
                <asp:DataList ID="Dlfilelist" runat="server" 
                    RepeatColumns="2" RepeatDirection="Horizontal" CellPadding="3" 
                    CellSpacing="3" Width="99%" 
                    HorizontalAlign="Center" onitemcommand="Dlfilelist_ItemCommand" 
                    onitemdatabound="Dlfilelist_ItemDataBound" >
                    <ItemTemplate>
                        <div class="dfile"> 
                           <div>
                            <asp:Image ID="Imageext" runat="server" ImageUrl='<%# Eval("Kftpe") %>' />
                            <asp:HyperLink ID="HLfname" runat="server" NavigateUrl='<%# Eval("Kfurl") %>' Target="_blank" Text='<%# Eval("KfnameShort") %>' Font-Underline="False"></asp:HyperLink>&nbsp;                            
                           </div>
                           <div style=" font-size:8pt;";>
                            <asp:Label ID="Labelfsize" runat="server" Text='<%# Eval("Kfsize") %>' ToolTip='<%# Eval("Kfdate") %>'   ></asp:Label>							
                            <asp:Label ID="Labelfdate" runat="server" Text='<%# Eval("Kfdate") %>'  ></asp:Label> 
                            <asp:ImageButton ID="ImgBtnDelete" runat="server" CommandArgument='<%# Eval("Kfurl") %>' 
                                CommandName="D" ImageUrl="~/images/delete.gif" ToolTip="删除" />
                            </div>
                        </div>
                    </ItemTemplate>
                    <SeparatorStyle BorderColor="Silver" BorderStyle="Dotted" BorderWidth="1px" />
                </asp:DataList>
        
        </div>
		<div style="padding:8px;margin:auto;">
        <div id="dleft" class="leftcss">
            <asp:Image ID="Imagedisk" runat="server" Height="25px" Width="25px" 
                ImageUrl="~/images/diskgreen.gif" />
        </div>
        <div id="dright" class="rightcss">   
        <div style="text-align: left">
         <asp:Button ID="BtnStu" runat="server" BackColor="#CFE4D0" BorderStyle="None" 
             Font-Bold="False" Font-Size="9pt" onclick="BtnStu_Click" Text="我的网盘" />
&nbsp;<asp:Button ID="BtnGroup" runat="server" BackColor="#CFE4D0" BorderStyle="None" 
             Font-Bold="False" Font-Size="9pt" onclick="BtnGroup_Click" Text="小组网盘" />
         <asp:CheckBox ID="CkIsGroup" runat="server" Enabled="False" Visible="False" />
&nbsp; <asp:Label ID="Labeldisk" runat="server" Font-Size="9pt" ForeColor="#3F6159"></asp:Label>
        </div>
        </div>  
		<div>
     </div>    
    </div>
    </form>
    <script type="text/javascript" >
    //acceptedFiles: ".txt,.pdf,.doc,.docx,.xlsx,.xls,.ppt,.pptx,.png,.jpg,.jpeg,.gif,.mp4,.py,.wav,.mp3,.psd,.fla,.rar",
        var isgroup = "<%=isgroup %>";
        var can = "<%=can %>";
        var urlstr = "share.ashx?isgroup=" + isgroup;
        if (can == "True") {
            $("#doc_area").dropzone({
                url: urlstr,       
                method: "POST",
                addRemoveLinks: true,
                maxFiles: 1,
                maxFilesize: 30,
                uploadMultiple: false,
                parallelUploads: 100,
                previewsContainer: false,
                success: function (file, response, e) {
                    alert(response);
                    location.reload();
                }
            });
        }
    </script>
</body>
</html>
