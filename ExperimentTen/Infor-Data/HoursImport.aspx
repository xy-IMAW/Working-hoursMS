<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HoursImport.aspx.cs" Inherits="WHMS.Infor_Data.GradeImport" %>

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
                         <f:FileUpload ID="FileUpload1" runat="server" Label="文件" LabelAlign="Right" />
                         <f:Button ID="btn1" runat="server" Text="上传" OnClick="btn1_Click" />  
                         <f:Button ID="Button1" runat="server" OnClick="btn_Click" Text="更新"  />   
                    </Items>
                </f:Toolbar>
            </Toolbars>
                <Items>
       <f:Grid ID="grid" runat="server">
           <Columns>
                   <f:BoundField DataField="学号" runat="server"></f:BoundField>
                          <f:BoundField DataField="姓名" runat="server"></f:BoundField>
                                 <f:BoundField DataField="活动" runat="server"></f:BoundField>
                          <f:BoundField DataField="工时" runat="server"></f:BoundField>
                         <f:BoundField DataField="学年学期" runat="server"></f:BoundField>
                          <f:BoundField DataField="日期" runat="server"></f:BoundField>
           </Columns>
           
       </f:Grid>
                
            </Items>     
        </f:Panel>
    </div>
    </form>
</body>
</html>
