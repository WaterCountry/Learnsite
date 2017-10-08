<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Stud.master" StylesheetTheme="Student"  AutoEventWireup="true" CodeFile="myfinger.aspx.cs" Inherits="Student_myfinger" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cphs" Runat="Server">
    <link href="../Images/Fingering/finger.css" rel="stylesheet" type="text/css" />
<div id="student">
<div class="left">
<center>
    <div id="inputdiv">
        <br />
        <div id="TextWord"  class="showtxt" ></div>
        <br />
        <div id="Meanword" class="meandiv" >
        </div>
        <br />
        <input id="InputWord" type="text"  class="inputtxt"  onpaste= "return   false; "   ondragenter= "return   false;" tabindex="0"  autocomplete="off" />
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
	<div class="tab keyText">Tab</div><div class="keyCom" id="keyQ">Q</div>
	<div class="keyCom" id="keyW">W</div><div class="keyCom" id="keyE">E</div>
	<div class="keyCom" id="keyR">R</div><div class="keyCom" id="keyT">T</div>
	<div class="keyCom" id="keyY">Y</div><div class="keyCom" id="keyU">U</div>
	<div class="keyCom" id="keyI">I</div><div class="keyCom" id="keyO">O</div>
	<div class="keyCom" id="keyP">P</div><div class="keyCom" id="keyZKH">{<br />[</div>
	<div class="keyCom" id="keyYKH">}<br />]</div><div class="enterup1 keyText"></div>
<!--第三行-->
	<div class="cap keyText">Caps</div><div class="keyCom" id="keyA">A</div>
	<div class="keyCom" id="keyS">S</div><div class="keyCom" id="keyD">D</div>
	<div class="keyCom" id="keyF">F</div><div class="keyCom" id="keyG">G</div>
	<div class="keyCom" id="keyH">H</div><div class="keyCom" id="keyJ">J</div>
	<div class="keyCom" id="keyK">K</div><div class="keyCom" id="keyL">L</div>
	<div class="keyCom" id="keyFHMH">:<br />;</div><div class="keyCom" id="keyDYSY">"<br />'</div>
	<div class="enterup2 keyText">Enter</div>
<!--第四行-->
	<div class="shift keyText" id="shiftl">Shift</div><div class="keyCom" id="keyZ">Z</div>
	<div class="keyCom" id="keyX">X</div><div class="keyCom" id="keyC">C</div>
	<div class="keyCom" id="keyV">V</div><div class="keyCom" id="keyB">B</div>
	<div class="keyCom" id="keyN">N</div><div class="keyCom" id="keyM">M</div>
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
        ImageUrl="~/Images/py.png" NavigateUrl="~/Student/mychinese.aspx" ></asp:HyperLink> 
    <asp:HyperLink ID="HkFinger" runat="server" 
        ImageUrl="~/Images/en.png" NavigateUrl="~/Student/myfinger.aspx"></asp:HyperLink>        
    <asp:HyperLink ID="HTyper" runat="server" 
        ImageUrl="~/Images/cn.png" NavigateUrl="~/Student/mytype.aspx" ></asp:HyperLink>       
    </div>
    <br />选择级别：
    <select name="ls" id="levelselect"  onchange="changelevel()" 
        style="font-size: 9pt" >
    <option value="0">小学英语</option>
    <option  value="1" selected="selected">中考英语</option>
    <option value="2">高考英语</option>
    </select>
    <br />
    <div id="snum" style="display:none"><%=this.mysnum%></div>
    <div class="letter"></div>
    <div id="lrpe" class="letter"></div>
    <div id="lnum" class="letter"></div>
    <div id="lrig" class="letter"></div>
    <div id="lwrg" class="letter"></div>
    <div id="wnum" class="letter"></div>
    <div id="lspd" class="letter"></div>
    <div id="wspd" class="letter"></div>
    <div id="lsec" class="letter"></div>
    <div id="weid" class="letter"></div>
    <div class="letter"></div>    
    <br />
    <div id="oldspd" runat="server" class="letter"></div> 
    <br />
    <div id="msg"></div>
    <br />
    <br />
    <asp:HyperLink ID="HLfinger" runat="server" 
        NavigateUrl="~/Student/allfinger.aspx" Target="_self" SkinID="HyperLink" 
        Width="120px" CssClass="txtszcenter" Height="18px">英文输入英雄榜</asp:HyperLink>
    <br />    
    <div id="victory" style=" display:none">
    <img src="../Js/images/v.gif"  alt=""/>
    </div>
    <br />
</div>
<br />
    <script src="../Js/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../Js/Finger.js" type="text/javascript"></script>
    <div id="tempdiv" style=" display:none"></div>
</div>
</asp:Content>


