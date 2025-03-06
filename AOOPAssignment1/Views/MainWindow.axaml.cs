using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Media;

namespace AOOPAssignment1.Views;

public partial class MainWindow : Window
{
    private Grid buttonGrid;
    private int[][] binaryArr;
    private int row;
    private int col;

    public MainWindow()
    {
        InitializeComponent();
    }

    // Creates a grid dynamically and populates it with buttons
    private void CreateGrid()
    {
        Panel.Children.Remove(buttonGrid);
        buttonGrid = new Grid();

        for (int i = 0; i < row; i++)
            buttonGrid.RowDefinitions.Add(new RowDefinition(GridLength.Auto));
        for (int j = 0; j < col; j++)
            buttonGrid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Auto));
        
        Panel.Children.Add(buttonGrid);
        AddButtons();
    }

    // Adds buttons to the grid, setting their colors based on binaryArr values
    private void AddButtons()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                var color = binaryArr[i][j] == 0 ? Brushes.Black : Brushes.White;
                var tinyButton = new Button
                {
                    Width = 16,
                    Height = 16,
                    Tag = (i, j), // Store row and column index in Tag
                    Background = color
                };
                tinyButton.Click += TinyButton_Click;

                Grid.SetRow(tinyButton, i);
                Grid.SetColumn(tinyButton, j);
                buttonGrid.Children.Add(tinyButton);
            }
        }
    }

    // Handles button clicks, toggling color and updating binaryArr
    private void TinyButton_Click(object? sender, RoutedEventArgs e)
    {
        if (sender is Button btn && btn.Tag is ValueTuple<int, int> indices)
        {
            (int x, int y) = indices;
            bool isWhite = btn.Background == Brushes.White;
            btn.Background = isWhite ? Brushes.Black : Brushes.White;
            binaryArr[x][y] = isWhite ? 0 : 1;
        }
    }

    // Opens a text file and reads grid dimensions and binary values
    private async void OpenFile_Click(object? sender, RoutedEventArgs e)
    {
        var dialog = new OpenFileDialog
        {
            Title = "Select a Text File",
            AllowMultiple = false,
            Filters = { new FileDialogFilter { Name = "Text Files", Extensions = { "txt" } } }
        };

        var result = await dialog.ShowAsync(this);
        if (result != null && result.Length > 0)
        {
            try
            {
                string[] lines = await File.ReadAllLinesAsync(result[0]);
                var values = lines.SelectMany(line => line.Split()).ToArray();
                
                if (values.Length < 3 || !int.TryParse(values[0], out row) || !int.TryParse(values[1], out col))
                    return;
                
                string binaryData = string.Concat(values.Skip(2));
                binaryArr = ConvertToJagged(binaryData.Select(c => c - '0').ToArray(), row, col);
                CreateGrid();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    // Saves the current grid state to a text file
    private void SaveFile_Click(object? sender, RoutedEventArgs e)
    {
        using StreamWriter writer = new("grid.txt");
        writer.WriteLine(row);
        writer.WriteLine(col);
        foreach (var line in binaryArr)
            writer.Write(string.Join("", line));
    }

    // Flips the grid horizontally
    private void Flip_Horizontally(object? sender, RoutedEventArgs e)
    {
        for (int i = 0; i < row; i++)
            Array.Reverse(binaryArr[i]);
        CreateGrid();
    }

    // Flips the grid vertically
    private void Flip_Vertically(object? sender, RoutedEventArgs e)
    {
        Array.Reverse(binaryArr);
        CreateGrid();
    }

    // Converts a 1D array to a jagged 2D array
    private static int[][] ConvertToJagged(int[] flatArray, int rows, int cols)
    {
        return Enumerable.Range(0, rows)
                         .Select(i => flatArray.Skip(i * cols).Take(cols).ToArray())
                         .ToArray();
    }
}
