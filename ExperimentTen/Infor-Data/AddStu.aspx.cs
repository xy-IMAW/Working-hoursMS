using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;
using System.Data.SqlClient;

namespace WHMS.Infor_Data
{
    public partial class AddStu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Common.checklogin("../login.aspx");
        }
        #region 添加学生
        protected void btnAdd_Click(object sender, EventArgs e)
        {
           
          

            bool flag = true;//标志是否账号已存在
            if ( txtStuID.Text==""||txtStuName.Text==""||txtClass.Text=="")
            {
                Alert.Show("请填写需要添加的学生信息", "提示", MessageBoxIcon.Warning);
            }
            else
            {
                string sqlstr = "select StuID from Student";
                Common.Open();
                SqlDataReader reader = Common.ExecuteRead(sqlstr);
                while (flag)
                {
                    int a = 0;//标志是否进行了下面的循环
                    while (reader.Read())
                    {
                        string id = reader.GetString(reader.GetOrdinal("StuID"));
                        if (id == txtStuID.Text)
                        {

                            Common.close();
                            flag = false;//该账号已被设置
                           txtStuID.Text = "";
                           txtStuID.Focus();
                            break;
                        }
                        a++;
                    }
                    if (a == 0)
                    {
                        break;
                    }
                }
                if (flag)
                {
                    Common.close();
                    string sqlstr2 = "insert into Student (StuID,StuName,Class) values ('" + txtStuID.Text + "','" + txtStuName.Text + "','" + txtClass.Text + "')";
                    Common.ExecuteSql(sqlstr2);
                    Alert.Show("添加成功", "提示信息", MessageBoxIcon.Information);
                }
                else
                {
                    Common.close();//任何情况结束后都要关闭连接
                    Alert.Show("该学生已在学生表中", "提示", MessageBoxIcon.Error);
                }
            }
        }
        #endregion
    }
}