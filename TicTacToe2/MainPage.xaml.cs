namespace TicTacToe2;

public partial class MainPage : ContentPage
{
    private int numbrows = 3;
    public MainPage()
    {
        InitializeComponent();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        CreateTheGrid();
    }

    private async void CreateTheGrid()
    {
        for (int i = 0; i < numbrows; ++i)
        {
            GridPageContent.AddRowDefinition(new RowDefinition());
        }

        for (int i = 0; i < numbrows; ++i)
        {
            GridPageContent.AddColumnDefinition(new ColumnDefinition());
        }

        for (int i = 0; i < numbrows; ++i)
        {
            for (int j = 0; j < numbrows; ++j)
            {
                Frame styledFrame = new Frame
                {
                    BackgroundColor = Color.FromRgb(255, 0, 0), // Set the background color
                    CornerRadius = 10, // Set rounded corners
                    HasShadow = true, // Add a shadow effect
                    Padding = new Thickness(10), // Add padding to the frame
                    BorderColor = Color.FromRgb(100, 100, 100)

                };
                GridPageContent.Add(styledFrame, j, i);
            }

        }
        //Bug in .NET version means we need this await Task before disabling the button
        await Task.Yield();
        mybtn.IsEnabled = false;
    }

    private void BtnMove_Clicked(object sender, EventArgs e)
    {
        int row, column;
        //Try Parse is another way to convert from string to integer, it checks whether the parse can work first instead of just crashing
        //int.TryParse(string, out) is the form of it
        //If it can parse it returns true and assigns the integer to the output variable
        //If it cannot parse it returns false and assigns 0 to the output variable
        if (!int.TryParse(EntryC.Text, out column) || !int.TryParse(EntryR.Text, out row))
        {
            //If either entry cannot be parsed, we exit out of the method by just using return;
            //No feedback will be given to the user
            return;
        }
        //We need to subtract one from each of column and row if it has got this far
        --column;
        --row;

        //Make sure we are within the limits of the grid
        if (column > numbrows || column < 0 || row > numbrows || row < 0)
            return;

        //We are going to do a loop over all the Children of the grid finding all the objects that are there, looking for a match
        foreach (var item in GridPageContent.Children)
        {
            //We only want to search Frames, so ignore all other types of items
            if (item.GetType() == typeof(Frame))
            {
                //Cast the object to type Frame so we can use all the Frame attributes and methods
                Frame frame = (Frame)item;

                //Search for a match, if we find one, do the move and exit out of the loop with break
                if (column == Convert.ToInt32(frame.GetValue(Grid.ColumnProperty).ToString()) && row == Convert.ToInt32(frame.GetValue(Grid.RowProperty).ToString()))
                {
                    DoMove(frame);
                    break;
                }
            }
        }
    }

    private void DoMove(Frame frame)
    {
        frame.BackgroundColor = Color.FromRgb(0, 255, 0);
    }


}
