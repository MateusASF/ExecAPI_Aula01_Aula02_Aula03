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

        //private static readonly string[] CPFs = new[]
        //{
        //"132.456.789-85", "784.231.156-78", "457.546.741-45", "515.443.467-60", "183.364.531-64",
        //    "001.861.937-18", "486.854.367-96", "775.456.346-08", "050.672.598-76", "672.592.447-63"
        //};


        //private static readonly string[] dates = new[]
        //{
        //        "12/04/2003", "20/05/2000", "12/04/2006", "25/09/1990", "01/12/2007",
        //             "13/04/1987", "30/01/2001", "27/02/1999", "14/06/1972", "17/10/1950"
        //};

        public List<Cadastro> pessoas { get; set; }

        public ValuesController()
        {
            pessoas = Enumerable.Range(0, 5).Select(index => new Cadastro
            {
                DataNascimento = DataRamdom(),//Convert.ToDateTime(dates[index + aoooba]),
                Nome = Nomes[Random.Shared.Next(Nomes.Length)],
                //Idade = CalcularIdade(index),
                Cpf = CPFRandom(), //CPFs[Random.Shared.Next(CPFs.Length)]
            }).ToList();
        }

        //private int? CalcularIdade(int index)
        //{
        //    var agora = Convert.ToInt32(DateTime.Now.Year);
        //    var nasc = Convert.ToInt32(DateTime.Parse(dates[index + aoooba]).Year);
        //    return agora - nasc;
        //}

        private DateTime DataRamdom()
        {
            Random gen = new Random();
            DateTime start = new DateTime(1900, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }

        private string CPFRandom()
        {
            var random = new Random();

            int soma = 0;
            int resto = 0;
            int[] multiplicadores = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string semente;

            do
            {
                semente = random.Next(1, 999999999).ToString().PadLeft(9, '0');
            }
            while (
                semente == "000000000"
                || semente == "111111111"
                || semente == "222222222"
                || semente == "333333333"
                || semente == "444444444"
                || semente == "555555555"
                || semente == "666666666"
                || semente == "777777777"
                || semente == "888888888"
                || semente == "999999999"
            );

            for (int i = 1; i < multiplicadores.Count(); i++)
                soma += int.Parse(semente[i - 1].ToString()) * multiplicadores[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            semente += resto;
            soma = 0;

            for (int i = 0; i < multiplicadores.Count(); i++)
                soma += int.Parse(semente[i].ToString()) * multiplicadores[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            semente = semente + resto;

            return semente;
        }

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
