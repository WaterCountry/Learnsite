var Ardublockly = Ardublockly || {};
Ardublockly.LOCALISED_TEXT = {
  translationLanguage: "Chinese",
  title: "ArduBlockly中文",
  blocks: "程序",
  /* Menu */
  upload:"保存xml文档",
  open: "打开xml文档",
  save: "下载ino代码",
  deleteAll: "删除全部代码",
  settings: "设置",
  documentation: "说明文档",
  reportBug: "问题反馈",
  examples: "示例",
  /* Settings */
  compilerLocation: "编译位置",
  compilerLocationDefault: "未知编译位置",
  sketchFolder: "文档目录",
  sketchFolderDefault: "未知文档目录",
  arduinoBoard: "Arduino板",
  arduinoBoardDefault: "未知 Arduino 板",
  comPort: "COM串口",
  comPortDefault: "COM未知串口",
  defaultIdeButton: "默认IDE 按钮",
  defaultIdeButtonDefault: "未知 IDE 选项",
  language: "语言",
  languageDefault: "未知语言",
  sketchName: "文档名",
  /* Arduino console output */
  arduinoOpMainTitle: "Arduino IDE output",
  arduinoOpWaiting: "Waiting for the IDE output...",
  arduinoOpUploadedTitle: "上传成功",
  arduinoOpVerifiedTitle: "验证通过",
  arduinoOpOpenedTitle: "使用IDE打开",
  arduinoOpOpenedBody: "代码不能加载到Arduino IDE里.",
  arduinoOpErrorUpVerTitle: "编译或上传失败！",
  arduinoOpErrorSketchTitle: "找不到程序代码",
  arduinoOpErrorFlagTitle: "无效的命令行参数",
  arduinoOpErrorFlagPrefTitle: "偏好'get-pref' 标志不存在",
  arduinoOpErrorIdeDirTitle: "找不到 Arduino IDE",
  arduinoOpErrorIdeDirBody: "Arduino编译器目录未设置.<br>" +
                            "请在配置中设置.",
  arduinoOpErrorIdeOptionTitle: "我们如何编写程序？",
  arduinoOpErrorIdeOptionBody: "IDE启动选项未设置.<br>" +
                               "请在配置里选择IDE启动选项.",
  arduinoOpErrorIdePortTitle: "串口无法使用",
  arduinoOpErrorIdePortBody: "这个串口无法使用.<br>" +
                             "请检查Arduino板，并选择正确的串口.",
  arduinoOpErrorIdeBoardTitle: "未知 Arduino板",
  arduinoOpErrorIdeBoardBody: "Arduino板未选择.<br>" +
                              "请选择合适的Arduino板.",
  /* Modals */
  noServerTitle: "Ardublockly 应用没有运行",
  noServerBody: "Ardublockly中文在线版",
  noServerBodyx: "<p>如果要所有Ardublockly功能都启用, Ardublockly桌面应用必须在本地计算机上运行.</p>" +
                     "<p>如果您在使用在线版，那么将不能设置应用参数，以及不能上传代码到Arduino板里.</p>" +
                     "<p>安装说明请到这里查看 <a href=\"https://github.com/carlosperate/ardublockly\">Ardublockly repository</a>.</p>" +
                     "<p>如果您已经安装了Ardublockly, 请确认Ardublockly应用运行.</p>",
  noServerNoLangBody: "如果Ardublockly应用未运行，那么语言选择显示不完整.",
  addBlocksTitle: "附加代码",
  /* Alerts */
  loadNewBlocksTitle: "加载新代码?",
  loadNewBlocksBody: "加载新的XML文档，将会覆盖当前工作区里的代码.<br>" +
                     "您确定要继续吗?",
  discardBlocksTitle: "删除代码",
  discardBlocksBody: "有 %1 块代码在工作区.<br>" +
                     "您确定要删除它们吗?",
  invalidXmlTitle: "无效XML格式",
  invalidXmlBody: "XML文档不能加载到代码框里. 请查看XML文档代码并重试.",
  /* Tooltips */
  uploadingSketch: "正在将代码上传到Arduino板...",
  uploadSketch: "上传代码到Arduino板",
  verifyingSketch: "编译验证中...",
  verifySketch: "编译验证",
  openingSketch: "正在Arduino IDE中打开...",
  openSketch: "使用Arduino IDE打开",
  notImplemented: "功能尚未实现",
  /* Prompts */
  ok: "好的",
  okay: "确定",
  cancel: "取消",
  return: "返回",
  /* Cards */
  arduinoSourceCode: "Arduino源代码",
  blocksXml: "XML文档",
  /* Toolbox Categories*/
  catLogic: "逻辑",
  catLoops: "循环",
  catMath: "数学",
  catText: "文本",
  catVariables: "变量",
  catFunctions: "函数",
  catInputOutput: "读写",
  catTime: "时间",
  catAudio: "声音",
  catMotors: "伺服",
  catComms: "通信",
};
