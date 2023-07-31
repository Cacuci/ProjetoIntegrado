namespace Configuration.API.DTOs
{
    public class UserResponseDTO
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public UserResponseDTO(string id, string name, string email)
        {
            ID = id;
            Name = name;
            Email = email;
        }
    }
}
