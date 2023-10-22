namespace AvatarCollectionLibrary
{
    public class User
    {
        public int UserID { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? AuthenticationId { get; set; }
        public bool IsAdmin { get; set; }

        // een op veel
        public ICollection<MyCollection>? MyCollections { get; set; }


    }
}
