using NServiceBus;
using NServiceBus.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObservableSaga.NServiceBusConfig.Features
{
    public class NotifySagaSubscribersFeature : Feature
    {
        internal NotifySagaSubscribersFeature()
        {
            EnableByDefault();
            DependsOn<NServiceBus.Features.Sagas>();
        }

        protected override void Setup(FeatureConfigurationContext context)
        {
            var pipeline = context.Pipeline;
            pipeline.Register(
                stepId: "NotifySagaSubscribers",
                behavior: typeof(SignalRUpdateBehavior),
                description: "Logs handler time");
        }
    }
}
