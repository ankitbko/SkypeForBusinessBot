using Autofac;
using LyncBot.Core;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Builder.Internals.Fibers;
using Microsoft.Bot.Builder.Internals.Scorables;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyncBot.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var manager = new LyncClientManager();
            RegisterBotDependencies();
            manager.Initialize();
            System.Console.WriteLine("Client running");
            System.Console.ReadLine();
        }

        static void RegisterBotDependencies()
        {
            var builder = new ContainerBuilder();

            #region Redis

            //RedisStoreOptions redisOptions = new RedisStoreOptions()
            //{
            //    Configuration = "localhost"
            //};

            //builder.Register(c => new RedisStore(redisOptions))
            //   .As<RedisStore>()
            //   .SingleInstance();

            //builder.Register(c => new CachingBotDataStore(c.Resolve<RedisStore>(),
            //                                              CachingBotDataStoreConsistencyPolicy.ETagBasedConsistency))
            //    .As<IBotDataStore<BotData>>()
            //    .AsSelf()
            //    .InstancePerLifetimeScope();
            #endregion

            #region InMemory
            builder.RegisterType<InMemoryDataStore>()
                .AsSelf()
                .SingleInstance();

            builder.Register(c => new CachingBotDataStore(c.Resolve<InMemoryDataStore>(),
                                                          CachingBotDataStoreConsistencyPolicy.ETagBasedConsistency))
                .As<IBotDataStore<BotData>>()
                .AsSelf()
                .InstancePerLifetimeScope();

            #endregion

            List<string> managerList = GetManagerList();

            builder.Register(c => new ManagerScorable(c.Resolve<IBotToUser>(), managerList))
                .Keyed<ManagerScorable>(FiberModule.Key_DoNotSerialize)
                .As<IScorable<IActivity, double>>()
                .InstancePerLifetimeScope();

            builder.Update(Conversation.Container);
        }

        private static List<string> GetManagerList()
        {
            return ConfigurationManager.AppSettings.Get("ManagerList").Split(';').ToList();
        }
    }
}
