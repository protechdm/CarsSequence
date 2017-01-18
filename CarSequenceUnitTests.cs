using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace CarsSequenceUnitTests
{
    [TestFixture]
    public class LongestCarTwoColourSequenceTests
    {
        [Test]
        public void when_no_cars()
        {
            Assert.AreEqual(0, CalculateLongestSequenceOfTwoCarColours(new string[0]));
        }

        [Test]
        public void when_one_car()
        {
            Assert.AreEqual(1, CalculateLongestSequenceOfTwoCarColours(new[] { "Red" }));
        }

        [Test]
        public void when_two_identical_cars()
        {
            Assert.AreEqual(2, CalculateLongestSequenceOfTwoCarColours(new[] { "Red", "Red" }));
        }

        [Test]
        public void when_two_different_cars()
        {
            Assert.AreEqual(2, CalculateLongestSequenceOfTwoCarColours(new[] { "Red", "Blue" }));
        }

        // Your first failing test has been written for you here
        [Test]
        public void when_three_different_cars()
        {
            Assert.AreEqual(2, CalculateLongestSequenceOfTwoCarColours(new[] { "Red", "Blue", "Green" }));
        }

        // add more tests, feel free to refactor all the tests as you see fit
        [Test]
        public void when_two_sequences_with_two_identical_cars()
        {
            Assert.AreEqual(4, CalculateLongestSequenceOfTwoCarColours(new[] { "Red", "Green", "Red", "Green" }));
        }

        [Test]
        public void when_two_sequences_first_sequence_is_larger_than_second()
        {
            Assert.AreEqual(5, CalculateLongestSequenceOfTwoCarColours(new[] { "Red", "Green", "Red", "Green", "Yellow", "Red", "Yellow", "Red", "Yellow" }));
        }

        [Test]
        public void when_two_sequences_second_sequence_is_larger_than_first()
        {
            Assert.AreEqual(5, CalculateLongestSequenceOfTwoCarColours(new[] { "Blue", "Red", "Blue", "Red", "Green", "Yellow", "Yellow", "Green", "Yellow" }));
        }

        [Test]
        public void when_null_cars_source()
        {
            Assert.Throws<Exception>(() => CalculateLongestSequenceOfTwoCarColours(null));
        }

        [Test]
        public void when_source_contains_null_values_throws_exception()
        {
            Assert.Throws<Exception>(() => CalculateLongestSequenceOfTwoCarColours(new[] { null, "Red", null, "Red", null, "Yellow", "Yellow", "Red", "Yellow" }));
        }

        [Test]
        public void when_two_identical_cars_and_then_a_different_colour_car()
        {
            Assert.AreEqual(3, CalculateLongestSequenceOfTwoCarColours(new[] { "Red", "Red", "Blue" }));
        }

        [Test]
        public void when_ten_identical_cars()
        {
            Assert.AreEqual(10, CalculateLongestSequenceOfTwoCarColours(new[] { "Red", "Red", "Red", "Red", "Red", "Red", "Red", "Red", "Red", "Red"}));
        }

        [Test]
        public void when_red_cars_spelt_incorrectly()
        {
            Assert.AreEqual(3, CalculateLongestSequenceOfTwoCarColours(new[] { "Red", "Redd", "Blue", "Red", "Redd", "Blue", "Red", "Purple", "Red", "Redd" }));
        }

        [Test]
        public void when_longest_sequence_is_last_3_cars()
        {
            Assert.AreEqual(3, CalculateLongestSequenceOfTwoCarColours(new[] { "1", "2", "3", "2" }));
        }
        /// <summary>
        /// Given a sequence of car colours, returns the length of longest streak containing just two colours.
        /// See read me for more information and examples.
        /// Complete this method however you wish.
        /// You may call other functions and classes, but please do not change this methods signature.
        /// </summary>
        /// <param name="carColours"></param>
        /// <returns></returns>
        public static int CalculateLongestSequenceOfTwoCarColours(IEnumerable<string> carColours)
        {
            //return SequenceMethods.Calculate(SequenceMethods.GetSequenceCount, carColours);
            return SequenceMethods.Calculate(SequenceMethods.GetLongestStreakContainingJustTwoColours, carColours);
        }
    }


    static class SequenceMethods
    {
        public delegate int DoCalculationDelegate(IEnumerable<string> _source);

        public static int Calculate(DoCalculationDelegate del, IEnumerable<string> _source)
        {
            return del(_source);
        }
        public static int GetLongestStreakContainingJustTwoColours(IEnumerable<string> _source)
        {
            if (_source == null)
            {
                throw new Exception("Source colours can not be null");
            }
            if (_source.Any(x => string.IsNullOrEmpty(x)))
            {
                throw new Exception("Source can not contain null or empty colours");
            }

            int _result = 0;
            int _sourceElementsCount = _source.Count();
            int _startPos = 0;
            string firstCarInSequenceColour = _source.Skip(_startPos).FirstOrDefault();
            string secondCarInSequenceColour = _source.Skip(_startPos).FirstOrDefault(x => x != firstCarInSequenceColour);

            if (_sourceElementsCount < 2)
            {
                return _sourceElementsCount;
            }
            if (secondCarInSequenceColour == null) //source is 2 cars with same colour - semantics of business requirements?
            {
                return _sourceElementsCount;
            }

            while (_startPos < (_sourceElementsCount-1))
            {
                int count = _source.Skip(_startPos).TakeWhile(x => x == firstCarInSequenceColour || x == secondCarInSequenceColour).Count();
                _startPos += count > 0 ? count - 1 : _sourceElementsCount;
                _result = Math.Max(_result, count);
                if (_startPos < (_sourceElementsCount-1))
                {
                    firstCarInSequenceColour = _source.Skip(_startPos).FirstOrDefault();
                    secondCarInSequenceColour = _source.Skip(_startPos+1).FirstOrDefault();
                }
            }
            return _result;
        }

        public static int GetSequenceCount(IEnumerable<string> _source)
        {
            return _source.Count();
        }
    }
}