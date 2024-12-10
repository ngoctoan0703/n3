using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace làm_việc_nhóm_3
{
    internal class function
    {
            // kết nối dữ liệu
            protected SqlConnection getConnection()
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Admin\\OneDrive\\Hình ảnh\\Tài liệu\\dbks.mdf\";Integrated Security=True;Connect Timeout=30";
                return con;
            }
        // lấy dữ liệu
        public DataSet GetData(string query)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection con = getConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = query;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xảy ra lỗi: " + ex.Message);
            }
            return ds;
        }


        // update dữ liệu
        public void setData(string query, string message)
            {
                SqlConnection con = getConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                // mở csdl
                con.Open();
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            public SqlDataReader getForCombo(string query)
            {
                SqlConnection con = getConnection();
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader(); // ExecuteReader trả về SqlDataReader
                return sdr;
            }
        

    }

}
