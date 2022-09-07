using Cliente.Core.Model;

namespace Cliente.Core.Interface
{
    public interface IClienteService
    {
        public List<Clientes> GetClientesService();
        public Clientes GetClienteNome(string nome);
        public Clientes GetClienteCpf(string cpf);
        public bool PostCliente(string cpf, Clientes cliente);
        public bool PutCliente(string cpf, Clientes cliente);
        public bool DeleteCliente(string cpf);
        public bool DeleteClienteId(long id);
    }
}
