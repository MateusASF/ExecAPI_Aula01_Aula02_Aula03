using Cliente.Core.Model;

namespace Cliente.Core.Interface
{
    public interface IClienteService
    {
        public List<Clientes> GetClientesService();
        public Clientes GetClienteNome(string nome);
        public Clientes GetClienteCpf(string cpf);
        public bool PostCliente(Clientes cliente);
        public bool PutCliente(Clientes cliente);
        public bool DeleteCliente(string cpf);
        public bool DeleteClienteId(long id);
    }
}
