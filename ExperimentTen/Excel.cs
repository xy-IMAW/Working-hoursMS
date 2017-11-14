using System;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using System.Text;
using NPOI.SS.Util;


namespace WHMS
{
    
    public class NPOI_EXCEL
    {
        /// <summary>  
        /// 将excel导入到datatable  
        /// </summary>  
        /// <param name="filePath">excel路径</param>  
        /// <param name="isColumnName">第一行是否是列名</param>  
        /// <returns>返回datatable</returns>  
       ///Excel读入
        public static DataTable ExcelToDataTable(string filePath, bool isColumnName)

        {
            DataTable dataTable = null;
            FileStream fs = null;
            DataColumn column = null;
            DataRow dataRow = null;
            IWorkbook workbook = null;
            ISheet sheet = null;
            IRow row = null;
            ICell cell = null;
            int startRow = 0;
            try
            {
                using (fs = File.OpenRead(filePath))
                {
                    // 2007版本  
                    if (filePath.IndexOf(".xlsx") > 0)
                        workbook = new XSSFWorkbook(fs);
                    // 2003版本  
                    else if (filePath.IndexOf(".xls") > 0)
                        workbook = new HSSFWorkbook(fs);

                    if (workbook != null)
                    {
                        sheet = workbook.GetSheetAt(0);//读取第一个sheet，当然也可以循环读取每个sheet  
                        dataTable = new DataTable();
                        if (sheet != null)
                        {
                            int rowCount = sheet.LastRowNum;//总行数  
                            if (rowCount > 0)
                            {
                                IRow firstRow = sheet.GetRow(0);//第一行  
                                int cellCount = firstRow.LastCellNum;//列数  

                                //构建datatable的列  
                                if (isColumnName)
                                {
                                    startRow = 1;//如果第一行是列名，则从第二行开始读取  
                                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                                    {
                                        cell = firstRow.GetCell(i);
                                        if (cell != null)
                                        {
                                            if (cell.StringCellValue != null)
                                            {
                                                column = new DataColumn(cell.StringCellValue);
                                                dataTable.Columns.Add(column);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                                    {
                                        column = new DataColumn("column" + (i + 1));
                                        dataTable.Columns.Add(column);
                                    }
                                }

                                //填充行  
                                for (int i = startRow; i <= rowCount; ++i)
                                {
                                    row = sheet.GetRow(i);
                                    if (row == null) continue;

                                    dataRow = dataTable.NewRow();
                                    for (int j = row.FirstCellNum; j < cellCount; ++j)
                                    {
                                        cell = row.GetCell(j);
                                        if (cell == null)
                                        {
                                            dataRow[j] = "";
                                        }
                                        else
                                        {
                                            //CellType(Unknown = -1,Numeric = 0,String = 1,Formula = 2,Blank = 3,Boolean = 4,Error = 5,)  
                                            switch (cell.CellType)
                                            {
                                                case CellType.Blank:
                                                    dataRow[j] = "";
                                                    break;
                                                case CellType.Numeric:
                                                    short format = cell.CellStyle.DataFormat;
                                                    if (NPOI.SS.UserModel.DateUtil.IsCellDateFormatted(cell))
                                                    {
                                                      //  dataRow[j] = cell.DateCellValue.ToString();
                                                        dataRow[j] = cell.DateCellValue.Date.ToString();
                                                    }
                                                    else
                                                    {
                                                        dataRow[j] = cell.NumericCellValue.ToString();
                                                    }
                                                    //对时间格式（2015.12.5、2015/12/5、2015-12-5等）的处理  
                                              //      if (format == 14 || format == 31 || format == 57 || format == 58)
                                             //           dataRow[j] = cell.DateCellValue;
                                              //      else
                                              //          dataRow[j] = cell.NumericCellValue;
                                                    break;
                                                case CellType.String:
                                                    dataRow[j] = cell.StringCellValue;
                                                    break;
                                            }
                                        }
                                    }
                                    dataTable.Rows.Add(dataRow);
                                }
                            }
                        }
                    }
                }
                return dataTable;
            }
            catch (Exception)
            {
                if (fs != null)
                {
                    fs.Close();
                }
                return dataTable;
            }
        }

        /// <summary>
        /// Excel导出
        /// </summary>
        /// <param name="dt">需要导出的表</param>
        /// <returns></returns>
        public static bool DataTableToExcel(DataTable dt)
        {
            bool result = false;
            IWorkbook workbook = null;
            FileStream fs = null;
            IRow row = null;
            ISheet sheet = null;
            ICell cell = null;
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    workbook = new HSSFWorkbook();
                    sheet = workbook.CreateSheet("Sheet0");//创建一个名称为Sheet0的表  
                    int rowCount = dt.Rows.Count;//行数  
                    int columnCount = dt.Columns.Count;//列数  

                    //设置列头  
                    row = sheet.CreateRow(0);//excel第一行设为列头  
                    for (int c = 0; c < columnCount; c++)
                    {
                        cell = row.CreateCell(c);
                        cell.SetCellValue(dt.Columns[c].ColumnName);
                    }

                    //设置每行每列的单元格,  
                    for (int i = 0; i < rowCount; i++)
                    {
                        row = sheet.CreateRow(i + 1);
                        for (int j = 0; j < columnCount; j++)
                        {
                            cell = row.CreateCell(j);//excel第二行开始写入数据  
                            cell.SetCellValue(dt.Rows[i][j].ToString());
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

        /// <summary>
        /// 模板文件下载
        /// </summary>
        public static  void DownLoad(string FN)
        {
            string fileName =FN;//客户端保存的文件名
          //  string filePath = Server.MapPath("~/ExperimentTen/res/DownLoad/muban.xls");//路径
            string filePath = HttpContext.Current.Server.MapPath("~/ExperimentTen/res/DownLoad/"+fileName);//路径

            //以字符流的形式下载文件
            FileStream fs = new FileStream(filePath, FileMode.Open);
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
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="FileUpload1">文件框</param>
        /// <param name="GridView1">表单</param>
        public static void upload(FineUI.FileUpload FileUpload1,GridView grid)
        {
            bool fileOK = true;


            //文件的上传路径
            string path = HttpContext.Current.Server.MapPath("~/ExperimentTen/res/Import/");


            //判断上传文件夹是否存在，若不存在，则创建
            if (!Directory.Exists(path))
            {
                //创建文件夹 
                Directory.CreateDirectory(path);
            }
            if (FileUpload1.HasFile)
            {


                //允许上传的类型
                //string[] allowExtesions = { ".doc", ".xls", ".rar", ".zip", ".ppt" };
                //for (int i = 0; i < allowExtesions.Length; i++)
                //{
                //    if (fileExtesion == allowExtesions[i])
                //    {
                //        //文件格式正确 允许上传
                //        fileOK = true;
                //    }
                //}


                string name = FileUpload1.FileName;// 获得上传文件的名字.
                int size = FileUpload1.PostedFile.ContentLength;// 文件大小.
                if (size > (1000000 * 1024))
                {
                    fileOK = false;
                    //   Label2.Text = "文件过大";
                }
                string type = FileUpload1.PostedFile.ContentType;// 文件类型.


                // 获取上传文件的类型(后缀)
                string fileExtesion = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();


                //string type2 = name.Substring(name.LastIndexOf(".") + 1);// LastIndexOf()最后一个索引位置匹配.Substring()里面的+1是重载.
                if (fileOK)
                {
                    try
                    {
                        Random ranNum = new Random();


                        // 在1和1000之间的随机正整数
                      //  int num = ranNum.Next(1, 1000);
                        // 获取当前时间
                        string newname =System.DateTime.Now.ToString("yyyyMMddHHmm");
                        // 声明文件名，防止重复
                        newname = newname + name + fileExtesion;

                        string ipath = HttpContext.Current.Server.MapPath("~/ExperimentTen/res/Import/") + "\\" + newname; // 取得根目录下面的upimg目录的路径.
                        string fpath = HttpContext.Current.Server.MapPath("~/ExperimentTen/res/Import/") + "\\" + newname;
                        string wpath = "~/ExperimentTen/res/Import/\\" + newname; // 获得虚拟路径
                        if (fileExtesion == ".jpg" || fileExtesion == ".gif" || fileExtesion == ".bmp" || fileExtesion == ".png")
                        {
                            FileUpload1.SaveAs(ipath); // 保存方法,参数是一个地址字符串.
                                                       //  Image1.ImageUrl = wpath;
                                                       //  Label1.Text = "你传的文件名是:" + name + "<br>文件大小为:" + size + "字节<br>文件类型是:" + type +
                                                       //     "<br>后缀是:" + fileExtesion + "<br>实际路径是:" + ipath + "<br>虚拟路径是:" + fpath;
                                                       //  Image1.Visible = true;
                                                       // fileUrl.Text = ipath;
                        }
                        else
                        {
                            //   Image1.Visible = false;
                            FileUpload1.SaveAs(fpath);
                            //   Label1.Text = "你传的文件名是:" + name + "<br>文件大小为:" + size + "字节<br>文件类型是:" + type +
                            //     "<br>后缀是:" + fileExtesion + "<br>实际路径是:" + ipath + "<br>虚拟路径是:" + fpath;
                            //   fileUrl.Text = fpath;
                        }
                        // FileUpload1.PostedFile.SaveAs(path + newname);


                        //Session["filename"] = newname;
                        //     Label2.Text = "上传成功";
                        //   fileName.Text = name;
                        //  filesize.Text = size.ToString();
                        DataTable dt = new DataTable();
                        dt = NPOI_EXCEL.ExcelToDataTable(ipath, true);
                        grid.DataSource = dt;
                        grid.DataBind();
                        //lab_upload.Text = "上传成功";
                    }
                    catch (Exception ex)
                    {


                        //   Label2.Text = "上传失败";
                        throw ex;
                    }
                }
            }
            else
            {
                //尚未选择文件
                //  Label2.Text = "尚未选择任何文件，请选择文件";
                return;
            }
        }

        public static void upload(System.Web.UI.WebControls.FileUpload FileUpload1, GridView GridView1)
        {
            bool fileOK = true;
            

            //文件的上传路径
            string path = HttpContext.Current.Server.MapPath("~/ExperimentTen/res/Import/");


            //判断上传文件夹是否存在，若不存在，则创建
            if (!Directory.Exists(path))
            {
                //创建文件夹 
                Directory.CreateDirectory(path);
            }
            if (FileUpload1.HasFile)
            {


                //允许上传的类型
                //string[] allowExtesions = { ".doc", ".xls", ".rar", ".zip", ".ppt" };
                //for (int i = 0; i < allowExtesions.Length; i++)
                //{
                //    if (fileExtesion == allowExtesions[i])
                //    {
                //        //文件格式正确 允许上传
                //        fileOK = true;
                //    }
                //}


                string name = FileUpload1.FileName;// 获得上传文件的名字.
                int size = FileUpload1.PostedFile.ContentLength;// 文件大小.
                if (size > (1000000 * 1024))
                {
                    fileOK = false;
                 //   Label2.Text = "文件过大";
                }
                string type = FileUpload1.PostedFile.ContentType;// 文件类型.


                // 获取上传文件的类型(后缀)
                string fileExtesion = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();


                //string type2 = name.Substring(name.LastIndexOf(".") + 1);// LastIndexOf()最后一个索引位置匹配.Substring()里面的+1是重载.
                if (fileOK)
                {
                    try
                    {
                        Random ranNum = new Random();


                        // 在1和1000之间的随机正整数
                      //  int num = ranNum.Next(1, 1000);
                        // 获取当前时间
                        string newname = System.DateTime.Now.ToString("yyyyMMddHHmm");
                        // 声明文件名，防止重复
                        newname = newname + name+fileExtesion;

                        string ipath = HttpContext.Current.Server.MapPath("~/ExperimentTen/res/Import/") + "\\" + newname; // 取得根目录下面的upimg目录的路径.
                        string fpath = HttpContext.Current.Server.MapPath("~/ExperimentTen/res/Import/") + "\\" + newname;
                        string wpath = "~/ExperimentTen/res/Import/\\" + newname; // 获得虚拟路径
                        if (fileExtesion == ".jpg" || fileExtesion == ".gif" || fileExtesion == ".bmp" || fileExtesion == ".png")
                        {
                            FileUpload1.SaveAs(ipath); // 保存方法,参数是一个地址字符串.
                          //  Image1.ImageUrl = wpath;
                          //  Label1.Text = "你传的文件名是:" + name + "<br>文件大小为:" + size + "字节<br>文件类型是:" + type +
                           //     "<br>后缀是:" + fileExtesion + "<br>实际路径是:" + ipath + "<br>虚拟路径是:" + fpath;
                          //  Image1.Visible = true;
                           // fileUrl.Text = ipath;
                        }
                        else
                        {
                         //   Image1.Visible = false;
                            FileUpload1.SaveAs(fpath);
                        //   Label1.Text = "你传的文件名是:" + name + "<br>文件大小为:" + size + "字节<br>文件类型是:" + type +
                           //     "<br>后缀是:" + fileExtesion + "<br>实际路径是:" + ipath + "<br>虚拟路径是:" + fpath;
                         //   fileUrl.Text = fpath;
                        }
                        // FileUpload1.PostedFile.SaveAs(path + newname);


                        //Session["filename"] = newname;
                   //     Label2.Text = "上传成功";
                     //   fileName.Text = name;
                      //  filesize.Text = size.ToString();
                        DataTable dt = new DataTable();
                        dt = NPOI_EXCEL.ExcelToDataTable(ipath, true);
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        //lab_upload.Text = "上传成功";
                    }
                    catch (Exception ex)
                    {


                     //   Label2.Text = "上传失败";
                        throw ex;
                    }
                }
            }
            else
            {
                //尚未选择文件
              //  Label2.Text = "尚未选择任何文件，请选择文件";
                return;
            }
        }


        public static void upload(System.Web.UI.WebControls.FileUpload FileUpload1,FineUI.Grid grid )
        {
            bool fileOK = true;


            //文件的上传路径
            string path = HttpContext.Current.Server.MapPath("~/ExperimentTen/res/Import/");


            //判断上传文件夹是否存在，若不存在，则创建
            if (!Directory.Exists(path))
            {
                //创建文件夹 
                Directory.CreateDirectory(path);
            }
            if (FileUpload1.HasFile)
            {


                //允许上传的类型
                //string[] allowExtesions = { ".doc", ".xls", ".rar", ".zip", ".ppt" };
                //for (int i = 0; i < allowExtesions.Length; i++)
                //{
                //    if (fileExtesion == allowExtesions[i])
                //    {
                //        //文件格式正确 允许上传
                //        fileOK = true;
                //    }
                //}


                string name = FileUpload1.FileName;// 获得上传文件的名字.
                int size = FileUpload1.PostedFile.ContentLength;// 文件大小.
                if (size > (1000000 * 1024))
                {
                    fileOK = false;
                    //   Label2.Text = "文件过大";
                }
                string type = FileUpload1.PostedFile.ContentType;// 文件类型.


                // 获取上传文件的类型(后缀)
                string fileExtesion = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();


                //string type2 = name.Substring(name.LastIndexOf(".") + 1);// LastIndexOf()最后一个索引位置匹配.Substring()里面的+1是重载.
                if (fileOK)
                {
                    try
                    {
                        Random ranNum = new Random();


                        // 在1和1000之间的随机正整数
                        //  int num = ranNum.Next(1, 1000);
                        // 获取当前时间
                        string newname = System.DateTime.Now.ToString("yyyyMMddHHmm");
                        // 声明文件名，防止重复
                        newname = newname + name + fileExtesion;

                        string ipath = HttpContext.Current.Server.MapPath("~/ExperimentTen/res/Import/") + "\\" + newname; // 取得根目录下面的upimg目录的路径.
                        string fpath = HttpContext.Current.Server.MapPath("~/ExperimentTen/res/Import/") + "\\" + newname;
                        string wpath = "~/ExperimentTen/res/Import/\\" + newname; // 获得虚拟路径
                        if (fileExtesion == ".jpg" || fileExtesion == ".gif" || fileExtesion == ".bmp" || fileExtesion == ".png")
                        {
                            FileUpload1.SaveAs(ipath); // 保存方法,参数是一个地址字符串.
                                                       //  Image1.ImageUrl = wpath;
                                                       //  Label1.Text = "你传的文件名是:" + name + "<br>文件大小为:" + size + "字节<br>文件类型是:" + type +
                                                       //     "<br>后缀是:" + fileExtesion + "<br>实际路径是:" + ipath + "<br>虚拟路径是:" + fpath;
                                                       //  Image1.Visible = true;
                                                       // fileUrl.Text = ipath;
                        }
                        else
                        {
                            //   Image1.Visible = false;
                            FileUpload1.SaveAs(fpath);
                            //   Label1.Text = "你传的文件名是:" + name + "<br>文件大小为:" + size + "字节<br>文件类型是:" + type +
                            //     "<br>后缀是:" + fileExtesion + "<br>实际路径是:" + ipath + "<br>虚拟路径是:" + fpath;
                            //   fileUrl.Text = fpath;
                        }
                        // FileUpload1.PostedFile.SaveAs(path + newname);


                        //Session["filename"] = newname;
                        //     Label2.Text = "上传成功";
                        //   fileName.Text = name;
                        //  filesize.Text = size.ToString();
                        DataTable dt = new DataTable();
                        dt = NPOI_EXCEL.ExcelToDataTable(ipath, true);
                        grid.DataSource = dt;
                        grid.DataBind();
                        //lab_upload.Text = "上传成功";
                    }
                    catch (Exception ex)
                    {


                        //   Label2.Text = "上传失败";
                        throw ex;
                    }
                }
            }
            else
            {
                //尚未选择文件
                //  Label2.Text = "尚未选择任何文件，请选择文件";
                return;
            }
        }

        public static DataTable upload(FineUI.FileUpload FileUpload1)
        {
            bool fileOK = true;
            DataTable datatable=new DataTable();

            //文件的上传路径
            string path = HttpContext.Current.Server.MapPath("~/ExperimentTen/res/Import/");


            //判断上传文件夹是否存在，若不存在，则创建
            if (!Directory.Exists(path))
            {
                //创建文件夹 
                Directory.CreateDirectory(path);
            }
            if (FileUpload1.HasFile)
            {


                //允许上传的类型
                //string[] allowExtesions = { ".doc", ".xls", ".rar", ".zip", ".ppt" };
                //for (int i = 0; i < allowExtesions.Length; i++)
                //{
                //    if (fileExtesion == allowExtesions[i])
                //    {
                //        //文件格式正确 允许上传
                //        fileOK = true;
                //    }
                //}


                string name = FileUpload1.FileName;// 获得上传文件的名字.
                int size = FileUpload1.PostedFile.ContentLength;// 文件大小.
                if (size > (1000000 * 1024))
                {
                    fileOK = false;
                    //   Label2.Text = "文件过大";
                }
                string type = FileUpload1.PostedFile.ContentType;// 文件类型.


                // 获取上传文件的类型(后缀)
                string fileExtesion = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();


                //string type2 = name.Substring(name.LastIndexOf(".") + 1);// LastIndexOf()最后一个索引位置匹配.Substring()里面的+1是重载.
                if (fileOK)
                {
                    try
                    {
                        Random ranNum = new Random();


                        // 在1和1000之间的随机正整数
                        //  int num = ranNum.Next(1, 1000);
                        // 获取当前时间
                        string newname = System.DateTime.Now.ToString("yyyyMMddHHmm");
                        // 声明文件名，防止重复
                        newname = newname + name + fileExtesion;

                        string ipath = HttpContext.Current.Server.MapPath("~/ExperimentTen/res/Import/") + "\\" + newname; // 取得根目录下面的upimg目录的路径.
                        string fpath = HttpContext.Current.Server.MapPath("~/ExperimentTen/res/Import/") + "\\" + newname;
                        string wpath = "~/ExperimentTen/res/Import/\\" + newname; // 获得虚拟路径
                        if (fileExtesion == ".jpg" || fileExtesion == ".gif" || fileExtesion == ".bmp" || fileExtesion == ".png")
                        {
                            FileUpload1.SaveAs(ipath); // 保存方法,参数是一个地址字符串.
                                                       //  Image1.ImageUrl = wpath;
                                                       //  Label1.Text = "你传的文件名是:" + name + "<br>文件大小为:" + size + "字节<br>文件类型是:" + type +
                                                       //     "<br>后缀是:" + fileExtesion + "<br>实际路径是:" + ipath + "<br>虚拟路径是:" + fpath;
                                                       //  Image1.Visible = true;
                                                       // fileUrl.Text = ipath;
                        }
                        else
                        {
                            //   Image1.Visible = false;
                            FileUpload1.SaveAs(fpath);
                            //   Label1.Text = "你传的文件名是:" + name + "<br>文件大小为:" + size + "字节<br>文件类型是:" + type +
                            //     "<br>后缀是:" + fileExtesion + "<br>实际路径是:" + ipath + "<br>虚拟路径是:" + fpath;
                            //   fileUrl.Text = fpath;
                        }
                        // FileUpload1.PostedFile.SaveAs(path + newname);


                        //Session["filename"] = newname;
                        //     Label2.Text = "上传成功";
                        //   fileName.Text = name;
                        //  filesize.Text = size.ToString();
                        DataTable dt = new DataTable();
                      datatable= NPOI_EXCEL.ExcelToDataTable(ipath, true);
                     //   grid.DataSource = dt;
                     //   grid.DataBind();
                        //lab_upload.Text = "上传成功";
                    //    return datatable;
                    }
                    catch (Exception ex)
                    {

                   //     return null;
                        //   Label2.Text = "上传失败";
                        throw ex;
                    }
                   
                }
            }
            else
            {
                //尚未选择文件
                //  Label2.Text = "尚未选择任何文件，请选择文件";
            //    return null;
            }
            return datatable;
        }

        public static DataTable getDataTable(FineUI.FileUpload FileUpload1)
        {
            DataTable datatable;
          datatable=  upload(FileUpload1);
          
            return datatable;
        }


        public static Stream RenderDataTableToExcel(DataTable SourceTable)
        {
            IWorkbook workbook = null;
            workbook = new HSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            //  IWorkbook workbook = null;
            ISheet sheet = null;
            sheet = workbook.CreateSheet();
            IRow headerRow = null;
            headerRow = sheet.CreateRow(0);
            IRow dataRow = null;

            // handling header. 
            foreach (DataColumn column in SourceTable.Columns)
                headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);

            // handling value. 
            int rowIndex = 1;

            foreach (DataRow row in SourceTable.Rows)
            {
                 dataRow = sheet.CreateRow(rowIndex);

                foreach (DataColumn column in SourceTable.Columns)
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                }

                rowIndex++;
            }

            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;

            sheet = null;
            headerRow = null;
            workbook = null;

            return ms;
        }
    }

    public class NPOIHelper
    {
        /// <summary>
        /// DataTable导出到Excel文件
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        /// <param name="strFileName">保存位置</param>
        public static void Export(DataTable dtSource, string strHeaderText, string strFileName)
        {
            using (MemoryStream ms = Export(dtSource, strHeaderText))
            {
                using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
        }

        /// <summary>
        /// DataTable导出到Excel的MemoryStream
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        public static MemoryStream Export(DataTable dtSource, string strHeaderText)
        {
            string sql1 = "select distinct Program,Date from [Working_hoursInfor] where SySe like '%" + Common.SySe + "%'";
            DataTable program = Common.datatable(sql1);

            HSSFWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("sheet1");

            #region 右击文件 属性信息
         /*   {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "NPOI";
                workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = "文件作者信息"; //填加xls文件作者信息
                si.ApplicationName = "创建程序信息"; //填加xls文件创建程序信息
                si.LastAuthor = "最后保存者信息"; //填加xls文件最后保存者信息
                si.Comments = "作者信息"; //填加xls文件作者信息
                si.Title = "标题信息"; //填加xls文件标题信息
                si.Subject = "主题信息";//填加文件主题信息
                si.CreateDateTime = DateTime.Now;
                workbook.SummaryInformation = si;
            }*/
            #endregion

            ICellStyle dateStyle = workbook.CreateCellStyle();
            IDataFormat format = workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

            #region 取得列宽
            /*       int[] arrColWidth = new int[dtSource.Columns.Count];
                   foreach (DataColumn item in dtSource.Columns)
                   {
                       arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
                   }
                   for (int i = 0; i < dtSource.Rows.Count; i++)
                   {
                       for (int j = 0; j < dtSource.Columns.Count; j++)
                       {
                           int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                           if (intTemp > arrColWidth[j])
                           {
                               arrColWidth[j] = intTemp;
                           }
                       }
                   }
                   */
            #endregion
            int rowIndex = 0;
            foreach (DataRow row in dtSource.Rows)
            {
                #region 新建表，填充表头，填充列头，样式
                if (rowIndex == 65535 || rowIndex == 0)
                {
                    if (rowIndex != 0)
                    {
                        sheet = workbook.CreateSheet();
                    } 

                    #region 表头
                    {

                        IRow row1 = sheet.CreateRow(0);
                        ICell cell = row1.CreateCell(0);
                        cell.SetCellValue(strHeaderText);
                        sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, program.Rows.Count + 3));//合并列  该方法的参数次序是：开始行号，结束行号，开始列号，结束列号。  
                                                                                                     //  row1.Height = 30 * 30; //行高  
                        cell.CellStyle = HeadStyle(workbook);

                        IRow row2 = sheet.CreateRow(1);
                        ICell cell2 = row2.CreateCell(0);
                        cell2.SetCellValue("学号");
                        sheet.AddMergedRegion(new CellRangeAddress(1, 2, 0, 0));
                        cell2.CellStyle = Sub_HeadStyle(workbook);
                      
                        ICell cell3 = row2.CreateCell(1);
                        cell3.SetCellValue("姓名");
                        sheet.AddMergedRegion(new CellRangeAddress(1, 2, 1, 1));
                       cell3.CellStyle = Sub_HeadStyle(workbook);

                        ICell cell4 = row2.CreateCell(2);
                        cell4.SetCellValue("班级");
                        sheet.AddMergedRegion(new CellRangeAddress(1, 2, 2, 2));
                        cell4.CellStyle = Sub_HeadStyle(workbook);

                        ICell cell5 = row2.CreateCell(program.Rows.Count + 3);
                        cell5.SetCellValue("合计");
                        sheet.AddMergedRegion(new CellRangeAddress(1, 2, program.Rows.Count + 3, program.Rows.Count + 3));
                         cell5.CellStyle = Sub_HeadStyle(workbook);
                      


                        for (int i = 3, j = 0; j < program.Rows.Count; i++, j++)
                        {
                            ICell cell_1 = row2.CreateCell(i);

                            cell_1.SetCellValue(Convert.ToDateTime(program.Rows[j][1]).Date);
                         
                             cell_1.CellStyle = Sub_HeadStyle(workbook);
                            ICellStyle cstyle = cell_1.CellStyle;
                            cstyle.Alignment = HorizontalAlignment.Center;
                            cstyle.DataFormat = dateStyle.DataFormat;
                            
                           // cell_1.CellStyle = dateStyle;

                        }

                        IRow row3 = sheet.CreateRow(2);
                        for (int i = 3, j = 0; j < program.Rows.Count; i++, j++)
                        {
                            ICell cell_2 = row3.CreateCell(i);
                            cell_2.SetCellValue(program.Rows[j][0].ToString());
                           cell_2.CellStyle = Sub_HeadStyle(workbook);
                        }
                    }
                    #endregion


                    #region 列头及样式
                    {
                       
                        for (int columnNum = 0; columnNum <= 26; columnNum++)
                        {
                            int columnWidth = sheet.GetColumnWidth(columnNum) / 256;//获取当前列宽度  
                            columnWidth = 20;
                            sheet.SetColumnWidth(columnNum, columnWidth * 256);
                        }
                    }
                    #endregion

                    rowIndex = 3;
                }
                #endregion


                #region 填充内容
               IRow dataRow = sheet.CreateRow(rowIndex);
                foreach (DataColumn column in dtSource.Columns)
                {
                    ICell newCell = dataRow.CreateCell(column.Ordinal);

                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String"://字符串类型
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime"://日期类型
                            DateTime dateV;
                            DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);

                            newCell.CellStyle = dateStyle;//格式化显示
                            break;
                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal"://浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }
                    ICellStyle cstyle = newCell.CellStyle;
                    cstyle.Alignment = HorizontalAlignment.Center;

                }
                #endregion

                rowIndex++;
            }
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
              
               // ms.Flush();
              //  ms.Position = 0;
                return ms;
            }
        }

        /// <summary>
        /// 用于Web导出
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        /// <param name="strFileName">文件名</param>
        public static void ExportByWeb(DataTable dtSource, string strHeaderText, string strFileName)
        {
            HttpContext curContext = HttpContext.Current;
            strFileName += DateTime.Now.Date.ToString();
            // 设置编码和附件格式
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AddHeader("Content-Disposition", string.Format("attachment; filename="+ strFileName +".xls"));
            curContext.Response.BinaryWrite(Export(dtSource, strHeaderText).GetBuffer());
            curContext.Response.End();
        }

        public static ICellStyle HeadStyle(HSSFWorkbook hwb)
        {
            ICellStyle tstyle = hwb.CreateCellStyle();
            tstyle.Alignment = HorizontalAlignment.Center;
            tstyle.VerticalAlignment = VerticalAlignment.Center;
            IFont tfont = hwb.CreateFont();
            tfont.FontHeight = 20 * 20;
            tfont.FontName = "华文宋体";
            //      tfont.Color = ;
            tfont.Boldweight = short.MaxValue;
            tstyle.SetFont(tfont);
            return tstyle;
        }
        public static ICellStyle Sub_HeadStyle(HSSFWorkbook hwb)
        {
            ICellStyle cstyle = hwb.CreateCellStyle();
            cstyle.Alignment = HorizontalAlignment.Center;
            cstyle.VerticalAlignment = VerticalAlignment.Center;
            IFont cfont = hwb.CreateFont();
            cstyle.WrapText = true; // 换行 要配合\n使用  
            cfont.FontHeight = 16 * 16;
            cfont.FontName = "华文楷体";
            cfont.Boldweight = short.MaxValue;
            cstyle.SetFont(cfont);
            return cstyle;
        }
    }


    public class NPOItest
    {
        public static void Batch_Update(DataTable dtSource)
        {
            string sql1 = "select distinct Program,Date from [Working_hoursInfor] where SySe like '%" + Common.SySe + "%'";
            DataTable program = Common.datatable(sql1);

            HSSFWorkbook hwb = new HSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            ICellStyle dateStyle = hwb.CreateCellStyle();
            IDataFormat format = hwb.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

            ISheet sheet = hwb.CreateSheet();//默认是sheet0  

            IRow row1 = sheet.CreateRow(0);
            ICell cell = row1.CreateCell(0);
            cell.SetCellValue("信管1501工时表");
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, program.Rows.Count+3));//合并列  该方法的参数次序是：开始行号，结束行号，开始列号，结束列号。  
          //  row1.Height = 30 * 30; //行高  
            cell.CellStyle = HeadStyle(hwb);

            IRow row2 = sheet.CreateRow(1);
           ICell cell2 = row2.CreateCell(0);
            cell2.SetCellValue("学号");
            sheet.AddMergedRegion(new CellRangeAddress(1, 2, 0, 0));
          //  row2.Height = 30 * 30;
            cell2.CellStyle = Sub_HeadStyle(hwb);
            //  sheet.SetColumnWidth(0, 16 * 256);

            ICell cell3 = row2.CreateCell(1);
            cell3.SetCellValue("姓名");
            sheet.AddMergedRegion(new CellRangeAddress(1, 2, 1, 1));          
            cell3.CellStyle = Sub_HeadStyle(hwb);

            ICell cell4 = row2.CreateCell(2);
            cell4.SetCellValue("班级");
            sheet.AddMergedRegion(new CellRangeAddress(1, 2, 2, 2));
            cell4.CellStyle = Sub_HeadStyle(hwb);

            ICell cell5 = row2.CreateCell(program.Rows.Count + 3);
            cell5.SetCellValue("合计");
            sheet.AddMergedRegion(new CellRangeAddress(1, 2, program.Rows.Count+3, program.Rows.Count + 3));
            cell5.CellStyle = Sub_HeadStyle(hwb);

            for (int i=3,j=0;j<program.Rows.Count;i++,j++)
            {
                ICell cell_1 = row2.CreateCell(i);

                cell_1.SetCellValue(Convert.ToDateTime( program.Rows[j][1]).Date);
                cell_1.CellStyle = Sub_HeadStyle(hwb);
               
            }

            IRow row3 = sheet.CreateRow(2);
            for (int i = 3, j = 0; j < program.Rows.Count; i++, j++)
            {
                ICell cell_2 = row3.CreateCell(i);
                cell_2.SetCellValue(program.Rows[j][0].ToString());
                cell_2.CellStyle = Sub_HeadStyle(hwb);
            }

            int rowindex = 3;
            foreach (DataRow row in dtSource.Rows)
            {
                #region 填充内容
                IRow dataRow = sheet.CreateRow(rowindex);
                foreach (DataColumn column in dtSource.Columns)
                {
                    ICell newCell = dataRow.CreateCell(column.Ordinal);

                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String"://字符串类型
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime"://日期类型
                            DateTime dateV;
                            DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);

                            newCell.CellStyle = dateStyle;//格式化显示
                            break;
                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal"://浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }
                   
                }
                #endregion
                rowindex++;
            }
       
            
         
