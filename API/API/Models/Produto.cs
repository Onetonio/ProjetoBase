using System.ComponentModel.DataAnnotations;

namespace API.Models;
public class Produto
{
    public Produto()
    {
        CriadoEm = DateTime.Now;
        Id = Guid.NewGuid().ToString();
    }

    public Produto(string nome, string descricao, double preco)
    {
        Nome = nome;
        Descricao = descricao;
        Preco = preco;
        CriadoEm = DateTime.Now;
        Id = Guid.NewGuid().ToString();
    }

    //Atributo ou propriedade - nome e descricao
    public string? Id { get; set; }
    
    //Data Annotations em C#

    [Required(ErrorMessage = "Este campo é obrigatório!")]
    public string? Nome { get; set; }

    [MinLength(3, ErrorMessage = "Este campo deve ter no mínimo 3 caracteres!")]
    [MaxLength(50, ErrorMessage = "Este campo deve ter no máximo 50 caracteres!")]
    public string? Descricao { get; set; }

    [Range(1, 1000, ErrorMessage = "O preço deve estar entre R$ 1,00 e R$ 1.000,00!")]
    public double Preco { get; set; }
    public int Quantidade { get; set; }
    public DateTime CriadoEm { get; set; }

}
