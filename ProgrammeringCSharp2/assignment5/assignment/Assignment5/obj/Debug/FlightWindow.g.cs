﻿#pragma checksum "..\..\FlightWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "2138DE9F487B7177C2B86ACB11BFC5B89C3FB8DEEAAC646B818F7800F0267FAF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Assignment5;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Assignment5 {
    
    
    /// <summary>
    /// FlightWindow
    /// </summary>
    public partial class FlightWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\FlightWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image AirloneLogo;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\FlightWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button StartFlightBtn;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\FlightWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox RouteComboBox;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\FlightWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button EndFlightBtn;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Assignment5;component/flightwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\FlightWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.AirloneLogo = ((System.Windows.Controls.Image)(target));
            return;
            case 2:
            this.StartFlightBtn = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\FlightWindow.xaml"
            this.StartFlightBtn.Click += new System.Windows.RoutedEventHandler(this.StartFlightBtn_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.RouteComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 21 "..\..\FlightWindow.xaml"
            this.RouteComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.RouteComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.EndFlightBtn = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\FlightWindow.xaml"
            this.EndFlightBtn.Click += new System.Windows.RoutedEventHandler(this.EndFlightBtn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
