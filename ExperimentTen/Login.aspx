<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="WHMS.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>工时管理系统——登录</title>
    <link rel="icon" runat="server" href="~/image/logo.ico" />
    <style type="text/css">
        body{
            moz-user-select: -moz-none;
            -moz-user-select: none;
            -o-user-select:none;
            -khtml-user-select:none;
            -webkit-user-select:none;
            -ms-user-select:none;
            user-select:none;
        }
        .textbox{
            border-top:0;
            border-left:0;
            border-right:0;
            border-bottom:solid 1px #000;
            background-color:transparent;
        }
        .btnlog{
            width: 3.8vw;
            height: 1.9vw;
            font-size: 1.2px;
            font-family: 微软雅黑;
            letter-spacing: 0.5vw;
            padding-left: 1vw;
            border-radius:0.4vw;
            border: 0.03vw solid #2576A8;
            background: -webkit-linear-gradient(top,#66B5E6,#2e88c0);
            background: -moz-linear-gradient(top,#66B5E6,#2e88c0);
            background: linear-gradient(top,#66B5E6,#2e88c0);
            background: -ms-linear-gradient(top,#66B5E6,#2e88c0);
            box-shadow: 0 1px 2px #8AC1E2 inset,0 -1px 0 #316F96 inset;
            color: #fff;
            text-shadow: 1px 1px 0.5px #22629B;
            opacity:0.9;
        }
        .btnlog:hover{
            background: -webkit-linear-gradient(top,#8DC9EF,#4E9FD1);
            background: -moz-linear-gradient(top,#8DC9EF,#4E9FD1);
            background: linear-gradient(top,#8DC9EF,#4E9FD1);
            background: -ms-linear-gradient(top,#8DC9EF,#4E9FD1);  

}
    </style>
</head>
<body style="width:100%;height:100% ;background-image:url(image/login.jpg);background-repeat:no-repeat; background-size:100%,100%;  position:absolute;">
    <form id="form1" runat="server">
        <f:PageManager runat="server" />     
            <!--登陆框-->  
            <div style="background-color:rgba(198,218,253,0.43); margin:15vw auto; width:30vw; height:15vw; border-radius:1vw;">
                <p style="text-align: center; font-family: 宋体; font-size: 1.3vw;line-height:2.8vw; margin-bottom:-0.55vw;">自动化青协工时统计</p>
                <hr />
                <!--左侧logo图-->
                <div id="img_icon" style="float:left;width:12vw;"><img src="image/icon.jpg" style="width:8vw;height:8vw;background-repeat:no-repeat; background-size:cover;
margin-left:2vw;margin-right:5vw; margin-top:1.2vw;"/></div>
                <!--右侧登陆栏-->
                <div id="login" style="width:18vw; height:10vw; float:right; margin-top:2vw; font-size:1vw; font-family:微软雅黑">
                    账号&nbsp&nbsp<asp:TextBox ID="tbxStuID" CssClass="textbox" runat="server" MaxLength="13" Wrap="False" Rows="1" Columns="20" ></asp:TextBox>
                    <br /><br />
                    密码&nbsp&nbsp<asp:TextBox ID="tbxPassword" CssClass="textbox" runat="server" MaxLength="13" Wrap="False" TextMode="Password" Rows="1" Columns="20"></asp:TextBox>

                    <br /><br />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    <asp:Button ID="btnLogin" CssClass="btnlog" runat="server" Text="登录" OnClick="btnLogin_Click" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    <asp:Button ID="btnReset" CssClass="btnlog" runat="server" Text="重置" OnClick="btnReset_Click"/>
                </div>
                <script>
                    //function ClearTxt() {
                    //    document.getElementById("btnLogin").value = "";
                    //    //document.getElementById("btnReset").value = "";
                    //    return false;
                    //}
                    ////var btn = document.getElementById("btnLogin");
                    ////btn.onmouseover = function () {
                    ////this.style.background = "#ccc";//鼠标移入变成灰色
                    ////}
                    //////btn.onmouseout = function () {
                    //////this.style.background = "#red";//鼠标移出变成红色
                    //////}                
            </script>
            </div>
    </form>
</body>
</html>
