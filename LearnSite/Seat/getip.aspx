<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .mid
        {
            font-family: Arial;
            font-size: 60pt;
            font-weight: bold;
            color: #0000CC;
            text-align: center;
        }
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
            font-size: 120pt;
            font-weight: bold;
            color: #000000;
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
                myhostname.InnerText = System.Net.Dns.GetHostName();//取客户机主机名
                mytime.InnerText = DateTime.Now.ToLongTimeString().ToString();
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
    <div id="myhostname" class="mid" runat="server" title="本机主机名"></div>
    <br />
    <br />
    <div id="myip" class="big" runat="server" title="本机ip"></div>
    <br />
    <br />
    <br />
    <div id="mytime" class="small" runat="server" title="本机时间"></div>    
    <br />
    <br />
    </form>
</body>
</html>
