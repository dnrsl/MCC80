using API.Models;

namespace API.DTOs.Roles;

public class RoleDefaultDto
{
    public Guid Guid { get; set; }
    public string Name { get; set; }

    public static implicit operator Role(RoleDefaultDto roleDefaultDto)
    {
        return new Role
        {
            Guid = roleDefaultDto.Guid,
            Name = roleDefaultDto.Name,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now,
        };
    }

    public static explicit operator RoleDefaultDto(Role role)
    {
        return new RoleDefaultDto
        {
            Name = role.Name
        };
    }
}
