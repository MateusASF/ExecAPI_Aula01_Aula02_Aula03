using Cliente.Core.Interface;
using Cliente.Core.Model;

namespace Cliente.Core.Service
{
    public class ClienteService : IClienteService
    {
        public IClienteRepository _clieteRepository;

        public ClienteService(IClienteRepository clieteRepository)
        {
            _clieteRepository = clieteRepository;
        }

        public List<Clientes> GetClientesService()
        {
            return _clieteRepository.GetClientesRepository();
        }

        public Clientes GetClienteNome(string nome)
        {
            return _clieteRepository.GetClienteNome(nome);
        }
        public Clientes GetClienteCpf(string cpf)
        {
            return _clieteRepository.GetClienteCpf(cpf);
        }
        public bool PostCliente(string cpf, Clientes cliente)
        {
            return _clieteRepository.PostCliente(cpf, cliente);
        }
        public bool PutCliente(string cpf, Clientes cliente)
        {
            return _clieteRepository.PutCliente(cpf, cliente);
        }
        public bool DeleteCliente(string cpf)
        {
            return _clieteRepository.DeleteCliente(cpf);
        }

        public bool DeleteClienteId(long id)
        {
            return _clieteRepository.DeleteClienteId(id);
        }
    }
}
