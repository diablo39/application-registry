using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using ApplicationRegistry.Infrastructure.Abstractions.Scheduler;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using YamlDotNet.Serialization;

namespace ApplicationRegistry.Application.AsyncJobs
{
    public class ProcessSpecifiacationTextChangedJob
    {
        private readonly IUnitOfWork _context;

        private readonly ILogger<ProcessSpecifiacationTextChangedJob> _logger;

        private readonly IScheduler _scheduler;

        private readonly string[] _httpMethods = new[] {
            "get",
            "put",
            "post",
            "delete",
            "options",
            "head",
            "patch",
            "trace"
        };

        public ProcessSpecifiacationTextChangedJob(IUnitOfWork context, ILogger<ProcessSpecifiacationTextChangedJob> logger, IScheduler scheduler)
        {
            _context = context;
            _logger = logger;
            _scheduler = scheduler;
        }

        public void Handle(Guid id)
        {
            try
            {
                HandleImplementation(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while processing specification");
                
            }
        }

        private void HandleImplementation(Guid id)
        {
            var specification = _context.ApplicationVersionSwaggerSpecifications.FirstOrDefault(e => e.Id == id);

            if (specification != null)
            {
                var versionSpecification = _context.ApplicationVersionSwaggerSpecifications.FirstOrDefault(e => e.Id == id);

                var result = new List<SwaggerSpecificationOperationEntity>();

                var text = specification.Specification;

                var contextType = specification.ContentType;

                var isjson = contextType.EndsWith("json", StringComparison.InvariantCultureIgnoreCase);

                if (!isjson)
                    text = ConvertToJson(text);

                GetSwaggerOperations(specification, result, text);

                if (result.Any())
                {
                    RemoveAllOldOperations(specification);

                    _context.SwaggerSpecificationOperations.AddRange(result);

                    var stringified = string.Concat("|", string.Join("|", result.Select(e => e.Path).OrderBy(e => e).Distinct()), "|");

                    versionSpecification.OperationsStringified = stringified.CalculateSHA256();

                    _context.SaveChanges();

                    //var autorestApplicationDependencies = _context.ApplicationVersionDependencies.AsNoTracking()
                    //    .Where(d => d.Dependency.IdDependencyType == DependencyTypes.AutorestClient)
                    //    .Where(e => !_context.ApplicationVersionDependencies.Any(a => a.IdParent == e.Id))
                    //    .Select(e => e.Id);

                    //foreach (var autorestApplicationDependencyId in autorestApplicationDependencies)
                    //{
                    //    _scheduler.Enqueue<ProcessAutoRestClientDependencyCreatedJob>(e => e.Handle(autorestApplicationDependencyId));
                    //}
                }
            }
        }

        private void RemoveAllOldOperations(ApplicationVersionSwaggerSpecificationEntity specification)
        {
            var itemsToRemove = _context.SwaggerSpecificationOperations.Where(e => e.IdApplicationVersionSpecification == specification.Id).ToList();

            _context.SwaggerSpecificationOperations.RemoveRange(itemsToRemove);
        }

        private void GetSwaggerOperations(ApplicationVersionSwaggerSpecificationEntity specification, List<SwaggerSpecificationOperationEntity> result, string text)
        {
            var swagger = JObject.Parse(text);

            var paths = swagger["paths"];

            foreach (JProperty path in paths)
            {
                foreach (JProperty item in path.Value)
                {
                    var propertyName = item.Name;

                    if (_httpMethods.Any(e => e == propertyName))
                    {
                        var operationId = item.Value["operationId"];

                        var resultItem = new SwaggerSpecificationOperationEntity
                        {
                            IdApplicationVersionSpecification = specification.Id,
                            Path = path.Name,
                            HttpMethod = propertyName,
                            OperationId = operationId?.ToString()
                        };

                        result.Add(resultItem);
                    }

                }
            }
        }

        public string ConvertToJson(string specification)
        {
            try
            {
                var deserializer = new DeserializerBuilder().Build();

                var obj = deserializer.Deserialize(new StringReader(specification));

                var serializer = new SerializerBuilder().JsonCompatible().Build();

                var json = serializer.Serialize(obj);

                return json;
            }
            catch (Exception ex)
            {

                throw new Exception("Error while parsing YAML. Check InnerException for details", ex);
            }
        }
    }
}
