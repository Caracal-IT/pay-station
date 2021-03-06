using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Caracal.PayStation.Application.UseCases.Withdrawals.UpdateClientEvent;
using Caracal.PayStation.Payments;
using Caracal.PayStation.Payments.Models;
using Elsa;
using Elsa.Activities.Http.Activities;
using Elsa.Activities.Http.Models;
using Elsa.Activities.Http.Services;
using Elsa.Attributes;
using Elsa.Extensions;
using Elsa.Results;
using Elsa.Scripting.Liquid;
using Elsa.Services.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Caracal.PayStation.Api.Workflow.Activities {
    [ActivityDefinition(
        Category = "HTTP",
        DisplayName = "Client Event",
        Description = "Response from the client action",
        RuntimeDescription = "x => !!x.state.path ? `Handle <strong>${ x.state.method } ${ x.state.path }</strong>.` : x.definition.description",
        Outcomes = new[] {OutcomeNames.Done}
    )]
    public class ClientEvent : ReceiveHttpRequest {
        private readonly UpdateClientEventUseCase _useCase;
        private readonly WithdrawalService _service;

        public ClientEvent(IHttpContextAccessor httpContextAccessor, IEnumerable<IHttpRequestBodyParser> parsers, UpdateClientEventUseCase useCase, WithdrawalService service)
            : base(httpContextAccessor, parsers) {
            _useCase = useCase;
            _service = service;
        }

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

        protected override async Task<ActivityExecutionResult> OnResumeAsync(WorkflowExecutionContext workflowContext,
            CancellationToken cancellationToken) {
            var result = await base.OnResumeAsync(workflowContext, cancellationToken);

            var requestModel = workflowContext.CurrentScope.LastResult.Value as HttpRequestModel;
            var body = requestModel?.Body.ToString();
            var request = JsonConvert.DeserializeObject<Withdrawal?>(body ?? "");

            if (string.IsNullOrWhiteSpace(request?.Amount)) return result;

            var withdrawalId = Convert.ToInt64(workflowContext.CurrentScope.GetVariable("withdrawalId"));
            await _service.UpdateAmountAsync(withdrawalId, request.Amount, cancellationToken);

            return result;
        }
    }
}