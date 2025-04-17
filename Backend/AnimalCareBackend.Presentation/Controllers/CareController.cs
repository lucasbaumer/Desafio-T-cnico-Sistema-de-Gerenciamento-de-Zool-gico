using AnimalCareBackend.Application.DTOS;
using AnimalCareBackend.Application.Interface;
using AnimalCareBackend.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AnimalCareBackend.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CareController : ControllerBase
    {
        private readonly ICareService _careService;

        public CareController(ICareService careService)
        {
            _careService = careService;
        }

        [HttpGet]
        public async Task<ActionResult<Care>> GetAllCares()
        {
            try
            {
                var cares = await _careService.GetAllCaresAsync();
                return Ok(cares);
            }
            catch(Exception ex)
            {
                return BadRequest(new { Message = $"Erro: {ex.Message}" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCareById(Guid id)
        {
            var care = await _careService.GetCareByIdAsync(id);
            if (care == null)
            {
                return NotFound(new { Message = "Não foi possível encontrar o cuidado!" });
            }

            return Ok(care);
        }

        [HttpPost]
        public async Task<ActionResult> RegisterCare(CareCreateDto careCreateDto)
        {
            try
            {
                await _careService.AddCareAsync(careCreateDto);
                return Ok(new { Message = "Cuidado foi cadastrado com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpGet("edit/{id}")]
        public async Task<ActionResult<CareUpdateDto>> getCareForEdit(Guid id)
        {
            try
            {
                var result = await _careService.GetCareForUpdateAsync(id);
               if(result == null)
                {
                    return NotFound(new { Message = "Cuidado não foi encontrado!" });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Erro ao cadastrar o cuidado: {ex.Message}" });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCare(Guid id, CareUpdateDto careUpdateDto)
        {
            try
            {
                var result = await _careService.UpdateCareAsync(id, careUpdateDto);
                if (!result)
                {
                    return NotFound(new { Message = "Cuidado não foi encontrado!" });
                }
                return Ok(new { Message = "Cuidado foi adicionado com sucesso!"});
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Erro ao cadastrar o cuidado: {ex.Message}" });
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCare(Guid id)
        {
            var result = await _careService.DeleteCareAsync(id);
            if (result)
            {
                return Ok(new { Message = "Cuidado foi excluido com sucesso!" });
            }

            return BadRequest(new { Message = "erro ao excluir cuidado!" });
        }
    }
}
