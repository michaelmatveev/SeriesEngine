using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SeriesEngine.Core.DataAccess;
using System.Linq;
using System.Xml.Linq;

namespace SeriesEngine.ExcelAddIn.Views
{
    public partial class StoredQueriesSelector : Form,
        IStoredQueriesView
    {
        public IList<StoredQuery> StoredQueries { get; set; }
        public event EventHandler StoredQueriesUpdated;

        public StoredQueriesSelector()
        {
            InitializeComponent();
        }

        private void ReloadStoredQuieries()
        {
            listViewStoredQueries.Items.Clear();
            var items = StoredQueries
                .Where(s => s.State != ObjectState.Deleted)
                .Select(s => new ListViewItem(new[] { s.Name, s.ModelName })
                {
                    Tag = s,
                })
                .OrderBy(s => s.Name)
                .ToArray();
            listViewStoredQueries.Items.AddRange(items);
        }

        void IStoredQueriesView.ShowIt()
        {
            ReloadStoredQuieries();
            if (ShowDialog() == DialogResult.OK)
            {
                StoredQueriesUpdated?.Invoke(this, EventArgs.Empty);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            var item = listViewStoredQueries.SelectedItems.Cast<ListViewItem>().SingleOrDefault();
            if(item != null)
            {
                (item.Tag as StoredQuery).State = ObjectState.Deleted;
                ReloadStoredQuieries();
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            var form = new XmlProperties();
            if(form.ShowDialog() == DialogResult.OK)
            {
                var text = form.StoredQueryText;
                if (!string.IsNullOrEmpty(text))
                {
                    string name, model;
                    GetName(text, out name, out model);                    
                    if (name != null)
                    {
                        var newStoredQuery = new StoredQuery
                        {
                            State = ObjectState.Added,
                            Text = text,
                            Name = name,
                            ModelName = model

                        };
                        StoredQueries.Add(newStoredQuery);
                        ReloadStoredQuieries();
                    }
                    else
                    {
                        MessageBox.Show("Не задано имя запроса или имя модели");
                    }
                }                
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            var item = listViewStoredQueries.SelectedItems.Cast<ListViewItem>().SingleOrDefault();
            if (item != null)
            {
                var updatedStoredQuery = item.Tag as StoredQuery;
                var form = new XmlProperties
                {
                    StoredQueryText = updatedStoredQuery.Text
                };
                if (form.ShowDialog() == DialogResult.OK)
                {
                    var text = form.StoredQueryText;
                    if (!string.IsNullOrEmpty(text))
                    {
                        string name, model;
                        GetName(text, out name, out model);
                        if (name != null && model != null)
                        {
                            updatedStoredQuery.Name = name;
                            updatedStoredQuery.ModelName = model;
                            updatedStoredQuery.Text = text;
                            if (updatedStoredQuery.State != ObjectState.Added)
                            {
                                updatedStoredQuery.State = ObjectState.Modified;
                            }
                            ReloadStoredQuieries();
                        }
                        else
                        {
                            MessageBox.Show("Не задано имя запроса или имя модели");
                        }
                    }
                }
            }
        }

        private static void GetName(string text, out string name, out string model)
        {
            try
            {
                var xDoc = XDocument.Parse(text);
                name = xDoc.Root.Attribute("Name").Value;
                model = xDoc.Root.Attribute("Model").Value;
            }
            catch(Exception)
            {
                name = null;
                model = null;
            }
        }
    }
}
