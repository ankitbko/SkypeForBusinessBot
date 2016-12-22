using Microsoft.Lync.Model;
using Microsoft.Lync.Model.Conversation;
using System;
using System.Threading.Tasks;

namespace LyncBot.Core
{
    public class ConversationService
    {
        private InstantMessageModality sender;

        public ConversationService(InstantMessageModality sender)
        {
            this.sender = sender;
        }

        public string ParticipantName
        {
            get
            {
                return sender.Participant.Properties[ParticipantProperty.Name] as string;
            }
        }

        public string ParticipantId
        {
            get
            {
                return sender.Participant.Contact.Uri;
            }
        }

        public string ConversationId
        {
            get
            {
                return sender.Conversation.Properties[ConversationProperty.Id] as string;
            }
        }

        public async Task SendMessageToUser(string message)
        {
            sender.BeginSetComposing(true, null, null);
            await Task.Delay(3000);
            sender.BeginSendMessage(message, AfterMessageSent, null);
        }

        private void AfterMessageSent(IAsyncResult ar)
        {
            try
            {
                sender.EndSendMessage(ar);
            }
            catch (Exception)
            {
                // eat exception
            }
            finally
            {
                sender.BeginSetComposing(false, null, null);
            }
        }
    }
}