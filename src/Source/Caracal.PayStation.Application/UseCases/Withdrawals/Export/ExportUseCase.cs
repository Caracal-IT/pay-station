using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;
using AutoMapper;
using Caracal.Framework.Data;
using Caracal.Framework.UseCases;
using Caracal.PayStation.Payments;

namespace Caracal.PayStation.Application.UseCases.Withdrawals.Export {
    public class ExportUseCase: UseCase<ExportResponse, ExportRequest> {
        private readonly IMapper _mapper;
        private readonly WithdrawalService _service;
        
        public ExportUseCase(WithdrawalService service) {
            _mapper = Mappings.Create();
            _service = service;
        }
        
        public override async Task<ExportResponse> ExecuteAsync(ExportRequest request, CancellationToken cancellationToken) {
            var withdrawals = await _service.GetWithdrawalsAsync(new PagedDataFilter(), cancellationToken);

            var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var x = new XmlSerializer(withdrawals.Items.GetType());
            x.Serialize(stringWriter, withdrawals.Items);
            try {
                return new ExportResponse {
                    Content = ApplyXSLT(sb.ToString()) //sb.ToString()
                };
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private string Xslt = @"<?xml version=""1.0""?>

            <xsl:stylesheet version=""1.0"" xmlns:xsl=""http://www.w3.org/1999/XSL/Transform"">
                <xsl:template match=""/"">
                    <xsl:for-each select=""ArrayOfWithdrawal/Withdrawal"">
                    <xsl:element name=""withdrawal"">
                        <xsl:attribute name=""id"">
                            <xsl:value-of select=""Id""/>
                        </xsl:attribute>
                        <xsl:attribute name=""account"">
                            <xsl:value-of select=""Account""/>
                        </xsl:attribute>
                        <xsl:attribute name=""amount"">
                            <xsl:value-of select=""Amount""/>
                        </xsl:attribute>
                    </xsl:element>
                    </xsl:for-each>
                </xsl:template>
            </xsl:stylesheet> 
        ";
        
        private string ApplyXSLT(string xmlInput)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlInput);

            var memoreStream = new MemoryStream();
            var reader = new StringReader(Xslt);
            
            XslCompiledTransform xsltTransform = new XslCompiledTransform();
            xsltTransform.Load(new XmlTextReader(reader));
 
           // MemoryStream memoreStream = new MemoryStream();
           xsltTransform.Transform(xmlDocument, null, memoreStream);
            memoreStream.Position = 0;
 
            StreamReader streamReader = new StreamReader(memoreStream);
            return streamReader.ReadToEnd();
        }
    }
}