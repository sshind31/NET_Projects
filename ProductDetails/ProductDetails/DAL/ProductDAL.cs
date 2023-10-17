using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using ProductDetails.Models;
using ProductDetails.Controllers;

namespace ProductDetails.DAL
{
    public class ProductDAL
    {
        private string conString= @"Data Source=(localdb)\ProjectsV13;Initial Catalog=ProductDetails;Integrated Security=True";
        public List<Product> GetAllProducts() 
        {
            List<Product> ProductList = new List<Product>();
            using (SqlConnection cn = new SqlConnection(conString)) 
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetProductMasterDetails";
                SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
                DataTable dtProducts = new DataTable();
                
                cn.Open();
                sqlDA.Fill(dtProducts);
                cn.Close();

                foreach(DataRow dr in dtProducts.Rows) 
                {
                    ProductList.Add(new Product
                    {
                        ProductID = Convert.ToInt32(dr["ProductID"]),
                        ProductName = dr["ProductName"].ToString(),
                        Price = Convert.ToDecimal(dr["Price"]),
                        Quantity = Convert.ToInt32(dr["Quantity"]),
                        Remark = dr["Remark"].ToString()
                    });
                }
            }
            return ProductList;
        }

        public bool InsertProductToDB(Product product)
        {
            int id=0;
            using (SqlConnection cn = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("InsertProductEntity", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@Quantity", product.Quantity);
                cmd.Parameters.AddWithValue("@Remark", product.Remark);

                cn.Open();
                id = cmd.ExecuteNonQuery();
                cn.Close();

            }
            return id > 0;

            /*//(For this type of sql operation all field are mandatory)
            SqlConnection cn = new SqlConnection(conString);
            try
            {
                Console.WriteLine("All okay");
                SqlCommand cmdInsert = new SqlCommand();
                cmdInsert.Connection = cn;
                cmdInsert.CommandType = CommandType.Text;
                cmdInsert.CommandText = "insert into dbo.ProductMaster(ProductName,Price,Quantity,Remark) values(@a,@b,@c,@d)";
                cmdInsert.Parameters.AddWithValue("@a", product.ProductName);
                cmdInsert.Parameters.AddWithValue("@b", product.Price);
                cmdInsert.Parameters.AddWithValue("@c", product.Quantity);
                cmdInsert.Parameters.AddWithValue("@d", product.Remark);
                cn.Open();
                id = cmdInsert.ExecuteNonQuery();
                Console.WriteLine("All okay");

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                cn.Close();
            }
            
            return id > 0;*/
        }

        public Product FetchByID(int id)
        {
            Product product = null;
            SqlConnection cn = new SqlConnection(conString);

            try
            {
                Console.WriteLine("All okay");
                SqlCommand cmdSelect = new SqlCommand();
                cmdSelect.Connection = cn;
                cmdSelect.CommandType = CommandType.Text;
                cmdSelect.CommandText = "select ProductID,ProductName,Price,Quantity,Remark from dbo.ProductMaster where ProductID=@a";
                cmdSelect.Parameters.AddWithValue("@a", id);
                SqlDataAdapter sqlDA = new SqlDataAdapter(cmdSelect);
                DataTable dtProducts = new DataTable();

                cn.Open();
                sqlDA.Fill(dtProducts);

                foreach (DataRow dr in dtProducts.Rows)
                {
                    product=new Product
                    {
                        ProductID = Convert.ToInt32(dr["ProductID"]),
                        ProductName = dr["ProductName"].ToString(),
                        Price = Convert.ToDecimal(dr["Price"]),
                        Quantity = Convert.ToInt32(dr["Quantity"]),
                        Remark = dr["Remark"].ToString()
                    };
                }

                Console.WriteLine("All okay");

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                cn.Close();
            }

            return product;
        }

        public bool updateProductDetails(Product product)
        {
            int id = 0;
            using (SqlConnection cn = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("UpdateProductEntity", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductID", product.ProductID);
                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@Quantity", product.Quantity);
                cmd.Parameters.AddWithValue("@Remark", product.Remark);

                cn.Open();
                id = cmd.ExecuteNonQuery();
                cn.Close();

            }
            return id > 0;
        }

        public bool deleteProductFromDB(int productid)
        {
            int id = 0;
            using (SqlConnection cn = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("DeleteProductEntity", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductID", productid);
                
                cn.Open();
                id = cmd.ExecuteNonQuery();
                cn.Close();

            }
            return id > 0;
        }

    }
}