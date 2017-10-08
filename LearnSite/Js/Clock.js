function ShowFlash()
{ 
url="../Images/clock.swf";
var Width=120;
var Height=120;
var wmode='transparent';
document.write(
  '<embed src="' + url + '" wmode=' + wmode +
  ' quality="high" pluginspage=http://www.macromedia.com/go/getflashplayer type="application/x-shockwave-flash" width="' + Width + 
  '" height="' + Height + '"></embed>');   
}