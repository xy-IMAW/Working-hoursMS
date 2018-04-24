using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using FineUI;
using System.Text;

namespace WHMS
{
    public partial class test : System.Web.UI.Page
    {
        #region
        protected void Page_Init(object sender, EventArgs e)
        {
            InitGrid();
        }

        public void InitGrid()
        {
            string sql3 = "select distinct  Program from Program where SySe='2017-2018-1' order by Program";
            DataTable program = Common.datatable(sql3);
            FineUI.BoundField bf;//数据绑定域
            FineUI.GroupField gf;//跨列表头

            bf = new FineUI.BoundField();
            bf.DataField = "StuID";
            bf.HeaderText = "学号";
            Grid1.Columns.Add(bf);

            bf = new FineUI.BoundField();
            bf.DataField = "StuName";
            bf.HeaderText = "姓名";
            Grid1.Columns.Add(bf);

            bf = new FineUI.BoundField();
            bf.DataField = "Class";
            bf.HeaderText = "班级";
            Grid1.Columns.Add(bf);

            for(int i = 0; i < program.Rows.Count; i++)
            {
                string sql = "select Date  from Program where SySe = '2017-2018-1' and Program = '" + program.Rows[i][0].ToString() + "' order by Date";
                DataTable date = Common.datatable(sql);
                gf = new GroupField();
                gf.HeaderText = program.Rows[i][0].ToString();
                for (int j = 0; j < date.Rows.Count; j++)
                {
                    bf = new FineUI.BoundField();
                    bf.DataField = program.Rows[i][0].ToString() + date.Rows[j][0].ToString();
                    DateTime time = Convert.ToDateTime(date.Rows[j][0].ToString()).Date;
                    bf.HeaderText = time.ToString("yyyy-MM-dd");
                    bf.TextAlign = FineUI.TextAlign.Center;
                    bf.DataFormatString = "{0:yyyy-MM-dd}";
                    gf.Columns.Add(bf);
                }
                Grid1.Columns.Add(gf);
            }
            bf = new FineUI.BoundField();
            bf.DataField = "合计";
            bf.BoxFlex = 1;
            bf.HeaderText = "合计";
            Grid1.Columns.Add(bf);
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["SySe"] = "2017-2018-1";
            BindGrid();
        }

        public void GetData(DataTable data)
        {
            DataRow dr;
            string sql1 = "select * from [Working_hoursInfor] where SySe like '%2017-2018-1%'";
            //查询出该班级学生学生信息
            string sql2 = "select StuID,StuName,Class from Student where Class like '%电气1701%' order by StuID";
            //查询出该学期活动信息
            string sql3 = "select distinct Program,Date from Program where SySe like '%2017-2018-1%' order by Program,Date";

            DataTable work = Common.datatable(sql1);
            DataTable student = Common.datatable(sql2);
            DataTable program = Common.datatable(sql3);

            data.Columns.Add("StuID", typeof(string));
            data.Columns.Add("StuName", typeof(string));
            data.Columns.Add("Class", typeof(string));

            for (int i = 0; i < program.Rows.Count; i++)
            {
                data.Columns.Add(program.Rows[i][0].ToString() + Convert.ToDateTime( program.Rows[i][1]).Date.ToString(), typeof(string));
            }

            data.Columns.Add("合计", typeof(string));

            //为输出表添加行
            for (int i = 0; i < student.Rows.Count; i++)//遍历每个学生
            {
                dr = data.NewRow();
                dr[0] = student.Rows[i][0].ToString();//学号
                dr[1] = student.Rows[i][1].ToString();//姓名
                dr[2] = student.Rows[i][2].ToString();//班级

                int total = 0;
                for (int j = 0; j < program.Rows.Count; j++)
                {
                    for (int t = 0; t < work.Rows.Count; t++)
                    {
                        string t1 = work.Rows[t][0].ToString();//总工时表学号
                        string t2 = student.Rows[i][0].ToString();//学生表学号
                        string t3 = work.Rows[t][2].ToString();//总工时表活动名
                        string t4 = program.Rows[j][0].ToString();//活动表活动名（这里已经修改过）

                        if (work.Rows[t][0].ToString() == student.Rows[i][0].ToString() && work.Rows[t][2].ToString() == program.Rows[j][0].ToString() && program.Rows[j][1].ToString() == work.Rows[t][5].ToString())
                        {
                            // dr[program.Rows[j][0].ToString()]= work.Rows[t][3].ToString();
                            dr[3 + j] = work.Rows[t][3].ToString();
                            total += Convert.ToInt32(work.Rows[t][3].ToString());
                        }
                    }
                }
                dr["合计"] = total;
                data.Rows.Add(dr);

            }

        }

        public void BindGrid()
        {
            DataTable work = new DataTable();
            GetData(work);
            Grid1.DataSource = work;
            Grid1.DataBind(); 
        }



        protected void btnout_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            GetData(dt);        
            NPOIHelper.ExportByWeb(dt, "asd", "asd");
           
        }
       
    }
}