using QuestPDF.Infrastructure;
using QuestPDF.Helpers;
using QuestPDF.Fluent;

namespace ShiftManagement.Api.BuildingBlocks.Pdf;

public static class PdfStyles
{
    public static TextStyle Title =>
        TextStyle.Default
            .FontSize(18)
            .SemiBold()
            .FontColor(Colors.Black);

    public static TextStyle Subtitle =>
        TextStyle.Default
            .FontSize(14)
            .Medium()
            .FontColor(Colors.Grey.Darken2);

    public static TextStyle Body =>
        TextStyle.Default
            .FontSize(11)
            .FontColor(Colors.Grey.Darken3);

    public static TextStyle Small =>
        TextStyle.Default
            .FontSize(9)
            .FontColor(Colors.Grey.Darken1);
}