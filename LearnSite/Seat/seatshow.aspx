<%@ Page Language="C#" AutoEventWireup="true" CodeFile="seatshow.aspx.cs" Inherits="Seat_seatshow" %>

<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>机房电脑布置图</title>
    <script src="../Js/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../Js/jquery-ui-1.8.24.custom.min.js" type="text/javascript"></script>
    <link href="../Js/computer.css" rel="stylesheet" type="text/css" />
    <script src="../Js/seatToolTip.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $(init);
            function init() {
                $(".computer").disableSelection();
                $(oldseat);
            }
        });

        function oldseat() {
            var done = "<%=this.firstshows %>";
            if (done.length > 10) {
                var old_collects = done.split('-');

                var srl = old_collects[3];
                var scook = old_collects[4];
                oldshow(srl, scook);
            }
        }
        function oldshow(roomset, cookset) {
            var croomoffset = $('#sortable').offset();
            var crl = croomoffset.left;

            if (cookset != null) {
                var cookslist = cookset.split('|');
                var cookscount = cookslist.length - 1;
                var roomfix = 0;
                if (roomset != null) {
                    roomfix = roomset - crl;
                }
                for (var i = 0; i < cookscount; i++) {
                    var cook = cookslist[i].split(':');
                    var cookname = cook[0];
                    var cookoffset = cook[1].split(',');
                    var cookleft = cookoffset[0] - roomfix; // js加运算不对，只能采用减运算
                    var cooktop = cookoffset[1];
                    restore(cookname, cookleft, cooktop);
                }
                refresher();
            }
        }
        function reviewstu(cmp, snum, sname, ptype) {
            $(".computer").each(function () {
                var id = $(this).attr("id");
                if (id === cmp) {
                    $(this).attr("title", snum);
                    $(this).attr("tabindex", ptype);
                    $(this).html(sname);
                }
            });
        }

        function restore(cn, cl, ct) {
            $(".computer").each(function () {
                var id = $(this).attr("id");
                if (id === cn) {
                    $(this).offset({
                        "top": ct,
                        "left": cl
                    });                    
                }
            });
        }

        function refresher() {
            var oldstu = $('#StoreMsg').html();
            if (oldstu != null) {
                if (oldstu.indexOf("|") > 0) {
                    var students = oldstu.split('|');
                    var scount = students.length;
                    for (var j = 0; j < scount; j++) {
                        var stu = students[j].split('-');
                        var cmp = stu[0];
                        var snum = stu[1];
                        var sname = stu[2];
                        var ptype = stu[3];
                        reviewstu(cmp, snum, sname, ptype);
                    }
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="houserfloor" class="floor">
        <div id="computerhouse" class="house">
        <center>
            <div class="nomenu">
                <asp:ImageButton runat="server" ID="reflashStudent" 
                    ImageUrl="~/Images/home.png" 
                    ToolTip="点我刷新" onclick="reflashStudent_Click" />
                &nbsp;<asp:Label runat="server" ID="LabelTitle" Font-Bold="True">机房名称</asp:Label>
                &nbsp;&nbsp;&nbsp;<label id="msg"></label></div>
            <div id="sortable" class="nosortablediv">
                <asp:Literal ID="myhouse" runat="server">
                <div >没有找到该机房电脑布置图！</div>
                </asp:Literal>
            </div>
            </center>
            <div id="StoreMsg" runat="server" style="display: none;">
            </div>
        </div>
    </div>
    </form>
</body>
</html>
