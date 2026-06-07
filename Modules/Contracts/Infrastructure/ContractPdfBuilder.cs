using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ShiftManagement.Api.BuildingBlocks.Pdf;
using ShiftManagement.Api.Modules.Contracts.Application;
using System.Globalization;

namespace ShiftManagement.Api.Modules.Contracts.Infrastructure.Pdf;

public class ContractPdfBuilder
{
    public IDocument Build(EmploymentContractPdfModel model)
    {
        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(40);

                page.DefaultTextStyle(x => PdfStyles.Body);

                page.Header()
                    .Element(header =>
                    {
                        PdfComponents.Header(header, "CONTRATO DE TRABAJO");
                    });

                page.Content().Column(col =>
                {
                    PdfComponents.SectionTitle(col.Item(), "Información del contrato");

                    PdfComponents.Paragraph(col.Item(),
                        $"Número de contrato: {model.Id}");

                    PdfComponents.Paragraph(col.Item(),
                        $"Estado: {model.Status}");

                    PdfComponents.SectionTitle(col.Item(), "Colaborador");

                    PdfComponents.Paragraph(col.Item(),
                        model.CollaboratorName);

                    PdfComponents.Paragraph(col.Item(),
                        $"Aprobado por: {model.ApprovedByName}");

                    PdfComponents.SectionTitle(col.Item(), "Condiciones laborales");

                    PdfComponents.Paragraph(col.Item(),
                        $"Tipo de contrato: {model.Type}");

                    PdfComponents.Paragraph(col.Item(),
                        $"Jornada: {model.WorkScheduleType}");

                    PdfComponents.SectionTitle(col.Item(), "Remuneración");

                    PdfComponents.Paragraph(col.Item(),
                        $"{model.SalaryAmount} {model.Currency} por {model.PayPeriod}");

                    PdfComponents.SectionTitle(col.Item(), "Vigencia");

                    PdfComponents.Paragraph(col.Item(),
                        $"Fecha de inicio: {model.StartsAt}");

                    PdfComponents.Paragraph(col.Item(),
                        $"Fecha de término: {(model.EndsAt.HasValue
                            ? model.EndsAt.Value.ToString("dd 'de' MMMM 'de' yyyy", new CultureInfo("es-CL"))
                            : "Indefinido")}");

                    if (!string.IsNullOrWhiteSpace(model.TerminationReason))
                    {
                        PdfComponents.SectionTitle(col.Item(), "Terminación");

                        PdfComponents.Paragraph(col.Item(),
                            model.TerminationReason);
                    }

                    // Pie de documento
                    col.Item()
                        .PaddingTop(20)
                        .Text(text =>
                        {
                            text.DefaultTextStyle(x =>
                                x.FontSize(9)
                                 .FontColor(Colors.Grey.Darken1));

                            text.Span("Documento generado el: ");
                            text.Span(model.CreatedAt.ToString(
                                "dd 'de' MMMM 'de' yyyy HH:mm",
                                new CultureInfo("es-CL")));
                        });
                });

                page.Footer()
                    .AlignCenter()
                    .Text(text =>
                    {
                        text.DefaultTextStyle(x =>
                            x.FontSize(9)
                            .FontColor(Colors.Grey.Darken1));

                        text.Span("Sistema de Gestión de Turnos • Documento confidencial");
                    });
            });
        });
    }
}