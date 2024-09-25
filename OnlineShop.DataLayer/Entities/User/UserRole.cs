namespace OnlineShop.DataLayer.Entities.User;


public class UserRole
{
    public int UrId { get; set; }
    public int? UserId { get; set; }
    public int RoleId { get; set; }

    #region Relations

    public User? User { get; set; }
    public Role? Role { get; set; }

    #endregion
}