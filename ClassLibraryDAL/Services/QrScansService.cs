using ClassLibraryDAL.Interfaces.ClassLibraryDAL.Interfaces;
using ClassLibraryModels;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ClassLibraryDAL.Services
{
    public class QrScansService : IQrScansInterface
    {
        public int Create(QrScansModel ob)
        {
            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_CreateQrScan", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@VehicleId", ob.VehicleId);
                        cmd.Parameters.AddWithValue("@ScanLocation",(object?)ob.ScanLocation ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ScanResult", ob.ScanResult);
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating QR scan: " + ex.Message);

                return 0;
            }
        }

        public List<QrScansModel> Read()
        {
            List<QrScansModel> scans =new List<QrScansModel>();

            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_GetQrScans", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader =cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                QrScansModel scan = new QrScansModel
                                    {
                                        ScanId = reader.GetInt64(reader.GetOrdinal("ScanId")),
                                        VehicleId =reader.GetInt64(reader.GetOrdinal( "VehicleId")),
                                        ScanLocation =reader.IsDBNull(reader.GetOrdinal("ScanLocation")) ? null: reader.GetString(reader.GetOrdinal("ScanLocation")),
                                        ScanResult =reader.GetString(reader.GetOrdinal("ScanResult")),
                                        ScannedAt =reader.GetDateTime(reader.GetOrdinal("ScannedAt"))
                                    };

                                scans.Add(scan);
                            }
                        }
                    }
                }

                return scans;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading QR scans: "+ ex.Message);
                return new List<QrScansModel>();
            }
        }

        public int Update(QrScansModel ob)
        {
            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_UpdateQrScan", con))
                    {
                        cmd.CommandType =CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ScanId", ob.ScanId);
                        cmd.Parameters.AddWithValue("@VehicleId", ob.VehicleId);
                        cmd.Parameters.AddWithValue("@ScanLocation", (object?)ob.ScanLocation?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ScanResult", ob.ScanResult);
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "Error updating QR scan: "
                    + ex.Message);

                return 0;
            }
        }

        public int Delete(long scanId)
        {
            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_DeleteQrScan", con))
                    {
                        cmd.CommandType =CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ScanId", scanId);
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting QR scan: "+ ex.Message);
                return 0;
            }
       
        }

        public List<QrScansModel> GetByVehicleId(long vehicleId)
        {
            List<QrScansModel> scans =
                new List<QrScansModel>();

            try
            {
                using (SqlConnection con = DBHelper.GetSqlConnection())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_GetQrScansByVehicleId", con))
                    {
                        cmd.CommandType =CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@VehicleId", vehicleId);
                        using (SqlDataReader reader =cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                QrScansModel scan =new QrScansModel
                                    {
                                        ScanId =reader.GetInt64(reader.GetOrdinal("ScanId")),
                                        VehicleId =reader.GetInt64(reader.GetOrdinal("VehicleId")),
                                        ScanLocation =reader.IsDBNull(reader.GetOrdinal("ScanLocation"))? null: reader.GetString(reader.GetOrdinal("ScanLocation")),
                                        ScanResult =reader.GetString(reader.GetOrdinal("ScanResult")),
                                        ScannedAt =reader.GetDateTime(reader.GetOrdinal("ScannedAt"))
                                    };
                                scans.Add(scan);
                            }
                        }
                    }
                }
                return scans;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting QR scans by vehicle: "+ ex.Message);
                return new List<QrScansModel>();
            }
        }
    }
}

