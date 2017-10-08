// EV3 ScratchX Plugin
// Copyright 2015 Ken Aspeslagh @massivevector
// Only tested on Mac. On Mac, ev3 brick must be named starting with "serial" if the plugin is to recognize it.
// Rename the brick before pairing it with the Mac or else the name gets cached and the serial port will have the old name
// My bricks are named serialBrick1 (etc)
// Turn off the iPod/iPhone/iPad checkbox on the EV3 Bluetooth settings after pairing or else it will not work at all

function timeStamp()
{
    return (new Date).toISOString().replace(/z|t/gi,' ').trim();
}

function console_log(str)
{
    console.log(timeStamp() + ": "  + str);
}

// scratchX is loading our javascript file again each time a saved SBX file is opened.
// JavaScript is weird and this causes our object to be reloaded and re-registered.
// Prevent this using global variable theEV3Device and EV3Connected that will only initialize to null the first time they are declared.
// This fixes a Windows bug where it would not reconnect.

var waitingCallbacks = waitingCallbacks || [[],[],[],[],[],[],[],[], []];
var waitingQueries = waitingQueries || [];
var global_sensor_result = global_sensor_result || [0, 0, 0, 0, 0, 0, 0, 0, 0];
var thePendingQuery = thePendingQuery || null;


var warnedAboutBattery = warnedAboutBattery || false;
var deviceTimeout = deviceTimeout || 0;
var counter = counter || 0;
var poller = poller || null;
var pingTimeout = pingTimeout || null;

var waitingForPing = waitingForPing || false;

var DIRECT_COMMAND_PREFIX = "800000";
var DIRECT_COMMAND_REPLY_PREFIX = "000100";
var DIRECT_COMMAND_REPLY_SENSOR_PREFIX = "000400";

// direct command opcode/prefixes
var SET_MOTOR_SPEED = "A400";
var SET_MOTOR_STOP = "A300";
var SET_MOTOR_START = "A600";
var SET_MOTOR_STEP_SPEED = "AC00";
var NOOP = "0201";
var PLAYTONE = "9401";
var INPUT_DEVICE_READY_SI = "991D";
var READ_SENSOR = "9A00";
var UIREAD  = "81"; // opUI_READ
var UIREAD_BATTERY = "12"; // GET_LBATT

var UIDRAW = "84";
var UIWRITE = "82";
var UIDRAW_FILLWINDOW = "13";
var UIDRAW_PICTURE = "07";
var UIDRAW_BMPFILE = "1C";
var UIDRAW_UPDATE = "00";
var UIWRITE_INIT_RUN = "19";
var BEGIN_DOWNLOAD = "0192";
var CONTINUE_DOWNLOAD = "8193"

var SYSTEM_REPLY_ERROR = 5;

var mode0 = "00";
var TOUCH_SENSOR = "10";
var COLOR_SENSOR = "1D";
var ULTRASONIC_SENSOR = "1E";
var ULTRSONIC_CM = "00";
var ULTRSONIC_INCH = "01";
var ULTRSONIC_LISTEN = "02";
var ULTRSONIC_SI_CM = "03";
var ULTRSONIC_SI_INCH = "04";
var ULTRSONIC_DC_CM = "05";
var ULTRSONIC_DC_INCH = "06";

var READ_MOTOR_POSITION = "01";
var READ_MOTOR_SPEED = "02";

var GYRO_SENSOR = "20";
var GYRO_ANGLE = "00";
var GYRO_RATE = "01";
var GYRO_FAST = "02";
var GYRO_RATE_AND_ANGLE = "03";
var GYRO_CALIBRATION = "04";
var IR_SENSOR = "21";
var IR_PROX = "00";
var IR_SEEKER = "01";
var IR_REMOTE = "02"
var IR_REMOTE_ADVANCE = "03";
var IR_CALIBRATION = "05";
var REFLECTED_INTENSITY = "00";
var AMBIENT_INTENSITY = "01";
var COLOR_VALUE = "02";
var COLOR_RAW_RGB = "04";
var READ_FROM_MOTOR = "FOOBAR";

var DRIVE_QUERY = "DRIVE_QUERY";
var DRIVE_QUERY_DURATION = "DRIVE_QUERY_DURATION";
var TONE_QUERY = "TONE_QUERY";
var UIDRAW_QUERY = "UIDRAW_QUERY";
var SYSTEM_COMMAND = "SYSTEM_COMMAND";

var frequencies = { "C4" : 262, "D4" : 294, "E4" : 330, "F4" : 349, "G4" : 392, "A4" : 440, "B4" : 494, "C5" : 523, "D5" : 587, "E5" : 659, "F5" : 698, "G5" : 784, "A5" : 880, "B5" : 988, "C6" : 1047, "D6" : 1175, "E6" : 1319, "F6" : 1397, "G6" : 1568, "A6" : 1760, "B6" : 1976, "C#4" : 277, "D#4" : 311, "F#4" : 370, "G#4" : 415, "A#4" : 466, "C#5" : 554, "D#5" : 622, "F#5" : 740, "G#5" : 831, "A#5" : 932, "C#6" : 1109, "D#6" : 1245, "F#6" : 1480, "G#6" : 1661, "A#6" : 1865 };

var colors = [ "none", "black", "blue", "green", "yellow", "red", "white"];

var IRbuttonNames = ['Top Left', 'Bottom Left', 'Top Right', 'Bottom Right', 'Top Bar'];
var IRbuttonCodes = [1,            2,              3,          4,              9];



function clearSensorStatuses()
{
    var numSensorBlocks = 9;
    for (x = 0; x < numSensorBlocks; x++)
    {
        waitingCallbacks[x] = [];
        global_sensor_result[x] = 0;
    }
}

var lastCommandWeWereTrying = null;

