﻿using MassTransit;

namespace Relatorio.API.Configuration
{
    public static class MessageBusConfigRabbit
    {
      /*  public static void AddMessageBusConfigurationRabbit(this IServiceCollection services,
           IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<PagamentoRealizadoIntegrationHandler>();
                x.AddConsumer<PagamentoCanceladoIntegrationHandler>();

                x.SetKebabCaseEndpointNameFormatter();
                x.UsingRabbitMq((context, cfg) =>
                {
                    var rabbitMQConfig = configuration.GetSection("MessageQueueConnection:MassTransit");
                    cfg.Host(new Uri(rabbitMQConfig["host"]), h =>
                    {
                        h.PublisherConfirmation = rabbitMQConfig.GetValue<bool>("publisherConfirms");
                        h.Username(rabbitMQConfig["credentials:username"]);
                        h.Password(rabbitMQConfig["credentials:password"]);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });
        }*/
    }
}
