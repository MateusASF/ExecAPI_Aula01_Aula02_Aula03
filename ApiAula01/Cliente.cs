using System.ComponentModel.DataAnnotations;

namespace ApiAula01
{
    public class Cliente
    {
        public long Id { get; set; }


        [Required(ErrorMessage = "Data é obrigatório")]
        public DateTime? DataNascimento { get; set; } 


        public string? Nome { get; set; }


        [Required(ErrorMessage = "CPF é obrigatório")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve conter 11 números, sem pontuação")]
        public string? Cpf { get; set; }


        [Range(18,int.MaxValue, ErrorMessage = "Você deve ter no minimo 18 anos.")]
        public int Idade => CalucularIdade(); 


        public int CalucularIdade()
        {
            int Idade = DateTime.Now.Year - ((DateTime)DataNascimento!).Year;
            if (DateTime.Now.DayOfYear < ((DateTime)DataNascimento).DayOfYear)
            {
                Idade--;
            }
            return Idade;
        }
    }
}

