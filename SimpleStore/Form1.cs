using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SimpleStore
{
    public partial class frmMain : Form
    {
 //connect the Database
        SqlConnection sqlCon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\MINE\Projects\c#\SimpleStore\DB\SimpleStoreDB.mdf;Integrated Security=True;Connect Timeout=30");
        int ItemId = 0;
        public frmMain()
        {
            InitializeComponent();
        }

        private void BtnInsert_Click(object sender, EventArgs e)
        {
            try
            {
 //check the connection
                if(sqlCon.State== ConnectionState.Closed )
                {
                    sqlCon.Open();
                }
 //Insert data to DataBase
                if(btnInsert.Text == "INSERT")
                {
                    SqlCommand sqlCmd = new SqlCommand("insert&update", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@mode", "ADD");
                    sqlCmd.Parameters.AddWithValue("@itemCode", 0);
                    sqlCmd.Parameters.AddWithValue("@itemCd", txtCode.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@itemName", txtItem.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@itemBrand", txtBrand.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@RDate", dte1.Value);
                    sqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully INSERTED");
                    Reset();
                    FillDataGridView();
                }
//edit and update
                else
                {
                    SqlCommand sqlCmd = new SqlCommand("insert&update", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@mode", "EDIT");
                    sqlCmd.Parameters.AddWithValue("@itemCode", ItemId);
                    sqlCmd.Parameters.AddWithValue("@itemCd", txtCode.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@itemName", txtItem.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@itemBrand", txtBrand.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@RDate", dte1.Value);
                    sqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully UPDATED");
                    Reset();
                    FillDataGridView();
                }


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
            finally
            {
                sqlCon.Close();
            }

        }
//search data from the DataBase and Display in grid
        void FillDataGridView()
        {
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();

            SqlDataAdapter sqlDa = new SqlDataAdapter("View&Search", sqlCon);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDa.SelectCommand.Parameters.AddWithValue("@itemName", txtSearch.Text.Trim());
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            dgvItems.DataSource = dtbl;
            dgvItems.Columns[0].Visible = false;
            sqlCon.Close();
        }
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                FillDataGridView();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
        }

        private void DgvItems_DoubleClick(object sender, EventArgs e)
        {
//retrive data onto form
            if(dgvItems.CurrentRow.Index != -1)
            {
                ItemId = Convert.ToInt32(dgvItems.CurrentRow.Cells[0].Value.ToString());
                txtCode.Text = dgvItems.CurrentRow.Cells[1].Value.ToString();
                txtItem.Text = dgvItems.CurrentRow.Cells[2].Value.ToString();
                txtBrand.Text = dgvItems.CurrentRow.Cells[3].Value.ToString();
                dte1.Value = Convert.ToDateTime(dgvItems.CurrentRow.Cells[4].Value.ToString());
                btnInsert.Text = "UPDATE";
                btnDelete.Enabled = true;


            }
        }
//Reset the program
        void Reset()
        {
            txtCode.Text = txtItem.Text = txtBrand.Text = txtSearch.Text = "";
            btnInsert.Text = "INSERT";
            btnDelete.Enabled = false;
            ItemId = 0;
            FillDataGridView();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            Reset();
            FillDataGridView();
        }
//Delete data
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                    SqlCommand sqlCmd = new SqlCommand("Delete", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@itemId", ItemId);
                    sqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully DELETED");
                Reset();
                FillDataGridView();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
        }
    }
}
