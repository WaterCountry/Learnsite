//upmodelֵ��ʾ��jpg��ʽ�����ʾ
//������ѡ��
function ShowRoad(first) {
    bannerjpg(first);
}

function bannerjpg(first) {
    jpgurl = first + "Images/road.jpg";
    var w = 980;
    var h = 100;
    document.write('<img src="' + jpgurl + '"  width="' + w + '" height="' + h + '"/>');
}