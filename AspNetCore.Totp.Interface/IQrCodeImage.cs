namespace AspNetCore.Totp.Interface
{
    public interface IQrCodeImage
    {
        string DataUri { get; }
        byte[] Bytes { get; }
    }
}