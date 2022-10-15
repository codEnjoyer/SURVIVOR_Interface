namespace Assets.Scripts.Model
{
    public abstract class GunModule : Item
    {
        protected float DeltaRecoil;
        protected float DeltaAccuracy;
        protected float DeltaNoise;
        protected float DeltaAverageDistance;
        protected float DeltaDamage;
        protected float DeltaErgonomics;
    }
}