using AutoMapper;
using Clase5_proyecto.DTOs;
using Clase5_proyecto.Models;
using Clase5_proyecto.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Clase5_proyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AeronaveController : Controller //ControllerBase -- Cambiar para vincular con las vistas (View() y poder verlo al lanzar la app)
    {
        private PlayContext _context;
        private IValidator<AeronaveInsertDto> _aeronaveInsertValidator;
        private IAeronaveService _aeronaveService;
        private IMapper _mapper;

        public AeronaveController(PlayContext context, IValidator<AeronaveInsertDto> aeronaveInsertValidator, IAeronaveService aeronaveService, IMapper mapper)
        {
            _context = context;
            _aeronaveInsertValidator = aeronaveInsertValidator;
            _aeronaveService = aeronaveService;
            _mapper = mapper;
    }

        // PARA VINCULAR CON EL FRONTEND:  
        [HttpGet("lista")]     
        public async Task<IActionResult> ListaAeronaves()
        {
            var aeronaves = await _aeronaveService.Get();
            return View(aeronaves);
        }

        // PETICIONES BACKEND:
        // CREAR y LEER TODOS LOS REGISTROS DE LA BBDD
        [HttpGet]
        public async Task<IEnumerable<AeronaveDto>> Get() =>
            await _aeronaveService.Get();

        // LEER LOS REGISTROS DE LA BBDD POR ID
        [HttpGet("{id}")]
        public async Task<ActionResult<AeronaveDto>> GetById(int id)
        {
            var aeronaveDto = await _aeronaveService.GetById(id);
            return aeronaveDto == null ? NotFound() : Ok(aeronaveDto);
        }

        // AÑADIR NUEVOS REGISTROS A LA BBDD
        [HttpPost]
        public async Task<ActionResult<AeronaveDto>> Add(AeronaveInsertDto aeronaveInsertDto)
        {
            // Validar los datos:
            var validationResult = await _aeronaveInsertValidator.ValidateAsync(aeronaveInsertDto);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            // Llamar al servicio para agregar la aeronave a la base de datos
            var createdAeronave = await _aeronaveService.Add(aeronaveInsertDto);
            return CreatedAtAction(nameof(GetById), new { id = createdAeronave.Id }, createdAeronave); // Opción mejorar para devolver 201 Created                        
        }

        // MODIFICAR POR ID
        [HttpPut("{id}")]
        public async Task<ActionResult<AeronaveDto>> Update(int id, AeronaveUpdateDto aeronaveUpdateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var aeronaveDto = await _aeronaveService.Update(id, aeronaveUpdateDto);
            if (aeronaveDto == null) return NotFound();

            return Ok(aeronaveDto);
        }

        // ELIMINAR UN REGISTRO POR ID
        [HttpDelete("{id}")]
        public async Task<ActionResult<AeronaveDto>> Delete(int id)
        {
            var aeronaveDto = await _aeronaveService.Delete(id);
            if (aeronaveDto == null) return NotFound();

            return Ok(aeronaveDto);
        }
    }
}
