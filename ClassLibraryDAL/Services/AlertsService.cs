using ClassLibraryDAL.Interfaces;
using ClassLibraryModels;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ClassLibraryDAL.Services
{
    public class AlertsService : IAlertsInterface
    {
        public int Create(AlertsModel ob)
        {
            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_CreateAlert", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", ob.UserId);
                        cmd.Parameters.AddWithValue("@VehicleId",(object?)ob.VehicleId ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@AlertType", ob.AlertType);
                        cmd.Parameters.AddWithValue("@Title", ob.Title);
                        cmd.Parameters.AddWithValue("@Description",(object?)ob.Description ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@IsRead", ob.IsRead);
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating alert: " + ex.Message);

                return 0;
            }
        }

        public List<AlertsModel> Read()
        {
            List<AlertsModel> alerts =new List<AlertsModel>();

            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_GetAlerts", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader =cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AlertsModel alert =new AlertsModel
                                    {
                                        AlertId =reader.GetInt64(reader.GetOrdinal("AlertId")),
                                        UserId =reader.GetInt64(reader.GetOrdinal("UserId")),
                                        VehicleId =reader.IsDBNull(reader.GetOrdinal("VehicleId"))? null: reader.GetInt64(reader.GetOrdinal("VehicleId")),
                                        AlertType =reader.GetString(reader.GetOrdinal("AlertType")),
                                        Title =reader.GetString(reader.GetOrdinal("Title")),
                                        Description =reader.IsDBNull(reader.GetOrdinal("Description"))? null: reader.GetString(reader.GetOrdinal("Description")),
                                        IsRead =reader.GetBoolean(reader.GetOrdinal("IsRead")),
                                        CreatedAt =reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
                                    };

                                alerts.Add(alert);
                            }
                        }
                    }
                }

                return alerts;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading alerts: " + ex.Message);
                return new List<AlertsModel>();
            }
        }

      
        public List<AlertsModel> GetByUserId(long userId)
        {
            List<AlertsModel> alerts =new List<AlertsModel>();

            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_GetAlertsByUserId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        using (SqlDataReader reader =cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AlertsModel alert =new AlertsModel
                                    {
                                        AlertId =reader.GetInt64(reader.GetOrdinal("AlertId")),
                                        UserId =reader.GetInt64(reader.GetOrdinal("UserId")),
                                        VehicleId =reader.IsDBNull(reader.GetOrdinal("VehicleId"))? null: reader.GetInt64(reader.GetOrdinal("VehicleId")),
                                        AlertType =reader.GetString(reader.GetOrdinal("AlertType")),
                                        Title =reader.GetString(reader.GetOrdinal("Title")),
                                        Description =reader.IsDBNull(reader.GetOrdinal("Description"))? null: reader.GetString(reader.GetOrdinal("Description")),
                                        IsRead =reader.GetBoolean(reader.GetOrdinal("IsRead")),
                                        CreatedAt =reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
                                    };

                                alerts.Add(alert);
                            }
                        }
                    }
                }

                return alerts;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting alerts by user: "+ ex.Message);
                return new List<AlertsModel>();
            }
        }

        public int Update(AlertsModel ob)
        {
            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateAlert", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@AlertId", ob.AlertId);
                        cmd.Parameters.AddWithValue("@VehicleId",(object?)ob.VehicleId ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@AlertType", ob.AlertType);
                        cmd.Parameters.AddWithValue("@Title", ob.Title);
                        cmd.Parameters.AddWithValue("@Description",(object?)ob.Description ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@IsRead", ob.IsRead);
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating alert: " + ex.Message);
                return 0;
            }
        }

        public int Delete(long alertId)
        {
            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_DeleteAlert", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@AlertId", alertId);
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting alert: " + ex.Message);
                return 0;
            }
        }
    }
}
