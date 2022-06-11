using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace SunTaiLibrary.Controls
{
    /// <summary>
    /// A Canvas which manages dragging/UpdateZOrder of the UIElements it contains.
    /// <seealso cref="https://www.codeproject.com/Articles/15354/Dragging-Elements-in-a-Canvas"/>
    /// </summary>
    public class DiagramCanvas : Canvas
    {
        #region Static Constructor

        static DiagramCanvas()
        {
            AllowDraggingProperty = DependencyProperty.Register(
                "AllowDragging",
                typeof(bool),
                typeof(DiagramCanvas),
                new PropertyMetadata(true, AllowDraggingPropertyChanged));

            AllowDragOutOfViewProperty = DependencyProperty.Register(
                "AllowDragOutOfView",
                typeof(bool),
                typeof(DiagramCanvas),
                new UIPropertyMetadata(false));

            CanBeDraggedProperty = DependencyProperty.RegisterAttached(
                "CanBeDragged",
                typeof(bool),
                typeof(DiagramCanvas),
                new UIPropertyMetadata(true));
        }

        private static void AllowDraggingPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        #endregion Static Constructor

        #region Constructor

        /// <summary>
        /// Initializes a new instance of drag DiagramCanvas.  UIElements in
        /// the DiagramCanvas will immediately be draggable by the user.
        /// </summary>
        public DiagramCanvas()
        {
            AddHandler(DiagramListBoxItem.AfterMouseLeftButtonDownEvent, new MouseButtonEventHandler(DiagramListBoxItem_AfterMouseLeftButtonDown));
        }

        #endregion Constructor

        #region Data

        // Stores a reference to the UIElement currently being dragged by the user.
        private UIElement elementBeingDragged;

        // Keeps track of where the mouse cursor was when a drag operation began.
        private Point origCursorLocation;

        // The offsets from the DragCanvas' edges when the drag operation began.
        private double origHorizOffset, origVertOffset;

        // Keeps track of which horizontal and vertical offset should be modified for the drag element.
        private bool modifyLeftOffset, modifyTopOffset;

        // True if a drag operation is underway, else false.
        private bool isDragInProgress;

        #endregion Data

        #region Attached Properties

        public static readonly DependencyProperty CanBeDraggedProperty;

        public static bool GetCanBeDragged(UIElement uiElement)
        {
            return uiElement != null && (bool)uiElement.GetValue(CanBeDraggedProperty);
        }

        public static void SetCanBeDragged(UIElement uiElement, bool value)
        {
            if (uiElement != null)
            {
                uiElement.SetValue(CanBeDraggedProperty, value);
            }
        }

        #endregion Attached Properties

        #region Dependency Properties

        public static readonly DependencyProperty AllowDraggingProperty;
        public static readonly DependencyProperty AllowDragOutOfViewProperty;

        #endregion Dependency Properties

        #region Interface

        #region AllowDragging

        /// <summary>
        /// Gets/sets whether elements in the DragCanvas should be draggable by the user.
        /// The default value is true.  This is a dependency property.
        /// </summary>
        public bool AllowDragging
        {
            get => (bool)GetValue(AllowDraggingProperty);
            set => SetValue(AllowDraggingProperty, value);
        }

        #endregion AllowDragging

        #region AllowDragOutOfView

        /// <summary>
        /// Gets/sets whether the user should be able to drag elements in the DragCanvas out of
        /// the viewable area.  The default value is false.  This is a dependency property.
        /// </summary>
        public bool AllowDragOutOfView
        {
            get => (bool)GetValue(AllowDragOutOfViewProperty);
            set => SetValue(AllowDragOutOfViewProperty, value);
        }

        #endregion AllowDragOutOfView

        #region BringToFront / SendToBack

        /// <summary>
        /// Assigns the element a z-index which will ensure that
        /// it is in front of every other element in the Canvas.
        /// The z-index of every element whose z-index is between
        /// the element's old and new z-index will have its z-index
        /// decremented by one.
        /// </summary>
        /// <param name="targetElement">
        /// The element to be sent to the front of the z-order.
        /// </param>
        public void BringToFront(UIElement element)
        {
            UpdateZOrder(element, true);
        }

        /// <summary>
        /// Assigns the element a z-index which will ensure that
        /// it is behind every other element in the Canvas.
        /// The z-index of every element whose z-index is between
        /// the element's old and new z-index will have its z-index
        /// incremented by one.
        /// </summary>
        /// <param name="targetElement">
        /// The element to be sent to the back of the z-order.
        /// </param>
        public void SendToBack(UIElement element)
        {
            UpdateZOrder(element, false);
        }

        #endregion BringToFront / SendToBack

        #region ElementBeingDragged

        /// <summary>
        /// Returns the UIElement currently being dragged, or null.
        /// </summary>
        /// <remarks>
        /// Note to inheritors: This property exposes a protected
        /// setter which should be used to modify the drag element.
        /// </remarks>
        public UIElement ElementBeingDragged
        {
            get => !AllowDragging ? null : elementBeingDragged;
            protected set
            {
                if (elementBeingDragged != null)
                {
                    elementBeingDragged.ReleaseMouseCapture();
                }

                if (!AllowDragging)
                {
                    elementBeingDragged = null;
                }
                else
                {
                    if (GetCanBeDragged(value))
                    {
                        elementBeingDragged = value;
                        elementBeingDragged.CaptureMouse();
                    }
                    else
                    {
                        elementBeingDragged = null;
                    }
                }
            }
        }

        #endregion ElementBeingDragged

        #region FindCanvasChild

        /// <summary>
        /// Walks up the visual tree starting with the specified DependencyObject,
        /// looking for a UIElement which is a child of the Canvas.  If a suitable
        /// element is not found, null is returned.  If the 'depObj' object is a
        /// UIElement in the Canvas's Children collection, it will be returned.
        /// </summary>
        /// <param name="depObj">
        /// A DependencyObject from which the search begins.
        /// </param>
        public UIElement FindCanvasChild(DependencyObject depObj)
        {
            while (depObj != null)
            {
                // If the current object is a UIElement which is a child of the
                // Canvas, exit the loop and return it.
                if (depObj is UIElement elem && Children.Contains(elem))
                {
                    break;
                }

                // VisualTreeHelper works with objects of type Visual or Visual3D.
                // If the current object is not derived from Visual or Visual3D,
                // then use the LogicalTreeHelper to find the parent element.
                depObj = depObj is Visual or Visual3D ? VisualTreeHelper.GetParent(depObj) : LogicalTreeHelper.GetParent(depObj);
            }
            return depObj as UIElement;
        }

        #endregion FindCanvasChild

        #endregion Interface

        #region Overrides

        // just work for DiagramListBoxItem
        private void DiagramListBoxItem_AfterMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OnMouseLeftButtonDown(e);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            isDragInProgress = false;

            // Cache the mouse cursor location.
            origCursorLocation = e.GetPosition(this);

            // Walk up the visual tree from the element that was clicked,
            // looking for an element that is a direct child of the Canvas.
            ElementBeingDragged = FindCanvasChild(e.OriginalSource as DependencyObject);
            if (ElementBeingDragged == null)
            {
                return;
            }

            // Get the element's offsets from the four sides of the Canvas.
            double left = GetLeft(ElementBeingDragged);
            double right = GetRight(ElementBeingDragged);
            double top = GetTop(ElementBeingDragged);
            double bottom = GetBottom(ElementBeingDragged);

            // Calculate the offset deltas and determine for which sides
            // of the Canvas to adjust the offsets.
            origHorizOffset = ResolveOffset(left, right, out modifyLeftOffset);
            origVertOffset = ResolveOffset(top, bottom, out modifyTopOffset);

            // Set the Handled flag so that a control being dragged
            // does not react to the mouse input.
            e.Handled = true;

            isDragInProgress = true;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            // If no element is being dragged, there is nothing to do.
            if (ElementBeingDragged == null || !isDragInProgress)
            {
                return;
            }

            // Get the position of the mouse cursor, relative to the Canvas.
            Point cursorLocation = e.GetPosition(this);

            // These values will store the new offsets of the drag element.
            double newHorizontalOffset, newVerticalOffset;

            #region Calculate Offsets

            // Determine the horizontal offset.
            newHorizontalOffset = modifyLeftOffset
                ? origHorizOffset + (cursorLocation.X - origCursorLocation.X)
                : origHorizOffset - (cursorLocation.X - origCursorLocation.X);

            // Determine the vertical offset.
            newVerticalOffset = modifyTopOffset
                ? origVertOffset + (cursorLocation.Y - origCursorLocation.Y)
                : origVertOffset - (cursorLocation.Y - origCursorLocation.Y);

            #endregion Calculate Offsets

            if (!AllowDragOutOfView)
            {
                #region Verify Drag Element Location

                // Get the bounding rect of the drag element.
                Rect elemRect = CalculateDragElementRect(newHorizontalOffset, newVerticalOffset);

                //
                // If the element is being dragged out of the viewable area,
                // determine the ideal rect location, so that the element is
                // within the edge(s) of the canvas.
                //
                bool leftAlign = elemRect.Left < 0;
                bool rightAlign = elemRect.Right > ActualWidth;

                if (leftAlign)
                {
                    newHorizontalOffset = modifyLeftOffset ? 0 : ActualWidth - elemRect.Width;
                }
                else if (rightAlign)
                {
                    newHorizontalOffset = modifyLeftOffset ? ActualWidth - elemRect.Width : 0;
                }

                bool topAlign = elemRect.Top < 0;
                bool bottomAlign = elemRect.Bottom > ActualHeight;

                if (topAlign)
                {
                    newVerticalOffset = modifyTopOffset ? 0 : ActualHeight - elemRect.Height;
                }
                else if (bottomAlign)
                {
                    newVerticalOffset = modifyTopOffset ? ActualHeight - elemRect.Height : 0;
                }

                #endregion Verify Drag Element Location
            }

            #region Move Drag Element

            if (modifyLeftOffset)
            {
                SetLeft(ElementBeingDragged, newHorizontalOffset);
            }
            else
            {
                SetRight(ElementBeingDragged, newHorizontalOffset);
            }

            if (modifyTopOffset)
            {
                SetTop(ElementBeingDragged, newVerticalOffset);
            }
            else
            {
                SetBottom(ElementBeingDragged, newVerticalOffset);
            }

            #endregion Move Drag Element
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);

            // Reset the field whether the left or right mouse button was
            // released, in case a context menu was opened on the drag element.
            ElementBeingDragged = null;
        }

        #endregion Overrides

        #region Private Helpers

        /// <summary>
        /// Returns a Rect which describes the bounds of the element being dragged.
        /// </summary>
        private Rect CalculateDragElementRect(double newHorizOffset, double newVertOffset)
        {
            if (ElementBeingDragged == null)
            {
                throw new InvalidOperationException("ElementBeingDragged is null.");
            }

            Size elemSize = ElementBeingDragged.RenderSize;

            double x, y;

            x = modifyLeftOffset ? newHorizOffset : ActualWidth - newHorizOffset - elemSize.Width;

            y = modifyTopOffset ? newVertOffset : ActualHeight - newVertOffset - elemSize.Height;

            var elemLoc = new Point(x, y);

            return new Rect(elemLoc, elemSize);
        }

        /// <summary>
        /// Determines one component of a UIElement's location
        /// within a Canvas (either the horizontal or vertical offset).
        /// </summary>
        /// <param name="side1">
        /// The value of an offset relative to a default side of the
        /// Canvas (i.e. top or left).
        /// </param>
        /// <param name="side2">
        /// The value of the offset relative to the other side of the
        /// Canvas (i.e. bottom or right).
        /// </param>
        /// <param name="useSide1">
        /// Will be set to true if the returned value should be used
        /// for the offset from the side represented by the 'side1'
        /// parameter.  Otherwise, it will be set to false.
        /// </param>
        private static double ResolveOffset(double side1, double side2, out bool useSide1)
        {
            // If the Canvas.Left and Canvas.Right attached properties
            // are specified for an element, the 'Left' value is honored.
            // The 'Top' value is honored if both Canvas.Top and
            // Canvas.Bottom are set on the same element.  If one
            // of those attached properties is not set on an element,
            // the default value is Double.NaN.
            useSide1 = true;
            double result;
            if (Double.IsNaN(side1))
            {
                if (Double.IsNaN(side2))
                {
                    // Both sides have no value, so set the
                    // first side to a value of zero.
                    result = 0;
                }
                else
                {
                    result = side2;
                    useSide1 = false;
                }
            }
            else
            {
                result = side1;
            }
            return result;
        }

        /// <summary>
        /// Helper method used by the BringToFront and SendToBack methods.
        /// </summary>
        /// <param name="element">
        /// The element to bring to the front or send to the back.
        /// </param>
        /// <param name="bringToFront">
        /// Pass true if calling from BringToFront, else false.
        /// </param>
        private void UpdateZOrder(UIElement element, bool bringToFront)
        {
            #region Safety Check

            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            if (!Children.Contains(element))
            {
                throw new ArgumentException("Must be a child element of the Canvas.", "element");
            }

            #endregion Safety Check

            #region Calculate Z-Indici And Offset

            // Determine the Z-Index for the target UIElement.
            int elementNewZIndex = -1;
            if (bringToFront)
            {
                foreach (UIElement elem in Children)
                {
                    if (elem.Visibility != Visibility.Collapsed)
                    {
                        ++elementNewZIndex;
                    }
                }
            }
            else
            {
                elementNewZIndex = 0;
            }

            // Determine if the other UIElements' Z-Index
            // should be raised or lowered by one.
            int offset = (elementNewZIndex == 0) ? +1 : -1;

            int elementCurrentZIndex = GetZIndex(element);

            #endregion Calculate Z-Indici And Offset

            #region Update Z-Indici

            // Update the Z-Index of every UIElement in the Canvas.
            foreach (UIElement childElement in Children)
            {
                if (childElement == element)
                {
                    SetZIndex(element, elementNewZIndex);
                }
                else
                {
                    int zIndex = GetZIndex(childElement);

                    // Only modify the z-index of an element if it is
                    // in between the target element's old and new z-index.
                    if ((bringToFront && elementCurrentZIndex < zIndex) ||
                        (!bringToFront && zIndex < elementCurrentZIndex))
                    {
                        SetZIndex(childElement, zIndex + offset);
                    }
                }
            }

            #endregion Update Z-Indici
        }

        #endregion Private Helpers
    }
}