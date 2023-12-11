using System;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFDataGridScrollPerf;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    
    public int rowsCount = 500;
    public int colsCount = 500;
    
    public DataTable dataTable = new();
    public DataView dataView => dataTable.DefaultView;
    
    public MainWindow()
    {
        InitializeComponent();
        AddColumns();
        AddRows();
        GenerateData();
    }
    
    public void AddColumns()
    {
        // add 50 columns
        for (int i = 0; i < colsCount; i++)
        {
            dataTable.Columns.Add(i.ToString(), typeof(int));
        }
        
    }

    public void AddRows()
    {
        // add 50 rows
        for (int i = 0; i < rowsCount; i++)
        {
            dataTable.Rows.Add();
        }
    }

    public void GenerateData()
    {
        var size = 4;
        var drawStart = 25;
        for (var i = 0; i < colsCount; i++)
        {
            // get random direction, 1 or -1
            var direction = new Random().Next(0, 2) == 0 ? 1 : -1;

            // check if drawStart is out of bounds, if so change direction
            if (drawStart < 0 + size && direction == -1 || drawStart > rowsCount - size && direction == 1)
            {
                direction *= -1;
            }

            // add a row at start
            dataTable.Rows[drawStart][i] = 1;

            // based on direction add size rows
            for (var j = 0; j < size; j++)
            {
                dataTable.Rows[drawStart + (j * direction)][i] = 1;
            }

            drawStart += size * direction;
        }
    }
    
}