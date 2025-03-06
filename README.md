# AOOPAssignment1 - Grid Button Application

## Overview
This application is built using Avalonia UI and allows users to interact with a dynamically generated grid of buttons. The grid is populated based on binary data from a text file, and each button represents either a `0` (black) or `1` (white). Users can toggle button colors, load a grid from a file, save the current grid, and perform horizontal and vertical flips.

## Features
- **Dynamic Grid Creation**: Generates a grid of buttons based on specified row and column dimensions.
- **Toggle Button States**: Clicking a button toggles its color between black and white, updating the underlying binary array.
- **File Handling**:
  - Load a grid from a `.txt` file.
  - Save the current grid state to a `.txt` file.
- **Grid Transformations**:
  - Flip the grid horizontally.
  - Flip the grid vertically.

## Installation & Usage
### Prerequisites
- .NET 6 or later
- Avalonia UI framework installed

### Running the Application
1. Clone the repository or download the project files.
2. Open the solution in an IDE that supports Avalonia (e.g., Visual Studio, JetBrains Rider).
3. Build and run the project.

### Loading a File
- Click the **Open File** button.
- Select a `.txt` file containing the grid dimensions followed by binary values (0s and 1s).

**File Format Example:**
```
5 5
0101011011001001111100000
```
- The first two numbers define the number of rows and columns.
- The remaining values define the grid contents.

### Saving a File
- Click the **Save File** button to save the current grid state to `grid.txt`.

### Flipping the Grid
- Click **Flip Horizontally** to mirror the grid left to right.
- Click **Flip Vertically** to mirror the grid top to bottom.

## Code Structure
- **`MainWindow.xaml.cs`**: Contains the logic for creating and managing the grid.
- **Methods:**
  - `CreateGrid()`: Initializes the grid.
  - `AddButtons()`: Adds buttons to the grid with appropriate colors.
  - `TinyButton_Click()`: Handles button clicks and toggles colors.
  - `OpenFile_Click()`: Reads a text file and populates the grid.
  - `SaveFile_Click()`: Saves the grid state to a file.
  - `Flip_Horizontally()`, `Flip_Vertically()`: Flip the grid layout.
  - `ConvertToJagged()`: Converts a 1D array into a jagged 2D array.

## Future Improvements
- Add a UI for selecting the file save location.
- Implement undo/redo functionality.
- Allow users to set custom grid sizes manually.

## License
This project is for educational purposes. Feel free to modify and expand it as needed.

## Author
Morten Lins
