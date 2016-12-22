using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LyncBot.Core
{
    public static class Extension
    {
        public static async Task PostAsync(this IDialogContext context, List<string> texts, string locale = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            Random rnd = new Random();
            var next = rnd.Next(0, texts.Count);
            await context.PostAsync(texts[next], locale, cancellationToken);
        }

        public static async Task PostOnlyOnceAsync(this IDialogContext context, List<string> texts, string key, string locale = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if(!context.PrivateConversationData.ContainsKey(key))
            {
                await context.PostAsync(texts, locale, cancellationToken);
                context.PrivateConversationData.SetValue(key, true);
            }
        }
    }
}
