namespace Game.Tasks.MedicalDisaster
{
    public record MedicalDisasterParameters
    {
        public readonly int valveCount;
        public readonly int minValveRotation;
        public readonly int maxValveRotation;

        public MedicalDisasterParameters(int valveCount, int minValveRotation, int maxValveRotation)
        {
            this.valveCount = valveCount;
            this.minValveRotation = minValveRotation;
            this.maxValveRotation = maxValveRotation;
        }
    }
}