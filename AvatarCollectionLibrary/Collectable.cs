using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AvatarCollectionLibrary
{
    public class Collectable
    {
        public int CollectableID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        [Column(TypeName = "DECIMAL(10,2)")]
        public decimal? Worth { get; set; }
        [Column(TypeName = "DECIMAL(10,2)")]
        public decimal? Price { get; set; }
        
        public DateOnly? Releasedate { get; set; }
        public string? Category { get; set; }
        public string? OperatingSystem { get; set;}
        public string? Platform { get; set; }
        public string? ComicEdition { get; set; }
        public string? Comic { get; set; }
        public string? Novel { get; set;}
        public string? BlueRay { get; set; }
        public string? DVD { get; set; }
        public string? PVC { get; set; }
        public string? FunkoPop { get; set; }

        // veel op veel
        public ICollection<MyCollection>? MyCollections { get; set; }
        //een op veel
        public Catalogue? Catalogue { get; set; }
        public int Id { get; set; }
    }
}
