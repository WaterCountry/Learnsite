/**
 * @license
 * Copyright 2012 Google LLC
 * SPDX-License-Identifier: Apache-2.0
 */

/**
 * @fileoverview Loading and saving blocks with localStorage and cloud storage.
 */
'use strict';

// Create a namespace.
var BlocklyStorage = {};

/**
 * Backup code blocks to localStorage.
 * @param {!Blockly.WorkspaceSvg} workspace Workspace.
 * @private
 */
 
 

BlocklyStorage.backupBlocks_ = function(workspace) {
  var txt="";
  if ('localStorage' in window) {
    var xml = Blockly.Xml.workspaceToDom(workspace);
	//console.log("保存到本地存储：");
	//console.log(xml);
    // Gets the current URL, not including the hash.
    var url = window.location.href.split('#')[0]+snum+id;
    window.localStorage.setItem(url, Blockly.Xml.domToText(xml));
	txt = Blockly.Xml.domToText(xml);

  }
  return txt;
};

/**
 * Bind the localStorage backup function to the unload event.
 * @param {Blockly.WorkspaceSvg=} opt_workspace Workspace.
 */
BlocklyStorage.backupOnUnload = function(opt_workspace) {
  var workspace = opt_workspace || Blockly.getMainWorkspace();
  window.addEventListener('unload',
      function() {BlocklyStorage.backupBlocks_(workspace);}, false);
};

/**
 * Restore code blocks from localStorage.
 * @param {Blockly.WorkspaceSvg=} opt_workspace Workspace.
 */
BlocklyStorage.restoreBlocks = function(opt_workspace) {
    var url = window.location.href.split('#')[0]+snum+id;
    var workspace = opt_workspace || Blockly.getMainWorkspace();     
	if(cf.length>2){
	    var codefile = decodeURIComponent(cf);
        codefile = decodeURIComponent(codefile);
	    console.log("从服务器恢复："); 
		var xmlstore= Blockly.Xml.textToDom(codefile);    
		//console.log(xmlstore);
		Blockly.Xml.domToWorkspace(xmlstore, workspace);  
	}
    else{
          if ('localStorage' in window && window.localStorage[url]) {
	        console.log("从本地存储恢复：");
            var xml = Blockly.Xml.textToDom(window.localStorage[url]);
	        //console.log(xml);
	        //workspace.clear();
            Blockly.Xml.domToWorkspace(xml, workspace); 
          }
    }

};

/**
 * Save blocks to database and return a link containing key to XML.
 * @param {Blockly.WorkspaceSvg=} opt_workspace Workspace.
 */
BlocklyStorage.link = function(opt_workspace) {
  var workspace = opt_workspace || Blockly.getMainWorkspace();
  var xml = Blockly.Xml.workspaceToDom(workspace, true);
  // Remove x/y coordinates from XML if there's only one block stack.
  // There's no reason to store this, removing it helps with anonymity.
  if (workspace.getTopBlocks(false).length === 1 && xml.querySelector) {
    var block = xml.querySelector('block');
    if (block) {
      block.removeAttribute('x');
      block.removeAttribute('y');
    }
  }
  var data = Blockly.Xml.domToText(xml);
  BlocklyStorage.makeRequest_('/storage', 'xml', data, workspace);
};

/**
 * Retrieve XML text from database using given key.
 * @param {string} key Key to XML, obtained from href.
 * @param {Blockly.WorkspaceSvg=} opt_workspace Workspace.
 */
BlocklyStorage.retrieveXml = function(key, opt_workspace) {
  var workspace = opt_workspace || Blockly.getMainWorkspace();
  BlocklyStorage.makeRequest_('/storage', 'key', key, workspace);
};

/**
 * Global reference to current AJAX request.
 * @type {XMLHttpRequest}
 * @private
 */
BlocklyStorage.httpRequest_ = null;

/**
 * Fire a new AJAX request.
 * @param {string} url URL to fetch.
 * @param {string} name Name of parameter.
 * @param {string} content Content of parameter.
 * @param {!Blockly.WorkspaceSvg} workspace Workspace.
 * @private
 */
