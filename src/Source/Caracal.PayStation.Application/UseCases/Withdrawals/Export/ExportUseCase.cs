using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Caracal.Framework.Data;
using Caracal.Framework.UseCases;
using Caracal.PayStation.Payments;

using Caracal.Framework.Xml;
using Microsoft.Extensions.FileProviders;

namespace Caracal.PayStation.Application.UseCases.Withdrawals.Export {
    public class ExportUseCase: UseCase<ExportResponse, ExportRequest> {
        private readonly IMapper _mapper;
        private readonly WithdrawalService _service;
        private readonly IFileProvider _fileProvider;
        
        public ExportUseCase(WithdrawalService service, IFileProvider fileProvider) {
            _mapper = Mappings.Create();
            _service = service;
            _fileProvider = fileProvider;
        }

        public override async Task<ExportResponse> ExecuteAsync(ExportRequest request, CancellationToken cancellationToken) {
            var withdrawals = await _service.GetWithdrawalsAsync(new PagedDataFilter(), cancellationToken);
            var xslt = await GetXsltAsync();

            return new ExportResponse { Content = withdrawals.Items.ToXml(xslt) };
        }

        private async Task<string> GetXsltAsync() {
            var dir = _fileProvider.GetDirectoryContents(Path.Join("Export", "Templates"));
            var file = dir.FirstOrDefault(f => f.Name.Equals("default.xslt"));

            if (file == null)
                return string.Empty;
            
            var contents = new StreamReader(file.CreateReadStream());
            return await contents.ReadToEndAsync();
        }
    }
}