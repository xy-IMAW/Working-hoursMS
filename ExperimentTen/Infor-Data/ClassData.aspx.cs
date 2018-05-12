using System;
using System.Web.UI.WebControls;
using System.Data;
using FineUI;
using System.Data.SqlClient;

namespace WHMS.Infor_Data
{
    public partial class ClassData : System.Web.UI.Page
    {

        #region init Grid
        protected void Page_Init(object sender, EventArgs e)
        {
            InitGrid();
        }

        public void InitGrid()
        {
            string sql3 = "select distinct  Program from Program where SySe='" + Session["SySe"].ToString() + "' order by Program";
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

            for (int i = 0; i < program.Rows.Count; i++)
            {
                string sql = "select Date  from Program where SySe = '" + Session["SySe"].ToString() + "' and Program = '" + program.Rows[i][0].ToString() + "' order by Date";
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
            if (!IsPostBack)
            {
                SessionManager.CheckLogin("../login.aspx");
                BindGrid();
            }
            Grid1.Title = Session["Class"].ToString() + "班\t" + Session["SySe"].ToString() + "学期工时表";
        }

        public void BindGrid()
        {
            DataTable work = new DataTable();
            GetData(work);
            Grid1.DataSource = work;
            Grid1.DataBind();
        }

        //获取输出数据
        public void GetData(DataTable data)
        {
            // DataTable data = new DataTable();
            DataRow dr;
            //查询出该学期所有工时信息
            string sql1 = "select * from [Working_hoursInfor] where SySe like '%" + Session["SySe"].ToString() + "%'";
            //查询出该班级学生学生信息
            string sql2 = "select StuID,StuName,Class from Student where Class like '%" + Session["Class"].ToString() + "%' order by StuID";
            //查询出该学期活动信息
            string sql3 = "select distinct Program,Date from [Working_hoursInfor] where SySe like '%" + Session["SySe"].ToString() + "%'order by Program,Date";
            DataTable work = Common.datatable(sql1);
            DataTable student = Common.datatable(sql2);
            DataTable program = Common.datatable(sql3);

            data.Columns.Add("StuID", typeof(string));
            data.Columns.Add("StuName", typeof(string));
            data.Columns.Add("Class", typeof(string));

            for (int i = 0; i < program.Rows.Count; i++)
            {
                data.Columns.Add(program.Rows[i][0].ToString() + Convert.ToDateTime(program.Rows[i][1]).Date.ToString(), typeof(string));
            }

            data.Columns.Add("合计", typeof(string));

            //为输出表添加行
            for (int i = 0; i < student.Rows.Count; i++)//遍历每个学生
            {
                dr = data.NewRow();
                dr[0] = student.Rows[i][0].ToString();//学号
                dr[1] = student.Rows[i][1].ToString();//姓名
                dr[2] = student.Rows[i][2].ToString();//班级

                double total = 0;
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
                            total += Convert.ToDouble(work.Rows[t][3].ToString());
                        }
                    }
                }
                dr["合计"] = total;
                data.Rows.Add(dr);
            }
         
        }


       /* private void BindGrid(GridView GridView1)
        {
            

            DataTable data = new DataTable();
            Bind(data);
            //dt：数据源  
            GridView1.DataSource = data;
            GridView1.DataBind();

         //   output(GridView1);
        }

    */
    /*    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                string sql3 = "select distinct Program,Date from Program where SySe like '%" + Session["SySe"].ToString() + "%'";
                DataTable program = new DataTable();
                program.Columns.Add("Program", typeof(string));
                program.Columns.Add("Date", typeof(DateTime));
                DataColumn[] key = new DataColumn[] { program.Columns["Date"] };//设置主键
                program.PrimaryKey = key;
                int index = 1;
                Common.Open();
                SqlDataReader reader = Common.ExecuteRead(sql3);
                while (reader.Read())
                {
                    string pro = reader.GetString(reader.GetOrdinal("Program"));
                    string date = reader.GetDateTime(reader.GetOrdinal("Date")).ToString();                   
                    while (program.Rows.Contains(date))
                    {
                        pro = pro + index.ToString();
                        index++;
                    }
                    program.Rows.Add(pro, date);
                }

                Common.close();

                TableCellCollection header = e.Row.Cells;
                header.Clear();
                string headtxt = "学号</th><th rowspan='2'>姓名</th>";
                // headtxt += "<th colspan='"+program.Rows.Count+"'></th>";  //跨四列  
                headtxt += "<th rowspan='2'>班级</th>";
                for (int i = 0; i < program.Rows.Count; i++)
                {
                    DateTime Time = Convert.ToDateTime(program.Rows[i][1].ToString());
                    DateTime time = Time.Date;
                    headtxt += "<th>" + time.ToString("yyyy-MM-dd");
                    //  headtxt = headtxt.Substring(0, headtxt.Length - 5);  //移除掉最后一个</th>  
                }
                headtxt += "<th rowspan='2'>合计</th>";
                headtxt += "<tr>";
                for (int i = 0; i < program.Rows.Count; i++)
                {
                    headtxt += "<th>" + program.Rows[i][0].ToString() + "</th>";
                }

                headtxt += "</tr>";

                TableHeaderCell cell = new TableHeaderCell();
                cell.Attributes.Add("rowspan", "2");  //跨两行   
                cell.Text = (headtxt);
                header.Add(cell);

            }
        }*/
        protected void Button2_Click(object sender, EventArgs e)
        {
            string Caption = Session["Class"].ToString() + "班\t" + Session["SySe"].ToString() + "学期工时表";
            DataTable dt = new DataTable();
            GetData(dt);
            NPOIHelper.ExportByWeb(dt,Caption,Caption);
        }        
    }
}