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
//using System.Windows.Forms;


namespace WHMS
{
    public partial class ShowEditDelete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                 BindGrid();
                //   bind();
                //  Bind();
                // InitGrid2();
             //   Button2.OnClientClick = window1.GetShowReference("test.aspx");
                
            }
        }


//数据源
        public void Bind(DataTable data)
        {
           // DataTable data = new DataTable();
            DataRow dr;

            string sql1 = "select * from [Working_hoursInfor] where SySe like '%2017-2018-1%'";
            string sql2 = "select StuID,StuName,Class from Student where Class='信管1501' order by Class,StuID";
            string sql3 = "select distinct Program,Date from [Working_hoursInfor] where SySe like '%2017-2018-1%'";
            DataTable dt = Common.datatable(sql1);
            DataTable student = Common.datatable(sql2);
            DataTable program = Common.datatable(sql3);

            data.Columns.Add("学号",typeof(string));
            data.Columns.Add("姓名", typeof(string));
            data.Columns.Add("班级", typeof(string));

            for (int i=0;i<=program.Rows.Count;i++)
            {
               
                if (i < program.Rows.Count)
                {
               //   string time = (Convert.ToDateTime(program.Rows[i][1].ToString()).Date).ToString();
                     data.Columns.Add(program.Rows[i][0].ToString(), typeof(string));
                }

                else
                {
                    data.Columns.Add("合计", typeof(int));
                }
            }
            //构建活动行
       /*     dr = data.NewRow();
            for (int i=0;i<program.Rows.Count;i++)
            {
               
                dr[i] = program.Rows[i][0].ToString();
            }
            data.Rows.Add(dr);
*/

            for (int i=0;i<student.Rows.Count;i++)
            {
                dr = data.NewRow();
                dr[0] = student.Rows[i][0].ToString();
                dr[1] = student.Rows[i][1].ToString();
                dr[2] = student.Rows[i][2].ToString();

                int total = 0;
                for (int j=0;j<program.Rows.Count;j++)
                {
                    for (int t=0;t<dt.Rows.Count;t++)
                    {
                        string t1 = dt.Rows[t][0].ToString();
                        string t2 = student.Rows[i][0].ToString();
                        string t3 = dt.Rows[t][2].ToString();
                        string t4 = program.Rows[j][0].ToString();
                        if (dt.Rows[t][0].ToString() == student.Rows[i][0].ToString() && dt.Rows[t][2].ToString() == program.Rows[j][0].ToString())
                        {
                            // dr[program.Rows[j][0].ToString()]= dt.Rows[t][3].ToString();
                            dr[3+j] = dt.Rows[t][3].ToString();
                            total += Convert.ToInt32(dt.Rows[t][3].ToString());
                        }
                    }
                }
                dr["合计"] = total;
                data.Rows.Add(dr);
            }
            
          GridView1.DataSource = data;
          GridView1.DataBind();
        }

   



   /*     public void Updata()
        {
            bool NoRepeat = true;
            int flag = 0;
            int flag2 = 0;
            string Class;
            string Grade;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                Class = GridView1.Rows[i].Cells[1].Text;
                Grade = GridView1.Rows[i].Cells[0].Text;
                string sqlstr1 = "select Class from Class";
                Common.Open();
                SqlDataReader re = Common.ExecuteRead(sqlstr1);
                while (re.Read())
                {
                    string class1 = re.GetString(re.GetOrdinal("Class"));
                    if (class1 == Class)
                    {
                        NoRepeat = false;
                        flag ++;//重复次数
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
                Alert.Show("添加成功！\r\n已添加" + flag2.ToString() + "条记录！", "提示", MessageBoxIcon.Information);
            }
            else
            {
                Alert.Show("已添加"+flag2.ToString()+"条记录\r\n 有"+flag.ToString()+"条记录重复", "提示", MessageBoxIcon.Information);

            }


        }*/



