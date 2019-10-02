
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util;

namespace MindStone
{
    [Activity(Label = "@string/app_name")]
    public class ImageActivity : Activity
    {
        public static readonly int PickImageId = 1000;
        public static readonly int CameraImageId = 1001;
        private ImageView _imageView;
        private Button _cameraButton;
        private Button _fileSelectorButton;
        private Button _nextPage;
        private Android.Net.Uri _capturedImageURI;
        private byte[] _imageAsByteArray;
        private string _source;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ImageLayout);
            FindViews();
            GetDataFromPrevActivity();
            RegisterOnClickEvents();
        }

        private void GetDataFromPrevActivity()
        {
            var data = Intent.Extras.Get("documentId").ToString();
            Toast.MakeText(this, data, ToastLength.Long).Show();
        }

        private void RegisterOnClickEvents()
        {
            _cameraButton.Click += CameraButtonOnClick;
            _fileSelectorButton.Click += FilePickerButtonOnClick;
            _nextPage.Click += _nextPage_Click;
        }

        private void _nextPage_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ShowResultsActivity));
            intent.PutExtra("image_source", _source);
            if (_source.Equals("camera"))
            {
                intent.PutExtra("image_data", _imageAsByteArray);
            }
            else
            {
                intent.PutExtra("image_data", _capturedImageURI);
            }

            StartActivity(intent);
        }

        private void FindViews()
        {
            _cameraButton = FindViewById<Button>(Resource.Id.OpenCamera);
            _fileSelectorButton = FindViewById<Button>(Resource.Id.PickFile);
            _imageView = FindViewById<ImageView>(Resource.Id.imageView1);
            _nextPage = FindViewById<Button>(Resource.Id.nextPage2);
        }

        private void CameraButtonOnClick(object sender, EventArgs eventArgs)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(intent, CameraImageId);
        }

        private void FilePickerButtonOnClick(object sender, EventArgs eventArgs)
        {
            Intent intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);
            StartActivityForResult(Intent.CreateChooser(intent, "Select Picture"), PickImageId);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if ((resultCode != Result.Ok) || (data == null))
            {
                string toast = "Could not display the image.";
                Toast.MakeText(this, toast, ToastLength.Long).Show();
                return;
            }
            _capturedImageURI = data.Data;
            if (requestCode == CameraImageId)
            {
                _source = "camera";
                Bitmap bitMap = (Bitmap)data.Extras.Get("data");
                //byte[] bitmapData;
                using (var stream = new MemoryStream())
                {
                    bitMap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                    _imageAsByteArray = stream.ToArray();
                }
                _imageView.SetImageBitmap(bitMap);
            }
            else if (requestCode == PickImageId)
            {
                _source = "filepicker";
               Android.Net.Uri uri = data.Data;
              _imageView.SetImageURI(uri);
            }
        }
    }
}
