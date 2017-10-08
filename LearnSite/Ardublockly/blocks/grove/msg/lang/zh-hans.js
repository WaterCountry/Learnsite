/**
 * @license Licensed under the Apache License, Version 2.0 (the "License"):
 *          http://www.apache.org/licenses/LICENSE-2.0
 */

/**
 * @fileoverview English strings for Grove module blocks. All names have the
 *     postfix BLOCKS_GROVE.
 */
'use strict';

goog.provide('Blockly.Msg.zh.hans');

goog.require('Blockly.Msg');
/**
 * Due to the frequency of long strings, the 80-column wrap rule need not apply
 * to message files.
 */

/// Toolbox category name
Blockly.Msg.BLOCKS_GROVE_CATEGORY = 'Grove';

/// LED block
Blockly.Msg.BLOCKS_GROVE_LED = '设置Grove LED灯';
Blockly.Msg.BLOCKS_GROVE_LED_TIP = 'Turns the LED On (HIGH) or Off (LOW).';

/// Button block
Blockly.Msg.BLOCKS_GROVE_BUTTON = '读取Grove按钮状态';
Blockly.Msg.BLOCKS_GROVE_BUTTON_TIP = 'Set to HIGH when the button is pressed, otherwise LOW.';

/// Joystick block
Blockly.Msg.BLOCKS_GROVE_JOYSTICK = '读取Grove游戏杆位置';
Blockly.Msg.BLOCKS_GROVE_JOYSTICK_2 = '连接器上';
Blockly.Msg.BLOCKS_GROVE_JOYSTICK_TIP = 'Reads the joystick position value from 200-800.';

/// PIR block
Blockly.Msg.BLOCKS_GROVE_PIR = '读取Grove PIR状态';
Blockly.Msg.BLOCKS_GROVE_PIR_TIP = 'On motion sense it outputs HIGH, otherwise LOW.';

/// Temperature block
Blockly.Msg.BLOCKS_GROVE_TEMPERATURE = '读取Grove温度传感器';
Blockly.Msg.BLOCKS_GROVE_TEMPERATURE_TIP = 'Returns the temperate in 潞C.';

/// LCD RGB block
Blockly.Msg.BLOCKS_GROVE_LCD_RGB = '设置Grove LCD RGB 文字';
Blockly.Msg.BLOCKS_GROVE_LCD_RGB_TIP = 'Sets the text on the LCD display.';
