using System;
using System.Data;
using FineUI;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace WHMS.Infor_Data
{
    public partial class Program : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDDL();//学期下拉框
                bind();
            }
         
        }

        #region DataBind
        //学期绑定到两个下拉框
        public void BindDDL()
        {
            int year = DateTime.Now.Year;
            int year2 = DateTime.Now.Year + 1;
            if (DateTime.Now.Month < 9)
            {
             

                for (int i = 1; i < 5; i++)
                {
                    ListItem li = new ListItem();
                    li.Text = li.Value =(--year).ToString() + "-" + (--year2).ToString();
                    DDL.Items.Add(li);
                    selectSy.Items.Add(li);
                }
            }
            else
            {
              

                for (int i = 1; i < 5; i++)
                {
                    ListItem li = new ListItem();
                    li.Text = li.Value = (year--).ToString() + "-" + (year2--).ToString();
                    DDL.Items.Add(li);
                    selectSy.Items.Add(li);
                }
            }

            List<string> list2 = new List<string>();
            list2.Add("1");
            list2.Add("2");
            selectSe.DataSource = list2;
            selectSe.DataBind();

        }
        //活动总表数据绑定，活动下拉框数据
        protected void bind()
        {
            string sqlstr = "select ProgramName from ProgramSummary";
            DataTable dt = Common.datatable(sqlstr);
            Grid1.DataSource = dt;
            Grid1.DataBind();
            bind2();
         
         
        }
        //活动下拉框数据
        protected void bind2()
        {
            selectPro.Items.Clear();//先清空选项/为啥选项都是表的最后一条记录
            string sqlstr = "select ProgramName from ProgramSummary";
            Common.Open();
         
            SqlDataReader read = Common.ExecuteRead(sqlstr);
            while (read.Read())
            {
                ListItem li = new ListItem();
                li.Text = li.Value = read["ProgramName"].ToString();
                selectPro.Items.Add(li);
            }
            Common.close();

        }
        //查询活动
        public void Bind()
        {
            string year = DDL.SelectedItem.Value;
            
            string sqlstr = "select Program,SySe,Date from ProgramList where SySe like '" + year + "%'";
            DataTable dt = Common.datatable(sqlstr);
            gridExample.DataSource = dt;
            gridExample.DataBind();
        }
        #endregion     

        #region event
        //查询某学期的活动
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Bind();
        }
        //页面编号
        protected void gridExample_PageIndexChange(object sender, GridPageEventArgs e)
        {
            gridExample.PageIndex = e.NewPageIndex;
        }
        //添加活动
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //查重
            bool flag = true;//标志是否重复
        
                string sqlstr = "select Program from ProgramList where SySe like '" + selectSy.SelectedItem.Text + "%'";
                Common.Open();
                SqlDataReader reader = Common.ExecuteRead(sqlstr);
            while (flag)
            {
                int a = 0;//标志是否进行了下面的循环
                while (reader.Read())
                {
                    string name = reader.GetString(reader.GetOrdinal("Program"));
                    if (name == selectPro.SelectedItem.Text)
                    {

                        Common.close();
                        flag = false;//该学年有重复
                        Alert.Show("该活动已存在！", "错误", MessageBoxIcon.Error);
                        break;
                    }
                    a++;
                }
                if (a == 0)
                {
                    break;//没有重复
                }
            }
                if (flag)//不重复
                {
                    Common.close();
                    string sqlstr2 = "insert into ProgramList (Program,SySe,Date) values ('" + selectPro.SelectedItem.Text + "','" + selectSy.SelectedItem.Text + "-" + selectSe.SelectedItem.Text + "','" + date.SelectedDate.Value + "')";
                    Common.ExecuteSql(sqlstr2);
                    Alert.Show("添加成功", "提示信息", MessageBoxIcon.Information);
                }
                else//重复
                {
                    Common.close();//任何情况结束后都要关闭连接
                    Alert.Show("该活动已在活动表中", "错误", MessageBoxIcon.Error);
                }
            
        }
        //删除活动
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string sqlstr = "delete from ProgramList where Program='" + gridExample.SelectedRow.Values[1] + "'and Date='" + gridExample.SelectedRow.Values[3] + "'";
            Common.ExecuteSql(sqlstr);
            Bind();
            Alert.Show("删除成功", "信息", MessageBoxIcon.Information);
        }
        //添加活动到总表
        protected void btnAddProgram_Click(object sender, EventArgs e)
        {
            bool flag = false;
            string sqlstr = "select ProgramName from ProgramSummary";
            DataTable dt = Common.datatable(sqlstr);
            foreach (DataRow dr in dt.Rows)
            {
                if (dr.ToString() == txtProgram.Text)
                {
                    flag = true;
                }
                else
                { }
            }
            if (flag)
            {
                Alert.Show("该活动已在表中", "错误", MessageBoxIcon.Error);
            }
            else
            {
                string sqlstr2 = "insert into ProgramSummary (ProgramName) values ('" + txtProgram.Text + "')";
                Common.ExecuteSql(sqlstr2);
                Alert.Show("添加成功", "提示", MessageBoxIcon.Information);
                bind();
            }
        }
        //从总表删除
        protected void btnDeleteProgram_Click(object sender, EventArgs e)
        {
            string sqlstr = "delete from ProgramSummary where ProgramName='" + Grid1.SelectedRow.Values[1].ToString() + "'";
            Common.ExecuteSql(sqlstr);
            bind();
            Alert.Show("删除成功", "信息", MessageBoxIcon.Information);
        }
        #endregion
    }
}