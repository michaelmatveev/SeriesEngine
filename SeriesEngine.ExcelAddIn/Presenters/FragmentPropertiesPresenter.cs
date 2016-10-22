﻿using SeriesEngine.ExcelAddIn.Views;
using SeriesEngine.ExcelAddIn.Models;
using SeriesEngine.ExcelAddIn.Models.Fragments;

namespace SeriesEngine.ExcelAddIn.Presenters
{
    public class FragmentPropertiesPresenter : Presenter<IFragmentPropertiesView>
    {
        public FragmentPropertiesPresenter(IFragmentPropertiesView view, IController controller) : base(view, controller)
        {
            View.FragmentChanged += (s, e) =>
            {
                Controller.GetInstance<IFragmentsProvider>().AddFragment(View.Fragment);
            };
        }

        public void EditFragment(DataFragment fragment)
        {
            View.Fragment = fragment;
            View.ShowIt();
        }

    }
}
