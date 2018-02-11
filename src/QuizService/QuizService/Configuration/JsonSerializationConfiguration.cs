using Microsoft.AspNetCore.Mvc;

namespace QuizService
{
    /// <summary>
    /// Provides methods for json serialization configuration.
    /// </summary>
    public class JsonSerializationConfiguration
    {
        /// <summary>
        /// Setups Json serialization.
        /// </summary>
        /// <param name="options">Json serialization options.</param>
        public static void Setup(MvcJsonOptions options)
        {
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }
    }
}
