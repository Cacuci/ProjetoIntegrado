using System.ComponentModel.DataAnnotations;

namespace Configuration.Application.Queries.DTOs.Warehouse
{
    public class WarehouseRequestDTO
    {
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        [MaxLength(20, ErrorMessage = "Valor não deve ser maior que 20 caracteres")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        [MaxLength(60, ErrorMessage = "Valor não deve ser maior que 60 caracteres")]
        public string Name { get; set; }
    }
}