BlocklyStorage.makeRequest_ = function(url, name, content, workspace) {
  if (BlocklyStorage.httpRequest_) {
    // AJAX call is in-flight.
    BlocklyStorage.httpRequest_.abort();
  }
  BlocklyStorage.httpRequest_ = new XMLHttpRequest();
  BlocklyStorage.httpRequest_.name = name;
  BlocklyStorage.httpRequest_.onreadystatechange =
      BlocklyStorage.handleRequest_;
  BlocklyStorage.httpRequest_.open('POST', url);
  BlocklyStorage.httpRequest_.setRequestHeader('Content-Type',
      'application/x-www-form-urlencoded');
  BlocklyStorage.httpRequest_.send(name + '=' + encodeURIComponent(content));
  BlocklyStorage.httpRequest_.workspace = workspace;
};

/**
 * Callback function for AJAX call.
 * @private
 */
BlocklyStorage.handleRequest_ = function() {
  if (BlocklyStorage.httpRequest_.readyState === 4) {
    if (BlocklyStorage.httpRequest_.status !== 200) {
      BlocklyStorage.alert(BlocklyStorage.HTTPREQUEST_ERROR + '\n' +
          'httpRequest_.status: ' + BlocklyStorage.httpRequest_.status);
    } else {
      var data = BlocklyStorage.httpRequest_.responseText.trim();
      if (BlocklyStorage.httpRequest_.name === 'xml') {
        window.location.hash = data;
        BlocklyStorage.alert(BlocklyStorage.LINK_ALERT.replace('%1',
            window.location.href));
      } else if (BlocklyStorage.httpRequest_.name === 'key') {
        if (!data.length) {
          BlocklyStorage.alert(BlocklyStorage.HASH_ERROR.replace('%1',
              window.location.hash));
        } else {
          BlocklyStorage.loadXml_(data, BlocklyStorage.httpRequest_.workspace);
        }
      }
      BlocklyStorage.monitorChanges_(BlocklyStorage.httpRequest_.workspace);
    }
    BlocklyStorage.httpRequest_ = null;
  }
};

/**
 * Start monitoring the workspace.  If a change is made that changes the XML,
 * clear the key from the URL.  Stop monitoring the workspace once such a
 * change is detected.
 * @param {!Blockly.WorkspaceSvg} workspace Workspace.
 * @private
 */
BlocklyStorage.monitorChanges_ = function(workspace) {
  var startXmlDom = Blockly.Xml.workspaceToDom(workspace);
  var startXmlText = Blockly.Xml.domToText(startXmlDom);
  function change() {
    var xmlDom = Blockly.Xml.workspaceToDom(workspace);
    var xmlText = Blockly.Xml.domToText(xmlDom);
    if (startXmlText !== xmlText) {
      window.location.hash = '';
      workspace.removeChangeListener(change);
    }
  }
  workspace.addChangeListener(change);
};

/**
 * Load blocks from XML.
 * @param {string} xml Text representation of XML.
 * @param {!Blockly.WorkspaceSvg} workspace Workspace.
 * @private
 */
BlocklyStorage.loadXml_ = function(xml, workspace) {
  try {
    xml = Blockly.Xml.textToDom(xml);
  } catch (e) {
    BlocklyStorage.alert(BlocklyStorage.XML_ERROR + '\nXML: ' + xml);
    return;
  }
  // Clear the workspace to avoid merge.
  workspace.clear();
  Blockly.Xml.domToWorkspace(xml, workspace);
};

/**
 * Present a text message to the user.
 * Designed to be overridden if an app has custom dialogs, or a butter bar.
 * @param {string} message Text to alert.
 */
BlocklyStorage.alert = function(message) {
  window.alert(message);
};

//


