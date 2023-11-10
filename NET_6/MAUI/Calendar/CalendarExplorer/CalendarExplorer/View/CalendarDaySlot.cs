using System;
using C1.Maui.Core;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace CalendarExplorer
{
    /// <summary>
    /// Represents the base class for defining a day slot visual element.
    /// </summary>
    public abstract class CalendarDaySlotBase : Grid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CalendarDaySlotBase"/> class.
        /// </summary>
        public CalendarDaySlotBase()
        {
            InitializeUI();
        }

        /// <summary>
        /// Initializes the UI.
        /// </summary>
        protected abstract void InitializeUI();
    }

    /// <summary>
    /// Represents the default day slot visual element
    /// </summary>
    public class CalendarDaySlot : CalendarDaySlotBase
    {
        #region ** dependency properties
#pragma warning disable 1591

        public static readonly BindableProperty DayTextProperty = BindableProperty.Create(nameof(DayText), typeof(string), typeof(CalendarDaySlot), null);
        public static readonly BindableProperty DayTextColorProperty = BindableProperty.Create(nameof(DayTextColor), typeof(Color), typeof(CalendarDaySlot), Colors.Black);
        public static readonly BindableProperty DayFontAttributesProperty = BindableProperty.Create(nameof(DayFontAttributes), typeof(FontAttributes), typeof(CalendarDaySlot), FontAttributes.None);
        public static readonly BindableProperty DayFontSizeProperty = BindableProperty.Create(nameof(DayFontSize), typeof(double), typeof(CalendarDaySlot), 15.0);
        public static readonly BindableProperty DayFontFamilyProperty = BindableProperty.Create(nameof(DayFontFamily), typeof(string), typeof(CalendarDaySlot), null);
        public static readonly BindableProperty DayHorizontalAlignmentProperty = BindableProperty.Create(nameof(DayHorizontalAlignment), typeof(LayoutOptions), typeof(CalendarDaySlot), LayoutOptions.Center);
        public static readonly BindableProperty DayVerticalAlignmentProperty = BindableProperty.Create(nameof(DayVerticalAlignment), typeof(LayoutOptions), typeof(CalendarDaySlot), LayoutOptions.Center);

#pragma warning restore 1591
        #endregion

        #region ** implementation

        /// <summary>
        /// Initializes a new instance of the <see cref="CalendarDaySlot"/> class.
        /// </summary>
        public CalendarDaySlot()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CalendarDaySlot"/> class.
        /// </summary>
        /// <param name="date">The date associated to this day slot.</param>
        public CalendarDaySlot(DateTime date)
        {
            DayText = date.Day.ToString();
        }

        /// <summary>
        /// Initializes the UI.
        /// </summary>
        protected override void InitializeUI()
        {
            var text = new Label();
            text.Padding = new Thickness(2);
            text.SetBinding(Label.TextProperty, new Binding("DayText"));
            text.SetBinding(Label.TextColorProperty, new Binding("DayTextColor"));
            text.SetBinding(Label.FontAttributesProperty, new Binding("DayFontAttributes"));
            text.SetBinding(Label.FontSizeProperty, new Binding("DayFontSize"));
            text.SetBinding(Label.FontFamilyProperty, new Binding("DayFontFamily"));
            text.SetBinding(Label.HorizontalOptionsProperty, new Binding("DayHorizontalAlignment"));
            text.SetBinding(Label.VerticalOptionsProperty, new Binding("DayVerticalAlignment"));
            text.BindingContext = this;
            Children.Add(text);
        }

        #endregion

        #region ** object model

        /// <summary>
        /// Gets or sets the day displayed in the day slot.
        /// </summary>
        public string DayText
        {
            get
            {
                return (string)GetValue(DayTextProperty);
            }
            set
            {
                SetValue(DayTextProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the day text.
        /// </summary>
        public Color DayTextColor
        {
            get
            {
                return (Color)GetValue(DayTextColorProperty);
            }
            set
            {
                SetValue(DayTextColorProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the day font attributes.
        /// </summary>
        public FontAttributes DayFontAttributes
        {
            get
            {
                return (FontAttributes)GetValue(DayFontAttributesProperty);
            }
            set
            {
                SetValue(DayFontAttributesProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the size of the day font.
        /// </summary>
        public double DayFontSize
        {
            get
            {
                return (double)GetValue(DayFontSizeProperty);
            }
            set
            {
                SetValue(DayFontSizeProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the day font family.
        /// </summary>
        public string DayFontFamily
        {
            get
            {
                return (string)GetValue(DayFontFamilyProperty);
            }
            set
            {
                SetValue(DayFontFamilyProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the day horizontal options.
        /// </summary>
        public LayoutOptions DayHorizontalAlignment
        {
            get
            {
                return (LayoutOptions)GetValue(DayHorizontalAlignmentProperty);
            }
            set
            {
                SetValue(DayHorizontalAlignmentProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the day vertical options.
        /// </summary>
        public LayoutOptions DayVerticalAlignment
        {
            get
            {
                return (LayoutOptions)GetValue(DayVerticalAlignmentProperty);
            }
            set
            {
                SetValue(DayVerticalAlignmentProperty, value);
            }
        }

        #endregion
    }

    /// <summary>
    /// Represents a day slot containing an image
    /// </summary>
    public class CalendarImageDaySlot : CalendarDaySlot
    {
        #region ** dependency properties
#pragma warning disable 1591

        public static readonly BindableProperty SourceProperty = BindableProperty.Create(nameof(Source), typeof(ImageSource), typeof(CalendarImageDaySlot), null);
        public static readonly BindableProperty AspectProperty = BindableProperty.Create(nameof(Aspect), typeof(Aspect), typeof(CalendarImageDaySlot), Aspect.AspectFit);

#pragma warning restore 1591
        #endregion

        #region ** implementation

        /// <summary>
        /// Initializes a new instance of the <see cref="CalendarImageDaySlot"/> class.
        /// </summary>
        public CalendarImageDaySlot() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CalendarImageDaySlot"/> class.
        /// </summary>
        /// <param name="date">The date associated to this day slot.</param>
        public CalendarImageDaySlot(DateTime date) : base(date)
        {
        }

        /// <summary>
        /// Initializes the UI.
        /// </summary>
        protected override void InitializeUI()
        {
            DayHorizontalAlignment = LayoutOptions.Start;
            DayVerticalAlignment = LayoutOptions.Start;
            var image = new Image();
            image.SetBinding(Image.SourceProperty, new Binding("Source"));
            image.SetBinding(Image.AspectProperty, new Binding("Aspect"));
            image.BindingContext = this;
            Children.Add(image);
            base.InitializeUI();
        }

        #endregion

        #region ** object model

        /// <summary>
        /// Gets or sets the image source displayed in the day slot.
        /// </summary>
        public ImageSource Source
        {
            get
            {
                return (ImageSource)GetValue(SourceProperty);
            }
            set
            {
                SetValue(SourceProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the aspect of the image displayed in the day slot.
        /// </summary>
        public Aspect Aspect
        {
            get
            {
                return (Aspect)GetValue(AspectProperty);
            }
            set
            {
                SetValue(AspectProperty, value);
            }
        }

        #endregion
    }
}
