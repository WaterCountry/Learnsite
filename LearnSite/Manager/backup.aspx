<%@ Page Title="" Language="C#" MasterPageFile="~/Manager/Manage.master" AutoEventWireup="true" CodeFile="backup.aspx.cs" Inherits="Manager_backup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <div class="manageplace" >
        <div style="border: 1px solid #EEEEEE; width: 700px;  margin: auto; ">
            <div style="border: 1px solid #EEEEEE; height: 18px; background-color: #EEEEEE;  margin: auto;">
                数据库备份与恢复</div>
            <br />
            操作说明<br />
            <br />
            <div style="border: 1px solid #E7E7E7; width: 597px; text-align: left; background-color: #EAEAEA;  margin: auto;">
                &nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp; 
                <br />
&nbsp;&nbsp;&nbsp; 数据库备份以当前短日期+时+分为文件名保存到网站下BackupDb文件夹中。注意：恢复前一定要备份当前的<br />
                <br />
                &nbsp;数据库后再进行，并自行妥善保存好备份的数据库。<br />
                <br />
&nbsp;&nbsp;&nbsp; 恢复功能的使用条件：网站的数据库的账号和密码跟master一致，并且要恢复的备份跟原数据库名称相同，<br />
                <br />
                &nbsp;否则就会出错。如果不一致，那么请参考说明必读中的还原数据库截图演示图片，进行手工恢复。<br />
                &nbsp;&nbsp;&nbsp; 
                <br />
&nbsp;&nbsp;&nbsp; 网站迁移说明：将原网站文件夹复制到新电脑的（除Ｃ盘外）分区中，去只读并加everyone可读写权限，并<br />
                <br />
                &nbsp;将数据库文件复制过去附加到数据库服务器中，同时修改web.config中的数据库连接字符串。<br />
                &nbsp;<br />
            </div>
            <br />
                    <asp:Button ID="Btnbackup" runat="server" BackColor="#E6E6E6" 
                        BorderColor="#D4D4D4" BorderWidth="1px" Font-Names="Arial" 
                Font-Size="9pt" Text="开始备份" Width="80px" Height="20px" 
                onclick="Btnbackup_Click" />
                    <br />
            <br />
        <asp:DataList ID="DlDbBackup" runat="server" 
                    RepeatColumns="3" RepeatDirection="Horizontal" Caption="当前数据库备份列表"  
                CaptionAlign="Left" CellPadding="3" CellSpacing="3" 
                onitemdatabound="DlDbBackup_ItemDataBound" 
                onitemcommand="DlDbBackup_ItemCommand" >
                    <ItemTemplate>
                        <div  style="border-width: thin; border-color: #C8D5CC; background-color: #F4FCF3; border-bottom-style: solid; text-align: left;">
                            <asp:Label ID="Labelfid" runat="server" Text='<%# Eval("fid") %>' BackColor="#EBF3ED"></asp:Label>&nbsp;
                            <asp:HyperLink ID="HLfname" runat="server" Target="_blank" Text='<%# Eval("fname") %>' ></asp:HyperLink>&nbsp;                            
                            <asp:Label ID="Labelfsize" runat="server" Text='<%# Eval("fsize") %>' ></asp:Label>
                            <asp:Label ID="Labelfread" runat="server" Text='<%#  Eval("fread") %>'  ToolTip="是否只读（T：只读 | F：可写）"  ForeColor="#00A279"></asp:Label>
                            <asp:Label ID="Labelurl" runat="server" Text='<%# Eval("furl") %>' Visible="false" ></asp:Label>
                            <asp:ImageButton ID="ImgBtnReStore" runat="server"  CommandName="ReStore" 
                                ImageUrl="~/Images/works.gif" ToolTip="提示：将当前数据库恢复到该备份日期状态" 
                                CommandArgument='<%# Eval("furl") %>' />
                        </div>
                    </ItemTemplate>
                    <SeparatorStyle BorderColor="Silver" BorderStyle="Dotted" BorderWidth="1px" />
                </asp:DataList>
            <br />
            <br />
            <div id="Loading" style=" display:none ;text-align: center; font-family: 宋体, Arial, Helvetica, sans-serif; font-size: 9pt; color: #FF0000;">
            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/load2.gif" />
            <input id="Textcmd" style="border-style: none" type="text" /></div>
            <br />
        </div>
        <br />
        <asp:Label ID="Labelmsg" runat="server" ForeColor="Red"></asp:Label>
        <br />
        <br />
</div>
</asp:Content>

