using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.MVC.Data;

namespace UserManagement.MVC.Models
{
    public class ReviewDataAccessLayer
    {
        string connectionString = "Server=localhost\\SQLEXPRESS;Database=PartyBusTESTY;Trusted_Connection=True;";
        public IEnumerable<Review> GetAllReviews(int busID)
        {
            List<Review> listReviews = new List<Review>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from Reviews RIGHT JOIN [Identity].[User] ON Reviews.userID=[Identity].[User].Id WHERE Reviews.busID = '"+ busID + "'", con);
                
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Review Review = new Review();

                    Review.Id = int.Parse(rdr["reviewsID"].ToString());
                    Review.UserId = rdr["userID"].ToString();
                    Review.Comment = rdr["Comment"].ToString();
                   // Review.User = rdr["UserName"].ToString();

                    listReviews.Add(Review);
                }
                con.Close();
            }
            return listReviews;
        }

        
       
        public void AddReview(int busID, string userId, string Comment)
        {
            string _query = "INSERT INTO Reviews (userID, busID, Comment) values ('" + userId + "', '" + busID + "', '" + Comment +"')";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = _query;

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
        public void DeleteReview(string Id)
        {
            string _query = "DELETE FROM Reviews where userID = @first";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = _query;

                    comm.Parameters.AddWithValue("@first", Id);

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
        public Bus GetBuses(int Id)
        {
            string _query = "SELECT * FROM Bus where busID = '" + Id+"'";
            Bus Restaurant = new Bus();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(_query, conn);

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Restaurant.busID = int.Parse(rdr["busID"].ToString());
                    Restaurant.model = rdr["model"].ToString();
                }
                conn.Close();
            }
            return Restaurant;
        }
        public RentCategory GetRentCategory(int Id)
        {
            string _query = "SELECT * FROM rentCategory where typeID = '"+Id+"'";
            RentCategory Category = new RentCategory();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(_query, conn);

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Category.typeID = int.Parse(rdr["busID"].ToString());
                    Category.categoryName = rdr["model"].ToString();
                }
                conn.Close();
            }
            return Category;
        }
    }
 
}
