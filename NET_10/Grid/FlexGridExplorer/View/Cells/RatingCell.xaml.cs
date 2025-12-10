using C1.Maui.Grid;
using System.Windows.Input;

namespace FlexGridExplorer;

public partial class RatingCell : ContentView
{
    const double OFF = 0.2;
    const double ON = 1.0;
    int _rating;

    public RatingCell()
	{
		InitializeComponent();

        ChangeStarCommand = new Command(OnChangeStar);

        BindingContext = this;
	}

    public int Rating 
    {
        get
        {
            return _rating;
        }
        internal set
        {
            _rating = value;
            OnPropertyChanged(nameof(Rating));
            OnPropertyChanged(nameof(Star1Opacity));
            OnPropertyChanged(nameof(Star2Opacity));
            OnPropertyChanged(nameof(Star3Opacity));
            OnPropertyChanged(nameof(Star4Opacity));
            OnPropertyChanged(nameof(Star5Opacity));
        }
    }
    
    public GridCellRange Range { get; internal set; }

    public double Star1Opacity
    {
        get
        {
            return ON;
        }
    }

    public double Star2Opacity
    {
        get
        {
            return Rating >= 1 ? ON : OFF;
        }
    }

    public double Star3Opacity
    {
        get
        {
            return Rating >= 2 ? ON : OFF;
        }
    }

    public double Star4Opacity
    {
        get
        {
            return Rating >= 3 ? ON : OFF;
        }
    }

    public double Star5Opacity
    {
        get
        {
            return Rating >= 4 ? ON : OFF;
        }
    }

    public ICommand ChangeStarCommand { get; set; }


    private void OnChangeStar(object obj)
    {
        Rating = int.Parse((string)obj);
    }
}