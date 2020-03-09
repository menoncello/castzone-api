using System;

using Microsoft.AspNetCore.Mvc;

namespace CastZone.Api.Controllers
{
    using System.Text;

    using RabbitMQ.Client;

    [ApiController]
    [Route("[controller]")]
    public class ImporterController : ControllerBase
    {
        private readonly ConnectionFactory rabbitFactory;

        public ImporterController()
        {
            this.rabbitFactory = new ConnectionFactory
            {
                HostName = Environment.GetEnvironmentVariable("RabbitUrl"),
                Password = Environment.GetEnvironmentVariable("RabbitPassword"),
                UserName = Environment.GetEnvironmentVariable("RabbitUserName")
            };
        }

        public bool Crawl()
        {
            using var connection = this.rabbitFactory.CreateConnection();
            using var channel = connection.CreateModel();
            var bytes = Encoding.UTF8.GetBytes("{}");
            channel.BasicPublish(string.Empty, "CastZone.Crawl", true, null, bytes);

            return true;
        }

        [Route("{id}")]
        public int Podcast(int id)
        {
            return id;
        }
    }
}