using System;
using LiteBroker;
using Xunit;

namespace LiteBroker
{
    public class Tests
    {
        public Tests()
        {
            //cBroker.Subscriptions.Clear();
        }

        [Fact]
        public void SubscriptionsAreInvokedOneTime() 
        {
            var calls = 0;
            var publisher = "publisher";
            this.Subscribe<string>(s => { calls++; });
            publisher.Publish();
            this.Unsubscribe<string>();

            Assert.True(calls == 1);
        }

        [Fact]
        public void CanSubscribeToInterfaces()
        {
            var works = false;
            var publisher = new Foo();
            this.Subscribe<IFoo>(s => { works = s.DoFoo(); });

            publisher.Publish();
            this.Unsubscribe<IFoo>();

            Assert.True(works);            
        }

        [Fact]
        public void CanSubscribeToAbstractClasses()
        {
            var works = false;
            var publisher = new IronBar();
            this.Subscribe<Bar>(s => { works = s.DoBar(); });

            publisher.Publish();
            this.Unsubscribe<Bar>();

            Assert.True(works);   
        }
    }
}
