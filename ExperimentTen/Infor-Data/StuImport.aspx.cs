using System;
using System.Data;
using FineUI;

namespace WHMS.Infor_Data
{
    public partial class StuImport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              //  Common.checklogin("../login.aspx");

            }
        }


        protected void btn_Click(object sender, EventArgs e)
        {
            DataTable dt = NPOI_EXCEL.getDataTable(FileUpload1);
            grid.DataSource = dt;
            grid.DataBind();
        }

        protected void btn1_Click(object sender, EventArgs e)
        {
            //上传并更新班级
            DataControl.UpdataStudent(grid);
        }
    }
}