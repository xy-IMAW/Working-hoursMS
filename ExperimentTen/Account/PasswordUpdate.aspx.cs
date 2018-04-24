using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;
using System.Data.SqlClient;

namespace WHMS.Account
{
    public partial class Update : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SessionManager.CheckLogin("../login.aspx");
                labStuID.Text = Session["ID"].ToString();
                if (Convert.ToString(Session["State"]) != "")
                {
                    LabState.Text = Session["State"].ToString();
                }
                else
                {
                    Alert.Show("管理员未设置权限？！");
                }
                string sqlstr = "select StuName from Student where StuID = '" + Session["ID"].ToString() + "'";
                Common.Open();
                SqlDataReader re = Common.ExecuteRead(sqlstr);
                if (re.Read())
                {
                    LabeName.Text = re.GetString(re.GetOrdinal("StuName"));
                }
                Common.close();
               
                
            }
        }
      

        protected void btnClose_Click(object sender, EventArgs e)
        {
            if (txtPwd1.Text == "")
            {

                Alert.Show("没有进行修改", "提示", MessageBoxIcon.Information);
            }
            else
            {
                if (txtPwd1.Text.Length > 13)
                {
                    txtPwd1.Reset();
                    Alert.Show("密码最大长度为13位", "警告", MessageBoxIcon.Warning);
                }
                else
                {
                    if (txtPwd1.Text == txtPwd2.Text)
                    {
                        string sqlstring = "update Account set Password ='" + txtPwd2.Text + "' where StuID ='" + Session["ID"]+"'";
                        Common.ExecuteSql(sqlstring);
                        Alert.Show("修改成功", "提示", MessageBoxIcon.Information);
                      

                    }
                    else
                    {
                        txtPwd2.Reset();
                        Alert.Show("密码不一致", "警告", MessageBoxIcon.Warning);

                    }
                }
            }
       
        }
    }
}