using FineUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace WHMS.Infor_Data
{
    public partial class Data : System.Web.UI.Page
    {
        public string t1;//学年
        public string t2;//学期
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!Page.IsPostBack)
            {
                Common.checklogin("../login.aspx");
                //判断是否从学生信息页面跳转过来的
                if (Common.Sid != "")
                {
                    txtId.Text = Common.Sid;
                    string sqlStr = "select SySe,Program,[Working_hours] from [Working_hoursInfor] where StuID=" + Common.Sid;
                    DataSet myds = Common.dataSet(sqlStr);
                    gridExample.DataSource = myds;
                    gridExample.DataBind();
                    btnAdd.OnClientClick = windowPop.GetShowReference("");
                }
       
                BindGrid3();
             
            }

        }

        //学期下拉框绑定
        protected void BindGrid3()
         
        {

            //学期绑定。九月为分界
            int year = DateTime.Now.Year;
            int year2 = DateTime.Now.Year + 1;
            if (DateTime.Now.Month < 9)
            {
                ListItem all = new ListItem();
                all.Text = "全部";
                DL1.Items.Add(all);

                for (int i = 1; i < 5; i++)
                {
                    ListItem li = new ListItem();
                    li.Text = li.Value =(--year).ToString() + "-"+ (--year2).ToString();
                    DL1.Items.Add(li);
                }
            }
            else
            {
                ListItem all = new ListItem();
                all.Text = "全部";
                DL1.Items.Add(all);

                for (int i = 1; i < 5; i++)
                {
                    ListItem li = new ListItem();
                    li.Text = li.Value = (year--).ToString() + "-"+ (year2--).ToString();
                    DL1.Items.Add(li);
                }
            }


            List<string> list2 = new List<string>();
            list2.Add("全部");
            list2.Add("1");
            list2.Add("2");

            DL2.DataSource = list2;
            DL2.DataBind();

        }
        protected void btnDelete_Click3(object sender, EventArgs e)
        {
          
            string id = gridExample.SelectedRow.Values[0].ToString();//选中行的第一列为ID

            string sqlStr = "delete from Working_hours where SySe= '" + id + " '";
            Common.ExecuteSql(sqlStr);
            this.BindGrid3();
            Alert.ShowInTop("删除成功", "信息", MessageBoxIcon.Information);
        }

        protected void gridExample_Sort(object sender, GridSortEventArgs e)
        {

        }

        protected void windowPop_Close(object sender, FineUI.WindowCloseEventArgs e)
        {
            this.BindGrid3();
        }
        #region select_handle
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            t1 = DL1.SelectedItem.Text;
            t2 = DL2.SelectedItem.Text;
            Common.Sid = txtId.Text;
            int count=0;
            StuID.Text = txtId.Text;
            string sql = "select StuName from Student where StuID='"+txtId.Text+"'";
            DataTable name = Common.datatable(sql);
            StuName.Text = name.Rows[0][0].ToString();

            if (t1 == "全部")
            {
                DL2.ForceSelection = true;
                string sqlStr = "select SySe,Program,[Working_hours] from [Working_hoursInfor] where StuID=" + Common.Sid;
                DataTable dt = Common.datatable(sqlStr);
                gridExample.DataSource = dt;
                gridExample.DataBind();
                count = OutPutSummaryData(dt);
                hourcount.Text = "获得的总工时为："+count;
            }
            else
            {
                if (t2 == "全部")
                {
                    string sqlStr = "select SySe,Program,[Working_hours] from [Working_hoursInfor] where (SySe like'%" + t1 + "%') and StuID =" + Common.Sid;
                    DataTable dt = Common.datatable(sqlStr);
                    gridExample.DataSource = dt;
                    gridExample.DataBind();
                    count = OutPutSummaryData(dt);
                    hourcount.Text =t1+ "学年获得的总工时为：" + count;

                }
                else
                {
                    string sqlStr = "select SySe,Program,Working_hours from [Working_hoursInfor] where (SySe like'" + t1 + "-" + t2 + "') and StuID =" + Common.Sid;
                    DataTable dt = Common.datatable(sqlStr);
                    gridExample.DataSource = dt;
                    gridExample.DataBind();
                    count = OutPutSummaryData(dt);
                    hourcount.Text =t1+"-"+t2+ "学期获得的总工时为：" + count;

                }

            }


        }



        private int OutPutSummaryData(DataTable dt)
        {
            int hours=0;
            foreach (DataRow dr in dt.Rows)
            {
                hours += Convert.ToInt32(dr["Working_hours"]);
            }
            JObject summary = new JObject();
            string str = "总工时";
            summary.Add("Working_hours",hours.ToString("F2"));
            summary.Add("SySe",str);
            
       gridExample.SummaryData = summary;
            return hours;
     
        }
        #endregion
        #region excel_handle
        protected void Button1_Click(object sender, EventArgs e)
        {
            string excelname = DateTime.Now.ToString("yyyyMMhhmmss");
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=" + excelname + ".xls");
            Response.ContentType = "application/x-xls";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(GetGridTableHtml(gridExample));
            Response.End();
        }

        private string GetGridTableHtml(Grid grid)
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
                    if (html.StartsWith(Grid.TEMPLATE_PLACEHOLDER_PREFIX))
                    {
                        // 模板列
                        string templateID = html.Substring(Grid.TEMPLATE_PLACEHOLDER_PREFIX.Length);
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