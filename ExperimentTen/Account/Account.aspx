<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="WHMS.Account.Account" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager_01" AutoSizePanelID="panelMain" runat="server" />
        <f:Panel ID="panelMain" runat="server" ShowBorder="false" ShowHeader="false" EnableCollapse="true" Layout="Fit">
            <Items>
                <f:Grid ID="gridExample" Title="权限管理" ShowBorder="true" AllowPaging="true" ShowHeader="true"
                    DataKeyNames="StuID" EnableCollapse="false" EnableCheckBoxSelect="true" PageSize="10"  PageIndex="1" OnPageIndexChange="gridExample_PageIndexChange"
                    EnableMultiSelect="false"  runat="server">
                    <Toolbars>
                        <f:Toolbar ID="toolbar_01" runat="server">
                            <Items>
                                <f:Button ID="btnAdd" Text="新增" Icon="UserAdd" runat="server" OnClick="btnAdd_Click" >
                                </f:Button>
                                <f:Button ID="btnDelete" Text="删除" Icon="UserDelete" runat="server" OnClick="btnDelete_Click">
                                </f:Button>
                                 <f:Button ID="Button1" EnableAjax="false" Icon="PageWhiteExcel" DisableControlBeforePostBack="false"  runat="server" Text="导出" OnClick="Button1_Click">
                                </f:Button>
                               
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                            <f:TemplateField Width="60px">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </f:TemplateField>
                        <f:BoundField Width="150px" ColumnID="StuID" SortField="StuID" DataField="StuID"
                                    TextAlign="Center" HeaderText="学号"></f:BoundField>
                        <f:BoundField Width="120px" ColumnID="StuName" SortField="StuName" DataField="StuName"
                                    TextAlign="Center" HeaderText="姓名"></f:BoundField>
                        <f:BoundField Width="120px" ColumnID="Class" SortField="Class" DataField="Class"
                                    TextAlign="Center" HeaderText="班级"></f:BoundField>
                        <f:BoundField Width="120px" ColumnID="Grade" SortField="Grade" DataField="Grade" 
                                    TextAlign="Center" HeaderText="年级"></f:BoundField>
                         <f:BoundField Width="120px" ColumnID="State" SortField="State" DataField="State" 
                                    TextAlign="Center" HeaderText="状态"></f:BoundField>
                    </Columns>
                </f:Grid>
            </Items>
        </f:Panel>
        <f:Window ID="window1" Title="添加管理员"  EnableCollapse="true" Hidden="true" EnableIFrame="true"  CloseAction="HidePostBack" EnableMaximize="true"
            EnableResize="true" EnableClose="true" OnClose="window1_Close"  Target="Top" IsModal="true" Width="850px" Height="450px" runat="server">
        </f:Window>
    </form>
</body>
</html>
