using System.Data.SqlClient;
using System.Web.UI.WebControls;
using FineUI;
using System.Data;
using System;

namespace WHMS
{

    public class DataControl
    {
        #region 数据操作
    
        public static void UpdataClass(Grid grid)
        {
            bool NoRepeat = true;
            int flag = 0;
            int flag2 = 0;
            string Class;
            string Grade;
            for (int i = 0; i < grid.Rows.Count; i++)
            {
                Grade = grid.Rows[i].Values[0].ToString();
                Class = grid.Rows[i].Values[1].ToString();
               
                string sqlstr1 = "select Class from Class";
                Common.Open();
                SqlDataReader re = Common.ExecuteRead(sqlstr1);
                while (re.Read())
                {
                    string class1 = re.GetString(re.GetOrdinal("Class"));
                    if (class1 == Class)
                    {
                        NoRepeat = false;
                        flag++;//重复次数
                        break;
                    }
                    NoRepeat = true;
                }
                Common.close();
                //未重复才添加
                if (NoRepeat)
                {

                    string sqlstr = "insert into Class (Class,Grade) values ('" + Class + "','" + Grade + "')";//插入新数据
                    Common.ExecuteSql(sqlstr);
                    flag2++;
                }

            }
            if (flag == 0)
            {
                Alert.Show("添加成功！\r\n已添加" + flag2.ToString() + "条记录！", "提示", FineUI.MessageBoxIcon.Information);
            }
            else
            {
                Alert.Show("已添加" + flag2.ToString() + "条记录\r\n 有" + flag.ToString() + "条记录重复", "提示", FineUI.MessageBoxIcon.Information);

            }


        }
 
  
        public static void UpdataStudent(Grid grid)
        {
            bool NoRepeat = true;
            int flag = 0;
            int flag2 = 0;
            string StuID;
            string StuName;
            string Class;
            for (int i = 0; i < grid.Rows.Count; i++)
            {
               
                StuID = grid.Rows[i].Values[0].ToString();
               StuName = grid.Rows[i].Values[1].ToString();
                Class = grid.Rows[i].Values[2].ToString();
             
                string sqlstr1 = "select StuID from Student";
                Common.Open();
                SqlDataReader re = Common.ExecuteRead(sqlstr1);
                while (re.Read())
                {
                    string id = re.GetString(re.GetOrdinal("StuID"));
                    if (id == StuID)
                    {
                        NoRepeat = false;
                        flag++;//重复次数
                        break;
                    }
                    NoRepeat = true;
                }
                Common.close();
                //未重复才添加
                if (NoRepeat)
                {

                    string sqlstr = "insert into Student (StuID,StuName,Class) values ('" + StuID + "','" + StuName + "','" + Class+ "')";//插入新数据
                    Common.ExecuteSql(sqlstr);
                    flag2++;
                }

            }
            if (flag == 0)
            {
                Alert.Show("添加成功！\r\n已添加" + flag2.ToString() + "条记录！", "提示", FineUI.MessageBoxIcon.Information);
            }
            else
            {
                Alert.Show("已添加" + flag2.ToString() + "条记录\r\n 有" + flag.ToString() + "条记录重复", "提示", FineUI.MessageBoxIcon.Information);

            }
        }


        public static void UpdataWorking_hours(Grid grid)
        {
            bool NoRepeat = true;
            int flag = 0;
            int flag2 = 0;
            string StuID;
            string StuName;
            string Program;
            string Working_hours;
            string SySe;
            string Date;
            for (int i = 0; i < grid.Rows.Count; i++)
            {
                StuID = grid.Rows[i].Values[0].ToString();
                StuName = grid.Rows[i].Values[1].ToString();
                Program = grid.Rows[i].Values[2].ToString();
                Working_hours = grid.Rows[i].Values[3].ToString();
                SySe = grid.Rows[i].Values[4].ToString();
                Date = grid.Rows[i].Values[5].ToString();

                string sqlstr1 = "select StuID from Working_hours where( Program ='" + Program+"' and SySe ='"+SySe+"' and Date = '"+Date+"')";
                Common.Open();
                SqlDataReader re = Common.ExecuteRead(sqlstr1);
                while (re.Read())
                {
                    string id = re.GetString(re.GetOrdinal("StuID"));
                    if (id == StuID)
                    {
                        NoRepeat = false;
                        flag++;//重复次数
                        break;
                    }
                    NoRepeat = true;
                }
                Common.close();
                //未重复才添加
                if (NoRepeat)
                {

                    string sqlstr = "insert into Working_hoursInfor (StuID,StuName,Program,Working_hours,SySe,Date) values ('" + StuID + "','" + StuName + "','" + Program + "','" + Working_hours +"','"+SySe+"','"+Date+ "')";//插入新数据
                    Common.ExecuteSql(sqlstr);
                    flag2++;
                }

            }
            if (flag == 0)
            {
                Alert.Show("添加成功！\r\n已添加" + flag2.ToString() + "条记录！", "提示", FineUI.MessageBoxIcon.Information);
            }
            else
            {
                Alert.Show("已添加" + flag2.ToString() + "条记录\r\n 有" + flag.ToString() + "条记录重复", "提示", FineUI.MessageBoxIcon.Information);

            }
        }

