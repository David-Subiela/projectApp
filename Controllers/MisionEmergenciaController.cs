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
    public class MisionEmergenciaController : Controller //ControllerBase -- Cambiar para vincular con las vistas (View() y poder verlo al lanzar la app)
    {
        private PlayContext _context;
        private IValidator<MisionEmergenciaInsertDto> _misionEmergenciaInsertValidator;
        private IMisionEmergenciaService _misionEmergenciaService;
        private IMapper _mapper;

        public MisionEmergenciaController(PlayContext context, IValidator<MisionEmergenciaInsertDto> misionEmergenciaInsertValidator, IMisionEmergenciaService misionEmergenciaService, IMapper mapper)
        {
            _context = context;
            _misionEmergenciaInsertValidator = misionEmergenciaInsertValidator;
            _misionEmergenciaService = misionEmergenciaService;
            _mapper = mapper;
        }

        // PARA VINCULAR CON EL FRONTEND:  
        [HttpGet("lista")]
        public async Task<ActionResult> ListaEmergencia()
        {
            var misionEmergencia = await _misionEmergenciaService.Get();
            return View(misionEmergencia);
        }

        // PETICIONES BACKEND:
        // CREAR y LEER TODOS LOS REGISTROS DE LA BBDD
        [HttpGet]
        public async Task<IEnumerable<MisionEmergenciaDto>> Get() =>
            await _misionEmergenciaService.Get();

        // LEER LOS REGISTROS DE LA BBDD POR ID
        [HttpGet("{id}")]
        public async Task<ActionResult<MisionEmergenciaDto>> GetById(int id)
        {
            var misionEmergenciaDto = await _misionEmergenciaService.GetById(id);
            return misionEmergenciaDto == null ? NotFound() : Ok(misionEmergenciaDto);
        }

        // AÑADIR NUEVOS REGISTROS A LA BBDD
        [HttpPost]
        public async Task<ActionResult<MisionEmergenciaDto>> Add(MisionEmergenciaInsertDto misionEmergenciaInsertDto)
        {
            // Validar los datos:
            var validationResult = await _misionEmergenciaInsertValidator.ValidateAsync(misionEmergenciaInsertDto);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            // Llamar al servicio para agregar la aeronave a la base de datos
            var createdMisionEmergencia = await _misionEmergenciaService.Add(misionEmergenciaInsertDto);
            return CreatedAtAction(nameof(GetById), new { id = createdMisionEmergencia.Id }, createdMisionEmergencia); // Opción mejorar para devolver 201 Created                        
        }

        // MODIFICAR POR ID
        [HttpPut("{id}")]
        public async Task<ActionResult<MisionEmergenciaDto>> Update(int id, MisionEmergenciaUpdateDto misionEmergenciaUpdateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var misionEmergenciaDto = await _misionEmergenciaService.Update(id, misionEmergenciaUpdateDto);
            if (misionEmergenciaDto == null) return NotFound();

            return Ok(misionEmergenciaDto);
        }

        // ELIMINAR UN REGISTRO POR ID
        [HttpDelete("{id}")]
        public async Task<ActionResult<MisionEmergenciaDto>> Delete(int id)
        {
            var misionEmergenciaDto = await _misionEmergenciaService.Delete(id);
            if (misionEmergenciaDto == null) return NotFound();

            return Ok(misionEmergenciaDto);
        }
    }
}
