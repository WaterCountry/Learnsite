<%@ Page Title="" Language="C#" MasterPageFile="~/Manager/Manage.master" AutoEventWireup="true" CodeFile="createroom.aspx.cs" Inherits="Manager_createroom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div class="manageplace" >                    
        <div  class="roomcreate">
            <div style="background-color: #EEEEEE; height: 18px; ">全校班级完整列表</div>
            <br />
            年级范围：<asp:DropDownList ID="DDLgrademin" runat="server" Font-Size="9pt" 
                Width="42px">
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
                <asp:ListItem>4</asp:ListItem>
                <asp:ListItem>5</asp:ListItem>
                <asp:ListItem>6</asp:ListItem>
                <asp:ListItem Selected="True">7</asp:ListItem>
                <asp:ListItem>8</asp:ListItem>
                <asp:ListItem>9</asp:ListItem>
                <asp:ListItem>10</asp:ListItem>
                <asp:ListItem>11</asp:ListItem>
                <asp:ListItem>12</asp:ListItem>
                <asp:ListItem>13</asp:ListItem>
                <asp:ListItem>14</asp:ListItem>
                <asp:ListItem>15</asp:ListItem>
                <asp:ListItem>16</asp:ListItem>
    </asp:DropDownList>年级到<asp:DropDownList ID="DDLgrademax" runat="server" Font-Size="9pt" 
                Width="42px">
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
                <asp:ListItem>4</asp:ListItem>
                <asp:ListItem>5</asp:ListItem>
                <asp:ListItem>6</asp:ListItem>
                <asp:ListItem>7</asp:ListItem>
                <asp:ListItem>8</asp:ListItem>
                <asp:ListItem Selected="True">9</asp:ListItem>
                <asp:ListItem>10</asp:ListItem>
                <asp:ListItem>11</asp:ListItem>
                <asp:ListItem>12</asp:ListItem>
                <asp:ListItem>13</asp:ListItem>
                <asp:ListItem>14</asp:ListItem>
                <asp:ListItem>15</asp:ListItem>
                <asp:ListItem>16</asp:ListItem>
    </asp:DropDownList>年级&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 班级数最大值：<asp:DropDownList 
                ID="DDLclassmax" runat="server" Font-Size="9pt"
        Width="42px">
    </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Btncreate" runat="server" BackColor="#E6E6E6" 
                        BorderColor="#D4D4D4" BorderWidth="1px" Font-Names="Arial" 
                Font-Size="9pt" Text="批量创建" Width="70px" Height="20px" 
                onclick="Btncreate_Click" />
                <br />
                <asp:GridView ID="GVclass" runat="server" AllowPaging="True" 
                    AutoGenerateColumns="False" BorderColor="#E7E7E7" BorderStyle="Solid" 
                    BorderWidth="1px" CellPadding="2" Font-Size="9pt" GridLines="None" Width="100%" 
                    onpageindexchanging="GVclass_PageIndexChanging" 
                    onrowdatabound="GVclass_RowDataBound" PageSize="15" DataKeyNames="Rid" 
                onrowcommand="GVclass_RowCommand" EnableModelValidation="True" >
                    <Columns>
                        <asp:BoundField HeaderText="序号" />
                        <asp:BoundField DataField="Rhid" HeaderText="教师" />
                        <asp:BoundField DataField="Rgrade" HeaderText="年级" />
                        <asp:BoundField DataField="Rclass" HeaderText="班级" />                   
                        <asp:ButtonField CommandName="Del" HeaderText="操作" Text="删除" />
                    </Columns>
                    <pagertemplate>
                        <div style="width:100%; height:13px; text-align:right">
                            第<asp:Label ID="lblPageIndex" runat="server" 
                                text="<%# ((GridView)Container.Parent.Parent).PageIndex + 1  %>" />
                            页  共<asp:Label ID="lblPageCount" runat="server" 
                                text="<%# ((GridView)Container.Parent.Parent).PageCount  %>" />
                            页 
                            <asp:LinkButton ID="btnFirst" runat="server" causesvalidation="False" 
                                commandargument="First" commandname="Page" Font-Underline="False" 
                                ForeColor="Black" text="首页" />
                            <asp:LinkButton ID="btnPrev" runat="server" causesvalidation="False" 
                                commandargument="Prev" commandname="Page" Font-Underline="False" 
                                ForeColor="Black" text="上一页" />
                            <asp:LinkButton ID="btnNext" runat="server" causesvalidation="False" 
                                commandargument="Next" commandname="Page" Font-Underline="False" 
                                ForeColor="Black" text="下一页" />
                            <asp:LinkButton ID="btnLast" runat="server" causesvalidation="False" 
                                commandargument="Last" commandname="Page" Font-Underline="False" 
                                ForeColor="Black" text="尾页" />
                        </div>
                    </pagertemplate>
                    <RowStyle BorderStyle="None" Font-Names="Arial" Font-Size="9pt" 
                        ForeColor="Black" Height="20px" />
                    <HeaderStyle BackColor="#EEEEEE" Font-Bold="False" Font-Names="Arial" 
                        Font-Size="9pt" />
                    <AlternatingRowStyle BackColor="#E7E7E7" />
                    <PagerStyle BackColor="#EEEEEE" Font-Names="Arial" Font-Size="9pt" />
                </asp:GridView>   
                <br />
                <div>
                    <strong>手动添加单个班级：</strong> 年级 
                    <asp:TextBox ID="TextBoxGrade" runat="server" Width="30px"></asp:TextBox>
&nbsp;班级<asp:TextBox ID="TextBoxClass" runat="server" Width="30px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="BtncreateOne" runat="server" BackColor="#E6E6E6" 
                        BorderColor="#D4D4D4" BorderWidth="1px" Font-Names="Arial" 
                Font-Size="9pt" Text="添加该班级" Width="70px" Height="20px" 
                onclick="BtncreateOne_Click" />
                </div>                 
                    </div>
                   
        <br />                   
        <br />
        <asp:Label ID="Labelmsg" runat="server" ForeColor="Red"></asp:Label>
        <br />
        <br />
 </div>
 </div>
</asp:Content>

