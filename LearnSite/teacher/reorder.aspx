<%@ page title="" language="C#" masterpagefile="~/teacher/Teach.master" autoeventwireup="true" inherits="Teacher_reorder, LearnSite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <p>
        <br />
        <span style="font-size: large"><strong>请选择要重新分班的年级<asp:DropDownList 
            ID="Gradeselect" runat="server">
        </asp:DropDownList>
        </strong></span></p>
    <p>
&nbsp;
        <table style="width:100%;">
            <tr>
                <td align="center">
                    班级<asp:DropDownList ID="Classone" 
                        runat="server" AutoPostBack="True" >
        </asp:DropDownList>
                </td>
                <td align="center">                    
                    班级<asp:DropDownList ID="Classtwo" 
                        runat="server" AutoPostBack="True" >
        </asp:DropDownList></td>
            </tr>
            <tr>
                <td align="center">
                    &nbsp;</td>
                <td align="center">
                    &nbsp;</td>
            </tr>
        </table>
    </p>
    <p>
    </p>
    <p>
        今天通过个人密码登录上交作业的同学设定班级为<asp:DropDownList ID="SetClass" runat="server">
        </asp:DropDownList>
&nbsp;
    </p>
    <p align="center">
        &nbsp;</p>
    <p>
        <asp:Button ID="Btnorder" runat="server" Text="对当前两个班级进行重新分班" 
            onclick="Btnorder_Click" />
    </p>
    <p style="font-size: large">
        <strong>请慎重考虑操作！仅用于两个班级内重新分配学生！</strong></p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
</asp:Content>

