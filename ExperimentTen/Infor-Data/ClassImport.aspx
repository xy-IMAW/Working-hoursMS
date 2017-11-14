<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClassImport.aspx.cs" Inherits="WHMS.Infor_Data.ClassImport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
 
<body>
    <form id="form1" runat="server">
    <div>
    <f:PageManager ID="pagemanager1" runat="server" />
        <f:Panel ID="panel1" runat="server">
            <Toolbars>
                <f:Toolbar ID="tool1" runat="server">
                    <Items>
                         <f:FileUpload ID="FileUpload1" runat="server" Label="文件" />
                         <f:Button ID="btn1" runat="server" Text="上传" OnClick="btn1_Click" />  
                         <f:Button ID="btn" runat="server" OnClick="btn_Click" Text="更新" />   
                    </Items>
                </f:Toolbar>
            </Toolbars>
                <Items>
                 <f:Grid ID="grid" runat="server">
                     <Columns>                         
                          <f:BoundField DataField="年级" Width="100px" HeaderText="年级" runat="server"></f:BoundField>
                          <f:BoundField DataField="班级" Width="100px" HeaderText="班级" runat="server"></f:BoundField>
                     </Columns>
                 </f:Grid>                
            </Items>     
        </f:Panel>
    </div>
    </form>
</body>
</html>
