using AutoMapper;
using DTOS;
using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CozyData;

namespace SistemaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly ContextDataBase _context;
        private readonly IMapper _mapper;

        public PaymentsController(ContextDataBase context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

            return Ok(new
            {
                totalCount,
                items = dto
            });
        }

        // GET: api/Payments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDto>> GetPayment(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
                return NotFound();

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
            return Ok(dto);
        }

        // POST: api/Payments
        [HttpPost]
        public async Task<IActionResult> PostPayment([FromBody] PaymentDto paymentDto)
        {
            if (paymentDto == null)
                return BadRequest();

            var payment = _mapper.Map<Payment>(paymentDto);
            // Si tienes campos como CreatedAt, agrégalos aquí
            // payment.CreatedAt = DateTime.UtcNow;

            _context.Payments.Add(payment);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
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
                return BadRequest();

            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
                return NotFound();

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
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar el pago: {ex.Message}");
            }

            return NoContent();
        }

        // DELETE: api/Payments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
                return NotFound();

            _context.Payments.Remove(payment);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar el pago: {ex.Message}");
            }

            return NoContent();
        }
    }
}