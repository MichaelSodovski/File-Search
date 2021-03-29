using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ProjectThree
{
    class DAL
    {
        public void SQLFactory(string searchText, string dir, string result)
        {
            string connectionString = "Data Source=.;Integrated Security=True";
            using (SqlConnection sql = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = sql;
                    command.CommandText = "insert into [SystemSearch].[dbo].[Search_tbl](SearchText,Dir,Result) values ('" + searchText + "','" + dir + "','" + result + "')";
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                    command.Connection.Close();

                }
            }
        }
    }
}
