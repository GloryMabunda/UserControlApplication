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

        #region Seed Data
        public async Task SeedDataAsync()
        {
            #region Populate Users Table
            var users = await GetUsers(null); 
            if (users == null || !users.Any())  
            {
                var defaultUsers = new List<Users>
                {
                    new Users { UserName = "John Doe" },
                    new Users { UserName = "Jane Smith" }
                };

                foreach (var user in defaultUsers)
                {
                    await AddUser(user); // Insert default users
                }
            }
            #endregion

            #region Populate GroupPermisions Table
            var groupPermissions = await GetGroupPermissions(null);  
            if (groupPermissions == null || !groupPermissions.Any())  
            {
                var sql = $" INSERT INTO GroupPermissions (GroupPermissionName) "
                          + "VALUES (@permissionName1),"
                          + "(@permissionName2)";

                using (var con = new SqlConnection(_connectionString))
                {
                    using (var cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@permissionName1", "Read");  
                        cmd.Parameters.AddWithValue("@permissionName2", "Write");  

                        await con.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            #endregion

            #region Populate Group Table
            var groups = await GetGroups(null); 
            if (groups == null || !groups.Any())  
            {
                var sql = $" INSERT INTO Groups (GroupId, GroupName, GroupPermissionId, UserId) "
                          + "VALUES (@groupid1, @group1, @permissionId1, @userId1), "
                          + "(@groupid2, @group2, @permissionId2, @userId2),"
                          + "(@groupid3, @group3, @permissionId3, @userId3),";

                using (var con = new SqlConnection(_connectionString))
                {
                    using (var cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@groupId1", 1);
                        cmd.Parameters.AddWithValue("@group1", "Admin");
                        cmd.Parameters.AddWithValue("@permissionId1", 1);  
                        cmd.Parameters.AddWithValue("@userId1", 1);  

                        cmd.Parameters.AddWithValue("@groupId2", 2);
                        cmd.Parameters.AddWithValue("@group2", "Clients");
                        cmd.Parameters.AddWithValue("@permissionId2", 2);  
                        cmd.Parameters.AddWithValue("@userId2", 2); 

                        cmd.Parameters.AddWithValue("@groupId3", 1);
                        cmd.Parameters.AddWithValue("@group2", "Other");
                        cmd.Parameters.AddWithValue("@permissionId2", 2);  
                        cmd.Parameters.AddWithValue("@userId2", 1);  

                        await con.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            #endregion

        }
        #endregion



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
                var sql = "insert into Users (UserName) values (@name)";
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
                var sql = "update Users set UserName = @name where UserId = @userid";
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

        #region Group
        public async Task<List<Groups>>? GetGroups(int? groupid)
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

        #region Group Permissions
        public async Task<List<GroupPermissions>>? GetGroupPermissions(int? perid)
        {
            var list = new List<GroupPermissions>();
            var data = new DataTable();
            using (var con = new SqlConnection(_connectionString))
            {
                var sql = "select * from GroupPermissions";
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
                        list.Add(new GroupPermissions
                        {
                            GroupPermissionId = Convert.ToInt32(row["GroupPermissionId"]),
                            GroupPermissionName = row["GroupPermissionName"].ToString(),                                            
                        });
                    }
                }

            }
            return perid != null ? list?.Where(x => x.GroupPermissionId == perid).ToList() : list;
        }

        #endregion
    }
}
