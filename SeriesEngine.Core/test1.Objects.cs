﻿// <autogenerated>
//   This file was generated by T4 code generator BuildMetamodels.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using SeriesEngine.Core;
using System.Collections.Generic;

namespace SeriesEngine.test1
{
    public static class test1Objects
    {
        public static IEnumerable<ObjectMetamodel> UsedObjects
        {
            get
            {
                yield return ObjectA;
                yield return ObjectB;
                yield return ObjectC;
            }
        }


        public static ObjectMetamodel ObjectA = new ObjectMetamodel
        {
            Name = "ObjectA",
            Variables = test1ObjectAVariables.AllVariables
        };

        public static ObjectMetamodel ObjectB = new ObjectMetamodel
        {
            Name = "ObjectB",
            Variables = test1ObjectBVariables.AllVariables
        };

        public static ObjectMetamodel ObjectC = new ObjectMetamodel
        {
            Name = "ObjectC",
            Variables = test1ObjectCVariables.AllVariables
        };
    }

}