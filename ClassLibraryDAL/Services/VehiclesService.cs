using ClassLibraryDAL.Interfaces;
using ClassLibraryModels;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ClassLibraryDAL.Services
{
    public class VehiclesService : IVehiclesInterface
    {
        public int Create(VehiclesModel ob)
        {
            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_CreateVehicle", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@UserId", ob.UserId);
                        cmd.Parameters.AddWithValue("@Name", ob.Name);
                        cmd.Parameters.AddWithValue("@PlateNumber", ob.PlateNumber);
                        cmd.Parameters.AddWithValue("@Color", ob.Color);
                        cmd.Parameters.AddWithValue("@VehicleType", ob.VehicleType);
                        cmd.Parameters.AddWithValue("@Status", ob.Status);

                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating vehicle: " + ex.Message);
                return 0;
            }
        }
        public List<VehiclesModel> Read()
        {
            List<VehiclesModel> vehicles = new List<VehiclesModel>();

            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_GetVehicles", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                VehiclesModel vehicle = new VehiclesModel
                                {
                                    VehicleId = reader.GetInt64(reader.GetOrdinal("VehicleId")),
                                    UserId = reader.GetInt64(reader.GetOrdinal("UserId")),
                                    Name = reader.GetString(reader.GetOrdinal("Name")),
                                    PlateNumber = reader.GetString(reader.GetOrdinal("PlateNumber")),
                                    Color = reader.GetString(reader.GetOrdinal("Color")),
                                    VehicleType = reader.GetString(reader.GetOrdinal("VehicleType")),
                                    Status = reader.GetString(reader.GetOrdinal("Status")),
                                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                                    UpdatedAt = reader.GetDateTime(reader.GetOrdinal("UpdatedAt"))
                                };
                                vehicles.Add(vehicle);
                            }
                        }
                    }
                }
                return vehicles;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading vehicles: " + ex.Message);
                return new List<VehiclesModel>();
            }
        }
        public int Update(VehiclesModel ob)
        {
            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_UpdateVehicle", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@VehicleId", ob.VehicleId);
                        cmd.Parameters.AddWithValue("@UserId", ob.UserId);
                        cmd.Parameters.AddWithValue("@Name", ob.Name);
                        cmd.Parameters.AddWithValue("@PlateNumber", ob.PlateNumber);
                        cmd.Parameters.AddWithValue("@Color", ob.Color);
                        cmd.Parameters.AddWithValue("@VehicleType", ob.VehicleType);
                        cmd.Parameters.AddWithValue("@Status", ob.Status);
                        cmd.Parameters.AddWithValue("@UpdatedAt", ob.UpdatedAt);

                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating vehicle: " + ex.Message);
                return 0;
            }
        }

        public int Delete(long vehicleId)
        {
            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_DeleteVehicle", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@VehicleId", vehicleId);
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting vehicle: " + ex.Message);
                return 0;
            }
        }

        public List<VehiclesModel> GetByUserId(long userId)
        {
            List<VehiclesModel> vehicles =new List<VehiclesModel>();

            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_GetVehiclesByUserId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@UserId", userId);

                        using (SqlDataReader reader =cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                VehiclesModel vehicle =new VehiclesModel
                                    {
                                        VehicleId =reader.GetInt64(reader.GetOrdinal("VehicleId")),

                                        UserId =reader.GetInt64(reader.GetOrdinal("UserId")),

                                        Name =reader.GetString(reader.GetOrdinal("Name")),

                                        PlateNumber =reader.GetString(reader.GetOrdinal("PlateNumber")),

                                        Color =reader.GetString(reader.GetOrdinal("Color")),

                                        VehicleType =reader.GetString(reader.GetOrdinal("VehicleType")),

                                        Status =reader.GetString(reader.GetOrdinal("Status")),

                                        CreatedAt =reader.GetDateTime(reader.GetOrdinal("CreatedAt")),

                                        UpdatedAt =reader.GetDateTime(reader.GetOrdinal("UpdatedAt"))
                                    };

                                vehicles.Add(vehicle);
                            }
                        }
                    }
                }

                return vehicles;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting vehicles by user: "+ ex.Message);

                return new List<VehiclesModel>();
            }
        }
    }
}
