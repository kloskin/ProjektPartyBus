using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System;
using Microsoft.VisualBasic;

namespace UserManagement.MVC.Models
{
    public class RentalDataAccessLayer
    {
        string connectionString = "Server=localhost\\SQLEXPRESS;Database=PartyBusTESTY;Trusted_Connection=True;";
        public IEnumerable<Rental> GetAllRental()
        {
            List<Rental> listRental = new List<Rental>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from Rental", con);

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Rental rental = new Rental();

                    rental.rentID = int.Parse(rdr["rentID"].ToString());
                    rental.userID = rdr["userID"].ToString();
                    rental.busID = int.Parse(rdr["busID"].ToString());
                    rental.typeID = int.Parse(rdr["typeID"].ToString());
                    rental.startDate = rdr["startDate"].ToString();
                    rental.Hours = int.Parse(rdr["Hours"].ToString());
                    rental.pickupLocation = rdr["pickupLocation"].ToString();
                    rental.finalPrice = float.Parse(rdr["rentID"].ToString());

                    listRental.Add(rental);
                }
                con.Close();
            }
            return listRental;
        }

        public void AddRental(string startDate, string pickupLocation, string userID, int busID, string description, int typeID, int Hours, float finalPrice)
        {
            //W tym miejscu nie wiedzielismy jak zrobić żeby dodawało do bazy danych (wczesniejsze zamowienia były dodawane ręcznie)

            float price = finalPrice * Hours;

            string _query = "INSERT INTO Rental (startDate, pickupLocation, userID, busID, description, typeID, Hours, finalPrice) VALUES ('" + startDate + "', '" + pickupLocation + "', '" + userID + "', '" + busID + "', '" + description + "', '" + typeID + "', '" + Hours + "', '" + price + "')";
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


        public Rental GetRental(string Id)
        {
            string _query = "SELECT * FROM Rental where userID = '" + Id + "'";
            Rental rental = new Rental();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(_query, conn);

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    rental.rentID = int.Parse(rdr["rentID"].ToString());
                    rental.userID = rdr["userID"].ToString();
                    rental.busID = int.Parse(rdr["busID"].ToString());
                    rental.typeID = int.Parse(rdr["typeID"].ToString());
                    rental.startDate = rdr["startDate"].ToString();
                    rental.Hours = int.Parse(rdr["Hours"].ToString());
                    rental.pickupLocation = rdr["pickupLocation"].ToString();
                    rental.finalPrice = float.Parse(rdr["rentID"].ToString());

                }
                conn.Close();
            }
            return rental;
        }
    }
}
