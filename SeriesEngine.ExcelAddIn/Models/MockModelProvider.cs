using SeriesEngine.Msk1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class MockModelProvider : IModelProvider
    {
        internal static ObjectMetamodel Region = new ObjectMetamodel
        {
            Name = "Region",
        };

        internal static ObjectMetamodel Consumer = new ObjectMetamodel
        {
            Name = "Consumer",
        };

        internal static ObjectMetamodel Contract = new ObjectMetamodel
        {
            Name = "Contract",
            Variables = new List<Variable>
            {
                new Variable
                {
                    Name = "Номер договора"
                },
                new Variable
                {
                    Name = "Дата договора"
                },
                new Variable
                {
                    Name = "Ценовая категория"
                },
            }
        };

        internal static ObjectMetamodel ConsumerObject = new ObjectMetamodel
        {
            Name = "ConsumerObject",
        };

        internal static ObjectMetamodel Point = new ObjectMetamodel
        {
            Name = "Point",
            Variables = new List<Variable>
            {
                new Variable
                {
                    Name = "Наименование"
                },
                new Variable
                {
                    Name = "Уровень напряжения"
                },
                new Variable
                {
                    Name = "Максимальная мощность"
                },
                new Variable
                {
                    Name = "Ценовая категория"
                }
            }
        };

        internal static ObjectMetamodel Device = new ObjectMetamodel
        {
            Name = "Device",
            Variables = new List<Variable>
            {
                new Variable
                {
                    Name = "Тип прибора учета"
                },
                new Variable
                {
                    Name = "Номер счетчика"
                },
                new Variable
                {
                    Name = "Направление перетока"
                },
                new Variable
                {
                    Name = "Показание счетчика"
                },
                new Variable
                {
                    Name = "Коэффициент трансформации"
                },
                new Variable
                {
                    Name = "Потери"
                },
                new Variable
                {
                    Name = "ОДН"
                },
            }
        };

        internal static List<ObjectMetamodel> _metamodels = new List<ObjectMetamodel>
        {
            Region, Consumer, Contract, ConsumerObject, Point, Device,
        };

        public IEnumerable<ObjectMetamodel> GetObjectMetamodels()
        {
            return _metamodels;
        }
    }
}
