<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master" StylesheetTheme="Teacher"  AutoEventWireup="true" CodeFile="typerset.aspx.cs" Inherits="Teacher_typerset" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <div >
    <div  class="quizdiv"> 
                    <div style="padding: 2px; width: 710px" class="centerdiv">
                    <div style="padding: 2px; margin: auto; font-weight: bold"> 
                    设置所教年级中文打字文章
                    </div>
                    选择：<asp:DropDownList ID="DDLgrade" runat="server" Font-Size="9pt" 
                        EnableTheming="True" Font-Names="Arial" AutoPostBack="True" 
                        onselectedindexchanged="DDLgrade_SelectedIndexChanged" >
        </asp:DropDownList>
                    年级<br />
                    <asp:DataList ID="DataListTyper" runat="server" RepeatColumns="5" Width="100%" 
                            CellPadding="3" CellSpacing="3" 
                            onitemdatabound="DataListTyper_ItemDataBound" >
                        <ItemTemplate>
                        <div  class="typerset">
                            <asp:CheckBox ID="ChkTyper" runat="server" 
                                Text='<%# Eval("Ttitle") %>'  />
                                <asp:Label ID="Lbtid" runat="server" Text='<%# Eval("Tid") %>' Visible="False"></asp:Label>
                                </div>
                        </ItemTemplate>
                    </asp:DataList>
                     <div style="margin: auto; padding: 2px">
                    <asp:Button ID="BtnSelect" runat="server" Text="提交选择" onclick="BtnSelect_Click" 
                        SkinID="BtnNormal" />
                        &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="BtnReturn" runat="server" Text="返回" onclick="BtnReturn_Click" 
                        SkinID="BtnNormal" />
                    </div>
                    </div>
                    <asp:Label ID="LabelTids" runat="server" Visible="False"></asp:Label>             
                    </div>
                  
    </div>

</asp:Content>

