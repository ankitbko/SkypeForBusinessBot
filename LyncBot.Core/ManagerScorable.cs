using Microsoft.Bot.Builder.Internals.Scorables;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Bot.Builder.Dialogs.Internals;

namespace LyncBot.Core
{
    public class ManagerScorable : IScorable<IActivity, double>
    {
        protected readonly IBotToUser botToUser;
        protected readonly List<string> managerList;
        public ManagerScorable(IBotToUser botToUser, List<string> managerList)
        {
            this.botToUser = botToUser;
            this.managerList = managerList;
        }
        public Task DoneAsync(IActivity item, object state, CancellationToken token)
        {
            return Task.CompletedTask;
        }

        public double GetScore(IActivity item, object state)
        {
            return 1.0;
        }

        public bool HasScore(IActivity item, object state)
        {
            if (managerList.Contains((state as string), StringComparer.OrdinalIgnoreCase))
                return true;
            return false;
        }

        public Task PostAsync(IActivity item, object state, CancellationToken token)
        {
            return Task.CompletedTask;
        }

        public Task<object> PrepareAsync(IActivity item, CancellationToken token)
        {
            var message = item as IMessageActivity;
            return Task.FromResult<object>(message.From.Id);
        }
    }
}
