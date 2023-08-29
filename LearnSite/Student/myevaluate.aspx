<%@ page title="" language="C#" stylesheettheme="Student" autoeventwireup="true" inherits="Student_myevaluate, LearnSite" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>  
    <script language=javascript type=text/javascript>
        document.oncontextmenu = new Function('event.returnValue=false;');
        document.onselectstart = new Function('event.returnValue=false;');
    </script> 
    </head>
<body>
<form id="form1" runat="server">
<center >

<div id="student">
<div  class="divcenter">  
<div >
    <br />
<asp:Image ID="Image2" runat="server" ImageUrl="~/images/wvote.png" />
      <asp:Label ID="Labelscope" runat="server" Font-Bold="True"></asp:Label>
      <strong>作品互评：</strong><asp:Label ID="Labelmtitle" runat="server" Font-Bold="True" ></asp:Label>     
      &nbsp;<asp:Label ID="Labelwmid" runat="server"  Visible="false"></asp:Label>
          <asp:Image ID="ImageWtype" runat="server" />&nbsp;<asp:Label ID="LabelWtype" runat="server"></asp:Label>&nbsp;
      作品总数：<asp:Label ID="Labelhow" runat="server"  Width="20px"></asp:Label>
      可投次数：<asp:Label ID="Labelegg" runat="server"  Width="20px"></asp:Label>
      我的得票：<asp:Label ID="Labelme" runat="server"  Width="20px"></asp:Label>
      互评得分：<asp:Label ID="Labelwfscore" runat="server"  Width="20px"></asp:Label>  
      <asp:Label ID="LabelMgid" runat="server"  Visible="false"></asp:Label>
      <asp:Label ID="Labelwdate" runat="server"  Visible="false" ></asp:Label>
      </div>
                <div>
                <asp:DataList ID="DataListvote" runat="server" RepeatDirection="Horizontal" 
                    RepeatColumns="15" DataKeyField="wid" OnItemCommand="DataListvote_ItemCommand" 
                        CellPadding="5" Font-Size="11pt" 
                        onitemdatabound="DataListvote_ItemDataBound" >
                    <ItemTemplate>
                        <div  style=" width :60px;text-align: center" >
                                  <div>
                                <asp:LinkButton ID="lBtnSname" runat="server"  CommandArgument='<%# Eval("Wurl") %>' CommandName="S" 
                                ToolTip="点击预览我的作品！" Text='<%# Eval("Wname") %>' ForeColor="#0000CC"></asp:LinkButton>
                                    </div>
                        <asp:Label ID="LabelWflash" runat="server" Text='<%# Eval("Wflash") %>' Visible="False"></asp:Label>
                        <asp:Label ID="LabelWid" runat="server" Text='<%# Eval("wid") %>' Visible="False"></asp:Label>
                        <asp:Label ID="LabelWnum" runat="server" Text='<%# Eval("Wnum") %>' Visible="False"></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
                <div style="margin: auto; background-color: #E8F3FF;">
                        <div>
                        <asp:DataList
                            ID="DataListGauge" runat="server" Font-Size="11pt" 
                            CellPadding="10" RepeatColumns="2" 
                            onitemdatabound="DataListGauge_ItemDataBound" HorizontalAlign="Center" 
                                RepeatDirection="Horizontal" CellSpacing="4" Width="100%" >
                        <ItemTemplate>
                        <div>
                            <asp:CheckBox ID="RbMitem" runat="server" Text='<%# Eval("Mitem") %>'  />
                            <asp:Label ID="LabelCount" runat="server"></asp:Label>
                            <asp:Image ID="Image3" runat="server" ImageUrl="~/images/smile16.gif" />
                            <asp:Label ID="LbMid" runat="server" Text='<%# Eval("mid") %>' Visible="False"></asp:Label>
                            <asp:Label ID="LbMscore" runat="server" Text='<%# Eval("Mscore") %>' Visible="False"></asp:Label>
                        </div>
                        </ItemTemplate>
                    </asp:DataList>
                    </div>
                    <script>
                        function check() {
                            var inputs = document.getElementById("<%=DataListGauge.ClientID%>").getElementsByTagName("input");
                            var s = 0;
                            for (var i = 0; i < inputs.length; i++) {
                                if (inputs[i].checked) {
                                    s++;
                                }
                            }
                            if (s > 3) {
                                alert("您最多只能选3项！");
                                return false;
                            }
                            else {
                                return true;
                            }
                        }
                    </script>
                    </div>                 
                </div>
                    <br />
                    <asp:Label ID="Labelmsg" runat="server" Font-Bold="False"></asp:Label>      
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Image ID="ImageDown0" runat="server"  ImageUrl="~/images/good16.png" />
                    <asp:CheckBox ID="CheckBoxGood" runat="server" Text="推荐" />
                    &nbsp;&nbsp;&nbsp;
                    &nbsp;<asp:Button ID="BtnVote" runat="server" onclick="BtnVote_Click" 
                        SkinID="buttonSkinPink" Text="请投我一票" Width="100px" />
                        <br />
                <div>
                <br />
                <center >
                <div>
                    <asp:Literal ID="Literal1" runat="server">
                    </asp:Literal> 
                    <br />
                    <asp:Label ID="Labelwname" runat="server"  Visible="False"></asp:Label>
                    <br />
                    <asp:Label ID="lbMyFeedback" runat="server" Visible="False"></asp:Label>
                    <br />
                    </div>
                    </center>
                </div>
</div>
</div> 

</center>
 </form>
</body>
</html>

