using Android.App;
using Android.Widget;
using Android.OS;
using Android.Provider;
using Android.Content;
using System.Collections.Generic;
using Android.Database;
using Android.Util;
using Android.Net;
using System;

namespace ContactTest
{
	[Activity(Label = "ContactTest", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button>(Resource.Id.myButton);

			button.Click += delegate { button.Text = $"{count++} clicks!"; FillContacts(); };

			FillContacts();
		}

		void FillContacts()
		{
			var uri = ContactsContract.Contacts.ContentUri;
			string[] projection = {
				ContactsContract.Contacts.InterfaceConsts.Id,
				ContactsContract.Contacts.InterfaceConsts.DisplayName,
				ContactsContract.Contacts.InterfaceConsts.PhotoUri
		 	};
			// CursorLoader introduced in Honeycomb (3.0, API11)
			var loader = new CursorLoader(this, uri, projection, null, null, null);
			var cursor = (ICursor)loader.LoadInBackground();

			var contactImage = FindViewById<ImageView>(Resource.Id.imageView1);


			if (cursor.MoveToFirst())
			{
				do
				{
					Console.WriteLine($"Id = {cursor.GetLong(cursor.GetColumnIndex(projection[0]))}");
					Console.WriteLine($"DisplayName = {cursor.GetString(cursor.GetColumnIndex(projection[1]))}");
					Console.WriteLine($"PhotoId = {cursor.GetString(cursor.GetColumnIndex(projection[2]))}");


					if (cursor.GetString(cursor.GetColumnIndex(projection[2])) != null)
					{
						Android.Net.Uri urireal = Android.Net.Uri.Parse(cursor.GetString(cursor.GetColumnIndex(projection[2])));
					
						contactImage.SetImageURI(urireal);
					}
					
				} while (cursor.MoveToNext());
			}
		}
	}
}

