using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TranslationManagement.Api.Controllers;
using TranslationManagement.Data.Configuration;
using TranslationManagement.Services.Contracts;
using TranslationManagement.Services.DTO;

namespace TranslationManagement.Api.Controlers
{
    [ApiController]
    [Route("api/translators")]

    public class TranslatorManagementController : BaseController
    {
        private readonly ITranslatorManagementService _translationManagementService;
        private readonly ILogger<TranslatorManagementController> _logger;

        public TranslatorManagementController(ILogger<TranslatorManagementController> logger, ITranslatorManagementService translationManagementService)
        {
            _logger = logger;
            _translationManagementService = translationManagementService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TranslatorDto>>> GetTranslators()
        {
            return Ok(await _translationManagementService.GetTranslators());
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<List<TranslatorDto>>> GetTranslatorsByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest(Constants.translatorNameErrorMessage);
            }
            return Ok(await _translationManagementService.GetTranslatorsByName(name));
        }

        [HttpPost]
        public async Task<ActionResult> AddTranslator([FromBody] CreateTranslatorRequestDto translator)
        {
            if (!await _translationManagementService.AddTranslator(translator))
            {
                return BadRequest(Constants.addTranslatorErrorMessage);
            }
 
            return Ok(translator);
        }

        [HttpPatch("{translatorId}/status")]
        public async Task<ActionResult> UpdateTranslatorStatus(int translatorId, int newStatus)
        {
            _logger.LogInformation("User status update request: " + newStatus + " for user " + translatorId.ToString());
            await _translationManagementService.UpdateTranslatorStatus(translatorId, newStatus);

            return Ok(Constants.translatorUpdatedStatusMessage);
        }
    }
}