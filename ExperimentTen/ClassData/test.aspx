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
              <asp:Button ID="btn1" runat="server" OnClick="btnout_Click" Text="导出文件" />
            <f:PageManager runat="server" />
      
          
                
      
          
                 <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="true" Title="表格" EnableCollapse="false" runat="server"
            DataKeyNames="Id">
                     <Toolbars>
                         <f:Toolbar runat="server">
                             <Items>
                <f:Button ID="btnout" runat="server" OnClick="btnout_Click" Text="导出文件"></f:Button>
                             </Items>

                         </f:Toolbar>
                     </Toolbars>
                     </f:Grid>
             <br />
             <br />
             <br />


        </div>
    </form>
</body>
</html>
