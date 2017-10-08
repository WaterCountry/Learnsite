var wordKeyObj; //当前要打的字符键对象
var oldwordKeyObj; //上次字符键对象
var downKeyWord; //按下键值转成的字符
var downKeyObj; //按下的键对像
var picWord = "keyCom2.gif"; //要打键的图
var picKey = "keyCom.gif"; //键正常状态图
var picErr = ""; //键错误图
var pos = 0;        //初始化当前位置为0 
var apples = 0; //苹果数
var chinaright = 0; //输入的正确字数
var wordslist = new Array(); //定义数组
var currentwords = "";
var wordscount = 0;
var lastsecond = 1;
var nidsave = 0;
var keycount = 0; //获取词语拼音时的击键次数，根据击键次数跟词语长度的对比，判断是否使用了自定义短语

$(document).ready(function () {
    var nid = $('#ctl00_Cphs_Lbnid').text();
    GetWords(nid); //读取拼音词语
    $('#InputWord').focus();
    $('#InputWord').keyup(function () {
        presskey();
        typingwords(); //打字检测
    });
});

passsecond(); //读秒

document.onkeydown = function (event) {
    e = event ? event : (window.event ? window.event : null);
    if (e.keyCode == 13) {
        return false;
    }
}

function presskey() {
    var e = window.event || arguments.callee.caller.arguments[0];
    var k = e.keyCode || e.which;

    downKeyWord = String.fromCharCode(k);
    if (downKeyWord != "") {
        var keytokey = downKeyWord;  //downKeyWord;
        downKeyObj = "key" + keytokey.toUpperCase();
        showKey(downKeyObj);
        keycount++;
    }
}

function showKey(wordKeyObj) {
    var ww = document.getElementById(oldwordKeyObj);
    if (ww != null) {
        ww.style.backgroundImage = "url(../Images/Fingering/" + picKey + ")";
    }
    var aa = document.getElementById(wordKeyObj);
    if (aa != null) {
        aa.style.backgroundImage = "url(../Images/Fingering/" + picWord + ")";
    }
    oldwordKeyObj = wordKeyObj;
}


//读取拼音词语列表
function GetWords(nid) {
    nidsave = nid;
    var geturl = "ChineseHandler.ashx?Nid=" + nid;
    $.ajax({
        type: "Get",
        url: geturl,
        dataType: "html",
        success: function (data) {
            if (data != "") {
                var chinesewords = data;
                wordslist = chinesewords.split("|");
                wordscount = wordslist.length;
                if (wordscount > 0) {
                    $('#InputWord')[0].focus();
                    pos = 0;
                    var ckname = 'reliveType' + nidsave + 'Cn';
                    var reliveCookie = $.cookie(ckname);
                    if (reliveCookie != null) {
                        pos = reliveCookie;
                    }
                    $('#totalapples').html("");
                    currentwords = wordslist[pos];
                    showwords();
                }
            }
        }
    });
}

//输入词语响应
function typingwords() {
    var iword = $.trim($('#InputWord').val());
    var ln = iword.length;
    var crln = currentwords.length;
    var right = 0;

    if (ln > 0) {
        for (var j = 0; j < ln; j++) {
            var chword = iword.substr(j, 1);
            var crword = currentwords.substr(j, 1);
            if (chword == crword)
                right++;
        }
        if (right == crln) {
            if (keycount > right) {
                chinaright = parseInt(chinaright) + parseInt(crln); //统计正确的输入字数
                pos++; //如果输入词语等于当前词语，则游标位置称到下个词语
                $('#InputWord').val("");
                if (pos >= wordscount) {
                    pos = 0;
                };

                currentwords = wordslist[pos]; //读取下个词语
                showwords(); //显示词语和拼音 
                $('#msg').html("");
                var relive = pos;
                var ckname = 'reliveType' + nidsave + 'Cn';
                $.cookie(ckname, relive, { expires: 1 }); //$.cookie('the_cookie', 'the_value');           
            }
            else {
                $('#InputWord').val("");
                $('#msg').html("请重新输入词语！");
            }

            keycount = 0; //重新统计击键次数
        }
        showapple(); //显示苹果
    }
}

//显示要打的词语
function showwords() {
    $('#Typechinese').html(currentwords); //显示词语
    var tip = trans(currentwords);

    $('#Typepingyin').html(tip); //显示拼音
    $('#InputWord').html("");
}
//显示苹果数
function showapple() {
    var appleone = "<img alt='' src='../images/apple.gif' />";
    var applehtm = "";
    if (chinaright > 0) {
        for (var k = 0; k < chinaright; k++) {
            applehtm = applehtm + appleone;
        }
    }
    $('#apples').html(applehtm);
}

//自动保存成绩
function SaveMspd() {
    if (chinaright > 0) {
        var rightnow = chinaright;
        chinaright = 0; //自动保存后，清空获得得苹果数
        var wordspeed = parseInt(rightnow) * 60 / lastsecond;
        lastsecond = 0; //读秒清零
        apples = parseInt(apples) + parseInt(rightnow);
        var saveurl = "SaveChinese.ashx?Apples=" + parseInt(rightnow) + "&Speed=" + parseInt(wordspeed);

        $.ajax({
            type: "Get",
            url: saveurl,
            dataType: "html",
            success: function (data) {
                $('#totalapples').html(data.toString());
                $('#apples').html("");
            }
        });
    }
}

function passsecond() {
    lastsecond++; //秒计时
    if (chinaright > 0 && lastsecond > 20) {
        var testspeed = parseInt(chinaright) * 60 / lastsecond;
        if (testspeed < 200)
            SaveMspd(); //如果收集到苹果且打字时间大于20秒，才自动保存；
        else {
            chinaright = 0;
            lastsecond = 0; //读秒清零
            $('#apples').html("");
            alert("你刷苹果的速度太快了！");
        }
    }
    if (lastsecond > 20)
        lastsecond = 0;
    window.setTimeout("passsecond()", 1000); //秒定时
}