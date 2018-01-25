using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using FineUI;
using System.Web.SessionState;
using System.Text;

namespace WHMS
{
    public class Common
    {


        #region common string
        private static string state;//管理员性质
        public static string State
        {
            set { state = value; }
            get { return state; }
        }

        private static string _ID;//管理员ID
        public static string ID
        {
            set { _ID = value; }
            get { return _ID; }
        }

        private static string _sid;//用于查询的id
        public static string Sid
        {
            set { _sid = value; }
            get { return _sid; }
        }
        private static string name;
        public static string Name
        {
            set { name = value; }
            get { return name; }
        }
        public static bool IsLogin = false;


        public static int grade;
        public static string Class;
        public static string SySe;
        public static string Program;
        public static string path;
        #endregion
        public static void checklogin(string url)
        {
            //state为空说明未正常登陆
            if (Common.state == "")
            {
                IsLogin = true;
                System.Web.HttpContext.Current.Response.Redirect(url);

            }
            else
            { IsLogin = false; }
        }
        #region sqlsever connection 
        private static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnStr"].ToString());
        private static SqlCommand cmd = new SqlCommand();
        /// <summary>
        /// 执行一条sql语句
        /// </summary>
        /// <param name="sqlStr">sql语句</param>
        public static void ExecuteSql(string sqlStr)
        {
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlStr;
                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Alert.Show(e.Message);
                throw e;
                //  throw new Exception(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        /// <summary>
        /// 返回一个数据集
        /// </summary>
        /// <param name="sqlStr">sql语句</param>
        /// <returns></returns>
        public static DataSet dataSet(string sqlStr)
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlStr;
                cmd.Connection = conn;
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            catch (Exception e)
            {
                Alert.Show(e.Message);
                throw e;
                //   throw new Exception(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        public static DataTable datatable(string sqlStr)
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlStr;
                cmd.Connection = conn;
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch (Exception e)
            {
                //throw new Exception(e.Message);
                string Message = "出现系统错误：" + e.Message;
                Alert.Show(Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
        /// <summary>
        /// 返回一个数据视图
        /// </summary>
        /// <param name="sqlStr">sql语句</param>
        /// <returns></returns>
        public static DataView dataView(string sqlStr)
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            DataView dv = new DataView();
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlStr;
                cmd.Connection = conn;
                conn.Open();
                da.SelectCommand = cmd;
                da.Fill(ds);
                dv = ds.Tables[0].DefaultView;
            }
            catch (Exception e)
            {
                Alert.Show("数据库错误：" + e.Message);
                throw e;
                // throw new Exception(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return dv;
        }
        public static SqlDataReader ExecuteRead(string sqlStr)
        {


            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlStr;
                cmd.Connection = conn;
                // conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                return reader;

            }
            catch (Exception e)
            {
                Alert.Show(e.Message);
                throw e;
                //   throw new Exception(e.Message);

            }
            //finally
            //{
            //   SqlDataReader reader = cmd.ExecuteReader();
            // if(reader!=null)
            //  conn.Close();
            // }
        }
        public static void Open()
        {
            cmd.Connection = conn;
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();
        }
        public static void close()
        {
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
        }
        #endregion
    }




    public class SessionManager
    {

        /// <summary>
        /// 检查是否正常登录
        /// </summary>
        /// <param name="url">与登录页的相对地址</param>
        public static void CheckLogin(string url)
        {
            if (Convert.ToString(System.Web.HttpContext.Current.Session["State"]) != "")//state为空说明未进行正常登录操作
            {
                System.Web.HttpContext.Current.Session["IsLogin"] = "true";//正常登录后设置IsLogin为true说明是登录无异常
            }
            else
            {
                System.Web.HttpContext.Current.Session["IsLogin"] = "false";//登录异常IsLogin设为false
                System.Web.HttpContext.Current.Response.Redirect(url);//并返回登录页面
            }

        }

        /// <summary>
        /// 设置登录者的state session
        /// </summary>
        /// <param name="id">登录者id</param>
        public static void setState(string id)
        {
            try
            {
                string sqlstr = "select State from Account where StuID = '" + id + "'";
                Common.Open();
                SqlDataReader re = Common.ExecuteRead(sqlstr);
                if (re.Read())
                {
                    //   Common.State = re.GetString(re.GetOrdinal("State"));
                    System.Web.HttpContext.Current.Session["State"] = re.GetString(re.GetOrdinal("State"));
                }
                Common.close();
            }
            catch (Exception e)
            {
                Alert.Show(e.Message);
            }

        }


    }
}

 