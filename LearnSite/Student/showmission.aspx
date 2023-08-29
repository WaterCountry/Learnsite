<%@ page title="" language="C#" masterpagefile="~/student/Scm.master" autoeventwireup="true" stylesheettheme="Student" inherits="Student_showmission, LearnSite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cpcm" Runat="Server">
<div  id="showcontent">
<br />
    <div   class="missiontitle">
    <asp:Label ID="LabelMtitle"  runat="server" ></asp:Label><br />
   </div>
   <div class="courseother">
    <asp:Label ID="LabelSnum"  runat="server" Visible="False"></asp:Label>
			<asp:CheckBox ID="CkMupload" runat="server" Enabled="false" Visible="False" />
            <asp:CheckBox ID="CkMgroup" runat="server" Enabled="false" Visible="False" />
            <asp:Label ID="LabelMid" runat="server" Visible="False"></asp:Label>            
            <asp:Label ID="LabelUploadType" runat="server" Visible="False"></asp:Label>
			<asp:Label ID="LabelMcid" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="LabelMsort"  runat="server" Visible="False"></asp:Label>
            <asp:Label ID="LabelLid"  runat="server" Visible="False"></asp:Label>
   </div>   
<div   id="Mcontent"  class="taskcontent" runat="server">	
</div>
		<br />
		<br />

<div class="sidebar"><br />
<center>    
    <link href="../kindeditor/themes/me/me.css" rel="stylesheet" type="text/css" />
    <script charset="utf-8" src="../kindeditor/kindeditor-min.js"></script>
		<script charset="utf-8" src="../kindeditor/lang/zh_CN.js"></script>
        <div ><br /></div><br />
        <input type="button" class="sharedisk" id="share" value="我的网盘" onclick="showShare()" />
        <br />
        <br />
            <asp:HyperLink  ID ="VoteLink" runat="server" Target="_blank" 
                CssClass="txtszcenter" SkinID="HyperLinkPink">作品互评</asp:HyperLink>        
        <br />
     <asp:Panel ID="Panelworks" runat="server" >
        <div>
