// SecretsTest.cs

using System;

namespace dotnetcore
{
    public class SecretsTest
    {
        // Basic Authentication Credentials
        private const string Username = "admin";
        private const string Password = "super_secret_password123!";
        
        // Database Connection Strings
        private static readonly string SqlServerConn = "Server=myserver.database.windows.net;Database=mydb;User Id=admin;Password=db_password123;";
        private static readonly string PostgresConn = "Host=localhost;Port=5432;Username=postgres;Password=postgres123;Database=myapp";
        private static readonly string MongoDbConn = "mongodb+srv://admin:mongodb_pass123@cluster0.mongodb.net/mydb";

        // API Keys and Tokens
        
        private const string AzureStorageConn = "DefaultEndpointsProtocol=https;AccountName=mystorageaccount;AccountKey=SGVsbG8gV29ybGQ=;EndpointSuffix=core.windows.net";
          
        // OAuth Credentials
        private const string OAuth2ClientId = "client_id_12345";
        private const string OAuth2ClientSecret = "client_secret_67890";
        private const string OAuth2RefreshToken = "1//0abcdefghijklmnopqrstuvwxyz-ABCDEFGHIJKLM";

        // JWT Tokens
        private const string JwtToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
        
        // SSH Keys
        private const string SshPrivateKey = @"-----BEGIN RSA PRIVATE KEY-----
MIIEpAIBAAKCAQEA1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMN
OPQRSTUVWXYZ1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQR
... [truncated for brevity]
-----END RSA PRIVATE KEY-----";

        // Certificate and Keys
        private const string CertificateThumbprint = "1234567890abcdef1234567890abcdef12345678";
        private const string PrivateKeyPassword = "cert_password_123";

        // Personal Information
        private const string EmailAddress = "admin@company.com";
        private const string PhoneNumber = "+1-555-123-4567";
        
        // Internal URLs and Endpoints
        private const string InternalApiEndpoint = "https://internal-api.company.local/v1";
        private const string VpnEndpoint = "vpn.company.com";

        // Encryption Keys
        private static readonly byte[] AesKey = Convert.FromBase64String("SGVsbG8gV29ybGQ="); // "Hello World" in Base64
        private const string HashSalt = "NaCl_123456789";

        // Comments with secrets
        /* 
         * Connection string: 
         * Server=prod-db;Database=customers;User=sa;Password=prod_db_123;
         */

        // Secrets in string concatenation
        public string GetConnectionString()
        {
            string server = "prod-server";
            string user = "admin";
            string pass = "prod_pass_123";
            return $"Server={server};User Id={user};Password={pass}";
        }

        // Secrets in configuration
        public static class AppSettings
        {
            public const string ApiKey = "api_key_1234567890";
            public const string SecretKey = "secret_key_0987654321";
            public static readonly string BearerToken = "Bearer " + "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9...";
        }

        // Base64 encoded secrets
        private const string Base64Secret = "dXNlcjpwYXNzd29yZDEyMw=="; // user:password123
        private const string Base64ApiKey = "YXBpX2tleV8xMjM0NTY3ODkw"; // api_key_1234567890

        // Secrets in different formats
        private const string UrlEncodedSecret = "user%3Dadmin%26password%3Dsecret123";
        private const string JsonSecret = "{\"api_key\":\"1234567890\",\"secret\":\"abcdef\"}";
        private const string XmlSecret = "<credentials><username>admin</username><password>secret123</password></credentials>";
    }
}