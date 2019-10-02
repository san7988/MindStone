
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MindStone.Common;

namespace MindStone
{
    [Activity(Label = "@string/app_name")]
    public class ShowResultsActivity : Activity
    {
        private ImageView _resultImageView;
        private LinearLayout _parentLinearLayout;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ShowResultLayout);
            FindViews();
            GetDataFromPrevActivity();

        }

        private void FindViews()
        {
            _resultImageView = FindViewById<ImageView>(Resource.Id.resultImageView);
            _parentLinearLayout = FindViewById<LinearLayout>(Resource.Id.parent_linear_layout);
        }

        private void GetDataFromPrevActivity()
        {
            var imageSource = Intent.Extras.Get("image_source").ToString();
            if (imageSource.Equals("camera"))
            {
                byte[] imageBytes = Intent.Extras.GetByteArray("image_data"); 
                Bitmap bitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                _resultImageView.SetImageBitmap(bitmap);
            }
            else
            {
                var data = Intent.Extras.Get("image_data").ToString();
                _resultImageView.SetImageURI(Android.Net.Uri.Parse(data));
            }
            
            UpdateViewFromDummyResultFromAPI();
        }

        private void UpdateViewFromDummyResultFromAPI()
        {
            var apiCaller = new APICaller();
            var extractedFieldList = apiCaller.CallApi()
                                              .GetAwaiter()
                                              .GetResult()
                                              .Extraction;
            foreach(var e in extractedFieldList)
            {
                LayoutInflater inflater = (LayoutInflater)GetSystemService(Context.LayoutInflaterService);
                View rowView = inflater.Inflate(Resource.Layout.ExtractedDetailLayout, null);
                var extractKey = rowView.FindViewById<TextView>(Resource.Id.extractKey);
                extractKey.Text = e.FieldName;
                var extractValue = rowView.FindViewById<TextView>(Resource.Id.extractValue);
                extractValue.Text = e.FieldValue;
                _parentLinearLayout.AddView(rowView);
            }
        }
    }
}
