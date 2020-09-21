using MultiMediaApplication.UserControls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace MultiMediaApplication.UserControls.DependencyProperties
{
    public static class MediaViewSelectionDependencyProperties
    {
        // Using a DependencyProperty as the backing store for MediaId.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MediaIdProperty =
            DependencyProperty.Register("MediaId", typeof(int), typeof(MediaViewSelectionUserControl), new PropertyMetadata(0));

        // Using a DependencyProperty as the backing store for MediaName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MediaNameProperty =
            DependencyProperty.Register("MediaName", typeof(int), typeof(MediaViewSelectionUserControl), new PropertyMetadata(0));

        // Using a DependencyProperty as the backing store for MediaImageSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MediaImageSourceProperty =
            DependencyProperty.Register("MediaImageSource", typeof(string), typeof(MediaViewSelectionUserControl), new PropertyMetadata(0));

        // Using a DependencyProperty as the backing store for MediaPreviewSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MediaPreviewSourceProperty =
            DependencyProperty.Register("MediaPreviewSource", typeof(string), typeof(MediaViewSelectionUserControl), new PropertyMetadata(0));
    }
}
