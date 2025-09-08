using AutoMapper;
using DTOS;
using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CozyData;
using Microsoft.Extensions.Logging;

namespace SistemaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly ContextDataBase _context;
        private readonly IMapper _mapper;
        private readonly ILogger<PaymentsController> _logger;

        public PaymentsController(ContextDataBase context, IMapper mapper, ILogger<PaymentsController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Payments
        [HttpGet]
        public async Task<ActionResult<object>> GetPayments(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? statusId = null,
            [FromQuery] double? amount = null,
            [FromQuery] int? rentalId = null,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null,
            [FromQuery] string? bankAccount = null
        )
        {
            try
            {
                var paymentsQuery = _context.Payments.AsNoTracking().AsQueryable();

                if (!string.IsNullOrEmpty(statusId))
                    paymentsQuery = paymentsQuery.Where(p => p.StatusId.ToLower().Contains(statusId.ToLower()));

                if (amount.HasValue)
                    paymentsQuery = paymentsQuery.Where(p => p.Amount <= amount.Value);

                if (rentalId.HasValue)
                    paymentsQuery = paymentsQuery.Where(p => p.RentalId == rentalId.Value);

                if (startDate.HasValue)
                    paymentsQuery = paymentsQuery.Where(p => p.PaymentDate.Date >= startDate.Value.Date);

                if (endDate.HasValue)
                    paymentsQuery = paymentsQuery.Where(p => p.PaymentDate.Date <= endDate.Value.Date);

                if (!string.IsNullOrEmpty(bankAccount))
                    paymentsQuery = paymentsQuery.Where(p => p.BankAccount.Length >= 4 && p.BankAccount.Substring(p.BankAccount.Length - 4).Contains(bankAccount));

                var totalCount = await paymentsQuery.CountAsync();
                var payments = await paymentsQuery
                    .OrderByDescending(p => p.PaymentDate)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var dto = _mapper.Map<List<PaymentDto>>(payments);

                // Enmascarar BankAccount
                foreach (var item in dto)
                {
                    if (!string.IsNullOrEmpty(item.BankAccount) && item.BankAccount.Length > 4)
                    {
                        var last4 = item.BankAccount.Substring(item.BankAccount.Length - 4);
                        item.BankAccount = new string('*', item.BankAccount.Length - 4) + last4;
                    }
                    else if (!string.IsNullOrEmpty(item.BankAccount))
                    {
                        item.BankAccount = new string('*', item.BankAccount.Length);
                    }
                }

                _logger.LogInformation("Consulta de pagos realizada correctamente. Total: {Count}", dto.Count);
                return Ok(new
                {
                    totalCount,
                    items = dto
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de pagos");
                return StatusCode(500, $"Error al obtener la lista de pagos: {ex.Message}");
            }
        }

        // GET: api/Payments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDto>> GetPayment(int id)
        {
            try
            {
                var payment = await _context.Payments.FindAsync(id);
                if (payment == null)
                {
                    _logger.LogWarning("Pago no encontrado para id {Id}", id);
                    return NotFound();
                }

                var dto = _mapper.Map<PaymentDto>(payment);
                // Enmascarar BankAccount
                if (!string.IsNullOrEmpty(dto.BankAccount) && dto.BankAccount.Length > 4)
                {
                    var last4 = dto.BankAccount.Substring(dto.BankAccount.Length - 4);
                    dto.BankAccount = new string('*', dto.BankAccount.Length - 4) + last4;
                }
                else if (!string.IsNullOrEmpty(dto.BankAccount))
                {
                    dto.BankAccount = new string('*', dto.BankAccount.Length);
                }
                _logger.LogInformation("Consulta de pago realizada correctamente para id {Id}", id);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el pago con id {Id}", id);
                return StatusCode(500, $"Error al obtener el pago: {ex.Message}");
            }
        }

        // POST: api/Payments
        [HttpPost]
        public async Task<IActionResult> PostPayment([FromBody] PaymentDto paymentDto)
        {
            if (paymentDto == null)
            {
                _logger.LogWarning("Intento de creación de pago fallido: DTO nulo");
                return BadRequest();
            }

            var payment = _mapper.Map<Payment>(paymentDto);
            // Si tienes campos como CreatedAt, agrégalos aquí
            // payment.CreatedAt = DateTime.UtcNow;

            _context.Payments.Add(payment);
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Pago creado correctamente con id {Id}", payment.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar el pago");
                return StatusCode(500, $"Error al guardar el pago: {ex.Message}");
            }

            var resultDto = _mapper.Map<PaymentDto>(payment);
            return CreatedAtAction(nameof(GetPayment), new { id = payment.Id }, resultDto);
        }

        // PUT: api/Payments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayment(int id, [FromBody] PaymentDto paymentDto)
        {
            if (paymentDto == null || id != paymentDto.Id)
            {
                _logger.LogWarning("Intento de actualización de pago fallido: DTO nulo o id no coincide");
                return BadRequest();
            }

            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                _logger.LogWarning("Pago no encontrado para id {Id} al intentar actualizar", id);
                return NotFound();
            }

            // Mapear campos excepto Id (y CreatedAt si existiera)
            payment.StatusId = paymentDto.StatusId;
            payment.Amount = paymentDto.Amount;
            payment.RentalId = paymentDto.RentalId;
            payment.PaymentDate = paymentDto.PaymentDate;
            payment.BankAccount = paymentDto.BankAccount;
            // payment.UpdatedAt = DateTime.UtcNow; // Si tienes este campo

            _context.Entry(payment).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Pago actualizado correctamente con id {Id}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el pago con id {Id}", id);
                return StatusCode(500, $"Error al actualizar el pago: {ex.Message}");
            }

            return NoContent();
        }
    }
}