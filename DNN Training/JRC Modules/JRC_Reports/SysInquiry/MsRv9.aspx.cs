using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.Reporting.WebForms;

public partial class MsRv9 : System.Web.UI.Page
{
    private DataTable GetDataTable(string strCmd)
    {
        string strConn = ConfigurationManager.ConnectionStrings["WDCConnectionString"].ConnectionString;
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(strCmd, strConn);
        da.Fill(dt);
        return dt;
    }
    private void RunReport()
    {
        // Set the processing mode for the ReportViewer to Local
        ReportViewer1.ProcessingMode = ProcessingMode.Local;
        ReportViewer1.ShowPrintButton = true;

        //LocalReport LocalReport1 = ReportViewer1.LocalReport;

        //LocalReport1.ReportPath = "Report1.rdlc";//"Sales Order Detail.rdlc";

        //ReportViewer1.LocalReport.ReportPath = "Report1.rdlc";
        ReportViewer1.LocalReport.ReportPath = "Reports/SysInquiry.rdlc";
        
        //DataSet DataSet1 = new DataSet("DataSet1");//("Sales Order Detail");
        string Status = CheckBox1.Checked ? "Y" : "N";
        string NoOfItems = TextBox1.Text;
        string strCmd = "SELECT ";
        if (TextBox1.Text != "0") strCmd += "TOP (" + TextBox1.Text + " ) ";
        strCmd += "x.ItemID,x.JRCIOfficeCode,x.LoadType,x.LoadID,x.LoadDate,x.LoadStatus,x.XMissionFile,cn.CustomerName,dm.DispatchName,x.CustPO,io.IOName,x.PUCityST,x.DPCityST,sp.DriverName,bk.BrokerName,la.BCustBill,la.BBaseLoad,la.RepDlr,la.DRTotDue,la.IOCommTot,la.IOAdminTot,la.ExTot,la.APComm4,la.JRCOffComm,la.JRCOnePct,la.JRCTotal,la.GPPct "
                + "FROM tblOOLoadSheetHeader AS x "
                + "LEFT OUTER JOIN tblLoadAcct AS la ON x.LoadID = la.LoadID "
                + "LEFT OUTER JOIN AR1_CustomerMaster AS cn ON x.CustomerNumber = cn.CustomerNumber "
                + "LEFT OUTER JOIN ARD_DispatchMasterfile AS dm ON x.DispatchCode = dm.DispatchCode  "
                + "LEFT OUTER JOIN ARD_SalesPersonMasterfile AS sp ON x.DriverCode = sp.DriverCode "
                + "LEFT OUTER JOIN ARD_BrokerMaster bk ON x.BrokerCode = bk.BrokerCode "
                + "LEFT OUTER JOIN ARD_InterOffice as io ON x.JRCIOCode = io.JRCIOfficeCode   ";

        //FillDataSet(strCmd,ref DataSet1);//GetSalesOrderData(salesOrderNumber, ref dataset);

        //ReportDataSource ReportDataSource1 = new ReportDataSource();
        //ReportDataSource1.Name = "DataSet1_DataTable1";
        //ReportDataSource1.Value = GetDataTable(strCmd);//DataSet1.Tables[0];
        ReportDataSource ReportDataSource1 = new ReportDataSource("DataSet1_DataTable1", GetDataTable(strCmd));
        //ReportDataSource ReportDataSource1 = new ReportDataSource("DataTable1", GetDataTable(strCmd));
        //ReportDataSource ReportDataSource2 = new ReportDataSource("DataSet1_tblDriverStatus", GetDataTable(strCmd));
        ReportViewer1.LocalReport.DataSources.Clear();
        //LocalReport1.DataSources.Add(ReportDataSource1);
        ReportViewer1.LocalReport.DataSources.Add(ReportDataSource1);
        //ReportViewer1.LocalReport.DataSources.Add(ReportDataSource2);

        // Create the prmCopyright ReportParameter
        ReportParameter prmCopyright = new ReportParameter("prmCopyright","Copyrighted by Jaydeep Bhatt");
        //prmCopyright.Name = "prmCopyright";
        //prmCopyright.Values.Add("Copyrighted by Jaydeep Bhatt");
        // Create the prmCopyright ReportParameter
        ReportParameter prmCompanyName = new ReportParameter("prmCompanyName", "Vision Consultants");

        // Set the report parameters for the report
        ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { prmCopyright, prmCompanyName });
        //ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { prmCopyright});
        //ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { prmCompanyName });
        
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {

        //if (CheckBox1.Checked)
        //    RunReport("Y");
        //else
        //    RunReport("N");
        RunReport();

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        RunReport();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) RunReport();
    }
}
