using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoreBot.Dialogs
{
    public class DialogB : HelperDialog
    {
        public DialogB() : base(nameof(DialogB))
        {
            var waterfallSteps = new WaterfallStep[]
            {
                FirstStepAsync,
                SecondStepAsync,
            };
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), waterfallSteps));
            AddDialog(new ChoicePrompt(nameof(ChoicePrompt)));
        }
        private async Task<DialogTurnResult> FirstStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            await stepContext.Context.SendActivityAsync(MessageFactory.Text($"You are at Dialog B."), cancellationToken);
            return await stepContext.PromptAsync(nameof(ChoicePrompt),
                new PromptOptions
                {                    
                    Choices = ChoiceFactory.ToChoices(
                        new List<string>
                        {
                            "DialogA",
                            "DialogB",
                            "MainDialog"
                        })
                });
        }
        private async Task<DialogTurnResult> SecondStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.EndDialogAsync(cancellationToken);
        }
    }
}
