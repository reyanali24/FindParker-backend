using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
namespace ClassLibraryDAL
{
    public class DBHelper
    {
        public static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(
                            "Data Source=DESKTOP-RPKKE8B\\SQLEXPRESS;" +
                            "Initial Catalog=FindParker;" +
                            "Integrated Security=True;" +
                            "Encrypt=False;" +
                            "TrustServerCertificate=True;");
        }
    }
}
