using AutoMapper;
using Clase5_proyecto.DTOs;
using Clase5_proyecto.Models;
using Clase5_proyecto.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Clase5_proyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PilotoController : Controller //ControllerBase -- Cambiar para vincular con las vistas (View() y poder verlo al lanzar la app)
    {
        private PlayContext _context;
        private IValidator<PilotoInsertDto> _pilotoInsertValidator;
        private IPilotoService _pilotoService;
        private IMapper _mapper;

        public PilotoController(PlayContext context, IValidator<PilotoInsertDto> pilotoInsertValidator, IPilotoService pilotoService, IMapper mapper)
        {
            _context = context;
            _pilotoInsertValidator = pilotoInsertValidator;
            _pilotoService = pilotoService;
            _mapper = mapper;
        }

        // PARA VINCULAR CON EL FRONTEND:  
        [HttpGet("lista")]
        public async Task<ActionResult> ListaPilotos()
        {
            var pilotos = await _pilotoService.Get();
            return View(pilotos);
        }

        // PETICIONES BACKEND:
        // CREAR y LEER TODOS LOS REGISTROS DE LA BBDD
        [HttpGet]
        public async Task<IEnumerable<PilotoDto>> Get() =>
            await _pilotoService.Get();

        // LEER LOS REGISTROS DE LA BBDD POR ID
        [HttpGet("{id}")]
        public async Task<ActionResult<PilotoDto>> GetById(int id)
        {
            var pilotoDto = await _pilotoService.GetById(id);
            return pilotoDto == null ? NotFound() : Ok(pilotoDto);
        }

        // AÑADIR NUEVOS REGISTROS A LA BBDD
        [HttpPost]
        public async Task<ActionResult<PilotoDto>> Add(PilotoInsertDto pilotoInsertDto)
        {
            // Validar los datos:
            var validationResult = await _pilotoInsertValidator.ValidateAsync(pilotoInsertDto);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            // Llamar al servicio para agregar la piloto a la base de datos
            var createdPiloto = await _pilotoService.Add(pilotoInsertDto);
            return CreatedAtAction(nameof(GetById), new { id = createdPiloto.Id }, createdPiloto); // Opción mejorar para devolver 201 Created                        
        }

        // MODIFICAR POR ID
        [HttpPut("{id}")]
        public async Task<ActionResult<PilotoDto>> Update(int id, PilotoUpdateDto pilotoUpdateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var pilotoDto = await _pilotoService.Update(id, pilotoUpdateDto);
            if (pilotoDto == null) return NotFound();

            return Ok(pilotoDto);
        }

        // ELIMINAR UN REGISTRO POR ID
        [HttpDelete("{id}")]
        public async Task<ActionResult<PilotoDto>> Delete(int id)
        {
            var pilotoDto = await _pilotoService.Delete(id);
            if (pilotoDto == null) return NotFound();

            return Ok(pilotoDto);
        }
    }
}
