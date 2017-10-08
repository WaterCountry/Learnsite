<%@ Page Language="C#" AutoEventWireup="true"  StylesheetTheme="Student" CodeFile="register.aspx.cs" Inherits="Student_register" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>新学员注册</title>   
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
            <div  class="path">&nbsp;
            <div>
            <br /><br />
                <div style="border: 1px solid #C5D6FE; line-height: 20px; font-size: 9pt; font-family: 宋体, Arial, Helvetica, sans-serif; background-color: #E1EAFF; width: 300px;">
                   <div style="background-color: #C5D6FE">
                    <strong>新学员注册</strong><br />
                    </div>
                    <br />
            年级选择：<asp:DropDownList 
                 ID="DDLgrade" runat="server" Width="60px" >
                    </asp:DropDownList>
                    <br />
                    <br />
            班级选择：<asp:DropDownList ID="DDLclass" runat="server" Width="60px" >
                    </asp:DropDownList>
                    <br />
                    <br />
                    性别选择：<asp:DropDownList ID="DDLsex" runat="server" Font-Size="9pt" Width="60px" 
                        BackColor="Cornsilk">
                    </asp:DropDownList>
                    <br />
                    <br />
                    姓名：<asp:TextBox ID="Tsname" runat="server" 
                        BackColor="Cornsilk" BorderColor="#B1C9FE"
                        BorderStyle="Dashed" BorderWidth="1px" Width="86px" Height="20px"></asp:TextBox>
                    <br />
                    <asp:Label ID="labelmsg" runat="server" SkinID="LabelMsgRed"></asp:Label>
                    <br />
                    <asp:Button ID="BtnRegister" runat="server" onclick="BtnRegister_Click" Text="确定" 
                    BorderStyle="None" BackColor="#B1C9FE" Font-Size="9pt" Height="20px" 
                        Width="80px" />
                    &nbsp;
                    <asp:Button ID="BtnReturn" runat="server" onclick="BtnReturn_Click" Text="返回" 
                    BorderStyle="None" BackColor="#B1C9FE" Font-Size="9pt" Height="20px" 
                        Width="80px" />
                    <br />
                    <br />
                </div>
            </div>
            <div>
                <br />
                <br />
            <br />
            <br />
            <div style="font-size: 9pt; color: #137DCC;">
                <img src="../Images/topicnormal.png" />友情提示：请选择老师指定的年级和班级进行注册，以免错班而无法处理！</div>
            </div>
            </div>
        </div>             
        </div>
         </center> 
         </div> 
        </center>  
    </form>
</body>
</html>
