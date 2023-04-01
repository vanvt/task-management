using TaskManagement.Entities;
using TaskManagement.Repository;

namespace UnitTest
{
    public class TestGenericRepository
    {
        GenericRepository<Ticket> _ticketRepository;
        GenericRepository<TicketImage> _ticketImageRepository;

        [SetUp]
        public void Setup()
        {
            File.Delete(typeof(Ticket).ToString() + ".json");
            File.Delete(typeof(TicketImage).ToString() + ".json");
            _ticketRepository = new GenericRepository<Ticket>();
            _ticketImageRepository = new GenericRepository<TicketImage>();
        }

        [Test]
        public void Add()
        {
            var ticket = new Ticket();
            ticket.IsFavorited = true;
            ticket.Name = "test";
            ticket.Id = 9999;
            ticket.Description = "test";
            _ticketRepository.Add(ticket);
            var result  =  _ticketRepository.Get().Find(t => t.Id == ticket.Id);
            Assert.IsTrue(ticket.Id == result.Id);
        }
        [Test]
        public void Delete()
        {
            var ticket = new Ticket();
            ticket.IsFavorited = false;
            ticket.Name = "test";
            ticket.Id = 188;
            _ticketRepository.Add(ticket);
            _ticketRepository.Delete(ticket);
            var result = _ticketRepository.Get().Find(t => t.Id == ticket.Id);
            Assert.Null(result);
        }
        [Test]
        public void AddRange()
        {
            var listImage = new List<TicketImage>();
            listImage.Add(new TicketImage { TaskId = 1111, ImageURL = "1111" });
            _ticketImageRepository.AddRange(listImage);
            var result =  _ticketImageRepository.Get().Count();
            Assert.IsTrue(result == 1);
        }
        [Test]
        public void Update()
        {
            var ticket = new Ticket();
            ticket.IsFavorited = false;
            ticket.Name = "test";
            ticket.Id = 188;
            _ticketRepository.Add(ticket);

            var list = _ticketRepository.Get();
            list[0].Name = "vanvttest";

            _ticketRepository.Update(list);
            var result = _ticketRepository.Get().Find(t => t.Id == ticket.Id);

            Assert.AreEqual("vanvttest", result.Name);
        }
    }
}