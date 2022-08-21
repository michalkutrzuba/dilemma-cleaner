using DilemmaCleaner.Api.Web.Concepts.Configuration;
using DilemmaCleaner.Api.Web.Concepts.Configuration.Models;
using Microsoft.AspNetCore.Mvc;

namespace DilemmaCleaner.Api.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class ConfigController : ControllerBase
{
    private readonly IConfigurationService _configurationService;

    public ConfigController(IConfigurationService configurationService)
    {
        _configurationService = configurationService;
    }

    [HttpGet]
    public async Task<ConfigurationModel> Get()
    {
        return await _configurationService.Get();
    }
}
