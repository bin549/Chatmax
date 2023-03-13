using System;
using UnityEngine;
using Newtonsoft.Json;

[Serializable]
public class ChatGPTChatMessage
{
    [field: SerializeField]
    [JsonProperty(PropertyName = "role")]
    public string Role { get; set; }

    [field: SerializeField]
    [JsonProperty(PropertyName = "content")]
    public string Content { get; set; }
}