<br />
            <asp:Image runat="server" ID="upFileType" Visible="False" />
            <asp:HyperLink ID="upFileUrl" runat="server" Height="16px" Visible="False" 
                Target="_blank">[upFileUrl]</asp:HyperLink>
        <br /> 
            <br />
            <asp:Panel ID="Panelswfupload" runat="server">
            <div id="swfu_container" style="margin: 0px 10px;">
		    <center>
        <script type="text/javascript">
                var lid = "<%=LabelLid.Text %>";
                var urlstr = "uploadworkm.aspx?lid=" + lid;
                KindEditor.ready(function (K) {

                    var uploadbutton = K.uploadbutton({
                        button: K('#uploadButton')[0],
                        fieldName: 'imgFile',
                        url: urlstr,
                        afterUpload: function (data) {
                            if (data.error === 0) {
                                alert("作品已经提交成功！");
                                OfficeToPng();
                                location.reload();
                            } else {
                                alert(data.message);
                            }
                        },
                        afterError: function (str) {
                            alert('出错信息: ' + str);
                        }
                    });
                    uploadbutton.fileBox.change(function (e) {
                        uploadbutton.submit();
                    });
                });


                //自动保存成绩
                function OfficeToPng() {
                    var mid = "<%=LabelMid.Text %>";
                    var num = "<%=LabelSnum.Text %>";
                    console.log("文档转图片调用开始");
                    var formData = new FormData();
                    formData.append('mid', mid);
                    formData.append('num', num);
                    var saveurl = "spire.ashx";
                    $.ajax({
                        url: saveurl,
                        type: "POST",
                        cache: false,
                        data: formData,
                        dataType: "html",
                        processData: false,
                        contentType: false
                    }).done(function (res) {
                        console.log(res);
                    }).fail(function (res) {
                        console.log("保存失败");
                    });
                }

	        </script>
				<input type="button" id="uploadButton" value="作品提交" />
		    </center>
	    </div>
            </asp:Panel>
            <br />
            <asp:Image ID="ImageType" runat="server" />
            <asp:Label ID="LabelMfiletype" runat="server"></asp:Label>
            格式<br />
    <asp:Label ID="Labelmsg" runat="server"  SkinID="LabelMsgRed"></asp:Label>
            <br />
    <br />
    </div>       
    </asp:Panel>
    <asp:Panel ID="Panelgroup" runat="server">     
        <br />
        <asp:GridView ID="GVgwork" runat="server" 
            AutoGenerateColumns="False" CellPadding="3" DataKeyNames="wid" 
            EnableModelValidation="True" 
            OnRowCommand="GVgwork_RowCommand" 
            onrowdatabound="GVgwork_RowDataBound" PageSize="15" SkinID="GridViewInfo" 
            Width="90%" Caption="小组合作面板">
            <Columns>
                <asp:TemplateField HeaderText="作品">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLinkWurl" runat="server" Target="_blank" Text='<%# Eval("Sname") %>' 
                            ToolTip='<%# Eval("Wurl") %>'></asp:HyperLink>
                    </ItemTemplate>
                    <ControlStyle Width="60px" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("Wlscore") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField  HeaderText="评" ShowHeader="False">
                    <ControlStyle Width="16px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButtonA" runat="server" CausesValidation="false" 
                            CommandArgument='<%# Bind("wid") %>' CommandName="A" Text="A"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField  HeaderText="价" ShowHeader="False">
                    <ControlStyle Width="16px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButtonP" runat="server" CausesValidation="false" 
                            CommandArgument='<%# Bind("wid") %>' CommandName="P" Text="P"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ControlStyle Width="16px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButtonE" runat="server" CausesValidation="false" 
                            CommandArgument='<%# Bind("wid") %>' CommandName="E" Text="E"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>                
            </Columns>
            
        </asp:GridView>
        <br />
        <asp:Image ID="upFileTypeGroup" runat="server" Visible="False" />
        <asp:HyperLink ID="upFileUrlGroup" runat="server" Height="16px" Target="_blank" 
            Visible="False">[upFileUrlGroup]</asp:HyperLink>
        <br /><br />
         <asp:Panel ID="PanelGroupUp" runat="server">
        <div id="swfu_containerTwo" style="margin: 0px 10px;">
		    <center>
            <script type="text/javascript">
                var lid = "<%=LabelLid.Text %>";
                var gurlstr = "uploadgroupm.aspx?lid=" + lid;
                KindEditor.ready(function (K) {
                    var uploadgroupbutton = K.uploadbutton({
                        button: K('#uploadgroupButton')[0],
                        fieldName: 'imgFilegroup',
                        url: gurlstr,
                        afterUpload: function (data) {
                            if (data.error === 0) {
                                alert("小组作品已经提交成功！");
                                location.reload(true);//重新刷新ctrl+F5
                            } else {
                                alert(data.message);
                            }
                        },
                        afterError: function (str) {
                            alert('出错信息: ' + str);
                        }
                    });
                    uploadgroupbutton.fileBox.change(function (e) {
                        uploadgroupbutton.submit();
                    });
                });
	        </script>
				<input type="button" id="uploadgroupButton" value="小组合作" />
		    </center>
	    </div>
        </asp:Panel>
        <br />
       <asp:Label ID="Labelgroupmsg" runat="server"  SkinID="LabelMsgRed"></asp:Label>
    <br /> 
    </asp:Panel>
    <br /> 
    </center>
</div>   
    <br />
        <link href="../js/tinybox.css" rel="stylesheet" type="text/css" />
        <script src="../js/tinybox.js" type="text/javascript"></script>
    <script type="text/javascript">
        function jsCopy(contentid) {
            var e = document.getElementById(contentid); //对象是content 
            e.select(); //选择对象 
            document.execCommand("Copy"); //执行浏览器复制命令 
        }
        function showShare() {
            var urlat = "../student/groupshare.aspx";
            TINY.box.show({ iframe: urlat, boxid: 'frameless', width: 600, height: 400, fixed: false, maskopacity: 60, close: true })
        }   
    </script>
</div>
</asp:Content>

