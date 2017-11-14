using System;
using FineUI;
using System.Data.SqlClient;

namespace WHMS.Infor_Data
{
    public partial class AddClass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Common.checklogin("../login.aspx");
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {



            bool flag = true;//标志是否账号已存在
            if (txtClass.Text == "" || txtGrade.Text == "" )
            {
                Alert.Show("请填写需要添加的班级", "提示", MessageBoxIcon.Warning);
            }
            else
            {
                string sqlstr = "select Class from Class";
                Common.Open();
                SqlDataReader reader = Common.ExecuteRead(sqlstr);
                while (flag)
                {
                    int a = 0;//标志是否进行了下面的循环
                    while (reader.Read())
                    {
                        string id = reader.GetString(reader.GetOrdinal("Class"));
                        if (id == txtClass.Text)
                        {

                            Common.close();
                            flag = false;//该班级已被添加
                            txtClass.Text = "";
                            txtClass.Focus();
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
                    string sqlstr2 = "insert into Class (Class,Grade) values ('" + txtClass.Text + "','" + txtGrade.Text + "')";
                    Common.ExecuteSql(sqlstr2);
                    Alert.Show("添加成功", "提示信息", MessageBoxIcon.Information);
                }
                else
                {
                    Common.close();//任何情况结束后都要关闭连接
                    Alert.Show("该班级已在表中", "提示", MessageBoxIcon.Error);
                }
            }
        }
    }
}