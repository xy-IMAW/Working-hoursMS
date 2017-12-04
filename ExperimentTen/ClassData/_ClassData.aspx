<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="_ClassData.aspx.cs" Inherits="WHMS.ClassData._ClassData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<%--<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>--%>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="RegionPanel1" EnableAjax="true" />
        <f:Panel runat="server" ShowHeader="true" Title="班级工时查询">
            <Toolbars>
                <f:Toolbar runat="server">
                    <Items>
                        <f:Label runat="server" ID="lab" Text=""/>
                          <f:DropDownList ID="DL1" runat="server" Label="学期" Required="true" LabelAlign="Right" />
                          <f:Button ID="btn" runat="server" Icon="ApplicationViewColumns" Text="查看班级工时" OnClick="btn_Click"/>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items>
                 <f:Grid ID="Grid1" Height="550px" BoxFlex="1" ShowBorder="false" runat="server" ShowGridHeader="true"
                                  DataKeyNames="Id,Name" EnableRowClickEvent="true" AllowPaging="true" PageSize="40" PageIndex="0" OnPageIndexChange="Grid1_PageIndexChange" EnableCollapse="false">
                          
                                 <Columns>
                                <f:TemplateField Width="60px">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:BoundField Width="250px" DataField="StuID" HeaderText="学号" TextAlign="Center"/>
                                <f:BoundField Width="100px"  DataField="StuName" HeaderText="姓名" TextAlign="Center" />
                                <f:BoundField Width ="100px" DataField="Class" HeaderText="班级" TextAlign="Center" />
                                <f:BoundField Width="100px"  DataField="Grade" HeaderText="年级" TextAlign="Center" />
                                <f:BoundField Width="100px"  DataField="Sex" HeaderText="性别" TextAlign="Center" />
                                <f:BoundField Width="100px"  DataField="Other" HeaderText="备注" TextAlign="Center" />

                            </Columns>
                        </f:Grid>      
            </Items>
        </f:Panel>
                 <f:Window ID="window1" Hidden="true" EnableIFrame="true" runat="server" EnableClose="true"  Width="800px" Height="600px" Target="Top"  EnableMaximize="true" ></f:Window>
       
        </div>
    </form>

</body>
</html>