using System.ComponentModel.DataAnnotations;

namespace Configuration.Application.Queries.DTOs.Warehouse
{
    public class WarehouseResponseDTO
    {
        public Guid Id { get; set; }

        [MaxLength(256, ErrorMessage = "Valor não deve ser maior que 256 caracteres")]
        public string Code { get; set; }

        [MaxLength(20, ErrorMessage = "Valor não deve ser maior que 20 caracteres")]
        public string Name { get; set; }

        public WarehouseResponseDTO(Guid id, string code, string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }
    }
}
