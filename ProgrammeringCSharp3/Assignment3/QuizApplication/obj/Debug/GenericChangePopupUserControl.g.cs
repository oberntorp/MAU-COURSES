﻿#pragma checksum "..\..\GenericChangePopupUserControl.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "96AFFC8CB225EFF9E27B3DC8B9C747E62A0E9DFFC10B9FBAC740DD48D288C00B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using QuizApplication;
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


namespace QuizApplication {
    
    
    /// <summary>
    /// GenericChangePopupUserControl
    /// </summary>
    public partial class GenericChangePopupUserControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\GenericChangePopupUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ChangedItemNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\GenericChangePopupUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label ChangedItemDescriptionLabel;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\GenericChangePopupUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ChangedItemDescriptionTextBox;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\GenericChangePopupUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label ChangedItemRightAnswerLabel;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\GenericChangePopupUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox ChangedItemRightAnswerCheckBox;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\GenericChangePopupUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label WarningLabel;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\GenericChangePopupUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveButton;
        
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
            System.Uri resourceLocater = new System.Uri("/QuizApplication;component/genericchangepopupusercontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\GenericChangePopupUserControl.xaml"
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
            this.ChangedItemNameTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.ChangedItemDescriptionLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.ChangedItemDescriptionTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.ChangedItemRightAnswerLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.ChangedItemRightAnswerCheckBox = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 6:
            this.WarningLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.SaveButton = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\GenericChangePopupUserControl.xaml"
            this.SaveButton.Click += new System.Windows.RoutedEventHandler(this.SaveButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

