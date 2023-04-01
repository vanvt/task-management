using TaskManagement.Entities;
using TaskManagement.Repository;

namespace TaskManagement.Service
{
    public class TicketService 
    {
        TicketRepository _ticketRepository;
        TicketImageRepository _ticketImageRepository;
        public TicketService(TicketRepository ticketRepository,TicketImageRepository ticketImageRepository)
        {
            _ticketRepository = ticketRepository;
            _ticketImageRepository = ticketImageRepository;
        }
        
        public void Add(Ticket ticket)
        {
            if(_ticketRepository.Get().Where(x=>x.Id==ticket.Id).Count() > 0)
            {
                throw new Exception("Id duplicate");
            }
            _ticketRepository.Add(ticket);
        }
        public void Update(Ticket ticket) {
            var list = Get();
            var updateItem = list.Find(x => x.Id == ticket.Id);
            if (updateItem == null) throw new Exception("Id not found");
            list.Remove(updateItem);
            list.Add(ticket);
            _ticketRepository.Update(list);
        }
        public void Delete(int ticketId) {
            _ticketRepository?.Delete(Get().Find(x => x.Id == ticketId));  
        }
        public List<Ticket> Get() {
            return _ticketRepository.Get();
        }
        public void AttachImages(List<TicketImage> ticketImages)
        {
            _ticketImageRepository.AddRange(ticketImages.ToList());
        }
    }
}
