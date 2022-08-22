using DilemmaCleaner.Api.Web.Concepts.Dilemmas;
using DilemmaCleaner.Api.Web.Concepts.Dilemmas.Models;
using Microsoft.AspNetCore.Mvc;

namespace DilemmaCleaner.Api.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class DilemmasController : ControllerBase
{
    private readonly IDilemmasService _dilemmasService;

    public DilemmasController(IDilemmasService dilemmasService)
    {
        _dilemmasService = dilemmasService;
    }

    [HttpGet]
    public async Task<DilemmasListModel> GetDilemmas()
    {
        return await _dilemmasService.GetList();
    }

    [HttpGet("{uid}")]
    public async Task<DilemmaModel> GetDilemmas(string uid)
    {
        return await _dilemmasService.Get(uid);
    }
}
