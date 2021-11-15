using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApplicationRegistry.UnitTests
{

    public class OpenApiTests
    {
        [Fact]
        public void Test()
        {
            var httpClient = new HttpClient();

            var stream1 = File.OpenRead(@"D:\swaggers\1.yaml"); // await httpClient.GetStreamAsync("https://raw.githubusercontent.com/OAI/OpenAPI-Specification/master/examples/v3.0/petstore.yaml");
            var stream2 = File.OpenRead(@"D:\swaggers\1.yaml");  //await httpClient.GetStreamAsync("https://raw.githubusercontent.com/OAI/OpenAPI-Specification/master/examples/v3.0/petstore.yaml");



            var c = new SwaggerComparator(stream1, stream2);

            c.Compare();

            // Read V3 as YAML
            // var openApiDocument = new OpenApiStreamReader().Read(stream, out var diagnostic);

            Console.WriteLine();

            // 1 check paths
            // 2 compare operations
            // 3 compare headers
            // 4 compare parameters
            // 5 compare results
        }
    }

    public class SwaggerComparator
    {
        private readonly System.IO.Stream _source;
        private readonly System.IO.Stream _target;

        public SwaggerComparator(System.IO.Stream source, System.IO.Stream target)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (target is null)
            {
                throw new ArgumentNullException(nameof(target));
            }
            _source = source;
            _target = target;
        }

        public void Compare()
        {
            var targetNewPaths = new List<OpenApiPathItem>();

            var targetRemovedPaths = new List<OpenApiPathItem>();

            var paths = new List<PathToCompare>();

            var source = new OpenApiStreamReader().Read(_source, out var _);
            var target = new OpenApiStreamReader().Read(_target, out var _);

            var targetPathsDictionary = target.Paths.ToDictionary(e => e.Key, e => e.Value);

            var sourcePathsDoctionary = source.Paths.ToDictionary(e => e.Key, e => e.Value);

            foreach (var targetPath in target.Paths)
            {
                if (sourcePathsDoctionary.TryGetValue(targetPath.Key, out var sourceItem))
                {
                    paths.Add(new PathToCompare(sourceItem, targetPath.Value));
                    targetPathsDictionary.Remove(targetPath.Key);
                    sourcePathsDoctionary.Remove(targetPath.Key);
                }
                else
                {
                    targetNewPaths.Add(targetPath.Value);
                }
            }

            targetRemovedPaths.AddRange(sourcePathsDoctionary.Select(e => e.Value));


            foreach (var pathPair in paths)
            {
                DenormalizeParameters(pathPair.Source);
                DenormalizeParameters(pathPair.Target);
            }
        }

        private static void DenormalizeParameters(OpenApiPathItem path)
        {
            foreach (var parameter in path.Parameters)
            {
                foreach (var operation in path.Operations)
                {
                    if (operation.Value.Parameters.Any(e => e.Name == parameter.Name && e.In == parameter.In))
                    {
                        continue; // parameter overrided on operation level
                    }
                    else
                    {
                        operation.Value.Parameters.Add(parameter);
                    }
                }
            }
        }
    }

    public class PathToCompare
    {
        public OpenApiPathItem Source { get; }

        public OpenApiPathItem Target { get; }


        public PathToCompare(OpenApiPathItem source, OpenApiPathItem target)
        {
            Source = source;
            Target = target;
        }
    }

    public class SwaggerComparisonResult
    {
        public bool IsContractCompatible { get; set; }


    }

    public class PathDifference
    {

    }
}
