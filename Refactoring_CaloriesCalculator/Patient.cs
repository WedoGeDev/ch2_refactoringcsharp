using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring_CaloriesCalculator
{
    public class Patient
    {
        public string SSN { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double HeightInInches { get; set; }
        public double WeightInPounds { get; set; }
        public double Age { get; set; }
        public Gender Gender { get; set; }

        public double DailyCaloriesRecommended()
        {
            return Gender == Gender.Female
                ? DailyCaloriesRecommendedFemale()
                : DailyCaloriesRecommendedMale();
        }

        public double IdealBodyWeight()
        {
            return Gender == Gender.Female
                ? IdealBodyWeightFemale()
                : IdealBodyWeightMale();
        }

        public double DistanceFromIdealWeight()
        {
            return Gender == Gender.Female
                ? DistanceFromIdealWeightFemale()
                : DistanceFromIdealWeightMale();
        }

        private double IdealBodyWeightFemale()
        {
            return (45.5 + (2.3 * (HeightInInches - 60))) * 2.2046;
        }

        private double IdealBodyWeightMale()
        {
            return (50 + (2.3 * (HeightInInches - 60))) * 2.2046;
        }
        private double DailyCaloriesRecommendedFemale()
        {
            return (655
                    + (4.3 * WeightInPounds)
                    + (4.7 * HeightInInches)
                    - 4.7 * Age);
        }

        private double DailyCaloriesRecommendedMale()
        {
            return (66
                    + (6.3 * WeightInPounds)
                    + (12.9 * HeightInInches)
                    - 6.8 * Age);
        }

        private double DistanceFromIdealWeightFemale()
        {
            return WeightInPounds - IdealBodyWeightFemale();
        }

        private double DistanceFromIdealWeightMale()
        {
            return WeightInPounds - IdealBodyWeightMale();
        }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
