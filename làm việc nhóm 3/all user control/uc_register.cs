using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace làm_việc_nhóm_3.all_user_control
{
   
    public partial class uc_register : UserControl
    {
        function fn = new function();
        string query;
        public uc_register()
        {
            InitializeComponent();
        }
        public void setComboBox(string query, ComboBox combo)
        {
            SqlDataReader sdr = fn.getForCombo(query); 
            try
            {
                combo.Items.Clear(); // Xóa các mục hiện có
                while (sdr.Read()) 
                { 
                    for(int i = 0; i < sdr.FieldCount; i++)
                    { 
                        combo.Items.Add(sdr.GetString(i)); 
                    } 
                } 

            } catch (Exception ex)
            { 
                MessageBox.Show("Xảy ra lỗi: " + ex.Message); 
            } finally 
            { 
                sdr.Close();
            }
            }
        private void cb_bed_SelectedIndexChanged(object sender, EventArgs e)
        {
            cb_room.SelectedIndex = -1;
            cb_nroom.Items.Clear();
            txt_price.Clear();

        }

        private void cb_room_SelectedIndexChanged(object sender, EventArgs e)
        {
            cb_nroom.Items.Clear();
            query = "select roomNo from rooms where bed = '" + cb_bed + "'and roomType ='" + cb_room + "' and booked = 'NO'";
            setComboBox(query, cb_nroom);

        }
        // biến toàn cục
        int rid;
        private void cb_nroom_SelectedIndexChanged(object sender, EventArgs e)
        {
            query = "select price, roomid from rooms where roomNo = '" + cb_nroom.Text + "'";
            DataSet ds = fn.GetData(query);
            txt_price.Text = ds.Tables[0].Rows[0][0].ToString();
            rid = int.Parse(ds.Tables[0].Rows[0][0].ToString());
        }

        private void btnaddpeople_Click(object sender, EventArgs e)
        {
            if (txt_name.Text != "" && txt_contact.Text != "" && txt_na.Text != "" && cb_sex.Text != "" && dtp_date.Text != "" && txt_id.Text != "" && txt_add.Text != "" && dtp_checkin.Text != "" && txt_price.Text != "")
            {
                string name = txt_name.Text;    
                Int64 phone = Int64.Parse(txt_contact.Text);
                string national = txt_na.Text;
                string sex = cb_sex.Text;
                string dob = dtp_date.Text;
                string id = txt_id.Text;
                string add = txt_add.Text;
                string checkin = dtp_checkin.Text;

                query = "insert into customer (name, phone, national, sex, dob, id, add, checkin) values ('" + name + "','" + phone + "','" + national + "','" + sex + "','" + dob + "','" + id + "','" + add + "','" + checkin + "'+"+ rid +") update rooms set booked ='YES' where roomNo = '"+ cb_nroom +"'";
                fn.setData(query, "Số phòng" + cb_nroom.Text + "Đăng ký khách hàng thành công");
                clearAll();
            } else
            {
                MessageBox.Show("Xin vui lòng nhập đầy đủ thông tin", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void clearAll()
        {
            txt_name.Clear();
            txt_contact.Clear();
            txt_na.Clear();
            cb_sex.SelectedIndex = -1;
            dtp_date.ResetText();
            txt_id.Clear();
            txt_add.Clear();
            dtp_checkin.ResetText();
            cb_bed.SelectedIndex = -1;
            cb_room.SelectedIndex = -1;
            cb_nroom.Items.Clear();
            txt_price.Clear();

        }

        private void uc_register_Load(object sender, EventArgs e)
        {

        }

        private void uc_register_Leave(object sender, EventArgs e)
        {
            clearAll();
        }
    }
}
