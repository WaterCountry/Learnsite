//upmodel值表示：jpg格式横幅显示
//请自行选择
function ShowRoad(first) {
    bannerjpg(first);
}

function bannerjpg(first) {
    jpgurl = first + "Images/road.jpg";
    var w = 980;
    var h = 100;
    document.write('<img src="' + jpgurl + '"  width="' + w + '" height="' + h + '"/>');
}