function startupBatteryCheckCallback(result)
{
    (timeStamp() + ": got battery level at connect: " + result);
    
    weConnected();
    
    playStartUpTones();
    
    if (result < 11 && !warnedAboutBattery)
    {
        alert("Your battery is getting low.");
        warnedAboutBattery = true;
    }
    
    clearScreen();
    
    uploadAndDrawCatFile();
    
    setupWatchdog();
    
    if (lastCommandWeWereTrying)
    {
        waitingQueries.push(lastCommandWeWereTrying);
        executeQueryQueue();
    }
}

function setupWatchdog()
{
    if (poller)
        clearInterval(poller);
    
    poller = setInterval(pingBatteryWatchdog, 10000);
}

function pingBatteryWatchdog()
{
    console_log("pingBatteryWatchdog");
    testTheConnection(pingBatteryCheckCallback);
    waitingForPing = true;
    pingTimeout = setTimeout(pingTimeOutCallback, 3000);
}

function pingTimeOutCallback()
{
    if (waitingForPing == true)
    {
        console_log("Ping timed out");
        if (poller)
            clearInterval(poller);
        
        disconnected();
    }
}

function pingBatteryCheckCallback(result)
{
    console_log("pinged battery level: " + result);
    if (pingTimeout)
        clearTimeout(pingTimeout);
    waitingForPing = false;
    
    if (result < 11 && !warnedAboutBattery)
    {
        alert("Your battery is getting low.");
        warnedAboutBattery = true;
    }
}

function testTheConnection(theCallback)
{
    readThatBatteryLevel(theCallback);
}

function playStartUpTones()
{
    var tonedelay = 1000;
    window.setTimeout(function()
                      {
                        playFreqM2M(262, 100);
                      }, tonedelay);
    
    window.setTimeout(function()
                      {
                        playFreqM2M(392, 100);
                      }, tonedelay+150);
    
    window.setTimeout(function()
                      {
                        playFreqM2M(523, 100);
                      }, tonedelay+300);
}


//// conversion helper routines

// create hex string from bytes
function createHexString(arr)
{
    var result = "";
    for (i in arr)
    {
        var str = arr[i].toString(16);
        str = str.toUpperCase();
        str = str.length == 0 ? "00" :
        str.length == 1 ? "0" + str :
        str.length == 2 ? str :
        str.substring(str.length-2, str.length);
        result += str;
    }
    return result;
}

function stringToHexString(instring)
{
    var outString = "";
    instring.split('').map(function (c) { var hex = c.charCodeAt(0).toString(16); if (hex.length == 2) { outString += hex; } else { outString += '0' + hex;} });
    outString += "00";
    return outString;
}

function decimalToLittleEndianHex(d, padding)
{
    var hex = Number(d).toString(16);
    padding = typeof (padding) === "undefined" || padding === null ? padding = 2 : padding;
    
    while (hex.length < padding)
    {
        hex = "0" + hex;
    }
    
    var a = hex.match(/../g);             // split number in groups of two
    a.reverse();                        // reverse the groups
    hex = a.join("");                // join the groups back together
    
    return hex;
}

function getFloatResult(inputData)
{
    var a = new ArrayBuffer(4);
    var c = new Float32Array(a);
    var arr = new Uint8Array(a);
    arr[0] = inputData[5];
    arr[1] = inputData[6];
    arr[2] = inputData[7]
    arr[3] = inputData[8]
    return c[0];
}

function getIRButtonNameForCode(inButtonCode)
{
    for (var i = 0; i < IRbuttonCodes.length; i++)
    {
        if (inButtonCode == IRbuttonCodes[i])
        {
            return IRbuttonNames[i];
        }
    }
    return "";
}

function createMessage(str)
{
    return str; // yeah
}

// add counter and byte length encoding prefix. return Uint8Array of final message
function packMessageForSending(str)
{
    var length = ((str.length / 2) + 2);
    
    var a = new ArrayBuffer(4);
    var c = new Uint16Array(a);
    var arr = new Uint8Array(a);
    c[1] = counter;
    c[0] = length;
    counter++;
    var mess = new Uint8Array((str.length / 2) + 4);
    
    for (var i = 0; i < 4; i ++)
    {
        mess[i] = arr[i];
    }
    
    for (var i = 0; i < str.length; i += 2)
    {
        mess[(i / 2) + 4] = window.parseInt(str.substr(i, 2), 16);
    }
    
    return mess;
}

// motor port bit field from menu choice string
function getMotorBitsHexString(which)
{
    if (which == "A")
        return "01";
    else if (which == "B")
        return "02";
    else if (which == "C")
        return "04";
    else if (which == "D")
        return "08";
    else if (which == "B+C")
        return "06";
    else if (which == "A+D")
        return "09";
    else if (which == "all")
        return "0F";
    
    return "00";
}

function getMotorIndex(which)
{
    if (which == "A")
        return 4;
    else if (which == "B")
        return 5;
    else if (which == "C")
        return 6;
    else if (which == "D")
        return 7;
}

// create 8 bit hex couplet
function hexcouplet(num)
{
    var str = num.toString(16);
    str = str.toUpperCase();
    if (str.length == 1)
    {
        return "0" + str;
    }
    return str;
}

// int bytes using weird serialization method
function getPackedOutputHexString(num, lc)
{
    // nonsensical unsigned byte packing. see cOutputPackParam in c_output-c in EV3 firmware
    var a = new ArrayBuffer(4);
    var sarr = new Int32Array(a);
    var uarr = new Uint8Array(a);
    
    sarr[0] = num;
    
    if (lc == 0)
    {
        var bits = uarr[0];
        bits &= 0x0000003F;
        return hexcouplet(bits);
    }
    else if (lc == 1)
    {
        return "81" + hexcouplet(uarr[0]);
    }
    else if (lc == 2)
    {
        return "82" + hexcouplet(uarr[0]) + hexcouplet(uarr[1]);
    }
    else if (lc == 3)
    {
        return "83" + hexcouplet(uarr[0]) + hexcouplet(uarr[1]) + hexcouplet(uarr[2]) + hexcouplet(uarr[3]);
    }
    
    return "00";
}

//// command/query queue

