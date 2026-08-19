using ClassLibraryModels;
using Microsoft.Data.SqlClient;
using System;
using ClassLibraryDAL;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ClassLibraryDAL.Services
{
    public class MaskedCallsService : IMaskedCallsInterface
    {
        public int Create(MaskedCallsModel ob)
        {
            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_CreateMaskedCall", con))
                    {
                        cmd.CommandType =CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", ob.UserId);
                        cmd.Parameters.AddWithValue("@VehicleId", (object?)ob.VehicleId ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@CallerMaskedNumber", (object?)ob.CallerMaskedNumber?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@CallStatus", ob.CallStatus);
                        cmd.Parameters.AddWithValue("@CallDurationSeconds",(object?)ob.CallDurationSeconds ?? DBNull.Value);
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating masked call: "+ ex.Message);

                return 0;
            }
        }

        public List<MaskedCallsModel> Read()
        {
            List<MaskedCallsModel> calls = new List<MaskedCallsModel>();

            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_GetMaskedCalls", con))
                    {
                        cmd.CommandType =CommandType.StoredProcedure;
                        using (SqlDataReader reader =cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MaskedCallsModel call = new MaskedCallsModel
                                    {
                     CallId = reader.GetInt64(reader.GetOrdinal("CallId")),
                     UserId =reader.GetInt64(reader.GetOrdinal("UserId")),
                     VehicleId = reader.IsDBNull(reader.GetOrdinal("VehicleId"))? null : reader.GetInt64(reader.GetOrdinal("VehicleId")),
                     CallerMaskedNumber = reader.IsDBNull(reader.GetOrdinal("CallerMaskedNumber")) ? null: reader.GetString(reader.GetOrdinal("CallerMaskedNumber")),
                     CallStatus =reader.GetString(reader.GetOrdinal("CallStatus")),
                     CallDurationSeconds =reader.IsDBNull(reader.GetOrdinal( "CallDurationSeconds")) ? null: reader.GetInt32(reader.GetOrdinal("CallDurationSeconds")),
                     CalledAt =reader.GetDateTime(reader.GetOrdinal("CalledAt"))
                            };
                                calls.Add(call);
                            }
                        }
                    }
                }
                return calls;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading masked calls: "+ ex.Message);
                return new List<MaskedCallsModel>();
            }
        }

        public int Update(MaskedCallsModel ob)
        {
            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_UpdateMaskedCall", con))
                    {
                        cmd.CommandType =CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CallId", ob.CallId);
                        cmd.Parameters.AddWithValue("@UserId", ob.UserId);
                        cmd.Parameters.AddWithValue("@VehicleId",(object?)ob.VehicleId?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@CallerMaskedNumber",(object?)ob.CallerMaskedNumber ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@CallStatus", ob.CallStatus);
                        cmd.Parameters.AddWithValue("@CallDurationSeconds", (object?)ob.CallDurationSeconds ?? DBNull.Value);
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating masked call: " + ex.Message);
                return 0;
            }
        }

        public int Delete(long callId)
        {
            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_DeleteMaskedCall", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CallId", callId);
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "Error deleting masked call: "+ ex.Message);
                return 0;
            }
        }
        public List<MaskedCallsModel> GetByUserId(long userId)
        {
            List<MaskedCallsModel> calls =new List<MaskedCallsModel>();

            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_GetMaskedCallsByUserId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@UserId", userId);

                        using (SqlDataReader reader =cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MaskedCallsModel call =new MaskedCallsModel
                                    {
                                        CallId =reader.GetInt64(reader.GetOrdinal("CallId")),
                                        UserId =reader.GetInt64(reader.GetOrdinal("UserId")),

                                        VehicleId =reader.IsDBNull(reader.GetOrdinal("VehicleId"))? null: reader.GetInt64(reader.GetOrdinal("VehicleId")),
                                        CallerMaskedNumber =reader.IsDBNull(reader.GetOrdinal("CallerMaskedNumber"))? null: reader.GetString(reader.GetOrdinal("CallerMaskedNumber")),
                                        CallStatus =reader.GetString(reader.GetOrdinal("CallStatus")),

                                        CallDurationSeconds =reader.IsDBNull(reader.GetOrdinal("CallDurationSeconds"))? null: reader.GetInt32(reader.GetOrdinal("CallDurationSeconds")),
                                        CalledAt =reader.GetDateTime(reader.GetOrdinal("CalledAt"))
                                    };

                                calls.Add(call);
                            }
                        }
                    }
                }

                return calls;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting masked calls by user: "+ ex.Message);
                return new List<MaskedCallsModel>();
            }
        }
    }
}