Blockly.Blocks['py_fun'] = {
  init: function() {
    this.appendDummyInput()
        .appendField(new Blockly.FieldTextInput("add"), "fun_name")
        .appendField(new Blockly.FieldTextInput("a,b"), "fun_val");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(290);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Python['py_fun'] = function(block) {
  var text_fun_name = block.getFieldValue('fun_name');
  var text_fun_val = block.getFieldValue('fun_val');
  // TODO: Assemble Python into code variable.
  var code = `${text_fun_name}(${text_fun_val})\n`;
  return code;
};

Blockly.Blocks['py_def'] = {
  init: function() {
    this.appendDummyInput()
        .appendField(new Blockly.FieldTextInput("add"), "define_name")
        .appendField(new Blockly.FieldTextInput("a,b"), "define_val");
    this.appendStatementInput("do")
        .setCheck(null);
    this.appendDummyInput()
        .appendField("return")
        .appendField(new Blockly.FieldTextInput("c"), "return_val");
    this.setColour(290);
    this.setPreviousStatement(true, null);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Python['py_def'] = function(block) {
  var text_define_name = block.getFieldValue('define_name');
  var text_define_val = block.getFieldValue('define_val');
  var statements_do = Blockly.Python.statementToCode(block, 'do');
  var text_return_val = block.getFieldValue('return_val');
  // TODO: Assemble Python into code variable.
  var code = `def ${text_define_name}(${text_define_val}):\n${statements_do}  return(${text_return_val})`;
  return code;
};

Blockly.Blocks['turtle_write'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("write")
        .appendField(new Blockly.FieldTextInput("\"你好！\""), "write_content");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(160);
 this.setTooltip("输出文本");
 this.setHelpUrl("");
  }
};

Blockly.Python['turtle_write'] = function(block) {
  var text_write_content = block.getFieldValue('write_content');
  // TODO: Assemble Python into code variable.
  var code = `write(${text_write_content})\n`;
  return code;
};

Blockly.Blocks['turtle_pensize'] = {
  init: function() {
    this.appendDummyInput()
        .appendField(new Blockly.FieldDropdown([["pensize","pensize"], ["speed","speed"]]), "pen")
		.appendField(new Blockly.FieldTextInput("2"), "size");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(160);
 this.setTooltip("画笔粗细、画笔速度 参数范围从0到10");
 this.setHelpUrl("");
  }
};

Blockly.Python['turtle_pensize'] = function(block) {
  var dropdown_pen = block.getFieldValue('pen');
  var text_size = block.getFieldValue('size');
  // TODO: Assemble Python into code variable.
  var code = `${dropdown_pen}(${text_size})\n`;
  return code;
};

Blockly.Blocks['turtle_circle'] = {
  init: function() {
    this.appendDummyInput()
        .appendField(new Blockly.FieldDropdown([["circle","circle"], ["dot","dot"]]), "circle")
        .appendField(new Blockly.FieldTextInput("20"), "radius");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(160);
 this.setTooltip("画圆、画点");
 this.setHelpUrl("");
  }
};

Blockly.Python['turtle_circle'] = function(block) {
  var dropdown_circle = block.getFieldValue('circle');
  var text_radius = block.getFieldValue('radius');
  // TODO: Assemble Python into code variable.
  var code =  `${dropdown_circle}(${text_radius})\n`;
  return code;
};

Blockly.Blocks['turtle_fun'] = {
  init: function() {
    this.appendDummyInput()
        .appendField(new Blockly.FieldDropdown([["home","home"], ["hideturtle","hideturtle"], ["showturtle","showturtle"], ["stamp","stamp"], ["clear","clear"], ["reset","reset"]]), "fun");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(160);
 this.setTooltip("回到起点、隐藏海龟、显示海龟、图章、擦除画布、清空画布");
 this.setHelpUrl("");
  }
};

Blockly.Python['turtle_fun'] = function(block) {
  var dropdown_fun = block.getFieldValue('fun');
  // TODO: Assemble Python into code variable.
  var code = `${dropdown_fun}()\n`;
  return code;
};

Blockly.Blocks['turtle_goto'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("goto")
        .appendField(new Blockly.FieldTextInput("0"), "x")
        .appendField(new Blockly.FieldTextInput("0"), "y");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(160);
 this.setTooltip("移动到当前坐标");
 this.setHelpUrl("");
  }
};

Blockly.Python['turtle_goto'] = function(block) {
  var text_x = block.getFieldValue('x');
  var text_y = block.getFieldValue('y');
  // TODO: Assemble Python into code variable.
  var code = `goto(${text_x},${text_y})\n`;
  return code;
};


Blockly.FieldColour.COLOURS = [
'LightPink', 'Pink', 'PaleVioletRed', 'HotPink', 'DeepPink', 'MediumVioletRed', 'Orchid', 'Thistle', 'plum', 'Violet', 'Fuchsia', 'Purple', 'MediumOrchid', 'DarkOrchid', 'Indigo', 'BlueViolet', 'MediumPurple', 'MediumSlateBlue', 'SlateBlue', 'DarkSlateBlue','Blue', 'MediumBlue', 'DarkBlue', 'RoyalBlue', 'CornflowerBlue', 'LightSteelBlue', 'SlateGray', 'DodgerBlue', 'SteelBlue', 'LightSkyBlue', 'SkyBlue', 'DeepSkyBlue', 'LightBLue', 'PowDerBlue', 'CadetBlue', 'Azure', 'LightCyan', 'PaleTurquoise', 'Cyan', 'DarkTurquoise', 'DarkSlateGray', 'DarkCyan', 'LightSeaGreen', 'Turquoise', 'MediumAquamarine', 'SpringGreen', 'SeaGreen','LightGreen', 'PaleGreen', 'DarkSeaGreen', 'LimeGreen', 'Lime', 'ForestGreen', 'Green', 'DarkGreen', 'LawnGreen', 'GreenYellow', 'OliveDrab', 'Beige', 'LightYellow', 'Yellow', 'Olive', 'DarkKhaki', 'LemonChiffon', 'Khaki', 'Gold', 'GoldEnrod','Wheat', 'Moccasin', 'Orange', 'PapayaWhip', 'BlanchedAlmond', 'NavajoWhite', 'AntiqueWhite', 'Tan', 'DarkOrange', 'Linen', 'Peru', 'PeachPuff', 'SandyBrown', 'Chocolate', 'SeaShell', 'Sienna', 'LightSalmon', 'Coral', 'OrangeRed', 'Tomato', 'MistyRose', 'Salmon', 'LightCoral', 'RosyBrown', 'IndianRed', 'Red', 'Crimson', 'Brown', 'FireBrick', 'DarkRed', 'White', 'WhiteSmoke', 'Gainsboro', 'LightGrey', 'Silver', 'Gray','DimGray', 'Black'
];

Blockly.Blocks['turtle_color'] = {
  init: function() {
    this.appendDummyInput()
        .appendField(new Blockly.FieldDropdown([["pencolor","pencolor"], ["fillcolor","fillcolor"]]), "color")
        .appendField(new Blockly.FieldColour("#000000"), "color_val");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(160);
 this.setTooltip("画笔颜色、填充颜色");
 this.setHelpUrl("");
  }
};

Blockly.Python['turtle_color'] = function(block) {
  var dropdown_color = block.getFieldValue('color');
  var colour_color_val = block.getFieldValue('color_val');
  // TODO: Assemble Python into code variable.
  var code = `${dropdown_color}('${colour_color_val}')\n`;
  return code;
};


Blockly.Blocks['turtle_pen'] = {
  init: function() {
    this.appendDummyInput()
        .appendField(new Blockly.FieldDropdown([["penup","penup"], ["pendown","pendown"]]), "turtle_pen");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(160);
 this.setTooltip("提起笔或落下笔");
 this.setHelpUrl("");
  }
};

Blockly.Python['turtle_pen'] = function(block) {
  var dropdown_pen  = block.getFieldValue('turtle_pen');
  // TODO: Assemble Python into code variable.
  var code = `${dropdown_pen }()\n`;
  return code;
};

Blockly.Blocks['turtle_fill'] = {
  init: function() {
    this.appendDummyInput()
        .appendField(new Blockly.FieldDropdown([["begin_fill","begin_fill"], ["end_fill","end_fill"]]), "turtle_fill");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(160);
 this.setTooltip("开始填充或结束填充");
 this.setHelpUrl("");
  }
};

Blockly.Python['turtle_fill'] = function(block) {
  var dropdown_fill  = block.getFieldValue('turtle_fill');
  // TODO: Assemble Python into code variable.
  var code = `${dropdown_fill }()\n`;
  return code;
};


Blockly.Blocks['turtle_move'] = {
  init: function() {
    this.appendDummyInput()
        .appendField(new Blockly.FieldDropdown([["forward","forward"], ["backward","backward"]]), "action")
        .appendField(new Blockly.FieldTextInput("100"), "move_step");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(160);
 this.setTooltip("前进或后退");
 this.setHelpUrl("");
  }
};

Blockly.Python['turtle_move'] = function(block) {
  var dropdown_action = block.getFieldValue('action');
  var text_move_step = block.getFieldValue('move_step');
  // TODO: Assemble Python into code variable.
  var code = `${dropdown_action}(${text_move_step})\n`;
  return code;
};


Blockly.Blocks['turtle_turn'] = {
  init: function() {
    this.appendDummyInput()
        .appendField(new Blockly.FieldDropdown([["left","left"], ["right","right"]]), "action")
        .appendField(new Blockly.FieldTextInput("90"), "turn_angle")
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(160);
 this.setTooltip("左转或右转");
 this.setHelpUrl("");
  }
};

Blockly.Python['turtle_turn'] = function(block) {
  var dropdown_turn = block.getFieldValue('action');
  var angle_turn_angle = block.getFieldValue('turn_angle');
  // TODO: Assemble Python into code variable.
  var code = `${dropdown_turn}(${angle_turn_angle})\n`;
  return code;
};


Blockly.Blocks['turtle_sleep'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("sleep")
		.appendField(new Blockly.FieldTextInput("1"), "sleep_step");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(0);
 this.setTooltip("睡眠（1秒），需要导入时间库time");
 this.setHelpUrl("");
  }
};

Blockly.Python['turtle_sleep'] = function(block) {
  var text_sleep_step = block.getFieldValue('sleep_step');
  // TODO: Assemble Python into code variable.
  var code = `sleep(${text_sleep_step})\n`;
  return code;
};

Blockly.Blocks['py_import'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("from")
        .appendField(new Blockly.FieldDropdown([["turtle","turtle"], ["time","time"], ["math","math"], ["random","random"]]), "model_val")
        .appendField("import *");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(0);
 this.setTooltip("导入库(海龟、时间、数学、随机)");
 this.setHelpUrl("");
  }
};

