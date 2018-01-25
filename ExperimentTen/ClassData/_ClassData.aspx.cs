using System;
using System.Data;
using FineUI;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.HSSF.UserModel;
//using System.Windows.Forms;
using System.Web.UI;
using AspNet = System.Web.UI.WebControls;
using System.Collections.Generic;
//using System.Windows.Forms;


namespace WHMS.ClassData
{
    public partial class _ClassData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bind();
                lab.Text = Session["Class"] + "工时查询";
                stuBind();         
            }
        }

        //日期下拉框数据绑定
      public  void bind()
        {
         int   year = DateTime.Now.Year;
            int year2 = DateTime.Now.Year + 1;
            if (DateTime.Now.Month < 9)
            {
                List<string> list = new List<string>();

                for (int i = 1; i < 5; i++)
                {
                    string y1 = (--year).ToString();
                    string y2 = (--year2).ToString();

                    FineUI.ListItem li = new FineUI.ListItem();
                    li.Text = li.Value = (y1).ToString() + "-" + (y2).ToString() + "-1";
                    DL1.Items.Add(li);

                    li = new FineUI.ListItem();
                    li.Text = li.Value = (y1).ToString() + "-" + (y2).ToString() + "-2";
                    DL1.Items.Add(li);
                }
            }
            else
            {
                List<string> list = new List<string>();

                for (int i = 1; i < 5; i++)
                {
                    string y1 = (year--).ToString();
                    string y2 = (year2--).ToString();

                    FineUI.ListItem li = new FineUI.ListItem();
                    li.Text = li.Value = (y1).ToString() + "-" + (y2).ToString() + "-1";
                    DL1.Items.Add(li);

                    li = new FineUI.ListItem();
                    li.Text = li.Value = (y1).ToString() + "-" + (y2).ToString() + "-2";
                    DL1.Items.Add(li);
                }
            }
            DL1.SelectedIndex = 0;
        }

        //班级信息
        public void stuBind()
        {
            //获取组织委员的班级信息
            string sqlstr = "select Class from Student where StuID='"+Session["ID"]+"'";
            DataTable d = Common.datatable(sqlstr);
            Session["Class"] = d.Rows[0][0].ToString();
            string sql = "select * from Student where Class ='" + Session["Class"] + "'";
            DataTable dt = Common.datatable(sql);
            Grid1.DataSource = dt;
            Grid1.DataBind();
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            if (DL1.SelectedIndex < 0)
            {
                Alert.Show("请选择要查看的学期", "提示", MessageBoxIcon.Warning);
            }
            else
            {
                Common.SySe = DL1.SelectedText.ToString();
                PageContext.RegisterStartupScript(window1.GetShowReference("~/Infor-Data/ClassData.aspx"));
            }
        }

        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
        }
    }
}