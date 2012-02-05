using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalloonShop.Data.CommerceLib
{
	interface IPipelineSection
	{
		void Process(OrderProcessor processor);
	}
}
