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
            Name = "Регион",
        };

        internal static ObjectMetamodel Consumer = new ObjectMetamodel
        {
            Name = "Потребитель",
        };

        internal static ObjectMetamodel Contract = new ObjectMetamodel
        {
            Name = "Договор",
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

        internal static ObjectMetamodel Point = new ObjectMetamodel
        {
            Name = "Точка поставки",
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
            Name = "Прибор учета",
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
            Region, Consumer, Contract, Point, Device,
        };

        public IEnumerable<ObjectMetamodel> GetObjectMetamodels()
        {
            return _metamodels;
        }
    }
}
