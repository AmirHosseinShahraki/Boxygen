using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.API.Controllers;

[ApiController]
[Route("[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    protected readonly IMapper Mapper;

    protected ApiControllerBase(IMapper mapper)
    {
        Mapper = mapper;
    }
}