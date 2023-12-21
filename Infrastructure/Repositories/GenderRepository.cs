using Microsoft.Extensions.Options;
using Npgsql;
using System;
using TaskCircle.UserManagerApi.Infrastructure.Repositories.Interfaces;
using TaskCircle.UserManagerApi.Infrastructure.Setting;
using TaskCircle.UserManagerApi.Models;

namespace TaskCircle.UserManagerApi.Infrastructure.Repositories
{
    public class GenderRepository : IGenderRepository
    {
        private readonly ConnectionSetting? _connection;

        public GenderRepository(IOptions<ConnectionSetting> connection)
        {
            _connection = connection.Value;
        }

        public async Task<Gender> getById(int id)
        {
            Gender? gender = null;

            using (var connect = new NpgsqlConnection(_connection?.PostgreSQLString))
            {
                await connect.OpenAsync();

                using (var cmd = new NpgsqlCommand("SELECT * FROM getGenderById(@id)", connect))
                {
                    cmd.Parameters.AddWithValue("id", id);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            gender = new Gender
                            {
                                IdGender = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Gender"))
                            };
                        }
                    }
                }
            }
            return gender;
        }
    }
}
