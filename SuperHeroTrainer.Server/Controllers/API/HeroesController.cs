using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SuperHeroTrainer.Models;
using SuperHeroTrainer.Shared.DTO;
using SuperHeroTrainer.Shared.Interfaces.Repository;
using SuperHeroTrainer.Shared.Services;

namespace SuperHeroTrainer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class HeroesController : ApiBaseController
    {
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;
        private IHeroesService _heroesService;
        private readonly ILogger<HomeController> _logger;
        private IUserService _userService;

        public HeroesController(IOptions<AppSettings> appSettings, IMapper mapper,
            IHeroesService heroesService, IUserService userService, ILogger<HomeController> logger)
        {
            _appSettings = appSettings.Value;
            _mapper = mapper;
            _heroesService = heroesService;
            _logger = logger;
            _userService = userService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetHeroes()
        {
            IList <HeroDTO> list = new List<HeroDTO>();

            try
            {
                if (!string.IsNullOrWhiteSpace(this.UserId))
                {
                    var user = await _userService.GetUserAsync(this.UserId);

                    if (user == null)
                        throw new UnauthorizedAccessException();

                    var heroes = await _heroesService.GetHeroesAsync(user);
                    list = _mapper.Map<IList<HeroDTO>>(heroes);
                    return Ok(list);
                }
                else
                {
                    throw new UnauthorizedAccessException();
                }

            }
            catch (UnauthorizedAccessException exp)
            {
                _logger.LogError(exp, exp.Message);
                return Unauthorized();
            }
            catch (Exception exp)
            {
                _logger.LogError(exp, exp.Message);
                return Ok(list);
            }
        }

        [HttpPost("train/{heroId}")]
        public async Task<IActionResult> TrainHeroes([FromRoute]string heroId)
        {
            IList<HeroDTO> list = new List<HeroDTO>();

            try
            {
                if (!string.IsNullOrWhiteSpace(this.UserId))
                {
                    var user = await _userService.GetUserAsync(this.UserId);

                    if (user == null)
                        throw new UnauthorizedAccessException();

                    Guid guidID;

                    if (!Guid.TryParse(heroId, out guidID)) {
                        throw new InvalidOperationException($"No hero with Id {heroId} found");
                    }

                    var hero = await _heroesService.GetHeroAsync(guidID);

                    if (hero == null)
                        throw new InvalidOperationException($"No hero with Id {heroId} found");

                    await _heroesService.TrainHeroAsync(hero);
                    var heroReplay = _mapper.Map<HeroDTO>(hero);
                    return Ok(heroReplay);
                }
                else
                {
                    throw new UnauthorizedAccessException();
                }

            }
            catch (UnauthorizedAccessException exp)
            {
                _logger.LogError(exp, exp.Message);
                return Unauthorized();
            }
            catch (Exception exp)
            {
                _logger.LogError(exp, exp.Message);
                return BadRequest(exp.Message);
            }
        }
    }
}