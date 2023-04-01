using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Entities;
using TaskManagement.Repository;
using TaskManagement.Service;

namespace UnitTest
{
    internal class TestTicketService
    {
        TicketService _ticketService;
        TicketImageRepository _ticketImageRepository;
        [SetUp]
        public void SetUp()
        {
            File.Delete(typeof(Ticket).ToString() + ".json");
            File.Delete(typeof(TicketImage).ToString() + ".json");
            _ticketService = new TicketService(new TicketRepository(), new TicketImageRepository());
            _ticketImageRepository = new TicketImageRepository();
        }
        [Test]
        public void Add()
        {
            var ticket = new Ticket();
            ticket.IsFavorited = true;
            ticket.Name = "test";
            ticket.Id = 9999;
            ticket.Description = "test";
            _ticketService.Add(ticket);
            var result = _ticketService.Get().Find(t => t.Id == ticket.Id);
            Assert.IsTrue(ticket.Id == result.Id);
        }
        [Test]
        public void AddDulicate()
        {
            var ticket = new Ticket();
            ticket.IsFavorited = true;
            ticket.Name = "test";
            ticket.Id = 9999;
            ticket.Description = "test";
            _ticketService.Add(ticket);
            Assert.Throws<Exception>(() => _ticketService.Add(ticket));
        }
        [Test]
        public void Delete()
        {
            var ticket = new Ticket();
            ticket.IsFavorited = false;
            ticket.Name = "test";
            ticket.Id = 188;
            _ticketService.Add(ticket);
            _ticketService.Delete(ticket.Id);
            var result = _ticketService.Get().Find(t => t.Id == ticket.Id);
            Assert.Null(result);
        }
        [Test]
        public void AttachImages()
        {
            var listImage = new List<TicketImage>();
            var taskId = 1111;
            var image1 = new TicketImage { TaskId = taskId, ImageURL = "1" };
            var image2 = new TicketImage { TaskId = taskId, ImageURL = "2" };
            listImage.AddRange(listImage);  
            _ticketService.AttachImages(listImage);
            var ticketImageResult = _ticketImageRepository.Get().Find(t => t.TaskId == taskId);
            var result = _ticketImageRepository.Get().Count();
            Assert.IsTrue(result == listImage.Count);
        }
        [Test]
        public void Update()
        {
            var ticket = new Ticket();
            ticket.IsFavorited = false;
            ticket.Name = "test";
            ticket.Id = 188;
            _ticketService.Add(ticket);

            var updateItem = _ticketService.Get().ElementAt(0);
            updateItem.Name = "vanvttest";
            _ticketService.Update(updateItem);

            var result = _ticketService.Get().Find(t => t.Id == ticket.Id);
            Assert.AreEqual("vanvttest", result.Name);
        }
        [Test]
        public void UpdateNotFound()
        {
            var ticket = new Ticket();
            ticket.IsFavorited = false;
            ticket.Name = "test";
            ticket.Id = 188;
            Assert.Throws<Exception>(() => _ticketService.Update(ticket));
        }
    }
}
