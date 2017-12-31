using Microsoft.AspNetCore.Mvc;

namespace QuizService
{
    public class JsonSerializationConfiguration
    {
        public static void Setup(MvcJsonOptions options)
        {
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }
    }
}
