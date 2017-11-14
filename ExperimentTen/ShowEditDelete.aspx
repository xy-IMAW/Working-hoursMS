<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowEditDelete.aspx.cs" Inherits="WHMS.ShowEditDelete" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<%--<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>--%>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="RegionPanel1" EnableFStateValidation="false" />
        <f:Panel ID="panel1" runat="server" >
        
            <Toolbars>
                <f:Toolbar runat="server">
                    <Items>
                           
               
                           <f:Button ID="Button1" runat="server" Text="导出" OnClick="Button2_Click" /> 
                         <f:Button ID="Button2" runat="server" Text="查看" OnClick="Button2_Click1" /> 
                    </Items>
                </f:Toolbar>               
            </Toolbars>
            <Items>
                <f:ContentPanel runat="server" Margin="50,,50,">
                    <div style="align-content:center; align-items:center; text-align:center">
                           <asp:GridView ID="GridView1" runat="server" Width="800px" Caption="工时信息" ShowFooter="true" ShowHeader="true" OnRowCreated="GridView1_RowCreated" EnableViewState="false" >
                               
                           </asp:GridView>
                 
                        <asp:Table ID="table1" runat="server" Width="70%" GridLines="Both" CellPadding="2" />
                    </div>                       
                </f:ContentPanel>                             
            </Items>           
        </f:Panel> 
        <f:Window ID="window1" Hidden="true" EnableIFrame="true" runat="server" EnableClose="true"  Width="800px" Height="600px" Target="Top"  EnableMaximize="true" ></f:Window>
                
    </div>      
        <div>
            <asp:Button ID="btn1" runat="server" Text="测试" OnClick="Button2_Click" />
        </div>
    </form>

</body>
</html>