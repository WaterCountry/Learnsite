// export and import
/* 
https://developers.google.com/blockly/guides/get-started/web#importing_and_exporting_blocks

var xml = Blockly.Xml.workspaceToDom(demoWorkspace);
var xml_text = Blockly.Xml.domToText(xml);


var xml = Blockly.Xml.textToDom(xml_text);
Blockly.Xml.domToWorkspace(xml, demoWorkspace);

*/

var xml_multiplechoiceresponse_demo =
  '<xml xmlns="http://www.w3.org/1999/xhtml"><variables><variable type="" id="*!F@5g2SzGhMDCDOId;o">WACC</variable><variable type="" id="yy=xKh_EU9O*KgU3FM+1">g</variable><variable type="" id="d46t9(3KnRJ1uo-6oBb|">result</variable><variable type="" id="Iu~G`_VuSTYr*xTCpr-9">wrong_result_1</variable><variable type="" id="Yr+Q-4McXU^5V!kYsxr$">wrong_result_2</variable><variable type="" id="$/Wau/c;!R#@gdV^)1{h">wrong_result_3</variable><variable type="" id="~u}Esl1+|R[`0Swsk5xt">fcf_1</variable></variables><block type="problem" id="/(q*nkRpywQGlE,H+9lQ" x="96" y="81"><statement name="python_script"><block type="variables_set" id="w*5UsKEzLae4Z+B2UvBM"><field name="VAR" id="~u}Esl1+|R[`0Swsk5xt" variabletype="">fcf_1</field><value name="VALUE"><block type="math_number" id="i7m54s_1^u9j/3Lb=xB}"><field name="NUM">100000</field></block></value><next><block type="variables_set" id=",:70:C!4)7w_Uukgu-cO"><field name="VAR" id="*!F@5g2SzGhMDCDOId;o" variabletype="">WACC</field><value name="VALUE"><block type="math_random_int" id="C*A*W0?D:IJONGcHQq]L"><value name="FROM"><shadow type="math_number" id="%T{kfj^#X?@@Qb`d)*(?"><field name="NUM">8</field></shadow></value><value name="TO"><shadow type="math_number" id="7hROz]@S2dRWqnhI+L?%"><field name="NUM">12</field></shadow></value></block></value><next><block type="variables_set" id=")KdWN_XInMXf)=P3EEC}"><field name="VAR" id="yy=xKh_EU9O*KgU3FM+1" variabletype="">g</field><value name="VALUE"><block type="math_number" id="?Mf_8{n/bT?e.VDP^6#!"><field name="NUM">6</field></block></value><next><block type="variables_set" id="t1s$0-25jHG(1bCz,Y{}"><field name="VAR" id="d46t9(3KnRJ1uo-6oBb|" variabletype="">result</field><value name="VALUE"><block type="format_number" id="yywA[*6_/XP|(;c~dc3G"><value name="number"><block type="math_arithmetic" id="3chY@lP#~wjEj^e0:WQ_"><field name="OP">DIVIDE</field><value name="A"><shadow type="math_number" id="$dn}BtUXj+3TXLAhAW=."><field name="NUM">1</field></shadow><block type="variables_get" id="($*TUj=YcZ#IXwv#ps=q"><field name="VAR" id="~u}Esl1+|R[`0Swsk5xt" variabletype="">fcf_1</field></block></value><value name="B"><shadow type="math_number" id="-#KvMLyohZx`1yeyrYz9"><field name="NUM">1</field></shadow><block type="math_arithmetic" id="UDEuI_X0M/MAQgWHmecL"><field name="OP">DIVIDE</field><value name="A"><shadow type="math_number" id="e#8-}lNuiWHC^+5u@1R{"><field name="NUM">1</field></shadow><block type="math_arithmetic" id="9;(*r%yT1Qzra]akkXim"><field name="OP">MINUS</field><value name="A"><shadow type="math_number" id="NokZx7:eYIpUh44bW6;y"><field name="NUM">1</field></shadow><block type="variables_get" id="q=aJ[P|#Cz2(XPih~-aJ"><field name="VAR" id="*!F@5g2SzGhMDCDOId;o" variabletype="">WACC</field></block></value><value name="B"><shadow type="math_number" id="@EkP9e)#GL[V-Pfk;-!g"><field name="NUM">1</field></shadow><block type="variables_get" id="3a8B6bD^W.xwM#/Cc7.L"><field name="VAR" id="yy=xKh_EU9O*KgU3FM+1" variabletype="">g</field></block></value></block></value><value name="B"><shadow type="math_number" id="nhFS[VBZU=4+Ai!h1UvS"><field name="NUM">100</field></shadow></value></block></value></block></value></block></value><next><block type="variables_set" id="=RQ`/f+vLBJ@ub7B=9zk"><field name="VAR" id="Iu~G`_VuSTYr*xTCpr-9" variabletype="">wrong_result_1</field><value name="VALUE"><block type="format_number" id="J=9^TnYgMlU@_o3A+Yaa"><value name="number"><block type="math_arithmetic" id="HxWo@6r4.kX}hY-pVgXy"><field name="OP">ADD</field><value name="A"><shadow type="math_number" id="vUH.l_y|Q[(lU$Ba+yz/"><field name="NUM">1</field></shadow><block type="math_arithmetic" id="`$+g6G5oN5#4NfK0G^B9"><field name="OP">DIVIDE</field><value name="A"><shadow type="math_number" id="$dn}BtUXj+3TXLAhAW=."><field name="NUM">1</field></shadow><block type="variables_get" id="=cRqUwNM_#P.IOC@hZr%"><field name="VAR" id="~u}Esl1+|R[`0Swsk5xt" variabletype="">fcf_1</field></block></value><value name="B"><shadow type="math_number" id="-#KvMLyohZx`1yeyrYz9"><field name="NUM">1</field></shadow><block type="math_arithmetic" id="`+QlmGBYnn((k=D44az9"><field name="OP">DIVIDE</field><value name="A"><shadow type="math_number" id="e#8-}lNuiWHC^+5u@1R{"><field name="NUM">1</field></shadow><block type="math_arithmetic" id=":s_7M.2_aryd)#VZnreA"><field name="OP">MINUS</field><value name="A"><shadow type="math_number" id="NokZx7:eYIpUh44bW6;y"><field name="NUM">1</field></shadow><block type="variables_get" id="y(?@!3nbpZ7=L62{.(6r"><field name="VAR" id="*!F@5g2SzGhMDCDOId;o" variabletype="">WACC</field></block></value><value name="B"><shadow type="math_number" id="@EkP9e)#GL[V-Pfk;-!g"><field name="NUM">1</field></shadow><block type="variables_get" id="v:4+ml=Iy|A5AL9v](V;"><field name="VAR" id="yy=xKh_EU9O*KgU3FM+1" variabletype="">g</field></block></value></block></value><value name="B"><shadow type="math_number" id="`x9iP,;YNnFs]~h|vP}]"><field name="NUM">100</field></shadow></value></block></value></block></value><value name="B"><shadow type="math_number" id="q*q8Awj(upnDsYSLDDEf"><field name="NUM">100000</field></shadow></value></block></value></block></value><next><block type="variables_set" id="0:#x;|(Np|O$1?I~XPB,"><field name="VAR" id="Yr+Q-4McXU^5V!kYsxr$" variabletype="">wrong_result_2</field><value name="VALUE"><block type="format_number" id="z~E$]?(Q.+f5@r*HoY[w"><value name="number"><block type="math_arithmetic" id="hrp5:`C%QGPQpL87ZL5$"><field name="OP">ADD</field><value name="A"><shadow type="math_number" id="vUH.l_y|Q[(lU$Ba+yz/"><field name="NUM">1</field></shadow><block type="math_arithmetic" id="[t_~RNou,}$?Sn{aaQ|P"><field name="OP">DIVIDE</field><value name="A"><shadow type="math_number" id="$dn}BtUXj+3TXLAhAW=."><field name="NUM">1</field></shadow><block type="variables_get" id="CpYJM#QPIeugzSlhKuB~"><field name="VAR" id="~u}Esl1+|R[`0Swsk5xt" variabletype="">fcf_1</field></block></value><value name="B"><shadow type="math_number" id="-#KvMLyohZx`1yeyrYz9"><field name="NUM">1</field></shadow><block type="math_arithmetic" id="6w$T*462m2q:uhVC;YPm"><field name="OP">DIVIDE</field><value name="A"><shadow type="math_number" id="e#8-}lNuiWHC^+5u@1R{"><field name="NUM">1</field></shadow><block type="math_arithmetic" id="R6D0+GqY.u-Kk_}EQ71r"><field name="OP">MINUS</field><value name="A"><shadow type="math_number" id="NokZx7:eYIpUh44bW6;y"><field name="NUM">1</field></shadow><block type="variables_get" id="nmzGNXEvCu_VFfPN3d]0"><field name="VAR" id="*!F@5g2SzGhMDCDOId;o" variabletype="">WACC</field></block></value><value name="B"><shadow type="math_number" id="@EkP9e)#GL[V-Pfk;-!g"><field name="NUM">1</field></shadow><block type="variables_get" id="_O@n3]ChhKL*~37hiaje"><field name="VAR" id="yy=xKh_EU9O*KgU3FM+1" variabletype="">g</field></block></value></block></value><value name="B"><shadow type="math_number" id="/)/e=bHTlpi_PTjKqmZz"><field name="NUM">100</field></shadow></value></block></value></block></value><value name="B"><shadow type="math_number" id="IJTcz*b5b_DkfU8%K.,Z"><field name="NUM">200000</field></shadow></value></block></value></block></value><next><block type="variables_set" id="H`E(k2XUtR$^L}AsDaD#"><field name="VAR" id="$/Wau/c;!R#@gdV^)1{h" variabletype="">wrong_result_3</field><value name="VALUE"><block type="format_number" id="rBEJ.8q=Q.y`(?-il4pc"><value name="number"><block type="math_arithmetic" id="oJ}.VT%d.5zS7BZNc+.n"><field name="OP">MINUS</field><value name="A"><shadow type="math_number" id="vUH.l_y|Q[(lU$Ba+yz/"><field name="NUM">1</field></shadow><block type="math_arithmetic" id="C9:-gTJaD8C@:?SZa1MH"><field name="OP">DIVIDE</field><value name="A"><shadow type="math_number" id="$dn}BtUXj+3TXLAhAW=."><field name="NUM">1</field></shadow><block type="variables_get" id="F|T,tZ`_J5meyj6HtMN)"><field name="VAR" id="~u}Esl1+|R[`0Swsk5xt" variabletype="">fcf_1</field></block></value><value name="B"><shadow type="math_number" id="-#KvMLyohZx`1yeyrYz9"><field name="NUM">1</field></shadow><block type="math_arithmetic" id="n8Rl~9]w/8Jc*(uVnMz4"><field name="OP">DIVIDE</field><value name="A"><shadow type="math_number" id="e#8-}lNuiWHC^+5u@1R{"><field name="NUM">1</field></shadow><block type="math_arithmetic" id="ih7PFZLBV/WVmy:LUsbx"><field name="OP">MINUS</field><value name="A"><shadow type="math_number" id="NokZx7:eYIpUh44bW6;y"><field name="NUM">1</field></shadow><block type="variables_get" id="2A;Y~`X$].geWsLXuZKn"><field name="VAR" id="*!F@5g2SzGhMDCDOId;o" variabletype="">WACC</field></block></value><value name="B"><shadow type="math_number" id="@EkP9e)#GL[V-Pfk;-!g"><field name="NUM">1</field></shadow><block type="variables_get" id="EMU+2Y+/[W|NUbt=Aej7"><field name="VAR" id="yy=xKh_EU9O*KgU3FM+1" variabletype="">g</field></block></value></block></value><value name="B"><shadow type="math_number" id="Mp`j_{wbx@`H~Ta?f`b#"><field name="NUM">100</field></shadow></value></block></value></block></value><value name="B"><shadow type="math_number" id="W@P4KVLF@Ios8w6,I1P/"><field name="NUM">100000</field></shadow></value></block></value></block></value></block></next></block></next></block></next></block></next></block></next></block></next></block></statement><statement name="problem_text"><block type="html_p" id="UIwyMyX]HxK0]zowZUTp"><field name="p">假设XYZ公司预计明年的自由现金流为(FCF_1) = 100,000美元，预计FCF将以6％的固定增长率增长。如果公司的加权平均资本成本是$WACC%，那么公司总价值是多少？</field></block></statement><statement name="response"><block type="multiplechoiceresponse" id="j5M]HDJDue*@q+VI.:j6"><statement name="statement"><block type="choice" id="QW/eh4;4sD|@CRkr~uNM"><field name="correct">false</field><field name="text">$$wrong_result_1</field><next><block type="choice" id="#K]TC^0{p]{UQFf?l5rN"><field name="correct">true</field><field name="text">$$result</field><next><block type="choice" id="pMgM_n28E`b`4qAs2k;a"><field name="correct">false</field><field name="text">$$wrong_result_2</field><next><block type="choice" id=".fOK-aOK{pf8EQJCnoP$"><field name="correct">false</field><field name="text">$$wrong_result_3</field></block></next></block></next></block></next></block></statement></block></statement><statement name="solution"><block type="html_p" id="q#N2PR+hm@|l[Z3oJw?:"><field name="p">解析</field><next><block type="html_p" id="m*!i8D0QKas(KQH0Jye`"><field name="p">公司总价值 = (FCF_1)/(WACC – g) = $100,000/($WACC% - 6%) = $$result</field></block></next></block></statement></block></xml>';

