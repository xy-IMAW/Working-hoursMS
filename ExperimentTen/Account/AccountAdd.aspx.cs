using FineUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Windows;
using Microsoft.Office.Interop;
using Microsoft.Office.Core;
using System.Data.SqlClient;

namespace WHMS.Account
{
    public partial class AccountAdd : System.Web.UI.Page
    {
        public string stuid;
        public string stuname;
        protected void Page_Load(object sender, EventArgs e)
        {
            Common.checklogin("../login.aspx");
        }
        protected void btnselect_Click(object sender, EventArgs e)
        {

            string Sqlstring = "select StuID,StuName,Class,Grade from Student where StuID= '" + txbStuID.Text + "'";
            DataSet ds = Common.dataSet(Sqlstring);
            DataTable dt = ds.Tables[0];
            if (ds == null)
            {
                Alert.Show("没有该学号");//未被执行
            }
            else
            {
                grid2.DataSource = dt;
                grid2.DataBind();
            }
        }

        protected void btnAdd2_Click(object sender, EventArgs e)
        {
            bool flag = true;//标志是否账号已被设置
            if (grid2.SelectedRowIndex < 0)
            {
                Alert.Show("请选择要添加的账户","提示",MessageBoxIcon.Warning);
            }
            else
            {
                stuid = grid2.SelectedRow.Values[0].ToString();//选中的第一列的值，即学号ID
                stuname = grid2.SelectedRow.Values[1].ToString();
                string sqlstr = "select StuID from Account";
                Common.Open();
                SqlDataReader reader = Common.ExecuteRead(sqlstr);
                while (flag)
                {
                    int a = 0;//标志是否进行了下面的循环
                    while (reader.Read())
                    {
                        string id = reader.GetString(reader.GetOrdinal("StuID"));
                        if (id == stuid)
                        {
                            
                            Common.close();
                            flag = false;//该账号已被设置
                            txbStuID.Text = "";
                            txbStuID.Focus();
                            break;
                        }
                        a++;
                    }
                    if (a == 0)
                    {
                        break;//账户没有被设置
                    }
                }
                if (flag)
                {
                    Common.close();
                    string sqlStr = "insert into Account (StuID,Password,State) values ( '" + stuid + "','"+stuid+"','"+drop.SelectedItem.Value.ToString()+ "') ";
                    Common.ExecuteSql(sqlStr);
                    Alert.ShowInParent("添加成功","提示",MessageBoxIcon.Information);
                }
                else
                {
                    Common.close();//任何情况结束后都要关闭连接
                    Alert.Show("该账号已被设置为管理员","提示",MessageBoxIcon.Error);
                }
            }
        }

    }
}