namespace ApiAula01
{
    public class Cadastro
    {
        public DateTime DataNascimento
        {
            get;
            set;
        }

        public string? Nome { get; set; }
        public string? Cpf
        {
            get;
            set;
        }
        public int? Idade
        {
            get
            {
                int Idade = DateTime.Now.Year - DataNascimento.Year;
                if (DateTime.Now.DayOfYear < DataNascimento.DayOfYear)
                {
                    Idade--;
                }
                return Idade;
            }
            set { }
        }
    }
}

