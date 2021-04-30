using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Caracal.PayStation.Application.UseCases.Withdrawals.ChangeStatus;
using Caracal.PayStation.Payments.Repositories;
using Elsa;
using Elsa.Activities.Http.Activities;
using Elsa.Activities.Http.Services;
using Elsa.Attributes;
using Elsa.Extensions;
using Elsa.Results;
using Elsa.Scripting.Liquid;
using Elsa.Scripting.Liquid.Services;
using Elsa.Services.Models;
using Microsoft.AspNetCore.Http;

namespace Caracal.PayStation.Api.Workflow.Activities {
    [ActivityDefinition(
    Category = "HTTP",
    DisplayName = "Client Event",
    Description = "Response from the client action",
    RuntimeDescription = "x => !!x.state.path ? `Handle <strong>${ x.state.method } ${ x.state.path }</strong>.` : x.definition.description",
    Outcomes = new[] { OutcomeNames.Done }
    )]
    public class ClientEvent: ReceiveHttpRequest {
        private ChangeWithdrawalStatusUseCase _useCase;
        private WithdrawalsRepository _repository;
       
        public ClientEvent(
            IHttpContextAccessor httpContextAccessor, IEnumerable<IHttpRequestBodyParser> parsers
            , ChangeWithdrawalStatusUseCase useCase, WithdrawalsRepository repository)
            : base(httpContextAccessor, parsers) {
            _useCase = useCase;
            _repository = repository;
        }

        [ActivityProperty(Hint = "Client workflow to run")]
        public string ClientWf { 
            get => GetState<string>();
            set => SetState(value); 
        }

        protected override async Task<ActivityExecutionResult> OnHaltedAsync(WorkflowExecutionContext context, CancellationToken cancellationToken) {
            var signal = await context.ExpressionEvaluator.EvaluateAsync<string>(
                new LiquidExpression<string>(Path.OriginalString), context, cancellationToken);
            
            var id = Convert.ToInt64(context.CurrentScope.GetVariable("withdrawalId"));

            await _repository.UpdateWFUrl(id, signal, cancellationToken);
            
            //var expression = new JavaScriptExpression<string>($"`{context.Workflow.Input.GetVariable<string>("Signal")}`");
            //var signaled =await expressionEvaluator.EvaluateAsync<string>(expression,context, cancellationToken);
            
            return await base.OnHaltedAsync(context, cancellationToken);
        }

        protected override Task<ActivityExecutionResult> OnExecuteAsync(WorkflowExecutionContext context, CancellationToken cancellationToken) {
            return base.OnExecuteAsync(context, cancellationToken);
        }
    }
}