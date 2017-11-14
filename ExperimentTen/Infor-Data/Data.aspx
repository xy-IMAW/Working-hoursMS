<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Data.aspx.cs" Inherits="WHMS.Infor_Data.Data" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
        .infor {
            font-size:15px;
            font-style:inherit;
            margin-left:6px;
            left:50px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager_01" AutoSizePanelID="panelMain" runat="server" />
                       
          <f:Panel ID="panelMain" runat="server" ShowBorder="false" ShowHeader="false" EnableCollapse="true" Layout="Fit">
            <Items>
                <f:Grid Height="750px" ID="gridExample" Title="工时信息" ShowBorder="false" AllowPaging="true" ShowHeader="true" IsDatabasePaging="true"
                    DataKeyNames="ID" EnableCollapse="false" EnableCheckBoxSelect="true" PageSize="20"  PageIndex="1" OnPageIndexChange="gridExample_PageIndexChange"
                    EnableMultiSelect="false"  runat="server" EnableSummary="true" SummaryPosition="Bottom">
                   
                    <Toolbars>                        
                   
                        <f:Toolbar ID="toolbar_01" runat="server">
                          
                            <Items>
                                <f:TextBox ID="txtId" Label="学号" runat="server" LabelAlign="Right"></f:TextBox>
                                
                                <f:DropDownList ID="DL1" runat="server" Label="学年" LabelAlign="Right" >
                                </f:DropDownList>
                                <f:DropDownList ID="DL2" runat="server" Label="学期" LabelAlign="Right">
                                </f:DropDownList>
                              <f:Button ID="btnSelect" Text="查询" runat="server" OnClick="btnSelect_Click" Icon="Zoom"></f:Button>
                                <f:Button ID="btnAdd" Text="新增" Icon="Add" runat="server">
                                </f:Button>
                                <f:Button ID="btnDelete" OnClick="btnDelete_Click3" Text="删除" Icon="Delete" runat="server">
                                </f:Button>
                                <f:Button ID="Button1" EnableAjax="false" Icon="PageWhiteExcel" DisableControlBeforePostBack="false"  runat="server" Text="导出为Excel文件" OnClick="Button1_Click">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                             <f:Toolbar ID="infor" runat="server">
                              <Items>
                                
                                <f:Label ID="StuID" Text="" runat="server"  AbsoluteX="100px" LabelWidth="200px"></f:Label>
                                 <f:Label ID="StuName" text="" runat="server" LabelAlign="Right" AbsoluteX="200px"></f:Label>
                                  <f:Label ID="hourcount" Text="" runat="server" AbsoluteX="300px"></f:Label>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                          <f:TemplateField Width="60px" HeaderText="序号">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                           </f:TemplateField>
                        <f:BoundField Width="150px" ColumnID="SySe" SortField="SySe" DataField="SySe"
                                    TextAlign="Center" HeaderText="学年学期"></f:BoundField>
                        <f:BoundField Width="150px" ColumnID="Program" SortField="Program" DataField="Program"
                                    TextAlign="Center" HeaderText="志愿活动"></f:BoundField>
                        <f:BoundField Width="150px" ColumnID="Working_hours" SortField="Working_hours" DataField="Working_hours"
                                    TextAlign="Center" HeaderText="获得工时"></f:BoundField>

                    </Columns>
                </f:Grid>
            </Items>
        </f:Panel>
                    
             
        <f:Window ID="windowPop" Title="编辑"  EnableCollapse="false" Hidden="true" EnableIFrame="true" IFrameUrl="about:blank" CloseAction="HidePostBack" EnableMaximize="false"
            EnableResize="false" EnableClose="false" OnClose="windowPop_Close" Target="Top" IsModal="true" Width="850px" Height="450px" runat="server">
        </f:Window>
         
    </form>
</body>
</html>
