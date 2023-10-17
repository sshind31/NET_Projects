using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginWeb.Models
{
    public class City
    {
        public int CityId { get; set; }
        public string CityName { get; set; }


        private string conString = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=JKJuly2022;Integrated Security=True";
        public List<SelectListItem> getCityList()
        {
            List<SelectListItem> CityList = new List<SelectListItem>();

            SqlConnection cn = new SqlConnection(conString);
            try
            {
                Console.WriteLine("All okay");
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select CityName,CityId from dbo.CityDetails";
                
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CityList.Add(new SelectListItem 
                    { 
                        Text = dr["CityName"].ToString(), 
                        Value = dr["CityId"].ToString() 
                    });
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

            return CityList;
        }
    }
}