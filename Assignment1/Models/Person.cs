using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Assignment1.Models
{
    public class Person
    {
        [Required(ErrorMessage = "Please Enter a Unique Login Name")]
        /* [Range(1,20,ErrorMessage ="Login Name Should be less than 20 words")]*/
        /* [RegularExpression(@"^[a-zA-Z''-'\s]{1,20}$",
              ErrorMessage = "Characters are not allowed.")]*/
        public string LoginName { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Enter Confirm Password")]
        [Compare("Password", ErrorMessage = "Please enter same confirm password as password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please Enter Full Name")]
        [DataType(DataType.Text)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Please Enter Email ID")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Enter Valid Email ID")]
        public string EmailId { get; set; }
        public int CityId { get; set; }

        [Required(ErrorMessage = "Please enter Phone Number")]
        /* [Range(1,10,ErrorMessage ="Please Enter Valid Mobile Number")]*/
        [DataType(DataType.PhoneNumber, ErrorMessage = "Please Enter Valid Mobile ")]
        public long Phone { get; set; }


        public static void InsertPerson(Person obj)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=JKJuly2022;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            try
            {
                cn.Open();
                SqlCommand cmdInsert = new SqlCommand();
                cmdInsert.Connection = cn;
                cmdInsert.CommandType = System.Data.CommandType.StoredProcedure;
                cmdInsert.CommandText = "InsertPerson";
                cmdInsert.Parameters.AddWithValue("@LoginName", obj.LoginName);
                cmdInsert.Parameters.AddWithValue("@Password", obj.Password);
                cmdInsert.Parameters.AddWithValue("@FullName", obj.FullName);
                cmdInsert.Parameters.AddWithValue("@EmailId", obj.EmailId);
                cmdInsert.Parameters.AddWithValue("@CityId", obj.CityId);
                cmdInsert.Parameters.AddWithValue("@Phone", obj.Phone);

                cmdInsert.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                cn.Close();
            }
        }

        public static bool ValidPerson(Person obj)
        {
            Person per = new Person();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=JKJuly2022;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            try
            {
                cn.Open();
                SqlCommand cmdValid = new SqlCommand();
                cmdValid.Connection = cn;
                cmdValid.CommandType = System.Data.CommandType.Text;
                cmdValid.CommandText = "select count(*) from Persons where LoginName= @LoginName and Password=@Password";
                cmdValid.Parameters.AddWithValue("@LoginName", obj.LoginName);
                cmdValid.Parameters.AddWithValue("@Password", obj.Password);

                int val = (int)cmdValid.ExecuteScalar();

                if (val == 1)
                {
                    return true;
                }


            }
            catch
            {

            }
            finally
            {
                cn.Close();
            }
            return false;
        }

        public bool isActive { get; set; }

        //==================================================

        public static Person GetDetails(string name)
        {
            Person per = new Person();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=JKJuly2022;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            try
            {
                cn.Open();
                SqlCommand cmdValid = new SqlCommand();
                cmdValid.Connection = cn;
                cmdValid.CommandType = System.Data.CommandType.Text;
                cmdValid.CommandText = "select * from Persons where LoginName= @LoginName";
                cmdValid.Parameters.AddWithValue("@LoginName", name);

                SqlDataReader dr = cmdValid.ExecuteReader();
                while (dr.Read())
                {
                    per.FullName = (string)dr["FullName"];
                    per.EmailId = (string)dr["EmailId"];
                    per.LoginName = (string)dr["LoginName"];
                }
                dr.Close();

            }
            catch
            {

            }
            finally
            {
                cn.Close();
            }
            return per;
        }
    }
}