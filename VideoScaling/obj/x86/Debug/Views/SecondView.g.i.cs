﻿#pragma checksum "..\..\..\..\Views\SecondView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "CD09B1158A5F139AFB867159FF88A0DC"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
using VideoScaling.Views;


namespace VideoScaling.Views {
    
    
    /// <summary>
    /// SecondView
    /// </summary>
    public partial class SecondView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\..\Views\SecondView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid SecondGrid;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\..\Views\SecondView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RowDefinition FirstRow;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\..\Views\SecondView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BrowseFileButton;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\Views\SecondView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FilePathTextBox;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\Views\SecondView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CurrentFrame;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\Views\SecondView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ProceedButton;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\Views\SecondView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PreviousFrameButton;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\Views\SecondView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button NextFrameButton;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\Views\SecondView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border FrameBorder;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\Views\SecondView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas RectangleCanvas;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\Views\SecondView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Frame;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\..\Views\SecondView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PreviousVideoButton;
        
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
            System.Uri resourceLocater = new System.Uri("/VideoScaling;component/views/secondview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\SecondView.xaml"
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
            this.SecondGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.FirstRow = ((System.Windows.Controls.RowDefinition)(target));
            return;
            case 3:
            this.BrowseFileButton = ((System.Windows.Controls.Button)(target));
            return;
            case 4:
            this.FilePathTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.CurrentFrame = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.ProceedButton = ((System.Windows.Controls.Button)(target));
            return;
            case 7:
            this.PreviousFrameButton = ((System.Windows.Controls.Button)(target));
            return;
            case 8:
            this.NextFrameButton = ((System.Windows.Controls.Button)(target));
            return;
            case 9:
            this.FrameBorder = ((System.Windows.Controls.Border)(target));
            return;
            case 10:
            this.RectangleCanvas = ((System.Windows.Controls.Canvas)(target));
            return;
            case 11:
            this.Frame = ((System.Windows.Controls.Image)(target));
            return;
            case 12:
            this.PreviousVideoButton = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

