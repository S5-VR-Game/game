using UnityEngine;

namespace Game.Tasks.BookRiddle
{
    /// <summary>
    /// This class implements the whole logic that is needed to determine
    /// whether the book riddle is already done or not.
    /// </summary>
    public class BookRiddleSolutionListener : MonoBehaviour
    {
        private BookRiddleSolution _solution;
        [SerializeField] private BookRiddle bookRiddle;

        private const int InitialRed = 0;
        private const int InitialBlue = 0;
        private const int InitialYellow = 0;
        private const int InitialGreen = 0;

        private const string RedString = "red_cover";
        private const string BlueString = "blue_cover";
        private const string YellowString = "yellow_cover";
        private const string GreenString = "green_cover";

        private int _currentRed = InitialRed;
        private int _currentBlue = InitialBlue;
        private int _currentYellow = InitialYellow;
        private int _currentGreen = InitialGreen;

        /// <summary>
        /// Setter-Method for the Solution
        /// </summary>
        /// <param name="solution">The current Solution of this riddle</param>
        public void SetBookRiddleSolution(BookRiddleSolution solution)
        {
            _solution = solution;
        }

        public void IncrementRed()
        {
            _currentRed++;
        }
        
        public void IncrementBlue()
        {
            _currentBlue++;
        }
        
        public void IncrementYellow()
        {
            _currentYellow++;
        }
        
        public void IncrementGreen()
        {
            _currentGreen++;
        }

        public void FullReset()
        {
            ResetRed();
            ResetBlue();
            ResetYellow();
            ResetGreen();
        }

        public void ResetRed()
        {
            _currentRed = InitialRed;
        }
        
        public void ResetBlue()
        {
            _currentBlue = InitialBlue;
        }
        
        public void ResetYellow()
        {
            _currentYellow = InitialYellow;
        }
        
        public void ResetGreen()
        {
            _currentGreen = InitialGreen;
        }

        public void CompareSolution()
        {
            bookRiddle.SetBookRiddleState(SolutionIsCorrect() ? TaskState.Successful : TaskState.Failed);
        }

        /// <summary>
        /// Checks the numbers with the solution and determines whether it is correct or not
        /// </summary>
        /// <returns>true if given sequence is correct, false otherwise</returns>
        private bool SolutionIsCorrect()
        {
            var solutionMap = _solution.GetSolutionMap();
            var solutionRed = solutionMap[RedString];
            var solutionBlue = solutionMap[BlueString];
            var solutionYellow = solutionMap[YellowString];
            var solutionGreen = solutionMap[GreenString];

            return solutionRed == _currentRed
                   && solutionBlue == _currentBlue
                   && solutionYellow == _currentYellow
                   && solutionGreen == _currentGreen;
        }
    }
}