using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace media_multi
{
	public class ArgBundle
	{
		public ConversionType TypeOfConversion { get; set; }
		public string SourceExtension { get; set; }
		public string TargetExtension { get; set; }
	}
}