<%@ page language="C#" autoeventwireup="true" inherits="Student_codeshare, LearnSite" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <style type="text/css">
        body{margin: 0;text-align: center;}
    </style>
</head>
<body>
<div style="text-align: center;">

  <script type="text/javascript">
    window.scratchConfig = {
      
      handleVmInitialized: (vm) => {
        window.vm = vm
        console.log("VM")
        
      },
      handleProjectLoaded:() => {
        console.log("load")

      },
      handleDefaultProjectLoaded:() => {

          window.scratch.loadProject("<%=sbfile %>", () => { 
             console.log("add")
             window.scratch.setProjectName("<%=sbtitle %>")
          })
      },
    }


  </script>

<center>
  <div id="scratchplayer" style=" width:482px; height:400px; border-color:Gray; border-width:thin;">    
  </div>
</center>
<script type="text/javascript" src="../scratch/lib.min.js"></script>
<script type="text/javascript" src="../scratch/chunks/player.js"></script>

  </div>
</body>
</html>

