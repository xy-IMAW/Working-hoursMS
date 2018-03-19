<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login_mobile.aspx.cs" Inherits="WHMS.login_mobile" %>

<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="UTF-8" name="viewport" content="width=device-width, initial-scale=1 user-scalable=0"> 
	<title>学号绑定</title>
</head>
<style type="text/css">
*{
	padding: 0;
	margin: 0;
}
body{
	background: #e7e7e7;
	background: linear-gradient(155deg,#f6f6f6 0%,#e9e9e9 100%)  100% 100%;
	width: 100vw;
	height: 100vh;
}
.loginwindow{
	width: 13.65rem;
	height: 22.507rem;
	background:#666;
	position: absolute;
	left: 0;
	top: 0;
	right: 0;
	bottom: 0;
	margin: auto;
	background: linear-gradient(165deg,transparent 10em,#fff 0)  100% 100%;
	box-shadow: 0rem 10px 1rem -1rem rgba(127,127,127,0.1),
				0rem 0.65rem 0rem -0.35rem rgba(220,220,220,0.9),
				0rem 0.5rem 2rem -0.5rem rgba(100,100,100,0.65),
				0rem 2rem 2rem -2rem rgba(100,100,100,0.65);
}
#btnLogin{
	outline: none;
	color: white;
	font-weight: bold;
	width: 6rem;
	height: 2rem;
	border: 0;
	border-radius: 1rem;
	position: absolute;
	left: 0;
	right: 0;
	bottom: -1rem;
	margin: auto;
	background: #000;
	box-shadow: 0rem 0.25rem 0.25rem rgba(40,40,40,0.25),
				0rem 0.5rem 0.5rem rgba(40,40,40,0.25),
				0rem 1rem 1rem rgba(40,40,40,0.25),
				0rem 2rem 2rem rgba(40,40,40,0.25),
				0rem 4rem 4rem rgba(40,40,40,0.25);

	background: linear-gradient(30deg,#ffe85a 0,#fe9807 100%)  100% 100%;
	box-shadow: 0rem 0.25rem 0.25rem rgba(254,158,12,0.25),
				0rem 0.5rem 0.5rem rgba(254,158,12,0.25),
				0rem 1rem 1rem rgba(254,158,12,0.25),
				0rem 2rem 2rem rgba(254,158,12,0.25),
				0rem 4rem 4rem rgba(254,158,12,0.25);

	transition: .5s;
}
#logo{
	position: absolute;
	bottom: 12.5rem;
	left: 0;
	right: 0;
	margin: auto;
	width: 3rem;
	height: 3rem;
	border-radius: 1.5rem;
	background: #000;
	background: linear-gradient(30deg,#ffe85a 0,#fe9807 100%)  100% 100%;
}
input{
	position: absolute;
	outline: none;
	border: 0;
	width: 10rem;
	height: 1.5rem;
	left: 0;
	right: 0;
	margin: auto;
	background: linear-gradient(30deg,#ffe85a 0,#fe9807 100%)  100% 100%;
	text-align: center;
	color: #999;
	}
input:hover{
	color: #fe9807;
}
#tbxStuID{
	background: none;
	border-bottom: 2px solid #fe9807;
	top: 13rem;
}
#tbxPassword{
	background: none;
	border-bottom: 2px solid #fe9807;
	top: 16rem;
}
#gs{
	background: transparent;
	color: #fe9807;
	text-align: center;
	position: absolute;
	left: 0;
	right: 0;
	top: 2.5rem;
	font-size: 1.5rem;
	margin: auto;
	width: 7rem;
	background: linear-gradient(0deg,transparent 0,transparent 35%,#fff 0,#fff 65%,transparent 0,transparent 100%);
	/*
	box-shadow: 0rem 1.5rem 0rem -0.7rem  rgba(255,255,255,1);
	*/
}
</style>
<body>
    <form id="form1" runat="server">
	<div class="loginwindow">
        <asp:Button ID="btnLogin" runat="server" Text="绑定" OnClick="btnLogin_Click" />
		<div id="gs">工时查询</div>
		<div id="logo"></div>
        <asp:TextBox ID="tbxStuID" runat="server" MaxLength="13" Wrap="False"></asp:TextBox>
        <input id="thxpasssword" readonly/>
	</div>
    </form>
</body>
    <script>
        document.getElementById("tbxStuID").placeholder = "ID";
        document.getElementById("tbxPassword").placeholder = "请绑定你的学号";
    </script>
</html>