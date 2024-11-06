using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using UserControlAPI.Models;

namespace UserControlAPI.Data
{
    public class DBService
    {
        private readonly string _connectionString;

        public DBService(string connectionString)
        {
            _connectionString = connectionString;
        }

        #region Users
        public async Task<List<Users>> GetUsers(int? userid)
        {
            var list = new List<Users>();
            var data = new DataTable();
            var sql = "select * from Users";
            using (var con = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand(sql, con);

                await con.OpenAsync();
                using (var r = new SqlDataAdapter(cmd))
                {
                    r.Fill(data);
                }
                
                if(data != null)
                {
                    foreach(DataRow row in data.Rows)
                    {
                        list.Add(new Users
                        {
                            UsertId = Convert.ToInt32(row["UserId"]),
                            UserName = row["UserName"].ToString(),
                        });
                    }
                }
                
            }
            return userid != null ? list?.Where(x => x.UsertId == userid).ToList() : list;
        }

        public async Task<bool> AddUser(Users user)
        {
            try
            {
                var sql = "insert into users (UserName) values (@name)";
                using (var con = new SqlConnection(_connectionString))
                {
                    using (var cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@name", user.UserName);
                        await con.OpenAsync();
                        return await cmd.ExecuteNonQueryAsync() > 0;
                    }
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error in AddUser: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateUser(Users user)
        {
            try
            {
                var sql = "update Users set Name = @name where UserId = @userid";
                using (var con = new SqlConnection(_connectionString))
                {
                    using (var cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@userid", user.UsertId);
                        cmd.Parameters.AddWithValue("@name", user.UserName);
                        await con.OpenAsync();
                        return await cmd.ExecuteNonQueryAsync() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateUser: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteUser(int userid)
        {
            try
            {
                var sql = "delete from Users where UserId = @userid";
                using (var con = new SqlConnection(_connectionString))
                {
                    using (var cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@userid", userid);
                        await con.OpenAsync();
                        return await cmd.ExecuteNonQueryAsync() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteUser: {ex.Message}");
                return false;
            }
        }
        #endregion

        #region Groups
        public async Task<List<Groups>>? GetGroupPermissions(int? groupid)
        {
            var list = new List<Groups>();
            var data = new DataTable();
            using (var con = new SqlConnection(_connectionString))
            {
                var sql = "select * from Groups";
                var cmd = new SqlCommand(sql, con);

                await con.OpenAsync();

                using (var r = new SqlDataAdapter(cmd))
                {
                    r.Fill(data);
                }

                if (data != null)
                {
                    foreach (DataRow row in data.Rows)
                    {
                        list.Add(new Groups
                        {
                            GroupId = Convert.ToInt32(row["GroupId"]),
                            GroupName = row["GroupName"].ToString(),
                            GroupPermissionId = Convert.ToInt32(row["GroupPermissionId"]),
                            UsertId = Convert.ToInt32(row["UserId"]),
                        });
                    }
                }

            }
            return groupid != null ? list?.Where(x => x.GroupId == groupid).ToList() : list;
        }

        #endregion
    }
}
