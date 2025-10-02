using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

public static class FirebaseInitializer
{
    private static bool _initialized = false;
    private static readonly object _lock = new object();

    public static void Initialize(IConfiguration configuration)
    {
        if (_initialized) return;

        lock (_lock)
        {
            if (_initialized) return;

            string firebaseConfigPath = configuration["Firebase:ConfigPath"];
            
            string jsonContent = File.ReadAllText(firebaseConfigPath);
            JObject config = JObject.Parse(jsonContent);

            // Obter as configurações do app1
            var app1Config = config["arenax"];
            var app1Options = new AppOptions()
            {
                Credential = GoogleCredential.FromJson(app1Config.ToString())
            };
            FirebaseApp.Create(app1Options, "arenax");

            var app2Config = config["arenaxjogador"];
            var app2Options = new AppOptions()
            {
                Credential = GoogleCredential.FromJson(app2Config.ToString())
            };
            FirebaseApp.Create(app2Options, "arenaxjogador");
            _initialized = true;
        }
    }
}