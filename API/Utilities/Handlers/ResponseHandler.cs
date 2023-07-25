namespace API.Utilities.Handlers;

public class ResponseHandler<TEtity>
{
    public int Code { get; set; }
    public string Status { get; set; }
    public string Message { get; set; }
    public TEtity? Data { get; set; }
}
