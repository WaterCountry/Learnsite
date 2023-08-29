<%@ page title="" language="C#" masterpagefile="~/teacher/Teach.master" stylesheettheme="Teacher" validaterequest="false" autoeventwireup="true" inherits="Teacher_courseshow, LearnSite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div  class="courseshow">	
    <asp:Label ID="LabelCtitle" runat="server"  CssClass="coursetitle"></asp:Label><br /><br />
    <div class="courseother">
					 <asp:Image ID="Imagebanner" runat="server" Height="20px" ToolTip="横幅图片"  />
                 <asp:Label ID="LabelCdate"  runat="server" ></asp:Label>&nbsp;
			     <asp:Label ID="LabelCclass"  runat="server" ></asp:Label>类型&nbsp;   
                 <asp:Label ID="LabelCobj"  runat="server" ></asp:Label>年级&nbsp;
                 第<asp:Label ID="LabelCterm"  runat="server" ></asp:Label>学期&nbsp;
                 第<asp:Label ID="LabelCks"  runat="server" ></asp:Label>课&nbsp;	
                 <asp:ImageButton ID="BtnEdit" runat="server" ToolTip="点击修改" 
                     ImageUrl="~/images/edit.gif" onclick="BtnEdit_Click" />
    </div>   
    <br /> 
<div class="courseother" style="width: 1080px">
<div style="margin: auto; width: 1080px;">
         <asp:LinkButton ID="LinkBtnAdd" runat="server"  
          OnClick="LinkBtnAdd_Click" CssClass="button24" >添加活动</asp:LinkButton>
             &nbsp;
         <asp:LinkButton ID="LinkBtnAddTopic" runat="server" 
          OnClick="LinkBtnAddTopic_Click" CssClass="button24" >添加讨论</asp:LinkButton>
             &nbsp; 
         <asp:LinkButton ID="LinkBtnAddSurvey" runat="server"  
          OnClick="LinkBtnAddSurvey_Click" CssClass="button24" >添加调查</asp:LinkButton>
             &nbsp; 
         <asp:LinkButton ID="LinkBtnAddTxtForm" runat="server"  
          OnClick="LinkBtnAddTxtForm_Click" CssClass="button24" >添加填表</asp:LinkButton>
             &nbsp;
          <asp:LinkButton ID="LinkBtnProgram" runat="server"  
          OnClick="LinkBtnProgram_Click" CssClass="button24" >积木编程</asp:LinkButton>
             &nbsp;
          <asp:LinkButton ID="LinkBtnPython" runat="server"  
          OnClick="LinkBtnPython_Click" CssClass="button24" >Python编程</asp:LinkButton>
             &nbsp;
          <asp:LinkButton ID="LinkBtnConsole" runat="server"  
          OnClick="LinkBtnConsole_Click" CssClass="button24" >Python测评</asp:LinkButton>
             &nbsp;
          <asp:LinkButton ID="LinkButtonGraph" runat="server"  
          OnClick="LinkBtnGraph_Click" CssClass="button24" >流程图</asp:LinkButton>
             &nbsp;
          <asp:LinkButton ID="LinkButtonPixel" runat="server"  
          CssClass="button24" onclick="LinkButtonPixel_Click" >像素画</asp:LinkButton>
             &nbsp;
          <asp:LinkButton ID="LinkButtonHtml" runat="server"  
          CssClass="button24" onclick="LinkButtonHtml_Click" >Html网页</asp:LinkButton>
             &nbsp;
         <asp:LinkButton ID="LinkBtnReturn" runat="server" 
             OnClick="LinkBtnReturn_Click" CssClass="button24" >返回</asp:LinkButton>
         </div>

             <br />

         <asp:GridView ID="GVlistmenu" runat="server"  Width="860px"  SkinID="GVmission" 
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
                     <ItemStyle Width="40px" />
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="类型">
                     <ItemTemplate>
                         <asp:Image ID="Image4" runat="server" ImageUrl="~/images/new_none.gif" />
                         <asp:Label ID="Label4" runat="server"></asp:Label>
                     </ItemTemplate>
                     <HeaderStyle HorizontalAlign="Left" />
                     <ItemStyle Width="100px" HorizontalAlign="Left" />
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="导航栏目">
                     <ItemTemplate>
                         <asp:HyperLink ID="HlLtitle" runat="server" NavigateUrl="" 
                             Text='<%# Eval("Ltitle") %>'></asp:HyperLink>
                     </ItemTemplate>
                     <HeaderStyle HorizontalAlign="Left" />
                     <ItemStyle HorizontalAlign="Left" />
                 </asp:TemplateField>
                 <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="ImageBtnTop" runat="server" CausesValidation="False" 
                            CommandName="Top"  CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                            Text="上" ToolTip="向上移" Font-Underline="False"></asp:LinkButton>
                    </ItemTemplate>
                     <ItemStyle Width="20px" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="ImageBtnBottom" runat="server" CausesValidation="False" 
                            CommandName="Bottom"  CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                            Text="下" ToolTip="向下移" Font-Underline="False"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="20px" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="发布" ShowHeader="False">
                     <ItemTemplate>
                         <asp:LinkButton ID="LinkBtnShow" runat="server" CausesValidation="false" 
                             CommandName="P" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                             Text='<%# Eval("lshow") %>' ToolTip="True显示，False隐藏"></asp:LinkButton>
                     </ItemTemplate>
                     <ItemStyle Width="60px" />
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
         </div>
         <div  id="Ccontent" class="coursecontent" runat ="server">   
    </div>
         <br />
    </div>
</asp:Content>

