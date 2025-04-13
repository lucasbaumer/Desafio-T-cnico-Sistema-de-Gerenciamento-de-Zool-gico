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
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCareById(Guid id)
        {
            var care = await _careService.GetCareByIdAsync(id);
            if (care == null)
            {
                return NotFound("Não foi possível encontrar o cuidado!");
            }

            return Ok(care);
        }

        [HttpPost]
        public async Task<ActionResult> RegisterCare(CareCreateDto careCreateDto)
        {
            try
            {
                await _careService.AddCareAsync(careCreateDto);
                return Ok("Cuidado foi cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCare(Guid id, CareUpdateDto careUpdateDto)
        {
            try
            {
                var result = await _careService.UpdateCareAsync(id, careUpdateDto);
                return result ? Ok("Cuidado atualizado com sucesso!") : BadRequest("Erro ao atualizar cadastro!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCare(Guid id)
        {
            var result = await _careService.DeleteCareAsync(id);
            if (result)
            {
                return Ok("Cuidado foi deletado com sucesso!");
            }

            return BadRequest("Erro ao deletar Cuidado!");
        }
    }
}
