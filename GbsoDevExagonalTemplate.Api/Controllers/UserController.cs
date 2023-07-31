using GbsoDevExagonalTemplate.Application.Interfaces;
using GbsoDevExagonalTemplate.Domain.Entities;
using GbsoDevExagonalTemplate.Dtos;
using Microsoft.AspNetCore.Mvc;
using static GbsoDevExagonalTemplate.Api.ExceptionMiddleware;

namespace GbsoDevExagonalTemplate.Api.Controllers
{
	[ApiController]
	[Route("api/[Controller]")]
	[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
	[ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
	public class UserController : BaseController
	{
		private IUserService UserService => _userService.Value;
		private readonly Lazy<IUserService> _userService;

		public UserController(Lazy<IUserService> userService, IServiceProvider serviceProvider) : base(serviceProvider)
		{
			this._userService = userService;
		}

		[HttpGet]
		[ProducesResponseType(typeof(UserDto[]), StatusCodes.Status200OK)]
		public async Task<IActionResult> Get()
		{
			var results = Mapper.Map<UserDto[]>(await UserService.GetAsync());
			return new OkObjectResult(results);
		}

		[HttpGet("{id}")]
		[ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Details(int id)
		{
			var result = Mapper.Map<UserDto>(await UserService.GetByIdAsync(id));
			return result != null ? new OkObjectResult(result) : new NotFoundResult();
		}

		[HttpPost]
		[ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
		public async Task<IActionResult> Post([FromBody] UserDto entityDto)
		{
			var entity = Mapper.Map<User>(entityDto);
			var result = Mapper.Map<UserDto>(await UserService.CreateAsync(entity));
			return result != null ? new OkObjectResult(result) : new BadRequestResult();
		}

		[HttpPut]
		[ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Put([FromBody] UserDto entityDto)
		{
			var entity = Mapper.Map<User>(entityDto);
			var result = Mapper.Map<UserDto>(await UserService.UpdateAsync(entity));
			return new OkObjectResult(result);
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Delete(int id)
		{
			await UserService.DesableAsync(id);
			return new OkResult();
		}

		[HttpPost(nameof(ValidateUser))]
		[ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> ValidateUser([FromBody] UserDto entityDto)
		{
			var entity = Mapper.Map<User>(entityDto);
			var result = await UserService.ValidateUserAsync(entity);
			return result ? new OkResult() : new UnauthorizedResult();
		}
	}
}
