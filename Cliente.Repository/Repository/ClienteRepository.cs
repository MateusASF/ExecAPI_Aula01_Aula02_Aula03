using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using Cliente.Core.Model;
using Cliente.Core.Interface;

namespace ApiAula01.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IConfiguration _configuration;

        public ClienteRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region GET'S
        public List<Clientes> GetClientesRepository() //GET
        {
            var query = "SELECT * FROM clientes";
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return conn.Query<Clientes>(query).ToList();
        }

        public Clientes GetClienteNome(string nome) //GET por nome
        {
            var query = "SELECT * FROM clientes WHERE clientes.nome = @nome";
            DynamicParameters parameters = new(new { nome });
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return conn.QueryFirstOrDefault<Clientes>(query, parameters);
        }

        public Clientes GetClienteCpf(string cpf) //GET por cpf 
        {
            var query = "SELECT * FROM clientes WHERE clientes.cpf = @cpf";
            DynamicParameters parameters = new(new { cpf });
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var teste = conn.QueryFirstOrDefault<Clientes>(query, parameters);
            return teste;
        }
        #endregion

        #region POST / INSERT
        public bool PostCliente(Clientes cliente)
        {
            var query = "INSERT INTO clientes VALUES (@cpf, @nome, @dataNascimento, @idade)";
            DynamicParameters parameters = new(new { cliente.Cpf, cliente.Idade, cliente.Nome, cliente.DataNascimento });
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return conn.Execute(query, parameters) == 1;
        }
        #endregion

        #region PUT / UPDATE
        public bool PutCliente(string cpf,  Clientes cliente)
        {
            var query = @"UPDATE clientes SET nome = @nome,
                          dataNascimento = @dataNascimento
                          WHERE clientes.cpf = @cpf";
            cliente.Cpf = cpf;
            DynamicParameters parameters = new(cliente);
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return conn.Execute(query, parameters) == 1;
        }
        #endregion

        #region DELETE
        public bool DeleteCliente(string cpf)
        {
            var query = "DELETE FROM clientes WHERE cpf = @cpf ";
            DynamicParameters parameters = new(new { cpf });
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return conn.Execute(query, parameters) == 1;
        }

        public bool DeleteClienteId(long id)
        {
            var query = "DELETE FROM clientes WHERE id = @id ";
            DynamicParameters parameters = new(new { id });
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return conn.Execute(query, parameters) == 1;
        }
        #endregion
    }
}