Blockly.Python['py_import'] = function(block) {
  var dropdown_model_val = block.getFieldValue('model_val');
  // TODO: Assemble Python into code variable.
  var code = `from ${dropdown_model_val} import * \n`;
  return code;
};


Blockly.Blocks['py_text'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("\"")
        .appendField(new Blockly.FieldTextInput("你好"), "content")
        .appendField("\"");
    this.setOutput(true, null);
    this.setColour(120);
 this.setTooltip("文本");
 this.setHelpUrl("");
  }
};

Blockly.Python['py_text'] = function(block) {
  var text_content = block.getFieldValue('content');
  // TODO: Assemble Python into code variable.
  var code =`"${text_content}"`; 
  // TODO: Change ORDER_NONE to the correct strength.
  return [code];
};

Blockly.Blocks['py_data'] = {
  init: function() {
    this.appendDummyInput()
        .appendField(new Blockly.FieldTextInput(""), "date_content");
    this.setOutput(true, null);
    this.setColour(120);
 this.setTooltip("表达式");
 this.setHelpUrl("");
  }
};

Blockly.Python['py_data'] = function(block) {
  var text_date_content = block.getFieldValue('date_content');
  // TODO: Assemble Python into code variable.
  var code = text_date_content;
  // TODO: Change ORDER_NONE to the correct strength.
  return [code];
};


