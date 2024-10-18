namespace Choix_des_technos_et_infras_de_développement___TP1.Domain
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int ProfileId { get; set; }
        public ProfileEntity Profile { get; set; }
    }
}
