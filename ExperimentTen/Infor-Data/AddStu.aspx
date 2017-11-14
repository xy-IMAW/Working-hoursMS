<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddStu.aspx.cs" Inherits="WHMS.Infor_Data.AddStu" %>

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
                        <f:TextBox ID="txtStuID" Label="学号" Text="" runat="server"></f:TextBox>
                       
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                         <f:TextBox ID="txtStuName" Label="姓名" Text="" runat="server" ></f:TextBox>
                         <f:TextBox ID="txtClass" Label="班级" Text="" runat="server"></f:TextBox>
                    </Items>
                </f:FormRow>
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <f:Button ID="btnAdd" EnablePostBack="true" Text="添加" runat="server" Icon="SystemSave" OnClick="btnAdd_Click">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
    </form>
</body>
</html>
