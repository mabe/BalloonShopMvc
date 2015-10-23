using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.UserTypes;
using NHibernate;
using System.Data;
using System.IO;
using System.Security.Cryptography;

namespace BalloonShop.Model
{
	public class CreditCard
	{
		public virtual string CardholderName { get; set; }

		public virtual string CardType { get; set; }

		public virtual string CardNumber { get; set; }

		public virtual string IssueDate { get; set; }

		public virtual string ExpiryDate { get; set; }

		public virtual string IssueNumber { get; set; }

		public CreditCard SetData(string value) {
			var values = Decrypt(value).Split(';');

			CardholderName = values[0];
			CardType = values[1];
			CardNumber = values[2];
			IssueDate = values[3];
			ExpiryDate = values[4];
			IssueNumber = values[5];

			return this;
		}

		public string GetData() {
			return Encrypt(string.Concat(CardholderName, ";", CardType, ";", CardNumber, ";", IssueDate, ";", ExpiryDate, ";", IssueNumber, ";"));
		}

		private string Encrypt(string sourceData)
		{
			// set key and initialization vector values
			byte[] key = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
			byte[] iv = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
			try
			{
				// convert data to byte array
				byte[] sourceDataBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(sourceData);

				// get target memory stream
				var tempStream = new MemoryStream();

				// get encryptor and encryption stream
				var encryptor = new DESCryptoServiceProvider();
				var encryptionStream = new CryptoStream(tempStream, encryptor.CreateEncryptor(key, iv), CryptoStreamMode.Write);

				// encrypt data
				encryptionStream.Write(sourceDataBytes, 0, sourceDataBytes.Length);
				encryptionStream.FlushFinalBlock();

				// put data into byte array
				byte[] encryptedDataBytes = tempStream.GetBuffer();

				// convert encrypted data into string
				return Convert.ToBase64String(encryptedDataBytes, 0, (int)tempStream.Length);
			}
			catch
			{
				throw new Exception("Unable to encrypt data.");
			}
		}

		private string Decrypt(string sourceData)
		{
			// set key and initialization vector values
			byte[] key = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
			byte[] iv = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
			try
			{
				// convert data to byte array
				byte[] encryptedDataBytes = Convert.FromBase64String(sourceData);

				// get source memory stream and fill it 
				var tempStream = new MemoryStream(encryptedDataBytes, 0, encryptedDataBytes.Length);

				// get decryptor and decryption stream 
				var decryptor = new DESCryptoServiceProvider();
				var decryptionStream = new CryptoStream(tempStream, decryptor.CreateDecryptor(key, iv), CryptoStreamMode.Read);

				// decrypt data 
				return new StreamReader(decryptionStream).ReadToEnd();
			}
			catch
			{
				throw new Exception("Unable to decrypt data.");
			}
		}
	}
	/*
	public class CreditCardMapper : IUserType
	{

		public object Assemble(object cached, object owner)
		{
			return DeepCopy(cached);
		}

		public object DeepCopy(object value)
		{
			return value;
		}

		public object Disassemble(object value)
		{
			return DeepCopy(value);
		}

		public bool Equals(object x, object y)
		{
			if (object.ReferenceEquals(x, y)) return true;
			if (x == null || y == null) return false;
			return x.Equals(y);
		}

		public int GetHashCode(object x)
		{
			return x.GetHashCode();
		}

		public bool IsMutable
		{
			get { return false; }
		}

		public object NullSafeGet(System.Data.IDataReader rs, string[] names, object owner)
		{
			object obj = NHibernateUtil.String.NullSafeGet(rs, names[0]);
			if (obj == null) return null;
			return new CreditCard().SetData((string)obj);
		}

		public void NullSafeSet(System.Data.IDbCommand cmd, object value, int index)
		{
			var parameter = (IDataParameter)cmd.Parameters[index];
			parameter.Value = (value == null) ? (object)DBNull.Value : ((CreditCard)value).GetData();
		}

		public object Replace(object original, object target, object owner)
		{
			return original;
		}

		public Type ReturnedType
		{
			get { return typeof(CreditCard); }
		}

		public NHibernate.SqlTypes.SqlType[] SqlTypes
		{
			get { return new NHibernate.SqlTypes.SqlType[] { NHibernateUtil.String.SqlType }; }
		}
	}
	*/
}
