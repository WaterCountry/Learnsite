// mBot.js

(function(ext) {
    var device = null;
    var _rxBuf = [];

    // Sensor states:
    var ports = {
        Port1: 1,
        Port2: 2,
        Port3: 3,
        Port4: 4,
        Port5: 5,
        Port6: 6,
        Port7: 7,
        Port8: 8,
		M1:9,
		M2:10,
		'led on board':7,
		'light sensor on board':6
    };
	var slots = {
		Slot1:1,
		Slot2:2
	};
	var switchStatus = {
		On:1,
		Off:0
	};
	var shutterStatus = {
		Press:0,
		Release:1,
		'Focus On':2,
		'Focus Off':3,
	};
	var button_keys = {
		"key1":1,
		"key2":2,
		"key3":3,
		"key4":4
	};
	var tones ={"B0":31,"C1":33,"D1":37,"E1":41,"F1":44,"G1":49,"A1":55,"B1":62,
			"C2":65,"D2":73,"E2":82,"F2":87,"G2":98,"A2":110,"B2":123,
			"C3":131,"D3":147,"E3":165,"F3":175,"G3":196,"A3":220,"B3":247,
			"C4":262,"D4":294,"E4":330,"F4":349,"G4":392,"A4":440,"B4":494,
			"C5":523,"D5":587,"E5":659,"F5":698,"G5":784,"A5":880,"B5":988,
			"C6":1047,"D6":1175,"E6":1319,"F6":1397,"G6":1568,"A6":1760,"B6":1976,
			"C7":2093,"D7":2349,"E7":2637,"F7":2794,"G7":3136,"A7":3520,"B7":3951,
	"C8":4186,"D8":4699};
	var beats = {"Half":500,"Quater":250,"Eighth":125,"Whole":1000,"Double":2000,"Zero":0};
	var ircodes = {	"A":69,
		"B":70,
		"C":71,
		"D":68,
		"E":67,
		"F":13,
		"↑":64,
		"↓":25,
		"←":7,
		"→":9,
		"Setting":21,
		"R0":22,
		"R1":12,
		"R2":24,
		"R3":94,
		"R4":8,
		"R5":28,
		"R6":90,
		"R7":66,
		"R8":82,
		"R9":74};
	var axis = {
		'X-Axis':1,
		'Y-Axis':2,
		'Z-Axis':3
	}
    var inputs = {
        slider: 0,
        light: 0,
        sound: 0,
        button: 0,
        'resistance-A': 0,
        'resistance-B': 0,
        'resistance-C': 0,
        'resistance-D': 0
    };
	var values = {};
	var indexs = [];
	var startTimer = 0;
	var versionIndex = 0xFA;
    ext.resetAll = function(){
    	device.send([0xff, 0x55, 2, 0, 4]);
    };
	ext.runArduino = function(){};
	
	ext.runBot = function(direction,speed) {
		var leftSpeed = 0;
		var rightSpeed = 0;
		if(direction=="run forward"){
			leftSpeed = -speed;
			rightSpeed = speed;
		}else if(direction=="run backward"){
			leftSpeed = speed;
			rightSpeed = -speed;
		}else if(direction=="turn left"){
			leftSpeed = speed;
			rightSpeed = speed;
		}else if(direction=="turn right"){
			leftSpeed = -speed;
			rightSpeed = -speed;
		}
        runPackage(5,short2array(leftSpeed),short2array(rightSpeed));
    };
    
	ext.runMotor = function(port,speed) {
		if(typeof port=="string"){
			port = ports[port];
		}
		if(port == 9){
			speed = -speed;
		}
        runPackage(10,port,short2array(speed));
    };
    var runServoDict = {};
    ext.runServo = function(port,slot,angle) {
		if(typeof port=="string"){
			port = ports[port];
		}
		if(typeof slot=="string"){
			slot = slots[slot];
		}
        runPackage(11,port,slot,angle);
    };
    
	ext.runBuzzer = function(tone, beat){
		if(typeof tone == "string"){
			tone = tones[tone];
		}
		if(typeof beat == "string"){
			beat = parseInt(beat) || beats[beat];
		}
		runPackage(34,short2array(tone), short2array(beat));
	};
	
	ext.stopBuzzer = function(){
		runPackage(34,short2array(0));
	};
	ext.runSevseg = function(port,display){
		if(typeof port=="string"){
			port = ports[port];
		}
		runPackage(9,port,float2array(display));
	};
	
	ext.runLed = function(port,ledIndex,red,green,blue){
		ext.runLedStrip(port, 2, ledIndex, red,green,blue);
	};
	ext.runLedStrip = function(port,slot,ledIndex,red,green,blue){
		if(typeof port=="string"){
			port = ports[port];
		}
		if("all" == ledIndex){
			ledIndex = 0;
		}
		if(typeof slot=="string"){
			slot = slots[slot];
		}
		runPackage(8,port,slot,ledIndex,red,green,blue);
	};
	ext.runLightSensor = function(port,status){
		if(typeof port=="string"){
			port = ports[port];
		}
		if(typeof status=="string"){
			status = switchStatus[status];
		}
		runPackage(3,port,status);
	};
	ext.runShutter = function(port,status){
		runPackage(20,shutterStatus[status]);
	};
	ext.runIR = function(message){
		runPackage(13,string2array(message));
	};
	ext.showCharacters = function(port,x,y,message){
		if(typeof port=="string"){
			port = ports[port];
		}
		var index = Math.max(0, Math.floor(x / -6));
		message = message.toString().substr(index, 4);
		if(index > 0){
			x += index * 6;
		}
		if(x >  16) x = 16;
		if(y >  8) y = 8;
		if(y < -8) y = -8;
		runPackage(41,port,1,x,7-y,message.length,string2array(message));
	}
	ext.showTime = function(port,hour,point,min){
		if(typeof port=="string"){
			port = ports[port];
		}
		runPackage(41,port,3,point==":"?1:0,hour,min);
	}
	ext.showDraw = function(port,x,y,bytes){
		if(typeof port=="string"){
			port = ports[port];
		}
		if(x >  16) x = 16;
		if(x < -16) x = -16;
		if(y >  8) y = 8;
		if(y < -8) y = -8;
		runPackage(41,port,2,x,y,bytes);
	}
	ext.resetTimer = function(){
		startTimer = (new Date().getTime())/1000.0;
	};
	/*
	ext.getLightOnBoard = function(nextID){
		var deviceId = 31;
		getPackage(nextID,deviceId,6);
	}
	*/
	ext.getButtonOnBoard = function(nextID,status){
		var deviceId = 35;
		if(typeof status == "string"){
			if(status=="pressed"){
				status = 0;
			}else if(status=="released"){
				status = 1;
			}
		}
		getPackage(nextID,deviceId,7,status);
	}
	ext.getUltrasonic = function(nextID,port){
		var deviceId = 1;
		if(typeof port=="string"){
			port = ports[port];
		}
		getPackage(nextID,deviceId,port);
	};
	ext.getPotentiometer = function(nextID,port) {
		var deviceId = 4;
		if(typeof port=="string"){
			port = ports[port];
		}
		getPackage(nextID,deviceId,port);
    };
    ext.getHumiture = function(nextID,port,valueType){
    	var deviceId = 23;
		if(typeof port=="string"){
			port = ports[port];
		}
		if(typeof valueType=="string"){
			valueType = ("humidity" == valueType) ? 0 : 1;
		}
		getPackage(nextID,deviceId,port,valueType);
    };
    ext.getFlame = function(nextID,port){
   		var deviceId = 24;
		if(typeof port=="string"){
			port = ports[port];
		}
		getPackage(nextID,deviceId,port);
    };
    ext.getGas = function(nextID,port){
    	var deviceId = 25;
		if(typeof port=="string"){
			port = ports[port];
		}
		getPackage(nextID,deviceId,port);
    };
    ext.gatCompass = function(nextID,port){
    	var deviceId = 26;
		if(typeof port=="string"){
			port = ports[port];
		}
		getPackage(nextID,deviceId,port);
    };
	ext.getLinefollower = function(nextID,port) {
		var deviceId = 17;
		if(typeof port=="string"){
			port = ports[port];
		}
		getPackage(nextID,deviceId,port);
    };
	ext.getLightSensor = function(nextID,port) {
		var deviceId = 3;
		if(typeof port=="string"){
			port = ports[port];
		}
		getPackage(0,deviceId,port);
    };
	ext.getJoystick = function(nextID,port,ax) {
		var deviceId = 5;
		if(typeof port=="string"){
			port = ports[port];
		}
		if(typeof ax=="string"){
			ax = axis[ax];
		}
		getPackage(nextID,deviceId,port,ax);
    };
	ext.getSoundSensor = function(nextID,port) {
		var deviceId = 7;
		if(typeof port=="string"){
			port = ports[port];
		}
		getPackage(nextID,deviceId,port);
    };
	ext.getInfrared = function(nextID,port) {
		var deviceId = 16;
		if(typeof port=="string"){
			port = ports[port];
		}
		getPackage(nextID,deviceId,port);
    };
	ext.getLimitswitch = function(nextID,port,slot) {
		var deviceId = 21;
		if(typeof port=="string"){
			port = ports[port];
		}
		if(typeof slot=="string"){
			slot = slots[slot];
		}
		getPackage(nextID,deviceId,port,slot);
    };
    ext.getTouchSensor = function(port){
    	var deviceId = 51;
    	if(typeof port=="string"){
			port = ports[port];
		}
		getPackage(0,deviceId,port);
    };
    ext.getButton = function(port, key){
    	var deviceId = 22;
    	if(typeof port=="string"){
			port = ports[port];
		}
		if(typeof key == "string"){
			key = button_keys[key];
		}
		getPackage(0,deviceId,port, key);
    };
	ext.getPirmotion = function(nextID,port) {
		var deviceId = 15;
		if(typeof port=="string"){
			port = ports[port];
		}
		getPackage(nextID,deviceId,port);
    };
	ext.getTemperature = function(nextID,port,slot) {
		var deviceId = 2;
		if(typeof port=="string"){
			port = ports[port];
		}
		if(typeof slot=="string"){
			slot = slots[slot];
		}
		getPackage(nextID,deviceId,port,slot);
    };
    ext.getGyro = function(nextID,ax) {
		var deviceId = 6;
		if(typeof ax=="string"){
			ax = axis[ax];
		}
		getPackage(nextID,deviceId,0,ax);
    };
	ext.getIrRemote = function(nextID,code){
		var deviceId = 14;
		if(typeof code=="string"){
			code = ircodes[code];
		}
		getPackage(nextID,deviceId,0,code);
	}
	ext.getIR = function(nextID){
		var deviceId = 13;
		getPackage(nextID,deviceId);
	}
	ext.getTimer = function(nextID){
		if(startTimer==0){
			startTimer = (new Date().getTime())/1000.0;
		}
		responseValue(nextID,(new Date().getTime())/1000.0-startTimer);
	}
	function runPackage(){
		var bytes = [0xff, 0x55, 0, 0, 2];
		for(var i=0;i<arguments.length;i++){
			if(arguments[i].constructor == "[class Array]"){
				bytes = bytes.concat(arguments[i]);
			}else{
				bytes.push(arguments[i]);
			}
		}
		bytes[2] = bytes.length-3;
		device.send(bytes);
	}
	
	function getPackage(){
		var nextID = arguments[0];
		
		var bytes = [0xff, 0x55];
		bytes.push(arguments.length+1);
		bytes.push(0);
		bytes.push(1);
		for(var i=1;i<arguments.length;i++){
			bytes.push(arguments[i]);
		}
		device.send(bytes);
	}

    var inputArray = [];
	var _isParseStart = false;
	var _isParseStartIndex = 0;
    function processData(bytes) {
		var len = bytes.length;
		if(_rxBuf.length>30){
			_rxBuf = [];
		}
		for(var index=0;index<bytes.length;index++){
			var c = bytes[index];
			_rxBuf.push(c);
			if(_rxBuf.length>=2){
				if(_rxBuf[_rxBuf.length-1]==0x55 && _rxBuf[_rxBuf.length-2]==0xff){
					_isParseStart = true;
					_isParseStartIndex = _rxBuf.length-2;
				}
				if(_rxBuf[_rxBuf.length-1]==0xa && _rxBuf[_rxBuf.length-2]==0xd&&_isParseStart){
					_isParseStart = false;
					
					var position = _isParseStartIndex+2;
					var extId = _rxBuf[position];
					position++;
					var type = _rxBuf[position];
					position++;
					//1 byte 2 float 3 short 4 len+string 5 double
					var value;
					switch(type){
						case 1:{
							value = _rxBuf[position];
							position++;
						}
							break;
						case 2:{
							value = readFloat(_rxBuf,position);
							position+=4;
						}
							break;
						case 3:{
							value = readShort(_rxBuf,position);
							position+=2;
						}
							break;
						case 4:{
							var l = _rxBuf[position];
							position++;
							value = readString(_rxBuf,position,l);
						}
							break;
						case 5:{
							value = readDouble(_rxBuf,position);
							position+=4;
						}
							break;
					}
					if(type<=5){
						if(values[extId]!=undefined){
							responseValue(extId,values[extId](value,extId));
						}else{
							responseValue(extId,value);
						}
						values[extId] = null;
					}
					_rxBuf = [];
				}
			} 
		}
    }
	function readFloat(arr,position){
		var f= [arr[position],arr[position+1],arr[position+2],arr[position+3]];
		return parseFloat(f);
	}
	function readShort(arr,position){
		var s= [arr[position],arr[position+1]];
		return parseShort(s);
	}
	function readDouble(arr,position){
		return readFloat(arr,position);
	}
	function readString(arr,position,len){
		var value = "";
		for(var ii=0;ii<len;ii++){
			value += String.fromCharCode(_rxBuf[ii+position]);
		}
		return value;
	}
    function appendBuffer( buffer1, buffer2 ) {
        return buffer1.concat( buffer2 );
    }

    // Extension API interactions
    var potentialDevices = [];
    ext._deviceConnected = function(dev) {
        potentialDevices.push(dev);

        if (!device) {
            tryNextDevice();
        }
    }

    function tryNextDevice() {
        // If potentialDevices is empty, device will be undefined.
        // That will get us back here next time a device is connected.
        device = potentialDevices.shift();
        if (device) {
            device.open({ stopBits: 0, bitRate: 57600, ctsFlowControl: 0 }, deviceOpened);
        }
    }

    var watchdog = null;
    function deviceOpened(dev) {
        if (!dev) {
            // Opening the port failed.
            tryNextDevice();
            return;
        }
        device.set_receive_handler('mbot',processData);
    };

    ext._deviceRemoved = function(dev) {
        if(device != dev) return;
        device = null;
    };

    ext._shutdown = function() {
        if(device) device.close();
        device = null;
    };

    ext._getStatus = function() {
        if(!device) return {status: 1, msg: 'mBot disconnected'};
        if(watchdog) return {status: 1, msg: 'Probing for mBot'};
        return {status: 2, msg: 'mBot connected'};
    }

      // Check for GET param 'lang'
  var paramString = window.location.search.replace(/^\?|\/$/g, '');
  var vars = paramString.split("&");
  var lang = 'en';
  for (var i=0; i<vars.length; i++) {
    var pair = vars[i].split('=');
    if (pair.length > 1 && pair[0]=='lang')
      lang = pair[1];
  }

  var blocks = {
    en: [
		["h","mBot Program","runArduino",{"src":"mbot","inc":"#include <MeMCore.h>\n","def":"","setup":"","loop":""}],
		
		["-"],
	
		["w", "%m.direction at speed %d.motorvalue","runBot", "run forward", 0,
		{"encode":"{d0}{s1}","setup":"","inc":"","def":"MBotDCMotor motor(0);\n","work":"motor.move({0},{1});\n","loop":""}],
		
		["w", "set motor%d.motorPort speed %d.motorvalue","runMotor", "M1", 0,
		{"encode":"{d0}{s1}","setup":"","inc":"","def":"MeDCMotor motor_{0}({0});\n","work":"motor_{0}.run(({0})==M1?-({1}):({1}));\n","loop":""}],
		
		["w", "set servo %d.servoPort %d.slot angle %d.servovalue","runServo", "Port1","Slot1", 90,
		{"encode":"{d0}{d1}{d2}","setup":"servo_{0}_{1}.attach(port_{0}.pin{1}());\n","inc":"","def":"Servo servo_{0}_{1};\nMePort port_{0}({0});\n","work":"servo_{0}_{1}.write({2});\n","loop":""}],
		
		["w", "set led %d.lport %d.index red%d.value green%d.value blue%d.value","runLed", "led on board","all",0,0,0,
		{"encode":"{d0}{d1}{d2}{d3}{d4}","setup":"","inc":"","def":"MeRGBLed rgbled_{0}({0}, {0}==7?2:4);\n","work":"rgbled_{0}.setColor({1},{2},{3},{4});\nrgbled_{0}.show();\n","loop":""}],
		
		["w", "set led strip %d.normalPort %d.slot %d.index2 red%d.value green%d.value blue%d.value","runLedStrip", "Port1","Slot2","all",0,0,0,
		{"encode":"{d0}{d1}{d2}{d3}{d4}","setup":"","inc":"","def":"MeRGBLed rgbled_{0}_{1}({0}, {1}, 32);\n","work":"rgbled_{0}_{1}.setColor({2},{3},{4},{5});\nrgbled_{0}_{1}.show();\n","loop":""}],
		
		["w", "play tone on note %d.note beat %d.beats","runBuzzer", "C4", "Half",
		{"encode":"{s0}","setup":"","inc":"","def":"MeBuzzer buzzer;\n","work":"buzzer.tone({0}, {1});\n","loop":""}],
		
		["w", "stop tone","stopBuzzer",0,
		{"encode":"{s0}","setup":"","inc":"","def":"MeBuzzer buzzer;\n","work":"buzzer.noTone();\n","loop":""}],
	
		["w", "show face %d.normalPort x:%n y:%n characters:%s","showCharacters", "Port1", 0,0,"Hi",
		{"encode":"{s0}","setup":"ledMtx_{0}.setColorIndex(1);\nledMtx_{0}.setBrightness(6);\n","inc":"","def":"MeLEDMatrix ledMtx_{0}({0});\n","work":"ledMtx_{0}.drawStr({1},7-{2},{3});\n","loop":""}],
		
		["w", "show time %d.normalPort hour:%n %m.points min:%n","showTime", "Port1", 10,":",20,
		{"encode":"{s0}","setup":"ledMtx_{0}.setColorIndex(1);\nledMtx_{0}.setBrightness(6);\n","inc":"","def":"MeLEDMatrix ledMtx_{0}({0});\n","work":"ledMtx_{0}.showClock({1},{3},strcmp({2},\":\")==0);\n","loop":""}],
		
		["w", "show drawing %d.normalPort x:%n y:%n draw:%m.drawFace","showDraw", "Port1", 0,0,"        ",
		{"encode":"{s0}","setup":"ledMtx_{0}.setColorIndex(1);\nledMtx_{0}.setBrightness(6);\n","inc":"","def":"MeLEDMatrix ledMtx_{0}({0});\nunsigned char drawBuffer[16];\nunsigned char *drawTemp;\n","work":"drawTemp = new unsigned char[16]{{3}};\nmemcpy(drawBuffer,drawTemp,16);\nfree(drawTemp);\nledMtx_{0}.drawBitmap({1},{2},16,drawBuffer);\n","loop":""}],
		
		["-"],
		
		["w", "set 7-segments display%d.bluePorts number %n","runSevseg", "Port1", 100,
		{"encode":"{d0}{f1}","setup":"","inc":"","def":"Me7SegmentDisplay seg7_{0}({0});\n","work":"seg7_{0}.display((double){1});\n","loop":""}],
		
		
		
		["w", "set light sensor %d.blackPorts led as %d.switch","runLightSensor", "Port3", "On",
		{"encode":"{d0}{d1}","setup":"","inc":"","def":"MePort lightsensor_{0}({0});\n","work":"lightsensor_{0}.dWrite1({1});\n","loop":""}],
		
		["w", "set camera shutter %d.normalPort as %d.shutter","runShutter","Port1", "Press",
		{"encode":"{d0}{d1}","setup":"","inc":"","def":"MePort shutter_{0}({0});\n","work":"shutter_{0}.dWrite1({1});\n","loop":""}],
		
		["-"],
		["R", "light sensor %d.laport","getLightSensor","light sensor on board",
		{"encode":"{d0}","setup":"","inc":"","def":"MePort lightsensor_{0}({0});\n","work":"lightsensor_{0}.aRead2()","loop":""}],
		
		["h", "when button %m.button_state","whenButtonPressed","pressed"],

		["b", "button %m.button_state","getButtonOnBoard","pressed",
		{"encode":"{n7}{d0}","setup":"pinMode(A7,INPUT);\n","inc":"","def":"","work":"({0}^(analogRead(A7)>10?0:1))","loop":""}],
		
		["-"],
		
		["R", "ultrasonic sensor %d.whitePorts distance","getUltrasonic","Port3",
		{"encode":"{d0}","setup":"","inc":"","def":"MeUltrasonicSensor ultrasonic_{0}({0});\n","work":"ultrasonic_{0}.distanceCm()","loop":""}],
		
		["R", "line follower %d.normalPort","getLinefollower","Port2",
		{"encode":"{d0}","setup":"","inc":"","def":"MePort linefollower_{0}({0});\n","work":"linefollower_{0}.dRead1()*2+linefollower_{0}.dRead2()","loop":""}],
		
		["R", "joystick %d.blackPorts %d.Axis","getJoystick","Port3","X-Axis",
		{"encode":"{d0}{d1}","setup":"","inc":"","def":"MeJoystick joystick_{0}({0});\n","work":"joystick_{0}.read({1})","loop":""}],
		
		["R", "potentiometer %d.blackPorts","getPotentiometer","Port3",
		{"encode":"{d0}","setup":"","inc":"","def":"MePort potentiometer_{0}({0});\n","work":"potentiometer_{0}.aRead2()","loop":""}],
		["R", "sound sensor %d.blackPorts","getSoundSensor","Port3",
		{"encode":"{d0}","setup":"","inc":"","def":"MePort soundsensor_{0}({0});\n","work":"soundsensor_{0}.aRead2()","loop":""}],
		["b", "limit switch %d.normalPort %d.slot","getLimitswitch","Port1","Slot1",
		{"encode":"{d0}{d1}","setup":"","inc":"","def":"MePort sw_{0}_{1}({0});\n","work":"sw_{0}_{1}.dpRead{1}()","loop":""}],
		
		["R", "temperature %d.normalPort%d.slot °C","getTemperature","Port3","Slot1",
		{"encode":"{d0}{d1}","setup":"","inc":"","def":"MeTemperature temperature_{0}_{1}({0},{1});\n","work":"temperature_{0}_{1}.temperature()","loop":""}],
		
		["R", "pir motion sensor %d.bluePorts","getPirmotion","Port2",
		{"encode":"{d0}","setup":"","inc":"","def":"MePort pir_{0}({0});\n","work":"pir_{0}.dRead2()","loop":""}],
		
		["R", "3-axis gyro %d.GyroAxis angle","getGyro","X-Axis",
		{"encode":"{d0}","setup":"gyro.begin();\n","inc":"","def":"MeGyro gyro;\n","work":"gyro.getAngle({0})","loop":"gyro.update();\n"}],
		
		["R", "humiture sensor %d.normalPort %d.humiture", "getHumiture", "Port1", "humidity", {"encode":"", "setup":"", "inc":"", "def":"MeHumiture humiture_{0}({0});\n", "work":"humiture_{0}.getValue({1})", "loop":"humiture_{0}.update();\n"}],
		["R", "flame sensor %d.blackPorts", "getFlame", "Port3", {"encode":"", "setup":"", "inc":"", "def":"MeFlameSensor flameSensor_{0}({0});\n", "work":"flameSensor_{0}.readAnalog()", "loop":""}],
		["R", "gas sensor %d.blackPorts", "getGas", "Port3", {"encode":"", "setup":"", "inc":"", "def":"MeGasSensor gasSensor_{0}({0});\n", "work":"gasSensor_{0}.readAnalog()", "loop":""}],
		["R", "compass sensor %d.normalPort", "gatCompass", "Port1",{"encode":"", "setup":"compass_{0}.begin();\n", "inc":"", "def":"MeCompass compass_{0}({0});\n", "work":"compass_{0}.getAngle()", "loop":""}],
		["b", "touch sensor %d.normalPort","getTouchSensor","Port1",
		{"encode":"{d0}","setup":"","inc":"","def":"MeTouchSensor touchSensor_{0}({0});\n","work":"touchSensor_{0}.touched()","loop":""}],
		["b", "button %d.blackPorts %m.button_key pressed","getButton","Port3","key1",
		{"encode":"{d0}","setup":"","inc":"","def":"Me4Button buttonSensor_{0}({0});\n","work":"(buttonSensor_{0}.pressed()=={1})","loop":""}],
		["-"],
		
		["b","ir remote %m.ircode pressed","getIrRemote","A",
		{"encode":"{n0}{d0}","setup":"ir.begin();\n","inc":"","def":"MeIR ir;\n","work":"ir.keyPressed({0})","loop":"ir.loop();\n"}],
		
		["-"],
		
		["w", "send mBot's message %s","runIR", "hello",
		{"encode":"{m0}","setup":"ir.begin();\n","inc":"","def":"MeIR ir;\n","work":"ir.sendString({0});\n","loop":""}],
		
		["R", "mBot's message received","getIR",
		{"encode":"{n0}","setup":"ir.begin();\n","inc":"","def":"MeIR ir;\n","work":"ir.getString()","loop":""}],
		
		["-"],
		
		["R", "timer","getTimer", "0",
		{"encode":"{n0}","setup":"","inc":"","def":"double currentTime = 0;\ndouble lastTime = 0;\ndouble getLastTime(){\n\treturn currentTime = millis()/1000.0 - lastTime;\n}\n","work":"getLastTime()","loop":""}],
		
		["w", "reset timer","resetTimer", "0",
		{"encode":"{n0}","setup":"","inc":"","def":"double currentTime = 0;\ndouble lastTime = 0;\n","work":"lastTime = millis()/1000.0;\n","loop":""}]
    ]
  };

  var menus = {
    en: {
		"yellowPorts":["Port1","Port2","Port3","Port4"],
    	"bluePorts":["Port1","Port2","Port3","Port4"],
    	"grayPorts":[],
    	"whitePorts":["Port1","Port2","Port3","Port4"],
    	"blackPorts":["Port3","Port4"],
		"motorPort":["M1","M2"],
		"normalPort":["Port1","Port2","Port3","Port4"],
		"servoPort":["Port1","Port2","Port3","Port4"],
		"slot":["Slot1","Slot2"],
		"index":["all",1,2,3,4],
		"index2":["all",1,15,30],
		"Axis":["X-Axis","Y-Axis"],
		"GyroAxis":["X-Axis","Y-Axis","Z-Axis"],
		"port":["Port1","Port2","Port3","Port4"],
		"lport":["led on board","Port1","Port2","Port3","Port4"],
		"laport":["light sensor on board","Port3","Port4"],
		"direction":["run forward","run backward","turn right","turn left"],
		"points":[":"," "],
		"note":["C2","D2","E2","F2","G2","A2","B2","C3","D3","E3","F3","G3","A3","B3","C4","D4","E4","F4","G4","A4","B4","C5","D5","E5","F5","G5","A5","B5","C6","D6","E6","F6","G6","A6","B6","C7","D7","E7","F7","G7","A7","B7","C8","D8"],
		"beats":["Half","Quater","Eighth","Whole","Double","Zero"],
		"servovalue":[0,45,90,135,180],
		"motorvalue":[255,100,50,0,-50,-100,-255],
		"value":[0,20,60,150,255],
		"button_state":["pressed","released"],
		"shutter":["Press","Release","Focus On","Focus Off"],
		"switch":["Off","On"],
		"ircode":["A","B","C","D","E","F","↑","↓","←","→","Setting","R0","R1","R2","R3","R4","R5","R6","R7","R8","R9"],
		"touch_mode":["direct","toggle"],
		"button_key":["key1","key2","key3","key4"],
		"humiture":["humidity","temperature"]
    }
  };
  var values = {
    en: {
		"Half":500,"Quater":250,"Eighth":125,"Whole":1000,"Double":2000,"Zero":0,
		"servovalue":[0,45,90,135,180],
		"all":0,
		"run":2,
		"stop":2,
		"get":1,
		"motor":10,
		"ir":13,
		"irremote":14,
		"irremotecode":18,
		"lightsensor":3,
		"linefollower":17,
		"timer":50,
		"joystick":5,
		"potentiometer":4,
		"soundsensor":7,
		"infrared":16,
		"limitswitch":21,
		"pirmotion":15,
		"temperature":2,
		"digital":30,
		"analog":31,
		"button":22,
		"buzzer":34,
		"button_inner":35,
		"pressed":0,
		"released":1,
		"led":8,
		"ultrasonic":1,
		"Slot1":1,
		"Slot2":2,
		"Port1":1,
		"Port2":2,
		"Port3":3,
		"Port4":4,
		"Port5":5,
		"Port6":6,
		"Port7":7,
		"Port8":8,
		"On":1,
		"Off":0,
		"LOW":0,
		"HIGH":1,
		"Press":0,
		"Release":1,
		"Focus On":2,
		"Focus Off":3,
		"led on board":7,
		"light sensor on board":6,
		"M1":9,
		"M2":10,
		"X-Axis":1,
		"Y-Axis":2,
		"Z-Axis":3,
		"run forward":1,
		"run backward":2,
		"turn left":3,
		"turn right":4,
		"B0":31,"C1":33,"D1":37,"E1":41,"F1":44,"G1":49,"A1":55,"B1":62,
			"C2":65,"D2":73,"E2":82,"F2":87,"G2":98,"A2":110,"B2":123,
			"C3":131,"D3":147,"E3":165,"F3":175,"G3":196,"A3":220,"B3":247,
			"C4":262,"D4":294,"E4":330,"F4":349,"G4":392,"A4":440,"B4":494,
			"C5":523,"D5":587,"E5":659,"F5":698,"G5":784,"A5":880,"B5":988,
			"C6":1047,"D6":1175,"E6":1319,"F6":1397,"G6":1568,"A6":1760,"B6":1976,
			"C7":2093,"D7":2349,"E7":2637,"F7":2794,"G7":3136,"A7":3520,"B7":3951,
			"C8":4186,"D8":4699,
		"A":69,
		"B":70,
		"C":71,
		"D":68,
		"E":67,
		"F":13,
		"↑":64,
		"↓":25,
		"←":7,
		"→":9,
		"R0":22,
		"R1":12,
		"R2":24,
		"R3":94,
		"R4":8,
		"R5":28,
		"R6":90,
		"R7":66,
		"R8":82,
		"R9":74,
		"humidity":0,
		"temperature":1,
		"Setting":21,
		"direct":0,
		"toggle":1,
		"key1":1,
		"key2":2,
		"key3":3,
		"key4":4
    }
  };
    var descriptor = {
    blocks: blocks[lang],
    menus: menus[lang],
    values: values[lang],
    url: 'http://bbs.makeblock.cc/'};
	ScratchExtensions.register('Mbot', descriptor, ext, {type: 'serial'});
})({});
