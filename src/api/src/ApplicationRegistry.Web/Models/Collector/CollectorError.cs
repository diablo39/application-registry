using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationRegistry.Web.Areas.Api.Models.Collector
{
    public class CollectorError
    {
        [Required]
        public string ApplicationCode { get; set; }

        [Required]
        public string IdEnvironment { get; set; }

        [Required]
        public string Version { get; set; }


        public string IdCommit { get; set; }

        public string ErrorMessage { get; set; }
    }
}
