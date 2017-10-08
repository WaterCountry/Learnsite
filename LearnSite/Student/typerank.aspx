<%@ Page Language="C#" AutoEventWireup="true" CodeFile="typerank.aspx.cs" Inherits="Student_typerank" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>文字输入擂台榜</title>
</head>

<body>
    <form id="form1" runat="server">
    <center>
    <strong>中文输入擂台</strong><br /><br />
    <div style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: 9pt"> 
        全校<br />
        <asp:DataList ID="DataList_allc" runat="server" 
                    onitemdatabound="DataList_allc_ItemDataBound" RepeatColumns="12" 
                    RepeatDirection="Horizontal">
                    <ItemTemplate>
                    <div style="margin: 2px; border: 1pt solid #EEEEEE; font-family: 宋体, Arial, Helvetica, sans-serif; font-size: 9pt; background-color: #EBFCEF; text-align: center;">
                        <asp:Image ID="allcImage1" runat="server"   Height="80px" Width="80px" /><br />
                        <asp:Label ID="allcLabelsname" runat="server" Text='<%# Eval("Sname") %>'></asp:Label><br />
                        <asp:Label ID="allcLabelpsnum" runat="server" Text='<%# Eval("Psnum") %>' Visible="false"></asp:Label>
                        <asp:Label ID="allcLabelsgrade" runat="server" Text='<%# Eval("Sgrade") %>'></asp:Label>.
                        <asp:Label ID="allcsclass" runat="server" Text='<%# Eval("Sclass") %>'></asp:Label>班<br />
                        <asp:Label ID="allcLabelpscore" runat="server" Text='<%# Eval("Pscore") %>'></asp:Label>汉字/分钟
                    </div>
                    </ItemTemplate>
                </asp:DataList>
                <br />
        <asp:DataList ID="DataList_wai" runat="server" 
            onitemdatabound="DataList_wai_ItemDataBound">
            <ItemTemplate>
                <asp:Label ID="Labelgrade" runat="server" Text='<%# Eval("Rgrade") %>' ></asp:Label>
                <asp:DataList ID="DataList_li" runat="server" 
                    onitemdatabound="DataList_li_ItemDataBound" RepeatColumns="12" 
                    RepeatDirection="Horizontal">
                    <ItemTemplate>
                    <div style="margin: 2px; border: 1pt solid #EEEEEE; font-family: 宋体, Arial, Helvetica, sans-serif; font-size: 9pt; background-color: #EBFCEF; text-align: center;">
                        <asp:Image ID="Image1" runat="server"    Height="80px" Width="80px"   /><br />
                        <asp:Label ID="Labelsname" runat="server" Text='<%# Eval("Sname") %>'></asp:Label><br />
                        <asp:Label ID="Labelpsnum" runat="server" Text='<%# Eval("Psnum") %>' Visible="false"></asp:Label>
                        <asp:Label ID="Labelsgrade" runat="server" Text='<%# Eval("Sgrade") %>'></asp:Label>.
                        <asp:Label ID="sclass" runat="server" Text='<%# Eval("Sclass") %>'></asp:Label>班<br />
                        <asp:Label ID="Labelpscore" runat="server" Text='<%# Eval("Pscore") %>'></asp:Label>汉字/分钟
                    </div>
                    </ItemTemplate>
                </asp:DataList>
            </ItemTemplate>
        </asp:DataList>   
    </div>
    <br />
    <strong>英文输入擂台</strong><br /><br />
    <div style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: 9pt"> 
    全校<br />
    <asp:DataList ID="DataList_enall" runat="server" 
                    onitemdatabound="DataList_enall_ItemDataBound" RepeatColumns="12" 
                    RepeatDirection="Horizontal">
                    <ItemTemplate>
                    <div style="margin: 2px; border: 1pt solid #EEEEEE; font-family: 宋体, Arial, Helvetica, sans-serif; font-size: 9pt; background-color: #EBFCEF; text-align: center;">
                        <asp:Image ID="lenImage1" runat="server"    Height="80px" Width="80px"  /><br />
                        <asp:Label ID="lenLabelsname" runat="server" Text='<%# Eval("Sname") %>'></asp:Label><br />
                        <asp:Label ID="lenLabelpsnum" runat="server" Text='<%# Eval("Psnum") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lenLabelsgrade" runat="server" Text='<%# Eval("Sgrade") %>'></asp:Label>.
                        <asp:Label ID="lensclass" runat="server" Text='<%# Eval("Sclass") %>'></asp:Label>班<br />
                        <asp:Label ID="lenLabelpspd" runat="server" Text='<%# Eval("Pspd") %>'></asp:Label>单词/分钟
                    </div>
                    </ItemTemplate>
                </asp:DataList>
                <br />
        <asp:DataList ID="DataList_enwai" runat="server" 
            onitemdatabound="DataList_enwai_ItemDataBound">
            <ItemTemplate>
                <asp:Label ID="enLabelgrade" runat="server" Text='<%# Eval("Rgrade") %>' ></asp:Label>
                <asp:DataList ID="DataList_enli" runat="server" 
                    onitemdatabound="DataList_enli_ItemDataBound" RepeatColumns="12" 
                    RepeatDirection="Horizontal">
                    <ItemTemplate>
                    <div style="margin: 2px; border: 1pt solid #EEEEEE; font-family: 宋体, Arial, Helvetica, sans-serif; font-size: 9pt; background-color: #EBFCEF; text-align: center;">
                        <asp:Image ID="enImage1" runat="server"    Height="80px" Width="80px"  /><br />
                        <asp:Label ID="enLabelsname" runat="server" Text='<%# Eval("Sname") %>'></asp:Label><br />
                        <asp:Label ID="enLabelpsnum" runat="server" Text='<%# Eval("Psnum") %>' Visible="false"></asp:Label>
                        <asp:Label ID="enLabelsgrade" runat="server" Text='<%# Eval("Sgrade") %>'></asp:Label>.
                        <asp:Label ID="ensclass" runat="server" Text='<%# Eval("Sclass") %>'></asp:Label>班<br />
                        <asp:Label ID="enLabelpspd" runat="server" Text='<%# Eval("Pspd") %>'></asp:Label>单词/分钟
                    </div>
                    </ItemTemplate>
                </asp:DataList>
            </ItemTemplate>
        </asp:DataList>   
    </div>
    <br />
        <asp:Label ID="Labeltop" runat="server" Text="8" Visible="False"></asp:Label>
    </center>
    </form>
</body>
</html>