function executeQueryQueueAgain()
{
    window.setTimeout(
          function()
          {
            executeQueryQueue();
          } , 1);
}

function executeQueryQueue()
{
    if (waitingQueries.length == 0)
        return; // nothing to do
    
    if (!checkConnected())
        return;
        
    var query_info = waitingQueries[0]; // peek at first in line
    var thisCommand = null;
    
    if (query_info.length == 5) // a query with a response
    {
        var port = query_info[0];
        var type = query_info[1];
        var mode = query_info[2];
        var callback = query_info[3];
        var theCommand = query_info[4];
        
        if (thePendingQuery)
        {
            // we are waiting for a result
            if (thePendingQuery[0] == port)
            {
                // special case: we are actually already in the process of querying this same sensor (should we also compare the type and mode, or maybe just match the command string?)
                // so we don't want to bother calling it again
                waitingQueries.shift(); // remove it from the queue
                if (callback)
                    waitingCallbacks[port].push(callback);
                return;
            }
            // do nothing. we'll try again after the query finishes
            return;
        }
        waitingQueries.shift(); // remove it from the queue
        thePendingQuery = query_info;
        // actually go ahead and make the query
        var packedCommand = packMessageForSending(theCommand);
        sendCommand(packedCommand);
    }
    else if (query_info.length == 4) // a query with no response
    {
        if (thePendingQuery)    // bail if we're waiting for a response
            return;
        
        var type = query_info[0];
        var duration = query_info[1];
        var callback = query_info[2];
        var theCommand = query_info[3];
        
        if (type == DRIVE_QUERY || type == DRIVE_QUERY_DURATION)
        {
            clearDriveTimer();
            if (type == DRIVE_QUERY_DURATION)
            {
                driveCallback = callback;   // save this callback in case timer is cancelled we can call it directly
                driveTimer = window.setTimeout(
                       function()
                       {
                           if (duration > 0) // allow zero duration to run motors asynchronously
                           {
                               motorsStop('coast'); // xxx
                           }
                           if (callback)
                               callback();
                       } , duration*1000);
            }
        }
        else if (type == TONE_QUERY)
        {
            window.setTimeout(
                function()
                {
                  if (callback)
                    callback();
                } , duration); // duration already in ms
        }
        waitingQueries.shift(); // remove it from the queue
        
        // actually go ahead and make the query
        var packedCommand = packMessageForSending(theCommand);
        sendCommand(packedCommand);
        
        executeQueryQueueAgain();   // maybe do the next one
    }
}

function addToQueryQueue(query_info)
{
    for (var i = 0; i < waitingQueries.length; i++)
    {
        var next_query = waitingQueries[i];
        if (next_query.length == 5) // a query with a response
        {
            var port = next_query[0];
            var type = next_query[1];
            var mode = next_query[2];
            var callback = next_query[3];
            var theCommand = next_query[4];
            var this_port = query_info[0];
            if (port == this_port)
            {
                var this_callback = query_info[3]
                if (this_callback)
                    waitingCallbacks[this_port].push(this_callback);
                console_log("coalescing query because there's already one in the queue.");
                return;
            }
        }
    }
    waitingQueries.push(query_info);
    executeQueryQueue();
}

function receive_handler(data)
{
    var inputData = new Uint8Array(data);
    console_log("received: " + createHexString(inputData));
    
    if (!(connectingOrConnected()))
    {
        console_log("Received Data but not connected or connecting");
        return;
    }
    
    if (!thePendingQuery)
    {
        console_log("Received Data and didn't expect it...");
        return;
    }
    
    var theResult = null;
    
    var port = thePendingQuery[0];
    var type = thePendingQuery[1];
    var mode = thePendingQuery[2];
    var callback = thePendingQuery[3];
    var theCommand = thePendingQuery[4];
    
    if (type == TOUCH_SENSOR)
    {
        var result = inputData[5];
        theResult = (result == 100);
    }
    else if (type == COLOR_SENSOR)
    {
        var num = Math.floor(getFloatResult(inputData));
        if (mode == AMBIENT_INTENSITY || mode == REFLECTED_INTENSITY)
        {
            theResult = num;
        }
        else if (mode == COLOR_VALUE)
        {
            if (num >= 0 && num < 7)
                theResult = colors[num];
            else
                theResult = "none";
        }
    }
    else if (type == IR_SENSOR)
    {
        if (mode == IR_PROX)
            theResult = getFloatResult(inputData);
        else if (mode == IR_REMOTE)
            theResult = getIRButtonNameForCode(getFloatResult(inputData));
    }
    else if (type == GYRO_SENSOR)
    {
        theResult = getFloatResult(inputData);
    }
    else if (type == READ_FROM_MOTOR)
    {
        if (mode == READ_MOTOR_POSITION)
            theResult = Math.round(getFloatResult(inputData) * 360); // round to nearest degree
        else
            theResult = getFloatResult(inputData);
    }
    else if (type == UIREAD)
    {
        if (mode == UIREAD_BATTERY)
        {
            theResult = inputData[5];
        }
    }
    else if (type == BEGIN_DOWNLOAD)
    {
        theResult = inputData[6];
        var handle = inputData[7];
        
        if (theResult == 0)
        {
            console_log("BEGIN_DOWNLOAD status: " + theResult + " handle: " + handle);
            
            var fileData = mode;
            
            continueDownload( decimalToLittleEndianHex(handle, 2), fileData);
        }
        else
        {
            console_log("BEGIN_DOWNLOAD non-success status: " + theResult + " handle: " + handle);
        }
    }
    
    global_sensor_result[port] = theResult;
    
    // do the callback
    console_log("result: " + theResult);
    if (callback)
        callback(theResult);
    
    while(callback = waitingCallbacks[port].shift())
    {
        console_log("result (coalesced): " + theResult);
        callback(theResult);
    }
    
    // done with this query
    thePendingQuery = null;
    
    // go look for the next query
    executeQueryQueueAgain();
}

//// extension callbacks



