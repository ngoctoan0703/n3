using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace làm_việc_nhóm_3.all_user_control
{
    public partial class uc_addroom : UserControl
    {
        function fn = new function();
        string query;
        public uc_addroom()
        {
            InitializeComponent();
        }

        private void uc_addroom_Load(object sender, EventArgs e)
        {
            query = "SELECT * FROM rooms";
            DataSet ds = fn.GetData(query);
            if (ds.Tables.Count > 0)
            {
                dtaGView.DataSource = ds.Tables[0];
            }
            else
            {
                MessageBox.Show("Không có dữ liệu.");

                // Xóa dữ liệu cũ trong TextBox
                clearAll();
            }
        }
            private void btnaddroom_Click(object sender, EventArgs e)
            {
                if (txtrnumber.Text != "" && txttype.Text != "" && txtprice.Text != "" && txttypebed.Text != "")
                {
                    // Lấy dữ liệu từ các ô nhập
                    string rn = txtrnumber.Text;
                    string t = txttype.Text;
                    string bed = txttypebed.Text;
                    Int64 price = Int64.Parse(txtprice.Text);

                    query = "insert into rooms (roomNo, roomType, bed, price) values ('" + rn + "','" + t + "','" + bed + "','" + price + "')";
                    fn.setData(query, "Đã thêm phòng");

                    // Làm mới dữ liệu
                    uc_addroom_Load(this, null);
                    clearAll();
                }
                else
                {
                    MessageBox.Show("Xin vui lòng điền đầy đủ thông tin", "Warning !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            public void clearAll()
            {
                txtrnumber.Clear();
                txttype.SelectedIndex = -1;
                txttypebed.SelectedIndex = -1;
                txtprice.Clear();
            }
            private void uc_addroom_Leave(object sender, EventArgs e)
            {
                clearAll();
            }
            private void uc_addroom_Enter(object sender, EventArgs e)
            {
                uc_addroom_Load(this, null);
            }
        }
    }
