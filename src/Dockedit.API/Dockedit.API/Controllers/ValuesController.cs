using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.Extensions.Configuration;

namespace Dockedit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        public ValuesController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpGet("GetAllImages")]
        public async Task<List<string>> GetAllImages()
        {
            var endpoint = Configuration.GetValue<string>("docker_engine", null);
            // Default Docker Engine on Windows
            DockerClient client = new DockerClientConfiguration(
                new Uri(endpoint))
                 .CreateClient();
            IList<ImagesListResponse> containers = await client.Images.ListImagesAsync(
                new ImagesListParameters()
                {
                    All=true,
                });
            return containers.Select(i => i.ID.ToString()).ToList();
        }
    }
}
