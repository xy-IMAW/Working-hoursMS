<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="WHMS.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>工时管理系统——登录</title>
    <link rel="icon" runat="server" href="~/image/logo.ico" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager runat="server" />
        <div  style="background-image:url(image/login.jpg);background-repeat:no-repeat; background-size:cover;  position:absolute; width:100.5%; height:100.9%; margin-left:-1vw;
margin-top:-1vw;" >           
            <div style="background-color:rgba(250,253,198,0.7); margin:15vw auto; width:30vw; height:15vw; border-radius:5px;">
                <h4 style="text-align:center; align-content:center;margin-top:auto;">自动化青协工时统计</h4>
                <hr />
                <div id="img_icon" style="float:left;width:12vw;"><img src="image/icon.jpg" style="width:9vw;height:9vw;background-repeat:no-repeat; background-size:cover;
margin-left:2vw;margin-right:5vw; margin-top:1vw;"/></div>
                <div id="login" style="width:18vw; height:10vw; float:right; margin-top:2vw;">
                    账户:<asp:TextBox ID="tbxStuID" runat="server" MaxLength="13" Wrap="False" ></asp:TextBox>
                    <br />
                    <br />
                    密码:<asp:TextBox ID="tbxPassword" runat="server" MaxLength="13" Wrap="False" TextMode="Password"></asp:TextBox>
                    <br /><br />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    <asp:Button ID="btnLogin" runat="server" Text="登录" OnClick="btnLogin_Click" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    <asp:Button ID="btnreset" runat="server" Text="重置"/>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
