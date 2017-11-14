<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PasswordUpdate.aspx.cs" Inherits="WHMS.Account.Update" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
   <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false"
            AutoScroll="true" BodyPadding="10px" runat="server">

            <Rows>
                <f:FormRow>
                    <Items>
                        <f:Label ID="labStuID" Label="学号" Text="" CssClass="highlight" runat="server" />
                        <f:Label ID="LabeName" Label="姓名" Text="" runat="server" />
                         <f:Label ID="LabState" Label="状态" Text="" runat="server"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>                     
                        <f:TextBox ID="txtPwd1" Label="请输入新密码" Text="" runat ="server" Required="true" TextMode="Text"></f:TextBox>
                        <f:TextBox ID="txtPwd2" Label ="请确认密码"   Text="" runat="server" Required="true" TextMode="Text"></f:TextBox>                        
                    </Items>
                </f:FormRow>
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <f:Button ID="btnClose" EnablePostBack="true" Text="保存" runat="server" Icon="SystemSave" OnClick="btnClose_Click">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
    </form>
</body>
</html>
