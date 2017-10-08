<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Scm.master" StylesheetTheme="Student"  Validaterequest="false"  AutoEventWireup="true" CodeFile="showcourse.aspx.cs" Inherits="Student_showcourse" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Cpcm" Runat="Server">
<div id="showcontent">
<div class="left"> 
    <br />  	
    <asp:Label ID="LabelCtitle" runat="server"  CssClass="coursetitle"></asp:Label><br />
    <div class="courseother">	
    </div>
    <div  id="Ccontent" class="coursecontent" runat ="server">   
    </div>
    <br/>
    <br />        
</div>

<div class="right"> 
    <div style="padding: 4px; background-color: #C6DCEE;">最近作品回顾</div> 
    <div style="padding: 2px">
        <asp:GridView ID="GVold" runat="server" AutoGenerateColumns="False" 
            EnableModelValidation="True" ShowHeader="False" Width="98%" 
            onrowdatabound="GVold_RowDataBound" CellPadding="3" BorderStyle="Solid" 
            GridLines="None" BorderColor="#F9F9F9" BorderWidth="1px">
            <Columns>
                <asp:BoundField DataField="Cks">
                <ItemStyle Width="30px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Mtitle">
                    <ItemTemplate>
                        <asp:HyperLink ID="HLurl" runat="server" NavigateUrl='<%# Eval("Wurl") %>' 
                            Target="_blank" Text='<%# Eval("Mtitle") %>' ToolTip='<%# Eval("Ctitle") %>'></asp:HyperLink>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" Height="24px" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
    </div>
    <br />
</div>

<br />
</div>
</asp:Content>

