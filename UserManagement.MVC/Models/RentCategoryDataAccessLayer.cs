using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.MVC.Models
{
    public class RentCategoryDataAccessLayer
    {

        string connectionString = "Server=localhost\\SQLEXPRESS;Database=PartyBus;Trusted_Connection=True;";
        public IEnumerable<RentCategory> GetAllCategory()
        {
            List<RentCategory> listCategory = new List<RentCategory>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from rentCategory", con);

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    RentCategory rentCategory = new RentCategory();

                    rentCategory.typeID = int.Parse(rdr["typeID"].ToString());
                    rentCategory.categoryName = rdr["categoryName"].ToString();
                    rentCategory.description = rdr["description"].ToString();
                    rentCategory.priceForHour = int.Parse(rdr["priceForHour"].ToString());


                    listCategory.Add(rentCategory);
                }
                con.Close();
            }
            return listCategory;
        }
        public void AddCategory(string Name, int hourPrice, string description)
        {
            string _query = "INSERT INTO rentCategory (categoryName,priceForHour,description) values (@first, @second, @third)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = _query;


                    comm.Parameters.AddWithValue("@first", Name);
                    comm.Parameters.AddWithValue("@second", hourPrice);
                    comm.Parameters.AddWithValue("@third", description);

                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception(ex.ToString());
                    }
                }
            }
        }
        public RentCategory GetCategory(int Id)
        {
            string _query = "SELECT * FROM rentCategory where typeID = '"+Id+"'";
            RentCategory rentCategory = new RentCategory();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(_query, conn);

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    rentCategory.typeID = int.Parse(rdr["typeID"].ToString());
                    rentCategory.categoryName = rdr["categoryName"].ToString();
                    rentCategory.description = rdr["description"].ToString();
                    rentCategory.priceForHour = int.Parse(rdr["priceForHour"].ToString());

                }
                conn.Close();
            }
            return rentCategory;
        }

    }
}
