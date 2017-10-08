<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Stud.master" StylesheetTheme="Student"
    AutoEventWireup="true" CodeFile="downfile.aspx.cs" Inherits="Student_downfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cphs" runat="Server">
    <div id="student">
        <div class="left">
            <br />
            <asp:Label ID="Labeltitle" runat="server" SkinID="LabelLightBlue" Width="98%" CssClass="txts24center"
                Height="24px"></asp:Label>
            <br />
            <div style="padding: 2px; margin: auto; border-bottom-style: dashed; border-width: 1px;
                border-color: #CCCCCC">
                属性：<asp:Label ID="Labelclass" runat="server" SkinID="LabelFileShow"></asp:Label>
                格式：<asp:Image ID="ImageType" runat="server" />
                <asp:Label ID="Labelfiletype" runat="server" SkinID="LabelFileShow"></asp:Label>
                点击率：<asp:Label ID="Labelhit" runat="server" SkinID="LabelFileShow"></asp:Label>
                更新日期：<asp:Label ID="Labeldate" runat="server" SkinID="LabelFileShow"></asp:Label>
                学分：<asp:Label ID="Labelopen" runat="server" SkinID="LabelFileShow"></asp:Label>
                <asp:Label ID="LabelFyid" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="LabelFid" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="LabelSid" runat="server" Visible="False"></asp:Label>
            </div>
            <center>
                <div>
                    <br />
                    <div class="downcontent">
                        <asp:Literal ID="Labelcontent" runat="server"></asp:Literal>
                    </div>
                    <br />
                    <br />
                </div>
            </center>
            <br />
            <asp:Label ID="Labelmsg" runat="server"></asp:Label>
            <br />
            <asp:Image ID="ImageDown" runat="server" ImageUrl="~/Images/down1.gif" />
            <asp:LinkButton ID="LBtnfile" runat="server" OnClick="LBtnfile_Click" Visible="False"
                Font-Underline="False" BorderColor="#7DBF80" BorderStyle="Dashed" BorderWidth="1px"
                CssClass="txtszcenter" Height="18px" BackColor="#E2F3E3" Width="80px">点击下载</asp:LinkButton>
            <br />
            <asp:HyperLink ID="HLurl" runat="server" Visible="false"></asp:HyperLink>
            <br />
        </div>
        <div class="right">
            <div style="width: 170px">
                <br />
                <asp:GridView ID="GVSoft" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    OnPageIndexChanging="GVSoft_PageIndexChanging" OnRowDataBound="GVSoft_RowDataBound"
                    Width="98%" SkinID="GridViewInfo" EnableModelValidation="True" CellPadding="4">
                    <AlternatingRowStyle BackColor="#E9EFF5" />
                    <Columns>
                        <asp:TemplateField HeaderText="标题">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("Fid", "downfile.aspx?Fid={0}") %>'
                                    Text='<%# strcut( Eval("Ftitle").ToString()) %>' ToolTip='<%# Eval("Ftitle")%>'>
                                </asp:HyperLink>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Width="280px" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle HorizontalAlign="Center" />
                    <HeaderStyle Height="24px" BackColor="#DEE7F1" BorderColor="LightSteelBlue" BorderStyle="Solid"
                        BorderWidth="1px" />
                    <PagerStyle HorizontalAlign="Center" Font-Size="9pt" />
                    <PagerTemplate>
                        <div>
                            <asp:LinkButton ID="btnFirst" runat="server" CausesValidation="False" CommandArgument="First"
                                CommandName="Page" Font-Underline="False" ForeColor="Black" Text="首页" />&nbsp;
                            <asp:LinkButton ID="btnPrev" runat="server" CausesValidation="False" CommandArgument="Prev"
                                CommandName="Page" Font-Underline="False" ForeColor="Black" Text="上页" />&nbsp;
                            <asp:LinkButton ID="btnNext" runat="server" CausesValidation="False" CommandArgument="Next"
                                CommandName="Page" Font-Underline="False" ForeColor="Black" Text="下页" />&nbsp;
                            <asp:LinkButton ID="btnLast" runat="server" CausesValidation="False" CommandArgument="Last"
                                CommandName="Page" Font-Underline="False" ForeColor="Black" Text="尾页" />
                        </div>
                    </PagerTemplate>
                    <RowStyle Height="30px" />
                </asp:GridView>
                <br />
                <asp:Image runat="server" ID="upFileType" Visible="False" />
                <asp:HyperLink ID="upFileUrl" runat="server" Height="16px" Visible="False" Target="_blank">[upFileUrl]</asp:HyperLink>
                <br />
                <br />
                <asp:Panel ID="Panelswfupload" runat="server">
                    <link href="../kindeditor/themes/me/me.css" rel="stylesheet" type="text/css" />
                    <script type="text/javascript" charset="utf-8" src="../kindeditor/kindeditor-min.js"></script>
                    <script type="text/javascript" charset="utf-8" src="../kindeditor/lang/zh_CN.js"></script>
                    <div id="swfu_container" style="margin: 0px 10px;">
                        <div style="text-align: center; margin: auto">
                            <script type="text/javascript">
                                KindEditor.ready(function (K) {
                                    var uploadbutton = K.uploadbutton({
                                        button: K('#uploadButton')[0],
                                        fieldName: 'imgFile',
                                        url: 'autoupload.aspx?yid=<%=LabelFyid.Text %>&fid=<%=LabelFid.Text %>&sid=<%=LabelSid.Text %>',
                                        afterUpload: function (data) {
                                            if (data.error == 0) {
                                                alert(data.message);
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
                            </script>
                            <div style="text-align: center">
                                <input type="button" id="uploadButton" value="作品保存" style="text-align: center" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <br />
                <br />                
                <br />
                <br />
                <asp:HyperLink ID="Hltonomic" runat="server" ImageUrl="~/Images/nomic.gif" NavigateUrl="~/Student/autonomic.aspx"
                    Target="_blank" BorderStyle="None">作品园</asp:HyperLink>
                <br />
                <br />
            </div>
            <div>
                <link href="../Js/tinybox.css" rel="stylesheet" type="text/css" />
                <script src="../Js/tinybox.js" type="text/javascript"></script>
                <script type="text/javascript">
                    function showShare() {
                        var urlat = "../Student/groupshare.aspx";
                        TINY.box.show({ iframe: urlat, boxid: 'frameless', width: 600, height: 400, fixed: false, maskopacity: 60, close: true })
                    }   
                </script>
            </div>
        </div>
    </div>
</asp:Content>
