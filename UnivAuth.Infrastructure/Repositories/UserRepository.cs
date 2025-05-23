using UnivAuth.Domain.Entities;
using UnivAuth.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;
using System.Net;

namespace UnivAuth.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("dbPersona")!;
        }

        public async Task<User?> LoginAsync(string usuario, string pwd)
        {
            try
            {
                using var conn = new NpgsqlConnection(_connectionString);
                using var cmd = new NpgsqlCommand("SELECT * FROM tf_login_usuario(@usuario, @pwd);", conn)
                {
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddWithValue("usuario", usuario);
                cmd.Parameters.AddWithValue("pwd", pwd);

                await conn.OpenAsync();
                using var reader = await cmd.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    return new User
                    {
                        id = Convert.ToInt32(reader["id"]),
                        persona_id = Convert.ToInt32(reader["persona_id"]),
                        usr = usuario,
                        secreto = reader["secreto"].ToString()!,
                        activo = Convert.ToBoolean(reader["activo"]),
                        rol_id = Convert.ToInt32(reader["rol_id"]),
                        tfa = Convert.ToBoolean(reader["tfa"]),
                        status = HttpStatusCode.OK
                    };
                }

                return new User
                {
                    status = HttpStatusCode.Unauthorized,
                    mensaje = "Usuario o contraseña incorrectos"
                };
            }
            catch (Exception ex)
            {
                return new User
                {
                    status = HttpStatusCode.InternalServerError,
                    mensaje = ex.Message
                };
            }            
        }

        public async Task<User?> Login2faAsync(string usuario, string secreto)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            using var cmd = new NpgsqlCommand("SELECT * FROM tf_login_usuario_upd(@usuario, @secreto);", conn)
            {
                CommandType = CommandType.Text
            };

            cmd.Parameters.AddWithValue("usuario", usuario);
            cmd.Parameters.AddWithValue("secreto", secreto);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new User
                {
                    id = Convert.ToInt32(reader["id"]),
                    persona_id = Convert.ToInt32(reader["persona_id"]),
                    usr = usuario,
                    secreto = reader["secreto"].ToString()!,
                    activo = Convert.ToBoolean(reader["activo"]),
                    rol_id = Convert.ToInt32(reader["rol_id"]),
                    tfa = Convert.ToBoolean(reader["tfa"]),
                    nombre_usuario = reader["nombre_usuario"].ToString()!,
                    doc_id = reader["doc_id"].ToString()!
                };
            }

            return null;
        }
    }
}
