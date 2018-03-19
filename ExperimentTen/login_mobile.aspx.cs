using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace WHMS
{
    public partial class login_mobile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("<script>alert('学号仅能绑定一次，请谨慎核对！');</script>");
            Common.close();//确保数据库连接关闭
                           //重置所有关键字符的值
            if (Convert.ToString(Session["IsLogin"]) == "false")
            {
                Response.Write("<script>alert('请正常登录！');</script>");
            }
            else {
                HttpContext httpcon = HttpContext.Current;
                UserAuth u1 = new UserAuth();
                u1.ProcessRequest(httpcon);
            }
        }



        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string YiBanID = Session["YiBanID"].ToString();
            string sqlStr = "select * from Student where StuID='" + tbxStuID.Text + "'";
            Common.Open();
            SqlDataReader reader = Common.ExecuteRead(sqlStr);
            if (reader.Read())
            {
               // string dbpassword = reader.GetString(reader.GetOrdinal("Password"));
                    if (YiBanID != null)
                    {
                        Common.close();
                        string sql = "UPDATE Student SET Ybid='" + Session["YiBanID"].ToString() + "' WHERE StuID='" + tbxStuID.Text + "'";
                        Common.ExecuteSql(sql);
                        Session["ID"] = tbxStuID.Text;
                        Account_Login();
                        reader.Close();
                        Response.Redirect("data_yb.aspx");
                    }
                    else
                    {
                        Common.close();
                        reader.Close();
                        Response.Write("<script>alert('请从易班端口进入！');</script>");
                    }
            }
            else
            {
                reader.Close();
                Response.Write("<script>alert('输入的学号不存在！');</script>");
                Common.close();
            }
        }
        public static void Account_Login()
        {
            DateTime date = System.DateTime.Now;
            string sql = "insert into Login (StuID,Date) values ('" + Common.ID + "','" + date + "')";
            Common.ExecuteSql(sql);
        }
    }
}