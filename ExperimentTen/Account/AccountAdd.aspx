<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountAdd.aspx.cs" Inherits="WHMS.Account.AccountAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            
         <f:PageManager ID="PageManager_01" AutoSizePanelID="panel2"  runat="server" > </f:PageManager>             
        <f:Panel ID="panel2" runat="server">
           <Items>
                <f:Form ID="Search" runat="server">
                    <Rows>
                        <f:FormRow>
                                  <Items>
                                    <f:TextBox ID="txbStuID" Label="学号" runat="server" Width="80px"> </f:TextBox>
                                    <f:Button ID="btnselect" Text="查询" runat="server" OnClick="btnselect_Click"> </f:Button>
                                  </Items>  
                        </f:FormRow>
                    </Rows>
                </f:Form>
                <f:Panel ID="panel1" runat="server">
                    <Items>
                        <f:Grid ID="grid2" Title="" ShowBorder="false" ShowHeader="false"
                    DataKeyNames="StuID" EnableCollapse="false" EnableCheckBoxSelect="true" 
                    EnableMultiSelect="false"  runat="server" Height="200px">
                            <Columns>
                              <f:BoundField Width="150px" ColumnID="StuID" SortField="StuID" DataField="StuID"
                                    TextAlign="Center" HeaderText="学号"></f:BoundField>
                        <f:BoundField Width="120px" ColumnID="StuName" SortField="StuName" DataField="StuName"
                                    TextAlign="Center" HeaderText="姓名"></f:BoundField>
                        <f:BoundField Width="120px" ColumnID="Class" SortField="Class" DataField="Class"
                                    TextAlign="Center" HeaderText="班级"></f:BoundField>
                        <f:BoundField Width="120px" ColumnID="Grade" SortField="Grade" DataField="Grade" 
                                    TextAlign="Center" HeaderText="年级"></f:BoundField>
                                </Columns>
                            <Toolbars>
                                <f:Toolbar runat="server">
                                    <Items>
                                        <f:DropDownList ID="drop" runat="server">
                                            <f:ListItem  Text="管理员" Value="管理员"/>
                                            <f:ListItem  Text="组织委员" Value="组织委员"/>
                                        </f:DropDownList>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                            </f:Grid>
                        <f:Button ID="btnAdd2" Text="添加" runat="server" OnClick="btnAdd2_Click" Width="80px"></f:Button> 
                    </Items>
                </f:Panel>
            </Items>
            </f:Panel>
    </div>
    </form>
</body>
</html>
