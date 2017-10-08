<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mysurveyclass.aspx.cs" Inherits="Student_mysurveyclass" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../App_Themes/Student/StyleSheet.css" rel="stylesheet" type="text/css" />
    <title>结果分析</title>
</head>
<body>
    <form id="form1" runat="server">
<div style="font-family: 宋体, Arial, Helvetica, sans-serif; text-align: center; font-size: 9pt; ">
    <div id="student">
    <div style="margin: auto; width: 720px; ">
        <asp:Label runat="server" ID="Lbtitle" Font-Bold="True" Font-Size="16pt" 
            ForeColor="#09272B"></asp:Label> 
    <br />
    <br />
        　年级：<asp:Label 
            runat="server" ID="Lbsgrade" ForeColor="#09272B" Font-Bold="True"></asp:Label>
        班级：<asp:Label runat="server" ID="Lbsclass" ForeColor="#09272B" Font-Bold="True"></asp:Label>
        日期：<asp:Label runat="server" ID="Lbdate" Font-Bold="True" ForeColor="#09272B"></asp:Label>
        &nbsp;参与人次：<asp:Label runat="server" ID="Lbstus" Font-Bold="True" 
            ForeColor="#09272B"></asp:Label>
        &nbsp;平均分：<asp:Label runat="server" ID="Lbscore" Font-Bold="True" 
            ForeColor="#09272B"></asp:Label>
        &nbsp;<asp:CheckBox ID="CBclose" runat="server" Visible="False" />
        &nbsp;&nbsp;
        <asp:HyperLink ID="HLclassrank" runat="server" Font-Bold="True" 
            ForeColor="#09272B" Target="_blank" Font-Underline="False" 
            ToolTip="如果是课堂测验类型，则排行成绩纳入学分统计！">查看排行</asp:HyperLink>
        <br />
    <div id="vcontent" runat="server"             
            
            style="border-width: 1px; border-color: #09272B; margin: auto; padding: 8px; text-align: left; width: 600px; border-top-style: dashed;"></div>
    </div>
    <br />
    <div style="border: 2px solid #8CB2B5; width: 600px; margin: auto; padding: 6px;text-align: left; ">
    <asp:DataList ID="DataListonly" runat="server" DataKeyField="Qid" 
                    RepeatColumns="1" RepeatLayout="Flow" 
            onitemdatabound="DataListonly_ItemDataBound" Width="98%" >
                    <ItemTemplate>
                        <div  style="margin: auto; ">
                        <div>
                        <div style="width: 30px; float: left; left:6px; top:2px; ">
                          &nbsp;<asp:Label ID="Labelnum" Text='<%# Container.ItemIndex + 1%> ' runat="server" Font-Bold="True"></asp:Label>
                        </div>
                        <div style="width: 480px; float: left; left:20px;top:2px; ">
                          &nbsp;<asp:Label ID="Labelquestion" runat="server" Text='<%# HttpUtility.HtmlDecode( Eval("Qtitle").ToString()) %>'></asp:Label> 
                          </div>
                          </div><br />
                            <div style="width:580px; float:left;  left:10px;  border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #B0B0B0;"> 
                              <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                EnableModelValidation="True" GridLines="None" ShowHeader="False" 
                                CellPadding="3" CellSpacing="3" BorderStyle="None" Width="100%" 
                                    onrowdatabound="GridView1_RowDataBound" >
                                <AlternatingRowStyle BackColor="#FEF8E0" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# "("+Eval("Mnum").ToString()+ ")" %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="30px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# HttpUtility.HtmlDecode( Eval("Mitem").ToString()) %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="500px" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Mhit" >
                                    <ItemStyle Font-Bold="True" ForeColor="#006666" Width="30px" />
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLink1" runat="server" ToolTip='<%# Eval("Mid") %>'
                                                Text='<%# Eval("Mper") %>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle Font-Bold="True" ForeColor="#0000CC" Width="30px" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle BackColor="#C9E0DE" />
                            </asp:GridView>  
                            </div>  
                        </div>                       
                    </ItemTemplate>
                </asp:DataList>
    
    </div>
    <br />
        <br />
        <div style="text-align: center; margin: auto">
        <asp:Button ID="Btnrefresh" runat="server" BackColor="#D5E2E3" 
            BorderColor="#4D7477" BorderStyle="Solid" BorderWidth="1px" 
            onclick="Btnrefresh_Click" Text="重新统计" Width="80px" Font-Size="9pt" 
            Font-Names="宋体" ToolTip="注意：如果调查已关闭则无法重新统计！" />
            &nbsp;&nbsp;&nbsp;
        <asp:Button ID="Btnclose" runat="server" BackColor="#D5E2E3" 
            BorderColor="#4D7477" BorderStyle="Solid" BorderWidth="1px" Text="关闭" 
                Width="80px" Font-Size="9pt" 
            Font-Names="宋体"  />
            &nbsp;&nbsp;&nbsp;
            <asp:HyperLink ID="HLreturn" runat="server" BackColor="#D5E2E3" 
                BorderColor="#4D7477" BorderStyle="Solid" BorderWidth="1px" Height="18px" 
                Target="_self" Width="80px" CssClass="txtszcenter" ForeColor="Black" >返回</asp:HyperLink>
            </div>
    <br />
        <asp:HyperLink ID="HlAll" runat="server">横向列表</asp:HyperLink>
    <br />
    <div id="stuList" runat="server" 
            style="margin: auto; text-align: left; color: #283D3E; width: 600px;"></div>
    <br />                       
        <link href="../Js/tinybox.css" rel="stylesheet" type="text/css" />
        <script src="../Js/tinybox.js" type="text/javascript"></script>
        <script type ="text/javascript" >
            function mate(m, v) {
                var urlat = "../Student/mysurveymate.aspx?Mid=" + m + "&Vid=" + v;
                TINY.box.show({ iframe: urlat, boxid: 'frameless', width: 400, height: 300, fixed: false, maskopacity: 40, closejs: function () { closeJS() } })
            }
        </script>          
</div>
</div>
    </form>
</body>
</html>