Blockly.Blocks['py_assign'] = {
  init: function() {
    this.appendValueInput("val")
        .setCheck(null)
        .appendField(new Blockly.FieldTextInput("n"), "assign_val")
        .appendField("=");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(120);
 this.setTooltip("变量赋值");
 this.setHelpUrl("");
  }
};

Blockly.Python['py_assign'] = function(block) {
  var text_assign_val = block.getFieldValue('assign_val');
  var value_val = Blockly.Python.valueToCode(block, 'val', Blockly.Python.ORDER_ATOMIC);
  // TODO: Assemble Python into code variable.
  var code = `${text_assign_val} = ${value_val}\n`;
  return code;
};

Blockly.Blocks['py_input'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("input")
        .appendField(new Blockly.FieldTextInput("请输入："), "input_tip");
    this.setOutput(true, "String");
    this.setColour(20);
 this.setTooltip("输入");
 this.setHelpUrl("");
  }
};

Blockly.Python['py_input'] = function(block) {
  var text_input_tip  = block.getFieldValue('input_tip');
  // TODO: Assemble Python into code variable.
  var code =`input("${text_input_tip}")`;
  // TODO: Change ORDER_NONE to the correct strength.
  return [code];
};


Blockly.Blocks['py_print'] = {
  init: function() {
    this.appendValueInput("print")
        .setCheck(null)
        .appendField("print");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(20);
 this.setTooltip("输出");
 this.setHelpUrl("");
  }
};

