//upmodel值表示：0为swf格式横幅显示，1为jpg格式横幅显示
//请自行选择
function ShowRoad(first) {
    var upmodel = '1';  
    switch (upmodel) {
        case '0':
            bannerswf(first);
            break;
        case '1':
            bannerjpg(first);
            break;
    }
}
function bannerswf(first) {
    swfurl = first + "Images/road.swf";
    var w = 980;
    var h = 100;
    document.write('<object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000"  width="' + w + '" height="' + h + '">');
    document.write('<param name="movie" value="' + swfurl + '" />');
    document.write('<param name="quality" value="high" />');
    document.write('<param name="wmode" value="transparent">');
    document.write('<embed wmode="transparent" src="' + swfurl + '" quality="high" type="application/x-shockwave-flash" width="' + w + '" height="' + h + '"></embed>');
    document.write('</object>');
}
function bannerjpg(first) {
    jpgurl = first + "Images/road.jpg";
    var w = 980;
    var h = 100;
    document.write('<img src="' + jpgurl + '"  width="' + w + '" height="' + h + '"/>');
}