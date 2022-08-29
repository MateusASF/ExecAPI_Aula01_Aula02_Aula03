using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiAula01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        static Random random = new Random();
        int aoooba = random.Next(0, 4);

        private static readonly string[] Nomes = new[]
        {
        "Mario", "João", "Julia", "Cristina", "Maria", "Kleber", "Paulo", "Amanda", "Sabrina", "Daniel"
        };

        private static readonly string[] CPFs = new[]
        {
        "132.456.789-85", "784.231.156-78", "457.546.741-45", "515.443.467-60", "183.364.531-64",
            "001.861.937-18", "486.854.367-96", "775.456.346-08", "050.672.598-76", "672.592.447-63"
        };


        private static readonly string[] dates = new[]
        {
                "12/04/2003", "20/05/2000", "12/04/2006", "25/09/1990", "01/12/2007",
                     "13/04/1987", "30/01/2001", "27/02/1999", "14/06/1972", "17/10/1950"
        };

        public List<Cadastro> pessoas { get; set; }

        public ValuesController()
        {
            pessoas = Enumerable.Range(0, 5).Select(index => new Cadastro
            {
                DataNascimento = Convert.ToDateTime(dates[index+aoooba]),
                Nome = Nomes[Random.Shared.Next(Nomes.Length)],
                //Idade = CalcularIdade(index),
                Cpf = CPFs[Random.Shared.Next(CPFs.Length)]
            }).ToList();
        }

        //private int? CalcularIdade(int index)
        //{
        //    var agora = Convert.ToInt32(DateTime.Now.Year);
        //    var nasc = Convert.ToInt32(DateTime.Parse(dates[index + aoooba]).Year);
        //    return agora - nasc;
        //}

        [HttpGet]
        public List<Cadastro> Get()
        {
            return pessoas;
        }

        [HttpPost]
        public Cadastro Post(Cadastro pessoa)
        {
            pessoas.Add(pessoa);
            return pessoa;
        }

        [HttpPut]
        public List<Cadastro> Put(int index, Cadastro pessoa)
        {
            pessoas[index] = pessoa;
            return pessoas;
        }

        [HttpDelete]
        public List<Cadastro> Deletar(int index)
        {
            pessoas.RemoveAt(index);
            return pessoas.ToList();
        }

    }
}
