﻿using SeriesEngine.ExcelAddIn.Models.Fragments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Models
{
    public abstract class BaseDataExporter
    {
        protected void ExportFromFragments(IEnumerable<SheetFragment> fragments)
        {
            foreach (var f in fragments)
            {
                f.Export(this);
            }
        }

        public abstract void ExportFragment(ObjectGridFragment fragment);

        //protected virtual void ExportGridFragment(ObjectGridFragment fragment)
        //{
        //    throw new NotImplementedException();
        //}

        //private void ExportNodeFragment(NodeFragment fragment)
        //{
        //    Excel.Worksheet sheet = _workbook.Sheets[fragment.Sheet];

        //    MockNetworkProvider netwrokProvider = new MockNetworkProvider();
        //    var network = netwrokProvider.GetNetworks(string.Empty).FirstOrDefault() as NetworkTree;
        //    int i = 1;
        //    var objectName = sheet.get_Range(fragment.Cell).Offset[i, 0].Value2;
        //    while(objectName != null)
        //    {
        //        if (!network.Nodes
        //            .Where(n => n.LinkedObject.ObjectModel == fragment.Model)
        //            .Any(n => n.NodeName == objectName))
        //        {
        //            MockNetworkProvider.mainTree.Nodes.Add(new NetworkTreeNode
        //            {
        //                Parent = null,
        //                LinkedObject = new ManagedObject()
        //                {
        //                    Name = objectName,
        //                    ObjectModel = fragment.Model
        //                }
        //            });
        //        }
        //        objectName = sheet.get_Range(fragment.Cell).Offset[i++, 0].Value2;
        //    }
        //}

    }

}
