﻿#pragma checksum "C:\Users\Batuhan\Documents\StartsHere.Source-v1.0.0.0\StartsHere.W10\Pages\ShellPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7C63D710792D1FC2CF849208F9FC57CD"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StartsHere.Pages
{
    partial class ShellPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.PageLayout = (global::Windows.UI.Xaml.Controls.Page)(target);
                    #line 11 "..\..\..\Pages\ShellPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Page)this.PageLayout).KeyUp += this.OnKeyUp;
                    #line default
                }
                break;
            case 2:
                {
                    this.shell = (global::AppStudio.Uwp.Controls.ShellControl)(target);
                }
                break;
            case 3:
                {
                    this.frame = (global::Windows.UI.Xaml.Controls.Frame)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}
