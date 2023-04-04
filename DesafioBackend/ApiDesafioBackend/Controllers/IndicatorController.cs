﻿using ApiDesafioBackend.Payloads;
using DesafioBackend.Commands;
using DesafioBackend.DataCollection;
using DesafioBackend.Indicators;
using DesafioBackend.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiDesafioBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndicatorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IndicatorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Indicator list" })]
        public async Task<List<IndicatorViewModel>> Get()
        {
            return await _mediator.Send(new GetIndicatorListQuery());
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        [SwaggerOperation(Tags = new[] { "Indicator by Id" })]
        public async Task<IndicatorViewModel> Get(Guid id)
        {
            return await _mediator.Send(new GetIndicatorByIdQuery(id));
        }

        // POST api/<ValuesController>
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Add new Indicator" })]
        public async Task Post([FromBody] AddIndicatorPayload value)
        {
            await _mediator.Send(new AddIndicatorCommand(value.Name, value.ResultType));
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        [SwaggerOperation(Tags = new[] { "Add DataCollectionPoint" })]
        public async Task Put(Guid id, [FromBody] AddDataCollectionPointPayload value)
        {
            await _mediator.Send(new AddDataCollectionPointCommand(id, value.Date, value.Value));
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        [SwaggerOperation(Tags = new[] { "Delete Indicator" })]
        public async Task Delete(Guid id)
        {
            await _mediator.Send(new DeleteIndicatorCommand(id));
        }
    }
}
