using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_universities")]

public class University : BaseEntity
{
    [Column ("code", TypeName = "nvarchar(50)")]
    public string Code { get; set; }

    [Column("name", TypeName = "nvarchar(100)")] //nvarchar bisa menerima unique symbol
    public string Name { get; set; }

    //Cardinality (Satu University punya banyak Education)
    public ICollection<Education>? Educations { get; set; } 
}
