<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StuImport.aspx.cs" Inherits="WHMS.Infor_Data.StuImport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<%--<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>--%>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <f:PageManager ID="PageManager1" runat="server" EnableFStateValidation="false" />
        <f:Panel ID="panel1" runat="server" >
        
            <Toolbars>
                <f:Toolbar runat="server">
                    <Items>
                      <f:FileUpload ID="FileUpload1" runat="server" Label="文件" ColumnWidth="200px" ShowRedStar="true" />
                       
                        
                    
                          
                         <f:Button ID="btn" runat="server" OnClick="btn_Click" Text="上传" Width="70px" />   
               <f:Button ID="btn1" runat="server" Text="更新" OnClick="btn1_Click" Width="70px" />    
                    </Items>
                </f:Toolbar>               
            </Toolbars>
            <Items>
                        <f:Grid ID="grid" runat="server">
                            <Columns>
                                   <f:BoundField DataField="学号" HeaderText="学号" runat="server"></f:BoundField>
                                  <f:BoundField DataField="姓名" HeaderText="姓名"  runat="server"></f:BoundField>
                                  <f:BoundField DataField="班级" HeaderText="班级"  runat="server"></f:BoundField>
                                  <f:BoundField DataField="年级" HeaderText="年级"  runat="server"></f:BoundField>
                                  <f:BoundField DataField="性别" HeaderText="性别"  runat="server"></f:BoundField>
                                  <f:BoundField DataField="备注" HeaderText="备注"  runat="server"></f:BoundField>

                            </Columns>
                        </f:Grid>
               
                
            </Items>           
        </f:Panel>      
    </div>      
    </form>

</body>
</html>
