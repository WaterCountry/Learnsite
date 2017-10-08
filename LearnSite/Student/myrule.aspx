<%@ Page Title="" Language="C#" StylesheetTheme="Student" AutoEventWireup="true" CodeFile="myrule.aspx.cs" Inherits="Student_myrule" %>
    
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>   
    <link href="../App_Themes/Student/StyleSheet.css" rel="stylesheet" type="text/css" />
    </head>
<body>
    <form id="form1" runat="server">
    <center>
      <div  class="studmasterhead">
            <div  class="banner" >
                <script src="../Js/road.js" type="text/javascript"></script>
      	<script type="text/javascript" >
      	    var first = "../";
      	    ShowRoad(first);
            </script> 
                </div>
                <center>
            <div  class="menu">
            
            </div>
            </center>
            <center>
            <div class="placeauto" >
            <div class="stu">
            <div  class="path"> 
            &nbsp;
            </div>
         <div id="student">
        <br />
        <br />
        <table   class="ruletabel"
            cellpadding="3">
            <tr>
                <td  class="rulehead" >
                    课堂守则</td>
            </tr>
            <tr>
                <td  align="left"  class="ruletd">
                    1、


 无请假缺席：每人扣1分</td>
            </tr>
            <tr>
                <td  align="left"  class="ruletd">

                     2、


 迟到：每人扣0.1分 </td>
            </tr>
            <tr>
                <td  align="left" class="ruletd">
                    3、


 吃零食带饮料：每人扣0.1分</td>
            </tr>
            <tr>
                <td  align="left" class="ruletd">
                    4、


 乱丢垃圾：每人扣0.1分且负责拖地一次 </td>
            </tr>
            <tr>
                <td  align="left" class="ruletd">
                    5、


 未经老师允许玩游戏：每人扣0.1分 </td>
            </tr>
            <tr>
                <td  align="left" class="ruletd">
                    6、


 带存储设备（mp3、U盘） 并使用：每人扣0.1分 </td>
            </tr>
            <tr>
                <td  align="left" class="ruletd">
                    7、


 故意搞乱电脑硬件，扣1分</td>
            </tr>
            <tr>
                <td  align="left" class="ruletd">
                    8、 未经老师允许，私自下座位或换座位，扣1分。</td>
            </tr>
            <tr>
                <td >&nbsp;
              </td>
            </tr>
            </table>
        <br />
        <br />
        <asp:Button ID="Btnreturn" runat="server"  Text="关闭" BorderStyle="None" 
                 CssClass="buttonimg" Width="80px"/>
        </div>
        </div>
        </center>        
        </div>
    </form>
</body>
</html>