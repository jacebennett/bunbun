﻿using RabbitMQ.Client;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;

namespace BunBun.Core.Messaging {
  public class RabbitMQRegistry : Registry {
    public RabbitMQRegistry(string rabbitHost) {
      For<ConnectionFactory>()
        .Singleton()
        .Use(() => new ConnectionFactory { HostName = rabbitHost });

     For<IConnection>()
        .Singleton()
        .Use(ctx => ctx.GetInstance<ConnectionFactory>().CreateConnection());

      For<IModel>()
        .LifecycleIs(new ThreadLocalStorageLifecycle())
        .Use(ctx => ctx.GetInstance<IConnection>().CreateModel());

      For<IEncodeTransportMessages>().Use<JsonMessageSerializer>();
      For<IDecodeTransportMessages>().Use<JsonMessageSerializer>();
    }
  }
}
