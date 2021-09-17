using Newtonsoft.Json;

namespace ApplicationRegistry.Tools.Graph
{
    public class Node
    {
        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("project", NullValueHandling = NullValueHandling.Ignore)]
        public string Project { get; set; }

        [JsonProperty("group", NullValueHandling = NullValueHandling.Ignore)]
        public string Group { get; set; }
        
        public bool IsVisited { get; set; }

        public Node(string id)
        {
            Id = id;
        }
    }
}
