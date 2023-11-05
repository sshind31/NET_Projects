using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Assignment1.Models
{
    public class City
    {
        public int CityId { get; set; }
        public string CityName { get; set; }

        public static List<City> GetAllCities()
        {
            List<City> cities = new List<City>();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=JKJuly2022;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            try 
            {
                cn.Open();
                SqlCommand cmdSelect = new SqlCommand();
                cmdSelect.Connection = cn;
                cmdSelect.CommandType = System.Data.CommandType.Text;
                cmdSelect.CommandText = "select * from Cities";

                SqlDataReader dr = cmdSelect.ExecuteReader();
                while (dr.Read())
                {
                    cities.Add(new City { CityId = (int)dr["CityId"], CityName = (string)dr["CityName"]});
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
            return cities;
        }
    }
}