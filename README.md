# LiteBroker
A NET Core implementation of a simple broker-based pub/sub framework

If you want to subscribe:
~~~
this.Subscribe<SomeClass>(x => {
    // Do something here.
});

// Or

Broker.Subscribe<SomeClass>(this, x => {
    // Do something here.
});
~~~
You can also subscribe to interfaces and base classes and receive notifications when their implementations publish an event.

Unsubscribing is just as easy:
~~~
this.Unsubscribe<SomeClass>();

// Or

Broker.Unsubscribe<SomeClass>(this);
~~~
To publish an object:
~~~
var sc = new SomeClass();
sc.Publish();

// Or

Broker.Publish(sc);
~~~