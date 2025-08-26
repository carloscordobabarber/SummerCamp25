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
        public async Task<ActionResult<IEnumerable<PaymentDto>>> GetPayments()
        {
            var payments = await _context.Payments.ToListAsync();
            var dto = _mapper.Map<List<PaymentDto>>(payments);
            return Ok(dto);
        }

        // GET: api/Payments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDto>> GetPayment(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
                return NotFound();

            var dto = _mapper.Map<PaymentDto>(payment);
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
            await _context.SaveChangesAsync();

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
            await _context.SaveChangesAsync();

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
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}