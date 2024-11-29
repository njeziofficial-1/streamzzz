using Microsoft.AspNetCore.Http;
namespace StremoCloud.Shared.Response;

public class OtpResponse
{
    public IFormFile? Image {  get; set; }
    public string FirstName {  get; set; }
    public string LastName {  get; set; }
    public string PhoneNumber {  get; set; }
    public DateTime CreatedOn {  get; set; }
}
