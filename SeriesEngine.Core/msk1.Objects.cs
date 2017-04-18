﻿// <autogenerated>
//   This file was generated by T4 code generator BuildMetamodels.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using SeriesEngine.Core;
using System.Collections.Generic;

namespace SeriesEngine.msk1
{
    public static class msk1Objects
    {
        public static IEnumerable<ObjectMetamodel> UsedObjects
        {
            get
            {
                yield return Region;
                yield return Consumer;
                yield return Contract;
                yield return ConsumerObject;
                yield return Point;
                yield return ElectricMeter;
            }
        }

        public static ObjectMetamodel Region = new ObjectMetamodel
        {
            Name = "Region",
            Variables = msk1RegionVariables.AllVariables,
			ObjectType = typeof(Region)
        };
        public static ObjectMetamodel Consumer = new ObjectMetamodel
        {
            Name = "Consumer",
            Variables = msk1ConsumerVariables.AllVariables,
			ObjectType = typeof(Consumer)
        };
        public static ObjectMetamodel Contract = new ObjectMetamodel
        {
            Name = "Contract",
            Variables = msk1ContractVariables.AllVariables,
			ObjectType = typeof(Contract)
        };
        public static ObjectMetamodel ConsumerObject = new ObjectMetamodel
        {
            Name = "ConsumerObject",
            Variables = msk1ConsumerObjectVariables.AllVariables,
			ObjectType = typeof(ConsumerObject)
        };
        public static ObjectMetamodel Point = new ObjectMetamodel
        {
            Name = "Point",
            Variables = msk1PointVariables.AllVariables,
			ObjectType = typeof(Point)
        };
        public static ObjectMetamodel ElectricMeter = new ObjectMetamodel
        {
            Name = "ElectricMeter",
            Variables = msk1ElectricMeterVariables.AllVariables,
			ObjectType = typeof(ElectricMeter)
        };
    }

}
