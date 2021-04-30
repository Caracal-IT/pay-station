using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Caracal.PayStation.Application.UseCases.Withdrawals.UpdateClientEvent;
using Elsa;
using Elsa.Activities.Http.Activities;
using Elsa.Activities.Http.Services;
using Elsa.Attributes;
using Elsa.Extensions;
using Elsa.Results;
using Elsa.Scripting.Liquid;
using Elsa.Services.Models;
using Microsoft.AspNetCore.Http;

namespace Caracal.PayStation.Api.Workflow.Activities {
    [ActivityDefinition(
        Category = "HTTP",
        DisplayName = "Client Event",
        Description = "Response from the client action",
        RuntimeDescription = "x => !!x.state.path ? `Handle <strong>${ x.state.method } ${ x.state.path }</strong>.` : x.definition.description",
        Outcomes = new[] {OutcomeNames.Done}
    )]
    public class ClientEvent : ReceiveHttpRequest {
        private UpdateClientEventUseCase _useCase;

        public ClientEvent(IHttpContextAccessor httpContextAccessor, IEnumerable<IHttpRequestBodyParser> parsers, UpdateClientEventUseCase useCase)
            : base(httpContextAccessor, parsers) => _useCase = useCase;

        [ActivityProperty(Hint = "Client workflow to run")]
        public string ClientWf {
            get => GetState<string>();
            set => SetState(value);
        }

        protected override async Task<ActivityExecutionResult> OnHaltedAsync(WorkflowExecutionContext context, CancellationToken token) {
            var signal = await context.ExpressionEvaluator.EvaluateAsync(new LiquidExpression<string>(Path.OriginalString), context, token);

            var request = new UpdateClientEventRequest {
                WithdrawalId = Convert.ToInt64(context.CurrentScope.GetVariable("withdrawalId")),
                WorkflowUrl = signal
            };

            await _useCase.ExecuteAsync(request, token);

            return await base.OnHaltedAsync(context, token);
        }
    }
}