<%@ page title="" language="C#" masterpagefile="~/student/Stud.master" stylesheettheme="Student" autoeventwireup="true" inherits="Student_quizstart, LearnSite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cphs" Runat="Server">
<div id="student">
<div class="left">
            <div style="background-color: #FFFFFF">
            <div>
            <div style="text-align: left; background-color: #DBEBEE">单选题：</div>

                <asp:DataList ID="DataListonly" runat="server" DataKeyField="Qid" 
                    RepeatColumns="1"  width="100%">
                    <ItemTemplate>
                        <div class="quizone">
                            <div class="quizleftnum">
                                <asp:Label ID="Labelnum" Text='<%# Container.ItemIndex + 1%> ' runat="server" ></asp:Label>
                                </div>
                            <div class="quizleftquestion">
                                <asp:Label ID="Labelquestion" runat="server" Text='<%# HttpUtility.HtmlDecode( Eval("Question").ToString()) %>'></asp:Label>
                                </div>
                            <div  class="quizleftanswer">
                                <asp:Label ID="Labelanswer" runat="server" Text='<%# Eval("Qanswer") %>' 
                                    Visible="False"></asp:Label>
                                <asp:Label ID="Labelscore" runat="server" Text='<%# Eval("Qscore") %>' 
                                    Visible="False"></asp:Label>
                            </div>
                            <div class="quizleftup">
                                <asp:RadioButtonList ID="RBLselect" runat="server" 
                                    RepeatDirection="Horizontal" Visible="True">
                                    <asp:ListItem>A</asp:ListItem>
                                    <asp:ListItem>B</asp:ListItem>
                                    <asp:ListItem>C</asp:ListItem>
                                    <asp:ListItem>D</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div>
            <div style="text-align: left; background-color: #DFEAE0">多选题：</div>

                <asp:DataList ID="DataListmore" runat="server" DataKeyField="Qid" 
                    RepeatColumns="1" width="100%">
                    <ItemTemplate>
                            <div class="quizone" >
                                <div class="quizleftnum">
                                    <asp:Label ID="Labelnumm" runat="server" Text="<%# Container.ItemIndex + 1%> "></asp:Label>
                                </div>
                                <div class="quizleftquestion">
                                    <asp:Label ID="Labelquestionm" runat="server" 
                                        Text='<%# HttpUtility.HtmlDecode( Eval("Question").ToString()) %>'></asp:Label>
                                </div>
                                <div class="quizleftanswer">
                                    <asp:Label ID="Labelanswerm" runat="server" Text='<%# Eval("Qanswer") %>' 
                                        Visible="False"></asp:Label>
                                        <asp:Label ID="Labelscorem" runat="server" Text='<%# Eval("Qscore") %>' 
                                    Visible="False"></asp:Label>
                                </div>
                                <div class="quizleftup">
                                    <asp:CheckBoxList ID="CBLselect" runat="server" RepeatDirection="Horizontal" 
                                        Visible="True">
                                        <asp:ListItem>A</asp:ListItem>
                                        <asp:ListItem>B</asp:ListItem>
                                        <asp:ListItem>C</asp:ListItem>
                                        <asp:ListItem>D</asp:ListItem>
                                    </asp:CheckBoxList>
                                </div>
                        </div>
                    </ItemTemplate>
                </asp:DataList> 

            </div>
            <div>
            <div style="text-align: left; background-color: #E3EADF">判断题：</div>

            <asp:DataList ID="DataListjudge" runat="server" DataKeyField="Qid" 
                    RepeatColumns="1"  width="100%">
                    <ItemTemplate>
                            <div class="quizone" >
                                <div class="quizleftnum">
                                    <asp:Label ID="Labelnumj" runat="server" Text="<%# Container.ItemIndex + 1%> "></asp:Label>
                                </div>
                                <div class="quizleftquestion">
                                    <asp:Label ID="Labelquestionj" runat="server" 
                                        Text='<%# HttpUtility.HtmlDecode( Eval("Question").ToString()) %>'></asp:Label>
                                </div>
                                <div class="quizleftanswer">
                                    <asp:Label ID="Labelanswerj" runat="server" Text='<%# Eval("Qanswer") %>' 
                                        Visible="False"></asp:Label>
                                        <asp:Label ID="Labelscorej" runat="server" Text='<%# Eval("Qscore") %>' 
                                    Visible="False"></asp:Label>
                                </div>
                                <div class="quizleftup">
                                    <asp:RadioButtonList ID="RBLjudge" runat="server" RepeatDirection="Horizontal" 
                                        Visible="True" >
                                    <asp:ListItem>对</asp:ListItem>
                                    <asp:ListItem>错</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                        </div>
                    </ItemTemplate>
                </asp:DataList>

            </div>
            </div>
            <br />            
                <asp:Button ID="Btnquiz" runat="server" OnClick="Btnquiz_Click"
                    Text="提交成绩" CausesValidation="False" CssClass="buttonimg" 
                BorderStyle="None"/>
            <br />
            <br />
            <br />
    </div>
<div class="right">  
<center>  
    <div class="quizresult">
        <input id="TextTime"  class="quiztime" type="text"  maxlength="30" readonly="readOnly"  name="TypeText7"  />
        <br />
        <br />
        <div class="quizmyscore">
        <br />
        <div class="quizmyhead">
        我的成绩单
        </div><br />
        
        <br />
        本次测验总得分：<asp:Label ID="Labelallscore" runat="server"></asp:Label>
        <br />
        <br />
        </div>
        <br />
        <asp:Label ID="Labelmsg" runat="server"  SkinID="LabelMsgRed"></asp:Label>
        <br />
        <br />
    
    <asp:HyperLink ID="HLanswer" runat="server"  Width="80px" 
            NavigateUrl="~/student/quizview.aspx" Enabled="False" 
            Visible="False" CssClass="buttonimg" Target="_blank">查看答案</asp:HyperLink>   
        <br />
        <div id="showscope"  runat="server" 
            style="text-align: left; margin: auto; width: 90%;"></div>
        <br />
        <br />
        <br />
    </div>   
    <br />
    <br />
    </center>
    </div>
<br />
</div>
<script src="../js/QuizClock.js" type="text/javascript"></script>
</asp:Content>

