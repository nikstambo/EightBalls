using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EightBalls {
    class Program {
        static void Main(string[] args) {

            List<Ball> balls = new List<Ball> {
            new Ball { Number=1, Weight = 10, Size=10},
            new Ball { Number=2, Weight = 10, Size=10},
            new Ball { Number=3, Weight = 10, Size=10},
            new Ball { Number=4, Weight = 10, Size=10},
            new Ball { Number=5, Weight = 11, Size=10},
            new Ball { Number=6, Weight = 10, Size=10},
            new Ball { Number=7, Weight = 10, Size=10},
            new Ball { Number=8, Weight = 10, Size=10},
            };

            IEnumerable<Ball> firstWeighResult;
            IEnumerable<Ball> secondWeighResult;

            // First weighing
            firstWeighResult = Scale.Weigh(balls.Take(3), balls.Skip(3).Take(3));

            // If first 6 balls are the same weight, go weigh the remaining 2 balls
            if (!firstWeighResult.Any()) {

                // Second weighing
                secondWeighResult = Scale.Weigh(balls.Skip(6).Take(1), balls.Skip(7));
                Scale.DisplayResult(secondWeighResult);

            // The heavy ball is one of the 3 balls on the heavier side
            } else {

                // Second weighing
                secondWeighResult = Scale.Weigh(firstWeighResult.Take(1), firstWeighResult.Skip(1).Take(1));

                // If the 2 selected balls (out of the 3 remaining) are equal in weight, then the remaining ball is the heaviest
                if (!secondWeighResult.Any()) {
                    Scale.DisplayResult(firstWeighResult.Skip(2));
                } else {
                    // If one of the 2 selected balls is heavier, return that.
                    Scale.DisplayResult(secondWeighResult);
                }
            }
        }
    }

    public class Ball {
            public int Number { get; set; }
            public decimal Weight { get; set; }
            public decimal Size { get; set; }
    }

    public class Scale {
        public static IEnumerable<Ball> Weigh(IEnumerable<Ball> leftScale, IEnumerable<Ball> rightScale) {
            decimal leftScaleWeight = leftScale.Sum(ls => ls.Weight);
            decimal rightScaleWeight = rightScale.Sum(rs => rs.Weight);

            // Return an empty list if both sides weigh equally
            if (leftScaleWeight == rightScaleWeight) {
                IEnumerable<Ball> temp = new List<Ball>();
                return temp;
            }

            return leftScaleWeight > rightScaleWeight ? leftScale : rightScale;
        }

        public static void DisplayResult(IEnumerable<Ball> result) {
            if (!result.Any()) {
                Console.WriteLine("All balls were the same weight");
            } else {
                Console.WriteLine(String.Format("Ball number {0} was the heaviest with weight = {1}",
                    result.Single().Number,
                    result.Single().Weight));
            }
        }
    }
}
