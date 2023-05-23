using FSH.Starter.Application.Catalog.{entityName}s;

namespace FSH.Starter.Host.Controllers.Catalog;

public class {entityName}Controller : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.{entityName}s)]
    [OpenApiOperation("Search brands using available filters.", "")]
    public Task<PaginationResponse<{entityName}Dto>> SearchAsync(Search{entityName}sRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.{entityName}s)]
    [OpenApiOperation("Get {entityName} details.", "")]
    public Task<{entityName}Dto> GetAsync(Guid id)
    {
        return Mediator.Send(new Get{entityName}Request(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.{entityName}s)]
    [OpenApiOperation("Create a new {entityName}.", "")]
    public Task<Guid> CreateAsync(Create{entityName}Request request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Brands)]
    [OpenApiOperation("Update a {entityName}.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(Update{entityName}Request request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.{entityName}s)]
    [OpenApiOperation("Delete a {entityName}.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new Delete{entityName}Request(id));
    }

    [HttpPost("generate-random")]
    [MustHavePermission(FSHAction.Generate, FSHResource.{entityName}s)]
    [OpenApiOperation("Generate a number of random {entityName}s.", "")]
    public Task<string> GenerateRandomAsync(GenerateRandom{entityName}Request request)
    {
        return Mediator.Send(request);
    }

    [HttpDelete("delete-random")]
    [MustHavePermission(FSHAction.Clean, FSHResource.{entityName}s)]
    [OpenApiOperation("Delete the {entityName}s generated with the generate-random call.", "")]
    [ApiConventionMethod(typeof(FSHApiConventions), nameof(FSHApiConventions.Search))]
    public Task<string> DeleteRandomAsync()
    {
        return Mediator.Send(new DeleteRandom{entityName}Request());
    }
}
