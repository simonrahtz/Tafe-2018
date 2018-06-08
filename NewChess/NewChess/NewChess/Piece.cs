﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Chess
{
    public class Square
    {
        public static System.Drawing.Image background;
        public Piece piece;

        public Square()
        {
            if (background == null)
            {
                background = Image.FromFile("Empty.png");
            }
        }
    }

    public enum Team { White, Black };

    public abstract class Piece
    {
        private Team team;
        protected List<Move> moves;
        public Image image;

        public Team Team
        {
            get { return team; }
        }

        public Piece(Team team, string file)
        {
            this.team = team;
            moves = new List<Move>();
            image = Image.FromFile(file + "_" + team.ToString().ToCharArray()[0] + ".png");
        }

        public virtual Move CanMove(BoardUtilities utils, Point origin, Point destination)
        {
            Move move = GetValidMove(origin, destination);
            if (utils.Obstructed(origin, destination))
            {
                move = null;
            }
            return move;
        }

        protected Move GetValidMove(Point origin, Point destination)
        {
            foreach (Move move in moves)
            {
                if (move.isValid(origin, destination))
                {
                    return move;
                }
            }
            return null;
        }
    }

    public class Rook : Piece
    {
        public Rook(Team team)
            : base(team, "Rook")
        {
            moves.Add(new MoveLinear());
        }
    }

    public class Pawn : Piece
    {
        public Pawn(Team team)
            : base(team, "Pawn")
        {
            moves.Add(new MoveLinearOnce(1, this));
            moves.Add(new MoveLinearOnce(2, this));
        }

        public void FirstMove()
        {
            moves.Clear();
            moves.Add(new MoveLinear(1));
        }
    }

    public class Bishop : Piece
    {
        public Bishop(Team team)
            : base(team, "Bishop")
        {
            moves.Add(new MoveDiagonal());
        }
    }

    public class Knight : Piece
    {
        public Knight(Team team) : base(team, "Knight")
        {
            moves.Add(new MoveOrthogonal());
        }
        public override Move CanMove(BoardUtilities utils, Point origin, Point destination)
        {
            return base.GetValidMove(origin, destination);
        }
    }

    public class King : Piece
    {
        public King(Team team)
            : base(team, "King")
        {
            moves.Add(new MoveDiagonal(1));
            moves.Add(new MoveLinear(1));
        }
    }

    public class Queen : Piece
    {
        public Queen(Team team)
            : base(team, "Queen")
        {
            moves.Add(new MoveDiagonal());
            moves.Add(new MoveLinear());
        }
    }
}
