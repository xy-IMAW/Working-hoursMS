using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WHMS.Infor_Data
{
	public partial class GradeImport : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}
        protected void btn1_Click(object sender, EventArgs e)
        {
            //上传并更新班级
            DataTable dt = NPOI_EXCEL.getDataTable(FileUpload1);
            grid.DataSource = dt;
            grid.DataBind();

        }

        protected void btn_Click(object sender, EventArgs e)
        {
            DataControl.UpdataWorking_hours(grid);
        }
    }
}