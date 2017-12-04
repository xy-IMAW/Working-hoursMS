<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default_f.aspx.cs" Inherits="WHMS.fineUI_Index" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>青协工时统计系统</title>
     <link rel="shortcut icon" runat="server" href="~/image/logo.ico" />
    <style>
        #header table {
            width: 100%;
            border-spacing: 0;
            border-collapse: separate;
        }

            #header table td {
                padding: 0;
            }

        #header .title a
        {
            font-weight: bold;
            font-size: 24px;
            text-decoration: none;
            line-height: 50px;
            margin-left: 10px;
        }

        #header .pro a {
            font-weight: bold;
            font-size: 12px;
            text-decoration: none;
            line-height: 50px;
            margin-left: 10px;
        }

        #header .pro {
            position: absolute;
            top: 0;
            font-size:12px;
            right: 10px;
        }

        .bottomtable {
            width: 100%;
            font-size: 12px;
        }


        /* 主题相关样式 - neptune */
        .f-theme-neptune #header,
        .f-theme-neptune .bottomtable,
        .f-theme-neptune .x-splitter {
            background-color: #1475BB;
            color: #fff;
        }

            .f-theme-neptune #header a,
            .f-theme-neptune .bottomtable a {
                color: #fff;
            }

            .f-theme-neptune #header .x-btn-over.x-btn-default-small {
                background-color: #3487c3;
            }


        /* 主题相关样式 - blue/classic */
        .f-theme-classic #header,
        .f-theme-classic .bottomtable {
            background-color: #DFE8F6;
            color: #000;
        }

            .f-theme-classic #header a,
            .f-theme-classic .bottomtable a {
                color: #000;
            }

            .f-theme-classic #header .x-btn-over.x-btn-default-small {
                background-color: #e4f3ff;
            }


        /* 主题相关样式 - gray */
        .f-theme-gray #header,
        .f-theme-gray .bottomtable {
            background-color: #E0E0E0;
            color: #333;
        }

            .f-theme-gray #header a,
            .f-theme-gray .bottomtable a {
                color: #333;
            }

            .f-theme-gray #header .x-btn-over.x-btn-default-small {
                background-color: #f3f3f3;
            }


        /* 主题相关样式 - crisp */
        .f-theme-crisp #header,
        .f-theme-crisp .bottomtable,
        .f-theme-crisp .x-splitter {
            background-color: #E1E1E1;
            color: #000;
        }

            .f-theme-crisp #header a,
            .f-theme-crisp .bottomtable a {
                color: #000;
            }

            .f-theme-crisp #header .x-btn-inner-default-small {
                color: #000;
            }

            .f-theme-crisp #header .x-btn-over.x-btn-default-small .x-btn-inner-default-small {
                color: #fff;
            }

            .f-theme-crisp #header .x-btn-over.x-btn-default-small 
            {
                background-color: #3487c3;
            }


        /* 主题相关样式 - triton */
        .f-theme-triton #header,
        .f-theme-triton .bottomtable,
        .f-theme-triton .x-splitter 
        {
            background-color: #477aa6;
            color: #fff;
        }

            .f-theme-triton #header a,
            .f-theme-triton .bottomtable a 
            {
                color: #fff;
            }

            .f-theme-triton #header .x-btn-over.x-btn-default-small
             {
                background-color: #5795cb;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="RegionPanel1" runat="server"></f:PageManager>
        <f:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server">
            <Regions>
                <f:Region ID="Region1" ShowBorder="true" Height="50px" ShowHeader="true"
                    Position="Top" Layout="Fit" runat="server">
                    <Content>
                        <div id="header">
                            <table>
                                <tr>                                   
                                    <td>
                                        <div class="title">
                                            <a  href="./Default_f.aspx">工时管理系统</a>
                                        </div>
                                    </td>
                                    <td  style="text-align:center">
                                        <div class="pro">
                                        </div>
                                    </td>
                                      <td style="text-align: right;">
                                        <div class="pro">
                                            <a href="./login.aspx" >重新登录</a>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </Content>
                </f:Region>
                
                <f:Region ID="Region2" Split="true" Width="200px" ShowHeader="true" Title="菜单"
                    EnableCollapse="true" Layout="Fit" Position="Left" runat="server">
                    <Items>
                        <f:Tree runat="server" ShowBorder="false" ShowHeader="false" EnableArrows="true" EnableLines="true" ID="leftMenuTree" OnLoad="leftMenuTree_Load" >
                       <%--         <Nodes>
                              <f:TreeNode Text="菜单" Expanded="true">
                                 
                                 <f:TreeNode NodeID="node1"  Text="学生信息" NavigateUrl="~/Infor-Data/Infor.aspx"></f:TreeNode>
                                         <f:TreeNode NodeID="node3"  Text="工时信息" NavigateUrl="~/Infor-Data/Data.aspx"></f:TreeNode>
                                    <f:TreeNode NodeID="node2"  Text="账户管理" NavigateUrl=""></f:TreeNode>
                            
                                  
                                </f:TreeNode>
                            </Nodes>     --%>     
                        </f:Tree>
                    </Items>
                </f:Region>
                <f:Region ID="mainRegion" ShowHeader="false" Layout="Fit" Position="Center"
                    runat="server">
                    <Items>
                        <f:TabStrip ID="mainTabStrip" EnableTabCloseMenu="true" ShowBorder="false" runat="server">
                            <Tabs>
                                <f:Tab ID="Tab1" Title="首页" Layout="Fit" Icon="House" runat="server">
                                    <Items>
                                <f:RegionPanel runat="server" ShowBorder="false">
                                            <Regions>
                                                <f:Region runat="server" ShowBorder="false" Position="Left" Width="400px">
                                                    <Items>
                                                           <f:ContentPanel ID="ContentPanel2" ShowBorder="false" BodyPadding="10px" ShowHeader="false" AutoScroll="true"
                                            runat="server">

                                            <h1>工时信息管理</h1>
                                            
                                          <h2>  <f:Label ID="handler" Label="操作员" Text="" runat="server"></f:Label></h2>
                                            
                                            <br />
                                          <f:Button ID="btnPasswordUpdate" Text="修改密码" OnClick="btnPasswordUpdate_Click"  runat="server" Icon="BasketEdit"></f:Button>
                                            
                                        </f:ContentPanel>
                                                    </Items>
                                                </f:Region>
                                                <f:Region runat="server" ShowBorder="false">
                                                    <Items>
                                                           <f:Grid runat="server" ID="grid" ShowBorder="false" RegionPosition="Center" Hidden="true">
                                                            <Columns>
                                                                <f:TemplateField HeaderText="序号">
                                                                    <ItemTemplate>
                                                                          <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </f:TemplateField>
                                                                <f:BoundField runat="server" DataField="StuID" Width="200px" TextAlign="Center" HeaderText="学号" />
                                                                <f:BoundField runat="server" DataField="StuName" Width="100px" TextAlign="Center" HeaderText="姓名" />
                                                                <f:BoundField runat="server" DataField="State" Width="100px" TextAlign="Center" HeaderText="权限" />
                                                                <f:BoundField runat="server" DataField="Date" Width="200px" TextAlign="Center" HeaderText="登陆时间" />

                                                            </Columns>
                                                        </f:Grid>
                                             <%--           <f:ContentPanel runat="server" ShowBorder="false" RegionPosition="Center">
                                                            <asp:GridView runat="server" ID="gridview" AllowPaging="true" PageSize="20" OnPageIndexChanging="gridview_PageIndexChanging">
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                          <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="StuID" HeaderText="学号" />
                                                                    <asp:BoundField DataField="StuName" HeaderText="姓名" />
                                                                    <asp:BoundField DataField="State" HeaderText="权限" />
                                                                    <asp:BoundField DataField="Date" HeaderText="登陆时间" />

                                                                </Columns>
                                                            </asp:GridView>
                                                        </f:ContentPanel>
                                                        --%> 
                                                    </Items>
                                                </f:Region>
                                            </Regions>
                                        </f:RegionPanel>
                                    </Items>
                                </f:Tab>
                            </Tabs>
                        </f:TabStrip>
                    </Items>
                </f:Region>
                <f:Region ID="bottomPanel" RegionPosition="Bottom" ShowBorder="false" ShowHeader="false" EnableCollapse="false" runat="server" Layout="Fit">
                    <Items>
                        <f:ContentPanel ID="ContentPanel3" runat="server" ShowBorder="false" ShowHeader="false">
                            <table class="bottomtable">
                                <tr>
                                    <td style="width: 300px;">&nbsp;版本:2017.09.01&nbsp;&nbsp; </td>
                                    <td style="text-align: center;">Copyright  &copy;&nbsp; @IMAW 信息管理协会  2014-2017</td>
                                    <td style="width: 300px; text-align: right;">&nbsp;</td>
                                </tr>
                               
                            </table>
                        </f:ContentPanel>
                    </Items>
                </f:Region>
            </Regions>
        </f:RegionPanel>
        <f:Window ID="window1" Title="修改密码"  EnableCollapse="true" Hidden="true" EnableIFrame="true"  CloseAction="HidePostBack" EnableMaximize="true"
            EnableResize="false" EnableClose="true"   Target="Top" IsModal="true" Width="850px" Height="450px" runat="server">
        </f:Window>
    </form>
    <script>
        var menuClientID = '<%= leftMenuTree.ClientID %>';
        var tabStripClientID = '<%= mainTabStrip.ClientID %>';

        // 页面控件初始化完毕后，会调用用户自定义的onReady函数
        F.ready(function () {

            var treeMenu = F(menuClientID);
            var mainTabStrip = F(tabStripClientID);
            // 初始化主框架中的树(或者Accordion+Tree)和选项卡互动，以及地址栏的更新
            // treeMenu： 主框架中的树控件实例，或者内嵌树控件的手风琴控件实例
            // mainTabStrip： 选项卡实例
            // updateHash: 切换Tab时，是否更新地址栏Hash值（默认值：true）
            // refreshWhenExist： 添加选项卡时，如果选项卡已经存在，是否刷新内部IFrame（默认值：false）
            // refreshWhenTabChange: 切换选项卡时，是否刷新内部IFrame（默认值：false）
            // maxTabCount: 最大允许打开的选项卡数量
            // maxTabMessage: 超过最大允许打开选项卡数量时的提示信息
            F.initTreeTabStrip(treeMenu, mainTabStrip, {
                maxTabCount: 10,
                maxTabMessage: '请先关闭一些选项卡（最多允许打开 10 个）！'
            });

        });
    </script>
</body>
</html>