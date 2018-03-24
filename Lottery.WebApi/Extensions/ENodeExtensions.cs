using System.Net;
using System.Reflection;
using ECommon.Components;
using ECommon.Socketing;
using ENode.Commanding;
using ENode.Configurations;
using ENode.EQueue;
using EQueue.Clients.Producers;
using EQueue.Configurations;
using Lottery.Core.Caching;
using Lottery.Engine;
using Lottery.Infrastructure;
using Lottery.Infrastructure.Mail;
using Lottery.Infrastructure.Sms;
using Lottery.WebApi.Configration;
using Lottery.WebApi.Configration.Mapper;

namespace Lottery.WebApi.Extensions
{
    public static class ENodeExtensions
    {
        private static CommandService _commandService;

        public static ENodeConfiguration UseEQueue(this ENodeConfiguration enodeConfiguration)
        {
            var assemblies = new[] { Assembly.GetExecutingAssembly() };
            enodeConfiguration.RegisterTopicProviders(assemblies);

            var configuration = enodeConfiguration.GetCommonConfiguration();
            configuration.RegisterEQueueComponents();

            _commandService = new CommandService();
            configuration.SetDefault<ICommandService, CommandService>(_commandService);

            return enodeConfiguration;
        }
        public static ENodeConfiguration StartEQueue(this ENodeConfiguration enodeConfiguration)
        {

            var commandResultProcessor = new CommandResultProcessor().Initialize(new IPEndPoint(SocketUtils.GetLocalIPV4(), 9010));

            _commandService.Initialize(commandResultProcessor, new ProducerSetting
            {
                NameServerList = ServiceConfigSettings.NameServerEndpoints
            });

            _commandService.Start();
            return enodeConfiguration;
        }

        public static ENodeConfiguration InitEmailSeting(this ENodeConfiguration enodeConfiguration)
        {
            EmailSettingConfigs.InitEmailSetting();
            return enodeConfiguration;
        }

        public static ENodeConfiguration BuildContainer(this ENodeConfiguration enodeConfiguration)
        {
            enodeConfiguration.GetCommonConfiguration().BuildContainer();
            return enodeConfiguration;
        }

        public static ENodeConfiguration UseRedisCache(this ENodeConfiguration enodeConfiguration)
        {
            var configuration = enodeConfiguration.GetCommonConfiguration();
          
            configuration.SetDefault<ICacheManager, RedisCacheManager>(new RedisCacheManager(new RedisConnectionWrapper(DataConfigSettings.RedisServiceAddress)));

            return enodeConfiguration;
        }

        public static ENodeConfiguration ClearCache(this ENodeConfiguration enodeConfiguration)
        {
            var webapiConfig = ObjectContainer.Resolve<ILotteryApiConfiguration>();
            var cacheManager = ObjectContainer.Resolve<ICacheManager>();
            if (webapiConfig.ClearHistroyCache)
            {
                cacheManager.Clear();
            }
            return enodeConfiguration;
        }

        public static ENodeConfiguration InitLotteryEngine(this ENodeConfiguration enodeConfiguration)
        {
            EngineContext.Initialize();
            return enodeConfiguration;
        }

        public static ENodeConfiguration UseAutoMapper(this ENodeConfiguration enodeConfiguration,
            params string[] assemblyNames)
        {
            MapperConfig.InitAutoMapperConfig(assemblyNames);
            return enodeConfiguration;
        }
    }
}