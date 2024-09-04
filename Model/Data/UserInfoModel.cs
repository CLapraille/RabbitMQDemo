namespace Model.Data;

//public record UserInfoModel(
//    Guid Id,
//    string FirstName,
//    string LastName,
//    string Email,
//    int Age,
//    string Address,    
//    CreditRating CreditRating);



public class UserInfoModel
{
    public UserInfoModel()
    {

    }

    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public int Age { get; set; }
    public string Address { get; set; } = default!;
    public CreditRating CreditRating { get; set; }
}
