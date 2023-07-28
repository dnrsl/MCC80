namespace API.Utilities.Handlers;

public class GenerateOtp
{
    public static int Otp()
    {
        Random random = new Random();
        int otpCode = random.Next(100000, 999999);
        return otpCode;
    }
}
