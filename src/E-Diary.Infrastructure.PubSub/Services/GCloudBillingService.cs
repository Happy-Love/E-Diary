using E_Diary.Core.Dto.Operation;
using E_Diary.Core.IntegrationServices;
using E_Diary.Infrastructure.PubSub.Settings;
using Google.Cloud.PubSub.V1;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace E_Diary.Infrastructure.PubSub.Services
{
    public class GCloudBillingService : IGCloudBillingService
    {
        private readonly GCloudIntegrationSettings settings;

        public GCloudBillingService(
            IOptions<GCloudIntegrationSettings> settings)
        {
            this.settings = settings.Value;
        }

        public async Task<OperationResponse?> TryProcessingRequest(OperationRequest request)
        {
            var topic = TopicName.FromProjectTopic(settings.ProjectId, settings.TopicId);
            var publisher = await PublisherClient.CreateAsync(topic);

            var message = JsonConvert.SerializeObject(request);
            await publisher.PublishAsync(message);

            var subscription = SubscriptionName.FromProjectSubscription(settings.ProjectId, settings.SubscriptionId);
            var subscriberClient = await SubscriberServiceApiClient.CreateAsync();

            var messages = await subscriberClient.PullAsync(subscription, 20);
            await subscriberClient.AcknowledgeAsync(subscription, messages.ReceivedMessages.Select(m => m.AckId));

            var latestMessage = messages.ReceivedMessages.LastOrDefault();

            if (latestMessage == null)
            {
                throw new ApplicationException("Can't receive latest message");
            }

            var response = System.Text.Encoding.UTF8.GetString(latestMessage.Message.Data.ToArray());
            var result = JsonConvert.DeserializeObject<OperationResponse>(response);

            return result;
        }
    }
}
