<%@ page title="" language="C#" masterpagefile="~/student/Stud.master" stylesheettheme="Student" autoeventwireup="true" inherits="Student_mychinese, LearnSite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cphs" Runat="Server">
 <link href="../images/fingering/finger.css" rel="stylesheet" type="text/css" />
 <script src="../js/jquery.cookie.js" type="text/javascript"></script>
<div id="student">
<div class="left">
<center>
    <div id="inputdiv">
        <div>
            <asp:DataList ID="DataList1" runat="server" CellPadding="3" 
                HorizontalAlign="Center" onitemdatabound="DataList1_ItemDataBound" 
                RepeatDirection="Horizontal" RepeatLayout="Flow" CellSpacing="3">
                <ItemTemplate>                    
                    <asp:Label ID="Lbtitle" runat="server" Text='<%# Eval("Ntitle") %>' CssClass="hand" ></asp:Label>
                    <asp:Label ID="Lbid" runat="server" Text='<%# Eval("nid") %>' Visible="false"></asp:Label>
                </ItemTemplate>
            </asp:DataList>

        </div>
        <br />
        <asp:Label ID="Lbnid" runat="server" Text="0"  CssClass="unsee"></asp:Label>
        <br />
        <br />
        <br />
            <div id="Typepingyin" class="typepy">
            </div>
            <div id="Typechinese" class="typecn">
            </div>
        <br />
        <br />
            <input id="InputWord" class="typewd" type="text" onpaste="return   false; " ondragenter="return   false;"tabindex="0" autocomplete="off" />
            <br />
        <br />
    </div>     
    <div id="keyboard">
<!--第一行-->
	<div class="keyCom" id="keyDHSY">~<br />`</div><div class="keyCom" id="key1">!<br />1</div>
	<div class="keyCom" id="key2">@<br />2</div><div class="keyCom" id="key3">#<br />3</div>
	<div class="keyCom" id="key4">$<br />4</div><div class="keyCom" id="key5">%<br />5</div>
	<div class="keyCom" id="key6">^<br />6</div><div class="keyCom" id="key7">&amp;<br />7</div>
	<div class="keyCom" id="key8">*<br />8</div><div class="keyCom" id="key9">(<br />9</div>
	<div class="keyCom" id="key0">)<br />0</div><div class="keyCom" id="keyJHSX">_<br />-</div>
	<div class="keyCom" id="keyDHJH">+<br />=</div><div class="keyCom" id="keyXXSX">|<br />\</div>
	<div class="key2 keyText">←</div>
<!--第二行-->
	<div class="tab keyText">Tab</div><div class="keyCom" id="keyQ">q</div>
	<div class="keyCom" id="keyW">w</div><div class="keyCom" id="keyE">e</div>
	<div class="keyCom" id="keyR">r</div><div class="keyCom" id="keyT">t</div>
	<div class="keyCom" id="keyY">y</div><div class="keyCom" id="keyU">u</div>
	<div class="keyCom" id="keyI">i</div><div class="keyCom" id="keyO">o</div>
	<div class="keyCom" id="keyP">p</div><div class="keyCom" id="keyZKH">{<br />[</div>
	<div class="keyCom" id="keyYKH">}<br />]</div><div class="enterup1 keyText"></div>
<!--第三行-->
	<div class="cap keyText">Caps</div><div class="keyCom" id="keyA">a</div>
	<div class="keyCom" id="keyS">s</div><div class="keyCom" id="keyD">d</div>
	<div class="keyCom" id="keyF">f</div><div class="keyCom" id="keyG">g</div>
	<div class="keyCom" id="keyH">h</div><div class="keyCom" id="keyJ">j</div>
	<div class="keyCom" id="keyK">k</div><div class="keyCom" id="keyL">l</div>
	<div class="keyCom" id="keyFHMH">:<br />;</div><div class="keyCom" id="keyDYSY">"<br />'</div>
	<div class="enterup2 keyText">Enter</div>
<!--第四行-->
	<div class="shift keyText" id="shiftl">Shift</div><div class="keyCom" id="keyZ">z</div>
	<div class="keyCom" id="keyX">x</div><div class="keyCom" id="keyC">c</div>
	<div class="keyCom" id="keyV">v</div><div class="keyCom" id="keyB">b</div>
	<div class="keyCom" id="keyN">n</div><div class="keyCom" id="keyM">m</div>
	<div class="keyCom" id="keyDHXY"><<br/>,</div><div class="keyCom" id="keyJHDY">><br />.</div>
	<div class="keyCom" id="keyXXWH">?<br />/</div><div class="shift keyText" id="shiftr">Shift</div>
<!--第五行-->
	<div class="ctrl keyText">Ctrl</div><div class="keymic keyText"></div>
	<div class="alt keyText">Alt</div><div class="space keyText" id="keyKG"></div>
	<div class="alt keyText">Alt</div><div class="keymic keyText"></div><div class="ctrl keyText">Ctrl</div>
</div>
    <br />
</center>
</div>
<div class="right">
    <div>
    <asp:HyperLink ID="HChinese" runat="server" 
        ImageUrl="~/images/py.png" NavigateUrl="~/student/mychinese.aspx" ></asp:HyperLink> 
    <asp:HyperLink ID="HkFinger" runat="server" 
        ImageUrl="~/images/en.png" NavigateUrl="~/student/myfinger.aspx"></asp:HyperLink>        
    <asp:HyperLink ID="HTyper" runat="server" 
        ImageUrl="~/images/cn.png" NavigateUrl="~/student/mytype.aspx" ></asp:HyperLink>       
    </div>
    <br />
    <div id="oldspd" class="letter">
        <img src="../images/apple.gif"  alt=""/>收集的苹果数：<span id="totalapples"></span>
    </div> 
    <br />
    <div id="apples"  class="applecss"></div>
    <br />
    <div id="msg"></div>
    <br />
    <asp:HyperLink ID="HLfinger" runat="server" 
        NavigateUrl="~/student/allchinese.aspx" Target="_blank" SkinID="HyperLink" 
        Width="120px" CssClass="txtszcenter" Height="18px">拼音输入英雄榜</asp:HyperLink>
    <br /> <br />     
    <div id="debug">
    </div>
    <br />
</div>
<br />
    <script src="../js/pydic.js" type="text/javascript"></script>
    <script src="../js/Chinese.js" type="text/javascript"></script>
</div>
</asp:Content>

