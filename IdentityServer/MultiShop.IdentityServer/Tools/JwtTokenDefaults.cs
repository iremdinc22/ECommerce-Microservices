namespace MultiShop.IdentityServer.Tools
{
    public static class JwtTokenDefaults
    {
        public const string ValidAudience = "http://localhost";
        public const string ValidIssuer = "http://localhost";
        
        // .NET 9+ projelerinde anahtarın yeterince uzun olması (en az 32 karakter) güvenlik için kritiktir.
        public const string Key = "MultiShop0102030405TokenKeySecurity2026_*/+-";
        
        // Dakika cinsinden geçerlilik süresi
        public const int Expire = 60; 
    }
}