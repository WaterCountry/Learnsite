<%@ Page Title="" Language="C#" MasterPageFile="~/Lessons/prescm.master"  StylesheetTheme="Student"  AutoEventWireup="true" CodeFile="presurvey.aspx.cs" Inherits="Lessons_presurvey" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Ppcm" Runat="Server">
    <div id="student">
    <br />
        <asp:ImageButton ID="Btnclock" runat="server" ImageUrl="~/Images/clock.gif"  Enabled="False" />
        <asp:Label runat="server" ID="Lbtitle" Font-Bold="True" Font-Size="16px"></asp:Label>
    <br />
    <br />
    <div style="border-width: 1px; border-color: #808080; border-bottom-style: dashed; padding-bottom: 2px;">
    　姓名：<asp:Label runat="server" ID="Lbsname" ForeColor="#0066FF">演示</asp:Label>
    　学号：<asp:Label runat="server" ID="Lbsnum" ForeColor="#0066FF">10101010</asp:Label>
    &nbsp;得分：<asp:Label runat="server" ID="Lbfscore" ForeColor="#0066FF">00</asp:Label>
    　类型：<asp:Label runat="server" ID="Lbtypecn" ForeColor="#0066FF" ></asp:Label>
        <asp:Label runat="server" ID="Lbtype" Visible="False"></asp:Label>
        <asp:Label runat="server" ID="Lbcheck" Font-Bold="False"></asp:Label>
    </div>
    <br />
    <div id="vcontent" runat="server" 
            style="margin: auto; padding: 2px; text-align: left; width: 500px; "></div>
    <br />
    <div style="width: 500px; margin: auto; padding: 2px; text-align: left; background-color: #FFFFFF;">
    <asp:DataList ID="DataListonly" runat="server" DataKeyField="Qid" 
                    RepeatColumns="1" RepeatLayout="Flow" 
            onitemdatabound="DataListonly_ItemDataBound" >
                    <ItemTemplate>
                        <div  onmouseover="this.style.backgroundColor='#F8DFC9'"   onmouseout="this.style.backgroundColor='' " style="margin: auto; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #B0B0B0;">
                           <div style="width: 30px; float: left; left:6px; background-color: #F8DFC9;">
                          &nbsp;<asp:Label ID="Labelnum" Text='<%# Container.ItemIndex + 1%> ' runat="server" Font-Bold="True"></asp:Label>
                          </div>
                          <div style="width: 450px; float: left; left:40px">
                          &nbsp;<asp:Label ID="Labelquestion" runat="server" Text='<%# HttpUtility.HtmlDecode( Eval("Qtitle").ToString()) %>'></asp:Label>
                          </div>
						  <br />
                          <div style="margin: auto; width: 80%;">
                          <asp:RadioButtonList ID="RBLselect" runat="server" 
                                  RepeatLayout="Flow" RepeatColumns="1" RepeatDirection="Horizontal" 
                                CellPadding="3" CellSpacing="6">
                                </asp:RadioButtonList>  
                           </div>      
                          </div>
                    </ItemTemplate>
                </asp:DataList>    
    </div>
    <br />
        <asp:Button ID="Btnok" runat="server"  Text="提交答卷" 
        BorderStyle="None" CssClass="buttonimg" />
    &nbsp;
        <asp:Button ID="Btnshow" runat="server"  Text="查看结果" 
        BorderStyle="None" CssClass="buttonimg" />
    <br />
        <script src="../Js/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script type="text/javascript" >
        function passrefresh() {
                var oldt = document.getElementById("ctl00_Ppcm_Lbtime").value;
                document.getElementById("ctl00_Ppcm_Lbtime").value = oldt + 1;
        }
        setTimeout("passrefresh()", 60000); //指定60秒刷新一次       
    </script>
    <script type="text/javascript">
        var left = $('#student').offset().left;
        $("#editInfo").css({ left: left + "px", top: "200px" });
        $(function () {
            $(window).scroll(function () {
                var top = $(window).scrollTop() + 200;
                $("#editInfo").css({ left: left + "px", top: top + "px" });
            });
        }); 
    </script> 

    <br />
    <div id="editInfo" 
        style="border: 1px solid #E1E1E1; float:left; width:100px;background-color:#C5D6FE; position:absolute; ">
    时间流逝：<asp:Label runat="server" ID="Lbtime" Font-Bold="True" >0</asp:Label>分钟 
    </div>
    <br />
    <br />
    <br />
    <br />
    <div>
    注意：调查测验限时8分钟，每超1分钟扣除1学分！
    </div>
    <br />
</div>
</asp:Content>