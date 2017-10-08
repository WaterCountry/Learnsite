$(function () {
    var x = 10;
    var y = 20;
    $(".computer").mouseover(function (e) {
        var id = $(this).attr("id");
        var ptype = $(this).attr("tabindex");
        var sname = $(this).html();
        $('#msg').html(id + ":" + sname);
        var mynum = $(this).attr("title");
        if (mynum != "?") {
            if (ptype != "0") {
                var imgtype = ".jpg";
                if (ptype === "2")
                    imgtype = ".gif";
                var url = "../StudentPhoto/" + mynum + imgtype;
                var tooltip = "<div id='tooltip'><img src='" + url + "' alt='原图'/></div>"; //创建 div 元素
                $("body").append(tooltip); //把它追加到文档中
            }
        }
        $("#tooltip")
			.css({
			    "top": (e.pageY + y) + "px",
			    "left": (e.pageX + x) + "px"
			}).show("fast");   //设置x坐标和y坐标，并且显示
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