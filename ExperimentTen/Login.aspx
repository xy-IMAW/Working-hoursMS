﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="WHMS.login" %>

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
            width: 72.9531px;
            height: 36.4688px;
            font-size: 12px;
            font-family: 微软雅黑;
            letter-spacing: 9.6px;
            padding-left: 19.2px;
            border-radius: 7.68px;
            border: 0.5625px solid #2576A8;
            background: -webkit-linear-gradient(top,#66B5E6,#2e88c0);
            background: -moz-linear-gradient(top,#66B5E6,#2e88c0);
            background: -ms-linear-gradient(top,#66B5E6,#2e88c0);
            background: linear-gradient(top,#66B5E6,#2e88c0);
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
<body style="width:100%;height:100% ;background-image:url(image/login.jpg); background-size:100%,100%;  position:absolute;">
    <form id="form1" runat="server">
        <f:PageManager runat="server" />     
            <!--登陆框-->  
            <div style="background-color:rgba(198,218,253,0.43); margin: 288px auto; width:576px; height:288px; border-radius:19.2px;">
                <p style="text-align: center; font-family: 宋体; font-size: 24.96px;line-height:53px; margin-bottom:-10.56px;">自动化青协工时统计</p>
                <hr />
                <!--左侧logo图-->
                <div id="img_icon" style="float:left;width:230.391px;">
                    <img src="image/icon.jpg" style="width:153.594px ;height:153.594px ;background-repeat:no-repeat; background-size:cover;
                            margin-left:38.4px ;margin-right:96px; margin-top:23.04px;"/>
                </div>
                <!--右侧登陆栏-->
                <div id="login" style="width:345.594px; height:192px; float:right; margin-top:38.4px; font-size:19.2px; font-family:微软雅黑">
                    账号&nbsp&nbsp<asp:TextBox ID="tbxStuID" CssClass="textbox" runat="server" MaxLength="13" Wrap="False" Rows="1" Columns="20"  autofocus></asp:TextBox>
                    <br /><br />
                    密码&nbsp&nbsp<asp:TextBox ID="tbxPassword" CssClass="textbox" runat="server" MaxLength="13" Wrap="False" TextMode="Password" Rows="1" Columns="20"></asp:TextBox>

                    <br /><br />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    <asp:Button ID="btnLogin" CssClass="btnlog" runat="server" Text="登录" OnClick="btnLogin_Click" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    <asp:Button ID="btnReset" CssClass="btnlog" runat="server" Text="重置" />
                </div>
                <script>
                    document.getElementById("btnReset").onclick = function () {
                        document.getElementById("tbxStuID").value = "";
                        document.getElementById("tbxPassword").value = "";

                    };
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
