using ApiDesafioBackend.Payloads;
using DesafioBackend.Commands;
using DesafioBackend.DataCollection;
using DesafioBackend.Indicators;
using DesafioBackend.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;


namespace ApiDesafioBackend.Controllers
{
    [Route("api/indicators")]
    [ApiController]
    public class IndicatorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IndicatorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Get Indicator list" })]
        public async Task<List<IndicatorViewModel>> GetIndicatorList()
        {
            return await _mediator.Send(new GetIndicatorListQuery());
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Tags = new[] { "Get Indicator by Id" })]
        public async Task<IndicatorViewModel> GetIndicatorById(Guid id)
        {
            return await _mediator.Send(new GetIndicatorByIdQuery(id));
        }

        [HttpGet("{id}/indicator-result")]
        [SwaggerOperation(Tags = new[] { "Get Indicator Result" })]
        public async Task<double> GetIndicatorResult(Guid id)
        {
            return await _mediator.Send(new GetIndicatorResultQuery(id));
        }

        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Create new Indicator" })]
        public async Task PostNewIndicator([FromBody] AddIndicatorPayload value)
        {
            await _mediator.Send(new AddIndicatorCommand(value.Name, value.ResultType));
        }

        [HttpPost("{id}/data-collection-point")]
        [SwaggerOperation(Tags = new[] { "Add New Data Collection Point" })]
        public async Task PostNewDataCollectionPoint(Guid id, [FromBody] AddDataCollectionPointPayload value)
        {
            await _mediator.Send(new AddDataCollectionPointCommand(id, value.Date, value.Value));
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Tags = new[] { "Edit Indicator" })]
        public async Task PutIndicator(Guid id, [FromBody] EditIndicatorPayload value)
        {
            await _mediator.Send(new EditIndicatorCommand(Id: id, Name: value.Name, ResultType: value.ResultType));
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Tags = new[] { "Delete Indicator" })]
        public async Task DeleteIndicator(Guid id)
        {
            await _mediator.Send(new DeleteIndicatorCommand(id));
        }

        [HttpDelete("{id}/data-collection-point/{date}")]
        [SwaggerOperation(Tags = new[] { "Delete Data Collection Point" })]
        public async Task DeleteDataCollectionPoint(Guid id, DateTime date)
        {
            await _mediator.Send(new DeleteDataCollectionPointCommand(id, date));
        }
    }
}
