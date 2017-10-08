<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Stud.master" AutoEventWireup="true" CodeFile="mymaker.aspx.cs" Inherits="Student_mymaker" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cphs" Runat="Server">
    <link href="../js/flexslider.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery.flexslider-min.js" type="text/javascript"></script>
<div class="flexslider carousel">
    <ul class="slides">
   <li>
    <a href="../blockly/index.html" target="_blank">
    <img src="../Images/blockly.gif"  alt="Blockly拼图" />
    </a>
  </li>
  <li>
    <a href="../ardublockly/index.html" target="_blank">
    <img src="../Images/ardublockly.gif" alt="Blockly机器人"/>
    </a>
  </li>
  <li>
    <a href="../plugins/sketchup/threejscloth.html" target="_blank">
    <img src="../Images/ThreeJsCloth.gif" alt="3D建模"/>
    </a>
  </li>
   </ul>
</div>
     <script type="text/javascript">
         $(window).load(function () {
             $('.flexslider').flexslider({
                 animation: "slide", 
                 controlNav: false,
                 animationLoop: false,
                 itemWidth: 960,                  
        		 itemMargin: 12
             });
         });

    </script>
</asp:Content>

