using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Caracal.Framework.UseCases;
using Caracal.PayStation.Payments;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RulesEngine.Extensions;
using RulesEngine.Models;

namespace Caracal.PayStation.Application.UseCases.Withdrawals.AutoAllocate {
    public class AutoAllocateUseCase: UseCase<AutoAllocateResponse, AutoAllocateRequest> {
        private readonly WithdrawalService _service;
        private readonly IFileProvider _fileProvider;
        
        public AutoAllocateUseCase(WithdrawalService service, IFileProvider fileProvider) {
            _service = service;
            _fileProvider = fileProvider;
        }
        
        public override async Task<AutoAllocateResponse> ExecuteAsync(AutoAllocateRequest request, CancellationToken cancellationToken) {
            var basicInfo = "{\"name\": \"hello\",\"email\": \"abcy@xyz.com\",\"creditHistory\": \"good\",\"country\": \"canada\",\"loyalityFactor\": 3,\"totalPurchasesToDate\": 10000}";
            var orderInfo = "{\"totalOrders\": 5,\"recurringItems\": 2}";
            var telemetryInfo = "{\"noOfVisitsPerMonth\": 10,\"percentageOfBuyingToVisit\": 15}";

            var converter = new ExpandoObjectConverter();

            dynamic input1 = JsonConvert.DeserializeObject<ExpandoObject>(basicInfo, converter);
            dynamic input2 = JsonConvert.DeserializeObject<ExpandoObject>(orderInfo, converter);
            dynamic input3 = JsonConvert.DeserializeObject<ExpandoObject>(telemetryInfo, converter);

            var inputs = new dynamic[]
            {
                input1,
                input2,
                input3
            };
            
            var workflowRules = JsonConvert.DeserializeObject<List<WorkflowRules>>(await GetRulesAsync());
            var reSettingsWithCustomTypes = new ReSettings { CustomTypes = new [] { typeof(Utils) } };
            
            var bre = new RulesEngine.RulesEngine(workflowRules.ToArray(), null, reSettingsWithCustomTypes);
            
            string discountOffered = "No discount offered.";

            List<RuleResultTree> resultList = await bre.ExecuteAllRulesAsync("Discount", inputs);

            resultList.OnSuccess((eventName) => {
                discountOffered = $"Discount offered is {eventName} % over MRP.";
                var bbb = resultList.ToArray();
            });

            resultList.OnFail(() => {
                discountOffered = "The user is not eligible for any discount.";
            });

            return new AutoAllocateResponse {
                Result = discountOffered
            };
        }
        
        private async Task<string> GetRulesAsync() {
            var dir = _fileProvider.GetDirectoryContents(Path.Join("Workflow", "Rules"));
            var file = dir.FirstOrDefault(f => f.Name.Equals("discount.rule.json"));

            if (file == null)
                return string.Empty;
            
            var contents = new StreamReader(file.CreateReadStream());
            return await contents.ReadToEndAsync();
        }
    }
}