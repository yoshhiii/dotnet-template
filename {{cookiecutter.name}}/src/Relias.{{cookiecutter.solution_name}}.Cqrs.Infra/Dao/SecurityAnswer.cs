using Newtonsoft.Json;

namespace Relias.{{cookiecutter.solution_name}}.Cqrs.Infra.Dao
{
    /// <summary>
    /// Security answer data access object
    /// </summary>
    public class SecurityAnswer
    {
        [JsonProperty("order")]
        public int Order { get; set; }

        [JsonProperty("question")]
        public string? Question { get; set; }

        [JsonProperty("answer")]
        public string? Answer { get; set; }
    }
}
