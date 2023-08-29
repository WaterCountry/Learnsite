<%@ page title="" language="C#" masterpagefile="~/student/Scm.master" stylesheettheme="Student" autoeventwireup="true" inherits="Student_mysurvey, LearnSite" %>

<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cpcm" Runat="Server">
<div id="student">
    <br />
        <asp:ImageButton ID="Btnclock" runat="server" ImageUrl="~/images/clock.gif" 
            onclick="Btnclock_Click" Enabled="False" />
        <asp:Label runat="server" ID="Lbtitle" Font-Bold="True" Font-Size="16px"></asp:Label>
    <br />
    <br />
    <div style="border-width: 1px; border-color: #808080; border-bottom-style: dashed; padding-bottom: 2px;">
    　姓名：<asp:Label runat="server" ID="Lbsname" ForeColor="#0066FF"></asp:Label>
    　学号：<asp:Label runat="server" ID="Lbsnum" ForeColor="#0066FF"></asp:Label>
    &nbsp;得分：<asp:Label runat="server" ID="Lbfscore" ForeColor="#0066FF"></asp:Label>
    　类型：<asp:Label runat="server" ID="Lbtypecn" ForeColor="#0066FF" ></asp:Label>
        <asp:Label runat="server" ID="Lbtype" Visible="False"></asp:Label>
        <asp:Label runat="server" ID="Lbcheck" Font-Bold="False"></asp:Label>
			<asp:Label ID="LabelCid" runat="server" Visible="False"></asp:Label>
			<asp:Label ID="LabelLid" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="LabelVid" runat="server" Visible="False"></asp:Label> 
            <asp:Label ID="LabelVtotal" runat="server" Visible="False"></asp:Label> 
    </div>
    <br />
    <div id="vcontent" runat="server"  style="margin: auto; padding: 2px; text-align: left; width: 980px; "></div>
    <br />
    <div style="width: 980px; margin: auto; padding: 2px; text-align: left;">
    <anthem:DataList ID="DataListonly" runat="server" DataKeyField="Qid" 
                    RepeatColumns="1" RepeatLayout="Flow" 
            onitemdatabound="DataListonly_ItemDataBound" >
                    <ItemTemplate>
                        <div  style="margin: auto;">
                           <div style=" background-color:#fff;" >
                          第<asp:Label ID="Labelnum" Text='<%# Container.ItemIndex + 1%> ' runat="server" ></asp:Label>题
                          &nbsp;&nbsp;<asp:Label ID="Labelquestion" runat="server" Text='<%# HttpUtility.HtmlDecode( Eval("Qtitle").ToString()) %>'></asp:Label>
                          </div>
						  <div>
                          <asp:RadioButtonList ID="RBLselect" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" 
                                CellPadding="6" CellSpacing="2" RepeatLayout="table">
                                </asp:RadioButtonList>  
						  </div>
                            <asp:Panel ID="Blanks" runat="server">
                            </asp:Panel>
                          </div>
                    </ItemTemplate>
                </anthem:DataList>    
    </div>
    <br />
        <asp:Button ID="Btnok" runat="server" onclick="Btnok_Click" Text="提交答卷" 
        BorderStyle="None" CssClass="buttonimg" />
    &nbsp;
        <asp:Button ID="Btnshow" runat="server" onclick="Btnshow_Click" Text="查看结果" 
        BorderStyle="None" CssClass="buttonimg" Visible="False" />
    <br />
    <br />
    <div id="editInfo">
    <asp:Label runat="server" ID="Lbtime" ></asp:Label> 
    </div>
    <br />
    <br />
    <br />
</div>
</asp:Content>