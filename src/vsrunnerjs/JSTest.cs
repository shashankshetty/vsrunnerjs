using System;

namespace vsrunnerjs
{
	public class JSTest
	{
		public JSTest()
		{
			Passed = true;
		}

		public string FileName { get; set; }
		public string Message { get; set; }
		public bool Passed { get; set; }
		public string Test { get; set; }

		public override string ToString()
		{
			return String.Format("{0} >>> {1}", FileName, Test);
		}
	}
}