using ExamRoomPOC.Domain.Interfaces.Repositories;
using ExamRoomPOC.Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ExamRoomPOC.Infra.SQLRepositories
{
    public class NotesRepository : INotesRepository
    {
        public readonly IConfiguration _configuration;
        public string ExamRoomPOCConnectionString { get; set; }

        const string CommandToSelectAll = "SELECT * FROM Notes (NOLOCK)";
        const string CommandToSelectOne = "SELECT * FROM Notes (NOLOCK) WHERE Id = @Id";
        const string CommandToInsert = "INSERT INTO Notes VALUES (@Title, @Description)";
        const string CommandToUpdate = "UPDATE Notes SET Title = @Title, Description = @Description WHERE Id = @Id";
        const string CommandToDelete = "DELETE FROM Notes WHERE Id = @Id";

        public NotesRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            ExamRoomPOCConnectionString = _configuration["ConnectionStrings:ExamRoomPOCConnectionString"];
        }

        public async Task<List<Note>> SelectAll()
        {
            var notes = new List<Note>();

            try
            {
                await Task.Run(() =>
                {
                    using (SqlConnection connection = new SqlConnection(ExamRoomPOCConnectionString))
                    {
                        connection.Open();

                        SqlCommand command = new SqlCommand(CommandToSelectAll, connection);
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            notes.Add(new Note
                            {
                                Id = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                Description = reader.GetString(2)
                            });
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }

            return notes;
        }

        public async Task<Note> SelectOne(int id)
        {
            var note = new Note();

            try
            {
                await Task.Run(() =>
                {
                    using (SqlConnection connection = new SqlConnection(ExamRoomPOCConnectionString))
                    {
                        connection.Open();

                        SqlCommand command = new SqlCommand(CommandToSelectOne, connection);
                        command.Parameters.AddWithValue("@Id", id);
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            note = new Note
                            {
                                Id = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                Description = reader.GetString(2)
                            };
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }

            return note;
        }

        public async void Insert(Note note)
        {
            try
            {
                await Task.Run(() =>
                {
                    using (SqlConnection connection = new SqlConnection(ExamRoomPOCConnectionString))
                    {
                        connection.Open();

                        SqlCommand command = new SqlCommand(CommandToInsert, connection);

                        command.Parameters.AddWithValue("@Title", note.Title);
                        command.Parameters.AddWithValue("@Description", note.Description);

                        command.ExecuteNonQuery();
                    }
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task<Note> Update(Note note, int id)
        {
            try
            {
                await Task.Run(() =>
                {
                    using (SqlConnection connection = new SqlConnection(ExamRoomPOCConnectionString))
                    {
                        connection.Open();

                        SqlCommand command = new SqlCommand(CommandToUpdate, connection);

                        command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@Title", note.Title);
                        command.Parameters.AddWithValue("@Description", note.Description);

                        command.ExecuteNonQuery();
                    }
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }

            return note;
        }

        public async void Delete(int id)
        {
            try
            {
                await Task.Run(() =>
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
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }
    }
}
