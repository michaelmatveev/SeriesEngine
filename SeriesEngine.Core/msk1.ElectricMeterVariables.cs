﻿// <autogenerated>
//   This file was generated by T4 code generator BuildMetamodels.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using SeriesEngine.Core;
using System.Collections.Generic;

namespace SeriesEngine.msk1
{
	public class msk1ElectricMeterVariables
	{
        public static IEnumerable<Variable> AllVariables
        {
            get
            {
				yield return Name;
				yield return PUType;
            }
        }

		public static Variable Name = new Variable
        {
			Name = "Name",
		};
		public static Variable PUType = new Variable
        {
			Name = "PUType",
		};
	}
}
