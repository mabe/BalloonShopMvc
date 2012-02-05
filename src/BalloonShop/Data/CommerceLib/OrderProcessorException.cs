using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalloonShop.Data.CommerceLib
{
	class OrderProcessorException : ApplicationException
	{
		private int sourceStage;

		public OrderProcessorException(string message,
		  int exceptionSourceStage)
			: base(message)
		{
			sourceStage = exceptionSourceStage;
		}

		public int SourceStage
		{
			get
			{
				return sourceStage;
			}
		}
	}
}