function capSpeed(speed)
{
    if (speed > 100) { speed = 100; }
    if (speed < -100) { speed = -100; }
    return speed;
}



function motor(which, speed)
{
    speed = capSpeed(speed);
    var motorBitField = getMotorBitsHexString(which);
    
    var speedBits = getPackedOutputHexString(speed, 1);
    
    var motorsOnCommand = createMessage(DIRECT_COMMAND_PREFIX + SET_MOTOR_SPEED + motorBitField + speedBits + SET_MOTOR_START + motorBitField);
    
    return motorsOnCommand;
}

function motor2(which, speed)
{
    speed = capSpeed(speed);
    var p =  which.split("+");
    
    var motorBitField1 = getMotorBitsHexString(p[0]);
    var motorBitField2 = getMotorBitsHexString(p[1]);
    var motorBitField = getMotorBitsHexString(which);
    
    var speedBits1 = getPackedOutputHexString(speed, 1);
    var speedBits2 = getPackedOutputHexString(speed * -1, 1);
    
    var motorsOnCommand = createMessage(DIRECT_COMMAND_PREFIX
                                        + SET_MOTOR_SPEED + motorBitField1 + speedBits1
                                        + SET_MOTOR_SPEED + motorBitField2 + speedBits2
                                        
                                        + SET_MOTOR_START + motorBitField);
    
    return motorsOnCommand;
}



function playFreqM2M(freq, duration)
{
    console_log("playFreqM2M duration: " + duration + " freq: " + freq);
    var volume = 100;
    var volString = getPackedOutputHexString(volume, 1);
    var freqString = getPackedOutputHexString(freq, 2);
    var durString = getPackedOutputHexString(duration, 2);
    
    var toneCommand = createMessage(DIRECT_COMMAND_PREFIX + PLAYTONE + volString + freqString + durString);
    
    addToQueryQueue([TONE_QUERY, 0, null, toneCommand]);
}

function clearDriveTimer()
{
    if (driveTimer)
        clearInterval(driveTimer);
    driveTimer = 0;
    if (driveCallback)
        driveCallback();
    driveCallback = 0;
}


var driveTimer = 0;
driveCallback = 0;

function howStopCode(how)
{
    if (how == 'brake')
        return 1;
    else
        return 0;
}

function motorsStop(how)
{
    console_log("motorsStop");
    
    var motorBitField = getMotorBitsHexString("all");
    
    var howHex = getPackedOutputHexString(howStopCode(how), 1);
    
    var motorsOffCommand = createMessage(DIRECT_COMMAND_PREFIX + SET_MOTOR_STOP + motorBitField + howHex);
    
    addToQueryQueue([DRIVE_QUERY, 0, null, motorsOffCommand]);
}

/*
 function sendNOP()
 {
 var nopCommand = createMessage(DIRECT_COMMAND_PREFIX + NOOP);
 }
 */



function readTouchSensor(portInt, callback)
{
    readFromSensor(portInt, TOUCH_SENSOR, mode0, callback);
}

function readIRRemoteSensor(portInt, callback)
{
    readFromSensor2(portInt, IR_SENSOR, IR_REMOTE, callback);
}

function readFromColorSensor(portInt, modeCode, callback)
{
    readFromSensor2(portInt, COLOR_SENSOR, modeCode, callback);
}



function readThatBatteryLevel(callback)
{
    console_log("Going to read battery level");
    var portInt = 8; // bogus port number
    UIRead(portInt, UIREAD_BATTERY, callback);
}


function readFromSensor(port, type, mode, callback)
{
    var theCommand = createMessage(DIRECT_COMMAND_REPLY_PREFIX +
                                   READ_SENSOR +
                                   hexcouplet(port) +
                                   type +
                                   mode + "60");
    
    addToQueryQueue([port, type, mode, callback, theCommand]);
}

function readFromSensor2(port, type, mode, callback)
{
    var theCommand = createMessage(DIRECT_COMMAND_REPLY_SENSOR_PREFIX +
                                   INPUT_DEVICE_READY_SI + "00" + // layer
                                   hexcouplet(port) + "00" + // type
                                   mode +
                                   "0160"); // result stuff
    
    addToQueryQueue([port, type, mode, callback, theCommand]);
}


// this routine is awful similar to readFromSensor2...
function readFromAMotor(port, type, mode, callback)
{
    var theCommand = createMessage(DIRECT_COMMAND_REPLY_SENSOR_PREFIX +
                                   INPUT_DEVICE_READY_SI + "00" + // layer
                                   hexcouplet(port+12) + "00" + // type
                                   mode +
                                   "0160"); // result stuff
    
    addToQueryQueue([port, type, mode, callback, theCommand]);
}

function UIRead(port, subtype, callback)
{
    var theCommand = createMessage(DIRECT_COMMAND_REPLY_PREFIX +
                                   UIREAD + subtype +
                                   "60"); // result stuff
    
    addToQueryQueue([port, UIREAD, subtype, callback, theCommand]);
}


function clearScreen()
{
    var theCommand = createMessage(DIRECT_COMMAND_PREFIX +
                                   UIDRAW + UIDRAW_FILLWINDOW +
                                   "000000" + UIDRAW + UIDRAW_UPDATE);
    
    addToQueryQueue([UIDRAW_QUERY, 0, null, theCommand]);
}

// image file code

