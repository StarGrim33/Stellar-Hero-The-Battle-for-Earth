namespace Enemy
{
    public class TargetDieTransition : Transition
    {
        private void Update()
        {
            if (!Target.IsAlive)
                NeedTransit = true;
        }
    }
}
