using System;
using System.Collections.Generic;

namespace TweakBot
{    
	class Connection
	{
		private Region regionFrom;
		private Region regionTo;
    
		public Connection(Region regionFrom, Region regionTo)
		{
			this.regionFrom = regionFrom;
			this.regionTo = regionTo;
		}
	}
}
