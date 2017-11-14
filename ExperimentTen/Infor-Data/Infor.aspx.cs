using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Data;
using FineUI;
using System.Web;
using System.IO;

namespace WHMS.Infor_Data
{
    public partial class Infor : System.Web.UI.Page
    {
        public static string grade;
        public static string Class1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Common.checklogin("../login.aspx");
                Common.Sid = "";
                grade = "";
                Class1 = "";
              
                bind();
                btnSearch.OnClientClick = Grid1.GetNoSelectionAlertReference("请选择要查看的学号","警告",MessageBoxIcon.Warning);
                btnadd.OnClientClick = window1.GetShowReference("AddStu.aspx","添加学生");
            
                btnImport.OnClientClick = window3.GetShowReference("StuImport.aspx","导入学生名单");
            }
      
        }

        #region 数据绑定

      
        #region 年级与班级数据绑定
        //年级与班级树
        public void bind()
        {

            DataTable table = CreateDataTable();
            DataSet ds = new DataSet();
            ds.Tables.Add(table);
            ds.Relations.Add("TreeRelation", ds.Tables[0].Columns["ID"], ds.Tables[0].Columns["ParentID"]);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (row.IsNull("ParentID"))
                {
                    TreeNode node = new TreeNode();
                    node.Text = row["text"].ToString();//数据源的数据绑定到节点上
                    node.Expanded = true;
                    Tree1.Nodes.Add(node);//添加到树
        
                    ResolveSubTree(row, node);//子节点绑定到父节点上
                }
            }
        }

        private void ResolveSubTree(DataRow dataRow, TreeNode treeNode)
        {
            DataRow[] rows = dataRow.GetChildRows("TreeRelation");
            if (rows.Length > 0)
            {
                treeNode.Expanded = false;//默认为不展开
                foreach (DataRow row in rows)
                {
                    TreeNode node = new TreeNode();
                    node.Text = row["text"].ToString();
                    node.NodeID = row["ID"].ToString();
                    treeNode.Nodes.Add(node);
                    node.EnableClickEvent = true;//设置节点选中可用

                    ResolveSubTree(row, node);
                }
            }
        }
        //创建树节点的数据源DataTable
        private DataTable CreateDataTable()
        {
            DataTable table = new DataTable();
            DataColumn column1 = new DataColumn("ID", typeof(string));
            DataColumn column2 = new DataColumn("text", typeof(string));
            DataColumn column3 = new DataColumn("ParentID", typeof(string));
            table.Columns.Add(column1);
            table.Columns.Add(column2);
            table.Columns.Add(column3);

            int thisyear = DateTime.Now.Year;
            int thismonth = DateTime.Now.Month;

            DataRow row = table.NewRow();
            row[0] = "grade";
            row[1] = "年级";
            row[2] = DBNull.Value;
            table.Rows.Add(row);

            if (thismonth >= 9)
            {
                for (int i = 1; i < 5; i++)
                {
                    string year = thisyear--.ToString();
                    DataRow dr = table.NewRow();
                    dr[0] = year;
                    dr[1] = year;
                    dr[2] = "grade";
                    table.Rows.Add(dr);


                    string sqlstr = "select Class from Class where grade =" + year;
                    DataTable dt = Common.datatable(sqlstr);

                    foreach (DataRow Drw in dt.Rows)
                    {
                        DataRow drw = table.NewRow();
                        drw[0] = Drw[0].ToString();
                        drw[1] = Drw[0].ToString();
                        drw[2] = year;
                        table.Rows.Add(drw);
                    }
                }
            }
            else
            {
                for (int i = 1; i < 5; i++)
                {
                    string year = (--thisyear).ToString();
                    DataRow dr = table.NewRow();
                    dr[0] = year;
                    dr[1] = year;
                    dr[2] = "grade";
                    table.Rows.Add(dr);
                    //查询对应年级包含的班级并添加到节点
                    string sqlstr = "select Class from Class where grade =" + year;
                    DataTable dt = Common.datatable(sqlstr);

                    foreach (DataRow Drw in dt.Rows)
                    {
                        DataRow drw = table.NewRow();
                        drw[0] = Drw[0].ToString();
                        drw[1] = Drw[0].ToString();
                        drw[2] = year;
                        table.Rows.Add(drw);
                    }
                }

            }
            return table;

        }
        #endregion

        #region 学生信息查询
        //BindGrid1 数据库分页 查询年级学生信息，按班级和学号排序
        private void BindGrid1(string SqlStr)
        {
 
            DataTable dt = Common.datatable(SqlStr);
            // 1.设置总项数（特别注意：数据库分页一定要设置总记录数RecordCount）
            Grid1.RecordCount = GetTotalCount(dt);

            // 2.获取当前分页数据
            DataTable table = GetPagedDataTable(Grid1.PageIndex, Grid1.PageSize,dt);

            // 3.绑定到Grid
            Grid1.DataSource = dt;
            Grid1.DataBind();
          
        }
        #region 数据库分页处理
        /// <summary>
        /// 模拟返回总项数
        /// </summary>
        /// <returns></returns>
        private int GetTotalCount(DataTable dt)
        {
            return dt.Rows.Count;
        }

        /// <summary>
        /// 模拟数据库分页
        /// </summary>
        /// <returns></returns>
        private DataTable GetPagedDataTable(int pageIndex, int pageSize,DataTable dt)
        {
            DataTable source = dt;

            DataTable paged = source.Clone();

            int rowbegin = pageIndex * pageSize;
            int rowend = (pageIndex + 1) * pageSize;
            if (rowend > source.Rows.Count)
            {
                rowend = source.Rows.Count;
            }

            for (int i = rowbegin; i < rowend; i++)
            {
                paged.ImportRow(source.Rows[i]);
            }

            return paged;
        }

        #endregion
        #endregion

        #region 班级查询
        //查询选中年级包含的班级
       
        #endregion


        #endregion
        #region Events


        //查看学生个人工时
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRow.Values[1].ToString() != null)
            {
                Common.Sid = Grid1.SelectedRow.Values[1].ToString();
                Response.Redirect("Data.aspx");
            }
        }
        // 窗体关闭时刷新页面
        protected void window1_Close(object sender, WindowCloseEventArgs e)
        {

            Grid1.Visible = true;
            //GridClass.Visible = false;
            //   grade = Tree1.Nodes[0].Nodes[0].Text;

            string SqlStr = "select StuID,StuName,Class,Grade from Student where Grade= " + grade + "order by Class,StuID";
            BindGrid1(SqlStr);
        }

        //树节点选中事件
        protected void Tree1_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            if (e.Node.Leaf != true)//有子节点则Leaf为false,没有为true
            {
                grade = e.Node.Text;//查找年级
                string SqlStr = "select StuID,StuName,Class,Grade from Student where Grade= '" + grade + "' order by Class,StuID";
                BindGrid1(SqlStr);
          
            }
            else
            {
                Class1 = e.Node.Text;//查找班级
                string SqlStr = "select StuID,StuName,Class,Grade from Student where Class= '" + Class1 + "' order by StuID";
                BindGrid1(SqlStr);
             
            }

        }
        //删除学生记录
        protected void btnDelet_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndex < 0)
            {
                Alert.Show("请选择一项进行删除", "警告", MessageBoxIcon.Warning);
            }
            else
            {

                string id = Grid1.SelectedRow.Values[1].ToString();//选中行的第二列数据为ID
                string sqlStr = "delete from Student where StuID= '" + id + " '";
                Common.ExecuteSql(sqlStr);

                //   grade = Tree1.Nodes[0].Nodes[0].Text;

                string SqlStr = "select StuID,StuName,Class,Grade from Student where Grade= " + grade + "order by Class,StuID";
                BindGrid1(SqlStr);

                Alert.ShowInTop("删除成功", "信息", MessageBoxIcon.Information);

            }
        }
        //查找个人信息
        protected void btnStuSerach_Click(object sender, EventArgs e)
        {
            string SqlStr = "select StuID,StuName,Class,Grade from Student where StuID= '" + txtStuID.Text + "'";
            BindGrid1(SqlStr);
        }
        //删除一条班级记录
      
        #endregion
        //页面编号处理
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
        }

        protected void window3_Close(object sender, WindowCloseEventArgs e)
        {
            grade = Tree1.SelectedNode.Text;
            string SqlStr = "select StuID,StuName,Class,Grade from Student where Grade= " + grade + "order by Class,StuID";
            BindGrid1(SqlStr);
        }

     

        protected void Tree1_Expand(object sender, EventArgs e)
        {
            bind();
        }

        protected void btnDownLoad_Click(object sender, EventArgs e)
        {
            string FN = "Student.xls";//模板名
           NPOI_EXCEL.DownLoad(FN);
            Page_Load(sender,e);
/*
            string fileName = FN;//客户端保存的文件名
                                 //  string filePath = Server.MapPath("~/ExperimentTen/res/DownLoad/muban.xls");//路径
            string filePath = HttpContext.Current.Server.MapPath("~/ExperimentTen/res/DownLoad/" + fileName);//路径

            //以字符流的形式下载文件
            System.IO.FileStream fs = new System.IO.FileStream(filePath, FileMode.Open);
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            //  Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.ContentType = "application/excel";

            //通知浏览器下载文件而不是打开
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;  filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
            HttpContext.Current.Response.BinaryWrite(bytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();

        */    
        }
    }
}