using DilemmaCleaner.Api.Web.Concepts.Dilemmas.Models;

namespace DilemmaCleaner.Api.Tests.Concepts.Dilemmas.DilemmasServiceSpec.Get;

// todo: add tests for GetDilemma

public abstract class GetContext : DilemmasServiceContext
{
    protected DilemmaModel Result = null!;
}
