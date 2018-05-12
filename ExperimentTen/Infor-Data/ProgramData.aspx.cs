using System;
using System.Web.UI.WebControls;
using System.Data;

namespace WHMS.Infor_Data
{
    public partial class ProgramData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionManager.CheckLogin("../login.aspx");
            //   BindGrid(GridView1);
            bindtest();
        }

        public void Bind(DataTable data)
        {
            // DataTable data = new DataTable();
            DataRow dr;

            string sql1 = "select * from [Working_hoursInfor] where SySe like '%" + Common.SySe + "%' and Program='"+Common.Program+"'";
            string sql2 = "select StuID,StuName,Class from Working_hours where SySe like '%" + Common.SySe + "%' and Program='" + Common.Program + "' order by StuID";
            string sql3 = "select distinct Program,Date from [Working_hoursInfor] where SySe like '%" + Common.SySe + "%' and Program='" + Common.Program + "'";
            DataTable dt = Common.datatable(sql1);
            DataTable student = Common.datatable(sql2);
            DataTable program = Common.datatable(sql3);

            data.Columns.Add("学号", typeof(string));
            data.Columns.Add("姓名", typeof(string));
            data.Columns.Add("班级", typeof(string));

            for (int i = 0; i <= program.Rows.Count; i++)
            {

                if (i < program.Rows.Count)
                {

                    data.Columns.Add(program.Rows[i][0].ToString(), typeof(string));
                }

                else
                {
                    data.Columns.Add("合计", typeof(int));
                }
            }

            for (int i = 0; i < student.Rows.Count; i++)
            {
                dr = data.NewRow();
                dr[0] = student.Rows[i][0].ToString();
                dr[1] = student.Rows[i][1].ToString();
                dr[2] = student.Rows[i][2].ToString();

                double total = 0;
                for (int j = 0; j < program.Rows.Count; j++)
                {
                    for (int t = 0; t < dt.Rows.Count; t++)
                    {
                        string t1 = dt.Rows[t][0].ToString();
                        string t2 = student.Rows[i][0].ToString();
                        string t3 = dt.Rows[t][2].ToString();
                        string t4 = program.Rows[j][0].ToString();
                        if (dt.Rows[t][0].ToString() == student.Rows[i][0].ToString() && dt.Rows[t][2].ToString() == program.Rows[j][0].ToString())
                        {                      
                            dr[3 + j] = dt.Rows[t][3].ToString();
                            total += Convert.ToDouble(dt.Rows[t][3].ToString());
                        }
                    }
                }
                dr["合计"] = total;
                data.Rows.Add(dr);
            }      
        }


        private void BindGrid(GridView GridView1)
        {       

            DataTable data = new DataTable();
            Bind(data);
            //data：数据源  
            GridView1.DataSource = data;
            GridView1.DataBind();
        }

        //表格构造
        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                string sql3 = "select distinct Program,Date from [Working_hoursInfor] where SySe like '%" + Common.SySe + "%' and Program='" + Common.Program + "'";
                DataTable program = Common.datatable(sql3);
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
        }

        //导出
        protected void Button2_Click(object sender, EventArgs e)
        {
         //   DataTable dt = new DataTable();
         //   Bind(dt);
            string sql = "select StuID as '学号',StuName as '姓名',Class as '班级',Program as '活动',Working_hours as '工时',SySe as '学期',Date as '日期' from Working_hours where Program ='" + Session["Program"] + "' and SySe ='" + Session["SySe"] + "' order by Date";
            DataTable dt = Common.datatable(sql);
            gridview.DataSource = dt;
            //   NPOItest.Batch_Update(dt);
            //    NPOIHelper.ExportByWeb(dt, GridView1.Caption, GridView1.Caption);

            //   NPOIHelper.ExportByWeb(dt, gridview.Caption, gridview.Caption);
            NPOI_EXCEL.ExportByWeb(dt,gridview.Caption);

        }

        //数据查询
        public void bindtest() {
            string sql;
            if(Session["Date"].Equals(""))//查询该活动的所有日期的工时
                 sql = "select StuID as '学号',StuName as '姓名',Class as '班级',Program as '活动',Working_hours as '工时',SySe as '学期',convert(varchar(12),Date,111) as '日期' from Working_hours where Program ='" + Session["Program"]+"' and SySe ='"+Session["SySe"]+"' order by Date";
            else//查询某个日期的活动工时
                sql = "select StuID as '学号',StuName as '姓名',Class as '班级',Program as '活动',Working_hours as '工时',SySe as '学期',convert(varchar(12),Date,111) as '日期' from Working_hours where Program ='" + Session["Program"] + "' and SySe ='" + Session["SySe"] + "' and Date = '"+Session["Date"]+"'";

            DataTable dt = Common.datatable(sql);
            gridview.DataSource = dt;
            gridview.DataBind();
            gridview.Caption=Session["SySe"].ToString()+"学期\t"+Session["Program"]+"\t活动工时表";
        }
    }
}