function uploadAndDrawCatFile()
{
    var fileName = "../prjs/tst/cat.rgf";
    var catRGFFile = "b28000000000000000000000000000000000000000000000000000000003000000000018000000000000000000000000000000000600000000001c000000000000000000000000000000001e00000000001e000000000000000000000000000000003e00000000001f00000000000000000000000000000000f600000000c01b00000000000000000000000000000000e601000000e039000000000000000000000000000000008603000000f03800000000000000000000000000000000060f0000003838000000000000000000000000000000000e1e0000001c32000000000000000000000000000000004e780000004e30000000000000000000000000000000000ef00000e00771000000000000000000000000000000000ec1c3ffff0374000000000000000000000000000000004e84ffff7f9060000000000000000000000000000000000e007e00800760000000000000000000000000000000000c490400004064000000000000000000000000000000004c0018920410e1000000000000000000000000000000000c9220005004e0000000000000000000000000000000008c0082240141c4010000000000000000000000000000000c480800081081030000000000000000000000000000000c022192200404070000000000000000000000000000009c10800082207f0e0000000000000000000000000000001c4408240888c11d0000000000000000000000000000001c00010041c2003b0000000000000000000000000000008c10fc10044000760000000000000000000000000000001c42cf43906400ec0000000000000000000000000000001c80030e016000d80100000000000000000000000000001c910018482200b00300000000000000000000000000000cc40030012000a00300000000000000000000000000004e60006024610060070000000000000000000000000000076100c00064004006000000000000000000000000000013240080906000c006000000000000000000000000008043200080014200c00e00000000000000000000000000800121000021c800800c00000000000000000000000000c0152400008bc000801d00000000000000000000000000c080600000038401871d00000000000000000000000000e00062000022900187190000000000000000000000000060244800008600038719000000000000000000000000007080c000060624068219000000000000000000000000037012c4000f26010480380000000000000000000000001e304090000702901cc07800000000000000000000000070380481010042023040f0000000000000000000000000c039110403000b00e020c0030000000000000000000000001f8020060021ff871f00074000000000000000000000009c04820c8081ff0700000e60000000000000000000000038100818c000ff0700001c1c0000000000000000000000788020f06000fe070000b80f0000000000000000000000982484821f00fc030000fe010000000000000000000000180040000000f003000066000000000000000000000000184912000000c00000006000000000000000000000000018000000000000000000c000000000000000000000000f7c240900000000000000c00000000000000000000000fe3f000000e00000000000c000000000000000000000000038490200900700000000c000000000000000000000000030000400103c00000000c00000000000000000000000007092000010e001000000c60000000000000000000000007000020010000f000000fc000000000000000000000000602404001800f0000000e03f0000000000000000000000e08000001000801f0000601c0000000000000000000000c0110800100000e007007000000000000000000000000080490200100000001c003000000000000000000000000080031000100000000c00380000000000000000000000000087040030000000060018000000000000000000000000000e20002000000003000c000000000000000000000000009c00004000008001000e0000000000000000000000000038520080000060000007000000000000000000000000007080000003003000800300000000000000000000000000e00800000e000c00e00100000000000000000000000000c023020038800300f0000000000000000000000000000080870800c0ff00003c0000000000000000000000000000001f1000000000001e0000000000000000000000000000c07f400000000080070000000000000000000080030000f0eb0501000000f00300000000000000000000e007000078801b040000007c0000000000000000000000700e00003c00e6010000c01f0000000000000000000000181800001e2406fe0000fe0300000000000000000000001838008087000600ffff3f0000000000000000000000000c3000c023522300f8ff010000000000000000000000000c6000f001000309e000000000000000000000000000000ce000f810894940c201000000000000000000000000000ec0003c40e000048803c00700000000000000000000000e80010e027284882007f00f00000000000000000000000e80018e9038212002063c3c2eae0f843fd739000000000e000306040e1000110e1e303131118ab49810000000000c0007462007080040bc0771a130118a649810000000000c000e0ee1a1000008f943608120110a449010000000000c001c0ec408020018e010629e000f0a44801f000000001800388e8001000088040060a000091e448010000000001850790c1223010000004472a1200911449010000000001000e01c400604004c92103823312911849810000000003004c29f044e00009c00801c1d9eb33b0ecf39000000002090083f200c09003e24091e00000000000000000000006004003c82180000770000070000000000000000000000c0201160081a1200e348c2030000000000000000000000800384e022988080c101f0010000000000000000000000008720c4001908c180077c000000000000000000000000001e0290111822e200df1f000000000000000000000000007c4802034e807400fc0700000000000000000000000000f00320fe17083800f000005c3cc6fd3f781c0e3f1f0000c03f80f93142700000000062428ca4458408042222000000fe7f002000e10000000061818c24450209042242000000f01f00c424c40100000041819420440209040a420000c0e7071201018003000000018194203c0209040e420000f0ff8140104b12070000000181a420240209040a420000383c2008c403400e0000004181a42024028944224200001c000481f08f041c0000006242c420a4848844322200004c0041207c1e10380000003c3c8e70ce78fc7e3f1f00000c4910081e3c42710000000000000000000000000000000c0004c1073808e40000000000000000000000000000009c2421f003f000c00100000000000000000000000000001c00087c00e025810700000000000000000000000000001892801f00c001140e00000000000000000000000000009800fe07008093401e00000000000000000000000000003824f80000000700f8ff010000000000000000000000003001390000001e92f0ff0f000000000000000000000000704870000000bc0002001f000000000000000000000000e000640000003848000078000000000000000000000000c024e1000000f000240970000000000000000000000000c001c0000000e0110120e10000000000000000000000008093c8000000c0439004c40000000000000000000000000003c200000000070490c00100000000000000000000000047c0000000004e2001c8010000000000000000000000001ec8000000001c0824c2010000000000000000000000003ce000000000780080c001000000000000000000000000f87c00000000f0bf00e000000000000000000000000000e03f00000000e0ffffff00000000000000000000000000800f0000000000f8ff3f000000000000000000000000000000000000000000f00700000000000000000000";
    
    uploadAndDrawRGFData(catRGFFile, fileName);
}

function drawFile(fileNameHex)
{
    console_log("UIDRAW_BMPFILE");
    var theCommand = createMessage(DIRECT_COMMAND_PREFIX +
                                   UIDRAW + UIDRAW_BMPFILE +
                                   "01" + // forground color
                                   getPackedOutputHexString(0,2) + getPackedOutputHexString(0,2) + // location
                                   "84" + fileNameHex + // LCS string encoding
                                   UIDRAW + UIDRAW_UPDATE);
    
    addToQueryQueue([UIDRAW_QUERY, 0, null, theCommand]);
}

