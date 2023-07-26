namespace API.Utilities.Handlers;

public class GenerateOtp
{
    public static string Otp()
    {
        Random random = new Random();
        string otpCode = random.Next(100000, 999999).ToString();
        return otpCode;
    }
}
