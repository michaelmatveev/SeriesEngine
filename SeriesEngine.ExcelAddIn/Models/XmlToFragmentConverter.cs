using SeriesEngine.ExcelAddIn.Models.Fragments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SeriesEngine.ExcelAddIn.Models
{
    public static class XmlToFragmentConverter
    {
        public static BaseFragment GetFragment(XDocument source, Period defaultPeriod)
        {
            var result = new ObjectGridFragment();
            result.CustomPeriod = defaultPeriod;
            result.Name = source.Root.Attribute("Name").Value;
            result.Sheet = source.Root.Attribute("Sheet").Value;
            result.Cell = source.Root.Attribute("Cell").Value;

            foreach (var f in source.Root.Descendants())
            {
                SubFragment newFragment;
                if(f.Name.LocalName == "CFragment")
                {
                    newFragment = new NodeSubFragment
                    {
                        NodeType = (NodeType)Enum.Parse(typeof(NodeType), f.Attribute("Type").Value)
                    };
                }
                else
                {
                    newFragment = new VariableSubFragment
                    {
                        Kind = (Kind)Enum.Parse(typeof(Kind), f.Attribute("Kind").Value),
                        VariableName = f.Attribute("Variable").Value
                    };
                }

                newFragment.Caption = f.Attribute("Caption").Value;
                newFragment.Level = Int32.Parse(f.Attribute("Level").Value);
                newFragment.CollectionName = f.Attribute("CollectionName").Value;
                newFragment.RefObject = f.Attribute("RefObject").Value;

                result.SubFragments.Add(newFragment);
            }
            return result;
        }
    }
}
