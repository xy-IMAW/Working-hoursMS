<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Class.aspx.cs" Inherits="WHMS.Infor_Data.Class" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <style>
                .mypanel {
        
        }

            .mypanel .mybutton {
             display: inline-block;
    padding: .3em .5em;
  background: -webkit-linear-gradient(top,#42a4e0,#2e88c0);
background: -moz-linear-gradient(top,#42a4e0,#2e88c0);
background: linear-gradient(top,#42a4e0,#2e88c0);
    border: 1px solid rgba(0,0,0,.2);
    border-radius: .3em;
    box-shadow: 0 1px white inset;
    text-align: center;
    text-shadow: 0 1px 1px black;
    color:white;
    font-weight: bold;
               width:150px;
            }
            .mybutton:active{
                box-shadow: .05em .1em .2em rgba(0,0,0,.6) inset;
    border-color: rgba(0,0,0,.3);
    background: #bbb;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="RegionPanel1" EnableFStateValidation="false" />
        <f:Panel ID="panel1" runat="server" EnableCollapse="true" ShowHeader="false">
            <Items>
                <f:Panel ID="panel11" runat="server" Title="班级添加/工时导入" ShowHeader="true" ShowBorder="true">
                    <Toolbars>
                        <f:Toolbar ID="tool1" runat="server" >
                            <Items>                                
                                   <f:TextBox ID="txtClassName" runat="server" Label="班级" Text="" ShowRedStar="true" LabelAlign="Right" Required="true" Width="300px"></f:TextBox>                                    
                            </Items>  
                              <Items>
                                 <f:TextBox ID="txtGrade" runat="server" Label="年级" Text="" ShowRedStar="true" LabelAlign="Right" Required="true" AbsoluteX="200px"></f:TextBox>
                                    <f:Button ID="btnAddClass" Icon="Add" runat="server" Text="新增" OnClick="btnAddClass_Click" AbsoluteX="600px"></f:Button>
                                  <f:Button ID="Import" Icon="PageGo" Text="导入班级" runat="server" CssClass="mybutton" AbsoluteX="500px"></f:Button>
                             <f:ContentPanel runat="server">
                                 <div class="mypanel">
                                 <asp:Button ID="DowmLoad" Text="下载班级模板" runat="server" CssClass="mybutton" OnClick="DownLoad_Click"/>
                                 </div>
                             </f:ContentPanel>
                            </Items>                         
                        </f:Toolbar>
                        <f:Toolbar ID="tool2" runat="server"  >
                         <Items>
                             <f:Button ID="btnDownLoad" Text="下载工时模板" Icon="Accept" runat="server" OnClick="btnDownLoad_Click" AbsoluteX="200px"></f:Button>
                              <f:Button ID="btnImport" Icon="PageGo" runat="server" Text="导入工时" />
                         </Items>
                            
                     </f:Toolbar>  
                    </Toolbars>
                </f:Panel>
            </Items>
        </f:Panel>
        <f:Panel ID="panel2" runat="server" EnableCollapse="true" Title="班级信息" ShowHeader="true" ShowBorder="true">            
                 <Toolbars >
                                <f:Toolbar ID="toolbar1" runat="server">
                                    <Items>                                                                                                                  
                                   <f:DropDownList ID="SelectGrade" runat="server" Label="年级" LabelAlign="Right" ></f:DropDownList>
                                        <f:Button ID="btnSearch" runat="server" Text="查找" Icon="Zoom" OnClick="btnSearch_Click"></f:Button>  
                                        <f:Button ID="btnDeleteClass" Icon="Delete" runat="server" Text="删除" ConfirmTitle="注意" ConfirmIcon="Question" ConfirmText="确认删除？" ConfirmTarget="Self" OnClick="btnDeleteClass_Click"></f:Button>                                     
                                     </Items>
                                </f:Toolbar>                                              
                    </Toolbars>         
            <Items>
                 <f:Grid ID="Grid3" Height="550px" BoxFlex="1" ShowBorder="false" ShowGridHeader="true"  runat="server" DataKeyNames="Id,Name" EnableRowClickEvent="true" BodyPadding="0px">
                     <Toolbars>
                               <f:Toolbar ID="toolbar2" runat="server">
                         <Items>
                              <f:DropDownList ID="DL3" runat="server" Label="学期" Required="true" LabelAlign="Right" >
                                </f:DropDownList>
                             
                        <f:Button ID="btn" runat="server" Icon="ApplicationViewColumns" Text="查看班级工时" OnClick="btn_Click"/>
                        <f:Button ID="btn2" runat="server" Icon="ApplicationViewDetail" Text="查看年级工时" OnClick="btn2_Click"/>

                        
                              
                         </Items>                                
                     </f:Toolbar>    
                     </Toolbars>   
                        <Columns>
                                <f:TemplateField Width="60px" HeaderText="序号" TextAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:BoundField Width="100px"  DataField="Grade" HeaderText="年级" TextAlign="Center" />
                                <f:BoundField Width ="100px" DataField="Class" HeaderText="班级" TextAlign="Center" />                              
                            </Columns>
                  </f:Grid>    
            </Items>                   
        </f:Panel>           
             <f:Window ID="window1" Hidden="true" EnableIFrame="true" runat="server" EnableClose="true"  Width="800px" Height="600px" Target="Top"  EnableMaximize="true" ></f:Window>

    </div>
    </form>
</body>
</html>
