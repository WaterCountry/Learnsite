clockShow();
function clockShow() {
    var now = new Date();
    var hour = now.getHours();
    var minute = now.getMinutes();
    var second = now.getSeconds();
    document.getElementById("TextTime").value = hour + ":" + minute + ":" + second + " ";
    window.setTimeout("clockShow()", 1000);
}
