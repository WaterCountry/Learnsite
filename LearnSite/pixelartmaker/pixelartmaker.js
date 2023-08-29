/*jslint browser: true*/
/*global  $*/

$(function () {
    "use strict";
    //the table where the grid will be built
    const PIXELCANVAS = $("#pixel_canvas");
    const PALETTE = $("#palette"); //color selection table
    const BODY = $("body");
    let colorPicker = "rgb(0, 0, 0)";
	let oldcolor;
    $("#colorPicker").on("change", function (e) {
        colorPicker = $(e.target).val();
		console.log(colorPicker);
			
    });
	
	
    // determine wether mouse is down or not
    let mouseDown = false;
	let mouseright=false;
	let mousegrid=true;
	let userid="wzsxpixel";
	let usercount="imgcount";
	let imgcount=0;
	let imgpos=1;
	
    PIXELCANVAS.on("mousedown", function (event) {
		if(event.button == 0){
			mouseDown = true;
			return false; //this stops a bug where continuous painting sometimes occurred when mouse was not held down
		}else if(event.button == 2){
			mouseright = true;
			return false; //this stops a bug where continuous painting sometimes			
		}
    });
			
    PIXELCANVAS.on("click", function (event) {
		saveimg();
    });
	
	function saveimg(){	
		imgcount++;	
		var img=PIXELCANVAS.html();
		imgpos=imgcount;

		if (imgcount==100) {
			localStorage.clear();
			imgpos=imgcount=0;
		}
		var userimg=userid+imgcount;
		localStorage.setItem(userimg,img);
		localStorage.setItem(usercount,imgcount);		
	}
		
	function history() {
		var imgcountset=localStorage.getItem(usercount);	
		if(imgcountset!=null){imgcount=parseInt(imgcountset);}
		
		if(imgpos<1){ imgpos=1;}		
		if(imgpos>imgcount){ imgpos=imgcount;}
		
		var userimg=userid+imgpos;
		var img=localStorage.getItem(userimg);
		PIXELCANVAS.html(img);	
	}	

	$(document).ready(function(){
        palette();	
		var img=PIXELCANVAS.html();
		if(img==""){
			makeGrid();
			console.log("内容为空");
		}
		else
		{
			console.log("加载完毕");
			//console.log(img);
		}
	});
	
    BODY.on("mouseup", function () {
        mouseDown = false;
		mouseright = false;
    });
	
	$("#logo").on("mousedown", function () {
		mousegrid = !mousegrid;
		//console.log(mousegrid);
		if(mousegrid === true){
			$(".cell").css("border-width", "1px");
		} else {
			$(".cell").css("border-width", "0px");
		}
    });
	
    //fill PALETTE with colours
	const colorArr = [
		"rgb(0,0,0)",
		"rgb(34,32,52)",
		"rgb(69,40,60)",
		"rgb(102,57,49)",
		"rgb(143,86,59)",
		"rgb(223,113,38)",
		"rgb(217,160,102)",
		"rgb(238,195,154)",
		"rgb(229,184,7)",
		"rgb(251,242,54)",
		"rgb(153,229,80)",
		"rgb(73,210,73)",
		"rgb(106,190,48)",
		"rgb(55,148,110)",
		"rgb(75,105,47)",
		"rgb(82,75,36)",
		"rgb(50,60,57)",
		"rgb(63,63,116)",
		"rgb(48,96,130)",
		"rgb(91,110,225)",
		"rgb(99,155,255)",
		"rgb(95,205,228)",
		"rgb(203,219,252)",
		"rgb(255,255,255)",
		"rgb(155,173,183)",
		"rgb(132,126,135)",
		"rgb(105,106,106)",
		"rgb(89,86,82)",
		"rgb(118,66,138)",
		"rgb(172,50,50)",		
		"rgb(215,28,28)",
		"rgb(217,87,99)",
		"rgb(215,123,186)",
		"rgb(143,151,74)",
		"rgb(138,111,48)"
	];
    function colorPalette() {
        let hue;
        let i;
        for (i = 0; i < colorArr.length; i += 1) {
            hue = colorArr[i];
							
			if(i==0){				
				$(".palette:first")
					.css("background-color", hue)
					.removeClass("palette")
					.addClass("paletteFull")					
					.css("border-width", "2px");
			} else{
				$(".palette:first")
					.css("background-color", hue)
					.removeClass("palette")
					.addClass("paletteFull");				
			}
        }
		
    }
    // make cells for colour palette
    function palette() {
        PALETTE.children().remove();
        PALETTE.prepend("<tr></tr>");
        const tr = $("tr");
        let i;
        for (i = 1; i <= colorArr.length; i += 1) {
			tr.first().append('<td class="palette"></td>');
        }

        colorPalette();
    }
    function makeGrid() {		
        const ROW = $("#input_height").val();
        const COLUMN = $("#input_width").val();
        //remove old grid (if any)
        PIXELCANVAS.children().remove();
        //build grid
        let i2;
        for (i2 = 1; i2 <= ROW; i2 += 1) {
            PIXELCANVAS.append("<tr></tr>");
            const tr = $("tr");
            let i;
            for (i = 1; i <= COLUMN; i += 1) {
                tr.last().append("<td class='cell'></td>");
            }
        }
    }
	
	
    // When size is submitted by the user, call makeGrid
    $("#sizePicker").on("submit", function (e) {
        e.preventDefault(); //prevent the page from reloading
        makeGrid();
    });
    //paint when a cell is clicked
	
    PIXELCANVAS.on("click", ".cell", function (e) {	
        const CURRENTCOLOR = oldcolor;
        //current cell will change color
        if (CURRENTCOLOR === "rgba(0, 0, 0, 0)") {
            $(e.target).css("background-color", colorPicker);
			oldcolor=colorPicker;
        }else {
			if(CURRENTCOLOR===colorPicker){			
            $(e.target).css("background-color", "rgba(0, 0, 0, 0)");
			oldcolor="rgba(0, 0, 0, 0)";
			}else{				
				$(e.target).css("background-color", colorPicker);
				oldcolor=colorPicker;
			}
        }
		voice();
    });
	
	
    // paint when mouse held down
    PIXELCANVAS.on("mouseenter", ".cell", function (e) {
		oldcolor=$(this).css("background-color");
        if (mouseDown === true) {
            $(e.target).css("background-color", colorPicker);
			oldcolor=colorPicker;
			//console.log(oldcolor);
			
        }else if(mouseright === true){
			$(e.target).css("background-color", "rgba(0, 0, 0, 0)");
			oldcolor="rgba(0, 0, 0, 0)";
		}
		
		$(this).css("background-color",colorPicker);
    });
	
	    // paint when mouse held down
    PIXELCANVAS.on("mouseout", ".cell", function (e) {
		$(this).css("background-color",oldcolor);
        if (mouseDown === true) {
            $(e.target).css("background-color", colorPicker);
			oldcolor=colorPicker;
			//console.log(oldcolor);
			
        }else if(mouseright === true){			
			$(e.target).css("background-color", "rgba(0, 0, 0, 0)");
			oldcolor="rgba(0, 0, 0, 0)";
		}
    });
	
	
	    // paint when mouse held down
    PIXELCANVAS.on("mouseenter", ".cell", function (e) {
		if (mouseDown === true && mouseright === true ) {		
			saveimg();
		}
	});

    // When a PALETTE cell is clicked, colorPicker value is that colour
    PALETTE.on("click", ".paletteFull", function (e) {
        colorPicker = $(e.target).css("background-color");
	
		$(".paletteFull").css("border-width", "1px");
		$(e.target).css("border-width", "2px");
    });
	
	$('body').bind('contextmenu', function() {
            return false;
    });
	
	function voice() {
		var audio = document.createElement("audio");
		audio.src = '../pixelartmaker/code.ogg';
		audio.play();
	}
			
	document.addEventListener('copy', function(e) {
		e.preventDefault();
		var img=PIXELCANVAS.html();
		e.clipboardData.setData('text', img);
		console.log("copy");
	});
	document.addEventListener('paste', function(e) {
		var img=e.clipboardData.getData('text');
		if(img!=null){
			PIXELCANVAS.html(img);
			console.log("paste");
		} else {
			history();			
		}
	});
	
	function savetopng(){	
		var img=PIXELCANVAS.html();		
		html2canvas(document.querySelector("#pixel_canvas")).then(pxl => {
			//document.body.appendChild(pxl);
            var urls = 'uploadpixel.ashx?id=' + id;
	        var formData = new FormData();
			img=encodeURIComponent(img);
	        formData.append('pxl', img);

	        $.ajax({
	            url: urls,
	            type: 'POST',
	            cache: false,
	            data: formData,
	            processData: false,
	            contentType: false
	        }).done(function (res) {
	            //alert("保存成功！");
	            console.log(res)
	        });
		});
	}
	
	$(document).keydown(function(event){
        var keyNum = event.which;   //获取键值
        switch(keyNum){  //判断按键
        case 37: console.log("左"); imgpos--;history(); break;
        case 39: console.log("右");imgpos++;history();break;
        case 119: console.log("保存");savetopng();break;
        default:
            break;        
        }
    });

	$("#savebtn").on("click", function (e) {
        savetopng();
		alert("保存成功！");
    });
	

	$("#returnbtn").on("click", function (e) {
        returnurl();
    });
	
});
