namespace AvatarCollectionLibrary
{
    public class MyCollection
    {
        public int MyCollectionID { get; set; }
        

        // Veel op veel relatie
        public ICollection<Collectable>? Collectables { get; set; }

        // een op veel
        public User? Users { get; set; }

    }
}
