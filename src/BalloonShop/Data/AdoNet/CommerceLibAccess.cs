using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace BalloonShop.Data.AdoNet {
	/// <summary>
	/// Wraps tax data
	/// </summary>
	public struct TaxInfo {
		public int TaxID;
		public string TaxType;
		public double TaxPercentage;
	}

	/// <summary>
	/// Wraps shipping data
	/// </summary>
	public struct ShippingInfo {
		public int ShippingID;
		public string ShippingType;
		public double ShippingCost;
		public int ShippingRegionId;
	}

	public class SecureCard {
	}

	public class MembershipUser {
		public string UserName;
		public string Address1;
		public string Address2;
		public string City;
		public string Region;
		public string PostalCode;
		public string Country;
		public string Email;
	}

	public class ProfileCommon {
		public string UserName;
		public string Address1;
		public string Address2;
		public string City;
		public string Region;
		public string PostalCode;
		public string Country;
	}

	/// <summary>
	/// Wraps order detail data
	/// </summary>
	public class CommerceLibOrderDetailInfo {
		private int orderID;
		private int productID;
		private string productName;
		private int quantity;
		private double unitCost;
		private string itemAsString;

		public int OrderID {
			get {
				return orderID;
			}
			set {
				orderID = value;
			}
		}
		public int ProductID {
			get {
				return productID;
			}
			set {
				productID = value;
			}
		}
		public string ProductName {
			get {
				return productName;
			}
			set {
				productName = value;
			}
		}
		public int Quantity {
			get {
				return quantity;
			}
			set {
				quantity = value;
			}
		}
		public double UnitCost {
			get {
				return unitCost;
			}
			set {
				unitCost = value;
			}
		}
		public string ItemAsString {
			get {
				return itemAsString;
			}
			set {
				itemAsString = value;
			}
		}
		public double Subtotal {
			get {
				return Quantity * UnitCost;
			}
		}

		public CommerceLibOrderDetailInfo(DataRow orderDetailRow) {
			OrderID = Int32.Parse(orderDetailRow["OrderID"].ToString());
			ProductID = Int32.Parse(orderDetailRow["ProductId"].ToString());
			ProductName = orderDetailRow["ProductName"].ToString();
			Quantity = Int32.Parse(orderDetailRow["Quantity"].ToString());
			UnitCost = Double.Parse(orderDetailRow["UnitCost"].ToString());
			// set info property
			Refresh();
		}

		public void Refresh() {
			StringBuilder sb = new StringBuilder();
			sb.Append(Quantity.ToString());
			sb.Append(" ");
			sb.Append(ProductName);
			sb.Append(", $");
			sb.Append(UnitCost.ToString());
			sb.Append(" each, total cost $");
			sb.Append(Subtotal.ToString());
			ItemAsString = sb.ToString();
		}
	}

	/// <summary>
	/// Wraps order data
	/// </summary>
	public class CommerceLibOrderInfo {
		public int orderID;
		public string dateCreated;
		public string dateShipped;
		public string comments;
		public int status;
		public string authCode;
		public string reference;

		public MembershipUser customer;
		public ProfileCommon customerProfile;
		public SecureCard creditCard;

		public double totalCost;
		public string orderAsString;
		public string customerAddressAsString;

		public ShippingInfo shipping;
		public TaxInfo tax;

		public List<CommerceLibOrderDetailInfo> orderDetails;
		private List<CommerceLibAuditInfo> auditTrail;

		public int OrderID {
			get {
				return orderID;
			}
			set {
				orderID = value;
			}
		}
		public string DateCreated {
			get {
				return dateCreated;
			}
			set {
				dateCreated = value;
			}
		}
		public string DateShipped {
			get {
				return dateShipped;
			}
			set {
				dateShipped = value;
			}
		}
		public string Comments {
			get {
				return comments;
			}
			set {
				comments = value;
			}
		}
		public int Status {
			get {
				return status;
			}
			set {
				status = value;
			}
		}
		public string AuthCode {
			get {
				return authCode;
			}
			set {
				authCode = value;
			}
		}
		public string Reference {
			get {
				return reference;
			}
			set {
				reference = value;
			}
		}
		public MembershipUser Customer {
			get {
				return customer;
			}
			set {
				customer = value;
			}
		}
		public ProfileCommon CustomerProfile {
			get {
				return customerProfile;
			}
			set {
				customerProfile = value;
			}
		}
		public SecureCard CreditCard {
			get {
				return creditCard;
			}
			set {
				creditCard = value;
			}
		}
		public double TotalCost {
			get {
				return totalCost;
			}
			set {
				totalCost = value;
			}
		}
		public string OrderAsString {
			get {
				return orderAsString;
			}
			set {
				orderAsString = value;
			}
		}
		public string CustomerAddressAsString {
			get {
				return customerAddressAsString;
			}
			set {
				customerAddressAsString = value;
			}
		}
		public ShippingInfo Shipping {
			get {
				return shipping;
			}
			set {
				shipping = value;
			}
		}
		public TaxInfo Tax {
			get {
				return tax;
			}
			set {
				tax = value;
			}
		}
		public List<CommerceLibOrderDetailInfo> OrderDetails {
			get {
				return orderDetails;
			}
			set {
				orderDetails = value;
			}
		}
		public List<CommerceLibAuditInfo> AuditTrail {
			get {
				if (auditTrail == null) {
					auditTrail = CommerceLibAccess.GetOrderAuditTrail(
					  OrderID.ToString());
				}
				return auditTrail;
			}
		}
		public string StatusAsString {
			get {
				try {
					return CommerceLibAccess.OrderStatuses[Status];
				}
				catch {
					return "Status unknown";
				}
			}
		}
		public string CustomerName {
			get {
				return customer.UserName;
			}
		}
		public CommerceLibOrderInfo(DataRow orderRow) {
			OrderID = Int32.Parse(orderRow["OrderID"].ToString());
			DateCreated = orderRow["DateCreated"].ToString();
			DateShipped = orderRow["DateShipped"].ToString();
			Comments = orderRow["Comments"].ToString();
			Status = Int32.Parse(orderRow["Status"].ToString());
			AuthCode = orderRow["AuthCode"].ToString();
			Reference = orderRow["Reference"].ToString();
			//Customer = Membership.GetUser(
			//  new Guid(orderRow["CustomerID"].ToString()));
			//CustomerProfile =
			//  (HttpContext.Current.Profile as ProfileCommon)
			//    .GetProfile(Customer.UserName);
			//CreditCard = new SecureCard(CustomerProfile.CreditCard);
			OrderDetails = CommerceLibAccess.GetOrderDetails(orderRow["OrderID"].ToString());
			// Get Shipping Data
			if (orderRow["ShippingID"] != DBNull.Value
			  && orderRow["ShippingType"] != DBNull.Value
			  && orderRow["ShippingCost"] != DBNull.Value) {
				shipping.ShippingID = Int32.Parse(orderRow["ShippingID"].ToString());
				shipping.ShippingType = orderRow["ShippingType"].ToString();
				shipping.ShippingCost = double.Parse(orderRow["ShippingCost"].ToString());
			}
			else {
				shipping.ShippingID = -1;
			}
			// Get Tax Data
			if (orderRow["TaxID"] != DBNull.Value
			  && orderRow["TaxType"] != DBNull.Value
			  && orderRow["TaxPercentage"] != DBNull.Value) {
				tax.TaxID = Int32.Parse(orderRow["TaxID"].ToString());
				tax.TaxType = orderRow["TaxType"].ToString();
				tax.TaxPercentage = double.Parse(orderRow["TaxPercentage"].ToString());
			}
			else {
				tax.TaxID = -1;
			}
			// set info properties
			Refresh();
		}

		public void Refresh() {
			// calculate total cost and set data
			StringBuilder sb = new StringBuilder();
			TotalCost = 0.0;
			foreach (CommerceLibOrderDetailInfo item in OrderDetails) {
				sb.AppendLine(item.ItemAsString);
				TotalCost += item.Subtotal;
			}
			// Add shipping cost
			if (Shipping.ShippingID != -1) {
				sb.AppendLine("Shipping: " + Shipping.ShippingType);
				TotalCost += Shipping.ShippingCost;
			}
			// Add tax
			if (Tax.TaxID != -1 && Tax.TaxPercentage != 0.0) {
				double taxAmount = Math.Round(TotalCost * Tax.TaxPercentage, MidpointRounding.AwayFromZero) / 100.0;
				sb.AppendLine("Tax: " + Tax.TaxType + ", $" + taxAmount.ToString());
				TotalCost += taxAmount;
			}
			sb.AppendLine();
			sb.Append("Total order cost: $");
			sb.Append(TotalCost.ToString());
			OrderAsString = sb.ToString();

			// get customer address string
			sb = new StringBuilder();
			sb.AppendLine(Customer.UserName);
			sb.AppendLine(CustomerProfile.Address1);
			if (CustomerProfile.Address2 != "") {
				sb.AppendLine(CustomerProfile.Address2);
			}
			sb.AppendLine(CustomerProfile.City);
			sb.AppendLine(CustomerProfile.Region);
			sb.AppendLine(CustomerProfile.PostalCode);
			sb.AppendLine(CustomerProfile.Country);
			CustomerAddressAsString = sb.ToString();
		}

		public void UpdateStatus(int status) {
			// call static method
			CommerceLibAccess.UpdateOrderStatus(OrderID, status);
			// update field
			Status = status;
		}

		public void SetAuthCodeAndReference(string authCode,
		  string reference) {
			// call static method
			CommerceLibAccess.SetOrderAuthCodeAndReference(OrderID,
			  authCode, reference);
			// update fields
			AuthCode = authCode;
			Reference = reference;
		}

		public void SetDateShipped() {
			// call static method
			CommerceLibAccess.SetOrderDateShipped(OrderID);
			// update field
			DateShipped = DateTime.Now.ToString();
		}
	}

	/// <summary>
	/// Wraps audit trail data
	/// </summary>
	public class CommerceLibAuditInfo {
		#region Private Fields
		private int auditID;
		private int orderID;
		private DateTime dateStamp;
		private string message;
		private int messageNumber;
		#endregion

		#region Public Properties
		public int AuditID {
			get {
				return auditID;
			}
		}
		public int OrderID {
			get {
				return orderID;
			}
		}
		public DateTime DateStamp {
			get {
				return dateStamp;
			}
		}
		public string Message {
			get {
				return message;
			}
		}
		public int MessageNumber {
			get {
				return messageNumber;
			}
		}
		#endregion

		#region Constructor
		public CommerceLibAuditInfo(DataRow auditRow) {
			auditID = (int)auditRow["AuditID"];
			orderID = (int)auditRow["OrderID"];
			dateStamp = (DateTime)auditRow["DateStamp"];
			message = auditRow["Message"] as string;
			messageNumber = (int)auditRow["messageNumber"];
		}
		#endregion
	}

	/// <summary>
	/// Summary description for CommerceLibAccess
	/// </summary>
	public class CommerceLibAccess {
		public static readonly string[] OrderStatuses = 
      {"Order placed, notifying customer", // 0
      "Awaiting confirmation of funds",    // 1
      "Notifying supplier—stock check",    // 2
      "Awaiting stock confirmation",       // 3
      "Awaiting credit card payment",      // 4
      "Notifying supplier—shipping",       // 5
      "Awaiting shipment confirmation",    // 6
      "Sending final notification",        // 7
      "Order completed",                   // 8
      "Order cancelled"};                  // 9

		public CommerceLibAccess() {
			//
			// TODO: Add constructor logic here
			//
		}

		public static List<CommerceLibOrderDetailInfo>
		 GetOrderDetails(string orderId) {
			// use existing method for DataTable
			DataTable orderDetailsData = OrdersAccess.GetDetails(orderId);
			// create List<>
			List<CommerceLibOrderDetailInfo> orderDetails =
			   new List<CommerceLibOrderDetailInfo>(
			   orderDetailsData.Rows.Count);
			foreach (DataRow orderDetail in orderDetailsData.Rows) {
				orderDetails.Add(
				  new CommerceLibOrderDetailInfo(orderDetail));
			}
			return orderDetails;
		}

		public static CommerceLibOrderInfo GetOrder(string orderID) {
			// get a configured DbCommand object
			DbCommand comm = GenericDataAccess.CreateCommand();
			// set the stored procedure name
			comm.CommandText = "CommerceLibOrderGetInfo";
			// create a new parameter
			DbParameter param = comm.CreateParameter();
			param.ParameterName = "@OrderID";
			param.Value = orderID;
			param.DbType = DbType.Int32;
			comm.Parameters.Add(param);
			// obtain the results
			DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
			DataRow orderRow = table.Rows[0];
			// save the results into an CommerceLibOrderInfo object
			CommerceLibOrderInfo orderInfo =
			  new CommerceLibOrderInfo(orderRow);
			return orderInfo;
		}

		public static List<ShippingInfo> GetShippingInfo(int shippingRegionId) {
			// get a configured DbCommand object
			DbCommand comm = GenericDataAccess.CreateCommand();
			// set the stored procedure name
			comm.CommandText = "CommerceLibShippingGetInfo";
			// create a new parameter
			DbParameter param = comm.CreateParameter();
			param.ParameterName = "@ShippingRegionId";
			param.Value = shippingRegionId;
			param.DbType = DbType.Int32;
			comm.Parameters.Add(param);
			// obtain the results
			DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
			List<ShippingInfo> result = new List<ShippingInfo>();
			foreach (DataRow row in table.Rows) {
				ShippingInfo rowData = new ShippingInfo();
				rowData.ShippingID = int.Parse(row["ShippingId"].ToString());
				rowData.ShippingType = row["ShippingType"].ToString();
				rowData.ShippingCost = double.Parse(row["ShippingCost"].ToString());
				rowData.ShippingRegionId = shippingRegionId;
				result.Add(rowData);
			}
			return result;
		}

		public static void CreateAudit(int orderID, string message,
		int messageNumber) {
			// get a configured DbCommand object
			DbCommand comm = GenericDataAccess.CreateCommand();
			// set the stored procedure name
			comm.CommandText = "CreateAudit";
			// create a new parameter
			DbParameter param = comm.CreateParameter();
			param.ParameterName = "@OrderID";
			param.Value = orderID;
			param.DbType = DbType.Int32;
			comm.Parameters.Add(param);
			// create a new parameter
			param = comm.CreateParameter();
			param.ParameterName = "@Message";
			param.Value = message;
			param.DbType = DbType.String;
			param.Size = 512;
			comm.Parameters.Add(param);
			// create a new parameter
			param = comm.CreateParameter();
			param.ParameterName = "@MessageNumber";
			param.Value = messageNumber;
			param.DbType = DbType.Int32;
			comm.Parameters.Add(param);
			// execute the stored procedure
			GenericDataAccess.ExecuteNonQuery(comm);
		}

		public static void UpdateOrderStatus(int orderID, int status) {
			// get a configured DbCommand object
			DbCommand comm = GenericDataAccess.CreateCommand();
			// set the stored procedure name
			comm.CommandText = "CommerceLibOrderUpdateStatus";
			// create a new parameter
			DbParameter param = comm.CreateParameter();
			param.ParameterName = "@OrderID";
			param.Value = orderID;
			param.DbType = DbType.Int32;
			comm.Parameters.Add(param);
			// create a new parameter
			param = comm.CreateParameter();
			param.ParameterName = "@Status";
			param.Value = status;
			param.DbType = DbType.Int32;
			comm.Parameters.Add(param);
			// execute the stored procedure
			GenericDataAccess.ExecuteNonQuery(comm);
		}

		public static void SetOrderAuthCodeAndReference(int orderID,
		string authCode, string reference) {
			// get a configured DbCommand object
			DbCommand comm = GenericDataAccess.CreateCommand();
			// set the stored procedure name
			comm.CommandText = "CommerceLibOrderSetAuthCode";
			// create a new parameter
			DbParameter param = comm.CreateParameter();
			param.ParameterName = "@OrderID";
			param.Value = orderID;
			param.DbType = DbType.Int32;
			comm.Parameters.Add(param);
			// create a new parameter
			param = comm.CreateParameter();
			param.ParameterName = "@AuthCode";
			param.Value = authCode;
			param.DbType = DbType.String;
			param.Size = 50;
			comm.Parameters.Add(param);
			// create a new parameter
			param = comm.CreateParameter();
			param.ParameterName = "@Reference";
			param.Value = reference;
			param.DbType = DbType.String;
			param.Size = 50;
			comm.Parameters.Add(param);
			// execute the stored procedure
			GenericDataAccess.ExecuteNonQuery(comm);
		}

		public static void SetOrderDateShipped(int orderID) {
			// get a configured DbCommand object
			DbCommand comm = GenericDataAccess.CreateCommand();
			// set the stored procedure name
			comm.CommandText = "CommerceLibOrderSetDateShipped";
			// create a new parameter
			DbParameter param = comm.CreateParameter();
			param.ParameterName = "@OrderID";
			param.Value = orderID;
			param.DbType = DbType.Int32;
			comm.Parameters.Add(param);
			// execute the stored procedure
			GenericDataAccess.ExecuteNonQuery(comm);
		}

		public static List<CommerceLibAuditInfo> GetOrderAuditTrail(
		string orderID) {
			// get a configured DbCommand object
			DbCommand comm = GenericDataAccess.CreateCommand();
			// set the stored procedure name
			comm.CommandText = "CommerceLibOrderGetAuditTrail";
			// create a new parameter
			DbParameter param = comm.CreateParameter();
			param.ParameterName = "@OrderID";
			param.Value = orderID;
			param.DbType = DbType.Int32;
			comm.Parameters.Add(param);
			// obtain the results
			DataTable orderAuditTrailData =
			  GenericDataAccess.ExecuteSelectCommand(comm);
			// create List<>
			List<CommerceLibAuditInfo> orderAuditTrail =
			  new List<CommerceLibAuditInfo>(
			  orderAuditTrailData.Rows.Count);
			foreach (DataRow orderAudit in orderAuditTrailData.Rows) {
				orderAuditTrail.Add(new CommerceLibAuditInfo(orderAudit));
			}
			return orderAuditTrail;
		}

		public static List<CommerceLibOrderInfo>
		ConvertDataTableToOrders(DataTable table) {
			List<CommerceLibOrderInfo> orders =
			  new List<CommerceLibOrderInfo>(table.Rows.Count);
			foreach (DataRow orderRow in table.Rows) {
				try {
					// try to add order
					orders.Add(new CommerceLibOrderInfo(orderRow));
				}
				catch {
					// can't add this order
				}
			}
			return orders;
		}

		public static List<CommerceLibOrderInfo> GetOrdersByCustomer(
		string customerID) {
			// get a configured DbCommand object
			DbCommand comm = GenericDataAccess.CreateCommand();
			// set the stored procedure name
			comm.CommandText = "CommerceLibOrdersGetByCustomer";
			// create a new parameter
			DbParameter param = comm.CreateParameter();
			param.ParameterName = "@CustomerID";
			param.Value = new Guid(customerID);
			param.DbType = DbType.Guid;
			comm.Parameters.Add(param);
			// obtain the results
			return ConvertDataTableToOrders(
			  GenericDataAccess.ExecuteSelectCommand(comm));
		}

		public static List<CommerceLibOrderInfo> GetOrdersByDate(
		string startDate, string endDate) {
			// get a configured DbCommand object
			DbCommand comm = GenericDataAccess.CreateCommand();
			// set the stored procedure name
			comm.CommandText = "CommerceLibOrdersGetByDate";
			// create a new parameter
			DbParameter param = comm.CreateParameter();
			param.ParameterName = "@StartDate";
			param.Value = startDate;
			param.DbType = DbType.Date;
			comm.Parameters.Add(param);
			// create a new parameter
			param = comm.CreateParameter();
			param.ParameterName = "@EndDate";
			param.Value = endDate;
			param.DbType = DbType.Date;
			comm.Parameters.Add(param);
			// obtain the results
			return ConvertDataTableToOrders(
			  GenericDataAccess.ExecuteSelectCommand(comm));
		}

		public static List<CommerceLibOrderInfo> GetOrdersByRecent(
		int count) {
			// get a configured DbCommand object
			DbCommand comm = GenericDataAccess.CreateCommand();
			// set the stored procedure name
			comm.CommandText = "CommerceLibOrdersGetByRecent";
			// create a new parameter
			DbParameter param = comm.CreateParameter();
			param.ParameterName = "@Count";
			param.Value = count;
			param.DbType = DbType.Int32;
			comm.Parameters.Add(param);
			// obtain the results
			return ConvertDataTableToOrders(
			  GenericDataAccess.ExecuteSelectCommand(comm));
		}

		public static List<CommerceLibOrderInfo> GetOrdersByStatus(
		int status) {
			// get a configured DbCommand object
			DbCommand comm = GenericDataAccess.CreateCommand();
			// set the stored procedure name
			comm.CommandText = "CommerceLibOrdersGetByStatus";
			// create a new parameter
			DbParameter param = comm.CreateParameter();
			param.ParameterName = "@Status";
			param.Value = status;
			param.DbType = DbType.Int32;
			comm.Parameters.Add(param);
			// obtain the results
			return ConvertDataTableToOrders(
			  GenericDataAccess.ExecuteSelectCommand(comm));
		}

		public static void UpdateOrder(int orderID,
		  string newDateCreated, string newDateShipped,
		  int newStatus, string newAuthCode, string newReference,
		  string newComments) {
			// get a configured DbCommand object
			DbCommand comm = GenericDataAccess.CreateCommand();
			// set the stored procedure name
			comm.CommandText = "CommerceLibOrderUpdate";
			// create a new parameter
			DbParameter param = comm.CreateParameter();
			param.ParameterName = "@OrderID";
			param.Value = orderID;
			param.DbType = DbType.Int32;
			comm.Parameters.Add(param);
			// create a new parameter
			param = comm.CreateParameter();
			param.ParameterName = "@DateCreated";
			param.Value = DateTime.Parse(newDateCreated);
			param.DbType = DbType.DateTime;
			comm.Parameters.Add(param);
			// The DateShipped parameter is sent only if data is available
			if (newDateShipped != null) {
				param = comm.CreateParameter();
				param.ParameterName = "@DateShipped";
				param.Value = DateTime.Parse(newDateShipped);
				param.DbType = DbType.DateTime;
				comm.Parameters.Add(param);
			}
			// create a new parameter
			param = comm.CreateParameter();
			param.ParameterName = "@Status";
			param.Value = newStatus;
			param.DbType = DbType.Int32;
			comm.Parameters.Add(param);
			// create a new parameter
			param = comm.CreateParameter();
			param.ParameterName = "@AuthCode";
			param.Value = newAuthCode;
			param.DbType = DbType.String;
			comm.Parameters.Add(param);
			// create a new parameter
			param = comm.CreateParameter();
			param.ParameterName = "@Reference";
			param.Value = newReference;
			param.DbType = DbType.String;
			comm.Parameters.Add(param);
			// create a new parameter
			param = comm.CreateParameter();
			param.ParameterName = "@Comments";
			param.Value = newComments;
			param.DbType = DbType.String;
			comm.Parameters.Add(param);
			// update the order
			GenericDataAccess.ExecuteNonQuery(comm);
		}
	}
}
