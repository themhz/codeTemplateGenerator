using FSH.Starter.Application.Catalog.Bananas;

namespace FSH.Starter.Host.Controllers.Catalog;

public class BananaController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Bananas)]
    [OpenApiOperation("Search brands using available filters.", "")]
    public Task<PaginationResponse<BananaDto>> SearchAsync(SearchBananasRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Bananas)]
    [OpenApiOperation("Get Banana details.", "")]
    public Task<BananaDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetBananaRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Bananas)]
    [OpenApiOperation("Create a new Banana.", "")]
    public Task<Guid> CreateAsync(CreateBananaRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Brands)]
    [OpenApiOperation("Update a Banana.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateBananaRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Bananas)]
    [OpenApiOperation("Delete a Banana.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteBananaRequest(id));
    }

    [HttpPost("generate-random")]
    [MustHavePermission(FSHAction.Generate, FSHResource.Bananas)]
    [OpenApiOperation("Generate a number of random Bananas.", "")]
    public Task<string> GenerateRandomAsync(GenerateRandomBananaRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpDelete("delete-random")]
    [MustHavePermission(FSHAction.Clean, FSHResource.Bananas)]
    [OpenApiOperation("Delete the Bananas generated with the generate-random call.", "")]
    [ApiConventionMethod(typeof(FSHApiConventions), nameof(FSHApiConventions.Search))]
    public Task<string> DeleteRandomAsync()
    {
        return Mediator.Send(new DeleteRandomBananaRequest());
    }
}
