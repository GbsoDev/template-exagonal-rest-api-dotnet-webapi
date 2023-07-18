using FluentValidation;
using GbsoDevExagonalTemplate.Domain.Entities;
using GbsoDevExagonalTemplate.Domain.Interfaces.Ports.Inputs;
using GbsoDevExagonalTemplate.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GbsoDevExagonalTemplate.Api.Controllers
{
	[ApiController]
	[Route("api/[Controller]")]
	public class UserController : BaseController
	{
		private IUserInputPort UserUseCase => _userInputPort.Value;
		private readonly Lazy<IUserInputPort> _userInputPort;

		public UserController(Lazy<IUserInputPort> userInputPort, IServiceProvider serviceProvider) : base(serviceProvider)
		{
			this._userInputPort = userInputPort;
		}

		[HttpGet]
		[ProducesResponseType(typeof(UserDto[]), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult Get()
		{
			try
			{
				var results = Mapper.Map<UserDto[]>(UserUseCase.Get());
				return new OkObjectResult(results);
			}
			catch (Exception)
			{
				return new StatusCodeResult(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpGet("{id}")]
		[ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult Details(int id)
		{
			try
			{
				var result = Mapper.Map<UserDto>(UserUseCase.GetById(id));
				return result != null ? new OkObjectResult(result) : new BadRequestResult();
			}
			catch (Exception)
			{
				return new StatusCodeResult(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpPost]
		[ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(StatusCodes.Status406NotAcceptable)]
		public IActionResult Post([FromBody] UserDto entityDto)
		{
			try
			{
				var entity = Mapper.Map<User>(entityDto);
				var result = Mapper.Map<UserDto>(UserUseCase.Create(entity));
				return result != null ? new OkObjectResult(result) : new BadRequestResult();
			}
			catch (ValidationException ex)
			{
				var rr = new ObjectResult(ex.Errors.Select(x => x.ErrorMessage));
				rr.StatusCode = StatusCodes.Status406NotAcceptable;
				return rr;
			}
			catch (Exception)
			{
				return new StatusCodeResult(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpPut]
		[ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status406NotAcceptable)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult Put([FromBody] UserDto entityDto)
		{
			try
			{
				var entity = Mapper.Map<User>(entityDto);
				var result = Mapper.Map<UserDto>(UserUseCase.Update(entity));
				return new OkObjectResult(result);
			}
			catch (ValidationException ex)
			{
				return new StatusCodeResult(StatusCodes.Status406NotAcceptable);
			}
			catch (Exception)
			{
				return new StatusCodeResult(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult Delete(int id)
		{
			try
			{
				UserUseCase.Desable(id);
				return new OkResult();
			}
			catch (Exception)
			{
				return new StatusCodeResult(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpPost(nameof(ValidateUser))]
		[ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(StatusCodes.Status406NotAcceptable)]
		public IActionResult ValidateUser([FromBody] UserDto entityDto)
		{
			try
			{
				var entity = Mapper.Map<User>(entityDto);
				var result = UserUseCase.ValidateUser(entity);
				return new OkObjectResult(result);
			}
			catch (ValidationException ex)
			{
				var rr = new ObjectResult(ex.Errors.Select(x => x.ErrorMessage));
				rr.StatusCode = StatusCodes.Status406NotAcceptable;
				return rr;
			}
			catch (Exception)
			{
				return new StatusCodeResult(StatusCodes.Status500InternalServerError);
			}
		}
	}
}
