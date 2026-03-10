using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebClient.Hubs;
using WebClient.Models;
using WebClient.Services;

namespace WebClient.Controllers.Api
{
    [Route("api/publishers")]
    [ApiController]
    public class PublisherApiController : ControllerBase
    {
        private readonly IPublisherService _publisherService;
        private readonly IHubContext<AppHub> _hubContext;

        public PublisherApiController(IPublisherService publisherService, IHubContext<AppHub> hubContext)
        {
            _publisherService = publisherService;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetPublishers()
        {
            var publishers = await _publisherService.GetAllPublishersAsync();
            return Ok(publishers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublisher(int id)
        {
            var publisher = await _publisherService.GetPublisherByIdAsync(id);

            if (publisher == null)
            {
                return NotFound();
            }

            return Ok(publisher);
        }

        [HttpPost]
        public async Task<IActionResult> PostPublisher(Publisher publisher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdPublisher = await _publisherService.CreatePublisherAsync(publisher);
            await _hubContext.Clients.All.SendAsync("DropdownsUpdated");

            return CreatedAtAction(nameof(GetPublisher), new { id = createdPublisher.PubId }, createdPublisher);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPublisher(int id, Publisher publisher)
        {
            if (id != publisher.PubId)
            {
                return BadRequest("PublisherId mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedPublisher = await _publisherService.UpdatePublisherAsync(id, publisher);

            if (updatedPublisher == null)
            {
                return NotFound();
            }

            await _hubContext.Clients.All.SendAsync("DropdownsUpdated");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            try
            {
                var success = await _publisherService.DeletePublisherAsync(id);
                if (!success)
                {
                    return NotFound();
                }

                await _hubContext.Clients.All.SendAsync("DropdownsUpdated");

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
