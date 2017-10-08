<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master" StylesheetTheme="Teacher" Validaterequest="false"  AutoEventWireup="true" CodeFile="courseshow.aspx.cs" Inherits="Teacher_courseshow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div  class="courseshow">
    <br />  	
    <asp:Label ID="LabelCtitle" runat="server"  CssClass="coursetitle"></asp:Label><br /><br />
    <div class="courseother">
                 日期：[<asp:Label ID="LabelCdate"  runat="server" ></asp:Label>]
			     类型：[<asp:Label ID="LabelCclass"  runat="server" ></asp:Label>]&nbsp;   
                 年级：[<asp:Label ID="LabelCobj"  runat="server" ></asp:Label>]&nbsp;
                 第[<asp:Label ID="LabelCterm"  runat="server" ></asp:Label>]学期&nbsp;
                 [课节：<asp:Label ID="LabelCks"  runat="server" ></asp:Label>]	&nbsp;	
                 <asp:ImageButton ID="BtnEdit" runat="server" ToolTip="点击修改" 
                     ImageUrl="~/Images/edit.gif" onclick="BtnEdit_Click" />
    </div>    
    <br />
    <br /> 
<div class="courseother" style="width: 700px">
<div style="margin: auto; width: 700px;">
         <asp:LinkButton ID="LinkBtnAdd" runat="server"  SkinID="LinkBtn"
          OnClick="LinkBtnAdd_Click" BackColor="#9BCBFF" >添加活动</asp:LinkButton>
             &nbsp;&nbsp;&nbsp; &nbsp;
         <asp:LinkButton ID="LinkBtnAddTopic" runat="server"  SkinID="LinkBtn"
          OnClick="LinkBtnAddTopic_Click" BackColor="#9BCBFF" >添加讨论</asp:LinkButton>
             &nbsp;&nbsp; &nbsp;&nbsp;
         <asp:LinkButton ID="LinkBtnAddSurvey" runat="server"  SkinID="LinkBtn"
          OnClick="LinkBtnAddSurvey_Click" BackColor="#9BCBFF" >添加调查</asp:LinkButton>
             &nbsp;
             &nbsp; &nbsp;
         <asp:LinkButton ID="LinkBtnAddTxtForm" runat="server"  SkinID="LinkBtn"
          OnClick="LinkBtnAddTxtForm_Click" BackColor="#9BCBFF" >添加表单</asp:LinkButton>
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          <asp:LinkButton ID="LinkBtnProgram" runat="server"  SkinID="LinkBtn"
          OnClick="LinkBtnProgram_Click" BackColor="#9BCBFF" >添加编程</asp:LinkButton>
             &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
         <asp:LinkButton ID="LinkBtnReturn" runat="server" SkinID="LinkBtn"  
             OnClick="LinkBtnReturn_Click" BackColor="#9BCBFF" >返回</asp:LinkButton>
         </div>

             <br />

         <asp:GridView ID="GVlistmenu" runat="server"  Width="600px"  SkinID="GVmission" 
             CellPadding="6" AutoGenerateColumns="False" 
             EnableModelValidation="True" HorizontalAlign="Center" 
        onrowcommand="GVlistmenu_RowCommand" 
        onrowdatabound="GVlistmenu_RowDataBound" >
             <Columns>             
                 <asp:TemplateField Visible="False">
                     <ItemTemplate>
                         <asp:Label ID="LabelLid" runat="server" Text='<%# Bind("Lid") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField Visible="False">
                     <ItemTemplate>
                         <asp:Label ID="LabelLxid" runat="server" Text='<%# Bind("Lxid") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField Visible="False">
                     <ItemTemplate>
                         <asp:Label ID="LabelLtype" runat="server" Text='<%# Bind("Ltype") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="序号">
                     <ItemTemplate>
                         <asp:Label ID="LabelLsort" runat="server" Text='<%# Bind("Lsort") %>'></asp:Label>
                     </ItemTemplate>
                     <ItemStyle Width="50px" />
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="类型">
                     <ItemTemplate>
                         <asp:Label ID="Label4" runat="server"></asp:Label>
                     </ItemTemplate>
                     <ItemStyle Width="50px" />
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="导航栏目">
                     <ItemTemplate>
                         <asp:HyperLink ID="HlLtitle" runat="server" NavigateUrl="" 
                             Text='<%# Eval("Ltitle") %>'></asp:HyperLink>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="ImageBtnTop" runat="server" CausesValidation="False" 
                            CommandName="Top"  CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                            Text="上" ToolTip="向上移" Font-Underline="False"></asp:LinkButton>
                    </ItemTemplate>
                     <ItemStyle Width="16px" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="ImageBtnBottom" runat="server" CausesValidation="False" 
                            CommandName="Bottom"  CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                            Text="下" ToolTip="向下移" Font-Underline="False"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="16px" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="发布" ShowHeader="False">
                     <ItemTemplate>
                         <asp:LinkButton ID="LinkBtnShow" runat="server" CausesValidation="false" 
                             CommandName="P" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                             Text='<%# Eval("lshow") %>' ToolTip="True显示，False隐藏"></asp:LinkButton>
                     </ItemTemplate>
                     <ItemStyle Width="50px" />
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="操作" ShowHeader="False">
                     <ItemTemplate>
                         <asp:LinkButton ID="LinkBtnDel" runat="server" CausesValidation="false" 
                             CommandName="D"  CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                             Text="删除"  ToolTip="请认真确定是否删除，不可恢复！"></asp:LinkButton>
                     </ItemTemplate>
                     <ItemStyle Width="50px" />
                 </asp:TemplateField>
             </Columns>
             <RowStyle Height="32px" />
         </asp:GridView>
         <div>
            <br />
         </div>       
         </div>
         <div  id="Ccontent" class="coursecontent" runat ="server">   
    </div>
         <br />
    </div>
</asp:Content>

