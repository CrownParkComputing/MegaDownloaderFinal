using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Telerik.Windows.Controls.QuickStart.Infrastructure
{
    public class NavigationService
    {
        private static readonly NavigationService instance = new NavigationService();
        private readonly Dictionary<ApplicationView, IView> cachedViews = new Dictionary<ApplicationView, IView>();
        private readonly IDictionary<ApplicationView, Type> viewAssociations = new Dictionary<ApplicationView, Type>();
        private IView currentView;
        private ContentControl frame = new ContentControl();

        private NavigationService()
        {
        }

        public event EventHandler PreviewNavigated;
        public event EventHandler Navigated;

        public static NavigationService Instance
        {
            get
            {
                return instance;
            }
        }

        /// <summary>
        /// Frame to use when navigating.
        /// </summary>
        public ContentControl Frame
        {
            get
            {
                return this.frame;
            }
            set
            {
                this.frame = value;
            }
        }

        public void AssociateViewWithType(ApplicationView view, Type viewType)
        {
            if (this.viewAssociations.ContainsKey(view))
            {
                this.viewAssociations.Remove(view);
            }

            this.viewAssociations.Add(view, viewType);
        }

        /// <summary>
        /// Navigate to the view, passing the specified parameter.
        /// <summary>
        /// <param name="view">ApplicationView value to navigate to</param>
        /// <param name="parameter">Parameter to pass to the view.</param>
        /// <returns>True if navigation is not canceled; otherwise, false.</returns>
        public bool Navigate(ApplicationView view, object parameter = null)
        {
            this.OnPreviewNavigated();
            //Type viewTypeToNavigate = this.viewAssociations[view];
            if (this.currentView != null)
            {
                this.currentView.OnNavigatedFrom(null);
            }

            IView viewToNavigate = this.GetViewInstance(view);
            this.frame.Content = viewToNavigate;
            this.currentView = viewToNavigate;
            this.currentView.OnNavigatedTo(parameter);
            this.OnNavigated();

            return true;
        }

        private void OnPreviewNavigated()
        {
            if (this.PreviewNavigated != null)
            {
                this.PreviewNavigated(this, EventArgs.Empty);
            }
        }

        private void OnNavigated()
        {
            if (this.Navigated != null)
            {
                this.Navigated(this, EventArgs.Empty);
            }
        }
  
        private IView GetViewInstance(ApplicationView applicationView)
        {
            if (this.cachedViews.ContainsKey(applicationView))
            {
                return this.cachedViews[applicationView];
            }

            Type typeToInstantiate = this.viewAssociations[applicationView];
            IView viewToNavigate = Activator.CreateInstance(typeToInstantiate) as IView;
            if (viewToNavigate == null)
            {
                throw new InvalidOperationException("View to navigate to should be IView");
            }

            this.cachedViews.Add(applicationView, viewToNavigate);
            return viewToNavigate;
        }
    }
}