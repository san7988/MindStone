
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MindStone
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/SocGen_logo", Theme ="@style/AppTheme")]
    public class LandingPageActivity : Activity
    {
        Button _startExtraction;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LandingPage);
            FindViews();
            LinkEventHandlers();
        }

        private void FindViews()
        {
            _startExtraction = FindViewById<Button>(Resource.Id.StartButton);
        }

        private void LinkEventHandlers()
        {
            _startExtraction.Click += _startExtraction_Click;
        }

        private void _startExtraction_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ChoiceActivity));
            StartActivity(intent);
        }
    }
}
