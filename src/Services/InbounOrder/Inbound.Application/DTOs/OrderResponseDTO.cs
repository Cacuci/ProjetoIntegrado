using System.ComponentModel.DataAnnotations;

namespace Inbound.Application.DTOs
{
    public class OrderResponseDTO
    {
        public OrderResponseDTO(Guid orderId,
                                string number,
                                string warehouseCode,
                                DateTime dateCreated,
                                IEnumerable<OrderDocumentResponseDTO> documents)
        {
            Id = orderId;
            Number = number;
            WarehouseCode = warehouseCode;
            DateCreated = dateCreated;
            Documents = documents;
        }

        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        [MaxLength(20, ErrorMessage = "Valor não deve ser maior que 20 caracteres")]
        public string Number { get; private set; }

        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        [MaxLength(7, ErrorMessage = "Valor não deve ser maior que 7 caracteres")]
        public string WarehouseCode { get; private set; }

        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public DateTime DateCreated { get; private set; }

        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public IEnumerable<OrderDocumentResponseDTO> Documents { get; set; }
    }
}
