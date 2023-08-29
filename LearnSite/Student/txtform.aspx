<%@ page title="" language="C#" masterpagefile="~/student/Scm.master" stylesheettheme="Student" autoeventwireup="true" inherits="Student_txtform, LearnSite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cpcm" Runat="Server">
<div  id="showcontent">
<div class="left">
<br />
    <div   class="missiontitle">
     <asp:Label ID="LabelMtitle"  runat="server" ></asp:Label>
   </div><br />
   <div class="courseother">
   </div>   
    <div  id="Mcontent"  class="coursecontent" runat="server">	
		</div>
        </div>
		<br />
		<br />
</div>
<div class="right">
<center>    
        &nbsp;<br />
        <br />
        <img id="sucessed" alt="" src="../images/sucessed.png" style="width: 120px; height: 120px; display:none;" /><br />
        <br />
        <br />
        <input id="Btnform" type="button" value="提交填写"  onclick="SaveForm();" 
            style="border-width: 0px; background-color: #3399FF; width: 80px; height: 24px;" />
        <br />
        <br />
        <br />
        <br /> 
        <br />
        <div id="msg" style="color: #FF0000"></div>
        <br />
        <br />
        <br />
        <asp:HyperLink ID="Hlresult" runat="server" CssClass="txts20center" 
            Height="20px" SkinID="HyperLink" 
            Target="_blank" Width="80px">查看结果</asp:HyperLink>
        <br />
        <script type="text/javascript">
            function SaveForm() {
                var saveurl = "saveform.ashx?lid=" + "<%=Lid %>";
                var wordstr = $("div.coursecontent").html();
                $.ajax({
                    type: "post",
                    url: saveurl,
                    data: { Word: wordstr },
                    dataType: "html",
                    success: function (data) {
                        $('#msg').html(data.toString());
                        alert("提交成功！");
                        location.reload(true);
                    }
                });
            }
            
            var isdone = "<%=Done %>";
            if (isdone == "true") {
                $("#sucessed").show();
            }
            else {
                $("#sucessed").hide();            
            }

        </script>
        <br />
        <br />
        <br />
        <br />
    </center>
</div>   
    <br />
    <asp:HiddenField ID="hiddencount" runat="server" />

</asp:Content>

