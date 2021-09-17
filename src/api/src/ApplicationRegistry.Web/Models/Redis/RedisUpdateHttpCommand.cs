using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace ApplicationRegistry.Web.Models.Redis
{
    public class RedisUpdateHttpCommand : Application.Commands.Redis.RedisUpdateCommand
    {
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        [BindNever]
        new public Guid Id { get; set; }
    }
}
