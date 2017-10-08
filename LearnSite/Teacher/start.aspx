<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master" StylesheetTheme="Teacher" AutoEventWireup="true" CodeFile="start.aspx.cs" Inherits="Teacher_start" %>
<%@ Register assembly="Anthem" namespace="Anthem" tagprefix="anthem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <div  class="placehold" >
    <div  class="startdiv">
        上课选择<asp:DropDownList ID="DDLgrade" 
            runat="server" Font-Size="9pt" 
            Width="40px" EnableTheming="True" AutoPostBack="True" 
            onselectedindexchanged="DDLgrade_SelectedIndexChanged">
        </asp:DropDownList>
        年级<asp:DropDownList ID="DDLclass" runat="server" Font-Size="9pt" Width="40px" 
            AutoPostBack="True" onselectedindexchanged="DDLclass_SelectedIndexChanged">
        </asp:DropDownList>
        班 <asp:DropDownList ID="DDLCid" runat="server" Font-Size="9pt" Width="180px" 
            AutoPostBack="True" onselectedindexchanged="DDLCid_SelectedIndexChanged">
        </asp:DropDownList>
        &nbsp;<asp:Button ID="Btnset" runat="server" Text="开始上课"  SkinID="BtnSmall"
            onclick="Btnset_Click" ToolTip="设置上课班级登录密码" BackColor="#9BCBFF" />
            &nbsp;<asp:Button ID="Btnstudent" runat="server" Text="模拟学生"  
            SkinID="BtnSmall" ToolTip="模拟本班级学生角色登录学生平台" 
            onclick="Btnstudent_Click" Enabled="False" BackColor="#9BCBFF" />
                    <asp:TextBox ID="TBpwd" runat="server"  ReadOnly="True" Width="50px" 
            Height="16px"  SkinID="TextBoxNum" CssClass="textcenter18" 
            BackColor="#E1FCE0" Font-Size="9pt"></asp:TextBox>
        &nbsp;<asp:HyperLink ID="HLworkshow" runat="server" BorderStyle="None" 
            CssClass="textcenter20" Font-Underline="False" Target="_blank" 
            Height="20px">作品展示</asp:HyperLink>
                    &nbsp;<asp:HyperLink ID="HLtotal" runat="server" BorderStyle="None" 
            CssClass="textcenter20" Font-Underline="False" Target="_blank" 
            Height="20px">汇总表</asp:HyperLink>
                    </div>    
    <div  class="startdiv">
        <br />
     <div  >
        &nbsp;<asp:Label ID="Labelnocolor" runat="server" 
             BackColor="#E8E8E8" Width="12px" 
             Height="12px" EnableViewState="False" ToolTip="没有作品"></asp:Label>
         &nbsp; 
         <asp:Label ID="Labelone" runat="server" BackColor="#B1D2FE" Width="12px" 
             Height="12px" EnableViewState="False" ToolTip="单个作品"></asp:Label>
&nbsp;
        <asp:Label ID="Labeltwo" runat="server" BackColor="#4F98FB" Width="12px" 
             Height="12px" EnableViewState="False" ToolTip="两个作品"></asp:Label>
&nbsp;
        <asp:Label ID="Labelthree" runat="server" BackColor="#CDE7CF" Width="12px" 
             Height="12px" EnableViewState="False" ToolTip="三个作品"></asp:Label>
             &nbsp;
        <asp:Label ID="Labelfour" runat="server" BackColor="#9BC47D" Width="12px" 
             Height="12px" EnableViewState="False" ToolTip="四个作品"></asp:Label>
             &nbsp;
        <asp:Label ID="Labelmore" runat="server" BackColor="#BCADE4" Width="12px" 
             Height="12px" EnableViewState="False" ToolTip="多个作品"></asp:Label>
         &nbsp;<asp:Label ID="Labelcount" runat="server" Width="290px" 
             Font-Names="Arial" Font-Size="9pt" EnableViewState="False"></asp:Label>
         今天已签到情况：<asp:Label ID="Labelsigin" runat="server" Width="60px"></asp:Label>位
