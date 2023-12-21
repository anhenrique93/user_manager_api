using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
using Npgsql;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Xml.Linq;
using TaskCircle.UserManagerApi.DTOs;
using TaskCircle.UserManagerApi.Infrastructure.Repositories.Interfaces;
using TaskCircle.UserManagerApi.Infrastructure.Setting;
using TaskCircle.UserManagerApi.Models;

namespace TaskCircle.UserManagerApi.Infrastructure.Repository;

public class UserRepository : IUserRepository
{

    private readonly ConnectionSetting? _connection;

    public UserRepository(IOptions<ConnectionSetting> connection)
    {
        _connection = connection.Value;
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        List<User> users = new List<User>();

        using (var connect = new NpgsqlConnection(_connection?.PostgreSQLString))
        {
            await connect.OpenAsync();

            using (var cmd = new NpgsqlCommand("SELECT * FROM getAllUsers()", connect))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        var user = new User()
                        {
                            IdUser = reader.GetInt32(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? null : reader.GetString(reader.GetOrdinal("Phone")),
                            Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? null : reader.GetString(reader.GetOrdinal("Address")),
                            Gender = reader.IsDBNull(reader.GetOrdinal("Gender")) ? null : new Gender()
                            {
                                IdGender = reader.GetInt32(reader.GetOrdinal("GenderId")),
                                Name = reader.GetString(reader.GetOrdinal("Gender"))
                            }
                        };
                        users.Add(user);
                    }
                }
            }
        }
        return users;
    }

    public async Task<User> GetById(int id)
    {
        User? user = null;

        using (var connect = new NpgsqlConnection(_connection?.PostgreSQLString))
        {
            await connect.OpenAsync();

            using (var cmd = new NpgsqlCommand("SELECT * FROM getuserById(@id)", connect))
            {
                cmd.Parameters.AddWithValue("id", id);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        user = new User
                        {
                            IdUser = reader.GetInt32(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? null : reader.GetString(reader.GetOrdinal("Phone")),
                            Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? null : reader.GetString(reader.GetOrdinal("Address")),
                            Gender = reader.IsDBNull(reader.GetOrdinal("Gender")) ? null : new Gender()
                            {
                                IdGender = reader.GetInt32(reader.GetOrdinal("GenderId")),
                                Name = reader.GetString(reader.GetOrdinal("Gender"))
                            }
                        };
                    }
                }
            }
        }
        return user;
    }

    public async Task<User> Create(User Entity)
    {

        using (var connect = new NpgsqlConnection(_connection?.PostgreSQLString))
        {
            await connect.OpenAsync();
            using (var cmd = new NpgsqlCommand("CALL addUser(@firstName, @lastName, @email, @phone, @address, @genderId)", connect))
            {
                cmd.Parameters.AddWithValue("firstName", Entity.FirstName);
                cmd.Parameters.AddWithValue("lastName", Entity.LastName);
                cmd.Parameters.AddWithValue("email", Entity.Email);
                cmd.Parameters.AddWithValue("phone", Entity.Phone ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("address", Entity.Address ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("genderId", Entity.Gender != null ? Entity.Gender.IdGender : (object)DBNull.Value); // Verifique se Entity.Gender é nulo antes de passar IdGender

                await cmd.ExecuteNonQueryAsync();
            }
        }

        return Entity;
    }

    public async Task<User> Update(User Entity)
    {
        using (var connect = new NpgsqlConnection(_connection?.PostgreSQLString))
        {
            await connect.OpenAsync();

            using (var cmd = new NpgsqlCommand("CALL updateUser(@id, @firstName, @lastName, @email, @phone, @address, @genderId)", connect))
            {
                cmd.Parameters.AddWithValue("id", Entity.IdUser);
                cmd.Parameters.AddWithValue("firstName", Entity.FirstName);
                cmd.Parameters.AddWithValue("lastName", Entity.LastName);
                cmd.Parameters.AddWithValue("email", Entity.Email);
                cmd.Parameters.AddWithValue("phone", Entity.Phone ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("address", Entity.Address ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("genderId", Entity.Gender?.IdGender ?? (object)DBNull.Value);

                await cmd.ExecuteNonQueryAsync();
            }
        }
        return Entity;
    }

    public async Task<User> Delete(int id)
    {

        User? user = await GetById(id);

        using (var connect = new NpgsqlConnection(_connection?.PostgreSQLString))
        {
            await connect.OpenAsync();

            using (var cmd = new NpgsqlCommand("CALL deleteUser(@id)", connect))
            {
                cmd.Parameters.AddWithValue("id", user.IdUser);

                await cmd.ExecuteNonQueryAsync();
            }
        }

        return user;
    }
}
