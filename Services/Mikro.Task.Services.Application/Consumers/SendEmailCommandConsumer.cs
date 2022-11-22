using MassTransit;
using Mikro.Task.Services.Application.Dtos;
using Mikro.Task.Services.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MikroTask.Services.Application.Consumers
{
    public class SendEmailCommandConsumer : IConsumer<RecommendMovieEmailDto>
    {
        private readonly IEmailService _emailService;
        public SendEmailCommandConsumer(IEmailService emailService)
        {
            _emailService = emailService;
        }
        public async Task Consume(ConsumeContext<RecommendMovieEmailDto> context)
        {
            try
            {
                var fileName = $"{context.Message.Email}-{context.MessageId}";
          
                var result = await _emailService.SendEmailAsync(context.Message);
                if (result)
                {
                    await File.WriteAllTextAsync($"wwwroot/email-reports/{fileName}.txt", JsonSerializer.Serialize(context.Message));
                }
                else
                    await File.WriteAllTextAsync($"wwwroot/email-errors/{fileName}.txt", JsonSerializer.Serialize(context.Message));
            }
            catch (Exception ex)
            {
                var errMsg = $"{ex.Message} {JsonSerializer.Serialize(context.Message)}";  
                await File.WriteAllTextAsync($"wwwroot/email-errors/{new Guid().ToString()}.txt", errMsg);
            }
        }
    }
}
