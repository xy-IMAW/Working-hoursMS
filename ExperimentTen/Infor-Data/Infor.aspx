<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Infor.aspx.cs" Inherits="WHMS.Infor_Data.Infor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="RegionPanel1" EnableAjax="true" />
        <f:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server">
            <Regions>
                <f:Region ID="Region1" ShowBorder="false" ShowHeader="false"
                    Width="200px" RegionPosition="Left" Layout="Fit"
                    runat="server">                    
                   <Items>                                           
                       <f:Tree ID="Tree1" Width="150px" EnableCollapse="true"  Title="年级与班级" runat="server" Icon="Anchor" OnExpand="Tree1_Expand" OnNodeCommand="Tree1_NodeCommand" EnableTextSelection="true"> 
                       </f:Tree>                                        
                    </Items>
                </f:Region>
                <f:Region ID="Region2" ShowBorder="true" ShowHeader="true" Position="Center" Title=""
                    Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Left" runat="server">
                    <Items>
                           <f:Panel ID="panel2" ShowBorder="false" ShowHeader="false" runat="server" >
                            <Items>                              
                             <f:Grid ID="Grid1" Height="650px" BoxFlex="1" ShowBorder="false" runat="server" ShowGridHeader="true"
                                  DataKeyNames="Id,Name" EnableRowClickEvent="true" AllowPaging="true" PageSize="40" PageIndex="0" OnPageIndexChange="Grid1_PageIndexChange" EnableCollapse="true">
                            <Toolbars >
                                <f:Toolbar ID="toolbar1" runat="server">
                                    <Items>
                                        <f:Button ID="btnSearch" Icon="Zoom" runat="server" Text="查看" ToolTip="查看选中学生的工时信息" OnClick="btnSearch_Click"></f:Button>
                                        <f:Button ID="btnadd" Icon="Add" runat="server" Text="新增"></f:Button>
                                         
                                     
                                        <f:Button ID="btnDelet" Icon="Delete" runat="server" Text="删除" ConfirmTitle="注意" ConfirmIcon="Question" ConfirmText="确认删除？" ConfirmTarget="Self" OnClick="btnDelet_Click"></f:Button>
                                        <f:TextBox ID="txtStuID" Label="学号" LabelAlign="Right" runat="server" Text=""></f:TextBox>
                                        <f:Button ID="btnStuSerach" Icon="Zoom" Text="查找" runat="server" OnClick="btnStuSerach_Click"></f:Button>
                                    </Items>
             <%--                        <Items>
                                          
                                       <f:ContentPanel runat="server">
                                              <f:CPHConnector runat="server">
                                                  <asp:Button Text="下载学生模板" runat="server" OnClick="btnDownLoad_Click" />
                                            </f:CPHConnector>
                                        </f:ContentPanel>
                                    </Items>
                 --%>
                                </f:Toolbar>
                                <f:Toolbar runat="server">
                                    <Items>
                                           <f:Button ID="btnImport" Icon="PageGo" runat="server" Text="导入名单"></f:Button>
                                        
                                          <f:Button ID="btnDownLoad" EnableAjax="false"  Icon="Zoom" runat="server" Text="下载模板" OnClick="btnDownLoad_Click"></f:Button>
                                    </Items>
                                    
                                </f:Toolbar>
                            </Toolbars>
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
                            </Columns>
                        </f:Grid>                     
                       </Items>
                     </f:Panel>
                         <f:Window ID="window1" Title="增加学生"  EnableCollapse="true" Hidden="true" EnableIFrame="true"  CloseAction="HidePostBack" EnableMaximize="true"
            EnableResize="true" EnableClose="true" Target="Top" OnClose="window1_Close" IsModal="true" Width="550px" Height="450px" runat="server">
        </f:Window>
                        
                        <f:Window ID="window3" Title="导入学生名单" EnableCollapse="true" Hidden="true" EnableIFrame="true"  CloseAction="HidePostBack" EnableMaximize="true"
            EnableResize="true" EnableClose="true" Target="Top" OnClose="window3_Close" IsModal="true" Width="800px" Height="600px" runat="server"></f:Window>
                    </Items>           
                </f:Region>
            </Regions>
        </f:RegionPanel>
        <br />
        <br />
    </div>
    </form>
</body>
</html>

