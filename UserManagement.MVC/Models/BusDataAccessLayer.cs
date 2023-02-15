using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.MVC.Models
{
    public class BusDataAccessLayer
    {
        string connectionString = "Server=localhost\\SQLEXPRESS;Database=PartyBus;Trusted_Connection=True;";
        public IEnumerable<Bus> GetAllBuses()
        {
            List<Bus> listBuses = new List<Bus>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from Bus", con);
                
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Bus Bus = new Bus();
                   
                    Bus.busID = int.Parse(rdr["busID"].ToString());
                    Bus.model = rdr["model"].ToString();
                    Bus.hourPrice = float.Parse(rdr["hourPrice"].ToString());
                    Bus.maxPeople = int.Parse(rdr["maxPeople"].ToString());

                    listBuses.Add(Bus);
                }
                con.Close();
            }
            return listBuses;
        }
        public void AddBus(string model, float hourPrice, int maxPeople)
        {

            string _query = "INSERT INTO Bus (model,hourPrice,maxPeople) values (@first, @second, @third)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = _query;

                   
                    comm.Parameters.AddWithValue("@first", model);
                    comm.Parameters.AddWithValue("@second", hourPrice);
                    comm.Parameters.AddWithValue("@third", maxPeople);

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
        public void DeleteBus(int busID)
        {

            string _query = "DELETE FROM Bus where busID = @first";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = _query;

                    comm.Parameters.AddWithValue("@first", busID);

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
            string _query = "SELECT * FROM Bus where busID = '"+Id+"'";
            Bus Bus = new Bus();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(_query, conn);

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Bus.busID = int.Parse(rdr["busID"].ToString());
                    Bus.model = rdr["model"].ToString();
                    Bus.hourPrice = float.Parse(rdr["hourPrice"].ToString());
                    Bus.maxPeople = int.Parse(rdr["maxPeople"].ToString());

                }
                conn.Close();
            }
            return Bus;
        }
    }
 
}
