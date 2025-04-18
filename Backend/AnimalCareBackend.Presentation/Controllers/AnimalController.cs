﻿
using AnimalCareBackend.Application.DTOS;
using AnimalCareBackend.Application.Interface;
using AnimalCareBackend.Core.Entities;
using AnimalCareBackend.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AnimalCareBackend.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalService _animalService;

        public AnimalController(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAnimals()
        {
            var animals = await _animalService.GetAllAsync();
            return Ok(animals);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnimalById(Guid id)
        {
            var animal = await _animalService.GetByIdAsync(id);
            if (animal == null)
            {
                return NotFound((new { Message = $"Erro ao encontrar o animal com a id: {id}" }));
            }

            return Ok(animal);
        }

        [HttpPost]
        public async Task<ActionResult> RegisterAnimal(AnimalCreateDto animalCreateDto)
        {
            try
            {
                await _animalService.RegisterAnimal(animalCreateDto);
                return Ok(new { Message = "Animal foi cadastrado com sucesso!"});
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Erro ao cadastrar o animal: {ex.Message}" });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAnimal(Guid id, AnimalUpdateDto animalUpdateDto)
        {
            try
            {
                var result = await _animalService.UpdateAnimal(id, animalUpdateDto);
                return result ? Ok(new { Message = "Animal foi atualizado com sucesso!" }) : BadRequest(new { Message = "Erro ao atualizar o animal" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Erro: {ex.Message}" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal(Guid id)
        {
            var result = await _animalService.DeleteAnimal(id);
            if (result)
            {
                return Ok(new { Message = "Animal foi excluído com sucesso!" });
            }

            return BadRequest(new { Message = "Erro ao deletar o animal" });
        }

    }
}