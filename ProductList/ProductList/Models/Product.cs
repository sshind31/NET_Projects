using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProductList.Models
{
    public class Product
    {
        [Required]
        public int ProductId { get; set; }

        [DisplayName("Product Name")]
        public string ProductName { get; set; }

        [Required]
        public decimal Rate { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Category { get; set; }

        private string conString = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=JKJuly2022;Integrated Security=True";

        //InsertProductToDB
        public bool InsertToDB(Product prod)
        {
            int id = 0;

            SqlConnection connect = new SqlConnection(conString);
            try 
            {
                SqlCommand cmd = new SqlCommand("InsertProductToDB",connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductId", prod.ProductId);
                cmd.Parameters.AddWithValue("@ProductName", prod.ProductName);
                cmd.Parameters.AddWithValue("@Rate", prod.Rate);
                cmd.Parameters.AddWithValue("@Description", prod.Description);
                cmd.Parameters.AddWithValue("@Category", prod.Category);
                connect.Open();
                id =cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                connect.Close();
            }

            return id > 0;
        }

        //AllProductList
        public List<Product> GetListOfProduct()
        {
            List<Product> ListOfProduct = new List<Product>();

            SqlConnection con = new SqlConnection(conString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("AllProductList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                while(dr.Read())
                {
                    ListOfProduct.Add(new Product
                    {
                        ProductId=Convert.ToInt32(dr["ProductId"]),
                        ProductName=(dr["ProductName"]).ToString(),
                        Rate = Convert.ToDecimal(dr["ProductId"]),
                        Description= (dr["Description"]).ToString(),
                        Category= (dr["Category"]).ToString()
                    });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }

            return ListOfProduct;
        }
    }
}