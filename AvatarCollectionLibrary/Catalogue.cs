namespace AvatarCollectionLibrary
{
    public class Catalogue
    {
        public int? CatalogueID { get; set; }

        //een op veel
        public ICollection<Collectable>? Collectables { get; set; }
    }
}
