using C1.Maui.Calendar;
using CalendarExplorer.Resources;

namespace CalendarExplorer
{
    public partial class CustomDayContent
    {
        public CustomDayContent()
        {
            InitializeComponent();
            Title = AppResources.CustomDayContentTitle;
        }
    }

    public class CustomCalendarAdapter : CalendarAdapter
    {
        private Dictionary<DateTime, ImageSource> WeatherForecast = new Dictionary<DateTime, ImageSource>();
        private List<ImageSource> _icons = new List<ImageSource>();
        private Random _rand = new Random();


        public CustomCalendarAdapter()
        {
            _icons.Add(ImageSource.FromFile("partly_cloudy_day_icon.png"));
            _icons.Add(ImageSource.FromFile("sunny_icon.png"));
            _icons.Add(ImageSource.FromFile("rain_icon.png"));
            _icons.Add(ImageSource.FromFile("snow_icon.png"));
            _icons.Add(ImageSource.FromFile("thunder_lightning_storm_icon.png"));
            _icons.Add(ImageSource.FromFile("overcast_icon.png"));

            for (int i = 0; i < 10; i++)
            {
                WeatherForecast[DateTime.Today.AddDays(i)] = GetRandomIcon();
            }
        }

        private ImageSource GetRandomIcon()
        {
            return _icons[_rand.Next(0, _icons.Count - 1)];
        }

        public override object GetSlotKind(CalendarSlotInfo slotInfo)
        {
            if (slotInfo is CalendarDaySlotInfo daySlotInfo && !daySlotInfo.IsAdjacent && WeatherForecast.ContainsKey(daySlotInfo.Date))
                return typeof(CalendarImageDaySlot); //The days with forecast will use a different slot
            return base.GetSlotKind(slotInfo);
        }

        public override CalendarSlotPresenter CreateSlot(CalendarSlotInfo slotInfo, object slotKind)
        {
            if (slotKind as Type == typeof(CalendarImageDaySlot))
                return new CalendarSlotPresenter { Content = new CalendarImageDaySlot() };
            return base.CreateSlot(slotInfo, slotKind);
        }

        public override void PrepareSlot(CalendarSlotPresenter slot, CalendarSlotInfo slotInfo)
        {
            base.PrepareSlot(slot, slotInfo);

            if(slotInfo is CalendarDayOfWeekSlotInfo dayOfWeekSlotInfo)
            {
                if (!dayOfWeekSlotInfo.IsWeekend)
                {
                    slot.FontAttributes = FontAttributes.Bold;
                    slot.FontSize = 12;
                }
                else
                {
                    slot.FontAttributes = FontAttributes.Italic;
                    slot.FontSize = 12;
                    slot.Foreground = Colors.Red;
                }

            }
        }
        
        public override void BindSlot(CalendarSlotPresenter slot, CalendarSlotInfo slotInfo, object slotKind)
        {
            base.BindSlot(slot, slotInfo, slotKind);
            if (slotInfo is CalendarDaySlotInfo daySlotInfo)
            {
                if (slotKind == Calendar.DaySlotTemplate)
                {
                    slot.Content.BindingContext = new MyDataContext(daySlotInfo.Date);
                }
                if (slot.Content is CalendarImageDaySlot daySlotWithImage)
                {
                    daySlotWithImage.DayText = daySlotInfo.Date.Day + " ";
                    daySlotWithImage.DayFontSize = 12;
                    daySlotWithImage.Aspect = Aspect.AspectFit;
                    daySlotWithImage.Source = WeatherForecast[daySlotInfo.Date];
                }
            }
        }
    }

    public class MyDataContext
    {
        private static Random _rand = new Random();
        public MyDataContext(DateTime date)
        {
            Day = date.Day;
            RedDotVisible = Day % 3 == 0;
            GreenDotVisible = Day % 3 == 1;
            BlueDotVisible = Day % 3 == 2;
        }

        public int Day { get; set; }
        public bool RedDotVisible { get; set; }
        public bool GreenDotVisible { get; set; }
        public bool BlueDotVisible { get; set; }
    }
}
