﻿using UnityEngine;
using System.Collections;

namespace CaveGeneration
{
    [System.Serializable]
    /// <summary>
    /// Convenience class for passing parameters for the map generator across interfaces.
    /// </summary>
    public class MapParameters
    {
        [Tooltip(Tooltips.MAP_LENGTH)]
        [SerializeField] int length;
        public int Length
        {
            get { return length; }
            set { SetLength(value); }
        }

        [Tooltip(Tooltips.MAP_WIDTH)]
        [SerializeField] int width;
        public int Width
        {
            get { return width; }
            set { SetWidth(value); }
        }

        [Tooltip(Tooltips.MAP_DENSITY)]
        [Range(MINIMUM_MAP_DENSITY, MAXIMUM_MAP_DENSITY)]
        [SerializeField] float initialMapDensity;
        public float InitialDensity
        {
            get { return initialMapDensity; }
            set { SetMapDensity(value); }
        }

        [Tooltip(Tooltips.MAP_FLOOR_EXPANSION)]
        [SerializeField] int floorExpansion;
        public int FloorExpansion
        {
            get { return floorExpansion; }
            set { SetFloorExpansion(value); }
        }

        [Tooltip(Tooltips.MAP_SEED)]
        [SerializeField] string seed;
        public string Seed
        {
            get
            {
                seed = GetSeed();
                return seed;
            }
            set { seed = value; }
        }

        [Tooltip(Tooltips.MAP_USE_RANDOM_SEED)]
        [SerializeField] bool useRandomSeed;
        public bool UseRandomSeed
        {
            get { return useRandomSeed; }
            set { useRandomSeed = value; }
        }

        [Tooltip(Tooltips.MAP_BORDER_SIZE)]
        [SerializeField] int borderSize;
        public int BorderSize
        {
            get { return borderSize; }
            set { SetBorderSize(value); }
        }

        [Tooltip(Tooltips.MAP_SQUARE_SIZE)]
        [SerializeField] int squareSize;
        public int SquareSize
        {
            get { return squareSize; }
            set { SetSquareSize(value); }
        }

        [Tooltip(Tooltips.MAP_MIN_WALL_SIZE)]
        [SerializeField] int minWallSize;
        public int MinWallSize
        {
            get { return minWallSize; }
            set { minWallSize = value; }
        }

        [Tooltip(Tooltips.MAP_MIN_FLOOR_SIZE)]
        [SerializeField] int minFloorSize;
        public int MinFloorSize
        {
            get { return minFloorSize; }
            set { minFloorSize = value; }
        }

        const int MINIMUM_LENGTH = 5;
        const int MINIMUM_WIDTH = 5;
        const int MINIMUM_BORDER_SIZE = 0;
        const int MINIMUM_SQUARE_SIZE = 1;
        const int MINIMUM_FLOOR_EXPANSION = 0;
        const float MINIMUM_MAP_DENSITY = 0.3f;
        const float MAXIMUM_MAP_DENSITY = 0.7f;

        const string DEFAULT_SEED = "";
        const int DEFAULT_LENGTH = 75;
        const int DEFAULT_WIDTH = 75;
        const float DEFAULT_DENSITY = 0.5f;
        const bool DEFAULT_SEED_STATUS = true;
        const int DEFAULT_BORDER_SIZE = 0;
        const int DEFAULT_FLOOR_EXPANSION = 0;
        const int DEFAULT_SQUARE_SIZE = 1;
        const int DEFAULT_WALL_THRESHOLD = 50;
        const int DEFAULT_FLOOR_THRESHOLD = 50;

        public MapParameters()
        {
            Reset();
        }
        
        void Reset()
        {
            length = DEFAULT_LENGTH;
            width = DEFAULT_WIDTH;
            initialMapDensity = DEFAULT_DENSITY;
            floorExpansion = DEFAULT_FLOOR_EXPANSION;
            useRandomSeed = DEFAULT_SEED_STATUS;
            seed = DEFAULT_SEED;
            borderSize = DEFAULT_BORDER_SIZE;
            squareSize = DEFAULT_SQUARE_SIZE;
            minWallSize = DEFAULT_WALL_THRESHOLD;
            minFloorSize = DEFAULT_FLOOR_THRESHOLD;
        }

        string GetSeed()
        {
            if (UseRandomSeed)
            {
                return System.Environment.TickCount.ToString();
            }
            else
            {
                return seed;
            }
        }

        public void OnValidate()
        {
            if (length < MINIMUM_LENGTH)
            {
                length = MINIMUM_LENGTH;
            }
            if (width < MINIMUM_WIDTH)
            {
                width = MINIMUM_WIDTH;
            }
            if (floorExpansion < MINIMUM_FLOOR_EXPANSION)
            {
                floorExpansion = MINIMUM_FLOOR_EXPANSION;
            }
            if (squareSize < MINIMUM_SQUARE_SIZE)
            {
                squareSize = MINIMUM_SQUARE_SIZE;
            }
            if (borderSize < MINIMUM_BORDER_SIZE)
            {
                borderSize = MINIMUM_BORDER_SIZE;
            }
        }

        void SetLength(int value)
        {
            SetParameter(ref length, value, MINIMUM_LENGTH, int.MaxValue);
        }

        void SetWidth(int value)
        {
            SetParameter(ref width, value, MINIMUM_WIDTH, int.MaxValue);
        }

        void SetBorderSize(int value)
        {
            SetParameter(ref borderSize, value, MINIMUM_BORDER_SIZE, int.MaxValue);
        }

        void SetFloorExpansion(int value)
        {
            SetParameter(ref floorExpansion, value, MINIMUM_FLOOR_EXPANSION, int.MaxValue);
        }

        void SetSquareSize(int value)
        {
            SetParameter(ref squareSize, value, MINIMUM_SQUARE_SIZE, int.MaxValue);
        }

        void SetMapDensity(float value)
        {
            SetParameter(ref initialMapDensity, value, MINIMUM_MAP_DENSITY, MAXIMUM_MAP_DENSITY);
        }

        // This method is used when assigning values to the cavegenerator through code.
        void SetParameter<T>(ref T parameter, T value, T minimum, T maximum) where T: System.IComparable<T>
        {
            bool tooSmall = value.CompareTo(minimum) == -1;
            bool tooBig = value.CompareTo(maximum) == 1;
            if (tooSmall)
            {
                throw new System.ArgumentException("Parameter " + parameter + " must be at least " + minimum + ".");
            }
            else if (tooBig)
            {
                throw new System.ArgumentException("Parameter " + parameter + " must be at most " + maximum + ".");
            }
            else
            {
                parameter = value;
            }
        }
    } 
}