Blockly.Python['py_print'] = function(block) {
  var value_print = Blockly.Python.valueToCode(block, 'print', Blockly.Python.ORDER_ATOMIC);
  // TODO: Assemble Python into code variable.
  var code = `print(${value_print})\n`;
  return code;
};

Blockly.Blocks['py_start'] = {
  init: function() {
    this.appendDummyInput()
		.appendField(new Blockly.FieldImage("/images/python20.png", 18, 18, { alt: "*", flipRtl: "FALSE" }))
        .appendField("Python 编程开始");
    this.setNextStatement(true, null);
    this.setColour(210);
 this.setTooltip("编程开始");
 this.setHelpUrl("");
  }
};

Blockly.Python['py_start'] = function(block) {
  // TODO: Assemble Python into code variable.
  var date=new Date();
  var code = '# 编程开始\n';
  return code;
};

Blockly.Blocks['py_range'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("range")
        .appendField(new Blockly.FieldTextInput("0"), "start")
        .appendField(new Blockly.FieldTextInput("4"), "end")
        .appendField(new Blockly.FieldTextInput("1"), "step");
    this.setOutput(true, null);
    this.setColour(120);
 this.setTooltip("数字序列 范围(起始值,终值,步长)，不包含终值");
 this.setHelpUrl("");
  }
};

Blockly.Python['py_range'] = function(block) {
  var text_start = block.getFieldValue('start');
  var text_end = block.getFieldValue('end');
  var text_step = block.getFieldValue('step');
  // TODO: Assemble Python into code variable.
  var code = `range(${text_start},${text_end},${text_step})`;
  /*
  if(text_step=="1"){
	  code = `range(${text_start},${text_end})`;	
  }
  if(text_step=="1" && text_start=="0"){
	  code = `range(${text_end})`;	
  }
  */
  // TODO: Change ORDER_NONE to the correct strength.
  return [code];
};

Blockly.Blocks['py_for'] = {
  init: function() {
    this.appendValueInput("py_condition")
        .setCheck(null)
        .appendField("for ")
        .appendField(new Blockly.FieldTextInput("i"), "for_val")
        .appendField(" in");
    this.appendStatementInput("py_do")
        .setCheck(null);
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(60);
 this.setTooltip("for循环");
 this.setHelpUrl("");
  }
};


Blockly.Python['py_for'] = function(block) {
  var text_for_val = block.getFieldValue('for_val');
  var value_py_condition = Blockly.Python.valueToCode(block, 'py_condition', Blockly.Python.ORDER_ATOMIC);
  var statements_py_do = Blockly.Python.statementToCode(block, 'py_do');
  // TODO: Assemble Python into code variable.
  var code = `for ${text_for_val} in ${value_py_condition}:\n${statements_py_do}`;
  return code;
};

Blockly.Blocks['py_while'] = {
  init: function() {
    this.appendValueInput("py_condition")
        .setCheck(null)
        .appendField("while");
    this.appendStatementInput("py_do")
        .setCheck(null);
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(60);
 this.setTooltip("while循环");
 this.setHelpUrl("");
  }
};

Blockly.Python['py_while'] = function(block) {
  var value_py_condition = Blockly.Python.valueToCode(block, 'py_condition', Blockly.Python.ORDER_ATOMIC);
  var statements_py_do = Blockly.Python.statementToCode(block, 'py_do');
  // TODO: Assemble Python into code variable.
  var code = `while ${value_py_condition}:\n${statements_py_do}`;
  return code;
};

Blockly.Blocks['py_break'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("break");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(60);
 this.setTooltip("终止当前循环");
 this.setHelpUrl("");
  }
};

Blockly.Python['py_break'] = function(block) {
  // TODO: Assemble Python into code variable.
  var code = 'break\n';
  return code;
};
