<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Program.aspx.cs" Inherits="WHMS.Infor_Data.Program" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager_01" AutoSizePanelID="RegionPanel1" runat="server" EnableFStateValidation="false" />
        <f:RegionPanel ID="RegionPanel1" runat="server" ShowBorder="false">
            <Regions>
                <f:Region ID="LeftRegion1" ShowBorder="true" ShowHeader="true"
                    Width="300px" RegionPosition="Left" Layout="Fit" Title="活动总表"
                    runat="server">   
                    <Toolbars>
                        <f:Toolbar ID="tool1" runat="server" >
                            <Items>
                                <f:TextBox ID="txtProgram" runat="server" Label="活动名" LabelAlign="Right" Required="true"></f:TextBox>
                            </Items>
                          </f:Toolbar>
                        <f:Toolbar ID="tool2" runat="server" ToolbarAlign="Right">  
                            <Items>
                                <f:Button ID="btnAddProgram" runat="server" Text="添加" Icon="Add" OnClick="btnAddProgram_Click" BoxConfigAlign="Center"></f:Button>
                             <f:Button ID="btnDeleteProgram" runat="server" Text="删除" Icon="Delete" OnClick="btnDeleteProgram_Click" RegionPosition="Center" ConfirmTitle="注意" ConfirmIcon="Question" ConfirmText="确认删除？"></f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Items>
                        <f:Grid ID="Grid1" runat="server" ShowBorder="false" ShowHeader="false" >
                            <Columns>
                                <f:TemplateField TextAlign="Center" HeaderText="序号">
                                    <ItemTemplate>
                                          <asp:Label ID="Label2" runat="server" Text='<%# Container.DataItemIndex + 1 %>' Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </f:TemplateField>
                             <f:BoundField Width="200px" ColumnID="ProgramName" SortField="ProgramName" DataField="ProgramName" TextAlign="Center" HeaderText="活动"></f:BoundField>
                            </Columns>
                        </f:Grid>
                    </Items>
                </f:Region>
                <f:Region ID="Region2" ShowBorder="true" ShowHeader="true" Position="Center" Title=""
                    Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Left" runat="server">
                    <Items>
                          <f:Panel ID="panel1" runat="server" ShowBorder="true" ShowHeader="true" EnableCollapse="true" Title="本学期活动添加">
                     <Items>
                <f:Form ID="form2" runat="server">
                    <Toolbars>
                        <f:Toolbar ID="toolbar1" runat="server">
                            <Items>
                            <f:DropDownList ID="selectSy" Label="学年" LabelAlign="Right" runat="server" Required="true" ></f:DropDownList>
                                  <f:DropDownList ID="selectSe" Label="学期" LabelAlign="Right" runat="server" Required="true"></f:DropDownList>
                             </Items>
                         </f:Toolbar>
                        <f:Toolbar ID="toolbar2" runat="server">
                            <Items>
                                  <f:DropDownList ID="selectPro" Text="" Label="活动选择" LabelAlign="Right" runat="server" Required="true" Width="300px" EnableEdit="false" MatchFieldWidth="true" ></f:DropDownList>
                                  <f:DatePicker ID="date" Label="日期" LabelAlign="Right" runat="server" Required="true" ></f:DatePicker>
                                 <f:Button ID="btnAdd" Text="新增" Icon="Add" runat="server" OnClick="btnAdd_Click" ></f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                </f:Form>
            </Items>
            </f:Panel>
        <f:Panel ID="panel2" runat="server" ShowBorder="true" ShowHeader="true" EnableCollapse="true" Layout="Fit" Title="活动信息">
              <Toolbars>
                        <f:Toolbar ID="toolbar_01" runat="server">
                            <Items>
                                <f:DropDownList ID="DDL" runat="server" Label="学期" LabelAlign="Right" Width="180px" LabelWidth="50px"></f:DropDownList>
                                <f:Button ID="btnSearch" Text="查看" Icon="Zoom" runat="server" OnClick="btnSearch_Click"></f:Button>                               
                                <f:Button ID="btnDelete" Text="删除" Icon="Delete" runat="server" OnClick="btnDelete_Click" ConfirmTitle="注意" ConfirmIcon="Question" ConfirmText="确认删除？"></f:Button>    
                                <f:Button runat="server" ID="btnSearch_hours" Text="查看工时" Icon="ApplicationViewList" OnClick="btnSearch_hours_Click"/>                                                   
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
            <Items>
                <f:Grid ID="gridExample" ShowBorder="false" AllowPaging="true" ShowHeader="false"
                    DataKeyNames="ID" EnableCollapse="false" EnableCheckBoxSelect="true" PageSize="20"  PageIndex="0" OnPageIndexChange="gridExample_PageIndexChange"
                    EnableMultiSelect="false"  runat="server" Height="500px">
                  
                    <Columns>
                        <f:TemplateField Width="80px" TextAlign="Center" HeaderText="序号">
                            <ItemTemplate>
                               <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:BoundField Width="100px" ColumnID="Program" SortField="Program" DataField="Program"
                                    TextAlign="Center" HeaderText="活动"></f:BoundField>
                          <f:BoundField Width="100px" ColumnID="SySe" SortField="SySe" DataField="SySe"
                                    TextAlign="Center" HeaderText="学期"></f:BoundField>
                        <f:BoundField Width="120px" ColumnID="Date" SortField="Date" DataField="Date"
                                    TextAlign="Center" HeaderText="日期"></f:BoundField>
                      
                    </Columns>
                </f:Grid>
            </Items>
        </f:Panel>
             <f:Window ID="window1" Hidden="true" EnableIFrame="true" runat="server" EnableClose="true"  Width="800px" Height="600px" Target="Top"  EnableMaximize="true" ></f:Window>

                    </Items>
                </f:Region>
            </Regions>
        </f:RegionPanel>
        
      
        
    </form>
</body>
</html>
