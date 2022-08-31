using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiAula01.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Consumes("application/json")] // -> limita a entrada
    [Produces("application/json")] // -> limita a saida
    public class ValuesController : ControllerBase
    {
        public List<Cadastro> pessoas { get; set; }

        private static readonly string[] Nomes = new[]
        {
        "Mario", "João", "Julia", "Cristina", "Maria", "Kleber", "Paulo", "Amanda", "Sabrina", "Daniel"
        };

        public ValuesController()
        {
            pessoas = Enumerable.Range(1, 5).Select(index => new Cadastro
            {
                DataNascimento = DateRandom(),
                Nome = Nomes[Random.Shared.Next(Nomes.Length)],
                Cpf = CpfRandom(),
            }).ToList();
        }

        private DateTime DateRandom()
        {
            Random gen = new Random();
            DateTime start = new DateTime(1900, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }

        private string CpfRandom()
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

        //GET
        //[HttpGet("Get_Simples")]
        //public List<Cadastro> Get()
        //{
        //    return pessoas;
        //}

        [HttpGet("Get_Interface_IActionResult/{index}/consultar")]//*
        public IActionResult GetAction1() //aqui não estou tipando estou apenas usando a interface,  *QUANDO NÃO RETORNAR NADA PROCURAR USAR INTERFACE, QUANDO RETORNAR PROCUAR USAR TIPADO
        {
            return Ok(pessoas);
        }

        //[HttpGet("Get_Action_Result")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public ActionResult<List<Cadastro>> GetAction2(int index) //aqui eu tipo meu retonro dizendo q é um objeto, Dar preferência pra este modelo devido interface se mt genérica
        //{
        //    return Ok(pessoas);
        //}

        //*
        //[FromQuery] => parâmetros simples como int, string, etc; esse é o dafault do ApiController, logo não é necessário reforçar
        //[FromRote] -> [HttpPost("Post_Simples")] => aqui eu passo o nome "Post Simples" pela rota, para passsar valores no index colocamos a variável entre chaves ex: [HttpPost("Post_Simples/{index}")]
        //geralmente usa-se palavras simples e verbos para indicar o que está acontecendo
        //[FromQuery] -> quando não for rta ou query


        //POST
        //[HttpPost("Post_Simples")]
        //public Cadastro Post(Cadastro pessoa)
        //{
        //    pessoas.Add(pessoa);
        //    return pessoa;
        //}

        [HttpPost("Post_ActionResult/{index}/inserir")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Cadastro> PostAction([FromBody] Cadastro pessoa) 
        {
            //if (!ModelState.IsValid) //esse cara faz as validações do Cadastro na hora de dar o Post
            //{
            //    return BadRequest();
            //}
            pessoas.Add(pessoa);
            return CreatedAtAction(nameof(PostAction), pessoa);
            //return StatusCode(501, pessoa); => Status code retorna qualquer cód pode ser com objeto ou não
        }


        //PUT
        //[HttpPut("Put_simples")]
        //public List<Cadastro> Put(int index, Cadastro pessoa)
        //{
        //    pessoas[index] = pessoa;
        //    return pessoas;
        //}

        //[HttpPut("Post_Interface_IActionResult")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //public IActionResult PutAction(int index, Cadastro pessoa)
        //{
        //    pessoas[index] = pessoa;
        //    return NoContent();
        //}

        [HttpPut("Post_Interface_ActionResult_Melhorado/{index}/editar")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PutAction2(int index, Cadastro pessoa)
        {
            pessoas[index] = pessoa;
            return NoContent();
        }


        //DELETE
        //[HttpDelete("Delete_simples")]
        //public List<Cadastro> Deletar(int index)
        //{
        //    pessoas.RemoveAt(index);
        //    return pessoas.ToList();
        //}

        [HttpDelete("Delete_Interface_IActionResult/{index}/deletar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeletarAction(int index)
        {
            if(index > 4) // pouco provavél o uso
            {
                return NotFound();
            }
            //var teste = Request.Headers;
            pessoas.RemoveAt(index);
            return Ok();
        }

    }
}
