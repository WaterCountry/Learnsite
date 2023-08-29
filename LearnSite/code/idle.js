(function () {
    var 
        importre = new RegExp("\\s*import"),
        
        defre = new RegExp("def.*|class.*"),
        
        comment = new RegExp("^#.*"),
        
        
        
        
        assignment = /^((\s*\(\s*(\s*((\s*((\s*[_a-zA-Z]\w*\s*)|(\s*\(\s*(\s*[_a-zA-Z]\w*\s*,)*\s*[_a-zA-Z]\w*\s*\)\s*))\s*)|(\s*\(\s*(\s*((\s*[_a-zA-Z]\w*\s*)|(\s*\(\s*(\s*[_a-zA-Z]\w*\s*,)*\s*[_a-zA-Z]\w*\s*\)\s*))\s*,)*\s*((\s*[_a-zA-Z]\w*\s*)|(\s*\(\s*(\s*[_a-zA-Z]\w*\s*,)*\s*[_a-zA-Z]\w*\s*\)\s*))\s*\)\s*))\s*,)*\s*((\s*((\s*[_a-zA-Z]\w*\s*)|(\s*\(\s*(\s*[_a-zA-Z]\w*\s*,)*\s*[_a-zA-Z]\w*\s*\)\s*))\s*)|(\s*\(\s*(\s*((\s*[_a-zA-Z]\w*\s*)|(\s*\(\s*(\s*[_a-zA-Z]\w*\s*,)*\s*[_a-zA-Z]\w*\s*\)\s*))\s*,)*\s*((\s*[_a-zA-Z]\w*\s*)|(\s*\(\s*(\s*[_a-zA-Z]\w*\s*,)*\s*[_a-zA-Z]\w*\s*\)\s*))\s*\)\s*))\s*\)\s*)|(\s*\s*(\s*((\s*((\s*[_a-zA-Z]\w*\s*)|(\s*\(\s*(\s*[_a-zA-Z]\w*\s*,)*\s*[_a-zA-Z]\w*\s*\)\s*))\s*)|(\s*\(\s*(\s*((\s*[_a-zA-Z]\w*\s*)|(\s*\(\s*(\s*[_a-zA-Z]\w*\s*,)*\s*[_a-zA-Z]\w*\s*\)\s*))\s*,)*\s*((\s*[_a-zA-Z]\w*\s*)|(\s*\(\s*(\s*[_a-zA-Z]\w*\s*,)*\s*[_a-zA-Z]\w*\s*\)\s*))\s*\)\s*))\s*,)*\s*((\s*((\s*[_a-zA-Z]\w*\s*)|(\s*\(\s*(\s*[_a-zA-Z]\w*\s*,)*\s*[_a-zA-Z]\w*\s*\)\s*))\s*)|(\s*\(\s*(\s*((\s*[_a-zA-Z]\w*\s*)|(\s*\(\s*(\s*[_a-zA-Z]\w*\s*,)*\s*[_a-zA-Z]\w*\s*\)\s*))\s*,)*\s*((\s*[_a-zA-Z]\w*\s*)|(\s*\(\s*(\s*[_a-zA-Z]\w*\s*,)*\s*[_a-zA-Z]\w*\s*\)\s*))\s*\)\s*))\s*\s*))([+-/*%&\^\|]?|[/<>*]{2})=/;

    Sk.globals = {
        _ih: new Sk.builtin.list(),
        _oh: new Sk.builtin.dict(),
        _: Sk.builtin.str.$empty,
        __: Sk.builtin.str.$empty,
        ___: Sk.builtin.str.$empty,
        _i: Sk.builtin.str.$empty,
        _ii: Sk.builtin.str.$empty,
        _iii: Sk.builtin.str.$empty,
        __name__: new Sk.builtin.str("__main__"),
    };
    Sk.globals.In = Sk.globals._ih;
    Sk.globals.Out = Sk.globals._oh;

    var stopExecution = false;
	var iserror=false;

    
    var info = "# Python 3.7( 请按 Ctrl+回车键 运行程序) " ;
	
	
    var cv = document.getElementById("cv");
	
    ipythonExample = function (dom) {
        if (ace === undefined) {
            throw Error("No ace");
        }
        this.editor = dom;

        
        var keyboardInterrupt = (e) => {
            if (e.ctrlKey && e.key === "c") {
                
                stopExecution = true;
            }
        };
        this.editor.addEventListener("keydown", keyboardInterrupt);

        
		this.infoCell();

        
        this.inputs = [];
        this.idx = 0;
        this.newCells();
        this.outf = this.outf.bind(this);
        this.lineHeight = this.inCell.renderer.lineHeight*1.2;
        this.pad = 15;		
		
		
        
        Sk.configure({
            output: this.outf,
            __future__: Sk.python3,
            yieldLimit: 1000,
            execLimit: 600000,
            retainGlobals: true,
            inputfunTakesPrompt: false,
            inputfun: (args) => {
                this.outf(args + "");
				var res = prompt(args);		
				if(res=="")
					res=1;
				if(res==null)
					res=2;
                this.outf((res+"\n"));		
				
                return res;
            },
        });
    };
	
	
	ipythonExample.prototype.beginCell=function(){
		
		
		
		
		this.infoCell();
		this.newCells();
		codes=new Array();
		
		
		
	}
	
	ipythonExample.prototype.infoCell = function () {
        var infoElement = document.createElement("DIV");
        infoElement.innerText = info;
		infoElement.className = "divprog";
        infoElement.style.margin = "5px";
        this.editor.appendChild(infoElement);
    };

	ipythonExample.prototype.addCell=function(msg,classname){
		const progElement = document.createElement("DIV");
		progElement.innerText = msg;
		progElement.className = classname;
		this.editor.appendChild(progElement);
	}
	
    ipythonExample.prototype.newCells = function () {
        this.idx = this.inputs.length;
        this.inCell = this.newIn();
        this.printCell = this.newPrint();
        this.outCell = this.newOut();
        this.inCell.focus();
        this.inCell.container.scrollIntoView();
    };
	
		
    ipythonExample.prototype.execute = function () {
		const code = this.inCell.getValue();	
		if(code!=""){	
			var codeold=code;
			const codeAsPyStr = Sk.ffi.remapToPy(code);
			Sk.globals["_i" + this.inputs.length] = codeAsPyStr; 
			Sk.globals.In.v.push(codeAsPyStr);
			
			this.inputs[this.inputs.length - 1] = code;

			let compile_code = code.trimEnd() || "None"; 
		
			const lines = compile_code.split("\n");
			let last_line = lines[lines.length - 1];
			if (
				!assignment.test(last_line) &&
				!defre.test(last_line) &&
				!importre.test(last_line) &&
				!comment.test(last_line) &&
				last_line.length > 0 &&
				last_line[0] !== " "
			) {
				lines[lines.length - 1] = "_" + this.inputs.length + " = " + last_line;
			}
			compile_code = lines.join("\n");
			
			this.inCell.setReadOnly(true);
			this.inCell.renderer.$cursorLayer.element.style.opacity = 0;
			
			
			(Sk.TurtleGraphics || (Sk.TurtleGraphics = {})).target = 'cv';

			
			const executionPromise = Sk.misceval.asyncToPromise(() => Sk.importMainWithBody("ipython", false, compile_code, true));
			executionPromise
				.then((res) => {
					const printContent = this.printCell.getValue();
					if (printContent.substring(printContent.length - 1) === "\n") {
						this.printCell.setValue(printContent.substring(0, printContent.length - 1), -1);				
					}
					this.printCell.container.style.height = this.printCell.session.getLength() * this.lineHeight + this.pad;
					this.printCell.resize();

					curoutput=printContent; 
														
					const last_input = Sk.globals["_" + this.inputs.length];
					if (Sk.builtin.checkNone(last_input) || last_input === undefined) {
						delete Sk.globals["_" + this.inputs.length]; 
					} else {
						this.outCell.setValue(Sk.ffi.remapToJs(Sk.misceval.objectRepr(last_input)), -1);
						if (last_input !== Sk.globals.Out) {
							Sk.abstr.objectSetItem(Sk.globals._oh, Sk.ffi.remapToPy(this.inputs.length), last_input);
							Sk.globals["___"] = Sk.globals["__"];
							Sk.globals["__"] = Sk.globals["_"];
							Sk.globals["_"] = last_input;
						}
					}
				})
				.catch((e) => {
					catcherror=e.toString();
					outContent = catcherror;
					savemsg.innerHTML=catcherror;
					//this.outf(catcherror);
					iserror=true;
				})
				.finally(() => {
					if(iserror){
						//console.log("错误标志：",iserror);
						this.inCell.setReadOnly(false);
						this.inCell.renderer.$cursorLayer.element.style.opacity = 1;
						iserror=false;
						//console.clear();
					}
					else{
						Sk.globals["_iii"] = Sk.globals["_ii"];
						Sk.globals["_ii"] = Sk.globals["_i"];
						Sk.globals["_i"] = Sk.globals["_i" + this.inputs.length];
						if (this.printCell.isVisible || this.outCell.isVisible) {
							this.inCell.container.style.height = this.inCell.session.getLength() * 40;
							this.inCell.resize();					
						}

						if(arrange){
							arrange=false;
							codes=new Array();
						}
						codes.push(codeold);
						
						this.newCells();			
						savemsg.innerHTML="";				
						}
					});
		}
		else{
			this.inCell.focus();
		}
							
    };

    ipythonExample.prototype.outf = function (text) {
        const curr_val = this.printCell.getValue();
        this.printCell.setValue(curr_val + text, 1);
    };

    ipythonExample.prototype.newIn = function () {
        let cell = document.createElement("DIV");
        this.editor.appendChild(cell);
        cell = ace.edit(cell);
        this.inputs.push("");
        cell.setOptions({
            showLineNumbers: false,
            firstLineNumber: this.inputs.length,
            printMargin: false,
            highlightGutterLine: false,
            highlightActiveLine: false,
			enableLiveAutocompletion: true, 
			enableSnippets: true, 
            showFoldWidgets: false,
            theme: "ace/theme/gruvbox",
            mode: "ace/mode/python",
			fontSize:22,
        });
        cell.session.addGutterDecoration(0, "in-gutter");
        cell.container.style.height = 40;
        cell.resize();
        cell.on("blur", () => {
            cell.selection.setSelectionRange(new ace.Range(0, 0, 0, 0), false);
        });
        cell.on("change", () => {
            cell.container.style.height = cell.session.getLength() * this.lineHeight + this.pad;
            cell.resize();
            cell.container.scrollIntoView();
        });
        cell.renderer.onResize(() => {});
        
		var countenter=0;
        cell.commands.addCommands([
            {
                name: "upHistory",
                bindKey: {
                    mac: "Up",
                    win: "Up",
                },
                exec: (e, t) => {
                    if (cell.getCursorPosition().row === 0) {
                        if (this.idx > 0) {
                            cell.setValue(this.inputs[--this.idx], 1);
                            cell.container.scrollIntoView();
                        }
                    } else {
                        cell.commands.commands.golineup.exec(e, t);
                    }
                },
            },
            {
                name: "downHistory",
                bindKey: {
                    mac: "Down",
                    win: "Down",
                },
                exec: (e, t) => {
                    if (cell.getCursorPosition().row === cell.session.getLength() - 1) {
                        if (this.idx < this.inputs.length - 1) {
                            cell.setValue(this.inputs[++this.idx], -1);
                            cell.container.scrollIntoView();
                        }
                    } else {
                        cell.commands.commands.golinedown.exec(e, t);
                    }
                },
            },
            {
                name: "execute",
                bindKey: {
                    win: "ctrl-enter",
                    mac: "command-enter",
                },
                exec: (e, t) => {
                    this.execute();
                },
            },
            {
                name: "exeauto",
                bindKey: {
                    win: "enter",
                },
                exec: (e, t) => {
					var codestr=cell.getValue();
					if(codestr!=""){
						var cursorPosition = cell.getCursorPosition();
						var alllen=cell.session.getLength();
						//console.log(cell.selection.getCursor());
						var nowline=cell.selection.getCursor()["row"]+1;
						var nowcol=cell.selection.getCursor()["column"];
						//console.log("总行数",alllen,"当前行",nowline,"当前列",nowcol);
						
						if(codestr.indexOf(":")>0){					
						    if(alllen!=nowline){						
								cell.session.insert(cursorPosition, "\n");
							}
							else{
								if(nowcol>0){
									cell.session.insert(cursorPosition, "\n");
								}
								else{
									this.execute();
								}
							}
						}
						else if(nowline<alllen){
							cell.session.insert(cursorPosition, "\n");
						}
						else if(nowline==alllen && nowcol==0){						
							cell.session.insert(cursorPosition, "\n");
							countenter++;
							if(countenter==1){
								this.execute();
								countenter=0;
							}
						}
						else{
							cell.session.insert(cursorPosition, "\n");
						}

					}
					else{
						console.log("无代码");
					}						
                },
            },
            {
                name: "keyboardInterrupt",
                bindKey: {
                    mac: "ctrl-C",
                    win: "ctrl-C",
                },
                exec: (e, t) => {
                    stopExecution = true;
                },
                readOnly: true,
            },
        ]);
		mycell=cell;
		
		
        return cell;
    };

    ipythonExample.prototype.newPrint = function () {
        let cell = document.createElement("DIV");
        this.editor.appendChild(cell);
        cell = ace.edit(cell);
        cell.setOptions({
            readOnly: true,
            printMargin: false,
            showGutter: false,
            showLineNumbers: false,
            highlightActiveLine: false,
            highlightGutterLine: false,
            highlightSelectedWord: false,
            theme: "ace/theme/gruvbox",
			fontSize:22,
        });
        cell.renderer.$cursorLayer.element.style.opacity = 0;
        cell.on("blur", () => {
            cell.selection.setSelectionRange(new ace.Range(0, 0, 0, 0), false);
            cell.renderer.$cursorLayer.element.style.opacity = 0;
        });
        cell.on("change", () => {
            if (!cell.isVisible) {
                cell.container.style.display = "block";
                cell.isVisible = true;
            }
            cell.container.style.height = cell.session.getLength() * this.lineHeight + this.pad;
            cell.resize();
            cell.focus();
            cell.renderer.$cursorLayer.element.style.opacity = "";
            cell.container.scrollIntoView();
        });
        cell.container.style.display = "none";
        cell.isVisible = false;
        cell.commands.addCommand({
            name: "keyboardInterrupt",
            bindKey: {
                mac: "ctrl-C",
                win: "ctrl-C",
            },
            exec: (e, t) => {
                stopExecution = true;
            },
            readOnly: true,
        });
        return cell;
    };

    ipythonExample.prototype.newOut = function () {
        let cell = document.createElement("DIV");
        this.editor.appendChild(cell);
        cell = ace.edit(cell);
        cell.setOptions({
            showLineNumbers: true,
            firstLineNumber: this.inputs.length,
            highlightActiveLine: false,
            highlightGutterLine: false,
            highlightSelectedWord: false,
            readOnly: true,
            printMargin: false,
            theme: "ace/theme/gruvbox",
			fontSize:22,
        });
        cell.session.addGutterDecoration(0, "out-gutter");
        cell.renderer.$cursorLayer.element.style.opacity = 0;
        cell.on("blur", () => {
            cell.selection.setSelectionRange(new ace.Range(0, 0, 0, 0), false);
        });
        cell.on("change", () => {
            if (!cell.isVisible) {
                cell.container.style.display = "block";
                this.printCell.container.style.height = this.printCell.session.getLength() * this.lineHeight;
                this.printCell.resize();
                cell.isVisible = true;
            }
            cell.container.style.height = cell.session.getLength() * this.lineHeight + this.pad;
            cell.resize();
            cell.container.scrollIntoView();
        });
        cell.container.style.display = "none";
        cell.isVisible = false;
        return cell;
    };	
	 
	 
})();

