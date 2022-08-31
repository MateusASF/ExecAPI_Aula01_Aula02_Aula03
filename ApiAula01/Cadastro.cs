using System.ComponentModel.DataAnnotations;

namespace ApiAula01
{
    public class Cadastro
    {
        [Required(ErrorMessage = "Data é obrigatório")]
        public DateTime? DataNascimento { get; set; } 

        public string? Nome { get; set; }

        [Required] //obriga que o cpf seja obrigatório
        [MaxLength(11, ErrorMessage = "Cpf é só 11 caracteres locão")]
        public string? Cpf { get; set; }

        [Range(18, 60, ErrorMessage = "Só 18 fi")] //para delimitar
        public int? Idade => CalucularIdade(); 

        public int CalucularIdade()
        {
            int Idade = DateTime.Now.Year - ((DateTime)DataNascimento).Year;
            if (DateTime.Now.DayOfYear < ((DateTime)DataNascimento).DayOfYear)
            {
                Idade--;
            }
            return Idade;
        }


    }
}

