using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CM = System.Configuration.ConfigurationManager;//ConfigurationManager alias import


namespace MusicLibrary
{
    class DataAccess
    {
        private static string connectionString = CM.ConnectionStrings["MusicLibrary"].ConnectionString;

        /// <summary>
        /// Queries a SQL Server database with a provided SELECT statement.
        /// </summary>
        /// <param name="sql">The SELECT SQL Statement to execute and query data</param>
        /// <returns>DataTable containing results from the provided SQL statement</returns>
        public static DataTable GetData(string sql)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(SQLCleaner(sql), conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dt);
            }

            return dt;
        }

        /// <summary>
        /// Queries a SQL Server database with a provided collection of SELECT statements.
        /// </summary>
        /// <param name="sqlStatements">The SELECT SQL Statements to execute and query data</param>
        /// <returns>DataSet containing results from the proivded SQL Statements. DataTables within the returned DataSet are in the order of the provided SQL statements</returns>
        public static DataSet GetData(string[] sqlStatements)
        {
            DataSet ds = new DataSet();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                for (int i = 0; i < sqlStatements.Length; i++)
                {
                    sqlStatements[i] = SQLCleaner(sqlStatements[i]);
                }

                string sql = String.Join(";", sqlStatements);

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                for (int i = 0; i < sqlStatements.Length; i++)
                {
                    da.TableMappings.Add(i.ToString(), $"Data{i}");
                }

                da.Fill(ds);
            }

            return ds;
        }

        public static DataSet GetDatas(string sqlStatements)
        {
            DataSet ds = new DataSet();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                

                string sql = String.Join(";", sqlStatements);

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                for (int i = 0; i < sqlStatements.Length; i++)
                {
                    da.TableMappings.Add(i.ToString(), $"Data{i}");
                }

                da.Fill(ds);
            }

            return ds;
        }

        /// <summary>
        /// Queries a SQL Server database for a scalar (single) value from the provided SELECT statement.
        /// </summary>
        /// <param name="sql">The SELECT SQL Statement to execute and query data for a scalar (single) value</param>
        /// <returns>The scalar value of the result of the SQL Statement execution</returns>
        public static object GetValue(string sql)
        {
            object returnValue;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(SQLCleaner(sql), conn);

                conn.Open();
                returnValue = cmd.ExecuteScalar();
                conn.Close();
            }

            return returnValue;
        }

        /// <summary>
        /// Support for execution of NonQuery sql statements
        /// </summary>
        /// <param name="sql">The NonQuery sql statement to execute</param>
        /// <returns>The rows affected</returns>
        public static int SendData(string sql)
        {
            int rowsAffected = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(SQLCleaner(sql), conn);

                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
                conn.Close();
            }

            return rowsAffected;
        }

        /// <summary>
        /// Clean our sql statements
        /// </summary>
        /// <param name="sqlStatement"></param>
        /// <returns></returns>
        public static string SQLCleaner(string sql)
        {
            while (sql.Contains("  "))
                sql = sql.Replace("  ", " ");
            return sql.Replace(Environment.NewLine, "");
        }

        /// <summary>
        /// Remove extra space on the sql statements
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string SQLFix(string str)
        {
            return str.Replace("'", "''");
        }
    }
}
