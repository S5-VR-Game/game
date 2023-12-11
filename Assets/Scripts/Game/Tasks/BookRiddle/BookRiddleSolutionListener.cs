using UnityEngine;

namespace Game.Tasks.BookRiddle
{
    /// <summary>
    /// This class implements the whole logic that is needed to determine
    /// whether the book riddle is already done or not.
    /// </summary>
    public class BookRiddleSolutionListener : MonoBehaviour
    {
        private BookRiddleSolution solution;
        [SerializeField] private BookRiddle bookRiddle;

        private const int InitialRed = 0;
        private const int InitialBlue = 0;
        private const int InitialYellow = 0;
        private const int InitialGreen = 0;

        private const string RedString = "red_cover (UnityEngine.Material)";
        private const string BlueString = "blue_cover (UnityEngine.Material)";
        private const string YellowString = "yellow_cover (UnityEngine.Material)";
        private const string GreenString = "green_cover (UnityEngine.Material)";

        private int _currentRed = InitialRed;
        private int _currentBlue = InitialBlue;
        private int _currentYellow = InitialYellow;
        private int _currentGreen = InitialGreen;

        private void Update()
        {
            Debug.Log("Current Red: " + _currentRed);
            Debug.Log("Current Blue: " + _currentBlue);
            Debug.Log("Current Yellow: " + _currentYellow);
            Debug.Log("Current Green: " + _currentGreen);
        }

        /// <summary>
        /// Setter-Method for the Solution
        /// </summary>
        /// <param name="newSolution">The current Solution of this riddle</param>
        public void SetBookRiddleSolution(BookRiddleSolution newSolution)
        {
            solution = newSolution;
            Debug.Log(newSolution);
            Debug.Log(solution);
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
            Debug.Log(solution);
            var solutionMap = solution.GetSolutionMap();
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