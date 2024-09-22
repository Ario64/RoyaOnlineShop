namespace OnlineShop.Core.DTOs.User;

public class UsersForAdminPanelViewModel
{
    public List<DataLayer.Entities.User.User>? Users { get; set; }
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
}