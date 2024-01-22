using Microsoft.Azure.CosmosRepository;
using Microsoft.Azure.CosmosRepository.Attributes;
using Newtonsoft.Json;

namespace Relias.{{cookiecutter.solution_name}}.Cqrs.Infra.Dao
{
    /// <summary>
    /// User profile data access object
    /// </summary>
    [PartitionKeyPath("/userId")]
    public class UserProfile : Item
    {
        [JsonProperty("userId")]
        public string? UserId { get; set; }

        [JsonProperty("securityAnswers")]
        public List<SecurityAnswer> SecurityAnswers { get; set; } = new List<SecurityAnswer>();

        protected override string GetPartitionKeyValue() => UserId!;
    }
}
