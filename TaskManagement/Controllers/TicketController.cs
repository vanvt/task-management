using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Entities;
using TaskManagement.Service;

namespace TaskManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketController : Controller
    {
        private TicketService _ticketService;
        private TicketImageService _ticketImageService;
        public TicketController(TicketService ticketService,TicketImageService ticketImageService)
        {
            _ticketService = ticketService;
            _ticketImageService = ticketImageService;
        }
        [HttpGet]
        //Users can drill into tasks to see all details
        public IEnumerable<Ticket> Get()
        {
            var listTicket = _ticketService.Get();
            var listTicketImage = _ticketImageService.Get();
            var rs = from ticket in listTicket join image in listTicketImage
                     on ticket.Id equals image.TaskId
                     into empty from result in empty.DefaultIfEmpty()
                     select new Ticket
                     {
                         DeadLine = ticket.DeadLine,
                         Id = ticket.Id,
                         Description = ticket.Description,
                         IsFavorited = ticket.IsFavorited,
                         Name = ticket.Name,
                         States = ticket.States,
                         TicketImages = listTicketImage?? new List<TicketImage>()
                     };
            return rs;

        }

        [HttpPost]
        //Users can add new tasks to the task board
        public void Post(Ticket ticket)
        {
            _ticketService.Add(ticket);
        }
        [HttpPut]
        // Users can edit tasks and all details
        // Users can add columns to the task list representing different work states (ie. ToDo, In Progress & Done)
        // Users can move tasks between columns
        public void Put(Ticket ticket)
        {
            _ticketService.Update(ticket);
        }
        [HttpDelete]
        // Users can delete tasks
        public void Delete(int id) {

            _ticketService.Delete(id);
        }
        [HttpPost("attach-images")]
        //Users can attach images to tasks
        public void AttachImages(List<TicketImage> images)
        {
            _ticketService.AttachImages(images);
        }
    }
}
