<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="data_yb.aspx.cs" Inherits="WHMS.data_yb"%>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1.0,maximum-scale=1.0, user-scalable=no"/>
    <title>工时查询</title>
</head>
<body style="width:100%;">
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager_01" runat="server" AutoSizePanelID="gridExample"/>
                       
          
                <f:Grid Height="600px" ID="gridExample" Title="工时信息" ShowBorder="false" ShowHeader="true" 
                    DataKeyNames="ID" EnableCollapse="false"  AllowPaging="true" PageSize="20"   PageIndex="0" OnPageIndexChange="gridExample_PageIndexChange"
                    EnableMultiSelect="false"  runat="server" EnableSummary="true" SummaryPosition="Top" AutoScroll="true" ShowPagingMessage="false">
                   
                    <Toolbars>                        
                   
                        <f:Toolbar ID="toolbar_01" runat="server">
                          
                            <Items>
                                <f:DropDownList Width="100px"  ID="DL1" runat="server" LabelAlign="Left" >
                                </f:DropDownList>
                                <f:DropDownList Width="70px" ID="DL2" runat="server" LabelAlign="Left">
                                </f:DropDownList>
                              <f:Button ID="btnSelect" Text="查询" runat="server" OnClick="btnSelect_Click" Icon="Zoom"></f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>             
                        <f:TemplateField Width="55px" HeaderText="序号" TextAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex + 1 %>' ></asp:Label>
                                    </ItemTemplate>
                           </f:TemplateField>             
                       
                        <f:BoundField Width="150px" ColumnID="Program" SortField="Program" DataField="Program"
                                    TextAlign="Center" HeaderText="志愿活动"></f:BoundField>
                        <f:BoundField Width="90px" ColumnID="Date" SortField="Date" DataField="Date"
                                    TextAlign="Center" HeaderText="日期" DataFormatString="{0:yyyy-MM-dd}"></f:BoundField>
                        <f:BoundField Width="" ColumnID="Working_hours" SortField="Working_hours" DataField="Working_hours"
                                    TextAlign="Center" HeaderText="工时"></f:BoundField>
                    </Columns>
                </f:Grid>
            
    </form>
    <!--
         <f:BoundField Width="100px" ColumnID="SySe" SortField="SySe" DataField="SySe"
                                    TextAlign="Center" HeaderText="学年学期"></f:BoundField>
        -->

</body>
</html>
