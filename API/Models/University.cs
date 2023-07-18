using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace API.Models;

public class University
{
    public Guid Guid { get; set; }
    public string code { get; set; }
    public string Name { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set;}
}
