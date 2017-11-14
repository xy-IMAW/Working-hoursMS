using FineUI;
using System;
using System.Data;
using System.Web.UI;
using System.IO;
using System.Text;


namespace WHMS.Account
{
    public partial class Account : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
           {
                Common.checklogin("../login.aspx");
             
                Bind1();
                btnAdd.OnClientClick = window1.GetShowReference("AccountAdd.aspx","添加管理员");
                
            }
        }
        private void Bind1()
        {
            string SqlString = "select * from Account_Student order by State ";
            DataSet ds = Common.dataSet(SqlString);
            gridExample.DataSource = ds;
            gridExample.DataBind();
        }

  

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (gridExample.SelectedRowIndex < 0)
            {
                Alert.Show("请选择一项进行删除", "警告", MessageBoxIcon.Warning);
            }
            else
            {
                string id = gridExample.SelectedRow.Values[0].ToString();//选中行的第一列数据为ID
                string sqlStr = "delete from Account where StuID= '" + id + " '";
                Common.ExecuteSql(sqlStr);
                this.Bind1();
                Alert.ShowInTop("删除成功", "信息", MessageBoxIcon.Information);

            }
         
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            window1.Hidden = false;
        }

        protected void window1_Close(object sender, WindowCloseEventArgs e)
        {
            this.Bind1();
        }
       
        
        
        #region excel_handle
        protected void Button1_Click(object sender, EventArgs e)
        {
            string excelname = DateTime.Now.ToString("yyMMddhhmmss");
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=" + excelname + ".xls");
            Response.ContentType = "application/-excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(GetGridTableHtml(gridExample));
            Response.End();
        }

        private string GetGridTableHtml(FineUI.Grid grid)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<meta http-equiv=\"content-type\" content=\"application/excel; charset=UTF-8\"/>");
            sb.Append("<table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;\">");
            sb.Append("<tr>");
            foreach (GridColumn column in grid.Columns)
            {
                sb.AppendFormat("<td>{0}</td>", column.HeaderText);
            }
            sb.Append("</tr>");
            foreach (GridRow row in grid.Rows)
            {
                sb.Append("<tr>");
                foreach (object value in row.Values)
                {
                    string html = value.ToString();
                    if (html.StartsWith(FineUI.Grid.TEMPLATE_PLACEHOLDER_PREFIX))
                    {
                        // 模板列
                        string templateID = html.Substring(FineUI.Grid.TEMPLATE_PLACEHOLDER_PREFIX.Length);
                        Control templateCtrl = row.FindControl(templateID);
                        html = GetRenderedHtmlSource(templateCtrl);
                    }
                    else
                    {
                        // 处理CheckBox
                        if (html.Contains("f-grid-static-checkbox"))
                        {
                            if (html.Contains("uncheck"))
                            {
                                html = "×";
                            }
                            else
                            {
                                html = "√";
                            }
                        }

                        // 处理图片
                        if (html.Contains("<img"))
                        {
                            string prefix = Request.Url.AbsoluteUri.Replace(Request.Url.AbsolutePath, "");
                            html = html.Replace("src=\"", "src=\"" + prefix);
                        }
                    }
                    sb.AppendFormat("<td>{0}</td>", html);
                }
                sb.Append("</tr>");
            }
            sb.Append("</table>");
            return sb.ToString();
        }
        /// <summary>
        /// 获取控件渲染后的HTML源代码
        /// </summary>
        /// <param name="ctrl"></param>
        /// <returns></returns>
        private string GetRenderedHtmlSource(Control ctrl)
        {
            if (ctrl != null)
            {
                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                    {
                        ctrl.RenderControl(htw);

                        return sw.ToString();
                    }
                }
            }
            return String.Empty;
        }
        #endregion

        protected void gridExample_PageIndexChange(object sender, GridPageEventArgs e)
        {
            gridExample.PageIndex = e.NewPageIndex;
        }
    }
}