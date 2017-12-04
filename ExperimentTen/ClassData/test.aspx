<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="WHMS.test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
 
        
        <div>
            <f:PageManager runat="server" />

         
            <f:Panel runat="server">
                <Toolbars>
                    <f:Toolbar runat="server">
                        <Items>
                            <f:FileUpload ID="fileupload" runat="server" Label="上传"></f:FileUpload>
                            <f:Button ID="btn2" Text="窗体" OnClick="btn1_Click" runat="server"></f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Items>
                    <f:ContentPanel runat="server" BodyPadding="100px" RegionPosition="Center" >
                         <asp:GridView ID="GridView1" runat="server" OnRowCreated="GridView1_RowCreated"/>
                    </f:ContentPanel>
                    <f:Grid ID="grid" runat="server" ShowBorder="true" />
                </Items>
            </f:Panel>
           <f:Window ID="window1" Hidden="true" EnableIFrame="true" runat="server" EnableClose="true"  Width="800px" Height="600px" Target="Top"  EnableMaximize="true" ></f:Window>

        </div>
    </form>
</body>
</html>
