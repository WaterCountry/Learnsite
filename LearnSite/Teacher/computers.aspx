<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master" StylesheetTheme="Teacher" AutoEventWireup="true" CodeFile="computers.aspx.cs" Inherits="Teacher_computers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <div   class="placehold">        
        <div style="margin: auto; text-align: center;"  >
            <asp:RadioButtonList ID="Radiobtnorder" runat="server" AutoPostBack="True" 
                onselectedindexchanged="Radiobtnorder_SelectedIndexChanged" 
                RepeatDirection="Horizontal" Height="20px" RepeatLayout="Flow">
                <asp:ListItem Selected="True" Value="1">IP地址</asp:ListItem>
                <asp:ListItem Value="2">计算机名</asp:ListItem>
                <asp:ListItem Value="3">日期</asp:ListItem>
            </asp:RadioButtonList>
        </div>
            <div  class="softdiv">
                <asp:GridView ID="GVComputer" runat="server" 
                    AutoGenerateColumns="False" CellPadding="2" SkinID="GridViewInfo"
                    PageSize="20" Width="100%"  EnableModelValidation="True" 
                    onrowcommand="GVComputer_RowCommand" 
                    onrowdatabound="GVComputer_RowDataBound" DataKeyNames="Pid">
                    <Columns>
                        <asp:BoundField HeaderText="序号" />
                        <asp:HyperLinkField DataNavigateUrlFields="Pip" 
                            DataNavigateUrlFormatString="ipstudent.aspx?Qip={0}" DataTextField="Pip" 
                            HeaderText="IP地址" Target="_blank" />
                        <asp:TemplateField HeaderText="计算机名">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Pmachine") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="电脑室">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Pm") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CheckBoxField DataField="Plock" HeaderText="绑定状态" />
                        <asp:TemplateField ShowHeader="False">                            
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" 
                                    CommandArgument='<%# Eval("Pid") %>' CommandName="Lock" 
                                    ImageUrl="~/Images/lock.png" Text="按钮" ToolTip="更新锁定状态" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Pdate" HeaderText="更新日期">
                        </asp:BoundField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" 
                               CommandArgument='<%# Eval("Pid") %>' CommandName="Del" Text="删除" ToolTip="删除该条记录"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>                    
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:GridView>                
            </div>
            <br />
        <asp:Button ID="BtnDelAll" runat="server" onclick="BtnDelAll_Click" 
            SkinID="BtnNormal" Text="全体删除" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="BtnUnlock" runat="server" onclick="BtnUnlock_Click" 
            SkinID="BtnNormal" Text="全体解绑" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="BtnOnlock" runat="server" 
            SkinID="BtnNormal" Text="全体绑定" onclick="BtnOnlock_Click"  />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="BtnRefresh" runat="server" 
            SkinID="BtnNormal" Text="刷新" onclick="BtnRefresh_Click"  />
        <br />
        <br />
        <span style="color: #5189FF">说明：解除绑定后，学生登录更新记录就会自动绑定</span> &nbsp;&nbsp;
        <asp:CheckBox ID="CheckBoxhostname" runat="server" AutoPostBack="True" 
                        Font-Size="9pt" oncheckedchanged="CheckBoxhostname_CheckedChanged" 
                    Text="是否自动获取主机名" ToolTip="同网段获取正常，如果跨网段请关闭并导入主机名和IP绑定表格" />
                <br />
        <div style="border: 1px solid #E3E3E3; margin: auto; padding: 2px; width: 500px; background-color: #F7F7F7;">
            <strong>采用手工导入主机名与IP对应表</strong>
        <br />
            <asp:FileUpload ID="FuHostnameIp" runat="server" Font-Size="9pt" />
&nbsp;
        <asp:Button ID="BtnImport" runat="server" onclick="BtnImport_Click" 
            SkinID="BtnNormal" Text="导入Excel" />
        <br />
            <asp:Label ID="Labelmsg" runat="server" ForeColor="#000099"></asp:Label>
        <br />
        Excel导入样式参考
        <center>
        <table style="border: 1px solid #808080; width: 300px; text-align: center;">
            <tr>
                <td style="border: 1px solid #999999">
                    ip</td>
                <td style="border: 1px solid #999999">
                    hostname</td>
            </tr>
            <tr>
                <td style="border: 1px solid #999999">
                    192.168.0.20</td>
                <td style="border: 1px solid #999999">
                    pc1</td>
            </tr>
            <tr>
                <td style="border: 1px solid #999999">
                    192.168.0.21</td>
                <td style="border: 1px solid #999999">
                    pc2</td>
            </tr>
        </table>
        </center>
        </div>
            <br />
    </div>
</asp:Content>

