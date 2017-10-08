<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .big
        {
            font-family: Arial;
            font-size: 96pt;
            font-weight: bold;
            color: #0000CC;
            text-align: center;
        }
        .small
        {
            font-family: Arial;
            font-size: 12pt;
            font-weight: bold;
            color: #0000CC;
            text-align: center;
        }
    </style>
    <script  runat="server">
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //（温州水乡 Learnsite学习平台机房布置专用）
                myip.InnerText = Page.Request.UserHostAddress;//取客户机IP 
                //myhostname.InnerText = Page.Request.UserHostName;//取客户机主机名
            }
        }            
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <br />
    <br />
    <br />
    <div id="myhostname" runat="server" title="显示本机主机名（其他用途，机房布置未用到）">
    <br />
    <br />
    <div id="myip" class="big" runat="server">
    </div>
    <br />    
    <br />
    <br />
    <br />
    <br />
    </div>
    </form>
</body>
</html>