function uploadAndDrawRGFData(fileDataHexString, name)
{
    var fileLength = fileDataHexString.length / 2;
    var lengthString = decimalToLittleEndianHex(fileLength, 8);
    
    var fileNameHex = stringToHexString(name);
    
    var theCommand = createMessage(
                                   BEGIN_DOWNLOAD + lengthString + fileNameHex //"6361742e726766000" // the filename...
                                   );
    
    addToQueryQueue([8, BEGIN_DOWNLOAD, fileNameHex + "|" + fileDataHexString, null, theCommand]);
}

function continueDownload(handle, fileData)
{
    console_log("CONTINUE_DOWNLOAD");
    var p =  fileData.split("|");
    
    var data = p[1];
    var nameHex = p[0];
    
    var theCommand = createMessage(
                                   CONTINUE_DOWNLOAD + handle + data
                                   );
    
    addToQueryQueue([SYSTEM_COMMAND, 0, null, theCommand]);
    
    drawFile(nameHex);
}

function startMotors(which, speed)
{
    clearDriveTimer();
    
    console_log("motor " + which + " speed: " + speed);
    
    motorCommand = motor(which, speed);
    
    addToQueryQueue([DRIVE_QUERY, 0, null, motorCommand]);
}

function motorDegrees(which, speed, degrees, howStop)
{
    speed = capSpeed(speed);
    
    if (degrees < 0)
    {
        degrees *= -1;
        speed *= -1;
    }
    
    var motorBitField = getMotorBitsHexString(which);
    var speedBits = getPackedOutputHexString(speed, 1);
    var stepRampUpBits = getPackedOutputHexString(0, 3);
    var stepConstantBits = getPackedOutputHexString(degrees, 3);
    var stepRampDownBits = getPackedOutputHexString(0, 3);
    var howHex = getPackedOutputHexString(howStopCode(howStop), 1);
    
    var motorsCommand = createMessage(DIRECT_COMMAND_PREFIX + SET_MOTOR_STEP_SPEED + motorBitField + speedBits
                                      + stepRampUpBits + stepConstantBits + stepRampDownBits + howHex
                                      + SET_MOTOR_START + motorBitField);
    
    addToQueryQueue([DRIVE_QUERY, 0, null, motorsCommand]);
}


function playTone(tone, duration, callback)
{
    var freq = frequencies[tone];
    console_log("playTone " + tone + " duration: " + duration + " freq: " + freq);
    var volume = 100;
    var volString = getPackedOutputHexString(volume, 1);
    var freqString = getPackedOutputHexString(freq, 2);
    var durString = getPackedOutputHexString(duration, 2);
    
    var toneCommand = createMessage(DIRECT_COMMAND_PREFIX + PLAYTONE + volString + freqString + durString);
    
    addToQueryQueue([TONE_QUERY, duration, callback, toneCommand]);
}

function playFreq(freq, duration, callback)
{
    console_log("playFreq duration: " + duration + " freq: " + freq);
    var volume = 100;
    var volString = getPackedOutputHexString(volume, 1);
    var freqString = getPackedOutputHexString(freq, 2);
    var durString = getPackedOutputHexString(duration, 2);
    
    var toneCommand = createMessage(DIRECT_COMMAND_PREFIX + PLAYTONE + volString + freqString + durString);
    
    addToQueryQueue([TONE_QUERY, duration, callback, toneCommand]);
}

function allMotorsOff(how)
{
    clearDriveTimer();
    motorsStop(how);
}

function steeringControl(ports, what, duration, callback)
{
    clearDriveTimer();
    var defaultSpeed = 50;
    var motorCommand = null;
    if (what == 'forward')
    {
        motorCommand = motor(ports, defaultSpeed);
    }
    else if (what == 'reverse')
    {
        motorCommand = motor(ports, -1 * defaultSpeed);
    }
    else if (what == 'right')
    {
        motorCommand = motor2(ports, defaultSpeed);
    }
    else if (what == 'left')
    {
        motorCommand = motor2(ports, -1 * defaultSpeed);
    }
    
    addToQueryQueue([DRIVE_QUERY_DURATION, duration, callback, motorCommand]);
}

function whenButtonPressed(port)
{
    if (notConnected())
        return false;
    var portInt = parseInt(port) - 1;
    readTouchSensor(portInt, null);
    return global_sensor_result[portInt];
}

function whenRemoteButtonPressed(IRbutton, port)
{
    if (notConnected())
        return false;
    
    var portInt = parseInt(port) - 1;
    readIRRemoteSensor(portInt, null);
    
    return (global_sensor_result[portInt] == IRbutton);
}

function readTouchSensorPort(port, callback)
{
    var portInt = parseInt(port) - 1;
    readTouchSensor(portInt, callback);
}

function readColorSensorPort(port, mode, callback)
{
    var modeCode = AMBIENT_INTENSITY;
    if (mode == 'reflected') { modeCode = REFLECTED_INTENSITY; }
    if (mode == 'color') { modeCode = COLOR_VALUE; }
    if (mode == 'RGBcolor') { modeCode = COLOR_RAW_RGB; }
    
    var portInt = parseInt(port) - 1;
    readFromColorSensor(portInt, modeCode, callback);
}

var lineCheckingInterval = 0;

function waitUntilDarkLinePort(port, callback)
{
    if (lineCheckingInterval)
        clearInterval(lineCheckingInterval);
    lineCheckingInterval = 0;
    var modeCode = REFLECTED_INTENSITY;
    var portInt = parseInt(port) - 1;
    global_sensor_result[portInt] = -1;
    
    lineCheckingInterval = window.setInterval(
      function()
      {
          readFromColorSensor(portInt, modeCode, null);
          if (global_sensor_result[portInt] < 25 && global_sensor_result[portInt] >= 0)    // darkness or just not reflection (air)
          {
              clearInterval(lineCheckingInterval);
              lineCheckingInterval = 0;
              callback();
          }
      }, 5);
}

