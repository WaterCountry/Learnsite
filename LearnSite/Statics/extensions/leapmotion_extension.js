/*
 *This program is free software: you can redistribute it and/or modify
 *it under the terms of the GNU General Public License as published by
 *the Free Software Foundation, either version 3 of the License, or
 *(at your option) any later version.
 *
 *This program is distributed in the hope that it will be useful,
 *but WITHOUT ANY WARRANTY; without even the implied warranty of
 *MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *GNU General Public License for more details.
 *
 *You should have received a copy of the GNU General Public License
 *along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

(function (ext) {

  var X_SCALE = 2;
  var Y_SCALE = 1.8;
  var Z_SCALE = 2;

  var controller = null;
  var activeHands = null;
  var activeTools = null;
  var activeGestures = {
    'keyTap': {active: false, timer: null},
    'screenTap': {active: false, timer: null},
    'swipe': {active: false, timer: null},
    'circle': {active: false, timer: null}
  };
  var handIds = {
    'hand A': null,
    'hand B': null
  };
  var fingers = {
    'finger 1': 0,
    'finger 2': 1,
    'finger 3': 2,
    'finger 4': 3,
    'finger 5': 4
  };
  var toolIds = {
    'tool A': null,
    'tool B': null
  };
  var gestures = {
    'tap': 'keyTap',
    'poke': 'screenTap',
    'swipe': 'swipe',
    'circle': 'circle'
  };

  function loadLeapJS() {
    $.getScript('http://khanning.github.io/scratch-leapmotion-extension/leap-0.6.4.js')
      .done(function(script, textStatus) {
        console.log('Loaded LeapJS');
        loadLeapJSPlugins();
      })
      .fail(function(jqxhr, settings, exception) {
        console.log('Error loading LeapJS');
        loadLeapJS();
    });
  }

  function loadLeapJSPlugins() {
    $.getScript('http://khanning.github.io/scratch-leapmotion-extension/leap-plugins-0.1.10.js')
      .done(function(script, textStatus) {
        console.log('Loaded LeapJS Plugins');
        begin();
      })
      .fail(function(jqxhr, settings, exception) {
        console.log('Error loading LeapJS');
        loadLeapJSPlugins();
    });
  }

  function begin() {
    controller = Leap.loop({enableGestures: true}, function(frame) {
      updateHands(frame.hands);
      updateTools(frame.tools);
      if (frame.gestures.length > 0) {
        frame.gestures.forEach(function(name) {
          var gesture = activeGestures[name.type];
          gesture.active = true;
          if (gesture.timer) clearTimeout(gesture.timer);
          gesture.timer = setTimeout(function() {
            gesture.active = false;
          }, 200);
        });
      }
    });
    controller.on('deviceStreaming', function() {
      connected = true;
    });
    controller.on('deviceRemoved', function() {
      connected = false;
    });
  }

  function updateHands(hands) {
    activeHands = hands;
    
    // Check if hands are still active
    if (hands.length == 0) {
      handIds['hand A'] = null;
      handIds['hand B'] = null;
      return;
    } else {
      if (!isValid(handIds['hand A']))
        handIds['hand A'] = null;
      if (!isValid(handIds['hand B']))
        handIds['hand B'] = null;
    }
    
    // Assign a hand
    if (!handIds['hand A'] && !handIds['hand B']) {
      handIds['hand A'] = hands[0].id;
    } else if (!handIds['hand A']) {
      for (var i=0; i<hands.length; i++) {
        if (hands[i].id != handIds['hand B'])
          handIds['hand A'] = hands[i].id;
      }
    } else if (!handIds['hand B']) {
      for (var i=0; i<hands.length; i++) {
        if (hands[i].id != handIds['hand A'])
          handIds['hand B'] = hands[i].id;
      }
    }
  }

  function updateTools(tools) {
    activeTools = tools;
    
    // Check if hands are still active
    if (tools.length == 0) {
      toolIds['tool A'] = null;
      toolIds['tool B'] = null;
      return;
    } else {
      if (!isValidTool(toolIds['tool A']))
        toolIds['tool A'] = null;
      if (!isValidTool(toolIds['tool B']))
        toolIds['tool B'] = null;
    }
    
    // Assign tools
    if (!toolIds['tool A'] && !toolIds['tool B']) {
      toolIds['tool A'] = tools[0].id;
    } else if (!toolIds['tool A']) {
      for (var i=0; i<tools.length; i++) {
        if (tools[i].id != toolIds['tool B'])
          toolIds['tool A'] = tools[i].id;
      }
    } else if (!toolIds['tool B']) {
      for (var i=0; i<tools.length; i++) {
        if (tools[i].id != toolIds['tool A'])
          toolIds['tool B'] = tools[i].id;
      }
    }
  }

  function isValid(hand) {
    for (var i=0; i<activeHands.length; i++) {
      if (hand === activeHands[i].id)
        return true;
    }
    return false;
  }

  function isValidTool(tool) {
    for (var i=0; i<activeTools.length; i++) {
      if (tool === activeTools[i].id)
        return true;
    }
    return false;
  }

  function getHand(name) {
    if (!handIds[name]) return null;
    for (var i = 0; i < activeHands.length; i++) {
      if (activeHands[i].id === handIds[name])
        return activeHands[i];
    }
    return null;
  }

  function getFinger(handName, name) {
    var hand = getHand(handName);
    if (!hand) return null;
    var num = fingers[name];
    if (num > hand.fingers.length) return null;
    return hand.fingers[num];
  }
  
  function getTool(name) {
    if (!toolIds[name]) return null;
    for (var i = 0; i < activeTools.length; i++) {
      if (activeTools[i].id === toolIds[name])
        return activeTools[i];
    }
    return null;
  }

  ext.getHandX = function(name) {
    var hand = getHand(name);
    if (!hand) return null;
    return Math.round(hand.stabilizedPalmPosition[0] * X_SCALE);
  };

  ext.getHandY = function(name) {
    var hand = getHand(name);
    if (!hand) return null;
    return Math.round((hand.stabilizedPalmPosition[1] * Y_SCALE) - 400);
  };
  
  ext.getHandZ = function(name) {
    var hand = getHand(name);
    if (!hand) return null;
    return Math.round(hand.stabilizedPalmPosition[2] * Z_SCALE);
  };

  ext.getHandRotation = function(name) {
    var hand = getHand(name);
    if (!hand) return null;
    return Math.round(hand.roll() * -100);
  };

  ext.isHandVisible = function(name) {
    var hand = getHand(name);
    if (!hand) return false;
    return true;
  };

  ext.isHandGrabbed = function(name) {
    var hand = getHand(name);
    if (!hand) return null;
    return hand.grabStrength > 0.9;
  };
  
  ext.getFingerX = function(hand, name) {
    var finger = getFinger(hand, name);
    if (!finger) return null;
    return Math.round(finger.stabilizedTipPosition[0] * X_SCALE);
  };

  ext.getFingerY = function(hand, name) {
    var finger = getFinger(hand, name);
    if (!finger) return null;
    return Math.round((finger.stabilizedTipPosition[1] * Y_SCALE) - 300);
  };
  
  ext.getFingerZ = function(hand, name) {
    var finger = getFinger(hand, name);
    if (!finger) return null;
    return Math.round(finger.stabilizedTipPosition[2] * Z_SCALE);
  };

  ext.isFingerExtended = function(hand, name) {
    var finger = getFinger(hand, name);
    if (!finger) return null;
    return finger.extended;
  };

  ext.getToolX = function(name) {
    var tool = getTool(name);
    if (!tool) return null;
    return Math.round(tool.tipPosition[0] * X_SCALE);
  };

  ext.getToolY = function(name) {
    var tool = getTool(name);
    if (!tool) return null;
    return Math.round((tool.tipPosition[1] * Y_SCALE) - 300);
  };

  ext.getToolZ = function(name) {
    var tool = getTool(name);
    if (!tool) return null;
    return Math.round(tool.stabilizedTipPosition[2] * Z_SCALE);
  };

  ext.isToolVisible = function(name) {
    var tool = getTool(name);
    if (!tool) return false;
    return true;
  };

  ext.whenGesture = function(name) {
    return activeGestures[gestures[name]].active;
  };

  ext.waitForGesture = function(name, callback) {
    var poller = setInterval(function() {
      if (activeGestures[gestures[name]].active) {
        clearInterval(poller);
        poller = null;
        callback();
      }
    }, 200);
  };

  ext._getStatus = function() {
    if (!connected)
      return { status:1, msg:'Disconnected' };
    else
      return { status:2, msg:'Connected' };
  };

  ext._shutdown = function() {
    controller = null;
    connected = false;
  };

  var descriptor = {
    blocks: [
      ['r', '%m.hands x position', 'getHandX', 'hand A'],
      ['r', '%m.hands y position', 'getHandY', 'hand A'],
      ['r', '%m.hands z position', 'getHandZ', 'hand A'],
      ['r', '%m.hands rotation', 'getHandRotation', 'hand A'],
      ['b', '%m.hands is visible?', 'isHandVisible', 'hand A'],
      ['b', '%m.hands is closed?', 'isHandGrabbed', 'hand A'],
      ['-'],
      ['r', '%m.hands %m.fingers x position', 'getFingerX', 'hand A', 'finger 1'],
      ['r', '%m.hands %m.fingers y position', 'getFingerY', 'hand A', 'finger 1'],
      ['r', '%m.hands %m.fingers z position', 'getFingerZ', 'hand A', 'finger 1'],
      ['b', '%m.hands %m.fingers is extended?', 'isFingerExtended', 'hand A', 'finger 1'],
      ['-'],
      ['r', '%m.tools x position', 'getToolX', 'tool A'],
      ['r', '%m.tools y position', 'getToolY', 'tool A'],
      ['r', '%m.tools z position', 'getToolZ', 'tool A'],
      ['b', '%m.tools is visible?', 'isToolVisible', 'tool A'],
      ['-'],
      ['h', 'when %m.gestures', 'whenGesture', 'tap'],
      ['w', 'wait until %m.gestures', 'waitForGesture', 'tap']
    ],  
    menus: {
      hands: ['hand A', 'hand B'],
      fingers: ['finger 1', 'finger 2', 'finger 3', 'finger 4', 'finger 5'],
      tools: ['tool A', 'tool B'],
      gestures: ['tap', 'poke', 'swipe', 'circle']
    },  
    url: 'http://khanning.github.io/scratch-leapmotion-extension'
  };

  ScratchExtensions.register('Leap Motion', descriptor, ext);
  loadLeapJS();

})({});
