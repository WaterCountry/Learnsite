var wenzhi = $('#Tcontent').text().replace(/(\s*$)/g, "");
var wln = wenzhi.length;
var timeset = wln*3;//限制打字时间为一个字3秒
var timeall = timeset;
$('#Labelmsg').html("打字限时为" + timeset + "秒");
$('#Text7').val(timeall);
var typestart = 0;
var myspeed = 0;
var rightsave = 0;
var check = 0;
var oldln = 0;
var lmm = 6; //限制一次性输入汉字的长度
var limitmsg = "你不可以一次输入超过" + lmm + "个汉字的长句子！";
$('#InputText')[0].focus();
$('#InputText').keyup(function () {
    var strtxt = $('#InputText').val();
    var ln = strtxt.length;
    if (ln - oldln > lmm) {
        $('#InputText').val(strtxt.substr(0, oldln));
        $('#Labelmsg').html(limitmsg);
    }
    else {
        if (ln > 0) {
            if (ln < wln) {
                var nextword = wenzhi.substr(ln, 1);
                var pingyistr = trans(nextword);
                var wbstr = trans_wb(nextword);
                $('#Textpy').val(pingyistr);
                $('#Textwb').val(wbstr);
            }
            checktype(strtxt);

            typestart = 1;
            $('#Labelmsg').html("");
            if ((ln == wln )||(ln > wln - 1)) {
                SaveMspd(); //打完就自动保存，并刷新页面
            }
            if (timeall < 0) {
                SaveMspd(); //超过限制时间也自动保存
            }

        }
    }
});

function checktype(s) {
    var ln = s.length;
    var right = 0;
    var wrong = 0;
    var colorword = "";
    for (var i = 0; i < ln; i++) {
        var inword = s.substr(i, 1);
        var tpword = wenzhi.substr(i, 1);
        if (inword == tpword) {
            colorword += "<span class='truechar'>" + tpword + "</span>";
            right++;
        }
        else {
            colorword += "<span class='falsechar'>" + tpword + "</span>";
            wrong++;
        }
    }
    oldln = ln;
    rightsave = right;
    check = right - wrong;
    $('#Text4').val(rightsave);
    var showhtml = colorword + wenzhi.substr(ln, wln - ln);
    $('#Tcontent').html(showhtml);
}
lefttime();
function lefttime() {
    if (typestart == 1) {
        $('#Text7').val(timeall);
        timeall = timeall - 1;
        var spendtime = timeset - timeall;
        myspeed = parseInt((check * 60) / spendtime);
        var speeder = myspeed + "字/分";
        if (spendtime > 1 && rightsave > 0) {
            $('#Text6').val(speeder);
        }

        if (timeall < 0) {
            SaveMspd(); //超过限制时间也自动保存
        }
    }
    window.setTimeout("lefttime()", 1000);
}
//自动保存成绩
function SaveMspd() {
    typestart = 0;
    if (check < -10) {
        alert("刷屏无效！");
        window.location.reload();
    }
    else {
        var Ts = myspeed;
        var Ptid = $('#ctl00_Cphs_LTid').html();
        var docurl = window.location.href;
        var ipurl = docurl.substr(0, docurl.lastIndexOf("/"));
        var saveurl = ipurl + "/Savetype.ashx?Ptid=" + Ptid + "&Ts=" + Ts;
        var msg = "你的打字速度为：" + getrecommend(Ts) + "自动保存成绩成功！";
        var error = "同班只能使用一个账号在本电脑上打字，本次成绩无效！";
        $.ajax({
            type: "Get",
            url: saveurl,
            dataType: "html",
            success: function (data) {
                var res = data.toString();
                switch (res) {
                    case "1":
                        alert(msg);
                        window.location.reload();
                        break;
                    case "-1":
                        alert("保存失败！");
                        window.location.reload();
                        break;
                    case "-2":
                        alert(error);
                        window.location.reload();
                        break;
                    default:
                        alert("加油！超过才更新成绩！");
                        window.location.reload();
                        break;
                }
                $('#InputText').val("");
                timeall = timeset;
                rightsave = 0;
                oldright = 0;
                oldcolorword = "";
            }
        });
        timeall = timeset;
        right = 0;
    }
}

function getrecommend(spd) {
    var recommend = "";

    if (spd < 10) {
        recommend = "嫩芽1级，还要加油哦！";
    }
    if (spd > 10 && spd < 20) {
        recommend = "青虫2级，还要加油哦！";
    }
    if (spd > 20 && spd < 30) {
        recommend = "菜鸟3级，还要加油哦！";
    }
    if (spd > 30 && spd < 40) {
        recommend = "老鸟4级，继续努力！";
    }
    if (spd > 40 && spd < 60) {
        recommend = "大虾5级，很不错了！";
    }
    if (spd > 60 && spd < 80) {
        recommend = "名士6级，可以独步天下了！";
    }
    if (spd > 80 && spd < 100) {
        recommend = "大师7级，可以招徒授艺了！";
    }
    if (spd > 120 && spd < 160) {
        recommend = "宗师8级，可以开宗立派了！";
    }
    if (spd > 160) {
        recommend = "神人9级，宇宙无敌高高手！";
    }
    var rmsg = spd.toString() + "字/分 " + recommend;
    return rmsg;
}