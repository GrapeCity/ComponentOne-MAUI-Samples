FlexViewer
----------------------------------------
Demonstrates to use FlexViewer to view Pdf documents, FlexReport and SSRS reports.

This sample demonstrates how to use the C1PdfDocumentSource, FlexReport, and SSRS components with FlexViewer to view PDF documents, FlexReport reports, and SSRS reports.

To open a PDF document in the FlexViewer, set the C1PdfDocumentSource.DocumentLocation property to the path of the PDF.

To open an SSRS report in the FlexViewer, set the C1SsrsDocumentSource.DocumentLocation property to the path of the SSRS report.

To load a FlexReport report into the FlexViewer, use the flexReport.Load method to load the report and link it to the FlexViewer.