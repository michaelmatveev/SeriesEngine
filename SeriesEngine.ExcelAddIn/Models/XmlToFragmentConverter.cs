﻿using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using System;
using System.Xml.Linq;

namespace SeriesEngine.ExcelAddIn.Models
{
    public static class XmlToDataBlockConverter
    {
        public static BaseDataBlock GetDataBlock(XDocument source, Period defaultPeriod)
        {
            var result = new CollectionDataBlock();
            result.CustomPeriod = defaultPeriod;
            result.Name = source.Root.Attribute("Name").Value;
            result.Sheet = source.Root.Attribute("Sheet").Value;
            result.Cell = source.Root.Attribute("Cell").Value;

            foreach (var f in source.Root.Descendants())
            {
                DataBlock newFragment;
                if(f.Name.LocalName == "CFragment")
                {
                    newFragment = new NodeDataBlock(result)
                    {
                        NodeType = (NodeType)Enum.Parse(typeof(NodeType), f.Attribute("Type").Value)
                    };
                }
                else
                {
                    newFragment = new VariableDataBlock(result)
                    {
                        Kind = (Kind)Enum.Parse(typeof(Kind), f.Attribute("Kind").Value),
                        VariableName = f.Attribute("Variable").Value
                    };
                }

                newFragment.Caption = f.Attribute("Caption").Value;
                newFragment.Level = Int32.Parse(f.Attribute("Level").Value);
                newFragment.CollectionName = f.Attribute("CollectionName").Value;
                newFragment.RefObject = f.Attribute("RefObject").Value;

                result.DataBlocks.Add(newFragment);
            }
            return result;
        }
    }
}
