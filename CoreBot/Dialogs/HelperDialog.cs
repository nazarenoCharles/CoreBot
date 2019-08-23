﻿using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoreBot.Dialogs
{
    public class HelperDialog : ComponentDialog
    {
        private const string HelpMsgText = "Show help here";
        private const string CancelMsgText = "Cancelling...";

        public HelperDialog(string id)
            : base(id)
        {
        }

        protected override async Task<DialogTurnResult> OnContinueDialogAsync(DialogContext innerDc, CancellationToken cancellationToken = default)
        {
            var result = await InterruptAsync(innerDc, cancellationToken);
            if (result != null)
            {
                return result;
            }
            return await base.OnContinueDialogAsync(innerDc, cancellationToken);
        }

        private async Task<DialogTurnResult> InterruptAsync(DialogContext innerDc, CancellationToken cancellationToken)
        {
            if (innerDc.Context.Activity.Type == ActivityTypes.Message)
            {
                var text = innerDc.Context.Activity.Text.ToLowerInvariant();

                switch (text)
                {
                    case "dialoga":                   
                        return await innerDc.BeginDialogAsync(nameof(DialogA), cancellationToken);
                    case "dialogb":
                        return await innerDc.BeginDialogAsync(nameof(DialogB), cancellationToken);
                    case "main":
                        return await innerDc.BeginDialogAsync(nameof(MainDialog), cancellationToken);
                }
            }
            return null;
        }   
    }
}
