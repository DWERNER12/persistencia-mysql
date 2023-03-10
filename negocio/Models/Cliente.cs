using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Negocio.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }

        private static readonly string conexao = "Server=localhost;Database=persistencia_codigo_do_futuro;Uid=root;Pwd=root;";

        public void Salvar()
        {

            using (var conn = new MySqlConnection(conexao))
            {
                conn.Open();
                var query = $"insert into clientes(nome, email)values('{this.Nome}', '{this.Email}');";
                if (this.Id > 0)
                    query = $"update clientes set nome='{this.Nome}', email='{this.Email}' where id = {this.Id};";

                var command = new MySqlCommand(query, conn);
                command.ExecuteNonQuery();
                conn.Clone();

            }
        }
        public static List<Cliente> BuscaPorIdOuEmail(string idOuEmail)
        {
            var clientes = new List<Cliente>();
            using (var conn = new MySqlConnection(conexao))
            {
                conn.Open();
                var query = $"""
                    select * from clientes 
                    where id = '{idOuEmail}' or
                    nome like '%{idOuEmail}%'; 
                """;
                var command = new MySqlCommand(query, conn);
                var dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    clientes.Add(new Cliente
                    {
                        Id = Convert.ToInt32(dataReader["Id"]),
                        Nome = dataReader["nome"].ToString(),
                        Email = dataReader["email"].ToString(),
                    });
                }
                conn.Clone();
            }
            return clientes;
        }

        public static List<Cliente> Todos()
        {
            var clientes = new List<Cliente>();
            using (var conn = new MySqlConnection(conexao))
            {
                conn.Open();
                var query = $"""
                    select * from clientes;
                """;
                var command = new MySqlCommand(query, conn);
                var dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    clientes.Add(new Cliente
                    {
                        Id = Convert.ToInt32(dataReader["Id"]),
                        Nome = dataReader["nome"].ToString(),
                        Email = dataReader["email"].ToString(),
                    });
                }
                conn.Clone();
            }
            return clientes;
        }

        public static void ApagaPorId(int id)
        {
            using (var conn = new MySqlConnection(conexao))
            {
                conn.Open();
                var query = $"delete from clientes where id = {id};";

                var command = new MySqlCommand(query, conn);
                command.ExecuteNonQuery();
                conn.Clone();

            }
        }

        public static Cliente? BuscaPorId(int id)
        {
            var cliente = new Cliente();
            using (var conn = new MySqlConnection(conexao))
            {
                conn.Open();
                var query = $"""
                    select * from clientes where id = '{id}'
                """;

                var command = new MySqlCommand(query, conn);
                var dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    cliente = new Cliente
                    {
                        Id = Convert.ToInt32(dataReader["id"]),
                        Nome = dataReader["nome"].ToString(),
                        Email = dataReader["email"].ToString(),
                    };
                }

                conn.Close();
            }

            return cliente.Id == 0 ? null : cliente;
        }
    }
}