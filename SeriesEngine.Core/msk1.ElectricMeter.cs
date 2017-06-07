﻿// <autogenerated>
//   This file was generated by T4 code generator BuildMetamodels.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using SeriesEngine.Core.DataAccess;

namespace SeriesEngine.msk1
{
    [Table("msk1.ElectricMeters")]
	public partial class ElectricMeter : NamedObject
	{
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ElectricMeter() : base(msk1Objects.ElectricMeter)
        {
			ElectricMeter_Directions = new HashSet<ElectricMeter_Direction>();
			ElectricMeter_Integrals = new HashSet<ElectricMeter_Integral>();
			ElectricMeter_CoeffOfTransformations = new HashSet<ElectricMeter_CoeffOfTransformation>();
			ElectricMeter_AdditionInPercents = new HashSet<ElectricMeter_AdditionInPercent>();
			ElectricMeter_Additions = new HashSet<ElectricMeter_Addition>();
			ElectricMeter_Odns = new HashSet<ElectricMeter_Odn>();
		}

        public override string GetName()
        {
            return Name;
        }

        public override void SetName(string newName)
        {
            Name = newName;
        }
	
        [Required]

        public String PUType { get; set; }
	
        [Required]

        public Boolean HourCount { get; set; }
	
        [Required]

        public String Class { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ElectricMeter_Direction> ElectricMeter_Directions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ElectricMeter_Integral> ElectricMeter_Integrals { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ElectricMeter_CoeffOfTransformation> ElectricMeter_CoeffOfTransformations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ElectricMeter_AdditionInPercent> ElectricMeter_AdditionInPercents { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ElectricMeter_Addition> ElectricMeter_Additions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ElectricMeter_Odn> ElectricMeter_Odns { get; set; }

		public static object NameParser(string value) 
		{
			return value;
		}

		public static object PUTypeParser(string value) 
		{
			return value;
		}

		public static object HourCountParser(string value) 
		{
			Boolean newValue;
			if(Boolean.TryParse(value, out newValue)) 
			{
				return newValue;
			}
			throw new Exception("Cannot cast variable");
		}

		public static object ClassParser(string value) 
		{
			return value;
		}

		public static object DirectionParser(string value) 
		{
			return value;
		}

		public static object IntegralParser(string value) 
		{
			value = value.Replace(",", ".");
			Double newValue;
			var style = NumberStyles.Number;
			var culture = CultureInfo.InvariantCulture;
			if(Double.TryParse(value, style, culture, out newValue)) 
			{
				return newValue;
			}
			throw new Exception("Cannot cast variable");
		}

		public static object CoeffOfTransformationParser(string value) 
		{
			value = value.Replace(",", ".");
			Double newValue;
			var style = NumberStyles.Number;
			var culture = CultureInfo.InvariantCulture;
			if(Double.TryParse(value, style, culture, out newValue)) 
			{
				return newValue;
			}
			throw new Exception("Cannot cast variable");
		}

		public static object AdditionInPercentParser(string value) 
		{
			value = value.Replace(",", ".");
			Double newValue;
			var style = NumberStyles.Number;
			var culture = CultureInfo.InvariantCulture;
			if(Double.TryParse(value, style, culture, out newValue)) 
			{
				return newValue;
			}
			throw new Exception("Cannot cast variable");
		}

		public static object AdditionParser(string value) 
		{
			value = value.Replace(",", ".");
			Double newValue;
			var style = NumberStyles.Number;
			var culture = CultureInfo.InvariantCulture;
			if(Double.TryParse(value, style, culture, out newValue)) 
			{
				return newValue;
			}
			throw new Exception("Cannot cast variable");
		}

		public static object OdnParser(string value) 
		{
			value = value.Replace(",", ".");
			Double newValue;
			var style = NumberStyles.Number;
			var culture = CultureInfo.InvariantCulture;
			if(Double.TryParse(value, style, culture, out newValue)) 
			{
				return newValue;
			}
			throw new Exception("Cannot cast variable");
		}
	}
}
