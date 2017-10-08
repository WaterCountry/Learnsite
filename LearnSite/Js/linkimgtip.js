$(function () {
    var x = 10;
    var y = 20;
    $("a").mouseover(function (e) {
        var url = $(this).attr("href");
        if (ispic(url)) {
            var tooltip = "<div id='tooltip'><img src='" + url + "'/></div>"; //创建 div 元素
            $("body").append(tooltip); //把它追加到文档中
            $("#tooltip")
			.css({
			    "top": (e.pageY + y) + "px",
			    "left": (e.pageX + x) + "px"
			}).show("fast");   //设置x坐标和y坐标，并且显示
        }
    }).mouseout(function () {
        $("#tooltip").remove();  //移除 
    }).mousemove(function (e) {
        $("#tooltip")
			.css({
			    "top": (e.pageY + y) + "px",
			    "left": (e.pageX + x) + "px"
			});
    });
})

function ispic(fname) {
    var isok = false;
    var pos = fname.lastIndexOf("."); //计算出点的位置
    var ext = fname.substring(pos + 1); //截取点之后的字符串
    switch (ext) {
        case "jpg":
        case "png":
        case "gif":
            isok = true;
            break;
    }
    return isok;
}