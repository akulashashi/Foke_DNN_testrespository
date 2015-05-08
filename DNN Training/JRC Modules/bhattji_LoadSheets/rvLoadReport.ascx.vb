' Copyright (c) 2002-2005
' by Jaydeep Bhatt, Vision Consultants. ( http://www.bhattji.com )
'
' Permission is hereby granted, to the person obtaining a copy of this software legaly and associated
' documentation files (the "Software"), to deal in the Software with restriction, including with limitation
' the rights to use, copy, modify, merge, PublishDate, distribute, sublicense, and/or sell copies of the Software, and
' to permit persons to whom the Software is furnished to do so, subject to the following conditions:
'
' The above copyright notice and this permission notice shall be included in all copies or substantial portions
' of the Software.
'
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
'

Imports System.Web.UI.WebControls
Imports DotNetNuke
Imports bhattji.Modules.LoadSheets.Business
Imports Microsoft.Reporting.WebForms
Imports System.IO

Namespace bhattji.Modules.LoadSheets

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The Links Class provides the UI for displaying the Links
    ''' </summary>
    '''
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[bhattji]	9/23/2004	Moved Links to a separate Project
    '''		[bhattji]	10/20/2004	Removed ViewOptions from Action menu
    ''' </history>
    ''' -----------------------------------------------------------------------------
    '''

    Public MustInherit Class rvLoadReport
        Inherits Entities.Modules.PortalModuleBase

#Region " Private Members "

        Private itemId As Integer
        'Private loadId As String
        'Private objOI As OptionsInfo

#End Region

#Region " Methods "

        Private Sub RunReport()
            ' Set the processing mode for the ReportViewer to Local 
            ReportViewer1.ProcessingMode = ProcessingMode.Local
            'ReportViewer1.ShowToolBar = False
            'ReportViewer1.ShowRefreshButton = True
            ReportViewer1.ShowZoomControl = False
            'ReportViewer1.ShowPrintButton = True

            'ReportViewer1.ShowExportControls = False
            ReportViewer1.ShowFindControls = False
            ReportViewer1.ShowPageNavigationControls = False

            ReportViewer1.LocalReport.ReportPath = Server.MapPath(ModulePath + "Reports/LoadReport.rdlc")

            'ReportViewer1.LocalReport.ListRenderingExtensions[1]
            'RenderingExtension re= New RenderingExtension();

            Dim dt As DataTable = (New LoadSheetsController).GetLoadConfirmHeader(itemId)
            Dim loadId = (New LoadSheetsController).GetLoadId(itemId)
            Dim dt2 As DataTable = (New LoadPUsController).GetReportLoadPUByLoadId(loadId)
            Dim dt3 As DataTable = (New LoadDropsController).GetReportLoadDropByLoadId(loadId)

            'If chkSortDesc.Checked Then
            '    dt.DefaultView.Sort = ddlSort.SelectedValue & " DESC"
            'Else
            '    dt.DefaultView.Sort = ddlSort.SelectedValue
            'End If

            Dim ReportDataSource1 As New ReportDataSource("DataSet1_DataTable1", dt)
            Dim ReportDataSource2 As New ReportDataSource("DataSet1_DataTable2", dt2)
            Dim ReportDataSource3 As New ReportDataSource("DataSet1_DataTable3", dt3)

            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.LocalReport.DataSources.Add(ReportDataSource1)
            ReportViewer1.LocalReport.DataSources.Add(ReportDataSource2)
            ReportViewer1.LocalReport.DataSources.Add(ReportDataSource3)

            ' Create the prmCopyright ReportParameter 
            'Dim prmCopyright As New ReportParameter("prmCopyright", "Copyrighted by JRC Transportation")
            'prmCopyright.Name = "prmCopyright"; 
            'prmCopyright.Values.Add("Copyrighted by Jaydeep Bhatt"); 
            ' Create the prmCopyright ReportParameter 
            'Dim prmCompanyName As New ReportParameter("prmCompanyName", "JRC Office: " + ddlJRCIOfficeCode.SelectedItem.Text)

            ' Set the report parameters for the report 
            'ReportViewer1.LocalReport.SetParameters(New ReportParameter() {prmCopyright, prmCompanyName})
            'ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { prmCopyright}); 
            'ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { prmCompanyName }); 
        End Sub