        /// <summary>
        /// 班级工时查询
        /// </summary>
        /// <param name="table1"></param>
        /// <param name="Class"></param>
        /// <param name="SySe"></param>
        public static string sql1;
        public static string sql2;
        public static string sql3;
        public static void ClassData(Table table1,string Class,string Sy,string Se)
        {

            switch (Se)
            {
                case "全部":
                    sql1 = "select * from Working_hours where SySe like(SySe like'%" + Sy + "%')";
                    break;
                default:
                    sql1 = "select * from Working_hours where SySe ='" + Sy + "-" + Se + "'";
                    break;

            }
            sql2 = "select StuID,StuName,Class from Student where Class='" + Class + "' order by Class,StuID";
            string sql3 = "select distinct Program from Working_hours where SySe like(SySe like'%" + Sy + "%')";
            DataTable dt = Common.datatable(sql1);
            DataTable student = Common.datatable(sql2);
            DataTable program = Common.datatable(sql3);

            //第一列表头
            TableRow tr = new TableRow();//行
            tr.HorizontalAlign = HorizontalAlign.Center;
            TableCell tc = new TableCell();//列
            tc.Text = "学号";
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "姓名";
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "行政班级";
            tr.Cells.Add(tc);

            for (int i = 0; i <= program.Rows.Count; i++)
            {
                tc = new TableCell();
                if (i < program.Rows.Count)
                {
                    tc.Text = program.Rows[i][0].ToString();
                }
                else
                {
                    tc.Text = "合计";
                }
                tr.Cells.Add(tc);
            }

            table1.Rows.Add(tr);

            for (int i = 0; i < student.Rows.Count; i++)
            {
                tr = new TableRow();
                tc = new TableCell();

                tc.Text = student.Rows[i][0].ToString();
                tr.Cells.Add(tc);

                tc = new TableCell();
                tc.Text = student.Rows[i][1].ToString();
                tr.Cells.Add(tc);

                tc = new TableCell();
                tc.Text = student.Rows[i][2].ToString();
                tr.Cells.Add(tc);

                int total = 0;
                for (int j = 0; j < program.Rows.Count; j++)
                {
                    tc = new TableCell();
                    for (int t = 0; t < dt.Rows.Count; t++)
                    {
                        if (dt.Rows[t][0].ToString() == student.Rows[i][0].ToString() && dt.Rows[t][3].ToString() == program.Rows[j][0].ToString())
                        {
                            tc.Text = dt.Rows[t][4].ToString();
                            total += Convert.ToInt32(tc.Text);
                        }
                    }
                    tc.Attributes.Add("text-align", "center");
                    tr.Cells.Add(tc);
                }
                tc = new TableCell();
                tc.Text = total.ToString();
                tr.Cells.Add(tc);
                table1.Rows.Add(tr);

            }


        }


       
        public static void GradeData(Table table1,string Sy, string Se,string Grade)
        {

            switch (Se)
            {
                case "全部":
                    sql1 = "select * from Working_hours where SySe like(SySe like'%" + Sy + "%')";
                    break;
                default:
                    sql1 = "select * from Working_hours where SySe ='" + Sy + "-" + Se + "'";
                    break;
            }
            sql2 = "select StuID,StuName,Class from Student where Grade='"+Grade+"' order by Class,StuID";
            string sql3 = "select distinct Program from Working_hours where SySe like(SySe like'%" + Sy + "%')";
            DataTable dt = Common.datatable(sql1);
            DataTable student = Common.datatable(sql2);
            DataTable program = Common.datatable(sql3);

            //第一列表头
            TableRow tr = new TableRow();//行
            tr.HorizontalAlign = HorizontalAlign.Center;
            TableCell tc = new TableCell();//列
            tc.Text = "学号";
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "姓名";
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "行政班级";
            tr.Cells.Add(tc);

            for (int i = 0; i <= program.Rows.Count; i++)
            {
                tc = new TableCell();
                if (i < program.Rows.Count)
                {
                    tc.Text = program.Rows[i][0].ToString();
                }
                else
                {
                    tc.Text = "合计";
                }
                tr.Cells.Add(tc);
            }

            table1.Rows.Add(tr);

            for (int i = 0; i < student.Rows.Count; i++)
            {
                tr = new TableRow();
                tc = new TableCell();

                tc.Text = student.Rows[i][0].ToString();
                tr.Cells.Add(tc);

                tc = new TableCell();
                tc.Text = student.Rows[i][1].ToString();
                tr.Cells.Add(tc);

                tc = new TableCell();
                tc.Text = student.Rows[i][2].ToString();
                tr.Cells.Add(tc);

                int total = 0;
                for (int j = 0; j < program.Rows.Count; j++)
                {
                    tc = new TableCell();
                    for (int t = 0; t < dt.Rows.Count; t++)
                    {
                        if (dt.Rows[t][0].ToString() == student.Rows[i][0].ToString() && dt.Rows[t][3].ToString() == program.Rows[j][0].ToString())
                        {
                            tc.Text = dt.Rows[t][4].ToString();
                            total += Convert.ToInt32(tc.Text);
                        }
                    }
                    tc.Attributes.Add("text-align", "center");
                    tr.Cells.Add(tc);
                }
                tc = new TableCell();
                tc.Text = total.ToString();
                tr.Cells.Add(tc);
                table1.Rows.Add(tr);

            }


        }




        #endregion

    }
 
 
}