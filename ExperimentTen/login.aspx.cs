using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;

namespace WHMS
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Common.close();//确保数据库连接关闭
                           //重置所有关键字符的值
      
            if (Convert.ToString( Session["IsLogin"])=="false")
            {
                Alert.Show("请正常登录！", "警告", MessageBoxIcon.Warning);
            }

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string id = tbxStuID.Text;
            
            string sqlStr = "select * from Account where StuID='" + tbxStuID.Text + "'";
            Common.Open();
            SqlDataReader reader = Common.ExecuteRead(sqlStr);
            if (reader.Read())
            {
                string dbpassword = reader.GetString(reader.GetOrdinal("Password"));
                if (tbxPassword.Text == dbpassword)
                {                  
                    Alert.ShowInTop("成功登录！", "提示", MessageBoxIcon.Information);
                    Common.close();
               //     Common.ID = tbxStuID.Text;//绑定登陆者ID
                    Session["ID"] = tbxStuID.Text;//设置登录者idsession
                    SessionManager.setState(id);//设置登录者State Session
                    Account_Login();
                    Response.Redirect("Default_f.aspx");
                }
                else
                {
                    Common.close();          
                    Alert.ShowInTop("用户名或密码错误！", "错误", MessageBoxIcon.Error);
                }
            }
            else
            {

                Alert.ShowInTop("输入的用户名不存在！", "错误", MessageBoxIcon.Error);
                Common.close();
            }
        }

        public void Account_Login()
        {
            DateTime date = System.DateTime.Now;        
            string sql = "insert into Login (StuID,Date) values ('" + Session["ID"].ToString() + "','" + date + "')";
            Common.ExecuteSql(sql);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            tbxStuID.Text = "";
            tbxPassword.Text = "";
        }
    }
}
