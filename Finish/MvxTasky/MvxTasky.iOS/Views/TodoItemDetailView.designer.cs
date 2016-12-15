// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace MvxTasky.iOS.Views
{
    [Register ("TodoItemDetailView")]
    partial class TodoItemDetailView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton deleteButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISwitch done { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel id { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField name { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField notes { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton saveButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (deleteButton != null) {
                deleteButton.Dispose ();
                deleteButton = null;
            }

            if (done != null) {
                done.Dispose ();
                done = null;
            }

            if (id != null) {
                id.Dispose ();
                id = null;
            }

            if (name != null) {
                name.Dispose ();
                name = null;
            }

            if (notes != null) {
                notes.Dispose ();
                notes = null;
            }

            if (saveButton != null) {
                saveButton.Dispose ();
                saveButton = null;
            }
        }
    }
}