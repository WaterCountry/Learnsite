function WriteBg()
{
	var a=["#E9FAFF","#CCCCCC","#FFFDDD","#EEFAEE","#A3CAEB","#D0CFB2","#B7D1A5","#C2CEDA","#9799AC"]
	for(var i=0;i<a.length;i++){
		document.write("<img src='"+ "../Images/b.gif' style='cursor:hand;width:8px;height:8px;border:1px solid #999;background:"+ a[i] +"' alt='"+"打字背景："+a[i]+"' onclick='ContentBg(\""+(a[i])+"\")'/> ");
	}
}
function ContentBg(color)
{
	var obj=document.getElementById("Tcontent");
	obj.style.backgroundColor=color;
}