/////////////////////////
var xml_numericalresponse_demo =
  '<xml xmlns="http://www.w3.org/1999/xhtml"><variables><variable type="" id="J*8PIv7]8+SmDzonG)RW">x1</variable><variable type="" id="[6f9B*e|4hhJjf|TX6PU">x2</variable></variables><block type="problem" id="Ztc30yg/)Fy5Sn0M.gWO" x="109" y="30"><statement name="python_script"><block type="variables_set" id=".Kuz{?JW114Q*p)4@|^M"><field name="VAR" id="J*8PIv7]8+SmDzonG)RW" variabletype="">x1</field><value name="VALUE"><block type="math_random_int" id="otonNOl$BBYXx`3UFUor"><value name="FROM"><shadow type="math_number" id="F{FtxeXK9]z{~{dQf6;o"><field name="NUM">1</field></shadow></value><value name="TO"><shadow type="math_number" id="o3OD=-_rr@BaPyBUk._|"><field name="NUM">100</field></shadow></value></block></value><next><block type="variables_set" id="u6}nlK|4u1LJH.~N$V/z"><field name="VAR" id="[6f9B*e|4hhJjf|TX6PU" variabletype="">x2</field><value name="VALUE"><block type="math_random_int" id="!r]rT3~C!Mlh_a)h:]w*"><value name="FROM"><shadow type="math_number" id="1M/f$d9i!FL_uL=j7[Hz"><field name="NUM">1</field></shadow></value><value name="TO"><shadow type="math_number" id="~g_{el//+v+ikk33lD*6"><field name="NUM">100</field></shadow></value></block></value></block></next></block></statement><statement name="problem_text"><block type="html_p" id="hJmE,?`H]q.+%Cr}+P2c"><field name="p">Some problems in the course will utilize randomized parameters.      For such problems, after you check your answer you will have the option      of resetting the question, which reconstructs the problem with a new      set of parameters.</field><next><block type="html_p" id="CHnTHZfc:hOA%PuXE!FI"><field name="p">Let (x_1 = $x1) and (x_2 = $x2). What is the value of (x_1+x_2)?</field></block></next></block></statement><statement name="response"><block type="numericalresponse" id="}YZ{3tX-1y98l%M4|[KA"><field name="answer">$y</field><field name="type">tolerance</field><field name="default1">0.01%</field><field name="textline_size">10</field></block></statement><statement name="solution"><block type="html_p" id="B%c$/P?_0A:8}X@:1.{S"><field name="p">hello</field></block></statement></block></xml>';

/////////////////////////

/////////////////////////
