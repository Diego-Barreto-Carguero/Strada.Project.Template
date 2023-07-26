// <copyright file="PersonController.cs" company="Strada">
// Copyright (c) Strada. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Mvc;
using Strada.Templates.WebApi.Configurations.Pagination;
using Strada.Templates.WebApi.V1.Models.Base;
using Strada.Templates.WebApi.V1.Models.Request;
using Strada.Templates.WebApi.V1.Models.Responde;
using Swashbuckle.AspNetCore.Annotations;

namespace Strada.Templates.WebApi.V1.Controllers;

[Route("api/v{version:apiVersion}/persons")]
public partial class PersonController : MainController
{
    public PersonController(ILogger<PersonController> logger)
        : base(logger)
    {
    }

    [HttpGet("{id:int}")]
    [SwaggerResponse(StatusCodes.Status200OK, "These request was successful.", type: typeof(DefaultResponse<PersonResponse>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The server cannot find the requested resource.")]
    [SwaggerOperation(Summary = "Get person by Id")]
    public IActionResult GetById([FromRoute(Name = "id")] int id) => Ok();

    [HttpGet("actives")]
    [SwaggerResponse(StatusCodes.Status200OK, "These request was successful.", type: typeof(CustomResponse<List<PersonResponse>>))]
    [SwaggerOperation(Summary = "Get person actives")]
    public IActionResult GetActives([FromQuery] PaginationParams paginationParams) => Ok();

    [HttpGet("pending-analyzis")]
    [SwaggerResponse(StatusCodes.Status200OK, "These request was successful.", type: typeof(CustomResponse<List<PersonResponse>>))]
    [SwaggerOperation(Summary = "Get person with pending analyzis")]
    public IActionResult GetPendingAnalyzis([FromQuery] PaginationParams pagination) => NotFound();

    [HttpGet("{id:int}/status-analyzis")]
    [SwaggerResponse(StatusCodes.Status200OK, "These request was successful.", type: typeof(CustomResponse<List<PersonResponse>>))]
    [SwaggerOperation(Summary = "Get Status of cadastral analysis")]
    public IActionResult GetSituationAnalyzis([FromRoute(Name = "id")] int id) => Ok();

    [HttpPost]
    [SwaggerResponse(StatusCodes.Status201Created, "Request was successful and a new resource was created as a result.", type: typeof(PersonResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The request could not be understood by the server due to incorrect syntax.", type: typeof(ErrorResponse))]
    [SwaggerOperation(Summary = "Create new Person")]
    public IActionResult Create([FromBody] PersonRequest sampleRequest) => Created(string.Empty, new PersonResponse());

    [HttpDelete("{id:int}")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "There is no content to submit for this request.")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The request could not be understood by the server due to incorrect syntax.", type: typeof(ErrorResponse))]
    [SwaggerOperation(Summary = "Delete Person")]
    public IActionResult Delete([FromRoute(Name = "id")] int id) => NoContent();

    [HttpPut]
    [SwaggerResponse(StatusCodes.Status204NoContent, "There is no content to submit for this request.")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The request could not be understood by the server due to incorrect syntax.", type: typeof(ErrorResponse))]
    [SwaggerOperation(Summary = "Full update Person")]
    public IActionResult PartialUpdate([FromBody] PersonRequest personRequest) => NoContent();

    [HttpPatch]
    [SwaggerResponse(StatusCodes.Status204NoContent, "There is no content to submit for this request.")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The request could not be understood by the server due to incorrect syntax.", type: typeof(ErrorResponse))]
    [SwaggerOperation(Summary = "Partial update Person")]
    public IActionResult FullUpdate([FromBody] PersonRequest personRequest) => NoContent();
}
