using Lime.Messaging.Contents;
using Lime.Protocol;
using NLog;
using System;
using System.Threading;
using System.Threading.Tasks;
using Takenet.MessagingHub.Client.InfoEntertainmentExtensions.Navigation;
using Takenet.MessagingHub.Client.InfoEntertainmentExtensions.Services;
using Takenet.MessagingHub.Client.Sender;
using Takenet.Mpa.MpaTranslateLime;
using BotPOC.Services;

namespace BotPOC.Receivers
{
public class DefaultMessageReceiver : MessageReceiverBase
    {

		private const string GenericErrorCommand = "#ErroGenerico";
        private readonly INavigationExtension _navigation;

        public DefaultMessageReceiver(
			IMessagingHubSender sender, 
			MySettings settings, 
			IContextManager context, 
			IContactService contactService,
			IMpaService mpaService,
            IGenericErrorService genericErrorService,
			ILogger logger,
            INavigationExtension navigation)
            : base(sender, settings, context, contactService, mpaService, genericErrorService, logger,navigation)
        {
            _navigation = navigation;
        }

        protected override async Task ReceiveMessageAsync(Message message, CancellationToken cancellationToken)
        {
            if (message.Metadata != null && message.Metadata.ContainsKey("messenger.ref"))
                await MakeNavigationBasedOnMetadata(message, cancellationToken);


            else
            {
                var navResult = await _mpaService.SendToMPA(message, cancellationToken);

                if (navResult.NavigationState == NavigationState.Error)
                {
                    _logger.Error($"Error on sending message to MPA. User from: {message.From} user message: {(message.Content as PlainText).Text}");

                    var result = await _mpaService.SendToMPA(GenericErrorCommand, message.From, cancellationToken);

                    if (result.NavigationState == NavigationState.Error)
                    {
                        throw (new Exception());
                    }
                } 
            }
        }

        private async Task MakeNavigationBasedOnMetadata(Message message, CancellationToken cancellationToken)
        {

            switch (message.Metadata["messenger.ref"])
            {
                case "MenuInicial":
                    StartQRCodeNavigation("MenuInicial", message, cancellationToken);
                    break;
                default:
                    await _navigation.GetAndExecuteNavigationAsync(message, cancellationToken);
                    break;
            }

        }

        private async void StartQRCodeNavigation(string text, Message message, CancellationToken cancellationToken)
        {
            await _navigation.GetAndExecuteNavigationAsync(message.From, text, cancellationToken);
        }

    }
}
