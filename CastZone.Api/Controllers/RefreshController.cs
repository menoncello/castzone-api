using System;

using Microsoft.AspNetCore.Mvc;

namespace CastZone.Api.Controllers
{
    using System.Text;

    using RabbitMQ.Client;

    [ApiController]
    [Route("[controller]")]
    public class RefreshController : ControllerBase
    {
        private readonly ConnectionFactory rabbitFactory;

        public RefreshController()
        {
            this.rabbitFactory = new ConnectionFactory
            {
                HostName = Environment.GetEnvironmentVariable("RabbitUrl"),
                Password = Environment.GetEnvironmentVariable("RabbitPassword"),
                UserName = Environment.GetEnvironmentVariable("RabbitUserName")
            };
        }

        [Route("all")]
        public bool AllPodcasts()
        {
            using var connection = this.rabbitFactory.CreateConnection();
            using var channel = connection.CreateModel();
            var bytes = Encoding.UTF8.GetBytes("{}");
            channel.BasicPublish(string.Empty, "CastZone.RefreshPodcasts", true, null, bytes);

            return true;
        }

        [Route("{id}")]
        public int Podcast(int id)
        {
            return id;
        }
    }
}