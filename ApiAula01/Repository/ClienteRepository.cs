using Microsoft.Data.SqlClient;
using Dapper;

namespace ApiAula01.Repository
{
    public class ClienteRepository
    {
        private readonly IConfiguration _configuration;

        public ClienteRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region GET'S
        public List<Cliente> GetClientes() //GET
        {
            var query = "SELECT * FROM clientes";
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return conn.Query<Cliente>(query).ToList();
        }

        public Cliente GetClienteNome(string nome) //GET por nome
        {
            var query = "SELECT * FROM clientes WHERE clientes.nome = @nome";
            DynamicParameters parameters = new(new { nome });
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return conn.QueryFirstOrDefault<Cliente>(query, parameters);
        }

        public Cliente GetClienteCpf(string cpf) //GET por cpf 
        {
            var query = "SELECT * FROM clientes WHERE clientes.cpf = @cpf";
            DynamicParameters parameters = new(new { cpf });
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return conn.QueryFirstOrDefault<Cliente>(query, parameters);
        }
        #endregion

        #region POST / INSERT
        public bool PostCliente(Cliente cliente)
        {
            var query = "INSERT INTO clientes VALUES (@cpf, @nome, @dataNascimento, @idade)";
            DynamicParameters parameters = new(new { cliente.Cpf, cliente.Idade, cliente.Nome, cliente.DataNascimento });
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return conn.Execute(query, parameters) == 1;
        }
        #endregion

        #region PUT / UPDATE
        public bool PutCliente(string cpf, Cliente cliente)
        {
            var query = @"UPDATE clientes SET nome = @nome,
                          dataNascimento = @dataNascimento
                          WHERE clientes.cpf = @cpf";
            cliente.Cpf = cpf;
            DynamicParameters parameters = new (cliente);
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
        #endregion
    }
}
