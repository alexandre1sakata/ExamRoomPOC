using ExamRoomPOC.Domain.Interfaces.Repositories;
using ExamRoomPOC.Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamRoomPOC.Infra.SQLRepositories
{
    public class UsersRepository : IUsersRepository
    {
        public readonly IConfiguration _configuration;
        public string ExamRoomPOCConnectionString { get; set; }

        const string CommandToSelectOneById = "SELECT * FROM Users (NOLOCK) WHERE Id = @Id";
        const string CommandToSelectOneByInfo = "SELECT * FROM Users (NOLOCK) WHERE Username = @Username AND Password = @Password";
        const string CommandToInsert = "INSERT INTO Users VALUES (@Username, @Password)";
        const string CommandToDelete = "DELETE FROM Users WHERE Id = @Id";

        public UsersRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            ExamRoomPOCConnectionString = _configuration["ConnectionStrings:ExamRoomPOCConnectionString"];
        }

        public User SelectById(int id)
        {
            var user = new User();

            try
            {
                using (SqlConnection connection = new SqlConnection(ExamRoomPOCConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(CommandToSelectOneById, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                                user = new User
                                {
                                    Id = reader.GetInt32(0),
                                    Username = reader.GetString(1),
                                    Password = reader.GetString(2)
                                };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }

            return user;
        }

        public User SelectByInfo(string username, string password)
        {
            var user = new User();

            try
            {
                using (SqlConnection connection = new SqlConnection(ExamRoomPOCConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(CommandToSelectOneByInfo, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                                user = new User
                                {
                                    Id = reader.GetInt32(0),
                                    Username = reader.GetString(1),
                                    Password = reader.GetString(2)
                                };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }

            return user;
        }

        public void Insert(User user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ExamRoomPOCConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(CommandToInsert, connection))
                    {
                        command.Parameters.AddWithValue("@Username", user.Username);
                        command.Parameters.AddWithValue("@Password", user.Password);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public void Delete(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ExamRoomPOCConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(CommandToDelete, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }
    }
}