function readGyroPort(mode, port, callback)
{
    var modeCode = GYRO_ANGLE;
    if (mode == 'rate') { modeCode = GYRO_RATE; }
    
    var portInt = parseInt(port) - 1;
    
    readFromSensor2(portInt, GYRO_SENSOR, modeCode, callback);
}

function readDistanceSensorPort(port, callback)
{
    var portInt = parseInt(port) - 1;
    
    readFromSensor2(portInt, IR_SENSOR, IR_PROX, callback);
}

function readRemoteButtonPort(port, callback)
{
    var portInt = parseInt(port) - 1;
    
    readIRRemoteSensor(portInt, callback);
}

function readFromMotor(mmode, which, callback)
{
    var portInt = getMotorIndex(which);
    var mode = READ_MOTOR_POSITION; // position
    if (mmode == 'speed')
        mode = READ_MOTOR_SPEED;
    
    readFromAMotor(portInt, READ_FROM_MOTOR, mode, callback);
}

function readBatteryLevel(callback)
{
    readThatBatteryLevel(callback);
}


// ScratchX specific stuff

var DEBUG_NO_EV3 = false;
var theEV3Device = theEV3Device || null;
var EV3ScratchAlreadyLoaded = EV3ScratchAlreadyLoaded || false;
var EV3Connected = EV3Connected || false;
var potentialEV3Devices = potentialEV3Devices || [];
var waitingForInitialConnection = waitingForInitialConnection || false;
var potentialDevices = potentialDevices || []; // copy of the list
var connecting = connecting || false;
var connectionTimeout = connectionTimeout || null;


function connectingOrConnected()
{
    return (EV3Connected || connecting);
}

function weConnected()
{
    waitingForInitialConnection = false;
    
    EV3Connected = true;
    connecting = false;
}

function notConnected()
{
    return (!theEV3Device || !EV3Connected);
}

function disconnected()
{
    EV3Connected = false;
    
    //    alert("The connection to the brick was lost. Check your brick and refresh the page to reconnect. (Don't forget to save your project first!)");
    /* if (r == true) {
     reconnect();
     } else {
     // do nothing
     }
     */
}


function sendCommand(commandArray)
{
    if ((EV3Connected || connecting) && theEV3Device)
    {
        console_log("sending: " + createHexString(commandArray));
        
        theEV3Device.send(commandArray.buffer);
    }
    else
    {
        console_log("sendCommand called when not connected");
    }
}

function resetConnection()
{
    clearSensorStatuses();
    
    waitingQueries = [];
    
    // clear a query we might have been waiting for
    thePendingQuery = null;
    
    counter = 0;
}

function tryToConnect()
{
    console_log("tryToConnect()");
    
    lastCommandWeWereTrying = waitingQueries.pop();

    resetConnection();
    
    theEV3Device.open({ stopBits: 0, bitRate: 57600, ctsFlowControl: 0});
    console_log(': Attempting connection with ' + theEV3Device.id);
    theEV3Device.set_receive_handler(receive_handler);
    
    connecting = true;
    testTheConnection(startupBatteryCheckCallback);
    waitingForInitialConnection = true;
    connectionTimeout = setTimeout(connectionTimeOutCallback, 5000);
}

function connectionTimeOutCallback()
{
    if (waitingForInitialConnection == true)
    {
        console_log("Initial connection timed out");
        connecting = false;
        
        if (potentialDevices.length == 0)
        {
            console_log("Tried all devices with no luck.");
            
            //  alert("Failed to connect to a brick.\n\nMake sure your brick is:\n 1) powered on with Bluetooth On\n 2) named starting with serial (if on a Mac)\n 3) paired with this computer\n 4) the iPhone/iPad/iPod check box is NOT checked\n 5) Do not start a connection to or from the brick in any other way. Let the Scratch plug-in handle it!\n\nand then try reloading the webpage.");
            /*  if (r == true) {
             reconnect();
             } else {
             // do nothing
             }
             */
            theEV3Device = null;
            
            // xxx at this point, we might have an outstanding query with a callback we need to call...
        }
        else
        {
            tryNextDevice();
        }
    }
}

function tryNextDevice()
{
    potentialDevices.sort((function(a, b){return b.id.localeCompare(a.id)}));
    
    console_log("tryNextDevice: " + potentialDevices);
    var device = potentialDevices.shift();
    if (!device)
        return;
    
    theEV3Device = device;
    
    if (!DEBUG_NO_EV3)
    {
        tryToConnect();
    }
}

function tryAllDevices()
{
    console_log("tryAllDevices()");
    potentialDevices = potentialEV3Devices.slice(0);
    // start recursive loop
    tryNextDevice();
}

function checkConnected()
{
    if (!EV3Connected && !connecting)
    {
        console_log("executeQueryQueue called with no connection");
        if (theEV3Device && !connecting)
        {
            tryToConnect(); // try to connect
        }
        else if (!connecting)
        {
            tryAllDevices(); // try device list again
        }
        return false;
    }
    return true;
}
        
