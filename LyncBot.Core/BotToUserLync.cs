using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Builder.Internals.Fibers;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LyncBot.Core
{
    public class BotToUserLync: IBotToUser
    {
        private IMessageActivity toBot;
        private Func<string, Task> _callback;

        public BotToUserLync(IMessageActivity toBot, Func<string, Task> callback)
        {
            SetField.NotNull(out this.toBot, nameof(toBot), toBot);
            _callback = callback;
        }

        public IMessageActivity MakeMessage()
        {
            return this.toBot;
        }

        public async Task PostAsync(IMessageActivity message, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _callback(message.Text);
            //if (message.Attachments?.Count > 0)
            //    _callback(ButtonsToText(message.Attachments));
        }

        private static string ButtonsToText(IList<Attachment> attachments)
        {
            var cardAttachments = attachments?.Where(attachment => attachment.ContentType.StartsWith("application/vnd.microsoft.card"));
            var builder = new StringBuilder();
            if (cardAttachments != null && cardAttachments.Any())
            {
                builder.AppendLine();
                foreach (var attachment in cardAttachments)
                {
                    string type = attachment.ContentType.Split('.').Last();
                    if (type == "hero" || type == "thumbnail")
                    {
                        var card = (HeroCard)attachment.Content;
                        if (!string.IsNullOrEmpty(card.Title))
                        {
                            builder.AppendLine(card.Title);
                        }
                        if (!string.IsNullOrEmpty(card.Subtitle))
                        {
                            builder.AppendLine(card.Subtitle);
                        }
                        if (!string.IsNullOrEmpty(card.Text))
                        {
                            builder.AppendLine(card.Text);
                        }
                        if (card.Buttons != null)
                        {
                            foreach (var button in card.Buttons)
                            {
                                builder.AppendLine(button.Title);
                            }
                        }
                    }
                }
            }
            return builder.ToString();
        }
    }
}
