using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace ShiftManagement.Api.BuildingBlocks.Pdf;

public static class PdfComponents
{
    public static void Header(IContainer container, string title)
    {
        container.Column(col =>
        {
            col.Item().Text(title).Style(PdfStyles.Title);
            col.Item().PaddingBottom(PdfSpacing.MD).LineHorizontal(1);
        });
    }

    public static void SectionTitle(IContainer container, string text)
    {
        container.PaddingBottom(PdfSpacing.SM)
                 .Text(text)
                 .Style(PdfStyles.Subtitle);
    }

    public static void Paragraph(IContainer container, string text)
    {
        container.PaddingBottom(PdfSpacing.SM)
                 .Text(text)
                 .Style(PdfStyles.Body);
    }
}