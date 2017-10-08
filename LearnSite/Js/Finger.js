var wordKeyObj; //当前要打的字符键对象
var oldwordKeyObj; //上次字符键对象
var downKeyWord; //按下键值转成的字符
var downKeyObj; //按下的键对像
var picWord="keyCom2.gif"; //要打键的图
var picKey = "keyCom.gif"; //键正常状态图
var picErr = ""; //键错误图
var wordright = 0; //单词正确数
var letterright = 0; //字母正确数
var lettercount = 0;//字母总数
var letterwrong = 0; //字母错误数
var lastsecond = 0;
var lastminute = 0;
var eposition = 0;
var eidold = 0;
var savecount = 1;
var oldmspd = 2;
var realsc = 0;
var pid = "0";
var pword = "";
var pmeaning = "";
var mspd = 0;
var mysnum = "1";
var ewords;
var emeanings;
var ecounts = 0;
var mcounts = 0;
var enext = 0;
var rnd = 0;
var order = 0;//如果为0则随机，如果为1则顺序
var TypeWord = "";  //当前单词
var TypeLengh = 0;
var docurl = document.URL;
var ipurl = docurl.substring(0, docurl.lastIndexOf("/"));
var victoryshow = 0;

var elevel = $('#levelselect').val();//取英文类别
NewWords(); //获取该类别字典

function nextword() {
    if (ewords != null) {
        if (order == 0) {
            GetRandom(ecounts - 1);
            pword = ewords[rnd];
            pmeaning = emeanings[rnd];
        }
        else {
            if (enext == ecounts) {
                enext = 0;
            }
            pword = ewords[enext];
            pmeaning = emeanings[enext];
        }
        $('#TextWord').html(pword);
        var changemeaning = GetMeaning(pmeaning);
        $('#Meanword').html(changemeaning);
        TypeWord = pword;
        TypeLengh = TypeWord.lengh;
        eposition = 0;
        var ktk = TypeWord.substr(eposition, 1);
        dkObj = "key" + ktk.toUpperCase();
        showKey(dkObj);
    }
}

document.onkeydown = function (event) {
    e = event ? event : (window.event ? window.event : null);
    if (e.keyCode == 13) {
        return false;
    }
}
passsecond();

function changelevel() {
    lastsecond = 0;//换类别时重新开始计时
    elevel = $('#levelselect').val();
    NewWords();
}

function passsecond() {
    if (victoryshow < 0)
        $('#victory').hide();
    else
        victoryshow--;
    lastsecond++; //秒计时
    $('#lspd').html("字母速度：" + (letterright / lastsecond).toFixed(2) + "个/秒");
    $('#lsec').html("流失时间：" + lastsecond + "秒");
    lastminute = lastsecond / 60;
    if (lastminute / savecount == 1) {
        savecount++;
        mspd = (wordright / lastminute).toFixed(2);
        $('#wspd').html("单词速度：" + mspd + "个/分");
        mysnum = $('#snum').html();  //取学号        
        if (mspd > oldmspd) {
            realsc++;
            oldmspd = mspd;
            SaveMspd(); //调用ashx进行保存，返回自动保存成绩成功!
            $('#victory').show(); //显示速度提升胜利手势
            victoryshow = 3;
        }
        else {
            $('#msg').html("比刚才的速度低!<br/>系统不自动保存");
        }
    }
    if (lettercount == 0)
        lastsecond = 0;//如果还未输入，则流失时间重置
    window.setTimeout("passsecond()", 1000); //秒定时
}

$('#InputWord').keyup(function () {
    var iword = $(this).val();
    var il = iword.length;
    var tl = TypeWord.length;
    eposition = il; //获取当前字符位置
    if (il > tl) {
        $(this).val(iword.substring(0, il - 1));
    }
    else {
        if (TypeWord == iword) {
            wordright++; //单词统计
            enext++;
            lettercount = lettercount + il; //字母统计
            letterright = lettercount - letterwrong;
            var rightrank = 0;
            if (lettercount > 0)
                rightrank = letterright * 100 / lettercount;
            $('#lrpe').html("正确率：" + rightrank.toFixed(2) + "%");
            $('#lnum').html("字母数：" + lettercount);
            $('#lrig').html("正确数：" + letterright);
            $('#lwrg').html("错误数：" + letterwrong);
            $('#wnum').html("单词数：" + wordright);
            nextword();
            $(this).val("");
        }
        else {
            var redword = "";
            var iright = 0;
            for (var i = 0; i < il; i++) {
                if (iword.substr(i, 1) == TypeWord.substr(i, 1)) {
                    iright = i;
                    redword = redword + TypeWord.substr(i, 1); //对的话加上这个字符
                }
                else {
                    letterwrong++;
                    redword = redword + '<span class=\"wrongchar\">' + TypeWord.substr(i, 1) + '</span>'; //错的话加上这个变红字符
                }
            }
            $('#TextWord').html(redword + TypeWord.substr(il, TypeLengh));
        }
        presskey();
    }
});

function presskey() {
    var e = window.event ||arguments.callee.caller.arguments[0];
    var k = e.keyCode || e.which;

    downKeyWord = String.fromCharCode(k);
    if (downKeyWord != "") {
        var keytokey = TypeWord.substr(eposition, 1);  //downKeyWord;
        downKeyObj = "key" + keytokey.toUpperCase();
        showKey(downKeyObj);
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

function NewWords() {
    var geturl = ipurl + "/FingerHandler.ashx?MyElevel=" + elevel;
    $.ajax({
        type: "Get",
        url: geturl,
        dataType: "html",
        success: function (data) {
            if (data != "") {
                var strwd = data.split("==");
                var ew = strwd[0];
                var em = strwd[1];
                ewords = ew.split("|");
                emeanings = em.split("|");
                ecounts = ewords.length;
                mcounts = emeanings.length;
                if (ecounts > 0) {
                    $('#weid').html("单词数" + ecounts + " 意义数" + mcounts);
                    $('#msg').html("每过1分钟自动保存<br/>达到最低要求的成绩!");
                    $('#InputWord')[0].focus();
                    enext = 0;
                    nextword();
                }
                else {
                    $('#Meanword').html("找不到该级别单词！");
                }
            }
        }
    });
}

//自动保存成绩
function SaveMspd() {
    var saveurl = ipurl + "/SaveHandler.ashx?Mysnum=" + mysnum + "&Myspd=" + mspd;
    $.ajax({
        type: "Get",
        url: saveurl,
        dataType: "html",
        success: function (data) {
            if (data > 0) {
                $('#msg').html("我的最快速度：" + mspd + "个/分<br/>" + "自动保存：" + realsc + "次");
            }
            else {
                if (mysnum != "")
                    $('#msg').html("自动保存无效，成绩比原来低！");
            }
        }
    });
}


function GetRandom(n) {
    rnd = Math.floor(Math.random() * n + 1); //获取随机范围内数值的函数
}
//处理换行问题
function GetMeaning(meaning) {
    $('#tempdiv').html(meaning);
    return $('#tempdiv').text();
}