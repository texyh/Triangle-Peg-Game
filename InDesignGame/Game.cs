using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InDesignGame
{
    public class Game
    {
        public List<List<int>> grid;

        public Game()
        {
            InitializeGrid();
            var grid = SetStartingPostion(5);
            FindMove(grid);
        }

        public void InitializeGrid()
        {
            grid = new List<List<int>>(5);
            int count = 0;

            for (var i = 1; i <= 5; i ++)
            {
                var row = new List<int>(i);
                for (var x = 0; x <i; x++ )
                {
                    row.Add(count + 1);
                    count+=1;
                }
                Console.WriteLine(row);
                grid.Add(row);
            }
        }

        public List<List<int>> SetStartingPostion(int position)
        {
            return grid.Select(x =>
           {
               return x.Select(y =>
               {
                   if (y == position)
                   {
                       return 0;
                   }

                   return 1;
               }).ToList();
           }).ToList();
        }

        public void FindMove(List<List<int>> currentGrid)
        {
            List<int> currentRow;
            int currentCell;

            for(var i = 0; i < currentGrid.Count; i++)
            {
                currentRow = currentGrid[i];
                for(var j = 0; j < currentRow.Count; j ++)
                {
                    currentCell = currentRow[j];
                    Postion from;
                    Postion jumpedOver;
                    Postion to;

                    if(currentCell == 0) // empty
                    {
                        to = new Postion { RowIndex = i, Cell = j };

                        //check left
                        if (j-2 > -1 &&  currentRow[j-2] == 1 && currentRow[j-1] == 1)
                        {
                            from = new Postion { RowIndex = i, Cell = j - 2 };
                            jumpedOver = new Postion { RowIndex = i, Cell = j - 1 };
                            Move(currentGrid, from, to, jumpedOver);
                        }

                        //check right
                        if (j+2 < currentRow.Count && currentRow[j + 2] == 1 && currentRow[j + 1] == 1)
                        {
                            from = new Postion { RowIndex = i, Cell = j + 2 };
                            jumpedOver = new Postion { RowIndex = i, Cell = j + 1 };
                            Move(currentGrid, from, to, jumpedOver);

                        }

                        //checktop
                        if (i - 2 > -1 &&  currentGrid[i-2].Count > j && currentGrid[i - 2][j] == 1 && currentGrid[i -1][j] == 1 )
                        {
                            from = new Postion { RowIndex = i -2, Cell = j };
                            jumpedOver = new Postion { RowIndex = i-1, Cell = j };
                            Move(currentGrid, from, to, jumpedOver);

                        }

                        //checkbottom
                        if (i + 2 < currentGrid.Count && currentGrid[i + 2][j] == 1 && currentGrid[i + 1][j] == 1)
                        {
                            from = new Postion { RowIndex = i + 2, Cell = j };
                            jumpedOver = new Postion { RowIndex = i + 1, Cell = j };
                            Move(currentGrid, from, to, jumpedOver);
                        }

                        //checktopleft
                        if (i - 2 > -1 && j - 2 > -1 && currentGrid[i - 2][j-2] == 1 && currentGrid[i - 1][j-1] == 1)
                        {
                            from = new Postion { RowIndex = i - 2, Cell = j-2 };
                            jumpedOver = new Postion { RowIndex = i - 1, Cell = j-1};
                            Move(currentGrid, from, to, jumpedOver);
                        }

                       

                        //bottom right
                        if ( i + 2 < currentGrid.Count && currentGrid[i + 1].Count > j + 2 && currentGrid[i + 2][j + 2] == 1 && currentGrid[i + 1][j + 1] == 1)
                        {
                            from = new Postion { RowIndex = i + 2, Cell = j + 2 };
                            jumpedOver = new Postion { RowIndex = i + 1, Cell = j + 1 };
                            Move(currentGrid, from, to, jumpedOver);

                        }


                    }

                }
            }
        }

        public void Move(List<List<int>> currentGrid, Postion from, Postion to, Postion jumpeOver)
        {
            var gridCopy = currentGrid;

            gridCopy[from.RowIndex][from.Cell] = 0;
            gridCopy[to.RowIndex][to.Cell] = 1;
            gridCopy[jumpeOver.RowIndex][jumpeOver.Cell] = 0;

            var x = grid[from.RowIndex][from.Cell];
            var y = grid[to.RowIndex][to.Cell];
            if(x == 2 && y == 7)
            {
                Console.WriteLine($"{grid[from.RowIndex][from.Cell]} to {grid[to.RowIndex][to.Cell]}");
            }

            var pegsCount = GetPegCount(gridCopy);

            if(pegsCount == 1)
            {
                Console.WriteLine("Complete");
            } else
            {
                FindMove(gridCopy);
            }

        }

        private int GetPegCount(List<List<int>> gridCopy)
        {
            int count = 0;

            gridCopy.ForEach(x =>
            {
                x.ForEach(y =>
                {
                    if (y == 1)
                    {
                        count += 1;
                    }
                });
            });
            
            return count;
        }
    }
}
