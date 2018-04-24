<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClassData.aspx.cs" Inherits="WHMS.Infor_Data.ClassData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
                .mypanel {
            text-align: center;
            padding-top: 10px;
            margin-top: 10px;
            border-top: solid 1px #ccc;
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
               width:50px;
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
         <div class="mypanel">
            <asp:Button ID="btn1" runat="server" Text="导出" CssClass="mybutton" OnClick="Button2_Click" />
        </div>
                                       
    <f:PageManager ID="pagemanager1" runat="server"  EnableFStateValidation="false" />            
          <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="true" Title="" EnableCollapse="false" runat="server" 
            DataKeyNames="Id" AutoScroll="true" Width="1507PX">
            </f:Grid>
                                                                                 
                       
               
       </div>
    </form>
  </body>
</html>
