using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class Index : System.Web.UI.Page
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Integrated Security=True;Connect Timeout=30";
        private SqlConnection sqlCon;
        //private object lblSuccessMessage;
        //private object lblErrorMessage;
               
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Clear();
                if(!String.IsNullOrEmpty(Request.Query String["id"]))
                {
                    int userID = Convert.ToInt32(Request.QueryString["id"]);
                    using (SqlConnection sqlCon = new SqlConnection(connectionString))
                    {
                        sqlCon.Open();
                        SqlDataAdapter sqlDa = new SqlDataAdapter("UserViewByyID", sqlCon);
                        sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                        sqlDa.SelectCommand.Parameters.AddWithValue("@UserID,userID");
                        DataTable dtbl = new DataTable();
                        sqlDa.Fill(dtbl);
                        hfUserID.Value = userID.ToString();
                        txtFirstName.Text = dtbl.Rows[0][1].ToString();
                        txtLastName.Text = dtbl.Rows[0][2].ToString();
                        txtContact.Text = dtbl.Rows[0][3].ToString();
                        ddlGender.Items.FindByValue(dtbl.Rows[0][4].ToString()).Selected = true;
                        txtAddress.Text = dtbl.Rows[0][5].ToString();
                        txtUsername.Text = dtbl.Rows[0][6].ToString();
                        txtPassword.Text = dtbl.Rows[0][7].ToString();
                        txtPassword.Attributes.Add("value", dtbl.Rows[0][7].ToString());
                        txtConfirmPassword.Text = dtbl.Rows[0][7].ToString();
                        txtConfirmPassword.Attributes.Add("value", dtbl.Rows[0][7].ToString());

                    }
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "" || txtPassword.Text == "")
            {
                lblErrorMessage.Text = "Please fill Mandatory fields";
            }
            else if (txtPassword.Text != txtConfirmPassword.Text)
            {
                lblErrorMessage.Text = "Password do not match";
            }
            else
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("UserAddOrEditt", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@UserID", Convert.ToInt32(hfUserID.Value == "" ? "0" : hfUserID.Value));
                    sqlCmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@LastName", txtLastName.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Contact", txtContact.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Gender", txtGender.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());
                    sqlCmd.ExecuteNonQuery();
                    Clear();
                    lblSuccessmessage.Text = "Submitted Successfully";
                }
            }
        }

        protected void txtFirstName_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click1(object sender, EventArgs e)
        {

        }
        void Clear()
        {
            txtFirstName.Text = txtLastName.Text = txtContact.Text = txtGender.Text = txtAddress.Text = txtUsername.Text = txtPassword.Text = txtConfirmPassword.Text = "";
            hfUserID.Value = "";
            lblSuccessmessage.Text = lblErrorMessage.Text = "";
        }
    }

    
}