#End Region

#Region " Event Handlers "

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Page_Load runs when the control is loaded
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[bhattji]	9/23/2004	Moved Links to a separate Project
        ''' </history>
        ''' -----------------------------------------------------------------------------
        '''
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Try
                'objOI = New OptionsInfo(ModuleId) 'This is must since it is being used in RowBound Method



                If Not (Request.QueryString("ItemId") Is Nothing) Then
                    itemId = Int32.Parse(Request.QueryString("ItemId"))
                    If Not Page.IsPostBack Then
                        RunReport()
                    End If
                Else
                    Response.Redirect(NavigateURL())
                End If


            Catch exc As Exception    'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try

        End Sub

        Protected Sub imbPrintSelection_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imbPrintSelection.Click
            ''txtNoOfItems.Text = "0"
            ''gdvViewList.AllowPaging = False
            ''ResetViewStates()
            'tblSearch.Visible = False 'not tblSearch.Visible
            ''If Page.IsPostBack Then Response.Write("<script type=""text/javascript"" language=""javascript"">window.print();</script>")
            ''Response.Write("<script type=""text/javascript"" language=""javascript"">window.print();</script>")
            'Me.Controls.Add(New LiteralControl("<script type=""text/javascript"" language=""javascript"">window.print();</script>"))

            'Dim warnings As Warning() = Nothing
            'Dim streamids As String() = Nothing
            'Dim mimeType As String = Nothing
            'Dim encoding As String = Nothing
            'Dim extension As String = Nothing
            'Dim bytes As Byte()


            'Dim FolderLocation As String = Server.MapPath("~/Documentation/")

            'FolderLocation = System.Configuration.ConfigurationManager.AppSettings("ReportOutputPath")

            'Define the Filename
            Dim strFileName As String = Server.MapPath("~/Documentation/LoadConfirm.PDF") 'System.IO.Path.ChangeExtension(System.IO.Path.GetTempFileName(), "PDF") '

            'First delete existing file
            File.Delete(strFileName)

            'Then create new pdf file
            Dim bytes As Byte() = ReportViewer1.LocalReport.Render("PDF", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            Dim fsFileStream As New FileStream(strFileName, FileMode.Create)
            fsFileStream.Write(bytes, 0, bytes.Length)
            fsFileStream.Close()

            ''Write the File to Browser

            ''Set the appropriate ContentType.
            'Response.ContentType = "Application/pdf"

            ''Write the file directly to the HTTP output stream.
            'Response.WriteFile(strFileName)
            'Response.End()

            'Open the PDF in New Window
            ResponseHelper.Redirect(ResolveUrl("~/Documentation/LoadConfirm.PDF"), "LoadConfirm")

        End Sub

        'Protected Sub imbPrintSelection_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imbPrintSelection.Click
        '    Dim buffer As Byte(), f As String, fs As System.IO.FileStream



        '    f = System.IO.Path.GetTempFileName()

        '    System.IO.Path.ChangeExtension(f, "PDF")

        '    ' there is probably a better way to set up the rendered PDF

        '    ' for redirecting to the Response output, but this one works.



        '    ' here is the binding bit. Revise to suit your dynamic situation.

        '    ' if you aren't really dynamically binding data against one 

        '    ' loaded report, but rather changing

        '    ' reports to suit the user's needs, that will work too.

        '    Dim ReportDataSourceX = New Microsoft.Reporting.WebForms.ReportDataSource()

        '    ReportDataSourceX.Name = "DataSet1_Recipient"

        '    ReportDataSourceX.Value = Me.SqlDataSource1



        '    With Me.ReportViewer1.LocalReport

        '        .DataSources.Clear()

        '        .DataSources.Add(ReportDataSourceX)

        '        buffer = .Render("PDF", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        '    End With



        '    fs = New System.IO.FileStream(f, System.IO.FileMode.Create)

        '    fs.Write(buffer, 0, buffer.Length)

        '    fs.Close()

        '    fs.Dispose()



        '    Response.ContentType = "Application/pdf"

        '    Response.WriteFile(f)

        '    Response.End()

        '    System.IO.File.Delete(f)

        'End Sub

#End Region


    End Class 'rvLoadReport

End Namespace 'bhattji.Modules.LoadSheets