//table
        public void bind()
        {
            //
            string sql1 = "select * from [Working_hoursInfor]";
            string sql2 = "select StuID,StuName,Class from Student where Class='信管1501' order by Class,StuID";
            string sql3 = "select distinct Program from [Working_hoursInfor]";
            DataTable dt = Common.datatable(sql1);
            DataTable student = Common.datatable(sql2);
            DataTable program = Common.datatable(sql3);

    
          //  Table table1 = new Table();
         //  FineUI.Label lb;

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
          
            for (int i=0;i<=program.Rows.Count;i++)
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

                //    lb = new FineUI.Label();
                //    lb.Text = student.Rows[i][0].ToString();
                //   tc.Controls.Add(lb);

                tc.Text = student.Rows[i][0].ToString();
                tr.Cells.Add(tc);
          
                tc = new TableCell();
                tc.Text = student.Rows[i][1].ToString();
                tr.Cells.Add(tc);
       
                tc = new TableCell();
                tc.Text = student.Rows[i][2].ToString();
                tr.Cells.Add(tc);
          
                int total = 0;
                for (int j=0;j<program.Rows.Count;j++)
                {
                    tc = new TableCell();
         
                    for (int t=0;t<dt.Rows.Count;t++)
                    {
                        string t1 = dt.Rows[t][0].ToString();
                        string t2 = student.Rows[i][0].ToString();
                        string t3 = dt.Rows[t][3].ToString();
                        string t4 = program.Rows[j][0].ToString();


                        if (dt.Rows[t][0].ToString()==student.Rows[i][0].ToString()&&dt.Rows[t][2].ToString()==program.Rows[j][0].ToString())
                        {
                            tc.Text = dt.Rows[t][3].ToString();
                            total += Convert.ToInt32(tc.Text);
                      
                        }
                    }
                    tc.Attributes.Add("text-align","center");
                    tr.Cells.Add(tc);
           
                }
                tc = new TableCell();
                tc.Text = total.ToString();
        
                tr.Cells.Add(tc);
                table1.Rows.Add(tr);
         
              
              //  int o = table1.Rows.Count;

            }
    
        }

   /*     protected void OUT_Click(object sender, EventArgs e)
        {
            DataTable datatable = new DataTable();
            TableToExcel(table1);


            SaveFileDialog sflg = new SaveFileDialog();
            sflg.Filter = "Excel(*.xls)|*.xls|Excel(*.xlsx)|*.xlsx";
            if (sflg.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            //this.GridView1.ExportToXls(sflg.FileName);
            //NPOI.xs book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            NPOI.SS.UserModel.IWorkbook book = null;
            if (sflg.FilterIndex == 1)
            {
                book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            }
            else
            {
                book = new NPOI.XSSF.UserModel.XSSFWorkbook();
            }

            NPOI.SS.UserModel.ISheet sheet = book.CreateSheet("test_001");

            // 添加表头
            NPOI.SS.UserModel.IRow row = sheet.CreateRow(0);
            int index = 0;
            foreach (DataColumn item in this.GridView1.Columns)
            {
               // GridColumn
                if (item.)
                {
                    NPOI.SS.UserModel.ICell cell = row.CreateCell(index);
                    cell.SetCellType(NPOI.SS.UserModel.CellType.String);
                    cell.SetCellValue(item.Caption);
                    index++;
                }
            }

            // 添加数据

            for (int i = 0; i < this.GridView1.Rows.Count; i++)
            {
                index = 0;
                row = sheet.CreateRow(i + 1);
                foreach (DataColumn item in this.GridView1.Columns)
                {
                    if (item.Visible)
                    {
                        NPOI.SS.UserModel.ICell cell = row.CreateCell(index);
                        cell.SetCellType(NPOI.SS.UserModel.CellType.String);
                        cell.SetCellValue(this.GridView1.GetRowCellValue(i, item).ToString());
                        index++;
                    }
                }
            }
            // 写入 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            book = null;

            using (FileStream fs = new FileStream(sflg.FileName, FileMode.Create, FileAccess.Write))
            {
                byte[] data = ms.ToArray();
                fs.Write(data, 0, data.Length);
                fs.Flush();
            }

            ms.Close();
            ms.Dispose();

        }
        */
        public static bool TableToExcel(Table table1)
        {

            bool result = false;
            IWorkbook workbook = null;
            FileStream fs = null;
            IRow row = null;
            ISheet sheet = null;
            ICell cell = null;
            try
            {
                if (table1 != null && table1.Rows.Count > 0)
                {
                    workbook = new HSSFWorkbook();
                    sheet = workbook.CreateSheet("Sheet0");//创建一个名称为Sheet0的表  
                 //   int rowCount = dt.Rows.Count;//行数  
               int     rowCount = table1.Rows.Count;
               //     int columnCount = dt.Columns.Count;//列数  
                 int   columnCount = table1.Rows[0].Cells.Count;

                    //设置列头  
                    row = sheet.CreateRow(0);//excel第一行设为列头  
                    for (int c = 0; c < columnCount; c++)
                    {
                       cell = row.CreateCell(c);
                        //cell.SetCellValue(dt.Columns[c].ColumnName);
                        cell.SetCellValue(table1.Rows[0].Cells[c].ToString());
                    }

                    //设置每行每列的单元格,  
                    for (int i = 0; i < rowCount; i++)
                    {
                        row = sheet.CreateRow(i + 1);
                        for (int j = 0; j < columnCount; j++)
                        {
                            cell = row.CreateCell(j);//excel第二行开始写入数据  
                           // cell.SetCellValue(dt.Rows[i][j].ToString());
                           cell.SetCellValue(table1.Rows[i].Cells[j].ToString());
                        }
                    }
                    using (fs = File.OpenWrite(@"D:/myxls.xls"))
                    {
                        workbook.Write(fs);//向打开的这个xls文件中写入数据  
                        result = true;
                    }
                }
                return result;
            }
            catch (Exception ex)
            {

                if (fs != null)
                {
                    fs.Close();
                    throw ex;
                }

                return false;
            }
        }


        public void InitGrid2()
        {
          /*  string sql1 = "select * from [Working_hoursInfor]";
            string sql2 = "select StuID,StuName,Class from Student where Class='信管1501' order by Class,StuID";
            string sql3 = "select distinct Program from [Working_hoursInfor]";
            DataTable dt = Common.datatable(sql1);
            DataTable student = Common.datatable(sql2);
            DataTable program = Common.datatable(sql3);

            FineUI.BoundField bf;

            bf = new FineUI.BoundField();
               bf.DataField = "StuID";
                bf.HeaderText = "学号";
                grid2.Columns.Add(bf);

            bf = new FineUI.BoundField();
            bf.DataField = "StuName";
            bf.HeaderText = "姓名";
            grid2.Columns.Add(bf);

            bf = new FineUI.BoundField();
            bf.DataField = "Class";
            bf.HeaderText = "行政班级";
            grid2.Columns.Add(bf);

            for (int i=0;i<program.Rows.Count;i++)
            {
                bf = new FineUI.BoundField();
                bf.DataField = program.Rows[i][0].ToString();
                bf.HeaderText = program.Rows[i][0].ToString();
                grid2.Columns.Add(bf);
            }

            bf = new FineUI.BoundField();
            bf.DataField = "count";
            bf.HeaderText = "合计";
            grid2.Columns.Add(bf);

            
            for (int i=0;i<student.Rows.Count;i++)
            {
                GridRow gr = new GridRow();
                
                gr.Values[0] = student.Rows[i][0].ToString();
                grid2.UpdateTemplateFields();
                grid2.Rows[i].Values[0] = student.Rows[i][0].ToString();
                grid2.Rows[i].Values[1]= student.Rows[i][1].ToString();
                grid2.Rows[i].Values[2] = student.Rows[i][2].ToString();

                int total = 0;
                for (int j=0;j<program.Rows.Count;j++)
                {
                    for (int t=0;t<dt.Rows.Count;t++)
                    {
                        if (dt.Rows[t][0].ToString() == student.Rows[i][0].ToString() && dt.Rows[t][3].ToString() == program.Rows[j][0].ToString())
                        {
                            grid2.Rows[i].Values[3 + j] = dt.Rows[t][4].ToString();
                            total += Convert.ToInt32(grid2.Rows[i].Values[3 + j]);
                        }
                        else
                        {
                            grid2.Rows[i].Values[3 + j] = "";
                        }
                    }
                }
                grid2.Rows[i].Values[3 + program.Rows.Count] = total.ToString();
            }*/
        }

        protected void PageManager1_Init(object sender, EventArgs e)
        {
            InitGrid2();
        }

        protected void OUT_Click(object sender, EventArgs e)
        {

        }

        // 创建GridView列的方法   
        private void CreateGridColumn(string dataField, string headerText, int width)
        {
            System.Web.UI.WebControls.BoundField bc = new System.Web.UI.WebControls.BoundField();
            bc.DataField = dataField;
            bc.HeaderText = headerText;
           // bc.HeaderStyle.CssClass = headerStyle;  //若有默认样式，此行代码及对应的参数可以移除  
          //  bc.ItemStyle.CssClass = itemStyle;   //若有默认样式，此行代码及对应的参数可以移除  
            GridView1.Columns.Add(bc);  //把动态创建的列，添加到GridView中  
            GridView1.Width = new Unit(GridView1.Width.Value + width); //每添加一列后，要增加GridView的总体宽度  

        }
        private void BindGrid()
        {
            #region  添加动态列   
           /*    GridView1.Columns.Clear();
                GridView1.Width = new Unit(0);

                string sql1 = "select * from [Working_hoursInfor] where SySe like '%2016-2017-1%'";
                string sql2 = "select StuID,StuName,Class from Student where Class='信管1501' order by Class,StuID";
                string sql3 = "select distinct Program,Date from [Working_hoursInfor] where SySe like '%2016-2017-1%'";
                DataTable dt = Common.datatable(sql1);                                                                                                                                          
                DataTable student = Common.datatable(sql2);
                DataTable program = Common.datatable(sql3);

                CreateGridColumn("学号", "学号", 150);
                CreateGridColumn("姓名", "姓名", 150);
                CreateGridColumn("班级", "班级", 150);


                for (int i=0;i<=program.Rows.Count;i++)
                {
                    if (i < program.Rows.Count)
                    {
                  //      DateTime time = Convert.ToDateTime(program.Rows[i][1].ToString()).Date;
                        CreateGridColumn(program.Rows[i][0].ToString(), program.Rows[i][0].ToString(), 150);
                    }
                    else
                    {
                       AspNet. TemplateField count = new AspNet. TemplateField();
                        GridView1.Columns.Add(count);
                    }
                }*/
            #endregion

          
            
            DataTable data = new DataTable();
            Bind(data);
            //dt：数据源  
            GridView1.DataSource = data;
            GridView1.DataBind();
            //   output(GridView1);
         //   NPOItest.Batch_Updat e(data);
        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                string sql3 = "select distinct Program,Date from [Working_hoursInfor] where SySe like '%2017-2018-1%'";
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
                    headtxt+="<th>"+program.Rows[i][0].ToString()+"</th>";
                }
             
                headtxt += "</tr>";
              
                TableHeaderCell cell = new TableHeaderCell();
                   cell.Attributes.Add("rowspan", "2");  //跨两行   
                   cell.Text = (headtxt);
                 header.Add(cell);
               
            }
        }




        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
                DataTable dt = new DataTable();
                Bind(dt);
              NPOItest.Batch_Update(dt);
            //     NPOIHelper.ExportByWeb(dt,"信管1501工时表", "信管1501工时表");
            //  BindGrid();
            //    string fn = "tets";
            // ExportExcel(fn,GridView1);
            //   ResolveGridView(GridView1);
          //  output(GridView1);
      /*     Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=MyExcelFile.xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;

            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GridView1.RenderControl(htw);

            Response.Write(sw.ToString());
            Response.End();*/
           
        }

        public void output(GridView GridView1)
        {
            ResolveGridView(GridView1);

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=MyExcelFile.xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;

            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GridView1.RenderControl(htw);

            Response.Write(sw.ToString());
            Response.End();
        }
        private void ResolveGridView(System.Web.UI.Control ctrl)
        {
            for (int i = 0; i < ctrl.Controls.Count; i++)
            {
                // 图片的完整URL
                if (ctrl.Controls[i].GetType() == typeof(AspNet.Image))
                {
                    AspNet.Image img = ctrl.Controls[i] as AspNet.Image;
                    img.ImageUrl = Request.Url.AbsoluteUri.Replace(Request.Url.AbsolutePath, Page.ResolveUrl(img.ImageUrl));
                }

                // 将CheckBox控件转化为静态文本
                if (ctrl.Controls[i].GetType() == typeof(AspNet.CheckBox))
                {
                    Literal lit = new Literal();
                    lit.Text = (ctrl.Controls[i] as AspNet.CheckBox).Checked ? "√" : "×";
                    ctrl.Controls.RemoveAt(i);
                    ctrl.Controls.AddAt(i, lit);
                }

                if (ctrl.Controls[i].HasControls())
                {
                    ResolveGridView(ctrl.Controls[i]);
                }

            }

        }

        protected void Button2_Click1(object sender, EventArgs e)
        {
            // BindGrid();
            //  Bind();
            Common.Class = "信管1501";
            Common.grade = 2015;
            Common.SySe = "2016-2017-1";
            PageContext.RegisterStartupScript(window1.GetShowReference("test.aspx"));
           // ClientScript.RegisterStartupScript(Page.GetType(),"",window1.GetShowReference("test.aspx"));
        }



   

     
    }
}