             hwb.Write(ms);
             HttpContext curContext = HttpContext.Current;
             curContext.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=工时-"+DateTime.Now.Date+".xls"));
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = System.Text.Encoding.UTF8;
             curContext.Response.BinaryWrite(ms.ToArray());
             hwb = null;
             ms.Close(); ms.Dispose();
        }
        /// <summary>  
        /// 大标题  
        /// </summary>  
        /// <param name="hwb"></param>  
        /// <returns></returns>  
        public static ICellStyle HeadStyle(HSSFWorkbook hwb)
        {
           ICellStyle tstyle = hwb.CreateCellStyle();
            tstyle.Alignment = HorizontalAlignment.Center;
            tstyle.VerticalAlignment = VerticalAlignment.Center;
            IFont tfont = hwb.CreateFont();
            tfont.FontHeight = 22 * 22;
            tfont.FontName = "华文行楷";
      //      tfont.Color = ;
            tfont.Boldweight = short.MaxValue;
            tstyle.SetFont(tfont);
            return tstyle;
        }

        /// <summary>  
        /// 副标题  
        /// </summary>  
        /// <param name="hwb"></param>  
        /// <returns></returns>  
        public static ICellStyle Sub_HeadStyle(HSSFWorkbook hwb)
        {
            ICellStyle cstyle = hwb.CreateCellStyle();
            cstyle.Alignment = HorizontalAlignment.Center;
            cstyle.VerticalAlignment = VerticalAlignment.Center;
            IFont cfont = hwb.CreateFont();
            cstyle.WrapText = true; // 换行 要配合\n使用  
            cfont.FontHeight = 15 * 15;
            cfont.FontName = "微软雅黑";
            cfont.Boldweight = short.MaxValue;
            cstyle.SetFont(cfont);
            return cstyle;
        }
    }
    }