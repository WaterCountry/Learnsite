<%@ Page Title="" Language="C#" StylesheetTheme="Student" AutoEventWireup="true"
    CodeFile="mynum.aspx.cs" Inherits="Student_mynum" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>学号查询</title>
    <link href="../App_Themes/Student/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../Js/tooltip.css" rel="stylesheet" type="text/css" />
    <script src="../Js/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        document.oncontextmenu = new Function('event.returnValue=false;');
    </script>
    <style type="text/css">
        html{ overflow-y: scroll }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <center>
        <div class="studmasterhead">
            <div class="banner">
                <script src="../Js/road.js" type="text/javascript"></script>
                <script type="text/javascript">
                    var first = "../";
                    ShowRoad(first);
                </script>
            </div>
            <center>
                <div class="menu">
                </div>
            </center>
            <center>
                <div class="placeauto">
                    <div class="stu">
                        <div class="path">
                        </div>
                        <div id="student">
                            <div style="line-height: 20px; font-size: 9pt; font-family: 宋体, Arial, Helvetica, sans-serif;">
                                年级选择：<asp:DropDownList ID="DDLgrade" runat="server" Width="50px">
                                </asp:DropDownList>
                                &nbsp; 班级选择：<asp:DropDownList ID="DDLclass" runat="server" Width="50px">
                                </asp:DropDownList>
                                &nbsp;
                                <asp:Button ID="BtnSearch" runat="server" OnClick="BtnSearch_Click" Text="查询" BorderStyle="None"
                                    CssClass="buttonimg" />
                                &nbsp;班级密码
                                <asp:TextBox ID="TextBoxPwd" runat="server" ReadOnly="True" CssClass="textboxcenter"
                                    Width="80px" BorderColor="#999999" BorderStyle="Dashed" BorderWidth="1px" onDblClick="copy()"></asp:TextBox>
                            </div>
                            <div>
                                <asp:DataList ID="DataListsnum" runat="server" RepeatDirection="Horizontal" RepeatColumns="10"
                                    CellPadding="3" OnItemDataBound="DataListsnum_ItemDataBound">
                                    <ItemTemplate>
                                        <div style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: 9pt; text-align: center;
                                            vertical-align: middle">
                                            <asp:Image ID="ImageStu" class="tooltip" runat="server" Height="83px" Width="70px" /><br />
                                            <asp:HyperLink ID="HLSnum" runat="server" Text='<%# Eval("Sname") %>' ToolTip='<%# Eval("Snum") %>'
                                                CssClass="textboxcenter"></asp:HyperLink>
                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </center>
        </div>
    </center>
    <script src="../Js/ToolTip.js" type="text/javascript"></script>
    <script>
        function copy() {
            var msg = document.getElementById("TextBoxPwd").value;
            window.clipboardData.setData("Text", msg);
        }
    </script>
    </form>
</body>
</html>
