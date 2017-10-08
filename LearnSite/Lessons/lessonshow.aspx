<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lessonshow.aspx.cs" Inherits="Lessons_lessonshow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>学案设计-常规检查</title>
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <!--startprint-->
    <div id="background" style="text-align: center; margin:auto;">
    <div style="width: 800px; margin:auto;">
    <div id="course" style="border: 1px solid #E3E3E3; margin:auto;">
    <asp:Repeater ID="Repeater1" runat="server">   
    <ItemTemplate>  	
    <div  style="height: 20px; text-align:center;font-size: 12pt; font-family: 宋体; color:#FFFFFF; font-weight: bold ;background-color:#3C3C3C ; "><%#Eval("Ctitle")%></div>    
    <div style="   font-size: 9pt; font-family:Arial;">
        <table width="780px"  >
			<tr>
			<td style="width: 160px; height: 22px; text-align:left">[日期：<%#Eval("Cdate")%>]</td>
			<td style="width: 240px; height: 22px; text-align:center">
			    学案类型：[<%# Eval("Cclass")%>] &nbsp;   
                学习年级：[<%# Eval("Cobj")%>]</td>
			<td  style="width: 100px; height:22px; text-align:right">[课时：<%#Eval("Cks")%>]			
			</td></tr>
		</table>
    <div >
    <div  style=" width:780px; word-break:break-all; word-wrap:break-word; text-align:left;">		
		<%# HttpUtility.HtmlDecode( Eval("Ccontent").ToString())%>
		</div>
    </div>
    <br/>
		</div>			            
					
		</ItemTemplate>
 </asp:Repeater>
    </div>
    <br />
    <asp:Repeater ID="Repeater2" runat="server">   
    <ItemTemplate>
    <div id="mission" style="border: 1px solid #E3E3E3">   	
    <div  style="height: 20px;;font-size: 10pt; font-family: 宋体;font-weight: bold ;background-color: #EEEEEE ; text-align:center">学习活动<%#toChineseNum(Eval("Msort").ToString())%>：<%#Eval("Mtitle")%></div>    
    <div style="   font-size: 9pt; font-family:Arial;">
        <table width="780px"  >
			<tr>
			<td style="width: 160px; height: 22px; text-align:left">日期：<%#Eval("Mdate")%></td>
			<td style="width: 160px; height: 22px; text-align:center">学案编号：[<%# Eval("Mcid")%>]</td>
			<td style="width: 160px; height: 22px; text-align:center">作品类型：<%#Eval("Mfiletype")%></td>
			<td  style="width: 120px; height:22px; text-align:right">作品提交：<%#Eval("Mupload")%></td>
			</tr>
		</table>
    <div >
    <div  style=" width:780px; word-break:break-all; word-wrap:break-word; text-align:left;">		
		<%# HttpUtility.HtmlDecode( Eval("Mcontent").ToString())%>
		</div>
         </div>
         <br/>
		</div>			            
		</div>
		<br />			
		</ItemTemplate>
 </asp:Repeater>    
    <br />
    
    <asp:Repeater ID="Repeater3" runat="server">   
    <ItemTemplate>  
    <div id="flection" style="border: 1px solid #E3E3E3">	
    <div  style="height: 20px; text-align:center;font-size: 10pt; font-family: 宋体;font-weight: bold ;background-color:#EEEEEE ; ">〖课后思考〗</div>    
    <div  style=" width:780px; word-break:break-all; word-wrap:break-word; font-size:9pt; text-align:left;">		
		<%# HttpUtility.HtmlDecode( Eval("Fcontent").ToString())%>
		</div>
        </div>
		</div>			            
		</div>
		<br />	
		</ItemTemplate>
 </asp:Repeater> 
    <div style="text-align: center">
    <asp:LinkButton ID="LinkBtn" runat="server" BackColor="#EBEBEB" BorderColor="#D1D1D1"
             BorderStyle="Solid" BorderWidth="1px" Font-Underline="False" 
            ForeColor="Black" Width="60px" 
            Font-Size="9pt" >关闭</asp:LinkButton>
    </div>
    <br />
    </div>
    </div>
    <!--endprint-->
    <script language="javascript" type="text/javascript">
        function preview() {
            bdhtml = window.document.body.innerHTML;
            sprnstr = "<!--startprint-->";
            eprnstr = "<!--endprint-->";
            prnhtml = bdhtml.substr(bdhtml.indexOf(sprnstr) + 17);
            prnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));
            window.document.body.innerHTML = prnhtml;
            window.print();
            //window.history.go(0);
            //prnform.htext.value=prnhtml;
            //prnform.submit();
            //alert(prnhtml);
        }
        </script>
        <br />
        <div style="margin: auto; text-align: center; background-color: #E8FFE8">
        <input id="BtnPrintView" style="border: 1px solid #CCCCCC; font-family: 宋体, Arial, Helvetica, sans-serif; font-size: 9pt; background-color: #EBEBEB;" 
            type="button" value="打印预览" onclick="preview()" />
        </div>
        <br />
        <br />
    </form>
</body>
</html>
