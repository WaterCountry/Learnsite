d="="
for line in open("grapheditor_zh.txt"):   
    #print(line)
    if d in line:
        str=line.split(d)        
        print(str[1])
