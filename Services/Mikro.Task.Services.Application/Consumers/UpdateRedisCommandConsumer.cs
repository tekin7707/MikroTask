using MassTransit;
using Mikro.Task.Services.Application.Dtos;
using Mikro.Task.Services.Application.Helpers;
using Mikro.Task.Services.Application.Models;
using Mikro.Task.Services.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static MassTransit.ValidationResultExtensions;

namespace MikroTask.Services.Application.Consumers
{
    public class UpdateRedisCommandConsumer : IConsumer<NoContent>
    {
        private readonly RedisService _redisService;

        public UpdateRedisCommandConsumer(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task Consume(ConsumeContext<NoContent> context)
        {
            //remove movielist from redis
            var status = await _redisService.GetDb().KeyDeleteAsync("movielist");
        }
    }
}
