var docurl = document.URL;
var ipurl = docurl.substring(0, docurl.lastIndexOf("/"));

function SaveSeats(hid, collects) {
    var saveurl = ipurl + "/saveseat.ashx?Hid=" + hid + "&Collects=" + collects;
    $.ajax({
        type: "Get",
        url: saveurl,
        dataType: "html",
        success: function (data) {
            if (data.toString() == "1") {
                $('#msg').html("当前布置提交成功!");
            }
            else {
                $('#msg').html("当前布置提交失败!");
            }
        }
    });
}