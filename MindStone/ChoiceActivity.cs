
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using static Android.Widget.AdapterView;

namespace MindStone
{
    [Activity(Label = "@string/app_name")]
    public class ChoiceActivity : Activity
    {
        private Spinner _countrySpinner;
        private Spinner _idTypeSpinner;
        private Button _nextPage;
        private string _selectedCountry;
        private string _selectedIdType;

        private readonly Dictionary<string, Dictionary<string, int>> IDTypeToIDNumberMapping;

        public ChoiceActivity()
        {
            IDTypeToIDNumberMapping = new Dictionary<string, Dictionary<string, int>>();
            IDTypeToIDNumberMapping["India"] = new Dictionary<string, int>
                         { { "PAN", 1 }, { "Passport",2 }, { "Aadhaar",3 } };
            IDTypeToIDNumberMapping["Cameroon"] = new Dictionary<string, int>
                                                         { { "Passport",4 } };
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ChoiceLayout);
            FindViews();
            GetCountryAdapter();
            
            _countrySpinner.ItemSelected +=
                new EventHandler<AdapterView.ItemSelectedEventArgs>
                (Country_ItemSelected);
            _nextPage.Click += _nextPage_Click;
        }



        private void _nextPage_Click(object sender, EventArgs e)
        {
            var documentId = IDTypeToIDNumberMapping[_selectedCountry][_selectedIdType];
            Intent intent = new Intent(this, typeof(ImageActivity));
            intent.PutExtra("documentId", documentId);
            StartActivity(intent);
        }

        private void FindViews()
        {
            _countrySpinner = FindViewById<Spinner>(Resource.Id.CountrySpinner);
            _idTypeSpinner = FindViewById<Spinner>(Resource.Id.IdTypeSpinner);
            _nextPage = FindViewById<Button>(Resource.Id.nextPage);
        }

        private void Country_ItemSelected(object sender,
                                          AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            _selectedCountry = spinner.GetItemAtPosition(e.Position).ToString();
            //string toast = string.Format("Selected country is {0}", _selectedCountry);
            //Toast.MakeText(this, toast, ToastLength.Long).Show();
            PopulateIdSpinner();
        }

        private void Id_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            _selectedIdType = spinner.GetItemAtPosition(e.Position).ToString();
            //string toast = string.Format("Selected ID is {0}", spinner.GetItemAtPosition(e.Position));
            //Toast.MakeText(this, toast, ToastLength.Long).Show();
        }

        private ArrayAdapter GetIdTypeAdapter()
        {
            ArrayAdapter adapterIdType = null;
            if (_selectedCountry == "India")
            {
                adapterIdType = ArrayAdapter.CreateFromResource(
                        this, Resource.Array.id_india,
                        Android.Resource.Layout.SimpleSpinnerItem);
            }
            else
            {
                adapterIdType = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.id_africa,
                    Android.Resource.Layout.SimpleSpinnerItem);
            }
            adapterIdType.SetDropDownViewResource(
                Android.Resource.Layout.SimpleSpinnerDropDownItem);

            return adapterIdType;
        }

        private void GetCountryAdapter()
        {

            var adapterCountry = ArrayAdapter.CreateFromResource(
                                            this, Resource.Array.country_array,
                                            Android.Resource.Layout.SimpleSpinnerItem);
            
            adapterCountry.SetDropDownViewResource(
                Android.Resource.Layout.SimpleSpinnerDropDownItem);
            
            _countrySpinner.Adapter = adapterCountry;
        }

        private void PopulateIdSpinner()
        {            
            _idTypeSpinner.Adapter = GetIdTypeAdapter();
            _idTypeSpinner.ItemSelected +=
                new EventHandler<AdapterView.ItemSelectedEventArgs>(Id_ItemSelected);
        }
       
    }
    
}
