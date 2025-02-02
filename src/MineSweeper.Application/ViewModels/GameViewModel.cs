﻿using System;

namespace MineSweeper.Application.ViewModels
{
    public class GameViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Rows { get; set; }

        public int Cols { get; set; }

        public int Mines { get; set; }
    }
}
