using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginRegister.Models
{
    public class User
    {
        [Key]
        [DisplayName("Login name")]
        public string LoginName { get; set; }

        [Required(ErrorMessage ="This field is compulsary")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password should match with previous")]
        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DisplayName("Full Name")]
        [DataType(DataType.Text,ErrorMessage ="Do not enter numbers within name")]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress,ErrorMessage ="Please enter valid email address")]
        public string EmailId { get; set; }

        public string City { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber,ErrorMessage ="Please enter valid phone no")]
        public string Phone { get; set; }

        private string conString = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=Practice;Integrated Security=True";

        public List<SelectListItem> GetListOfCity()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            SqlConnection cn = new SqlConnection(conString);
            try
            {
                SqlCommand cmd = new SqlCommand("GetCityList", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while(dr.Read())
                {
                    list.Add(new SelectListItem
                    {
                        Text=dr["CityName"].ToString(),
                        Value= dr["CityName"].ToString()
                    });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                cn.Close();
            }

            return list;
        }

        public bool InsertUserDataToDB(User user)
        {
            int id = 0;

            SqlConnection cn = new SqlConnection(conString);
            try
            {
                SqlCommand cmd = new SqlCommand("InsertUserToDB", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LoginName",user.LoginName);
                cmd.Parameters.AddWithValue("@Password",user.Password);
                cmd.Parameters.AddWithValue("@FullName",user.FullName);
                cmd.Parameters.AddWithValue("@EmailId",user.EmailId);
                cmd.Parameters.AddWithValue("@City",user.City);
                cmd.Parameters.AddWithValue("@Phone",user.Phone);
                cn.Open();
                id = cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                cn.Close();
            }

            return id > 0;
        }

        public bool validateUser(LoginUser lu)
        {
            int id = 0;

            SqlConnection cn = new SqlConnection(conString);
            try
            {
                SqlCommand cmd = new SqlCommand("ValidateUserFromDB",cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LoginName", lu.LoginName);
                cmd.Parameters.AddWithValue("@Password", lu.Password);

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read()) { id++; }
                dr.Close();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                cn.Close();
            }

            return id > 0;
        }

        public User GetuserByLoginName(LoginUser lu)
        {
            User u=new User();

            SqlConnection cn = new SqlConnection(conString);
            try
            {
                SqlCommand cmd = new SqlCommand("ValidateUserFromDB", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LoginName", lu.LoginName);
                cmd.Parameters.AddWithValue("@Password", lu.Password);

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read()) 
                {
                    u = new User
                    {
                        LoginName = dr["LoginName"].ToString(),
                        Password = dr["Password"].ToString(),
                        ConfirmPassword = null,
                        FullName = dr["FullName"].ToString(),
                        EmailId = dr["EmailId"].ToString(),
                        City = dr["City"].ToString(),
                        Phone = dr["Phone"].ToString()
                    };
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cn.Close();
            }

            return u;
        }
    }
}