</div>
        <br />
        <anthem:DataList ID="DLonline" runat="server" RepeatColumns="8" onitemdatabound="DLonline_ItemDataBound" 
            DataKeyField="Qid" onitemcommand="DLonline_ItemCommand"  RepeatDirection="Vertical" HorizontalAlign="Center">
                    <ItemTemplate>
                        <div  class="divonline">
                            <div><asp:Label ID="Labelqnum"  runat="server"  Text='<%# Eval("Qnum") %>' Font-Size="8pt" ></asp:Label></div>  
                            <div><asp:Label ID="HyperSname" runat="server" Text='<%# Eval("Qname") %>' CssClass="labelname"></asp:Label>
                            </div> 
                            <div><asp:Label ID="LabelQmachine" runat="server"  Text='<%# Eval("QmachineShort") %>'  Font-Size="8pt" ></asp:Label>
                            </div>                            
                            <div><asp:HyperLink ID="Groupflag" runat="server" >g</asp:HyperLink>
                            <asp:Label ID="Labelcolor" runat="server" Text='<%# Eval("Qgscore") %>' ToolTip='<%# "组评语："+Eval("Qgroup") %>'   CssClass="groupscore"></asp:Label>
                            <asp:LinkButton ID="Lunlock" runat="server" CommandArgument="Qid" CommandName="UnLock" ToolTip="单击执行：让该学生重新登录！" CssClass="lockbtn"></asp:LinkButton>
                            <div>                            
                            <asp:Label ID="Labelwork" runat="server" Text='<%# Eval("Qwork") %>' Visible="false" ></asp:Label>
                            <asp:Label ID="Labelattitude" runat="server" Text='<%# Eval("Qattitude") %>' Visible="false" ></asp:Label>
                            <asp:Label ID="Labelnote" runat="server" Text='<%# Eval("Qnote") %>' Visible="false" ></asp:Label>
                            <asp:Label ID="LabelSleader" runat="server" Text='<%# Eval("Sleader") %>' Visible="false" ></asp:Label>
                            <asp:Label ID="LabelSgroup" runat="server" Text='<%# Eval("Sgroup") %>' Visible="false" ></asp:Label>
                            <asp:Label ID="LabelSgtitle" runat="server" Text='<%# Eval("Sgtitle") %>' Visible="false" ></asp:Label>
                        </div>
                    </ItemTemplate>
                </anthem:DataList>   
        <br />
        <asp:RadioButtonList ID="RBsort" runat="server" AutoPostBack="True" 
            Font-Size="9pt" onselectedindexchanged="RBsort_SelectedIndexChanged" 
            RepeatDirection="Horizontal" RepeatLayout="Flow">
            <Items>
                <asp:ListItem Value="3">机房视图</asp:ListItem>
            <asp:ListItem Value="0">主机排序</asp:ListItem>
            <asp:ListItem Value="1" Selected="True">学号排序</asp:ListItem>
            <asp:ListItem Value="2">小组排序</asp:ListItem>
            </Items>
        </asp:RadioButtonList>&nbsp;
     <asp:CheckBox ID="CheckBoxScratch" runat="server" 
            Text="编程控制" AutoPostBack="True" 
            ToolTip="提示：编程开关控制，选中表示可以进入编程页面" 
            oncheckedchanged="CheckBoxScratch_CheckedChanged" />
     <asp:CheckBox ID="CheckBoxRgauge" runat="server" 
            Text="作品互评" AutoPostBack="True" 
            oncheckedchanged="CheckBoxRgauge_CheckedChanged" 
            ToolTip="提示：作品互评控制，选中表示开启" />
        <asp:CheckBox ID="CheckBoxip" runat="server" 
            Text="IP锁定登录" AutoPostBack="True" 
            oncheckedchanged="CheckBoxip_CheckedChanged" 
            ToolTip="提示：根据上次登录的IP进行锁定登录" />
     <asp:CheckBox ID="CheckBoxOpen" runat="server" 
            Text="快速模式" AutoPostBack="True" 
            oncheckedchanged="CheckBoxOpen_CheckedChanged" 
            ToolTip="提示：本班学生登录后，直接进入当前学案学案导航！" />
        <asp:CheckBox ID="CheckBoxPwd" runat="server" 
            Text="班级密码" AutoPostBack="True" 
            oncheckedchanged="CheckBoxPwd_CheckedChanged" 
            ToolTip="提示：选中表示公开显示班级密码，未选表示隐藏班级密码！" />
     </div>
        <div  class="startdiv">
            <br />
            <asp:Label ID="Label2" runat="server" Width="450px" Height="16px" 
                ForeColor="White" ></asp:Label>
            今天未签到情况：<asp:Label ID="Labelsigno" runat="server" Width="60px"></asp:Label>位
            <br />
            <br />
        <asp:DataList ID="DLnotline" runat="server" RepeatColumns="8" 
            RepeatDirection="Horizontal" onitemdatabound="DLnotline_ItemDataBound" 
                HorizontalAlign="Center">
                    <ItemTemplate>
                        <div class="divunline"> 
                            <div><asp:Label ID="LabelNnum"  runat="server" Text='<%# Eval("Snum") %>' Font-Size="8pt" ></asp:Label></div>                                                  
                            <div><asp:Label ID="lbQname" runat="server" Text='<%# Eval("Sname") %>' CssClass="labelname"></asp:Label>                            
                             </div>  
                            <div>
                            <asp:Label ID="LabelSscore" runat="server" Text='<%# Eval("Sscore") %>' Font-Size="8pt" Width="66px" ToolTip="总学分"></asp:Label>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
        </div>
        <asp:Label ID="Labelfresh" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label> 
        <br />
        <asp:DropDownList ID="DDLhouse" runat="server" Font-Size="9pt" Width="100px" 
            AutoPostBack="True" onselectedindexchanged="DDLhouse_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:HyperLink ID="HyperLinkSeat" runat="server" Target="_blank" >座位表</asp:HyperLink>
        &nbsp;
        &nbsp;&nbsp; <asp:ImageButton ID="Btnrefresh" runat="server" onclick="Btnrefresh_Click" Enabled="False"
            ImageUrl="~/Images/refresh.gif" />
        &nbsp;&nbsp;<asp:CheckBox ID="CheckBoxShare" runat="server" 
            Text="网盘开关" AutoPostBack="True" 
            oncheckedchanged="CheckBoxShare_CheckedChanged" 
            ToolTip="提示：选中表示网盘启用，未选表示网盘禁用！" />
       <asp:CheckBox ID="CheckBoxGroupShare" runat="server" 
            Text="小组网盘" AutoPostBack="True" 
            oncheckedchanged="CheckBoxGroupShare_CheckedChanged" 
            ToolTip="提示：选中表示小组网盘启用（前提为前面的网盘开关启用），未选表示小组网盘禁用！" />
        &nbsp;&nbsp; <asp:HyperLink ID="HylkDiskstu" runat="server" 
            ImageUrl="~/Images/disksmallstu.gif" Target="_blank" 
            ToolTip="查看学生网盘存档情况"></asp:HyperLink>
        &nbsp;&nbsp; <asp:HyperLink ID="HylkDiskGroup" runat="server" 
            ImageUrl="~/Images/disksmall.gif" Target="_blank" ToolTip="查看小组网盘存档情况"></asp:HyperLink>
        <br />     
        <div  class="startdiv">
        <div style="margin:auto; text-align:left; position: relative;">           
            已学学案：<br />
            <asp:DataList ID="DLdonekc" runat="server" ForeColor="Black" RepeatColumns="20" 
                    RepeatDirection="Horizontal" RepeatLayout="Flow" CellPadding="0"  
                CellSpacing="0" DataKeyField="Cid" 
                onitemdatabound="DLdonekc_ItemDataBound" >
                    <ItemTemplate>
                    <div class="doneksdiv"> 
                        <div><asp:HyperLink ID="ks" runat="server"  Text='<%# Eval("Cks") %>' 
                         ToolTip='<%# Eval("Ctitle") %>' CssClass="donekc"></asp:HyperLink></div>
                        <div><asp:Label ID="wk" runat="server" ToolTip="作品总数"></asp:Label></div>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
            <br /><br /><br />未学学案：<br />
            <asp:DataList ID="DLnewkc" runat="server" ForeColor="Black" RepeatColumns="20" 
                    RepeatDirection="Horizontal" RepeatLayout="Flow" CellPadding="0"  
                CellSpacing="0" onitemdatabound="DLnewkc_ItemDataBound" 
                onitemcommand="DLnewkc_ItemCommand" DataKeyField="Cid" >
                    <ItemTemplate>
                    <div class="doneksdiv">
                        <div><asp:HyperLink ID="ks" runat="server" Text='<%# Eval("Cks") %>' 
                         ToolTip='<%# Eval("Ctitle") %>' CssClass="newkc"></asp:HyperLink></div>
                        <div><asp:CheckBox ID="Ck" runat="server" Checked='<%# Eval("Cpublish") %>' Enabled="False" /></div>
                            <div> <asp:ImageButton runat="server"  ID="PubSet" 
                             CommandArgument="Cid" CommandName="P" ImageUrl="~/Images/cardsmall.gif" /><br />
                            </div>
                            </div>
                    </ItemTemplate>
                </asp:DataList>
        </div>
        </div>       
        <br />
        <br />
        <div class="startdiv"> 
        <asp:Button ID="BtnaAllQuit" runat="server" Text="全班下线"  SkinID="BtnSmall"
            onclick="BtnaAllQuit_Click" Visible="False" EnableViewState="False" />
        <br />
        <asp:Label ID="LabelToday" runat="server" Font-Size="9pt" 
            ToolTip="*服务器日期校准：作品、签到日期以此为准*" Font-Bold="False"  ></asp:Label>
        <br />
        </div>
        <script src="../Js/jquery-1.8.2.min.js" type="text/javascript"></script>
        <link href="../Js/tooltip.css" rel="stylesheet" type="text/css" />
        <script src="../Js/spanToolTip.js" type="text/javascript"></script>
        <link href="../Js/tinybox.css" rel="stylesheet" type="text/css" />
        <script src="../Js/tinybox.js" type="text/javascript"></script>
        <script type ="text/javascript" >
            function myrefresh() {
                document.getElementById("<%= Btnrefresh.ClientID %>").click();
            }
            setTimeout("myrefresh()", 120000); //指定120秒刷新一次            

             function notsg(n, g, m) {
                var urlsg ="../Teacher/NotSign.aspx?Nnum=" +n + "&Ngrade=" +g + "&Qname=" + m;
                TINY.box.show({ iframe: urlsg, boxid: 'frameless', width: 360, height: 260, fixed: false, maskopacity: 60, close:false })
            }
            function attitude(q, m, a,c) {
                var urlat = "../Teacher/attitude.aspx?Qid=" + q + "&Qname=" + m + "&Qattitude=" + a + "&Qcid=" + c;
                TINY.box.show({ iframe: urlat, boxid: 'frameless', width: 360, height: 300, fixed: false, maskopacity: 60, close:false })
            }
            function attitudegroup(g, m, q, c) {
                var urlat = "../Teacher/attitudegroup.aspx?Sg=" + g + "&Ld=" + m + "&Qd=" + q + "&Qcid=" + c;
                TINY.box.show({ iframe: urlat, boxid: 'frameless', width: 360, height: 200, fixed: false, maskopacity: 60, close:false })
            }
        </script>
</div>
</asp:Content>

