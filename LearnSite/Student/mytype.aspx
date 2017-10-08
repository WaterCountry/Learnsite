<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Stud.master"  StylesheetTheme="Student" AutoEventWireup="true" CodeFile="mytype.aspx.cs" Inherits="Student_mytype" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cphs" Runat="Server">
    <link href="../Js/Typer.css" rel="stylesheet" type="text/css" />
<div id="student">
<div class="left">
<center>
<table style="vertical-align: top;" 
        onselectstart= "return    false "       
        oncopy= "return    false "       
        oncut= "return    false "       
        onpaste= "return   false "       
        oncontextmenu= "return   false " >
     <tr>
<td>
      <div  style="text-align:left">      
                <asp:DataList ID="DLTid" runat="server" ForeColor="#333333" RepeatColumns="36" 
                    RepeatDirection="Horizontal" RepeatLayout="Flow" CellPadding="0" CellSpacing="0">
                    <ItemTemplate>
                        <asp:HyperLink ID="id" runat="server"   Height="14px" BackColor="#EEEEEE"
                            NavigateUrl='<%# "mytype.aspx?Tid="+Eval("Tid") %>' 
                            Text='<%# Eval("Tid") %>'  ToolTip='<%# Eval("Ttitle") %>' 
                            Font-Underline="False" ForeColor="#333333"></asp:HyperLink>
                    </ItemTemplate>
                </asp:DataList>
                </div>
                <div class="divcenter">
                <asp:Label ID="LTid" runat="server"></asp:Label>&nbsp;<asp:Label ID="Ttitle" runat="server" ></asp:Label> 
                </div>
                <div id="Tcontent"  class="typecontent"><asp:Literal ID="Literal1" runat="server"></asp:Literal>
                </div>
    <div style="text-align: center">    
        限时<input id="Text7"  class="text7"
                 type="text" hidefocus="hideFocus" maxlength="30" readonly="readOnly" 
                 unselectable="on" name="TypeText7" value="600"  />&nbsp;正确<input id="Text4" class="text7" 
                 type="text" hidefocus="hideFocus" maxlength="30" readonly="readOnly" 
                 unselectable="on" value="0" name="TypeText4" />&nbsp;速度<input 
                 id="Text6" class="text7"
                 type="text" hidefocus="hideFocus" maxlength="30" readonly="readOnly" 
                 unselectable="on" name="Typeresult" value="0"  />&nbsp;拼音<input id="Textpy"  class="text3" 
                 type="text" hidefocus="hideFocus" maxlength="30" readonly="readOnly" 
                 unselectable="on" />&nbsp;五笔<input 
                 id="Textwb"  class="text3"
                 type="text" hidefocus="hideFocus" maxlength="30" readonly="readOnly" 
                 unselectable="on" /></div>
    <textarea id="InputText"  class="textareacss" cols="6" ondragenter= "return   false; " rows="6" ></textarea>
                <br />
                <div class="divcenter">
                <label id="Labelmsg"></label>
                <asp:Label ID="Labeltids" runat="server" Visible="False"></asp:Label>
                </div>
</td>
</tr>
</table>
</center>
</div>
<div class="right">
<center>
    <div>
    <asp:HyperLink ID="HChinese" runat="server" 
        ImageUrl="~/Images/py.png" NavigateUrl="~/Student/mychinese.aspx" ></asp:HyperLink> 
    <asp:HyperLink ID="HkFinger" runat="server" 
        ImageUrl="~/Images/en.png" NavigateUrl="~/Student/myfinger.aspx"></asp:HyperLink>        
    <asp:HyperLink ID="HTyper" runat="server" 
        ImageUrl="~/Images/cn.png" NavigateUrl="~/Student/mytype.aspx" ></asp:HyperLink>       
    </div>
    <br />
<div>
    <script src="../Js/Backcolor.js" type="text/javascript"></script>
    <script type="text/javascript"> WriteBg();</script>
</div>
    <asp:GridView ID="GVTyper" runat="server" AllowPaging="True"  Caption="中文输入英雄榜" CellPadding="2"         
        onpageindexchanging="GVTyper_PageIndexChanging"  PageSize="20"
        OnRowDataBound="GVTyper_RowDataBound" Width="99%" SkinID="GridViewInfo">
        <Columns>
            <asp:BoundField HeaderText="名次" />
            <asp:BoundField DataField="Sname" HeaderText="姓名">
            <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="Pscore" HeaderText="速度">
            <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="Ptid" HeaderText="文章">
            <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="Ptype" HeaderText="次数" />
        </Columns>
        <PagerTemplate>
            <div style="color: black;  text-align:center">
                <asp:LinkButton ID="btnFirst" runat="server" CausesValidation="False" 
                    CommandArgument="First" CommandName="Page" Font-Underline="False" 
                    ForeColor="Black" Text="首页"></asp:LinkButton>
                <asp:LinkButton ID="btnPrev" runat="server" CausesValidation="False" 
                    CommandArgument="Prev" CommandName="Page" Font-Underline="False" 
                    ForeColor="Black" Text="上页"></asp:LinkButton>
                <asp:LinkButton ID="btnNext" runat="server" CausesValidation="False" 
                    CommandArgument="Next" CommandName="Page" Font-Underline="False" 
                    ForeColor="Black" Text="下页"></asp:LinkButton>
                <asp:LinkButton ID="btnLast" runat="server" CausesValidation="False" 
                    CommandArgument="Last" CommandName="Page" Font-Underline="False" 
                    ForeColor="Black" Text="尾页"></asp:LinkButton>               
            </div>
        </PagerTemplate>
        <PagerStyle Font-Size="9pt" />
    </asp:GridView>
    <br />
    
    <asp:HyperLink ID="HyperLink1" runat="server"  Width="120px" SkinID="HyperLink" 
        Height="18px" NavigateUrl="~/Student/alltyper.aspx" CssClass="txtszcenter" 
        Target="_self">中文输入英雄榜</asp:HyperLink>   
    <br />
    <br />
    <asp:HyperLink ID="HyperLink2" runat="server"  Width="120px" SkinID="HyperLink" 
        Height="18px" CssClass="txtszcenter" 
        Target="_self">本篇班内英雄榜</asp:HyperLink>   
    <br />
    </center>
</div>
<br />
</div>
    <script src="../Js/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../Js/Typer.js" type="text/javascript"></script>
    <script src="../Js/pydic.js" type="text/javascript"></script>
    <script src="../Js/wbdic.js" type="text/javascript"></script>
</asp:Content>

