using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HubControlScroll
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //MyHub.ScrollToSection(MyHub.SectionsInView.FirstOrDefault());
        }

        private void Hub_SectionHeaderClick(object sender, HubSectionHeaderClickEventArgs e)
        {
            Frame.Navigate(typeof(MyPage1));

            ScrollViewer myscrollviewer = FindControl<ScrollViewer>(MyHub);

            if (myscrollviewer != null)
            {
                double horiOffset = (double)myscrollviewer.GetValue(ScrollViewer.HorizontalOffsetProperty);
                double vertOffset = (double)myscrollviewer.GetValue(ScrollViewer.VerticalOffsetProperty);
            }

            DisplayVisualTree(MyHub, 0);
            VisualTreeHelper.GetParent(MyHubSection);


            ScrollViewer childScrollViewer = findChildScrollViewer(MyHub);
            if (childScrollViewer != null)
            {
                double horizOffset = (double)childScrollViewer.GetValue(ScrollViewer.HorizontalOffsetProperty);
                double vertOffset = (double)childScrollViewer.GetValue(ScrollViewer.VerticalOffsetProperty);
            }
        }


        private ScrollViewer findChildScrollViewer(DependencyObject toSearch)
        {
            Queue<DependencyObject> children = new Queue<DependencyObject>();
            children.Enqueue(toSearch);
            while (children.Count != 0)
            {
                DependencyObject dequeued = children.Dequeue();
                if (dequeued is ScrollViewer)
                {
                    return (ScrollViewer)dequeued;
                }
                else
                {
                    for (int index = 0; index < VisualTreeHelper.GetChildrenCount(dequeued); index++)
                    {
                        children.Enqueue(VisualTreeHelper.GetChild(dequeued, index));
                    }
                }
            }
            return null;
        }


        private void DisplayVisualTree(DependencyObject control, int indent)
        {
            string tab = "";
            for (int i = 1; i <= indent; i++)
                tab += "t";


            System.Diagnostics.Debug.WriteLine(string.Format("{0}{1}",
                tab, control.ToString()));

            int childNumber = VisualTreeHelper.GetChildrenCount(control);

            for (int i = 0; i < childNumber; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(control, i);
                if (child is Button)
                {
                    Button BT = (Button)child;
                }

                DisplayVisualTree(child, indent + 1);
            }
        }

        public static T FindControl<T>(DependencyObject root) where T : class
        {
            var MyQueue = new Queue<DependencyObject>();
            MyQueue.Enqueue(root);
            while (MyQueue.Count > 0)
            {
                DependencyObject current = MyQueue.Dequeue();
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(current); i++)
                {
                    var child = VisualTreeHelper.GetChild(current, i);
                    var typedChild = child as T;
                    if (typedChild != null)
                    {
                        return typedChild;
                    }
                    MyQueue.Enqueue(child);
                }
            }
            return null;
        }



        private void GetScrollView(DependencyObject control)
        {
            var parent = control as DependencyObject;
            while (!(parent is ScrollViewer))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            var gv = (parent as ScrollViewer);

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
