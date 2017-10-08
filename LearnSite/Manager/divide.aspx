<%@ Page Title="" Language="C#" MasterPageFile="~/Manager/Manage.master"  StylesheetTheme="Teacher"  AutoEventWireup="true" CodeFile="divide.aspx.cs" Inherits="Manager_divide" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div class="manageplace" >
    <div style=" margin: auto; border: 1px solid #E0E0E0; width: 380px; ">
        <div style="background-color: #EEEEEE; height: 18px;">
            分班说明</div>
        <br />
        <div style="margin: auto; width: 335px; text-align: left; background-color: #EEEEEE;">
        &nbsp;&nbsp;&nbsp;&nbsp; 
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp; 1、请在新学期学年升班后再进行操作。<br />
            <br />
&nbsp;&nbsp;&nbsp;&nbsp; 2、请上传该年级段重新分班后的Excel表格。<br />
            <br />
&nbsp;&nbsp;&nbsp;&nbsp; 3、进行分班操作后，用所教班级的教师账号进行确认。<br />
            <br />
&nbsp;&nbsp;&nbsp;&nbsp; 4、学校所提供的重新分班表格中可能有新的插班生，<br />
            <br />
&nbsp;&nbsp;&nbsp;&nbsp; 需要教师在上课前，在教师平台的学生管理中添加。<br />
            <br />
&nbsp;&nbsp;&nbsp;&nbsp; 5、在您分班操作前，请注意做好备份！<br />
            <br />
&nbsp;&nbsp;&nbsp;&nbsp; 6、平台分班是根据上传的学生的班级替换原班级，<br />
            <br />
&nbsp;&nbsp;&nbsp;&nbsp; 请注意同姓名学生，不对同姓名学生进行分班。<br />
            <br />
&nbsp;&nbsp;&nbsp;&nbsp; 如果年级和班级格式错误，请处理Excel中年级班级格式<br />
&nbsp;<br />
            <br />
        </div>
        <br />
        </div>
    <br />
    <asp:FileUpload ID="FileUpload1" runat="server" Font-Size="9pt" />
&nbsp;
                    <asp:Button ID="Btndivide" runat="server" BackColor="#E6E6E6" 
                        BorderColor="#D4D4D4" BorderWidth="1px" Font-Names="Arial" 
                        Font-Size="9pt" Text="重新分班" Width="80px" 
            onclick="Btndivide_Click" />
       
    <br />
    <asp:Label ID="Labelmsg" runat="server"></asp:Label>
    <br />
    <br />
    分班表格 Excel样式<table align="center" border="1" style="width: 48%; height: 38px;">
        <tr>
            <td>
                年级</td>
            <td>
                班级</td>
            <td>
                姓名</td>
        </tr>
        <tr>
            <td>
                8</td>
            <td>
                1</td>
            <td>
                张三</td>
        </tr>
        <tr>
            <td>
                8</td>
            <td>
                1</td>
            <td>
                李四</td>
        </tr>
    </table>
    <br />
    如果导入出错，请注意Excel中年级、班级列格式！<br />
    <br />
    <br />
    <br />
       
</div>
</asp:Content>

