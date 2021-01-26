
using System.ComponentModel.DataAnnotations.Schema;

namespace TestRP.Model
{

[Table("Product")]
    public class Product {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    }

}