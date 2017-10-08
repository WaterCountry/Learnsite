function ShowFlash() {
    var url = "../Images/road.swf";
    var Width = 640;
    var Height = 480;
    var wmode = 'transparent';
    document.write(
  '<embed src="' + url + '" wmode=' + wmode +
  ' quality="high" pluginspage=http://www.macromedia.com/go/getflashplayer type="application/x-shockwave-flash" width="' + Width +
  '" height="' + Height + '"></embed>');
}