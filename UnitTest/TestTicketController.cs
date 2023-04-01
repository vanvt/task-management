using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Controllers;
using TaskManagement.Entities;
using TaskManagement.Repository;
using TaskManagement.Service;

namespace UnitTest
{
    public class TestTicketController
    {
        TicketService _ticketService;
        TicketImageService _ticketImageService;
        TicketController _ticketController;
        [SetUp]
        public void Setup() {
            File.Delete(typeof(Ticket).ToString() + ".json");
            File.Delete(typeof(TicketImage).ToString() + ".json");
            _ticketService = new TicketService(new TaskManagement.Repository.TicketRepository(),
                new TaskManagement.Repository.TicketImageRepository());
            _ticketImageService = new TicketImageService();
            _ticketController = new TicketController(_ticketService, _ticketImageService);
        }
        [Test]
        public void GetEmptyList()
        {
           var list = _ticketController.Get();
            Assert.IsEmpty(list);
        }
        [Test]
        public void GetList()
        {
            var ticket = new Ticket();
            ticket.Name = "Name";
            ticket.Description = "Description";
            ticket.States = "Todo";
            _ticketController.Post(ticket);
            var list = _ticketController.Get();
            Assert.IsNotEmpty(list);
        }
        [Test]
        public void Post()
        {
            var ticket = new Ticket();
            ticket.Name = "Name";
            ticket.Description = "Description";
            ticket.States = "Todo";
            _ticketController.Post(ticket);
            var list = _ticketController.Get();
            Assert.IsNotEmpty(list);
        }
        [Test]
        public void Put()
        {
            var ticket = new Ticket();
            ticket.IsFavorited = false;
            ticket.Name = "test";
            ticket.Id = 188;
            _ticketController.Post(ticket);

            var updateItem = _ticketController.Get().ElementAt(0);
            updateItem.Name = "vanvttest";
            _ticketController.Put(updateItem);

            var result = _ticketController.Get().ToList().Find(t => t.Id == ticket.Id);
            Assert.AreEqual("vanvttest", result.Name);
        }

        [Test]
        public void Delete()
        {
            var ticket = new Ticket();
            ticket.IsFavorited = false;
            ticket.Name = "test";
            ticket.Id = 188;
            _ticketController.Post(ticket);
            _ticketController.Delete(ticket.Id);
            var result = _ticketController.Get().ToList().Find(t => t.Id == ticket.Id);
            Assert.Null(result);
        }

        [Test]
        public void AttachImages()
        {
            var ticket = new Ticket();
            ticket.IsFavorited = false;
            ticket.Name = "test";
            ticket.Id = 188;

            var listImage = new List<TicketImage>();
          
            var image1 = new TicketImage { TaskId = ticket.Id, ImageURL = "1" };
            var image2 = new TicketImage { TaskId = ticket.Id, ImageURL = "2" };
            
            listImage.Add(image1);
            listImage.Add(image2);
            _ticketController.Post(ticket);
            _ticketController.AttachImages(listImage);
            var ticketResult = _ticketController.Get().ToList().Find(t => t.Id == ticket.Id);
            Assert.IsTrue(ticketResult.TicketImages.Count() == listImage.Count);
        }

    }
}
