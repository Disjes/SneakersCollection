using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SneakersCollection.Data.Entities;
public class Sneaker
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid BrandId { get; set; }
    public decimal Price { get; set; }
    public decimal SizeUS { get; set; }
    public int Year { get; set; }

    public virtual Brand Brand { get; set; }
}
