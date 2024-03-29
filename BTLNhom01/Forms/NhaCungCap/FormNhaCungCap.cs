﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTLNhom01
{
    public partial class FormNhaCungCap : Form
    {
        DBConfig dbConfig = new DBConfig();

        public void LoadDB()
        {
            guna2DataGridView1.DataSource = dbConfig.GetTable("SELECT * FROM tNhaCungCap");
            if (guna2DataGridView1.Rows.Count == 0)
            {
                btnUpdate.Enabled = btnDelete.Enabled = false;
            }
            else
            {
                btnUpdate.Enabled = btnDelete.Enabled = true;
            }
        }
        public FormNhaCungCap()
        {
            InitializeComponent();
            guna2DataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
        }

        private void FormNhaCungCap_Load(object sender, EventArgs e)
        {
            LoadDB();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FormThemNhaCungCap formThem = new FormThemNhaCungCap(this);
            formThem.Visible = true;
            this.Enabled = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xoá nhà cung cấp này không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string id = guna2DataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                try
                {
                    dbConfig.Excute($"DELETE FROM tNhaCungCap WHERE MaNCC = '{id}'");
                    LoadDB();
                    MessageBox.Show("Xoá thành công !");
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
                
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            FormSuaNhaCungCap formSua = new FormSuaNhaCungCap(this, guna2DataGridView1);
            formSua.Visible = true;
            formSua.changeToUpdateForm();
            this.Enabled = false;
        }
    }
}
