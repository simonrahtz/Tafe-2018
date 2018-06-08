﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public interface BoardUtilities
    {
        bool Obstructed(Point origin, Point destination);
    }

    public class Model : BoardUtilities, CommandHandler<ModelCommand>
    {
        private const int BoardRows = 8;
        private const int BoardColumns = 8;
        private CommandHandler<ViewCommand> commandHandler;
        private Square[,] board = new Square[BoardRows, BoardColumns];
        private Team currentPlayer;
        private Point? selectedSquare;

        public Model(CommandHandler<ViewCommand> commandHandler)
        {
            this.commandHandler = commandHandler;
            for (int i = 0; i < BoardRows; i++)
            {
                for (int j = 0; j < BoardColumns; j++)
                {
                    board[i, j] = new Square();
                }
            }
        }

        public void Handle(ModelCommand command)

        { command.execute(this);

        }


        public void Start()
        {
            InitPawns(1, Team.White);
            InitBackRow(0, Team.White);
            InitPawns(6, Team.Black);
            InitBackRow(7, Team.Black);
            UpdateAll();
        }

        private void UpdateAll()
        {
            Console.WriteLine("Updating all;");
            for (int i = 0; i < BoardRows; i++)
            {
                for (int j = 0; j < BoardColumns; j++)
                {
                    Update(new Point(i, j));
                }
            }
        }

        public bool Select(Point coord)
        {
            Piece piece = board[coord.X, coord.Y].piece;
            if ((piece != null) && (piece.Team == currentPlayer))
            {
                selectedSquare = coord;
                return true;
            }
            else if (selectedSquare != null)
            {
                bool success = Move(selectedSquare.Value, coord);
                selectedSquare = null;
                return success;
            }
            return false;
        }

        private bool Move(Point origin, Point destination)
        {
            Piece mover = board[origin.X, origin.Y].piece;
            Move move = mover.CanMove(this, origin, destination);
            if (move != null)
            {
                move.Execute();
                board[destination.X, destination.Y].piece = mover;
                board[origin.X, origin.Y].piece = null;
                Update(origin);
                Update(destination);
                currentPlayer = currentPlayer == Team.White ? Team.Black : Team.White;
                return true;
            }
            return false;
        }

        public bool Obstructed(Point origin, Point destination)
        {
            Point move = origin;
            Point oneBefore = destination;
            int xDir = Math.Sign(destination.X - origin.X);
            int yDir = Math.Sign(destination.Y - origin.Y);
            oneBefore.X -= xDir;
            oneBefore.Y -= yDir;
            while (move != oneBefore)
            {
                move.X += xDir;
                move.Y += yDir;
                if (board[move.X, move.Y].piece != null) { return true; }
            }
            return false;
        }

        private void InitBackRow(int row, Team team)
        {
            Place(0, row, new Rook(team));
            Place(1, row, new Knight(team));
            Place(2, row, new Bishop(team));
            Place(3, row, new Queen(team));
            Place(4, row, new King(team));
            Place(5, row, new Bishop(team));
            Place(6, row, new Knight(team));
            Place(7, row, new Rook(team));
            
            
        }

        private void InitPawns(int row, Team team)
        {
            for (int i = 0; i < 8; i++)
            {
                Place(i, row, new Pawn(team));
            }
        }

        private void Place(int col, int row, Piece piece)
        {
            board[row, col].piece = piece;
            Update(new Point(row, col));
        }

        private void Update(Point coord)
        {
            Piece piece = board[coord.X, coord.Y].piece;
            commandHandler.Handle(new DrawSquareCommand(coord, piece));
        }
    }
}