(
function(ext)
{
     ext.reconnectToDevice = function()
     {
        tryAllDevices();
     }
     
     ext._getStatus = function()
     {
         if (!EV3Connected)
            return { status:1, msg:'Disconnected' };
         else
            return { status:2, msg:'Connected' };
     };
     
     ext._deviceRemoved = function(dev)
     {
         console_log('Device removed');
         // Not currently implemented with serial devices
     };
     
     ext._deviceConnected = function(dev)
     {
         console_log('_deviceConnected: ' + dev.id);
         if (EV3Connected)
         {
            console_log("Already EV3Connected. Ignoring");
         }
         // brick's serial port must be named like tty.serialBrick7-SerialPort
         // this is how 10.10 is naming it automatically, the brick name being serialBrick7
         // the Scratch plugin is only letting us know about serial ports with names that
         // "begin with tty.usbmodem, tty.serial, or tty.usbserial" - according to khanning
         
         if ((dev.id.indexOf('/dev/tty.serial') === 0 && dev.id.indexOf('-SerialPort') != -1) || dev.id.indexOf('COM') === 0)
         {
         
             if (potentialEV3Devices.filter(function(e) { return e.id == dev.id; }).length == 0)
             {
                potentialEV3Devices.push(dev);
             }
             
             if (!deviceTimeout)
                deviceTimeout = setTimeout(tryAllDevices, 1000);
         }
     };
     
     
     ext._shutdown = function()
     {
         console_log('SHUTDOWN: ' + ((theEV3Device) ? theEV3Device.id : "null"));
        /*
         if (theEV3Device)
         theEV3Device.close();
         if (poller)
         clearInterval(poller);
         EV3Connected = false;
         theEV3Device = null;
         */
     };
     
     ext.startMotors = function(which, speed)
     {
        startMotors(which, speed);
     }
     
     ext.motorDegrees = function(which, speed, degrees, howStop)
     {
        motorDegrees(which, speed, degrees, howStop);
     }
     
     ext.playTone = function(tone, duration, callback)
     {
        playTone(tone, duration, callback);
     }
     
     ext.playFreq = function(freq, duration, callback)
     {
        playFreq(freq, duration, callback);
     }
     
     ext.allMotorsOff = function(how)
     {
        allMotorsOff(how)
     }
     
     ext.steeringControl = function(ports, what, duration, callback)
     {
        steeringControl(ports, what, duration, callback)
     }
     
     ext.whenButtonPressed = function(port)
     {
        return whenButtonPressed(port);
     }
     
     ext.whenRemoteButtonPressed = function(IRbutton, port)
     {
        return whenRemoteButtonPressed(IRbutton, port);
     }
     
     ext.readTouchSensorPort = function(port, callback)
     {
        readTouchSensorPort(port, callback);
     }
     
     ext.readColorSensorPort = function(port, mode, callback)
     {
        readColorSensorPort(port, mode, callback);
     }
     
     
     ext.waitUntilDarkLinePort = function(port, callback)
     {
        waitUntilDarkLinePort(port, callback);
     }
     
     ext.readGyroPort = function(mode, port, callback)
     {
        readGyroPort(mode, port, callback);
     }
     
     ext.readDistanceSensorPort = function(port, callback)
     {
        readDistanceSensorPort(port, callback);
     }
     
     ext.readRemoteButtonPort = function(port, callback)
     {
        readRemoteButtonPort(port, callback);
     }
     
     ext.readFromMotor = function(mmode, which, callback)
     {
        readFromMotor(mmode, which, callback);
     }
     
     ext.readBatteryLevel = function(callback)
     {
        readBatteryLevel(callback);
     }
     
     // Block and block menu descriptions
     var descriptor = {
     blocks: [
              ['w', 'drive %m.dualMotors %m.turnStyle %n seconds',         'steeringControl',  'B+C', 'forward', 3],
              [' ', 'start motor %m.whichMotorPort speed %n',              'startMotors',      'B+C', 100],
              [' ', 'rotate motor %m.whichMotorPort speed %n by %n degrees then %m.brakeCoast',              'motorDegrees',      'A', 100, 360, 'brake'],
              [' ', 'stop all motors %m.brakeCoast',                       'allMotorsOff',     'brake'],
              ['h', 'when button pressed on port %m.whichInputPort',       'whenButtonPressed','1'],
              ['h', 'when IR remote %m.buttons pressed port %m.whichInputPort', 'whenRemoteButtonPressed','Top Left', '1'],
              ['R', 'button pressed %m.whichInputPort',                    'readTouchSensorPort',   '1'],
              ['w', 'play note %m.note duration %n ms',                    'playTone',         'C5', 500],
              ['w', 'play frequency %n duration %n ms',                    'playFreq',         '262', 500],
              ['R', 'light sensor %m.whichInputPort %m.lightSensorMode',   'readColorSensorPort',   '1', 'color'],
              //    ['w', 'wait until light sensor %m.whichInputPort detects black line',   'waitUntilDarkLinePort',   '1'],
              ['R', 'measure distance %m.whichInputPort',                  'readDistanceSensorPort',   '1'],
              ['R', 'remote button %m.whichInputPort',                     'readRemoteButtonPort',   '1'],
              // ['R', 'gyro  %m.gyroMode %m.whichInputPort',                 'readGyroPort',  'angle', '1'],
              ['R', 'motor %m.motorInputMode %m.whichMotorIndividual',     'readFromMotor',   'position', 'A'],
              
              //    ['R', 'battery level',   'readBatteryLevel'],
              //  [' ', 'reconnect', 'reconnectToDevice'],
              ],
     menus: {
     whichMotorPort:   ['A', 'B', 'C', 'D', 'A+D', 'B+C'],
     whichMotorIndividual:   ['A', 'B', 'C', 'D'],
     dualMotors:       ['A+D', 'B+C'],
     turnStyle:        ['forward', 'reverse', 'right', 'left'],
     brakeCoast:       ['brake', 'coast'],
     lightSensorMode:  ['reflected', 'ambient', 'color'],
     motorInputMode: ['position', 'speed'],
     gyroMode: ['angle', 'rate'],
     note:["C4","D4","E4","F4","G4","A4","B4","C5","D5","E5","F5","G5","A5","B5","C6","D6","E6","F6","G6","A6","B6","C#4","D#4","F#4","G#4","A#4","C#5","D#5","F#5","G#5","A#5","C#6","D#6","F#6","G#6","A#6"],
     whichInputPort: ['1', '2', '3', '4'],
     buttons: IRbuttonNames,
     },
     };
     
     var serial_info = {type: 'serial'};
     ScratchExtensions.register('EV3 Control', descriptor, ext, serial_info);
     console_log(' registered extension. theEV3Device:' + theEV3Device);
     
     console_log("EV3ScratchAlreadyLoaded: " + EV3ScratchAlreadyLoaded);
     EV3ScratchAlreadyLoaded = true;
})({});
