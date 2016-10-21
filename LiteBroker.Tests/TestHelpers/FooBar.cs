namespace LiteBroker
{
    class FooBar : Bar, IFoo
    {
        public bool DoFoo()
        {
            return true;
        }

        public override bool DoBar()
        {
            return true;
        }
    }
}