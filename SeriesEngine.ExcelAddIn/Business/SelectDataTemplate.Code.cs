using Microsoft.Office.Interop.Excel;
using SeriesEngine.Core;
using SeriesEngine.Core.DataAccess;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Business
{
    public partial class SelectDataTemplate
    {
        private readonly CollectionDataBlock _collectionDataBlock;
        private readonly HierarchyMemamodel _hierarchyModel;
        private readonly int _networkId;
        public SelectDataTemplate(CollectionDataBlock collectionDataBLock, Network network)
        {
            _collectionDataBlock = collectionDataBLock;
            _hierarchyModel = network.HierarchyModel;
            _networkId = network.Id;
          
            _DataReaders = new List<Action<BaseModelContext, DbDataReader>>();
        }

        public string GetCommandText()
        {
            return this.TransformText();

            //return @"

            //DECLARE @Nodes TABLE(
            //Id INT NOT NULL PRIMARY KEY,
            //ParentId INT NULL,
            //NetId INT NOT NULL,
            //ValidFrom DATETIME2(7) NULL,
            //ValidTill DATETIME2 (7) NULL,
            //Tag INT NULL,
            //Region_Id INT NULL,
            //Consumer_Id INT NULL,
            //Contract_Id INT NULL,
            //ConsumerObject_Id INT NULL,
            //Point_Id INT NULL,
            //ElectricMeter_Id INT NULL
            //);

            //INSERT INTO @Nodes
            //EXEC msk1.MainHierarchy_Read2 1, '';

            //SELECT * FROM @Nodes;

            //SELECT * FROM msk1.Point_ContractPriceCategorys AS O
            //INNER JOIN @Nodes AS N
            //ON O.ObjectId = N.Point_Id;

            //SELECT * FROM msk1.Points AS O
            //INNER JOIN @Nodes AS N
            //ON O.Id = N.Point_Id;";
        }

        //public void LoadDataFromReader(BaseModelContext context, DbDataReader reader)
        //{
        //    ((IObjectContextAdapter)context).ObjectContext.Translate(reader, "", System.Data.Entity.Core.Objects.MergeOption.NoTracking);
        //}

        public List<Action<BaseModelContext, DbDataReader>> _DataReaders;
        public IEnumerable<Action<BaseModelContext, DbDataReader>> DataReaders
        {
            get
            {
                return _DataReaders;
            }
        }
    }
}
